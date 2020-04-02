using System;
using System.Collections.Generic;
using System.Text;

namespace DataMigration
{
    internal class MigrationOptions
    {
        public string ConnectionString { get; set; }
        public string Environment { get; set; } = "Local";
        public string Password { get; set; }
    }
}
