using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace SirCoPOS.Client.ViewModels.Caja
{
    class LoadClienteViewModel : Helpers.ModalViewModelBase, Utilities.Interfaces.IModal
    {
        public string Title => "Cargar Cliente";
        private Common.ServiceContracts.IDataServiceAsync _proxy;
        private Helpers.CommonHelper _common;
        public LoadClienteViewModel()
        {
            if (!this.IsInDesignMode)
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();

            _common = new Helpers.CommonHelper();
            this.Screen = "search";
            this.ChangeViewCommand = new RelayCommand<string>(v => {
                this.Screen = v;
            });

            this.PropertyChanged += LoadClienteViewModel_PropertyChanged;

            this.SearchCommand = new RelayCommand(() => {
                if (this.ClienteNombreSearch == null && string.IsNullOrWhiteSpace(this.ClienteTelefonoSearch))
                {
                    if (this.Cliente != null)
                    {
                        Messenger.Default.Send(new Messages.ClienteMessage
                        {
                            Cliente = this.Cliente
                        }, this.GID);
                    }
                }
                else
                {
                    var nombre = _common.PrepareNombre(this.ClienteNombreSearch);
                    var phone = _common.PreparePhone(this.ClienteTelefonoSearch);

                    this.Cliente = _proxy.FindCliente(this.ClienteSearch, phone, nombre);
                    if (this.Cliente != null)
                    {
                        this.ClienteSearch = null;
                        this.ClienteTelefonoSearch = null;
                        this.ClienteNombreSearch = null;
                    }
                    else
                        MessageBox.Show("Cliente no encontrado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            });

            if (this.IsInDesignMode)
            {
                this.ClienteSearch = 123;
                this.ClienteTelefonoSearch = "1234567890";
                this.ClienteNombreSearch = "Ricardo Milos Galvan";
                this.NuevoCliente = new Models.NuevoCliente
                {
                    Nombre = "nombre",
                    ApPaterno = "ap paterno",
                    ApMaterno = "ap materno",
                    Calle = "calle",
                    Celular = "1234567890",
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
                this.NuevoCliente.Colonia = this.Colonias.First();
            }
        }

        private void LoadClienteViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.Screen):
                    {
                        switch (this.Screen)
                        {
                            case "new":
                                this.NuevoCliente = new Models.NuevoCliente();
                                this.NuevoCliente.IsValid();
                                this.NuevoCliente.PropertyChanged += Cliente_PropertyChanged;
                                break;
                            case "search":
                                if (this.NuevoCliente != null)
                                    this.NuevoCliente.PropertyChanged -= Cliente_PropertyChanged;
                                this.NuevoCliente = null;
                                break;
                        }
                    }
                    break;
                case nameof(this.Cliente):
                case nameof(this.NuevoCliente):
                    this.AcceptCommand.RaiseCanExecuteChanged();
                    break;
            }
        }

        private async void Cliente_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.NuevoCliente.CodigoPostal):
                    if (this.NuevoCliente.CodigoPostal != null && this.NuevoCliente.CodigoPostal.Length == 5)
                    {
                        this.IsBusy = true;
                        this.Colonias = await _proxy.FindColoniasAsync(this.NuevoCliente.CodigoPostal);
                        this.IsBusy = false;
                    }
                    else
                        this.Colonias = null;
                    break;
            }
            this.AcceptCommand.RaiseCanExecuteChanged();
        }

        public bool CloseTab { get { return false; } }
        protected override void Accept()
        {
            if (this.Screen == "new")
            {
                var exists = _proxy.CheckCelular(this.NuevoCliente.Celular);
                if(!exists)
                {
                    Messenger.Default.Send(new Messages.NuevoClienteMessage
                    {
                        Cliente = this.NuevoCliente
                    }, this.GID);
                }
                else
                    MessageBox.Show("El telefono esta duplicado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (this.Screen == "search")
            {
                Messenger.Default.Send(new Messages.ClienteMessage
                {
                    Cliente = this.Cliente
                }, this.GID);
            }
        }

        protected override bool CanAccept()
        {
            if(this.Screen == "new")
                return this.NuevoCliente.IsValid();
            if(this.Screen == "search")
                return this.Cliente != null;
            return false;
        }

        #region properties
        private string _screen;
        public string Screen 
        {
            get => _screen;
            set => this.Set(nameof(this.Screen), ref _screen, value);
        }
        private IEnumerable<Common.Entities.Colonia> _colonias;
        public IEnumerable<Common.Entities.Colonia> Colonias
        {
            get { return _colonias; }
            set { this.Set(nameof(this.Colonias), ref _colonias, value); }
        }
        private Models.NuevoCliente _nuevoCliente;
        public Models.NuevoCliente NuevoCliente
        {
            get { return _nuevoCliente; }
            set { this.Set(nameof(this.NuevoCliente), ref _nuevoCliente, value); }
        }
        private Common.Entities.Cliente _cliente;
        public Common.Entities.Cliente Cliente
        {
            get { return _cliente; }
            set { this.Set(nameof(this.Cliente), ref _cliente, value); }
        }
        private int? _clienteSearch;
        public int? ClienteSearch
        {
            get { return _clienteSearch; }
            set { this.Set(nameof(this.ClienteSearch), ref _clienteSearch, value); }
        }
        private string _ClienteTelefonoSearch;
        public string ClienteTelefonoSearch
        {
            get => _ClienteTelefonoSearch;
            set => this.Set(nameof(ClienteTelefonoSearch), ref _ClienteTelefonoSearch, value);
        }
        private string _ClienteNombreSearch;
        public string ClienteNombreSearch
        {
            get => _ClienteNombreSearch;
            set => this.Set(nameof(ClienteNombreSearch), ref _ClienteNombreSearch, value);
        }
        #endregion
        #region commands
        public RelayCommand<string> ChangeViewCommand { get; private set; }
        public RelayCommand SearchCommand { get; private set; }
        #endregion
    }
}
