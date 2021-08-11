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
using System.Windows.Shapes;



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
        public CajaView4()
        {
            InitializeComponent();
            Messenger.Default.Register<string>(this, "DoFocus", doFocus);

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

        private void scanTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var vm = (ViewModels.Tabs.CajaViewModel)this.DataContext;
            if (vm.Cajero.Depto == 3)
            {
                System.Windows.Clipboard.Clear();
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
                var Marca = ((Client.Models.Producto)dgView.SelectedItem).Marca;
                var Modelo = ((Client.Models.Producto)dgView.SelectedItem).Modelo;
                var Modelo2 = Modelo.Replace(" ", "_");

                var imagen = new BitmapImage(new Uri(FTP + Marca + Modelo2 + "F3.png"));
                if (imagen == null)
                {
                    imagen = new BitmapImage(new Uri(IPP + Marca + Modelo2 + "F1.jpg"));
                }
                //MessageBox.Show(Marca + " " + Modelo2, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //"http://201.148.82.174/FOTOS/CHY___2996F4.png"
                PB.Source = imagen;

                PB.StretchDirection = StretchDirection.Both;
        }
    }
}
