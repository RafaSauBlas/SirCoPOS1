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
    public class Procedimientos
    {
        DataAccess.SirCoDataContext ctx = new DataAccess.SirCoDataContext();
        public int UpdateCliInfo()
        {
            var cmd =
@"UPDATE [dbo].[articulosest] SET [descripc] = 'jajjasjjuas'
WHERE [idarticulo] = 1";
            SqlParameter[] param = { };
            return ctx.ExecuteCommand(cmd, param);
        }
    }
}
