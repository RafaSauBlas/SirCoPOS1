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

namespace SirCoPOS.Client.Views.Caja
{
    /// <summary>
    /// Interaction logic for LoadClienteSearchView.xaml
    /// </summary>
    public partial class LoadClienteSearchView : UserControl
    {
        public Client.ViewModels.Caja.LoadClienteViewModel CL;

        public LoadClienteSearchView()
        {
            
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.txt_Telefono.Focus();
        }


        private void txt_Nombre_Copy_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void txt_Nombre_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void CambioVentana_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txt_Nombre_KeyDown(object sender, KeyEventArgs e)
        {
            txt_Nombre.CharacterCasing = CharacterCasing.Upper;
        }

        private void txtAppa_KeyDown(object sender, KeyEventArgs e)
        {
            txtAppa.CharacterCasing = CharacterCasing.Upper;
        }

        private void txtApma_KeyDown(object sender, KeyEventArgs e)
        {
            txtApma.CharacterCasing = CharacterCasing.Upper;
        }

        private void txt_ciudad_KeyDown(object sender, KeyEventArgs e)
        {
            txtCalle.CharacterCasing = CharacterCasing.Upper;
        }

        private void txt_cp_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            
            int character = Convert.ToInt32(Convert.ToChar(e.Text));
            if (character >= 48 && character <= 57)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txtemail_LostFocus(object sender, RoutedEventArgs e)
        {
            ActualizarCliente();
        }

        private void txt_Telefono_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void txt_cp_LostFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void txtemail_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void txt_cp_TextChanged(object sender, TextChangedEventArgs e)
        {
            CL = new Client.ViewModels.Caja.LoadClienteViewModel();
            if (txt_cp.Text.Length == 5)
            {
                cbColonia.Items.Clear();
                CL.RefrescarColonias(txt_cp.Text);
                foreach (var col in CL.Colonias)
                {
                    cbColonia.Items.Add(col.Nombre.ToString());
                }
                cbColonia.SelectedIndex = cbColonia.Items.IndexOf(CL.FindColonia());
            }
        }
        public void SeleccionarColonia()
        {
            CL = new Client.ViewModels.Caja.LoadClienteViewModel();
            if (txt_cp.Text.Length == 5)
            {
                cbColonia.SelectedIndex = cbColonia.Items.IndexOf(CL.FindColonia());
            }
        }
        public void ActualizarCliente()
        {
            CL = new Client.ViewModels.Caja.LoadClienteViewModel();
         string name = txt_Nombre.Text;
         string appa = txtAppa.Text;
         string apma = txtApma.Text;
         string codigopostal = txt_cp.Text;
         string calle = txtCalle.Text;
         short numero = Convert.ToInt16(txtNumero.Text);
         string celular = txttel.Text; 
         string email = txtemail.Text;
         string colonia = cbColonia.Text;
            CL.Clientexd(name, appa, apma, codigopostal, calle, numero, celular, email, colonia);
        }

        private void txt_Nombre_LostFocus(object sender, RoutedEventArgs e)
        {
            ActualizarCliente();
        }

        private void txtAppa_LostFocus(object sender, RoutedEventArgs e)
        {
            ActualizarCliente();
        }

        private void txtApma_LostFocus(object sender, RoutedEventArgs e)
        {
            ActualizarCliente();
        }

        private void cbColonia_LostFocus(object sender, RoutedEventArgs e)
        {
            ActualizarCliente();
        }

        private void txtCalle_LostFocus(object sender, RoutedEventArgs e)
        {
            ActualizarCliente();
        }

        private void txtNumero_LostFocus(object sender, RoutedEventArgs e)
        {
            ActualizarCliente();
        }

        private void txttel_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txttel_LostFocus(object sender, RoutedEventArgs e)
        {
            ActualizarCliente();
        }
    }
}
