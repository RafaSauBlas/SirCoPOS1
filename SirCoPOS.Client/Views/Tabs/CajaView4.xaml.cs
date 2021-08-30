using System;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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
using System.IO;
using System.Windows.Shapes;
using NLog;
using System.Diagnostics;



namespace SirCoPOS.Client.Views.Tabs
{
    /// <summary>
    /// Interaction logic for CajaView4.xaml
    /// </summary>
    [Utilities.Extensions.ExportView]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    //[ExportMetadata("View", Utilities.Constants.TabType.Caja)]
    [Utilities.Extensions.MetadataTab(Utilities.Constants.TabType.Caja)]
    public partial class CajaView4 : UserControl, Utilities.Interfaces.ITabView
    {
        public string FTP = "http://201.148.82.174/FOTOS/";
        public string IPP = @"\\10.10.1.1\Sistema\ZT\Fotos\";
        private System.Windows.Threading.DispatcherTimer _dt;
        private IDictionary<Guid, TabItem> _tabs;
        private ILogger _log;

        public CajaView4()
        {
            InitializeComponent();
            _tabs = new Dictionary<Guid, TabItem>();
            Messenger.Default.Register<string>(this, "DoFocus", doFocus);
            _dt = new System.Windows.Threading.DispatcherTimer();
            _dt.Tick += Dt_Tick;
            _dt.Interval = TimeSpan.FromSeconds(Common.Constants.Inactividad.Segundos);
            _log = CommonServiceLocator.ServiceLocator.Current.GetInstance<ILogger>();
            this.RegisterMessages();

            _dt.Start();
        }

        private void RegisterMessages()
        {

            Messenger.Default.Register<Utilities.Messages.CloseTab>(this,
               m => {
                   Messenger.Default.Send(m, m.GID);
                   Console.WriteLine($"removing: {m.GID}");
                   if (!_tabs.Any())
                   {
                       _dt.Stop();
                   }
               });

            Messenger.Default.Register<Utilities.Messages.OpenModal>(this,
                m => {
                    Messenger.Default.Send(m, m.GID);
                    Console.WriteLine($"removing: {m.GID}");
                    if (!_tabs.Any())
                    {
                        _dt.Stop();
                    }
                });

            Messenger.Default.Register<Utilities.Messages.LogoutTimeout>(this, m => {
                _dt.Stop();
            });

            Messenger.Default.Register<Utilities.Messages.OpenModal>(this,
               m => {
                   Messenger.Default.Send(m, m.GID);
                   Console.WriteLine($"open: {m.GID}");       
                   _dt.Stop();
               });

            Messenger.Default.Register<Utilities.Messages.OpenModalItem>(this,
                m =>
                {
                    Messenger.Default.Send(m, m.GID);
                    Console.WriteLine($"open: {m.GID}");

                });
        }

        public void doFocus(string msg)
        {
            if (msg == "focus")
                this.scanTextBox.Focus();
        }
        public void Init()
        {
            this.scanTextBox.IsEnabled = true;
            this.scanTextBox.Focus();
            //var dato = new SirCoPOS.Common.Entities.Empleado();
            //int depto = dato.Depto;
            var vm = (ViewModels.Tabs.CajaViewModel)this.DataContext;
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            var dt = (System.Windows.Threading.DispatcherTimer)sender;
            dt.Stop();
            Messenger.Default.Send(new Utilities.Messages.LogoutTimeout());
        }

        private void scanTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
         {
            var vm = (ViewModels.Tabs.CajaViewModel)this.DataContext;
            if (vm.Cajero.Depto == 3)
            {
                System.Windows.Clipboard.Clear();
            }

            if(e.Key == Key.Enter)
            {

            }
        }

        private void DualButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new Windows.DualWindow();
            var view = new Views.CajaDualView();
            view.DataContext = this.DataContext;
            win.Content = view;
            //win.Top = _screen.Bounds.Top;
            //win.Left = _screen.Bounds.Left;
            win.Loaded += (se, ev) =>
            {
                //win.WindowState = WindowState.Maximized;
            };
            win.Show();

            //this.test.UpdateLayout();
            //this.test.Show();
        }


        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var Marca = "";
            var Modelo = "";
            var Modelo2 = "";

            if(dgView.SelectedItem == null)
            {
                PB.Source = null;
            }
            else
            {
                Marca = ((Client.Models.Producto)dgView.SelectedItem).Marca;
                Modelo = ((Client.Models.Producto)dgView.SelectedItem).Modelo;
                Modelo2 = Modelo.Replace(" ", "_");

                var imagen = new BitmapImage(new Uri(FTP + Marca + Modelo2 + "F3.png"));

                if (imagen.CanFreeze == false)
                {
                    if (!File.Exists(IPP + Marca + Modelo2 + "F1.jpg"))
                    {
                        imagen = null;
                    }
                    else
                    {
                        imagen = new BitmapImage(new Uri(IPP + Marca + Modelo2 + "F1.jpg"));
                    }
                }
                PB.Source = imagen;

                PB.Stretch = Stretch.Fill;
            }
        }

        private void ListBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                Common.Constants.ClienteDato.opcion = 0;
            }
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {       
                _dt.Stop();
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _dt.Stop();
        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _dt.Start();
        }

        private void UserControl_KeyUp(object sender, KeyEventArgs e)
        {
                _dt.Start();
        }

        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _dt.Stop();
        }

        private void Button_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _dt.Start();
        }

        private void dgView_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        public void seleccionar()
        {
            this.scanTextBox.Focus();
            this.scanTextBox.SelectAll();
        }

        private void scanTextBox_KeyUp(object sender, KeyEventArgs e)
        {
                if (e.Key == Key.Enter)
                {
                    seleccionar();
                }            
        }
    }
}
