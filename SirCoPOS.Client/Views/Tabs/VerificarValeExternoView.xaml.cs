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
using NLog;

namespace SirCoPOS.Client.Views.Tabs
{
    /// <summary>
    /// Interaction logic for VerificarValeExternoView.xaml
    /// </summary>
    [Utilities.Extensions.ExportView]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Utilities.Extensions.MetadataTab(Utilities.Constants.TabType.VerificarValeExterno)]
    public partial class VerificarValeExternoView : UserControl, Utilities.Interfaces.ITabView
    {

        private IDictionary<Guid, TabItem> _tabs;
        private ILogger _log;

        public VerificarValeExternoView()
        {
            InitializeComponent();

            _tabs = new Dictionary<Guid, TabItem>();
            _log = CommonServiceLocator.ServiceLocator.Current.GetInstance<ILogger>();
        }

        public void Init()
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

        private void PreviewTextInputOnlyLetters(object sender, TextCompositionEventArgs e)
        {
            int character = Convert.ToInt32(Convert.ToChar(e.Text));
            if ((character >= 65 && character <= 90) || (character >= 97 && character <= 122))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void PreviewTextInputOnlyNumbers(object sender, TextCompositionEventArgs e)

        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));

            if (ascci >= 48 && ascci <= 57) e.Handled = false;

            else e.Handled = true;

        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void UserControl_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
        }

        public void Seleccionar()
        {
            this.txtCuenta.Focus();
            this.txtCuenta.SelectAll();
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Seleccionar();
            }
        }

        private void txt_negocio_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(this.txt_negocio.Text.Length == 2)
            {
                this.txtCuenta.Focus();
            }
        }

        public void Detener(string msg)
        {
            if (msg == "stop")
            {
                Messenger.Default.Send<string>("detener", "detener");
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Register<string>(this, "Detener", Detener);
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
        }

        private void tbVale_KeyDown(object sender, KeyEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
        }

        private void txt_negocio_KeyDown(object sender, KeyEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
        }

        private void txtCuenta_KeyDown(object sender, KeyEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
        }
    }
}
