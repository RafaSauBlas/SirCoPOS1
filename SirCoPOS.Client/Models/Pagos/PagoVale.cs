using SirCoPOS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SirCoPOS.Client.Models.Pagos
{
    public class PagoVale : Pago
    {
        public PagoVale()
        {
            this.PropertyChanged += PagoVale_PropertyChanged;
        }

        private void PagoVale_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.SelectedPromocion):
                    this.RaisePropertyChanged(nameof(this.HasPromocion));
                    break;
                case nameof(this.SelectedPlazo):
                    {
                        if (this.SelectedPlazo.HasValue && this.Importe.HasValue)
                        {
                            var part = this.Importe.Value / this.SelectedPlazo.Value;
                            this.Primero = Math.Ceiling(part);
                            var res = this.Primero.Value * (this.SelectedPlazo.Value - 1);
                            this.Ultimo = this.Importe.Value - res;
                        }
                        else
                        {
                            this.Primero = null;
                            this.Ultimo = null;
                        }
                        this.RaisePropertyChanged(nameof(this.Primero));
                        this.RaisePropertyChanged(nameof(this.Ultimo));
                    }
                    break;
            }
        }
        public decimal? Primero { get; set; }
        public decimal? Ultimo { get; set; }
        public string Vale { get; set; }
        public string NoCuenta { get; set; }
        public int? Negocio { get; set; }
        [XmlIgnore]
        public PagoValeInfo Info { get; set; }
        [XmlIgnore]
        public IEnumerable<int> Plazos { get; set; }
        [XmlIgnore]
        public IEnumerable<DateTime> Promociones { get; set; }
        private int? _selectedPlazo;
        public int? SelectedPlazo {
            get => _selectedPlazo;
            set => this.Set(nameof(SelectedPlazo), ref _selectedPlazo, value);
        }
        public DateTime? SelectedPromocion { get; set; }
        public bool HasSelectedPromocion
        {
            get {
                if (this.SelectedPromocion.HasValue 
                    && this.Promociones != null && this.Promociones.Any())
                {
                    return this.SelectedPromocion != this.Promociones.First();
                }
                return false;
            }
        }
        public bool ContraVale { get; set; }
        public decimal? Limite { get; set; }
        public IEnumerable<ProductoPlazo> ProductosPlazos { get; set; }
    }
    public class PagoValeInfo
    { 
        public string Distribuidor { get; set; }
        public bool Electronica { get; set; }
        public bool Promocion { get; set; }
        public bool SoloCalzado => !this.Electronica;        
    }
    public class PagoContraVale : PagoVale
    {
        public string Sucursal { get; set; }
    }
    public class PagoCredito : PagoVale
    { 
    
    }
}
