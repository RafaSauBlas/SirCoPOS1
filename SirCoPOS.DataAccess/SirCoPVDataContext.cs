using SirCoPOS.DataAccess.SirCoPV;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess
{
    public class SirCoPVDataContext : DbContext
    {
        public SirCoPVDataContext()
            : base("SirCoPV")
        {

        }

        public virtual DbSet<Venta> Ventas { get; set; }
        public virtual DbSet<VentaDetalle> VentasDetalle { get; set; }
        public virtual DbSet<Devolucion> Devoluciones { get; set; }
        public virtual DbSet<DevolucionDetalle> DevolucionesDetalle { get; set; }
        public virtual DbSet<Pago> Pagos { get; set; }
        public virtual DbSet<PagoDetalle> PagosDetalle { get; set; }
        public virtual DbSet<Promociones> Promociones { get; set; }
        public virtual DbSet<PromocionesAgrupaciones> PromocionesAgrupaciones { get; set; }
        public virtual DbSet<PromocionesExclusiones> PromocionesExclusiones { get; set; }
        public virtual DbSet<PromocionesCupones> PromocionesCupones { get; set; }
        public virtual DbSet<PromocionesDetalle> PromocionesDetalle { get; set; }
        public virtual DbSet<PromocionesPlazas> PromocionesPlazas { get; set; }
        public virtual DbSet<Cupones> Cupones { get; set; }
        public virtual DbSet<CuponesDetalle> CuponesDetalle { get; set; }
        public virtual DbSet<Agrupaciones> Agrupaciones { get; set; }
        public virtual DbSet<AgrupacionesDetalle> AgrupacionesDetalle { get; set; }
        public virtual DbSet<FormaPago> FormasPago { get; set; }
        public virtual DbSet<DescuentoEspecial> DescuentoEspeciales { get; set; }
        public virtual DbSet<DevolucionRazon> DevolucionRazones { get; set; }
        public virtual DbSet<NotaRazon> NotaRazones { get; set; }
    }
}
