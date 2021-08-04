using System;
using System.Collections.Generic;
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
using System.Text.RegularExpressions;

namespace SirCoPOS.Client.Views.Caja
{
    /// <summary>
    /// Interaction logic for LoadClienteNewView.xaml
    /// </summary>
    public partial class LoadClienteNewView : UserControl
    {
        private Common.ServiceContracts.IDataServiceAsync _proxy;
        private Helpers.CommonHelper _common;
        public LoadClienteNewView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.tbNombre.Focus();
        }

        private void tbNombre_KeyDown(object sender, KeyEventArgs e)
        {
            
            tbNombre.CharacterCasing = CharacterCasing.Upper;
        }

        private void tbApPa_KeyDown(object sender, KeyEventArgs e)
        {
            tbApPa.CharacterCasing = CharacterCasing.Upper;
        }

        private void tbApMa_KeyDown(object sender, KeyEventArgs e)
        {
            tbApMa.CharacterCasing = CharacterCasing.Upper;
        }

        private void tbCalle_KeyDown(object sender, KeyEventArgs e)
        {
            tbCalle.CharacterCasing = CharacterCasing.Upper;
        }

        private void tbCP_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int character = Convert.ToInt32(Convert.ToChar(e.Text));
            if (character >= 48 && character <= 57)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void tbNumero_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int character = Convert.ToInt32(Convert.ToChar(e.Text));
            if (character >= 48 && character <= 57)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void tbCelular_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int character = Convert.ToInt32(Convert.ToChar(e.Text));
            if (character >= 48 && character <= 57)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void tbCelular2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int character = Convert.ToInt32(Convert.ToChar(e.Text));
            if (character >= 48 && character <= 57)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void tbNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textboxSender = (TextBox)sender;
            var cursorPosition = tbNombre.SelectionStart;
            tbNombre.Text = Regex.Replace(textboxSender.Text, "[^a-zA-Z ]", "");
            tbNombre.SelectionStart = cursorPosition;
        }

        private void tbApPa_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textboxSender = (TextBox)sender;
            var cursorPosition = tbApPa.SelectionStart;
            tbApPa.Text = Regex.Replace(textboxSender.Text, "[^a-zA-Z ]", "");
            tbApPa.SelectionStart = cursorPosition;
        }

        private void tbApMa_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textboxSender = (TextBox)sender;
            var cursorPosition = tbApMa.SelectionStart;
            tbApMa.Text = Regex.Replace(textboxSender.Text, "[^a-zA-Z ]", "");
            tbApMa.SelectionStart = cursorPosition;
        }

        private void tbCalle_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textboxSender = (TextBox)sender;
            var cursorPosition = tbCalle.SelectionStart;
            tbCalle.Text = Regex.Replace(textboxSender.Text, "[^0-9a-zA-Z ]", "");
            tbCalle.SelectionStart = cursorPosition;
        }

        private void tbNumero_GotFocus(object sender, RoutedEventArgs e)
        {
            tbNumero.SelectAll();


        }

        private void tbApMa_LostFocus(object sender, RoutedEventArgs e)
        {
            _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
            var name = tbNombre.Text + " " + tbApPa.Text + " " + tbApMa.Text;
            var exists = _proxy.CheckNombreC(name);
            if (!exists)
            {
                
            }
            else
            {
                MessageBox.Show("YA HAY UN CLIENTE LLAMADO \"" + name + "\"", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                tbApPa.Text = "";
                tbApMa.Text = "";
            }
        }
        public void enfoca()
        {
            this.tbCelular.Text = "";
            this.tbCelular.Focus();
        }

        private void tbIdenti_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
