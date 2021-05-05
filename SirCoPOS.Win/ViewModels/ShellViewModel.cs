using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Input;

namespace SirCoPOS.Win.ViewModels
{
    public class ShellViewModel : Utilities.Helpers.ViewModelBase    {
        public ShellViewModel()
        {
            this.KeyCommand = new RelayCommand<Key>(k => {
                Messenger.Default.Send(new Utilities.Messages.ShortcutMessage { Key = k });
            });

            //this.LoginCommand = new GalaSoft.MvvmLight.Command.RelayCommand(() =>
            //{
            //    var win = new Windows.LoginWindow();
            //    GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Messages.LoginResponse>(this, msg => {
            //        if (msg.Success)
            //        {
            //            win.Close();
            //            this.User = msg.Empleado.Usuario;


            //            var gi = new GenericIdentity(msg.Empleado.Usuario, "SirCoPOS");
            //            var gp = new GenericPrincipal(gi, new string[] { });
            //            Thread.CurrentPrincipal = gp;
            //            AppDomain.CurrentDomain.SetThreadPrincipal(gp);
            //        }                                       
            //    });                
            //    win.ShowDialog();
            //});

            if (IsInDesignMode)
            {
                this.User = "user";
                this.Version = "1.0.0.0";
                //this.Sucursal = "01";                
            }
            else
            {
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    this.Version = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
                    var qs = ApplicationDeployment.CurrentDeployment.ActivationUri?.Query;
                    if (!String.IsNullOrEmpty(Properties.Settings.Default.Sucursal))
                    {
                        this.Sucursal = Properties.Settings.Default.Sucursal;
                    }
                    else if (qs != null)
                    {
                        var nv = HttpUtility.ParseQueryString(qs);
                        this.Sucursal = nv["sucursal"];
                        Properties.Settings.Default.Sucursal = this.Sucursal;
                        Properties.Settings.Default.Save();
                    }                    
                    else
                    {
                        this.Sucursal = "N/A";
                        //MessageBox.Show("invalid argument");
                        //Application.Current.Shutdown();
                    }
                }
                else
                {
                    this.Version = "---";
                    this.Sucursal = "---";
                }
            }
        }

        #region properties        
        private string _user;
        private string _version;
        private string _sucursal;
        public string Sucursal
        {
            get { return _sucursal; }
            set { Set(nameof(this.Sucursal), ref _sucursal, value); }
        }
        public string User
        {
            get { return _user; }
            set { Set(nameof(this.User), ref _user, value); }
        }
        public string Version
        {
            get { return _version; }
            set { Set(nameof(this.Version), ref _version, value); }
        }
        #endregion
        #region commands
        public RelayCommand<Key> KeyCommand { get; private set; }
        #endregion
    }
}
