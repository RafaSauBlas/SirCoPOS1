using SirCoPOS.Common.Constants;
using SirCoPOS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.ViewModels
{
    partial class PuntoDeVentaViewModel
    {        
        public ICollectionView FormasPago { get; set; }
        public ObservableCollection<Producto> Productos { get; set; }
        public ObservableCollection<Models.Pagos.Pago> Pagos { get; set; }

        private Common.Entities.Empleado _cajero;
        public Common.Entities.Empleado Cajero
        {
            get { return _cajero; }
            set { Set(() => this.Cajero, ref _cajero, value); }
        }
        private Common.Entities.Empleado _vendedor;
        public Common.Entities.Empleado Vendedor
        {
            get { return _vendedor; }
            set { Set(() => this.Vendedor, ref _vendedor, value); }
        }

        private int? _vendedorSearch;
        public int? VendedorSearch
        {
            get { return _vendedorSearch; }
            set { Set(() => this.VendedorSearch, ref _vendedorSearch, value); }
        }
        private string _cajeroSearch;
        public string CajeroSearch
        {
            get { return _cajeroSearch; }
            set { Set(() => this.CajeroSearch, ref _cajeroSearch, value); }
        }
        private bool _showSerie;
        public bool ShowSerie
        {
            get { return _showSerie; }
            set { this.Set(() => this.ShowSerie, ref _showSerie, value); }
        }
        private Producto _selectedItem;
        public Producto SelectedItem
        {
            get { return _selectedItem; }
            set { Set(() => this.SelectedItem, ref _selectedItem, value); }
        }
        private string _serie;
        public string Serie
        {
            get { return _serie; }
            set { Set(() => this.Serie, ref _serie, value); }
        }        
        private FormaPago _selectedFormaPago;
        public FormaPago SelectedFormaPago
        {
            get { return _selectedFormaPago; }
            set { this.Set(() => this.SelectedFormaPago, ref _selectedFormaPago, value); }
        }
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { this.Set(() => this.IsBusy, ref _isBusy, value); }
        }
    }
}
