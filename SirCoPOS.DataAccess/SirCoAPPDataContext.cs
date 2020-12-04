using SirCoPOS.DataAccess.SirCoAPP;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess
{
    public class SirCoAPPDataContext : DbContext
    {
        public SirCoAPPDataContext()
            : base("SirCoAPP")
        {

        }
        public virtual DbSet<EmpleadosImg> EmpleadosImgs { get; set; }
        public virtual DbSet<ValeDigital> ValesDigital { get; set; }
        public virtual DbSet<Dinero> Dineros { get; set; }
        public virtual DbSet<DineroDetalle> DinerosDetalle { get; set; }
    }
}
