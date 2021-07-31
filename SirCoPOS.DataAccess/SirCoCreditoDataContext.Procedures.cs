using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using EntityFramework.Functions;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Core.Objects;

namespace SirCoPOS.DataAccess
{
    public partial class SirCoCreditoDataContext
    {

        public int updatecliente()
        {
            var cmd =
@"UPDATE [dbo].[Cliente] SET [sexo] = 'F'
WHERE [idcliente] = 15543320";
            SqlParameter[] param = {
                //new SqlParameter("@serie", 15543320),
                //new SqlParameter("@idusuario", idusuario),
                //new SqlParameter("@status", "F"),
                //new SqlParameter("@statusCurrent", statusCurrent.ToString())
            };
            return this.ExecuteCommand(cmd, param);
        }
    }
}