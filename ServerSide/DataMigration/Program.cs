using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace DataMigration
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new CommandLineParser<MigrationOptions>().Parse(args);
            var config = GetConfiguration();
            var connectionString = config.GetConnectionString("Ticketing");
            var dbUserPassword = options.Password ?? config["DbUser:Password"];
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config.GetSection("Logging"))
                .CreateLogger();

            var dbSteps = GetDbStepsForEnvironment(options.Environment?.ToLowerInvariant());

            var migrator = new Migrator(connectionString,dbSteps, dbUserPassword);

            migrator.ExecuteMigrations();
           
        }

        private static DbSteps GetDbStepsForEnvironment(string environmentName)
        {
            return environmentName switch
            {
                "dev" => DbSteps.ForAzureDevelopmentEnvironment,
                "prod" => DbSteps.ForProduction,
                "local" => DbSteps.ForLocalDevelopmentEnvironment,
                _ => throw new ArgumentException("Environment must be one of 'dev', 'prod' or 'local'", nameof(environmentName)),
            };
        }

        private static IConfigurationRoot GetConfiguration()
        {
            var devEnvironmentVariable = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");
            //var isDevelopment = string.IsNullOrEmpty(devEnvironmentVariable) ||
                                //devEnvironmentVariable.ToLower() == "development";

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            //if (isDevelopment)
            //{
            //    builder.AddUserSecrets()
            //}

            var config = builder.Build();
            return config;
        }
    }
}
