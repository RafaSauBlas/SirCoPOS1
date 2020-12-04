using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SirCoPOS.Common.Entities;
using System.Configuration;
using System.Windows.Data;
using System.ComponentModel;
using SirCoPOS.Common.Constants;

namespace SirCoPOS.Client.ViewModels
{
    partial class PuntoDeVentaViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        private readonly Common.ServiceContracts.IDataServiceAsync _proxy;
        private readonly Common.ServiceContracts.ICommonServiceAsync _pproxy;
        private readonly Helpers.CommonHelper _common;
        private IDictionary<FormaPago, bool> _formas;
        public PuntoDeVentaViewModel()
        {
            if (!IsInDesignMode)
            {
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
                _pproxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.ICommonServiceAsync>();
            }
            this.Productos = new ObservableCollection<Producto>();
            this.Pagos = new ObservableCollection<Models.Pagos.Pago>();
            _formas = new Dictionary<FormaPago, bool> {
                { FormaPago.EF, true },
                { FormaPago.TC, true },
                { FormaPago.TD, true }
            };            
            this.FormasPago = CollectionViewSource.GetDefaultView(_formas);
            this.FormasPago.Filter = i => ((KeyValuePair<FormaPago, bool>)i).Value;
            this.ShowSerie = true;
            _common = new Helpers.CommonHelper();

            this.SaleCommand = new GalaSoft.MvvmLight.Command.RelayCommand(() => 
            {
                var sale = new SaleRequest {                    
                    VendedorId = 0,
                    Productos = this.Productos.Select(i => new SerieFormasPago { Serie = i.Serie }),
                    Sucursal = "01",
                    Pagos = this.Pagos.Where(i => (i.Importe ?? 0) > 0).Select(i => new Common.Entities.Pago {
                        FormaPago = i.FormaPago,
                        Importe = i.Importe.Value
                    })
                };
                //var res = await _proxy.SaleAsync(sale);
                //MessageBox.Show($"ID: {res}");
            }, () => {
                var sum = this.Pagos.Sum(i => i.Importe) ?? 0;
                var rem = this.Total - sum;
                return this.Total > 0 && rem == 0;
            });
            this.AddFormaCommand = new GalaSoft.MvvmLight.Command.RelayCommand(() => {
                var p = new Models.Pagos.Pago { FormaPago = this.SelectedFormaPago };
                this.Pagos.Add(p);
                p.PropertyChanged += (s, e) => RaisePropertyChanged(nameof(this.TotalPayment));
                if (this.SelectedFormaPago == FormaPago.EF)
                {
                    _formas[FormaPago.EF] = false;                    
                    this.FormasPago.Refresh();                    
                }                
            });
            this.FindVendedorCommand = new GalaSoft.MvvmLight.Command.RelayCommand(async () =>
            {
                this.Vendedor = await _pproxy.FindVendedorAsync(this.VendedorSearch.Value);
                if (this.Vendedor != null)
                    this.VendedorSearch = null;
            });
            this.FindCajeroCommand = new GalaSoft.MvvmLight.Command.RelayCommand(async () =>
            {
                this.Cajero = await _pproxy.FindCajeroAsync(this.CajeroSearch);
                if (this.Cajero != null)
                    this.CajeroSearch = null;
            });

            this.AddCommand = new GalaSoft.MvvmLight.Command.RelayCommand(async () =>
            {
                this.IsBusy = true;
                var ser = _common.PrepareSerie(this.Serie);

                var q = this.Productos.Where(i => i.Serie == ser).SingleOrDefault();
                if (q != null)
                {
                    MessageBox.Show($"Already Added: {ser}");
                    return;
                }

                var item = await _proxy.ScanProductoAsync(ser, "01");
                if (item != null)
                {
                    if (item.Status == Status.AC || item.Status == Status.IF
                        || item.Status == Status.CA)
                    {
                        //if(await _proxy.RequestProductoAsync(item.Producto.Serie))
                        //    this.Productos.Add(item.Producto);                        
                    }
                    else
                        MessageBox.Show($"{item.Producto.Serie} - {item.Status}");
                    this.Serie = null;
                }
                else
                    MessageBox.Show($"Not Found: {ser}");
                this.IsBusy = false;
            }, () =>
            {
                return !string.IsNullOrWhiteSpace(this.Serie);
            });
            this.RemoveCommand = new GalaSoft.MvvmLight.Command.RelayCommand(() =>
            {
                //await _proxy.ReleaseProductoAsync(this.SelectedItem.Serie);
                //this.Productos.Remove(this.SelectedItem);
            }, () =>
            {
                return this.SelectedItem != null;
            });

            this.PropertyChanged += PuntoDeVenta_PropertyChanged;
            this.Productos.CollectionChanged += Productos_CollectionChanged;
            this.Pagos.CollectionChanged += Pagos_CollectionChanged;

            if (this.IsInDesignMode)
            {
                this.Serie = "123";
                this.Productos.Add(new Producto { Id = 1, Serie = "001", Marca = "a", Modelo = "b", Talla = "c", Precio = 100, Total = 100, HasImage = true });
                this.Productos.Add(new Producto { Id = 2, Serie = "002", Marca = "a", Modelo = "b", Talla = "c", Precio = 100, Total = 1000, HasImage = true });
                this.Productos.Add(new Producto { Id = 3, Serie = "003", Marca = "a", Modelo = "b", Talla = "c", Precio = 100, Total = 1234.25m });
                this.Productos.Add(new Producto { Id = 4, Serie = "004", Marca = "a", Modelo = "b", Talla = "c", Precio = 100, Total = 12456 });
                this.Productos.Add(new Producto { Id = 5, Serie = "005", Marca = "a", Modelo = "b", Talla = "c", Precio = 100, Total = 9.99m });

                this.Pagos.Add(new Models.Pagos.Pago { FormaPago = FormaPago.EF, Importe = 100 });
                this.Pagos.Add(new Models.Pagos.Pago { FormaPago = FormaPago.TD, Importe = 30 });
                this.Pagos.Add(new Models.Pagos.Pago { FormaPago = FormaPago.TD, Importe = 20 });
            }            
        }        

