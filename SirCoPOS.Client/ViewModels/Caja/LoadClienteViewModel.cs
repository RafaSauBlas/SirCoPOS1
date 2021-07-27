using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SirCoPOS.Common.Constants;
using SirCoPOS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace SirCoPOS.Client.ViewModels.Caja
{
    public class LoadClienteViewModel : Helpers.ModalViewModelBase, Utilities.Interfaces.IModal
    {
        public string Title => "Cargar Cliente";
        private Common.ServiceContracts.IDataServiceAsync _proxy;
        private Helpers.CommonHelper _common;
        private Utilities.Interfaces.IClienteView _Client;

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

            this.searchnameCommand = new RelayCommand(() => {
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

                    this.Cliente = _proxy.FinClienteName(this.ClienteNombreSearch);
                    if (this.Cliente != null)
                    {
                        this.ClienteSearch = null;
                        this.ClienteTelefonoSearch = null;
                        this.ClienteNombreSearch = null;
                    }
                    else
                        MessageBox.Show("Cliente no encontrado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.ClienteNombreSearch = null;
                }
            });

            //Los comandos funcionan como metodos que realizan alguna accion predeterminada
            this.PopUpCommand = new RelayCommand(async () => {
                var nombre = _common.PrepareNombre(this.ClienteNombreSearch);
                var ApPa = _common.PrepareApPa(this.ClienteApPaSearch);
                var ApMa = _common.PrepareApMa(this.ClienteApMaSearch);

                if (nombre == "")
                    nombre = null;
                if (ApPa == "")
                    ApPa = null;
                if (ApMa == "")
                    ApMa = null;
                List<Cliente> lista = new List<Cliente>();

                    lista = _proxy.FindCliente2(null, nombre, ApPa, ApMa);
                    if (this.Cliente != null)
                    {
                        this.ClienteTelefonoSearch = null;
                        this.ClienteNombreSearch = null;
                        this.ClienteApPaSearch = null;
                        this.ClienteApMaSearch = null;
                    }

                _Client = CommonServiceLocator.ServiceLocator.Current.GetInstance<Utilities.Interfaces.IClienteView>();

                _Client.OpenCliente(lista);

                this.ClienteTelefonoSearch = "";
                this.ClienteNombreSearch = "";
                this.ClienteApPaSearch = "";
                this.ClienteApMaSearch = "";
            });
        }

        public void Busca(string celular)
        {

            var nombre = "";
            var phone = _common.PreparePhone(celular);
            

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

        public void LoadClienteViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
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

        public async void Cliente_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
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

        public void TraerColonias()
        {
                this.Colonias =  _proxy.FindColonias(SirCoPOS.Common.Constants.ClienteInfo.cp);
                SirCoPOS.Common.Constants.ClienteInfo.Colonias = this.Colonias;
        }

        public void RefrescarColonias(string cp)
        {
            SirCoPOS.Services.DataService DS = new SirCoPOS.Services.DataService();
            this.Colonias = _proxy.FindColonias(cp.ToString());
            foreach (var ci in Colonias)
            {
                SirCoPOS.Common.Constants.ClienteInfo.ciudad = _proxy.FindCiudad(ci.CiudadId);
                SirCoPOS.Common.Constants.ClienteInfo.estado = _proxy.FindEstado(ci.EstadoId);
            }

            SirCoPOS.Common.Constants.ClienteInfo.Colonias = this.Colonias;
        }


        public bool CloseTab { get { return false; } }
        protected override void Accept()
        {
            if (this.Screen == "new")
            {
                var celular = _common.PreparePhone(this.NuevoCliente.Celular);
                var exists = _proxy.CheckCelular(celular);
                if (!exists)
                {
                    Messenger.Default.Send(new Messages.NuevoClienteMessage
                    {
                        Cliente = this.NuevoCliente
                    }, this.GID);
                }
                else
                    MessageBox.Show("YA EXISTE UN CLIENTE REGISTRADO CON EL MISMO NÚMERO CELULAR.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (this.Screen == "search")
            {
                Messenger.Default.Send(new Messages.ClienteMessage
                {
                    Cliente = this.Cliente
                }, this.GID);
            }
            if (this.Screen == "")
            {
                MessageBox.Show("YA EXISTE UN CLIENTE REGISTRADO CON EL MISMO NÚMERO CELULAR.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
        private string _search;
        public string Search
        {
            get { return _search; }
            set
            {
                this.Set(nameof(this.Search), ref _search, value);
            }
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
        private string _ClienteApPaSearch;
        public string ClienteApPaSearch
        {
            get => _ClienteApPaSearch;
            set => this.Set(nameof(ClienteApPaSearch), ref _ClienteApPaSearch, value);
        }
        private string _ClienteApMaSearch;
        public string ClienteApMaSearch
        {
            get => _ClienteApMaSearch;
            set => this.Set(nameof(ClienteApMaSearch), ref _ClienteApMaSearch, value);
        }
        #endregion
        #region commands
        public RelayCommand<string> ChangeViewCommand { get; private set; }
        public RelayCommand SearchCommand { get; private set; }
        public RelayCommand searchnameCommand { get; private set; }
        public RelayCommand PopUpCommand { get; private set; }
        #endregion
    }
}
