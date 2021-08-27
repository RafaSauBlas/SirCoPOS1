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
    /// Interaction logic for PagoValeExternoView.xaml
    /// </summary>
    [Utilities.Extensions.ExportPagoView]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Utilities.Extensions.MetadataFormaPago(Common.Constants.FormaPago.VE)]
    public partial class PagoValeExternoView : UserControl
    {
        public PagoValeExternoView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.tbVale.Focus();
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
            if (cboPlazo != null && this.txtCuenta.Text.Length > 0)
            {
                cboPlazo.SelectedIndex = 7;
            }
        }

        public void seleccionar()
        {
            if(this.txtDistrib.Text != "" && this.txtDisponible.Text != "" && this.txtEstatus.Text != ""
                && this.txtDistribuidor.Text != "")
            {
                if (this.cboPromocion.IsEnabled == true)
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
                this.txtCuenta.Focus();
                this.txtCuenta.SelectAll();
            }
        }

        private void txtCuenta_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                seleccionar();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(this.txtNeg.Text.Length == 2)
            {
                this.txtCuenta.Focus();
            }
        }
    }
}
