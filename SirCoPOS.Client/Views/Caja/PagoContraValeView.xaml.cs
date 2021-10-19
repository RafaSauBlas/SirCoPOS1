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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SirCoPOS.Client.Views.Caja
{
    /// <summary>
    /// Interaction logic for PagoContraValeView.xaml
    /// </summary>
    [Utilities.Extensions.ExportPagoView]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Utilities.Extensions.MetadataFormaPago(Common.Constants.FormaPago.CV)]
    public partial class PagoContraValeView : UserControl
    {
        public PagoContraValeView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.tbSucursal.Focus();
        }

        private void TabControl_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            var tc = (TabControl)sender;
            var tabs = tc.Items;
            if (tc.Items.Count > 0)
                tc.SelectedIndex = 0;
        }

        private void cboPromocion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Messenger.Default.Send<string>("rest", "Reiniciar");
            if (cboPlazo != null && this.txtCuenta.Text.Length > 0)
            {
                cboPlazo.SelectedIndex = 7;
            }
        }

        public void seleccionar()
        {
            if(this.txtNoVale.Text != "" && this.txtDisponible.Text != "" && this.txtCuenta.Text != ""
                && this.txtEstatus.Text != "" && this.txtDistribuidor.Text != "")
            {
               if(this.cboPromocion.IsEnabled == true)
                {
                    this.cboPromocion.Focus();
                }
                else
                {
                    this.cboPlazo.Focus();
                }
            }
            else
            {
                this.tbVale.Focus();
                this.tbVale.SelectAll();
            }
        }

        private void tbVale_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                seleccionar();
            }
        }

        private void tbSucursal_TextChanged(object sender, TextChangedEventArgs e)
        {
            Messenger.Default.Send<string>("rest", "Reiniciar");
            if (this.tbSucursal.Text.Length == 2)
            {
                if(this.tbSucursal.Text == "01" || this.tbSucursal.Text == "02" || this.tbSucursal.Text == "06" 
                    || this.tbSucursal.Text == "08")
                {
                    this.tbVale.Focus();
                }
                else
                {
                    MessageBox.Show("La sucursal \"" + tbSucursal.Text + "\" NO es una sucursal válida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.tbSucursal.SelectAll();
                }
            }
        }

        private void tbVale_TextChanged(object sender, TextChangedEventArgs e)
        {
            Messenger.Default.Send<string>("rest", "Reiniciar");
            if (this.tbVale.Text == "" && this.txtNoVale.Text != "")
            {
                if(this.cboPromocion.IsEnabled == true)
                {
                    this.cboPromocion.Focus();
                }
                else
                {
                    this.cboPlazo.Focus();
                }
            }
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            Messenger.Default.Send<string>("rest", "Reiniciar");
        }

        private void cboPlazo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Messenger.Default.Send<string>("rest", "Reiniciar");
        }
    }
}