        private void Pagos_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            _formas[FormaPago.EF] = !this.Pagos.Where(i => i.FormaPago == FormaPago.EF).Any();
            this.FormasPago.Refresh();
            RaisePropertyChanged(nameof(this.Total));
        }

        private void Productos_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(this.SubTotal));
            RaisePropertyChanged(nameof(this.Descuento));        
        }
        private void PuntoDeVenta_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.SelectedItem):
                    this.RemoveCommand.RaiseCanExecuteChanged();
                    break;
                case nameof(this.Serie):
                    this.AddCommand.RaiseCanExecuteChanged();
                    break;
                case nameof(this.SubTotal):
                case nameof(this.Descuento):
                    RaisePropertyChanged(nameof(this.Total));
                    break;
                case nameof(this.Total):
                case nameof(this.TotalPayment):
                    RaisePropertyChanged(nameof(this.Remaining));
                    break;
                case nameof(this.Remaining):
                    this.SaleCommand.RaiseCanExecuteChanged();
                    break;
            }            
        }

        #region properties
        public decimal SubTotal
        {
            get {
                return this.Productos.Sum(i => i.Precio.Value);
            }            
        }
        public decimal Descuento
        {
            get {
                return 0m/*this.Productos.Where(i => i.Descuento.HasValue).Sum(i => i.Descuento.Value)*/;
            }            
        }
        public decimal Total
        {
            get {
                return this.SubTotal - this.Descuento;
            }            
        }
        public decimal TotalPayment
        {
            get
            {
                return this.Pagos.Where(i => i.Importe.HasValue).Sum(i => i.Importe.Value);
            }
        }
        public decimal Remaining
        {
            get {
                return this.Total - this.TotalPayment;
            }            
        }
        #endregion

        #region commands        
        public GalaSoft.MvvmLight.Command.RelayCommand SaleCommand { get; private set; }
        public GalaSoft.MvvmLight.Command.RelayCommand AddFormaCommand { get; private set; }
        public GalaSoft.MvvmLight.Command.RelayCommand AddCommand { get; private set; }
        public GalaSoft.MvvmLight.Command.RelayCommand RemoveCommand { get; private set; }
        public GalaSoft.MvvmLight.Command.RelayCommand FindVendedorCommand { get; private set; }
        public GalaSoft.MvvmLight.Command.RelayCommand FindCajeroCommand { get; private set; }
        #endregion
    }
}
