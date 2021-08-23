using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.ViewModels.Tabs
{
    class ConsultaProductoViewModel : Helpers.TabViewModelBase
    {
        private readonly Common.ServiceContracts.IDataServiceAsync _proxy;
        private Helpers.CommonHelper _common;
        public ConsultaProductoViewModel()
        {
            _common = new Helpers.CommonHelper();
            this.SearchCommand = new RelayCommand(async () =>
            {
                this.IsBusy = true;
                await this.Search();
                this.IsBusy = false;
            }, () => {
                if (string.IsNullOrEmpty(this.Serie)) 
                    return true;
                if (string.IsNullOrEmpty(this.Marca) && string.IsNullOrEmpty(this.Modelo))
                    return true;
                return false;
            });

            this.NadaCommand = new RelayCommand(() => {

            });

            this.FindMedidaCommand = new RelayCommand(async () =>
            {
                this.IsBusy = true;
                this.Existencias = await _proxy.GetExistenciasAsync(this.Item.Id.Value, this.SelectedMedida);
                this.IsBusy = false;
            }, () => this.Item != null && this.SelectedMedida != null);

            this.PropertyChanged += ConsultaProductoViewModel_PropertyChanged;
            if (this.IsInDesignMode)
            {
                this.Serie = "0000003806584";
                this.Marca = "ADD";
                this.Modelo = "1197";
                this.Item = new Common.Entities.Producto
                {
                    Serie = "0000003806584",
                    Marca = "ADD",
                    Modelo = "1197",
                    Talla = "28.5",
                    Precio = 1799.99m,
                    Corrida = "A",
                    Electronica = false,
                    ParUnico = true,
                    MaxPlazos = 10,
                    Sucursal = "08"
                };
                this.Corridas = new Common.Entities.MedidasCorridas
                {
                    Corridas = new Common.Entities.TallaPrecio[] {
                        new Common.Entities.TallaPrecio { Corrida = "A", MedidaFin = "14-", MedidaInicio = "29-", Precio = 1099.99m },
                        new Common.Entities.TallaPrecio { Corrida = "B", MedidaFin = "24", MedidaInicio = "29-", Precio = 999m },
                        new Common.Entities.TallaPrecio { Corrida = "C", MedidaFin = "30", MedidaInicio = "39", Precio = 1234.56m }
                    },
                    Medidas = new string[] { "20", "20-", "24-" }
                };
                this.SelectedMedida = "24-";
                this.Existencias = new Common.Entities.SucursalExistencia[]
                {
                    //new Common.Entities.SucursalExistencia { Sucursal = "08", Count = 10 },
                    //new Common.Entities.SucursalExistencia { Sucursal = "01", Count = 30 },
                    //new Common.Entities.SucursalExistencia { Sucursal = "04", Count = 5 }
                };

                this.ItemStatus = Common.Constants.Status.CA;
                this.Promos = new Common.Entities.Promocion[] {
                    new Common.Entities.Promocion { PromocionId = 1, Nombre = "promo 1" },
                    new Common.Entities.Promocion { PromocionId = 1, Nombre = "promo 2" },
                    new Common.Entities.Promocion { PromocionId = 1, Nombre = "promo 3" }
                };
            }
            else
            {
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
            }
        }

        private void ConsultaProductoViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.SelectedMedida):
                    this.FindMedidaCommand.RaiseCanExecuteChanged();
                    break;
            }
        }

        private async Task Search()
        {
            if (!string.IsNullOrEmpty(this.Serie))
            {
                this.Item = null;
                this.ItemStatus = null;
                this.Promos = null;
                this.Corridas = null;
                this.Existencias = null;

                var ser = _common.PrepareSerie(this.Serie);
                var res = await _proxy.ScanProductoAsync(ser, this.Sucursal.Clave);
                if (res != null)
                {
                    this.Serie = null;
                    this.Item = res.Producto;
                    this.ItemStatus = res.Status;

                    if (res.Producto.Id.HasValue)
                    {
                        this.Corridas = await _proxy.GetPreciosAsync(res.Producto.Id.Value);
                    }
                }
            }
            else if (!string.IsNullOrEmpty(this.Marca) && !string.IsNullOrEmpty(this.Modelo))
            {
                this.Item = null;
                this.ItemStatus = null;
                this.Promos = null;
                this.Corridas = null;
                this.Existencias = null;

                //var model = String.Format("{0,7}", this.Modelo);
                var model = _common.PrepareModelo(this.Modelo);

                var res = _proxy.FindProducto(this.Marca, model, this.Sucursal.Clave);
                if (res != null)
                {
                    this.Marca = null;
                    this.Modelo = null;

                    this.Item = res;

                    if (res.Id.HasValue)
                    {
                        this.Corridas = await _proxy.GetPreciosAsync(res.Id.Value);
                    }
                }
            }
            else
                return;

            if (this.Item != null)
            {
                var request = new Common.Entities.CheckPromocionesRequest
                {
                    Sucursal = this.Sucursal.Clave,
                    Productos = new Common.Entities.SerieFormasPago[]
                    {
                        new Common.Entities.SerieFormasPago
                        {
                            ArticuloId = this.Item.Id,
                            Serie = this.Item.Serie,
                            //FormasPago = new Common.Constants.FormaPago[]
                            //{
                            //    Common.Constants.FormaPago.EF
                            //}
                        }
                    }
                };
                this.Promos = await _proxy.GetPromocionesAsync(request);
            }
        }

        #region commnads
        public RelayCommand SearchCommand { get; private set; }
        public RelayCommand NadaCommand { get; private set; }
        #endregion
        #region properties
        private IEnumerable<Common.Entities.Promocion> _Promos;
        public IEnumerable<Common.Entities.Promocion> Promos
        {
            get { return _Promos; }
            set { Set(nameof(this.Promos), ref _Promos, value); }
        }
        private Common.Entities.MedidasCorridas _Corridas;
        public Common.Entities.MedidasCorridas Corridas
        {
            get { return _Corridas; }
            set { Set(nameof(this.Corridas), ref _Corridas, value); }
        }

        private Common.Entities.Producto _Item;
        public Common.Entities.Producto Item
        {
            get { return _Item; }
            set { Set(nameof(this.Item), ref _Item, value); }
        }
        private Common.Constants.Status? _ItemStatus;
        public Common.Constants.Status? ItemStatus
        {
            get => _ItemStatus;
            set => this.Set(nameof(ItemStatus), ref _ItemStatus, value);
        }

        private string _Serie;
        public string Serie
        {
            get { return _Serie; }
            set { Set(nameof(this.Serie), ref _Serie, value); }
        }
        private string _Marca;
        public string Marca
        {
            get { return _Marca; }
            set { Set(nameof(this.Marca), ref _Marca, value); }
        }
        private string _Modelo;
        public string Modelo
        {
            get { return _Modelo; }
            set { Set(nameof(this.Modelo), ref _Modelo, value); }
        }
        private string _SelectedMedida;
        public string SelectedMedida
        {
            get { return _SelectedMedida; }
            set { Set(nameof(this.SelectedMedida), ref _SelectedMedida, value); }
        }
        private IEnumerable<Common.Entities.SucursalExistencia> _Existencias;
        public IEnumerable<Common.Entities.SucursalExistencia> Existencias
        {
            get { return _Existencias; }
            set { Set(nameof(this.Existencias), ref _Existencias, value); }
        }

        #endregion

        public RelayCommand FindMedidaCommand { get; private set; }
    }
}
