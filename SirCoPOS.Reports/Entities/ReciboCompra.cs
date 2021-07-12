using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Reports.Entities
{
    public class ReciboCompra
    {
        public string RFC { get; set; }
        public string SucursalId { get; set; }
        public string SucursalNombre { get; set; }
        public string Direccion { get; set; }
        public string Colonia { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string CP { get; set; }
        public string VendedorId { get; set; }
        public string VendedorNombre { get; set; }
        public string CajeroId { get; set; }
        public string CajeroNombre { get; set; }
        public string Folio { get; set; }
        public decimal? Efectivo { get; set; }
        public string CantidadLetra { get; set; }
        public decimal? Descuento { get; set; }
        public decimal? Blindaje { get; set; }
        public DateTime Cambio { get; set; }
        public DateTime Fecha { get; set; }
        public decimal? Dinero { get; set; }
        public string ClienteId { get; set; }
        public string ClienteNombre { get; set; }
        public string ClienteCalle { get; set; }
        public string ClienteColonia { get; set; }
        public string ClienteCiudad { get; set; }
        public string ClienteTelefono { get; set; }
        public string Distrib { get; set; }
        public string DistribNombre { get; set; }
        public string Nota { get; set; }
        public DateTime NotaFecha { get; set; }
        public string Vale { get; set; }
        public int Plazo { get; set; }
        public decimal? ImporteSaldo { get; set; }
        public int NumPagos { get; set; }
        public decimal? ImportePrimerPago { get; set; }
        public decimal? ImporteSigPagos { get; set; }
        public DateTime Fecha1erPago { get; set; }
        public short? ContraVale { get; set; }
        public decimal? PagarCon { get; set; }

    }
}
