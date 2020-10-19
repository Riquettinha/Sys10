using System;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using Microsoft.Extensions.Configuration;
using Sys10.Data.Programmability.Functions;
using Sys10.Data.Programmability.Stored_Procedures;

namespace Sys10.Data.Context
{
    public partial class Sys10Context : DbContext, ISys10Context
    {
        public Guid UserId;
        public const string ContextName = "Sys10Context";

        public Sys10Context(DbConnection connection) : base(connection, true)
        {
            StoredProcedures = new StoredProcedures(this);
            ScalarValuedFunctions = new ScalarValuedFunctions(this);
            TableValuedFunctions = new TableValuedFunctions(this);
        }

        public Sys10Context() : base(GetConnectionString(ContextName))
        {
            StoredProcedures = new StoredProcedures(this);
            ScalarValuedFunctions = new ScalarValuedFunctions(this);
            TableValuedFunctions = new TableValuedFunctions(this);
        }

        public static string GetConnectionString(string connectionString)
        {
            var defaultConnection = ConfigurationManager.ConnectionStrings[connectionString]?.ConnectionString;
            var coreConnection = GetCoreConnectionString();
            var connection = defaultConnection ?? coreConnection ?? connectionString;

            return connection;
        }

        private static string GetCoreConnectionString()
        {
            var appSettingsPath = Directory.GetCurrentDirectory() + "/appsettings.json";

            if (File.Exists(appSettingsPath))
            {
                var builder = new ConfigurationBuilder().AddJsonFile(appSettingsPath);
                var configuration = builder.Build();
                return configuration.GetConnectionString("Sys10Context");
            }

            return "";
        }

        public int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}



