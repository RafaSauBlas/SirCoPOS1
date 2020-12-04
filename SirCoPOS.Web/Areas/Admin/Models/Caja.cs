using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SirCoPOS.Web.Areas.Admin.Models
{
    public class Caja
    {
        public DataAccess.SirCoPOS.Caja Item { get; set; }
        public int? Fondo { get; set; }
        public int? Responsable { get; set; }
    }
}