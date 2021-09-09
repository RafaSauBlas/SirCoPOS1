using SirCoPOS.Common.Constants;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SirCoPOS.Client.ViewModels.Tabs
{
    partial class CajaViewModel
    {
        private void CajaViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.SerieSearch):
                    //this.AddCommand.RaiseCanExecuteChanged();
                    break;
                case nameof(this.TipoFPVta):
                case nameof(this.SubTotal):
                case nameof(this.Descuento):
                    RaisePropertyChanged(nameof(this.Total));
                    break;
                case nameof(this.Total):
                case nameof(this.TotalPayment):
                    RaisePropertyChanged(nameof(this.Remaining));
                    break;
                case nameof(this.Remaining):
                    this.PagarCommand.RaiseCanExecuteChanged();
                    this.AddFormaCommand.RaiseCanExecuteChanged();
                    //this.SaleCommand.RaiseCanExecuteChanged();
                    //this.AddFormaCommand.RaiseCanExecuteChanged();
                    break;
                case nameof(this.SaleResponse):
                    //this.SaleCommand.RaiseCanExecuteChanged();
                    this.RaisePropertyChanged(nameof(this.IsComplete));
                    break;                
                case nameof(this.SelectedItem):
                    this.MoveProductoCommand.RaiseCanExecuteChanged();
                    this.AddDescuentoAdicional.RaiseCanExecuteChanged();
                    this.AddNotaCommand.RaiseCanExecuteChanged();
                    break;
                case nameof(this.SelectedPago):
                    this.MovePagoCommand.RaiseCanExecuteChanged();
                    break;
                case nameof(this.SelectedCupon):
                    this.MoveCuponCommand.RaiseCanExecuteChanged();
                    break;
                case nameof(this.Vendedor):
                    this.SaleCommand.RaiseCanExecuteChanged();
                    this.RemoveVendedorCommand.RaiseCanExecuteChanged();
                    break;
                case nameof(this.IsComplete):
                    this.PrintCommand.RaiseCanExecuteChanged();
                    break;
            }
        }
        private async void Pagos_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.RaisePropertyChanged(nameof(this.Remaining));            
            this.RaisePropertyChanged(nameof(this.HasPagos));
            this.RaisePropertyChanged(nameof(this.HasVale));
            this.RaisePropertyChanged(nameof(this.TotalPayment));
            //await this.UpdatePromociones();         
            //====================================================================================================================================
            
            await this.RefreshPromociones();
            this.AddFormaCommand.RaiseCanExecuteChanged();
            this.SaleCommand.RaiseCanExecuteChanged();
        }

        private async void Productos_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var i in e.NewItems)
                {
                    var item = (Models.Producto)i;
                    item.PropertyChanged += Item_PropertyChanged;
                }
            }
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var i in e.OldItems)
                {
                    var item = (Models.Producto)i;
                    item.PropertyChanged -= Item_PropertyChanged;
                }
            }
            RaisePropertyChanged(nameof(this.SubTotal));
            RaisePropertyChanged(nameof(this.Descuento));
            RaisePropertyChanged(nameof(this.Unidades));            
            await this.RefreshPromociones(true);
            this.LoadVendedorCommand.RaiseCanExecuteChanged();
            this.SaleCommand.RaiseCanExecuteChanged();
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Models.Producto.Descuento):
                    RaisePropertyChanged(nameof(this.Descuento));
                    break;
                case nameof(Models.Producto.Monedero):
                    RaisePropertyChanged(nameof(this.Monedero));
                    break;
            }                
        }
        private async void PromocionesCupones_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {                         
                        var items = e.NewItems.OfType<Common.Entities.Promocion>();
                        foreach (var item in items)
                        {
                            item.PropertyChanged += Promocion_PropertyChanged;
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    {
                        var items = e.OldItems.OfType<Common.Entities.Promocion>();
                        foreach (var item in items)
                        {
                            item.PropertyChanged -= Promocion_PropertyChanged;
                        }
                    }
                    break;                                    
            }
            await this.RefreshPromociones();
            this.PromocionesCuponesUsadas.Refresh();
        }
        private static object syncRefresh = new object();
        private async Task RefreshPromociones(bool update = false)
        {
            Monitor.Enter(syncRefresh);
            if(update)
                await this.GetPromociones();
            await RefreshPromocionesHelper();
            Monitor.Exit(syncRefresh);
        }
        private async Task RefreshPromocionesHelper()
        {
            this.UpdatePagos();

            if (_skipPromociones)
                return;

            await this.UpdatePromociones(this.TipoFPVta);
            UpdatePagos();
            await this.UpdatePromociones(this.TipoFPVta);
            UpdatePagos();
        }

        private async void Promocion_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Enabled":
                    await this.RefreshPromociones();
                    this.PromocionesCuponesUsadas.Refresh();
                    break;
            }
        }
    }
}
