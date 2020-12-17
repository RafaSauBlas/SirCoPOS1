using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SirCoPOS.Client.ViewModels.Tabs
{
    class GastoViewModel : Helpers.TabViewModelBase
    {
        private readonly Common.ServiceContracts.IAdminServiceAsync _proxy;
        private readonly Common.ServiceContracts.IDataServiceAsync _data;
        public GastoViewModel()
        {
            if (!this.IsInDesignMode)
            {
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IAdminServiceAsync>();
                _data = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
                this.Opciones = _proxy.GetTiposGasto();
            }

            this.PropertyChanged += GastoViewModel_PropertyChanged;
            this.LoadSolicitanteCommand = new RelayCommand(() =>
            {
                this.Empleado = _data.FindAuditorApertura(this.EmpleadoSearch.Value, this.Cajero.Id);
                if (this.Empleado != null)
                {
                    this.EmpleadoSearch = null;
                }
                else
                {
                    MessageBox.Show("Solicitante no valido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }, () => this.EmpleadoSearch.HasValue);
            this.SaveCommand = new RelayCommand(() =>
            {
                var request = new Common.Entities.GastoRequest
                {
                    Tipo = this.SelectedOption.Value,
                    Monto = this.Monto.Value,
                    Sucursal = this.Sucursal.Clave,
                    CajeroId = this.Cajero.Id,
                    SolicitaId = this.Empleado.Id,
                    Descripcion = this.Descripcion
                };
                _proxy.GenerarGasto(request);
                MessageBox.Show("ready");
                this.CloseCommand.Execute(null);
            }, () => this.SelectedOption.HasValue && this.Monto.HasValue && this.Empleado != null);
            if (this.IsInDesignMode)
            {
                this.Descripcion = "desc";
                this.EmpleadoSearch = 100;
                this.Monto = 199.99m;
                this.Opciones = new Common.Entities.Option[] {
                    new Common.Entities.Option { Id = 1, Text = "a" },
                    new Common.Entities.Option { Id = 2, Text = "b" },
                    new Common.Entities.Option { Id = 3, Text = "c" }
                };
                this.SelectedOption = 2;
                this.Empleado = new Common.Entities.Empleado
                {
                    Nombre = "aaa", 
                    ApellidoMaterno = "mat", 
                    ApellidoPaterno = "pat"
                };
            }
        }
        protected override bool IsReady()
        {
            return true;
        }
        private void GastoViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.EmpleadoSearch):
                    this.LoadSolicitanteCommand.RaiseCanExecuteChanged();
                    break;
                case nameof(this.Monto):
                case nameof(this.SelectedOption):
                case nameof(this.Empleado):
                    this.SaveCommand.RaiseCanExecuteChanged();
                    break;
            }
        }
        private string _Descripcion;
        public string Descripcion
        {
            get { return _Descripcion; }
            set { Set(nameof(this.Descripcion), ref _Descripcion, value); }
        }

        private decimal? _Monto;
        public decimal? Monto
        {
            get { return _Monto; }
            set { Set(nameof(this.Monto), ref _Monto, value); }
        }

        private Common.Entities.Empleado _Empleado;
        public Common.Entities.Empleado Empleado
        {
            get { return _Empleado; }
            set { Set(nameof(this.Empleado), ref _Empleado, value); }
        }

        private int? _EmpleadoSearch;
        public int? EmpleadoSearch
        {
            get { return _EmpleadoSearch; }
            set { Set(nameof(this.EmpleadoSearch), ref _EmpleadoSearch, value); }
        }

        private IEnumerable<Common.Entities.Option> _Opciones;
        public IEnumerable<Common.Entities.Option> Opciones
        {
            get { return _Opciones; }
            set { Set(nameof(this.Opciones), ref _Opciones, value); }
        }
        private int? _SelectedOption;
        public int? SelectedOption
        {
            get { return _SelectedOption; }
            set { Set(nameof(this.SelectedOption), ref _SelectedOption, value); }
        }
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand LoadSolicitanteCommand { get; private set; }
    }
}
