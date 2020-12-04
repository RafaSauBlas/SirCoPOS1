using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SirCoPOS.Client.ViewModels
{
    class NuevoClienteViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        private Common.ServiceContracts.IDataServiceAsync _proxy;
        public NuevoClienteViewModel()
        {
            if (!IsInDesignMode)
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();

            this.Cliente = new Models.NuevoCliente();
            this.Cliente.IsValid();
            this.Cliente.PropertyChanged += Cliente_PropertyChanged;
            this.SaveCommand = new GalaSoft.MvvmLight.Command.RelayCommand(() =>
            {
                MessageBox.Show("save");
            }, () => this.Cliente.IsDirty() && !this.Cliente.HasErrors);
            if (this.IsInDesignMode)
            {
                this.Cliente = new Models.NuevoCliente
                {
                    Nombre = "nombre",
                    ApPaterno = "ap paterno",
                    ApMaterno = "ap materno",
                    Calle = "calle",
                    Celular = "celular",
                    CodigoPostal = "cp",
                    Colonia = null,
                    Email = "email",
                    Referencia = "entre calles",
                    Numero = 123, 
                };

                this.Colonias = new Common.Entities.Colonia[] {
                    new Common.Entities.Colonia
                    {
                        Id = 1,
                        Nombre  = "colonia",
                        CodigoPostal  = "cp",
                        CiudadId = 2,
                        CiudadNombre = "ciudad",
                        EstadoId = 3,
                        EstadoNombre = "estado"
                    }
                };
                this.SelectedColonia = this.Colonias.First();
            }
        }

        private void Cliente_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.Cliente.CodigoPostal):
                    if (this.Cliente.CodigoPostal != null && this.Cliente.CodigoPostal.Length == 5)
                        this.Colonias = _proxy.FindColonias(this.Cliente.CodigoPostal);
                    else
                        this.Colonias = null;
                    break;
            }
            this.SaveCommand.RaiseCanExecuteChanged();
        }
        #region commands
        public GalaSoft.MvvmLight.Command.RelayCommand SaveCommand { get; private set; }
        #endregion
        #region properties
        private Common.Entities.Colonia _selectedColonia;
        public Common.Entities.Colonia SelectedColonia
        {
            get { return _selectedColonia; }
            set { this.Set(nameof(this.SelectedColonia), ref _selectedColonia, value); }
        }
        private IEnumerable<Common.Entities.Colonia> _colonias;
        public IEnumerable<Common.Entities.Colonia> Colonias
        {
            get { return _colonias; }
            set { this.Set(nameof(this.Colonias), ref _colonias, value); }
        }
        private Models.NuevoCliente _cliente;
        public Models.NuevoCliente Cliente
        {
            get { return _cliente; }
            set { this.Set(nameof(this.Cliente), ref _cliente, value); }
        }
        #endregion
    }
}
