using System;

namespace DataMigration
{
    [Flags]
    public enum DbSteps
    {
        None = 0,
        All = ~None,
        EnsureDatabase = 1,
        ResetDatabase = 2,
        UpgradeSchema = 4,
        ForIntegrationTests = ResetDatabase | UpgradeSchema,
        ForLocalDevelopmentEnvironment = EnsureDatabase | UpgradeSchema,
        ForAzureDevelopmentEnvironment = ResetDatabase | UpgradeSchema,
        ForProduction = UpgradeSchema
    }
}