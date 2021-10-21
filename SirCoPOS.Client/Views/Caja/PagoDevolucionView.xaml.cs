using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
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

namespace SirCoPOS.Client.Views.Caja
{
    /// <summary>
    /// Interaction logic for PagoDevolucionView.xaml
    /// </summary>
    [Utilities.Extensions.ExportPagoView]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Utilities.Extensions.MetadataFormaPago(Common.Constants.FormaPago.DV)]
    public partial class PagoDevolucionView : UserControl
    {
        public PagoDevolucionView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.tbSucursal.Focus();
        }

        public void seleccionar()
        {
            this.txtDevolucion.Focus();
            this.txtDevolucion.SelectAll();
        }

        private void txtDevolucion_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Messenger.Default.Send<string>("focus", "DoFocus");
            }
        }

        private void tbSucursal_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int character = Convert.ToInt32(Convert.ToChar(e.Text));
            if (character >= 48 && character <= 57)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void tbSucursal_TextChanged(object sender, TextChangedEventArgs e)
        {
            Messenger.Default.Send<string>("rest", "Reiniciar");
            if (this.tbSucursal.Text.Length == 2)
            {
                if(this.tbSucursal.Text == "01" || this.tbSucursal.Text == "02" || this.tbSucursal.Text == "06"
                    || this.tbSucursal.Text == "08")
                {
                    this.txtDevolucion.Focus();
                }
            }
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            Messenger.Default.Send<string>("rest", "Reiniciar");
        }

        private void txtDevolucion_TextChanged(object sender, TextChangedEventArgs e)
        {
            Messenger.Default.Send<string>("rest", "Reiniciar");
        }

        private void txtPagar_TextChanged(object sender, TextChangedEventArgs e)
        {
            Messenger.Default.Send<string>("rest", "Reiniciar");
        }
    }
}
