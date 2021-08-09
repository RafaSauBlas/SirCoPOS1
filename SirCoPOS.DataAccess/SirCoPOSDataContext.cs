using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess
{
    public class SirCoPOSDataContext : DbContext
    {
        public SirCoPOSDataContext()
            : base("SirCoPOS")
        { 
            
        }

        public virtual DbSet<SirCoPOS.ClienteAcceso> ClienteAccesos { get; set; }
        public virtual DbSet<SirCoPOS.Caja> Cajas { get; set; }
        public virtual DbSet<SirCoPOS.CajaFormaPago> CajaFormasPago { get; set; }
        public virtual DbSet<SirCoPOS.Fondo> Fondos { get; set; }
        public virtual DbSet<SirCoPOS.FondoArqueo> FondoArqueos { get; set; }
        public virtual DbSet<SirCoPOS.FondoMovimiento> FondoMovimientos { get; set; }
        public virtual DbSet<SirCoPOS.FondoFormaPago> FondoFormasPago { get; set; }
        public virtual DbSet<SirCoPOS.FondoArqueoFormaPago> FondoArqueoFormaPagos { get; set; }
        public virtual DbSet<SirCoPOS.FondoArqueoFormaPagoTicket> FondoArqueoFormaPagoTickets { get; set; }
        public virtual DbSet<SirCoPOS.FondoArqueoSerie> FondoArqueoSeries { get; set; }
        public virtual DbSet<SirCoPOS.Pago> Pagos { get; set; }
        public virtual DbSet<SirCoPOS.Huella> Huellas { get; set; }
        public virtual DbSet<SirCoPOS.Nota> Notas { get; set; }
        public virtual DbSet<SirCoPOS.NotaDetalle> NotasDetalle { get; set; }
        public virtual DbSet<SirCoPOS.NotaPago> NotasPago { get; set; }
        public virtual DbSet<SirCoPOS.SolicitudCreditoVale> SolicitudesCreditoVales { get; set; }
        public virtual DbSet<SirCoPOS.ValeCliente> ValesCliente { get; set; }
        public virtual DbSet<SirCoPOS.Reimpresion> Reimpresiones { get; set; }
    }
}
