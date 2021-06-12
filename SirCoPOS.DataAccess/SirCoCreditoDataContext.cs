using SirCoPOS.DataAccess.SirCoCredito;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess
{
    public class SirCoCreditoDataContext : DbContext
    {
        public SirCoCreditoDataContext()
            : base("SirCoCredito")
        {

        }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Distribuidor> Distribuidores { get; set; }
        public virtual DbSet<Valeras> Valeras { get; set; }
        public virtual DbSet<ValesCancelados> ValesCancelados { get; set; }
        public virtual DbSet<DistribuidorFirma> DistribuidorFirmas { get; set; }
        public virtual DbSet<DistribuidorObserva> DistribuidorObservaciones { get; set; }
        public virtual DbSet<PlanPagos> PlanPagos { get; set; }
        public virtual DbSet<PlanPagosDetalle> PlanPagosDetalle { get; set; }
        public virtual DbSet<PromocionCredito> PromocionesCredito { get; set; }
        public virtual DbSet<Calendario> Calendarios { get; set; }
        public virtual DbSet<Pagos> Pagos { get; set; }
        public virtual DbSet<PagosDetalle> PagosDetalle { get; set; }
        public virtual DbSet<ContraVale> ContraVales { get; set; }
        public virtual DbSet<DescuentoAdicional> DescuentosAdicionales { get; set; }
        public virtual DbSet<DistribuidorComercial> DistribuidorComerciales { get; set; }
        public virtual DbSet<ValeFisico> ValeFisicos { get; set; }
    }
}
