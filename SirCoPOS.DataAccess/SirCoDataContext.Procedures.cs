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
    public partial class SirCoDataContext
    {
        public int UpdateSerieStatus(string serie, Common.Constants.Status status, Common.Constants.Status statusCurrent, int idusuario)
        {
            var cmd =
@"UPDATE [dbo].[serie] SET [status] = @status, [idusuariocaja] = @idusuario, [fechacaja] = GETDATE()
WHERE [serie] = @serie AND [status] = @statusCurrent";
            SqlParameter[] param = {
                new SqlParameter("@serie", serie),
                new SqlParameter("@idusuario", idusuario),
                new SqlParameter("@status", status.ToString()),
                new SqlParameter("@statusCurrent", statusCurrent.ToString())
            };
            return this.ExecuteCommand(cmd, param);
        }

        [Function(FunctionType.ComposableScalarValuedFunction, "ValorMedida", Schema = "dbo")]
        [return: Parameter(DbType = "varchar")]
        public string ValorMedida(
            [Parameter(DbType = "varchar")]
            string medida
        ) => Function.CallNotSupported<string>();
    }
}