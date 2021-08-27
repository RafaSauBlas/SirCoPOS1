﻿using System;
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
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.tbVale.Focus();
        }

        public void seleccionar()
        {
            if(this.txtNoVale.Text != "" && this.txtDisponible.Text != "" && this.txtCuenta.Text != ""
                && this.txtEstatus.Text != "" && this.txtDistribuidor.Text != "")
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
                this.tbVale.Focus();
                this.tbVale.SelectAll();
            }
        }

        private void TabControl_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            var tc = (TabControl)sender;
            var tabs = tc.Items;
            if (tc.Items.Count > 0)
                tc.SelectedIndex = 0;
        }

        private void cboPromocion_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (cboPlazo != null && this.txtCuenta.Text.Length>0)
            {
                cboPlazo.SelectedIndex = 7;
            }
        }

        private void tbVale_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                seleccionar();
            }
        }

        private void tbVale_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(this.tbVale.Text == "" || this.txtNoVale.Text != "")
            {
                this.cboPromocion.Focus();
            }
        }
    }
}
