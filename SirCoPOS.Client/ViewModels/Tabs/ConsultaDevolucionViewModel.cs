using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.ViewModels.Tabs
{
    class ConsultaDevolucionViewModel : Helpers.TabViewModelBase
    {
        private Common.ServiceContracts.IDataServiceAsync _proxy;
        private Helpers.CommonHelper _common;
        private Helpers.ReportsHelper _reports;
        public ConsultaDevolucionViewModel()
        {
            if (!this.IsInDesignMode)
            {
                _common = new Helpers.CommonHelper();
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
                _reports = new Helpers.ReportsHelper();
            }
            this.PropertyChanged += ConsultaDevolucionViewModel_PropertyChanged;
            this.SearchCommand = new RelayCommand(() => {
                var settings = CommonServiceLocator.ServiceLocator.Current.GetInstance<Utilities.Models.Settings>();
                var folio = _common.PrepareVentaDevolucion(this.Search);
                var dev = this.Devolucion = _proxy.FindDevolucionView(settings.Sucursal.Clave, folio, this.Cajero.Id);
                if (dev != null)
                {
                    this.Devolucion = dev;
                    this.Search = null;
                }
                    
            });

            if (this.IsInDesignMode)
            {
                this.Search = "devolucion";
                this.Devolucion = new Common.Entities.DevolucionView
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

        private void ConsultaDevolucionViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.Devolucion):
                    this.PrintCommand.RaiseCanExecuteChanged();
                    break;
            }
        }
        #region properties
        private Common.Entities.DevolucionView _Devolucion;
        public Common.Entities.DevolucionView Devolucion
        {
            get { return _Devolucion; }
            set { Set(nameof(this.Devolucion), ref _Devolucion, value); }
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
                            _reports.Devolucion(this.Devolucion.Sucursal, this.Devolucion.Folio);
                        }, () => this.Devolucion != null
                    );
                }
                return _PrintCommand;
            }
        }
        #endregion
    }
}
