using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Tests.Helpers
{
    class TestHelper
    {
        public static void Clean(string name)
        {
            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings[name].ConnectionString);
            var cmd = conn.CreateCommand();

            var cmds = new String[] {
                "EXEC sp_MSForEachTable 'DISABLE TRIGGER ALL ON ?'",
                "EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'",
                "EXEC sp_MSForEachTable 'SET QUOTED_IDENTIFIER ON; DELETE FROM ?'",
                "EXEC sp_MSForEachTable 'ALTER TABLE ? CHECK CONSTRAINT ALL'",
                "EXEC sp_MSForEachTable 'ENABLE TRIGGER ALL ON ?'"
            };

            cmd.Connection.Open();
            foreach (var scmd in cmds)
            {
                cmd.CommandText = scmd;
                cmd.ExecuteNonQuery();
            }
            cmd.Connection.Close();
        }
        public static void RunScript(string name, string script)
        {
            var sconn = ConfigurationManager.ConnectionStrings[name].ConnectionString;
            var scriptText = System.IO.File.ReadAllText(script);
            var sqlConnection = new SqlConnection(sconn);
            var svrConnection = new ServerConnection(sqlConnection);
            var server = new Server(svrConnection);
            server.ConnectionContext.ExecuteNonQuery(scriptText);
        }
    }
}
