using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SirCoPOS.DataAccess.SirCo;

namespace SirCoPOS.BusinessLogic.Entities
{
    class ProductoPromocion
    {
        public int? CustomOrder { get; set; }
        public int? Group { get; set; }
        public int Order { get; set; }
        public string Key { get; set; }
        public Common.Entities.SerieFormasPago SerieFormaPago { get; set; }
        public Serie Serie { get; internal set; }
        public Corrida Corrida { get; internal set; }
        public int AgrupacionId { get; set; }
    }
}
