using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SirCoPOS.Win.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            
            Messenger.Default.Register<Messages.LoginResponse>(this, 
                m => {
                    if (this.NavigationService == null)
                        return;

                    if (!m.Success)
                    {
                        MessageBox.Show("El usuario o contraseña no son válidos.","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                        return;
                    }
                    var settings = CommonServiceLocator.ServiceLocator.Current.GetInstance<Utilities.Models.Settings>();
                    settings.Cajero = m.Empleado;
                    var proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.ICommonServiceAsync>();
                    settings.Sucursal = proxy.FindSucursal(Properties.Settings.Default.Sucursal);
                    
                    this.NavigationService.Navigate(new MenuPage());
                });
        }
    }
}
