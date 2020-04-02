using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text.RegularExpressions;
using DbUp;
using DbUp.Helpers;
using Polly;
using Serilog;

namespace DataMigration
{
    internal class Migrator
    { 
        private static readonly TimeSpan ScriptTimeout = TimeSpan.FromMinutes(5);

        private readonly string _connectionString;
        private readonly DbSteps _dbSteps;
        private readonly string _dbUserPW;
        private readonly UpgradeLogger _upgradeLogger;

        public Migrator(string connectionString, DbSteps dbSteps, string dbUserPW)
        {
            _connectionString = connectionString;
            _dbSteps = dbSteps;
            _dbUserPW = dbUserPW;
            _upgradeLogger = new UpgradeLogger();
        }

        private bool ShouldEnsureDatabaseExists => (_dbSteps & DbSteps.EnsureDatabase) == DbSteps.EnsureDatabase;
        private bool ShouldResetDatabase => (_dbSteps & DbSteps.ResetDatabase) == DbSteps.ResetDatabase;
        private bool ShouldUpgradeSchema => (_dbSteps & DbSteps.UpgradeSchema) == DbSteps.UpgradeSchema;

        public void ExecuteMigrations()
        {
            if (ShouldEnsureDatabaseExists)
                ExecuteOperationAndLogFailures(EnsureDatabaseExists);

            if (ShouldResetDatabase)
                ExecuteOperationAndLogFailures(ZeroDatabase);

            if (ShouldUpgradeSchema)
                ExecuteOperationAndLogFailures(ExecuteSchemaUpgradeScripts);
        }

        private void ExecuteOperationAndLogFailures(Action migrationFunction)
        {
            try
            {
                migrationFunction();
            }
            catch (Exception e)
            {
                Log.Error(e, "An exeception ocurred executing migrations against database with connection string {connectionstring}. Exception thrown was {error}", _connectionString, e.Message);
                throw;
            }
        }

        private void ZeroDatabase()
        {
            Log.Information("Resetting database...");

            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                var command = dbConnection.CreateCommand();
                command.CommandText = SqlScripts.ZeroDb();
                command.ExecuteNonQuery();
            }
        }

        private void EnsureDatabaseExists()
        {
            Log.Information("Ensuring database exists...");

            Policy.Handle<SqlException>()
                .WaitAndRetry(new[] { TimeSpan.FromSeconds(15), TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(45) },
                    (retry, seconds) => Log.Warning("Sql Server is not ready yet, retry after {seconds}", seconds))
                .Execute(() => EnsureDatabase.For.SqlDatabase(_connectionString));
        }

        private void ExecuteSchemaUpgradeScripts()
        {
            Log.Information("Executing schema upgrade...");
            //ProcessMasterDbScripts();
            ProcessSqlScripts(_connectionString, s => !s.Contains("Master"));
        }

        private void ProcessSqlScripts(string connectionString, Func<string, bool> scriptFilter, bool withNullJournal = false)
        {
            var deployChangesSetup = DeployChanges.To.SqlDatabase(connectionString)
                .WithVariable("password", _dbUserPW)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), scriptFilter)
                .WithTransaction()
                .LogTo(_upgradeLogger)
                .WithExecutionTimeout(ScriptTimeout);

            if (withNullJournal)
            {
                deployChangesSetup.JournalTo(new NullJournal());
            }

            var schemaUpgrader = deployChangesSetup.Build();

            var result = schemaUpgrader.PerformUpgrade();

            if (!result.Successful)
                throw new MigrationException("Falied to execute schema upgrade.", result.Error);
        }

        private void ProcessMasterDbScripts()
        {
            var masterConnectionString = Regex.Replace(_connectionString, @"(?<assignTo>Database|Catalog)=\w*[-]*\w*", "${assignTo}=Master");

            ProcessSqlScripts(masterConnectionString, s => s.Contains("master") || s.Contains("Master"), true);
        }
    }
}
