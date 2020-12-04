using SirCoPOS.DataAccess.SirCoNomina;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess
{
    public class SirCoNominaDataContext : DbContext
    {
        public SirCoNominaDataContext()
            : base("SirCoNomina")
        {

        }

        public virtual DbSet<Empleado> Empleados { get; set; }
        public virtual DbSet<Sucursal> Sucursales { get; set; }        
        public virtual DbSet<Huellas> Huellas { get; set; }
        public virtual DbSet<Periodo> Periodos { get; set; }
        public virtual DbSet<Nomina> Nominas { get; set; }
        public virtual DbSet<NominaDetalle> NominaDetalles { get; set; }
        public virtual DbSet<Repetitivo> Repetitivos { get; set; }
        public virtual DbSet<RepetitivoDetalle> RepetitivosDetalles { get; set; }
        public virtual DbSet<HuellasPOS> HuellasPOS { get; set; }
    }
}
