using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess
{
    public class SirCoLogsDataContext : BaseDataContext
    {
        public SirCoLogsDataContext()
            : base("SirCoLogs")
        { 
            
        }
        public virtual DbSet<SirCoLogs.Log> Logs { get; set; }
        public virtual DbSet<SirCoLogs.FaultMessage> FaultMessages { get; set; }
        public virtual DbSet<SirCoLogs.Message> Messages { get; set; }
        public void ClearLog()
        {
            this.ExecuteCommand("DELETE FROM [dbo].[Log]");
        }
        public void AddLog(
            string application, 
            string machineName, 	
            string level,
            string logger,
            string callsite,
            string type,
            string module,
            string message,
            string exception)
        {
            this.ExecuteStoreCommand("[dbo].[AddLog]", 
                new SqlParameter("@application", application),
                new SqlParameter("@machineName", machineName),
                new SqlParameter("@level", level),
                new SqlParameter("@logger", logger),
                new SqlParameter("@callsite", callsite),
                new SqlParameter("@type", type),
                new SqlParameter("@module", module),
                new SqlParameter("@message", message),
                new SqlParameter("@exception", exception));            
        }
    }
}
