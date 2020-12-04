using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.Models
{
    public class ProductoCambio : GalaSoft.MvvmLight.ObservableObject
    {
        public ProductoCambio()
        {
            this.PropertyChanged += ProductoCambio_PropertyChanged;
        }

        private void ProductoCambio_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {            
            switch (e.PropertyName)
            {
                case nameof(this.OldItem):
                case nameof(this.NewItem):
                    this.RaisePropertyChanged(nameof(this.Complete));
                    this.RaisePropertyChanged(nameof(this.Saldo));

                    this.RaisePropertyChanged(nameof(this.Descuento));
                    this.RaisePropertyChanged(nameof(this.DescuentoPorcentaje));
                    this.RaisePropertyChanged(nameof(this.Pagar));
                    this.RaisePropertyChanged(nameof(this.Usable));
                    this.RaisePropertyChanged(nameof(this.PorPagar));
                    break;
            }
        }

        public bool Complete
        {
            get
            {
                return this.OldItem != null && this.NewItem != null;                
            }
        }

        private Common.Entities.ProductoDevolucion _oldItem;
        //private Common.Entities.Producto _newItem;
        private Models.Producto _newItem;

        public Common.Entities.ProductoDevolucion OldItem
        {
            get { return _oldItem; }
            set { this.Set(nameof(this.OldItem), ref _oldItem, value); }
        }
        //public Common.Entities.Producto NewItem
        public Models.Producto NewItem
        {
            get { return _newItem; }
            set { this.Set(nameof(this.NewItem), ref _newItem, value); }
        }
        public bool SameCorrida
        {
            get {
                if (this.OldItem != null && this.NewItem != null)
                {
                    return this.OldItem.Corrida == this.NewItem.Corrida
                        && this.OldItem.Marca == this.NewItem.Marca
                        && this.OldItem.Modelo == this.NewItem.Modelo;
                }
                return false;
            }
        }
        public bool SameModelo
        {
            get {
                if (this.OldItem != null && this.NewItem != null)
                {
                    return this.OldItem.Marca == this.NewItem.Marca
                        && this.OldItem.Modelo == this.NewItem.Modelo;
                }
                return false;
            }
        }
        public decimal? Descuento
        {
            get {
                if (this.OldItem != null && this.NewItem != null)
                {
                    if (this.SameCorrida)
                    {
                        if (this.OldItem.Precio != this.NewItem.Precio)
                        {
                            if (this.NewItem.Precio < this.OldItem.Precio)
                            {
                                var ndesc = this.NewItem.Precio * this.OldItem.DescuentoPorcentaje;
                                return ndesc ?? 0;
                            }
                        }
                        return this.NewItem.Precio;
                    }
                    else if (this.SameModelo)
                    {
                        if (this.OldItem.Precio != this.NewItem.Precio)
                        {
                            if (this.NewItem.Precio < this.OldItem.Precio)
                            {
                                var ndesc = this.NewItem.Precio * this.OldItem.DescuentoPorcentaje;
                                return ndesc ?? 0;
                            }
                        }
                        return this.OldItem.Precio;
                    }
                    else
                        return this.OldItem.Pago;                    
                }
                return null;
            }
        }
        public decimal? DescuentoPorcentaje
        {
            get {
                if (this.NewItem != null && this.OldItem != null
                    && this.Descuento.HasValue)
                {
                    if (this.SameCorrida)
                    {
                        return null;
                    }
                    if (this.NewItem.Precio < this.OldItem.Precio)
                    {
                        var desc = this.NewItem.Precio - (this.Descuento ?? 0);
                        var per = desc / this.NewItem.Precio;
                        return 1 - per;
                    }
                }
                return null;
            }
        }
        public decimal? PorPagar
        { 
            get
            {
                if (this.NewItem != null && this.OldItem != null)
                {
                    if (this.SameCorrida)
                        return null;

                    if (this.NewItem.Precio < this.OldItem.Precio)
                        return this.NewItem.Precio - (this.Descuento ?? 0);
                    else
                        return this.NewItem.Precio;
                }
                return null;
            }
        }
        public decimal? Pagar
        {
            get {
                if (this.NewItem != null && this.OldItem != null)
                {
                    return this.NewItem.Precio - (this.Descuento ?? 0);
                }
                return null;
            }
        }
        public decimal? Usable
        {
            get
            {                
                if (this.OldItem != null && this.NewItem != null)
                {
                    if (this.SameCorrida)
                    {
                        return null;
                    }
                    //if (this.SameCorrida || this.SameModelo)
                    if (this.SameModelo)
                    {
                        if (this.NewItem.Precio < this.OldItem.Precio)
                        {
                            return this.OldItem?.Pago;
                        }
                        return this.OldItem.Precio;
                    }                    
                }                
                return this.OldItem?.Pago;
            }            
        }
        public decimal? Saldo
        {
            get
            {
                if (this.OldItem != null && this.NewItem != null)
                {
                    if (this.SameCorrida)
                    {
                        //con disponible
                        //if (this.OldItem.Precio != this.NewItem.Precio)
                        //{
                        //    if (this.NewItem.Precio < this.OldItem.Precio)
                        //    {
                        //        var ndesc = this.NewItem.Precio * this.OldItem.DescuentoPorcentaje;
                        //        var npre = this.NewItem.Precio - (ndesc ?? 0);
                        //        return npre - this.OldItem.Pago;
                        //    }
                        //}
                        return 0;
                    }
                    else if (this.SameModelo)
                    {
                        if (this.OldItem.Precio != this.NewItem.Precio)
                        {
                            if (this.NewItem.Precio < this.OldItem.Precio)
                            {
                                var ndesc = this.NewItem.Precio * this.OldItem.DescuentoPorcentaje;
                                var npre = this.NewItem.Precio - (ndesc ?? 0);
                                return npre - this.OldItem.Pago;
                            }
                        }
                        return this.NewItem.Precio - this.OldItem.Precio;
                    }
                    else
                        return this.NewItem.Precio - this.OldItem.Pago;
                }
                return null;
            }            
        }
        public string Razon { get; set; }
        public int? RazonId { get; set; }
    }
}
