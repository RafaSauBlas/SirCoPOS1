using SirCoPOS.DataAccess.SirCoControl;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess
{
    public class SirCoControlDataContext : DbContext
    {
        public SirCoControlDataContext()
            : base("SirCoControl")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Colonia>()
            //    .HasRequired(i => i.Ciudad)
            //    .WithMany(i => i.Colonias)
            //    //.HasForeignKey(i => new { i.idciudad, i.idestado });
            //    .HasForeignKey(i => new { i.idestado, i.idciudad });
        }
        public virtual DbSet<Sucursal> Sucursales { get; set; }
        public virtual DbSet<Plaza> Plazas { get; set; }
        public virtual DbSet<Estado> Estados { get; set; }
        public virtual DbSet<Ciudad> Ciudades { get; set; }
        public virtual DbSet<Colonia> Colonias { get; set; }
        public virtual DbSet<Parametro> Parametros { get; set; }
        public virtual DbSet<NegocioExterno> NegociosExternos { get; set; }
        public virtual DbSet<MotivoCancelacion> MotivosCancelacion { get; set; }
    }
}
