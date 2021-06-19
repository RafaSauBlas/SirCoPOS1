using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess
{
    public class BaseDataContext : DbContext
    {
        public BaseDataContext(string cs)
            : base(cs)
        {

        }
        #region Commands
        private string CreateCommand(string spname, params SqlParameter[] param)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0}", spname);
            if (param.Any())
            {
                var q = param.Select(i => String.Format("{0} = {0}", i.ParameterName));
                sb.Append(" " + String.Join(",", q));
            }
            var res = sb.ToString();
            return res;
        }
        protected int ExecuteCommand(string cmd, params SqlParameter[] param)
        {
            var res = this.Database.ExecuteSqlCommand(cmd, param);
            return res;

        }
        protected int ExecuteStoreCommand(string spname, params SqlParameter[] param)
        {
            var cmd = CreateCommand(spname, param);
            //Esta linea de codigo tiene que ver con el error de SirCoPOS
            var res = this.Database.ExecuteSqlCommand(cmd, param);
            return res;

        }
        protected IEnumerable<T> ExecuteStoreQuery<T>(string spname, params SqlParameter[] param)
        {
            //var cmd = CreateCommand(spname, param);
            //var res = this.Database.SqlQuery<T>(cmd, param);
            var res = this.Database.SqlQuery<T>(spname, param).ToList();
            return res;
        }
        protected IEnumerable<T> ExecuteQuery<T>(string query, params SqlParameter[] param)
        {
            var res = this.Database.SqlQuery<T>(query, param);
            return res;

        }
        #endregion
    }
}
