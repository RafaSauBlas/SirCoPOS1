using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Ports;
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
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class DemoMenuPage : Page
    {
        private Utilities.Models.Settings _settings;
        private Lazy<LoginPage> _loginPage;
        public DemoMenuPage()
        {
            InitializeComponent();
            //this.sucursalTextBox.Text = "01";
            _loginPage = new Lazy<LoginPage>();

            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Messages.LoginResponse>(this, lr => {
                if (lr.Success)
                {
                    var cajero = lr.Empleado;
                    this.cajeroTextBox.Text = $"{cajero.Nombre} {cajero.ApellidoPaterno} {cajero.ApellidoMaterno}";
                    this.sucursalTextBox.IsReadOnly = true;
                    //Properties.Settings.Default.Sucursal = this.sucursalTextBox.Text;
                    Properties.Settings.Default.Cajero = cajero.Usuario;
                    Properties.Settings.Default.Save();
                }
            });

            this.sucursalTextBox.Text = Properties.Settings.Default.Sucursal;
            if (!GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                var ports = SerialPort.GetPortNames();
                if (ports?.Any() ?? false)
                {
                    foreach (var item in ports)
                    {
                        this.comboBox.Items.Add(item);
                    }
                }

                _settings = CommonServiceLocator.ServiceLocator.Current.GetInstance<Utilities.Models.Settings>();
                var proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.ICommonServiceAsync>();
                var cajero = Properties.Settings.Default.Cajero;
                if (!String.IsNullOrEmpty(cajero))
                {
                    try
                    {
                        _settings.Cajero = proxy.FindCajero(cajero);
                        if (_settings.Cajero != null)
                        {
                            _settings.Sucursal = proxy.FindSucursal(this.sucursalTextBox.Text);
                            this.cajeroTextBox.Text = $"{_settings.Cajero.Nombre} {_settings.Cajero.ApellidoPaterno} {_settings.Cajero.ApellidoMaterno}";
                            this.sucursalTextBox.IsReadOnly = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                this.checkBox.IsChecked = Properties.Settings.Default.MultiCaja;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Pages.ReportsPage());
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Pages.PuntoDeVentaPage());
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            var w = new Tests.Windows.LoginWindow();
            w.Show();
        }

        private void Button3_Copy_Click(object sender, RoutedEventArgs e)
        {
            var w = new Tests.Windows.MenuWindow();
            w.Show();
        }

        private void Button3_Copy1_Click(object sender, RoutedEventArgs e)
        {
            var w = new Tests.Windows.CajaWindow();
            w.Show();
        }
        
        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            var win = new Tests.Windows.MainWindow();
            win.Show();
        }

        private void menuButton_Click(object sender, RoutedEventArgs e)
        {
            if (_settings.Cajero == null)
            {
                MessageBox.Show("No se ha cargado el cajero.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(this.comboBox.SelectedIndex != -1)
            _settings.Scanner = (string)this.comboBox.SelectedItem;
            _settings.MultiCaja = this.checkBox.IsChecked ?? false;
            Properties.Settings.Default.MultiCaja = _settings.MultiCaja;            
            Properties.Settings.Default.Save();
            this.NavigationService.Navigate(new MenuPage());
        }
        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            log();
        }
        private void log()
        {
            if (1>2)
            {
                MessageBox.Show("No se ha especificado la sucursal.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //Properties.Settings.Default.Sucursal = this.sucursalTextBox.Text;
            //Properties.Settings.Default.Save();
            this.NavigationService.Navigate(_loginPage.Value);
        }
        private void Button7_Click(object sender, RoutedEventArgs e)
        {
            var win = new Tests.Windows.ValidacionWindow();
            win.Show();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            this.sucursalTextBox.Text = null;
            this.cajeroTextBox.Text = null;
            _settings.Cajero = null;
            _settings.Sucursal = null;            
            Properties.Settings.Default.Reset();

            var ls = new Helpers.LocalStorage();
            ls.ClearAll();
        }

        private void button7_Copy_Click(object sender, RoutedEventArgs e)
        {
            var win = new Tests.Windows.PagosWindow();
            win.Show();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            //var win = new Windows.ModalWindow();
            //win.Width = 600;
            //win.Height = 400;
            //win.Content = new Views.Tabs.CajaView();
            //win.Show();
            throw new ApplicationException("test exception");
        }

        private void buttonConfig_Click(object sender, RoutedEventArgs e)
        {
            var p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "explorer.exe";
            p.StartInfo.Arguments = Environment.CurrentDirectory;
            p.Start();
        }

        private void buttonWeb_Click(object sender, RoutedEventArgs e)
        {
            var url = ConfigurationManager.AppSettings["baseUrl"];
            System.Diagnostics.Process.Start(url);            
        }

        private void buttonDebug_Click(object sender, RoutedEventArgs e)
        {
            var win = new Windows.DebugWindow();
            win.ShowDialog();
        }

        private void buttonserverex_Click(object sender, RoutedEventArgs e)
        {
            var proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
            proxy.test();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var p = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IServiceDuplex>();
            p.Connect("1");
            MessageBox.Show("connected");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var sa = new Helpers.SyncAgent();
            var success = sa.Sync();
            MessageBox.Show($"{success}");
        }
    }
}
