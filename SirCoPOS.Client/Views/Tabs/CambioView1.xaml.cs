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

        public CambioView1()
        {
            InitializeComponent();
        }

        public void Init()
        {

            
            var depto = (int)Common.Constants.Departamento.TDA;
            if (depto <= 2)
            {
                this.scanTextBox.IsEnabled = true;
                this.scanTextBox.Focus();
            }
            var vm = (ViewModels.Tabs.CambioViewModel)this.DataContext;


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
            double alto = imagen.Height;
            double ancho = imagen.Width;
            if (alto == 1 && ancho == 1)
            {
                imagen = new BitmapImage(new Uri(IPP + Marca + Modelo2 + "F1.jpg"));
            }
            //MessageBox.Show(Marca + " " + Modelo2, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //"http://201.148.82.174/FOTOS/CHY___2996F4.png"
            PB.Source = imagen;

            PB.Stretch = Stretch.Fill;
        }
    }
}
