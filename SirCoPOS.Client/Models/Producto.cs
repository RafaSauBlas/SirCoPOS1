using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media.Imaging;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.Models
{
    public class Producto : GalaSoft.MvvmLight.ObservableObject, Utilities.Interfaces.IProducto
    {
        public Producto()
        {
            this.PropertyChanged += Producto_PropertyChanged;            
            this.FormasPago = new ObservableCollection<Common.Entities.FormaPagoImporte>();

            if (GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                this.Marca = "Marca";
                this.Modelo = "modelo";
                this.Precio = 500m;
                this.DescuentoDirecto = 250m;
            }
        }

        private void Producto_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.Fijo):
                case nameof(this.DescuentoDirecto):
                case nameof(this.DescuentoAdicional):
                    this.RaisePropertyChanged(nameof(this.Descuento));
                    break;
                case nameof(this.Descuento):
                    this.RaisePropertyChanged(nameof(this.DescuentoPorcentaje));
                    this.RaisePropertyChanged(nameof(this.Total));
                    break;
                case nameof(this.Total):
                case nameof(this.Pago):
                    this.RaisePropertyChanged(nameof(this.Pagado));
                    this.RaisePropertyChanged(nameof(this.Saldo));
                    break;
                case nameof(this.MaxPlazos):
                    this.RaisePropertyChanged(nameof(this.HasPlazos));
                    break;
                case nameof(this.Precio):
                    this.RaisePropertyChanged(nameof(this.IsPrecioEdited));
                    this.RaisePropertyChanged(nameof(this.Total));
                    break;
            }                            
        }
        public int? Id { get; set; }
        public string Serie { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string MarcaModelo 
        { get { return Marca + Modelo ; }
        }
        public string Talla { get; set; }
        public string Corrida { get; set; }
        public BitmapImage Foto = new BitmapImage(new Uri("https://www.gravatar.com/avatar/1c32fe01c48edccb228f69feb0377b7a?s=64&d=identicon&r=PG"));
        public decimal? PrecioOriginal { get; set; }
        private decimal? _Precio;
        public decimal? Precio
        {
            get { return _Precio; }
            set { Set(nameof(this.Precio), ref _Precio, value); }
        }
        public bool IsPrecioEdited
        {
            get => this.PrecioOriginal != this.Precio;
        }

        private decimal? _descuentoDirecto;
        public decimal? DescuentoDirecto {
            get => _descuentoDirecto;
            set => this.Set(nameof(this.DescuentoDirecto), ref _descuentoDirecto, value);
        }
        private decimal? _monedero;
        public decimal? Monedero
        {
            get => _monedero;
            set => this.Set(nameof(this.Monedero), ref _monedero, value);
        }
        private int? _index;
        public int? Index
        {
            get => _index;
            set => this.Set(nameof(this.Index), ref _index, value);
        }
        private int? _promocionId;
        public int? PromocionId {
            get => _promocionId;
            set => this.Set(nameof(this.PromocionId), ref _promocionId, value);
        }
        public decimal? Total {
            get => this.Precio - (this.Descuento ?? 0);
        }
        public decimal? Descuento
        {
            get
            {
                var res = (this.DescuentoDirecto ?? 0);
                if (this.Fijo.HasValue && this.Precio.HasValue)
                {
                    var desc = this.Precio.Value - this.Fijo.Value;
                    res += desc;
                }
                if (this.DescuentoAdicional != null)
                {
                    var desc = this.Precio.Value * this.DescuentoAdicional.Descuento.Descuento;
                    res += desc;
                }
                return res > 0 ? res : (decimal?)null;
            }
        }
        public decimal? DescuentoPorcentaje
        {
            get {
                if (!this.Descuento.HasValue)
                    return null;
                var p = this.Descuento / this.Precio;
                return p;
            }
        }
        private Messages.DescuentoEspecial _DescuentoAdicional;
        public Messages.DescuentoEspecial DescuentoAdicional
        {
            get { return _DescuentoAdicional; }
            set { Set(nameof(this.DescuentoAdicional), ref _DescuentoAdicional, value); }
        }

        public bool HasImage { get; set; }
        public decimal? _fijo;
        public decimal? Fijo
        {
            get => _fijo;
            set => this.Set(nameof(Fijo), ref _fijo, value);
        }
        private decimal? _pago;
        public decimal? Pago
        {
            get => _pago;
            set => this.Set(nameof(this.Pago), ref _pago, value);
        }
        public bool Pagado
        {
            get {
                if ((this.Descuento ?? 0) > 0)
                {
                    if (this.Total == 0)
                        return true;
                }
                return (this.Total ?? 0) > 0 && (this.Total ?? 0) == (this.Pago ?? 0);
            }
        }
        public bool PagadoInit
        {
            get {
                var total = this.Precio;
                if (this.Init.HasValue)
                    total -= this.Init;
                return total == (this.Pago ?? 0);
            }
        }
        public decimal? Saldo
        {
            get => this.Total - (this.Pago ?? 0);
        }
        public ObservableCollection<Common.Entities.FormaPagoImporte> FormasPago { get; set; }
        public bool Electronica { get; set; }
        public bool Accesorio { get; set; }
        public bool ParUnico { get; set; }
        public byte? MaxPlazos { get; set; }
        public bool HasPlazos => this.MaxPlazos.HasValue;                
        public decimal? Init { get; internal set; }
        private string _Notas;
        public string Notas
        {
            get { return _Notas; }
            set { Set(nameof(this.Notas), ref _Notas, value); }
        }
        private int? _NotaRazon;
        public int? NotaRazon { 
            get { return _NotaRazon; }
            set { Set(nameof(this.NotaRazon), ref _NotaRazon, value); }
        }

    }
}
