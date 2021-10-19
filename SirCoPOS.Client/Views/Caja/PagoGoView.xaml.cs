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
    /// Interaction logic for PagoGoView.xaml
    /// </summary>
    [Utilities.Extensions.ExportPagoView]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Utilities.Extensions.MetadataFormaPago(Common.Constants.FormaPago.GO)]
    public partial class PagoGoView : UserControl, Utilities.Interfaces.ITabView
    {
        public PagoGoView()
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

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            Messenger.Default.Send<string>("rest", "Reiniciar");
        }

        private void pagarConTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Messenger.Default.Send<string>("rest", "Reiniciar");
        }

        private void pagarTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Messenger.Default.Send<string>("rest", "Reiniciar");
        }
    }
}
