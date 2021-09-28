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
        Common.Constants.ClienteDato CD = new Common.Constants.ClienteDato();
        private Client.Views.Caja.LoadClienteSearchView _CV;
        private Common.ServiceContracts.IDataServiceAsync _proxy;
        private Helpers.CommonHelper _common;
        private Utilities.Interfaces.IClienteView _Client;
        private Utilities.Models.Settings _settings;
        public int coloniaid;
        public int accion;
        //accion 0 => Buscar
        //accion 1 => Editar
        //accion 2 => Agregar
        // DATOS ACTUALES
        public string name1;
        public string appa1;
        public string apma1;
        public string codigopostal1;
        public string calle1;
        public short numero1;
        public string celular1;
        public string celular;
        public string email1;
        public int colonia1;
        public string identif1;
        public string sexo1;
        // DATOS ACTUALIZADOS
        public string name2;
        public string appa2;
        public string apma2;
        public string codigopostal2;
        public string calle2;
        public short numero2;
        public string celular2;
        public string celular12;
        public string email2;
        public int colonia2;
        public string colname;
        public string identif2;
        public string sexo2;

        public LoadClienteViewModel()
        {
            try
            {
                if (!this.IsInDesignMode)
                    _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
                _settings = CommonServiceLocator.ServiceLocator.Current.GetInstance<Utilities.Models.Settings>();
                _CV = new Client.Views.Caja.LoadClienteSearchView();
                _common = new Helpers.CommonHelper();
                this.Screen = "search";
                this.ChangeViewCommand = new RelayCommand<string>(v => {
                    this.Screen = v;
                });

                this.PropertyChanged += LoadClienteViewModel_PropertyChanged;

                this.SearchCommand = new RelayCommand(() =>
                {
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
                            Common.Constants.ClienteInfo.colonia = this.Cliente.Colonia ?? default(int); ;
                            name1 = this.Cliente.Nombre;
                            appa1 = this.Cliente.ApPaterno;
                            apma1 = this.Cliente.ApMaterno;
                            codigopostal1 = this.Cliente.CodigoPostal;
                            calle1 = this.Cliente.Calle;
                            numero1 = this.Cliente.Numero;
                            celular1 = this.Cliente.Celular1;
                            celular = this.Cliente.Celular;
                            email1 = this.Cliente.Email;
                            sexo1 = this.Cliente.Sexo;
                            colonia1 = Convert.ToInt32(this.Cliente.Colonia);

                            this.Colonias = _proxy.FindColonias(this.Cliente.CodigoPostal);
                            if (this.Cliente.Colonia != 0)
                            {
                                var col = _proxy.findcol(this.Cliente.Colonia);
                            }

                            this.ClienteNombreSearch = this.Cliente.Nombre;
                            this.ClienteApPaSearch = this.Cliente.ApPaterno;
                            this.ClienteApMaSearch = this.Cliente.ApMaterno;
                            this.ClienteCP = this.Cliente.CodigoPostal;
                            this.ClienteCelular = this.Cliente.Celular1;
                            this.ClienteCelular1 = this.Cliente.Celular;
                            this.ClienteCalle = this.Cliente.Calle;
                            this.ClienteSexo = this.Cliente.Sexo;
                            this.ClienteNumero = this.Cliente.Numero.ToString();
                            this.ClienteEmail = this.Cliente.Email;
                            this.ClienteIdentificacion = this.Cliente.Identificacion;


                            this.ClienteSearch = null;
                            this.ClienteTelefonoSearch = null;
                        }
                        else
                        {
                            MessageBox.Show("Cliente no encontrado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            this.ClienteTelefonoSearch = "";
                        }
                    }
                });

                this.searchnameCommand = new RelayCommand(() => {
                    BusquedaName();
                });

                this.agregarclientecommand = new RelayCommand(() =>
                {
                    if (this.ClienteNombreSearch == null)
                    {
                        MessageBox.Show("El campo NOMBRE no puede estar vacio.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else if (this.ClienteApPaSearch == null)
                    {
                        MessageBox.Show("El campo APELLIDO PATERNO no puede estar vacio.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else if (this.ClienteApMaSearch == null)
                    {
                        MessageBox.Show("El campo APELLIDO MATERNO no puede estar vacio.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else if (this.ClienteCP == null)
                    {
                        MessageBox.Show("El campo CODIGO POSTAL no puede estar vacio.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else if (this.ClienteColonia == null)
                    {
                        MessageBox.Show("Se debe seleccionar una COLONIA para poder continuar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else if (this.ClienteCalle == null)
                    {
                        MessageBox.Show("El campo CALLE no puede estar vacio.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else if (this.ClienteCelular1 == null)
                    {
                        //Views.Caja.LoadClienteSearchView LCSV = new Views.Caja.LoadClienteSearchView();
                        //LCSV.focusear();
                        MessageBox.Show("El campo CELULAR no puede estar vacio.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else if (this.ClienteSexo == null)
                    {
                        MessageBox.Show("Se debe seleccionar un SEXO para poder continuar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        AgregarCliente();
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
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Error: {0}", e);
                throw;
            }

        }
        public void BusquedaName()
        {
            try
            {
                var nombre = _common.PrepareNombre(this.ClienteNombreSearch);
                var appa = _common.PrepareApPa(this.ClienteApPaSearch);
                var apma = _common.PrepareApMa(this.ClienteApMaSearch);
                var nc = nombre + " " + appa + " " + apma;

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
                        Common.Constants.Inactividad.Opcion = 1;
                        Common.Constants.ClienteInfo.colonia = this.Cliente.Colonia ?? default(int); ;
                        name1 = this.Cliente.Nombre;
                        appa1 = this.Cliente.ApPaterno;
                        apma1 = this.Cliente.ApMaterno;
                        codigopostal1 = this.Cliente.CodigoPostal;
                        calle1 = this.Cliente.Calle;
                        numero1 = this.Cliente.Numero;
                        celular1 = this.Cliente.Celular1;
                        celular = this.Cliente.Celular;
                        email1 = this.Cliente.Email;
                        sexo1 = this.Cliente.Sexo;
                        colonia1 = Convert.ToInt32(this.Cliente.Colonia);


                        this.Colonias = _proxy.FindColonias(this.Cliente.CodigoPostal);
                        if (this.Cliente.Colonia != 0)
                        {
                            var col = _proxy.findcol(this.Cliente.Colonia);
                        }

                        this.ClienteCP = this.Cliente.CodigoPostal;
                        this.ClienteCelular = this.Cliente.Celular1;
                        this.ClienteCelular1 = this.Cliente.Celular;
                        this.ClienteCalle = this.Cliente.Calle;
                        this.ClienteSexo = this.Cliente.Sexo;
                        this.ClienteNumero = this.Cliente.Numero.ToString();
                        this.ClienteEmail = this.Cliente.Email;
                        this.ClienteIdentificacion = this.Cliente.Identificacion;

                        this.ClienteSearch = null;
                        this.ClienteTelefonoSearch = null;
                    }
                    else
                    {
                        Common.Constants.Inactividad.Opcion = 1;
                        MessageBox.Show("Cliente no encontrado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        this.ClienteNombreSearch = null;
                        this.ClienteApPaSearch = null;
                        this.ClienteApMaSearch = null;
                    }
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Error: {0}", e);
                throw;
            }
        }
        //===========================================================================================================================================================================================================
        Models.NuevoCliente NC = new Models.NuevoCliente();
        public void AgregarCliente()
        {
            try
            {
                var nombrecomp = this.ClienteNombreSearch + " " + this.ClienteApPaSearch + " " + this.ClienteApMaSearch;
                var celverif = _proxy.CheckCelular(_common.PreparePhone(this.ClienteCelular1));
                var existname = _proxy.CheckNombreC(nombrecomp);
                this.Colonias = _proxy.FindColonias(this.ClienteCP);
                var cajero = _settings.Cajero.Id;

                if (!existname)
                {
                    if (!celverif)
                    {
                        NC.Nombre = this.ClienteNombreSearch;
                        NC.ApPaterno = this.ClienteApPaSearch;
                        NC.ApMaterno = this.ClienteApMaSearch;
                        NC.CodigoPostal = this.ClienteCP;
                        NC.Calle = this.ClienteCalle;
                        NC.Numero = Convert.ToInt16(this.ClienteNumero);
                        NC.Celular1 = _common.PreparePhone(this.ClienteCelular1);
                        NC.Celular = _common.PreparePhone(this.ClienteCelular);
                        NC.Email = this.ClienteEmail;
                        NC.Colonia = this.Colonias.Where(i => i.Nombre == this.ClienteColonia && i.CodigoPostal == this.ClienteCP).Single();
                        NC.Sexo = this.ClienteSexo;
                        NC.Identificacion = this.ClienteIdentificacion;
                        NC.Idusuario = cajero;

                        //this.Cliente = new Common.Entities.Cliente
                        //{
                        //    Nombre = this.ClienteNombreSearch,
                        //    ApPaterno = this.ClienteApPaSearch,
                        //    ApMaterno = this.ClienteApMaSearch,
                        //    CodigoPostal = this.ClienteCP,
                        //    Calle = this.ClienteCalle,
                        //    Numero = Convert.ToInt16(this.ClienteNumero),
                        //    Celular1 = _common.PreparePhone(this.ClienteCelular1),
                        //    Celular = _common.PreparePhone(this.ClienteCelular),
                        //    Email = this.ClienteEmail,
                        //    Colonia = this.Colonias.Where(i => i.Nombre == this.ClienteColonia && i.CodigoPostal == this.ClienteCP).Single().Id,
                        //    Sexo = this.ClienteSexo,
                        //    Idusuario = cajero
                        //};

                        //Messenger.Default.Send(new Messages.NuevoClienteMessage
                        //{
                        //    Cliente = NC
                        //}, this.GID);
                        this.AcceptCommand.RaiseCanExecuteChanged();

                    }
                    else
                    {
                        MessageBox.Show("Ya existe un cliente registrado con el mismo número de celular.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Ya existe un cliente registrado con el mismo nombre.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Error: {0}", e);
                throw;
            }
        }
        //===========================================================================================================================================================================================================
        public void Clientexd(string name, string appaterno, string apmaterno, string codigopostal, string calle, int numero, string celular, string cel, string email, string colonia, string sexo, string identificacion)
        {
            try
            {
                _CV.ActualizarCliente();
                int colid;
                if (codigopostal != "" && colonia != "")
                {
                    colid = _proxy.FindColidByName(colonia, codigopostal);
                }
                else
                {
                    colid = 0;
                }
                if (colonia == "")
                {
                    colonia = " ";
                }

                if (name != name1 || appaterno != appa1 || apmaterno != apma1
                    || codigopostal != codigopostal1 || calle != calle1 || numero != numero1
                    || email != email1 || colonia1 != colonia2 || sexo1 != sexo2 || celular1 != celular12 || celular != celular2 || identificacion != identif1)
                {
                    var celular1 = _common.PreparePhone(celular);
                    var celu = _common.PreparePhone(cel);
                    string nc = name1 + " " + appa1 + " " + apma1;
                    var cajero = _settings.Cajero.Id;
                    var datos = Convert.ToInt32(_proxy.Clientexd(name, appaterno, apmaterno, codigopostal, calle, numero, celular1, celu, email, colonia, cajero, sexo, identificacion));
                    Common.Constants.ClienteInfo.colonia = datos;
                }

            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Error: {0}", e);
                throw;
            }
        }
        public void Busca(string celular)
        {
            try
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
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Error: {0}", e);
                throw;
            }
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
                case nameof(ClienteCP):
                case nameof(ClienteColonia):
                case nameof(ClienteCalle):
                case nameof(ClienteCelular1):
                case nameof(ClienteSexo):
                    {
                        if(this.ClienteCP != null && this.ClienteColonia != null && this.ClienteCalle != null
                            && this.ClienteCelular1 != null && this.ClienteSexo != null)
                        {
                            var nombrecomp = this.ClienteNombreSearch + " " + this.ClienteApPaSearch + " " + this.ClienteApMaSearch;
                            var celverif = _proxy.CheckCelular(_common.PreparePhone(this.ClienteCelular1));
                            var existname = _proxy.CheckNombreC(nombrecomp);

                            if (!existname && !celverif)
                            {
                                AgregarCliente();
                            }
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
            try
            {
                this.Colonias = _proxy.FindColonias(SirCoPOS.Common.Constants.ClienteInfo.cp);
                SirCoPOS.Common.Constants.ClienteInfo.Colonias = this.Colonias;
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Error: {0}", e);
                throw;
            }
        }

        public void RefrescarColonias(string cp)
        {
            try
            {
                this.Colonias = null;
                this.Colonias = _proxy.FindColonias(cp.ToString());
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Error: {0}", e);
                throw;
            }
        }
        public string FindColonia(int coloniaid)
        {
            try
            {
                var ret = _proxy.FindColonia(coloniaid);
                return ret;
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Error: {0}", e);
                throw;
            }
        }

        public void actualizainfo()
        {
            try
            {
                name2 = Common.Constants.ClienteDato.nombre;
                appa2 = Common.Constants.ClienteDato.appa;
                apma2 = Common.Constants.ClienteDato.apma;
                codigopostal2 = Common.Constants.ClienteDato.cp;
                calle2 = Common.Constants.ClienteDato.calle;
                numero2 = Common.Constants.ClienteDato.numero;
                celular2 = Common.Constants.ClienteDato.celular;
                email2 = Common.Constants.ClienteDato.email;
                colname = Common.Constants.ClienteDato.colonia;
                sexo2 = Common.Constants.ClienteDato.sexo;
                celular12 = Common.Constants.ClienteDato.celular1;

                if (codigopostal2 != "" && colname != "")
                {
                    colonia2 = _proxy.FindColidByName(colname, codigopostal2);
                }
                else
                {
                    colonia2 = 0;
                }
                Clientexd(name2, appa2, apma2, codigopostal2, calle2, numero2, celular2, celular12, email2, colname, sexo2, identif2);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Error: {0}", e);
                throw;
            }
        }

        public bool CloseTab { get { return false; } }

        protected override void Accept()
        {
            if (NC.Nombre != null && NC.ApPaterno != null && NC.ApMaterno != null)
            {
                this.Screen = "new";
                    Messenger.Default.Send(new Messages.NuevoClienteMessage
                    {
                        Cliente = NC
                    }, this.GID);
            }
            if (this.Screen == "search")
            {
                actualizainfo();
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
            try
            {
            if(NC.Nombre != null && NC.ApPaterno != null && NC.ApMaterno != null)
                return NC.IsValid();
            if(this.Screen == "search")
                return this.Cliente != null;
            return false;
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Error: {0}", e);
                throw;
            }
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
        //DATOS CLIENTE NUEVO
        public string _ClienteNombre;
        public string ClienteNombre
        {
            get => _ClienteNombre;
            set => this.Set(nameof(ClienteNombre), ref _ClienteNombre, value);
        }
        public string _ClienteApPaterno;
        public string ClienteApPaterno {
            get => _ClienteApPaterno;
            set => this.Set(nameof(ClienteApPaterno), ref _ClienteApPaterno, value);
        }
        public string _ClienteApMaterno;
        public string ClienteApMaterno
        {
            get => _ClienteApMaterno;
            set => this.Set(nameof(ClienteApMaterno), ref _ClienteApMaterno, value);
        }
        public string _ClienteCP;
        public string ClienteCP
        {
            get => _ClienteCP;
            set => this.Set(nameof(ClienteCP), ref _ClienteCP, value);
        }
        public string _ClienteColonia;
        public string ClienteColonia
        {
            get => _ClienteColonia;
            set => this.Set(nameof(ClienteColonia), ref _ClienteColonia, value);
        }
        public string _ClienteCalle;
        public string ClienteCalle
        {
            get => _ClienteCalle;
            set => this.Set(nameof(ClienteCalle), ref _ClienteCalle, value);
        }
        public string _ClienteNumero;
        public string ClienteNumero
        {
            get => _ClienteNumero;
            set => this.Set(nameof(ClienteNumero), ref _ClienteNumero, value);
        }
        public string _ClienteCelular1;
        public string ClienteCelular1
        {
            get => _ClienteCelular1;
            set => this.Set(nameof(ClienteCelular1), ref _ClienteCelular1, value);
        }
        public string _ClienteCelular;
        public string ClienteCelular
        {
            get => _ClienteCelular;
            set => this.Set(nameof(ClienteCelular), ref _ClienteCelular, value);
        }
        public string _ClienteEmail;
        public string ClienteEmail
        {
            get => _ClienteEmail;
            set => this.Set(nameof(ClienteEmail), ref _ClienteEmail, value);
        }
        public string _ClienteSexo;
        public string ClienteSexo
        {
            get => _ClienteSexo;
            set => this.Set(nameof(ClienteSexo), ref _ClienteSexo, value);
        }
        public string _ClienteIdentificacion;
        public string ClienteIdentificacion
        {
            get => _ClienteIdentificacion;
            set => this.Set(nameof(ClienteIdentificacion), ref _ClienteIdentificacion, value);
        }
        #endregion
        #region commands
        public RelayCommand<string> ChangeViewCommand { get; private set; }
        public RelayCommand SearchCommand { get; private set; }
        public RelayCommand searchnameCommand { get; private set; }
        public RelayCommand agregarclientecommand { get; private set; }
        public RelayCommand PopUpCommand { get; private set; }
        #endregion
    }
}
