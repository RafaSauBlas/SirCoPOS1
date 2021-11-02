using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SirCoPOS.Client.ViewModels.Tabs
{
    class ConsultaVentaViewModel : Helpers.TabViewModelBase
    {
        private Common.ServiceContracts.IDataServiceAsync _proxy;
        private Helpers.CommonHelper _common;
        private Helpers.ReportsHelper _reports;
        public ConsultaVentaViewModel()
        {
            _common = new Helpers.CommonHelper();
            this.PropertyChanged += ConsultaVentaViewModel_PropertyChanged;
            _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
            _reports = new Helpers.ReportsHelper();
            
            this.SearchCommand = new RelayCommand(() => {
                var settings = CommonServiceLocator.ServiceLocator.Current.GetInstance<Utilities.Models.Settings>();
                if (settings != null)
                {
                    var folio = _common.PrepareVentaDevolucion(this.Search);
                    var vta = _proxy.FindVentaView(settings.Sucursal.Clave, folio, this.Cajero.Id);
                    if (vta != null)
                    {
                        this.Venta = vta;
                        this.VtaSucursal = vta.Sucursal;
                        this.VtaFolio = vta.Folio;
                        this.Search = null;
                    }
                    else
                    {
                        MessageBox.Show("No se encontró la Venta", "Consulta Venta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        this.Search = null;
                    }
                }
            });
            if (this.IsInDesignMode)
            {
                this.Search = "venta";
                this.Venta = new Common.Entities.VentaView
                {
                    Folio = "folio",
                    Sucursal = "sucursal",
                    Productos = new Common.Entities.ProductoView[] {
                        new Common.Entities.ProductoView { Serie = "1", Precio = 100 },
                        new Common.Entities.ProductoView { Serie = "2", Precio = 99.9m },
                        new Common.Entities.ProductoView { Serie = "3", Precio = 1999.89m }
                    }
                };
            }
        }

        private void ConsultaVentaViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.Venta):
                    this.PrintCommand.RaiseCanExecuteChanged();
                    break;
            }
        }
        #region properties
        private Common.Entities.VentaView _Venta;
        public Common.Entities.VentaView Venta
        {
            get { return _Venta; }
            set { Set(nameof(this.Venta), ref _Venta, value); }
        }
        private string _sucursal;
        public string VtaSucursal
        {
            get { return _sucursal; }
            set { _sucursal = value; }
        }
        private string _folio;
        public string VtaFolio
        {
            get { return _folio; }
            set { _folio = value; }
        }
        private string _Search;
        public string Search
        {
            get { return _Search; }
            set { Set(nameof(this.Search), ref _Search, value); }
        }        
        #endregion
        #region commands
        public RelayCommand SearchCommand { get; private set; }
        private RelayCommand _PrintCommand;
        public RelayCommand PrintCommand
        {
            get
            {
                if (_PrintCommand == null)
                {
                    _PrintCommand = new RelayCommand(
                        () =>
                        {
                            this.Venta = null;
                            _reports.Compra(this.VtaSucursal, this.VtaFolio, true);
                            _proxy.ContabilizaReimpresion("VENTA", this.VtaSucursal, this.VtaFolio);
                        }, () => this.Venta != null
                    );
                }
                return _PrintCommand;
            }
        }
        private RelayCommand _UltimaVentaCommand;
        public RelayCommand UltimaVentaCommand
        {
            get
            {
                if (_UltimaVentaCommand == null)
                {
                    _UltimaVentaCommand = new RelayCommand(
                        () =>
                        {
                            this.Search = null;
                            this.SearchCommand.Execute(null);
                        }
                    );
                }
                return _UltimaVentaCommand;
            }
        }


        #endregion
    }
}
