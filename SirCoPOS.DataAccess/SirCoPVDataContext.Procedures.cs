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
    public partial class SirCoPVDataContext
    {
        public IEnumerable<Common.Entities.Agrupacion> GetAgrupacionesPorSerie(string serie)
        {
            string cmdText = "usp_TraerAgrupacionesPorSerie @serie";

            SqlParameter[] param = {
                new SqlParameter("@serie", serie)
            };

            return this.ExecuteStoreQuery<Common.Entities.Agrupacion>(cmdText, param);
        }
    }
}
