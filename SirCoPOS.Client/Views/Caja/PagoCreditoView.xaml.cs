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
    /// Interaction logic for PagoCreditoView.xaml
    /// </summary>
    [Utilities.Extensions.ExportPagoView]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Utilities.Extensions.MetadataFormaPago(Common.Constants.FormaPago.CP)]
    public partial class PagoCreditoView : UserControl
    {
        public PagoCreditoView()
        {
            InitializeComponent();
            Messenger.Default.Register<string>(this, "NextFocus", nextFocus);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.tbSearch.Focus();
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
                cboPlazo.SelectedIndex = 3;
            }
        }
        private void tbSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                tbSearch.SelectAll();
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
    }
}
