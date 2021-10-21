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
using GalaSoft.MvvmLight.Messaging;

namespace SirCoPOS.Client.Views.Caja
{
    /// <summary>
    /// Interaction logic for PagoValeView.xaml
    /// </summary>
    [Utilities.Extensions.ExportPagoView]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Utilities.Extensions.MetadataFormaPago(Common.Constants.FormaPago.VA)]
    public partial class PagoValeView2 : UserControl
    {

        public PagoValeView2()
        {
            InitializeComponent();
            Messenger.Default.Register<string>(this, "NextFocus", nextFocus);
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

        private void cboPromocion_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            Messenger.Default.Send<string>("rest", "Reiniciar");
            if (cboPlazo != null && this.txtCuenta.Text.Length>0)
            {
                cboPlazo.SelectedIndex = 7;
            }
        }

        private void tbVale_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                tbVale.SelectAll();
            }
        }

        public void nextFocus(string msg)
        {
            if (msg == "focus")
                if (cboPromocion.IsEnabled)
                {
                    this.cboPromocion.Focus();
                }
            else
                {
                    this.cboPlazo.Focus();
                }
                
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            Messenger.Default.Send<string>("rest", "Reiniciar");
        }

        private void PagoVale_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void PagoVale_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void tbVale_GotFocus(object sender, RoutedEventArgs e)
        {
        }

        private void tbVale_TextChanged(object sender, TextChangedEventArgs e)
        {
            Messenger.Default.Send<string>("rest", "Reiniciar");
        }

        private void cboPlazo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Messenger.Default.Send<string>("rest", "Reiniciar");
        }
    }
}
