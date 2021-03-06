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
    /// Interaction logic for PagoTarjetaDebitoView.xaml
    /// </summary>
    [Utilities.Extensions.ExportPagoView]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Utilities.Extensions.MetadataFormaPago(Common.Constants.FormaPago.TD)]
    public partial class PagoTarjetaDebitoView : UserControl
    {
        public PagoTarjetaDebitoView()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            this.terminacionTextBox.Focus();
            this.id.Visibility = Visibility.Collapsed;
            this.botones.Visibility = Visibility.Collapsed;
            this.checkBox.IsEnabled = true;
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
        private void maskedTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                return;
            }
        }
        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            Messenger.Default.Send<string>("rest", "Reiniciar");
        }

        private void ChkBox_CheckedChanged (object sender, EventArgs e)
        {
            if (checkBox.IsChecked == true)
            {
                this.botones.Visibility = Visibility.Visible;
                this.id.Visibility = Visibility.Visible;
            }
            else
            {
                this.id.Visibility = Visibility.Collapsed;
                this.botones.Visibility = Visibility.Collapsed;
            }
        }
    }
}
