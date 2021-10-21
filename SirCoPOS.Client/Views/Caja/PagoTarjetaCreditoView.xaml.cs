using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using GalaSoft.MvvmLight.Messaging;
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

namespace SirCoPOS.Client.Views.Caja
{
    /// <summary>
    /// Interaction logic for PagoTarjetaCreditoView.xaml
    /// </summary>
    [Utilities.Extensions.ExportPagoView]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Utilities.Extensions.MetadataFormaPago(Common.Constants.FormaPago.TC)]
    public partial class PagoTarjetaCreditoView : UserControl
    {
        public PagoTarjetaCreditoView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.terminacionTextBox.Focus();
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            Messenger.Default.Send<string>("rest", "Reiniciar");
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Messenger.Default.Send<string>("rest", "Reiniciar");
        }

        private void terminacionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Messenger.Default.Send<string>("rest", "Reiniciar");
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            Messenger.Default.Send<string>("rest", "Reiniciar");
        }
    }
}
