using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Messaging;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace SirCoPOS.Client.Views.Caja
{
    /// <summary>
    /// Interaction logic for NotaView.xaml
    /// </summary>
    [Utilities.Extensions.ExportModal]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Utilities.Extensions.MetadataModal(Utilities.Constants.Modals.nota)]
    public partial class NotaView : UserControl
    {
        public string Marca = "";
        public string Modelo = "";
        public string Modelo2 = "";
        public string FTP = "http://201.148.82.174/FOTOS/";
        public string IPP = @"\\10.10.1.1\Sistema\ZT\Fotos\";

        public NotaView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.tbPrecio.Focus();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            txtRazon.CharacterCasing = CharacterCasing.Upper;
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            Messenger.Default.Send<string>("rest", "Reiniciar");
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Messenger.Default.Send<string>("rest", "Reiniciar");
        }

        private void tbPrecio_TextChanged(object sender, TextChangedEventArgs e)
        {
            Messenger.Default.Send<string>("rest", "Reiniciar");
        }

        private void txtRazon_TextChanged(object sender, TextChangedEventArgs e)
        {
            Messenger.Default.Send<string>("rest", "Reiniciar");
        }

        private void ProductoItemView_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void tbPrecio_GotFocus(object sender, RoutedEventArgs e)
        {
            var imagen = new BitmapImage(new Uri(FTP + Common.Constants.ProductoDatos.marca + Common.Constants.ProductoDatos.modelo2 + "F3.png"));

            if (imagen.CanFreeze == false)
            {
                if (!File.Exists(IPP + Common.Constants.ProductoDatos.marca + Common.Constants.ProductoDatos.modelo2 + "F1.jpg"))
                {
                    imagen = null;
                }
                else
                {
                    imagen = new BitmapImage(new Uri(IPP + Common.Constants.ProductoDatos.marca + Common.Constants.ProductoDatos.modelo2 + "F1.jpg"));
                }
            }
            PB.Source = imagen;

            PB.Stretch = Stretch.Fill;
        }
    }
}
