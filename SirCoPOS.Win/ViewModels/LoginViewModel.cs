using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Win.ViewModels
{
    class LoginViewModel : Utilities.Helpers.ViewModelBase
    {
        private Common.ServiceContracts.ICommonServiceAsync _proxy;        
        public LoginViewModel()
        {
            if(!IsInDesignMode)
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.ICommonServiceAsync>();

            this.LoginCommand = new GalaSoft.MvvmLight.Command.RelayCommand(async () => {
                this.IsBusy = true;
                var pass = this.PasswordHandler();
                var item = await _proxy.LoginAsync(
                    sucursal: Properties.Settings.Default.Sucursal, 
                    user: this.UserName, 
                    pass: pass);
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(
                    new Messages.LoginResponse { Success = item != null, Empleado = item });
                this.Password = null;
                this.UserName = null;
                this.IsBusy = false;
            }, () => {
                return !string.IsNullOrEmpty(this.UserName) && 
                !string.IsNullOrEmpty(this.Password);
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
            switch (e.PropertyName)
            {
                case "UserName":
                case "Password":
                    this.LoginCommand.RaiseCanExecuteChanged();
                    break;
                case nameof(Scanning):
                    this.ScanCommand.RaiseCanExecuteChanged();
                    break;
            }
        }
        public Func<string> PasswordHandler;
        #region properties
        private string _user;
        private string _pass;
        public string UserName
        {
            get { return _user; }
            set { this.Set(nameof(this.UserName), ref _user, value); }
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
