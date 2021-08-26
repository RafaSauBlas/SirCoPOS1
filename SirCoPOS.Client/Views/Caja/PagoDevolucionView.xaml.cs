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
                seleccionar();
            }
        }
    }
}
