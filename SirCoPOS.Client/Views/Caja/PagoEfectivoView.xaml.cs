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
    /// Interaction logic for PagoEfectivoView.xaml
    /// </summary>
    [Utilities.Extensions.ExportPagoView]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Utilities.Extensions.MetadataFormaPago(Common.Constants.FormaPago.EF)]
    public partial class PagoEfectivoView : UserControl, Utilities.Interfaces.ITabView
    {
        public PagoEfectivoView()
        {
            InitializeComponent();
        }

        public void Init()
        {
            this.pagarConTextBox.SelectAll();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.pagarConTextBox.Focus();            
        }

        private void pagarTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            this.pagarTextBox.SelectAll();
        }
    }
}
