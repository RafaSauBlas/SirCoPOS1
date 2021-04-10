using SirCoPOS.DataAccess.SirCo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess
{
    public partial class SirCoDataContext : BaseDataContext
    {
        public SirCoDataContext()
            : base("SirCo")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(new EntityFramework.Functions.FunctionConvention<SirCoDataContext>());
            //modelBuilder.DbFunction(typeof(MyContext), nameof(MyContext.Foo));
        }
        public virtual DbSet<Articulo> Articulos { get; set; }
        public virtual DbSet<Serie> Series { get; set; }
        public virtual DbSet<Corrida> Corridas { get; set; }
        public virtual DbSet<Medida> Medida { get; set; }
        public virtual DbSet<DetalleGasto> DetalleGastos { get; set; }
        public virtual DbSet<Gasto> Gastos { get; set; }
    }
}
