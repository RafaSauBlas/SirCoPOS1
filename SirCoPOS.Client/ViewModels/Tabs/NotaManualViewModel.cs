using CommonServiceLocator;
using SirCoPOS.Client.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SirCoPOS.Client.ViewModels.Tabs
{
    class NotaManualViewModel : Helpers.TabViewModelBase
    {
        private readonly Common.ServiceContracts.INoteServiceAsync _notes;
        private readonly Common.ServiceContracts.IDataServiceAsync _data;
        private readonly ServiceClient _client;
        public NotaManualViewModel()
        {
            this.IsConnected = false;
            this.Items = new ObservableCollection<Models.ProductoNota>();
            this.Items.CollectionChanged += Items_CollectionChanged;
            this.RestoreCommand = new GalaSoft.MvvmLight.Command.RelayCommand(() =>
            {
                foreach (var item in this.Items)
                {
                    item.Precio = item.Producto.Precio.Value;
                }
            });
            this.SearchCommand = new GalaSoft.MvvmLight.Command.RelayCommand(async () => {
                var item = _data.ScanProducto(this.SerieSearch, this.Sucursal.Clave);
                if (item != null)
                {
                    if (item.Status != Common.Constants.Status.AC)
                    {
                        MessageBox.Show($"status: {item.Status}");
                        return;
                    }

                    await _client.RequestProductoAsync(item.Producto.Serie);
                    this.Items.Add(new Models.ProductoNota { 
                        Precio = item.Producto.Precio.Value,
                        Producto = item.Producto 
                    });
                }
                this.SerieSearch = null;            
            });
            this.SaveCommand = new GalaSoft.MvvmLight.Command.RelayCommand(() => {
                var items = this.Items.Select(i => 
                    new Common.Entities.NoteDetalle { 
                        Serie = i.Producto.Serie, 
                        Amount = i.Precio.Value,
                        Comments = i.Comentarios
                    });
                var request = new Common.Entities.NoteRequest
                {
                    VendedorId = 0,
                    Sucursal = this.Sucursal.Clave,
                    Items = items
                };
                var res = _notes.SaveNote(request);
                MessageBox.Show($"saved: {res}");
            });            
            if (this.IsInDesignMode)
            {
                this.SerieSearch = "0000003332927";
                this.Items.Add(new Models.ProductoNota { Precio = 1m, Producto = new Common.Entities.Producto { Id = 1, Marca = "a1", Modelo = "m1", Talla = "t", Precio = 9.99m } });
                this.Items.Add(new Models.ProductoNota { Precio = 100m, Comentarios = "comments", Producto = new Common.Entities.Producto { Id = 1, Marca = "a2", Modelo = "m2", Talla = "t", Precio = 100 } });
                this.Items.Add(new Models.ProductoNota { Precio = 99.9m, Producto = new Common.Entities.Producto { Id = 1, Marca = "a3", Modelo = "m3", Talla = "t", Precio = 1000 } });
            }
            else
            {
                _client = CommonServiceLocator.ServiceLocator.Current.GetInstance<ServiceClient>();
                _notes = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.INoteServiceAsync>();
                _data = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
            }
        }

        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (var item in e.NewItems.OfType<Models.ProductoNota>())
                    {
                        item.PropertyChanged += Item_PropertyChanged;
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    foreach (var item in e.OldItems.OfType<Models.ProductoNota>())
                    {
                        item.PropertyChanged -= Item_PropertyChanged;
                    }
                    break;                
            }
            this.RaisePropertyChanged(nameof(this.Total));
        }

        private void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.RaisePropertyChanged(nameof(this.Total));
        }

        public GalaSoft.MvvmLight.Command.RelayCommand SearchCommand { get; private set; }
        public GalaSoft.MvvmLight.Command.RelayCommand SaveCommand { get; private set; }
        public GalaSoft.MvvmLight.Command.RelayCommand RestoreCommand { get; private set; }
        public bool IsConnected { get; set; }
        private string _serieSearch;
        public string SerieSearch
        {
            get => _serieSearch;
            set => this.Set(nameof(this.SerieSearch), ref _serieSearch, value);
        }
        public ObservableCollection<Models.ProductoNota> Items { get; private set; }
        public decimal? Total
        {
            get {
                if (this.Items.Any())
                {
                    var total = this.Items.Where(i => i.Precio.HasValue).Sum(i => i.Precio.Value);
                    return total;
                }
                return null;
            }
        }
    }
}
