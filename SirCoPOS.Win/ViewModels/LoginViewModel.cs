using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Forms;
using GalaSoft.MvvmLight.Command;
using System.Windows;

namespace SirCoPOS.Win.ViewModels
{
    class LoginViewModel : Utilities.Helpers.ViewModelBase
    {
        private Common.ServiceContracts.ICommonServiceAsync _proxy;
        public LoginViewModel()
        {

            try
            {
                if (!IsInDesignMode)
                    _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.ICommonServiceAsync>();

                this.LoginCommand = new GalaSoft.MvvmLight.Command.RelayCommand(async () =>
                {
                    this.IsBusy = true;
                    var pass = this.PasswordHandler();
                    var item = await _proxy.LoginAsync(
                        sucursal: this.Sucursal,
                        user: this.UserName,
                        pass: pass);
                    if (item != null)
                    {
                        Properties.Settings.Default.Sucursal = this.Sucursal;
                        int secs = await _proxy.TimeOutAsync();
                        TimeSpan TimeOut = new TimeSpan(0, 0, secs);
                        Properties.Settings.Default.Timeout =TimeOut;
                        Properties.Settings.Default.Save();
                    }
                    GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(
                        new Messages.LoginResponse { Success = item != null, Empleado = item });
                    
                    
                    this.Password = null;
                    this.UserName = null;
                    this.Sucursal = null;
                    this.IsBusy = false;
                }, () =>
                {
                    return !string.IsNullOrEmpty(this.UserName)
                    && !string.IsNullOrEmpty(this.Password)
                    && !string.IsNullOrEmpty(this.Sucursal)
                    && this.Sucursal.Length ==2;
                });

                this.PropertyChanged += Login_PropertyChanged;
                this.ScanCommand = new GalaSoft.MvvmLight.Command.RelayCommand(async () =>
                {
                    this.Scanning = true;
                    var fh = new Helpers.FingerPrintHelper();
                    if (fh.Connect())
                    {
                        var finger = await fh.Scan();
                        fh.Close();
                        if (finger != null)
                        {
                            var item = _proxy.CheckFingerLogin(Properties.Settings.Default.Sucursal, finger);
                            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(
                                new Messages.LoginResponse { Success = item != null, Empleado = item });
                            this.Password = null;
                            this.UserName = null;
                        }
                    }
                    this.Scanning = false;
                }, () => !this.Scanning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("A handled exception just occurred: " + ex.Message, "Exception Sample");
            }
        }
        private void Login()
        { 
        
        }
        private bool _scanning;
        public bool Scanning
        {
            get => _scanning;
            set => this.Set(nameof(this.Scanning), ref _scanning, value);
        }

        private void Login_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            { 
            switch (e.PropertyName)
            {
                case "UserName":
                case "Password":
                case "Sucursal":
                    this.LoginCommand.RaiseCanExecuteChanged();
                    break;
                case nameof(Scanning):
                    this.ScanCommand.RaiseCanExecuteChanged();
                    break;
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show("A handled exception just occurred: " + ex.Message, "Exception Sample");
            }
        }
        public Func<string> PasswordHandler;
        #region properties
        private string _user;
        private string _pass;
        public string UserName
        {
            
            get { return _user; }
            set {
                if (Helpers.Usuario.Completar(value,8))
                {
                   SirCoPOS.Common.Entities.Empleado item = _proxy.FindEmpleado(value);
                   if (item != null)
                   {
                        if (item.Depto == (int)Common.Constants.Departamento.TDA)
                        {
                            Sucursal = item.Clave.Substring(0, 2);
                            NombreSucursal = item.Sucursal;
                            PedirSucursal = false;
                        }
                        else
                        {
                            Sucursal = "";
                            NombreSucursal = "";
                            PedirSucursal = true;
                        }
                    }
                   else
                    {
                        MessageBox.Show("Usuario NO existe ", "Acceso", MessageBoxButton.OK, MessageBoxImage.Information);
                        value = "";
                    }
                }
                else
                {
                    Sucursal = "";
                    NombreSucursal = "";
                    PedirSucursal = true;
                }
                this.Set(nameof(this.UserName), ref _user, value); 
            }
        }
        private string _suc;
        public string Sucursal
        {
            get { return _suc; }
            set {
                if (Helpers.Usuario.Completar(value, 2))
                {
                    var item = _proxy.FindSucursal(value);
                    if (item != null)
                    {
                        NombreSucursal = item.Descripcion;
                    }
                    else
                    {
                        MessageBox.Show("Sucursal NO existe ", "Acceso", MessageBoxButton.OK, MessageBoxImage.Information);
                        value = "";
                        NombreSucursal = "";
                    }
                }
                this.Set(nameof(this.Sucursal), ref _suc, value); 
            }
        }
        private bool _pedirsucursal;
        public bool PedirSucursal
        {
            get { return _pedirsucursal; }
            set { this.Set(nameof(this.PedirSucursal), ref _pedirsucursal, value); }
        }
        private string _nomsucursal;
        public string NombreSucursal
        {
            get { return _nomsucursal; }
            set { this.Set(nameof(this.NombreSucursal), ref _nomsucursal, value); }
        }
        public string Password
        {
            get { return _pass; }
            set { this.Set(nameof(this.Password), ref _pass, value); }
        }
        #endregion
        #region commands
        public GalaSoft.MvvmLight.Command.RelayCommand LoginCommand { get; private set; }
        public GalaSoft.MvvmLight.Command.RelayCommand ScanCommand { get; private set; }
        #endregion
    }
}
