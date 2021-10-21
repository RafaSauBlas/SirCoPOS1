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
    /// Interaction logic for PagoCreditoDistribuidorView.xaml
    /// </summary>
    [Utilities.Extensions.ExportPagoView]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Utilities.Extensions.MetadataFormaPago(Common.Constants.FormaPago.CD)]
    public partial class PagoCreditoDistribuidorView : UserControl
    {
        public PagoCreditoDistribuidorView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.tbDistrib.Focus();
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
                if (this.comboBox.IsEnabled == true)
                {
                    this.comboBox.Focus();
                }
                else
                {
                    this.cboPlazo.Focus();
                }
            }
            else
            {
                this.tbDistrib.Focus();
                this.tbDistrib.SelectAll();
            }
        }

        private void tbDistrib_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                seleccionar();
            }
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            Messenger.Default.Send<string>("rest", "Reiniciar");
        }

        private void tbDistrib_TextChanged(object sender, TextChangedEventArgs e)
        {
            Messenger.Default.Send<string>("rest", "Reiniciar");
        }

        private void cboPlazo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Messenger.Default.Send<string>("rest", "Reiniciar");
        }
    }
}
