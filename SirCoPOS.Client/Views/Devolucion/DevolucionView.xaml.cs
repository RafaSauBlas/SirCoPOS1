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

namespace SirCoPOS.Client.Views.Devolucion
{
    /// <summary>
    /// Interaction logic for DevolucionView.xaml
    /// </summary>
    [Utilities.Extensions.ExportModal]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Utilities.Extensions.MetadataModal(Utilities.Constants.Modals.devolucion)]
    public partial class DevolucionView : UserControl
    {
        public string FTP = "http://201.148.82.174/FOTOS/";
        public string IPP = @"\\10.10.1.1\Sistema\ZT\Fotos\";
        public DevolucionView()
        {
            InitializeComponent();
        }

        private void txtMarca_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtModelo_TextChanged(object sender, TextChangedEventArgs e)
        {
            imagen();
        }

        public void imagen()
        {
            var Marca = this.txtMarca.Text;
            var Modelo = this.txtModelo.Text;
            var Modelo2 = Modelo.Replace(" ", "_");

            var imagen = new BitmapImage(new Uri(FTP + Marca + Modelo2 + "F3.png"));
            var imgpath = IPP + Marca + Modelo2 + "F1.jpg";
            double alto = imagen.Height;
            double ancho = imagen.Width;
            if (alto == 1 && ancho == 1)
            {
                if (!File.Exists(imgpath))
                {
                    imagen = null;
                }
                else
                {
                    imagen = new BitmapImage(new Uri(imgpath));
                }
            }
            PB.Source = imagen;
            PB.Stretch = Stretch.Fill;

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.cb_opciones.Focus();
        }
    }
}
