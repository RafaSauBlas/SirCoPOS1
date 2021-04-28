using SirCoPOS.Utilities.DataAccess.DataObjects;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Win.DataAccess
{
    class DataContext : DbContext
    {
        public DataContext()
            : base("MyDB")
        {
            
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        public virtual DbSet<Venta> Ventas { get; set; }
        public virtual DbSet<VentaArticulo> VentaArticulos { get; set; }
        public virtual DbSet<VentaCupon> VentaCupones { get; set; }
        public virtual DbSet<VentaPago> VentaPagos { get; set; }
    }
}
