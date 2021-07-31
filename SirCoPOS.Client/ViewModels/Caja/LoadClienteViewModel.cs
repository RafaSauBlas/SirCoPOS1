﻿using GalaSoft.MvvmLight.Command;
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
        Common.Constants.ClienteDato CD = new Common.Constants.ClienteDato();
        private Common.ServiceContracts.IDataServiceAsync _proxy;
        private Helpers.CommonHelper _common;
        private Utilities.Interfaces.IClienteView _Client;
        public int coloniaid;

        public string name;
        public string appa;
        public string apma;
        public string codigopostal;
        public string calle;
        public short numero;
        public string celular;
        public string email;
        public string colonia;

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
                        this.ClienteNombreSearch = this.Cliente.Nombre;
                        this.ClienteApPaSearch = this.Cliente.ApPaterno;
                        this.ClienteApMaSearch = this.Cliente.ApMaterno;
                        this.ClienteSearch = null;
                        this.ClienteTelefonoSearch = null;
                    }
                    else
                        MessageBox.Show("Cliente no encontrado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });

            this.searchnameCommand = new RelayCommand(() => {
                var nombre = _common.PrepareNombre(this.ClienteNombreSearch);
                var appa = _common.PrepareApPa(this.ClienteApPaSearch);
                var apma = _common.PrepareApMa(this.ClienteApMaSearch);
                var nc = nombre + " " + appa + " " + apma;
                SirCoPOS.Client.Views.Caja.LoadClienteSearchView LC = new SirCoPOS.Client.Views.Caja.LoadClienteSearchView();

                if (this.ClienteNombreSearch == null && this.ClienteApPaSearch == null && this.ClienteApMaSearch == null)
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
                    
                    this.Cliente = _proxy.FinClienteName(nc);

                    if (this.Cliente != null)
                    {
                        coloniaid = this.Cliente.Colonia ?? default(int);
                        Common.Constants.ClienteInfo.colonia = coloniaid;
                        this.Colonias = _proxy.FindColonias(this.Cliente.CodigoPostal);
                        
                        if (this.Cliente.Colonia != 0)
                        {
                            var col = _proxy.findcol(this.Cliente.Colonia);
                        }
                        this.ClienteSearch = null;
                        this.ClienteTelefonoSearch = null;
                        
                        CD.Nombre = this.Cliente.Nombre;
                        CD.ApPaterno = this.Cliente.ApPaterno;
                        CD.ApMaterno = this.Cliente.ApMaterno;
                        CD.Celular1 = this.Cliente.Celular1;
                        CD.Calle = this.Cliente.Calle;
                        CD.Ciudad = this.Cliente.Ciudad;
                        CD.Estado = this.Cliente.Estado;
                        CD.Colonia = this.Cliente.Colonia;
                        CD.CodigoPostal = this.Cliente.CodigoPostal;
                        CD.Email = this.Cliente.Email;
                    }
                    else
                    { 
                    this.ClienteNombreSearch = null;
                    this.ClienteApMaSearch = null;
                    this.ClienteApPaSearch = null;
                    MessageBox.Show("Cliente no encontrado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                //LC.accion();
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
        public void Clientexd(string name, string appaterno, string apmaterno, string codigopostal, string calle, int numero, string celular, string email, string colonia)
        {
            var colid = _proxy.FindColidByName(colonia, codigopostal);
            if (name != CD.Nombre || appaterno != CD.ApPaterno || apmaterno != CD.ApMaterno
                || codigopostal != CD.CodigoPostal || calle != CD.Calle || numero != CD.Numero
                || email != CD.Email || colid != CD.Colonia)
            {
                var celular1 = _common.PreparePhone(celular);
                var datos = Convert.ToInt32(_proxy.Clientexd(name, appaterno, apmaterno, codigopostal, calle, numero, celular1, email, colonia));
                Common.Constants.ClienteInfo.colonia = datos;
            }
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

        public void cambio()
        {
            var co = this.Colonias;
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
            this.Colonias = null;
            this.Colonias = _proxy.FindColonias(cp.ToString());
        }
        public string FindColonia()
        {
            var coloniaid = Common.Constants.ClienteInfo.colonia;
            var ret = _proxy.FindColonia(coloniaid);
            return ret;
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
