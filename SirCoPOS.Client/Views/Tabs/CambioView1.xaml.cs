using System;
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
using System.Windows.Shapes;
using System.IO;
using GalaSoft.MvvmLight.Messaging;
using NLog;

namespace SirCoPOS.Client.Views.Tabs
{
    /// <summary>
    /// Interaction logic for CambioView1.xaml
    /// </summary>
    [Utilities.Extensions.ExportView]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Utilities.Extensions.MetadataTab(Utilities.Constants.TabType.Cambio)]
    public partial class CambioView1 : UserControl, Utilities.Interfaces.ITabView
    {
        public string FTP = "http://201.148.82.174/FOTOS/";
        public string IPP = @"\\10.10.1.1\Sistema\ZT\Fotos\";

        private IDictionary<Guid, TabItem> _tabs;
        private ILogger _log;

        public CambioView1()
        {
            InitializeComponent();
            _tabs = new Dictionary<Guid, TabItem>();
            _log = CommonServiceLocator.ServiceLocator.Current.GetInstance<ILogger>();
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            var dt = (System.Windows.Threading.DispatcherTimer)sender;
            dt.Stop();
            Messenger.Default.Send(new Utilities.Messages.LogoutTimeout());
        }

        public void Init()
        {
            var depto = (int)Common.Constants.Departamento.TDA;
            if (depto <= 2)
            {
                this.scanTextBox.IsEnabled = true;
                this.scanTextBox.Focus();
            }
            this.scanTextBox.Focus();
            var vm = (ViewModels.Tabs.CambioViewModel)this.DataContext;
            if (vm.Cajero.Depto == 3)
                this.scanTextBox.ContextMenu = new ContextMenu();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var viewModel = (ViewModels.Tabs.CambioViewModel)DataContext;
            if (viewModel.PrintCommand.CanExecute(null))
                viewModel.PrintCommand.Execute(null);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (ViewModels.Tabs.CambioViewModel)DataContext;
            if (viewModel.PrintCommand.CanExecute(null))
                viewModel.PrintCommand.Execute(null);
        }

        private void scanTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var vm = (ViewModels.Tabs.CambioViewModel)this.DataContext;
            if (vm.Cajero.Depto == 3)
            {
                System.Windows.Clipboard.Clear();
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var Marca = ((Client.Models.ProductoCambio)dgv_Prod.SelectedItem).OldItem.Marca;
            var Modelo = ((Client.Models.ProductoCambio)dgv_Prod.SelectedItem).OldItem.Modelo;
            var Modelo2 = Modelo.Replace(" ", "_");
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
            //MessageBox.Show(Marca + " " + Modelo2, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //"http://201.148.82.174/FOTOS/CHY___2996F4.png"
            PB.Source = imagen;

            PB.Stretch = Stretch.Fill;
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Grid_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void Grid_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void Grid_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        public void Seleccionar()
        {
            this.scanTextBox.Focus();
            this.scanTextBox.SelectAll();
        }

        private void scanTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Seleccionar();
            }
        }

        public void Detener(string msg)
        {
            if (msg == "stop")
            {
                Messenger.Default.Send<string>("detener", "detener");
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Register<string>(this, "Detener", Detener);
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
        }

        private void scanTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
        }

        private void dgv_Prod_KeyDown(object sender, KeyEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
        }

        private void dgv_Prod_KeyUp(object sender, KeyEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
        }

        private void dgv_Prod_MouseMove(object sender, MouseEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
        }

        private void ListBox_KeyDown(object sender, KeyEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
        }

        private void ListBox_KeyUp(object sender, KeyEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
        }

        private void ListBox_MouseMove(object sender, MouseEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
        }
    }
}
