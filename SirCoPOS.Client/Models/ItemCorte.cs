using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.Models
{
    class ItemCorte : ObservableObject
    {
        public ItemCorte()
        {
            this.Detalle = new ObservableCollection<ItemCorteDetalle>();
            this.Detalle.CollectionChanged += Detalle_CollectionChanged;
            this.PropertyChanged += ItemCorte_PropertyChanged;
        }

        private void Detalle_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.RaisePropertyChanged(nameof(this.Entrega));
            this.RaisePropertyChanged(nameof(this.Monto));
        }

        private void ItemCorte_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.Entrega):
                    this.RaisePropertyChanged(nameof(this.FaltanteEntrega));
                    this.RaisePropertyChanged(nameof(this.Complete));
                    break;
                case nameof(this.Monto):
                    this.RaisePropertyChanged(nameof(this.FaltanteMonto));
                    break;
            }
        }

        public Common.Entities.FormaPagoCorte Item { get; set; }

        #region computed
        public int? Entrega
        {
            get => this.Detalle.Count;            
        }
        public decimal? Monto
        {
            get => this.Detalle.Sum(i => i.Amount);            
        }
        public int? FaltanteEntrega
        {
            get => this.Item.Count - this.Entrega;
        }
        public decimal? FaltanteMonto
        {
            get => this.Item.Total - this.Monto;
        }
        public bool Complete
        {
            get {
                if (this.Entrega.HasValue && this.Monto.HasValue)
                {
                    return this.FaltanteEntrega.Value == 0 
                        && this.FaltanteMonto.Value == 0;
                }
                return false;
            }
        }
        #endregion
        public ObservableCollection<ItemCorteDetalle> Detalle { get; private set; }
    }
    class ItemCorteDetalle
    { 
        public decimal? Amount { get; set; }
    }
    class ItemCorteSerie : ObservableObject
    {
        public Common.Entities.SeriePrecio Item { get; set; }
        private bool _reportado;
        public bool Reportado 
        {
            get => _reportado;
            set => this.Set(nameof(this.Reportado), ref _reportado, value);
        }
    }
}
