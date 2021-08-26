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
            var opcion = Common.Constants.ClienteDato.opcion;
            if(opcion == 1)
            {
                txt_cp.IsEnabled = false;
                cbColonia.IsEnabled = false;
                txtCalle.IsEnabled = false;
                txtNumero.IsEnabled = false;
                txttel.IsEnabled = false;
                txttel2.IsEnabled = false;
                txtidentif.IsEnabled = false;
                txtemail.IsEnabled = false;
                cbSexo.IsEnabled = false;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.txt_Telefono.Focus();
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

        private void txt_cp_TextChanged(object sender, TextChangedEventArgs e)
        {
            CL = new Client.ViewModels.Caja.LoadClienteViewModel();
            this.txt_cp.BorderBrush = Brushes.LightGray;
            if (txt_cp.Text.Length == 5)
            {
                cbColonia.Items.Clear();
                CL.RefrescarColonias(txt_cp.Text);
                foreach (var col in CL.Colonias)
                {
                    cbColonia.Items.Add(col.Nombre.ToString());
                }
                cbColonia.SelectedIndex = cbColonia.Items.IndexOf(CL.FindColonia(SirCoPOS.Common.Constants.ClienteInfo.colonia));
            }
        }
        public void SeleccionarColonia()
        {
            CL = new Client.ViewModels.Caja.LoadClienteViewModel();
            if (txt_cp.Text.Length == 5)
            {
                cbColonia.SelectedIndex = cbColonia.Items.IndexOf(CL.FindColonia(SirCoPOS.Common.Constants.ClienteInfo.colonia));
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
         short numero;
            if (txtNumero.Text == "")
            {
             numero = 0;
            }
            else
            {
                numero = Convert.ToInt16(txtNumero.Text);
            }
         string celular = txttel.Text;
         string celular1 = txttel2.Text;
         string email = txtemail.Text;
         string colonia = cbColonia.Text;
         string sexo = cbSexo.Text;
        Common.Constants.ClienteDato.nombre = name;
        Common.Constants.ClienteDato.appa = appa;
        Common.Constants.ClienteDato.apma = apma;
        Common.Constants.ClienteDato.cp = codigopostal;
        Common.Constants.ClienteDato.calle = calle;
        Common.Constants.ClienteDato.numero = numero;
        Common.Constants.ClienteDato.celular = celular;
        Common.Constants.ClienteDato.email = email;
        Common.Constants.ClienteDato.colonia = colonia;
        Common.Constants.ClienteDato.sexo = sexo;
        Common.Constants.ClienteDato.celular1 = celular1;
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
            //this.txt_Nombre.Focus();
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

        private void txttel_LostFocus(object sender, RoutedEventArgs e)
        {
            ActualizarCliente();
        }

        private void txt_Telefono_LostFocus(object sender, RoutedEventArgs e)
        {
            ActualizarCliente();
        }

        private void txtNumero_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txt_Nombre.Text == "")
            {
                txt_Nombre.Focus();
                MessageBox.Show("NECESITA INTRODUCIR UN NOMBRE PARA CONTINUAR", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtAppa.Text == "")
            {
                txtAppa.Focus();
                MessageBox.Show("NECESITA INTRODUCIR UN APELLIDO PATERNO PARA CONTRINUAR.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtApma.Text == "")
            {
                txtApma.Focus();
                MessageBox.Show("NECESITA INTRODUCIR UN APELLIDO MATERNO PARA CONTINUAR", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txt_cp.Text == "")
            {
                txt_cp.Focus();
                MessageBox.Show("NECESITA INTRODUCIR UN CODIGO POSTAL PARA CONTINUAR", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (cbColonia.Text == null)
            {
                cbColonia.Focus();
                MessageBox.Show("NECESITA SELECCIONAR UNA COLONIA PARA CONTINUAR", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if(txtCalle.Text == "")
            {
                txtCalle.Focus();
                MessageBox.Show("NECESITA INTRODUCIR UNA CALLE PARA CONTINUAR", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            txtNumero.SelectAll();
        }

        private void cbSexo_LostFocus(object sender, RoutedEventArgs e)
        {
            ActualizarCliente();
        }

        private void cbSexo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ActualizarCliente();
            if(cbSexo.Text != null)
            {
                cbSexo.BorderBrush = Brushes.LightGray;
            }
            else
            {
                cbSexo.BorderBrush = Brushes.Red;
            }
        }

        private void txt_Nombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(this.txt_Nombre.Text != "")
            {
                this.txt_Nombre.BorderBrush = Brushes.LightGray;
            }
            else
            {
                this.txt_Nombre.BorderBrush = Brushes.Red;
            }
        }

        private void txtAppa_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.txtAppa.Text != "")
            {
                this.txtAppa.BorderBrush = Brushes.LightGray;
            }
            else
            {
                this.txtAppa.BorderBrush = Brushes.Red;
            }
        }

        private void txtApma_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.txtApma.Text != "")
            {
                this.txtApma.BorderBrush = Brushes.LightGray;
            }
            else
            {
                this.txtApma.BorderBrush = Brushes.Red;
            }
        }

        private void cbColonia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.cbColonia.Text != "")
            {
                this.cbColonia.BorderBrush = Brushes.Gray;
            }
            else
            {
                this.cbColonia.BorderBrush = Brushes.Red;
            }
        }

        private void txtCalle_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.txtCalle.Text != "")
            {
                this.txtCalle.BorderBrush = Brushes.LightGray;
            }
            else
            {
                this.txtCalle.BorderBrush = Brushes.Red;
            }
        }

        private void txttel_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.txttel.Text != "(___) ___-____")
            {
                this.txttel.BorderBrush = Brushes.LightGray;
            }
            else
            {
                this.txttel.BorderBrush = Brushes.Red;
            }
        }

        private void cbSexo_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.cbSexo.Text != "")
            {
                this.cbSexo.BorderBrush = Brushes.Gray;
            }
        }

        private void txt_cp_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txt_Nombre.Text == "")
            {
                txt_Nombre.Focus();
                MessageBox.Show("NECESITA INTRODUCIR UN NOMBRE PARA CONTINUAR", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtAppa.Text == "")
            {
                txtAppa.Focus();
                MessageBox.Show("NECESITA INTRODUCIR UN APELLIDO PATERNO PARA CONTRINUAR.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtApma.Text == "")
            {
                txtApma.Focus();
                MessageBox.Show("NECESITA INTRODUCIR UN APELLIDO MATERNO PARA CONTINUAR", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtCalle_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txt_Nombre.Text == "")
            {
                txt_Nombre.Focus();
                MessageBox.Show("NECESITA INTRODUCIR UN NOMBRE PARA CONTINUAR", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtAppa.Text == "")
            {
                txtAppa.Focus();
                MessageBox.Show("NECESITA INTRODUCIR UN APELLIDO PATERNO PARA CONTRINUAR.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtApma.Text == "")
            {
                txtApma.Focus();
                MessageBox.Show("NECESITA INTRODUCIR UN APELLIDO MATERNO PARA CONTINUAR", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txt_cp.Text == "")
            {
                txt_cp.Focus();
                MessageBox.Show("NECESITA INTRODUCIR UN CODIGO POSTAL PARA CONTINUAR", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (cbColonia.Text == null)
            {
                MessageBox.Show("NECESITA SELECCIONAR UNA COLONIA PARA CONTINUAR", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txttel_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txt_Nombre.Text == "")
            {
                txt_Nombre.Focus();
                MessageBox.Show("NECESITA INTRODUCIR UN NOMBRE PARA CONTINUAR", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtAppa.Text == "")
            {
                txtAppa.Focus();
                MessageBox.Show("NECESITA INTRODUCIR UN APELLIDO PATERNO PARA CONTRINUAR.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtApma.Text == "")
            {
                txtApma.Focus();
                MessageBox.Show("NECESITA INTRODUCIR UN APELLIDO MATERNO PARA CONTINUAR", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txt_cp.Text == "")
            {
                txt_cp.Focus();
                MessageBox.Show("NECESITA INTRODUCIR UN CODIGO POSTAL PARA CONTINUAR", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (cbColonia.Text == null)
            {
                cbColonia.Focus();
                MessageBox.Show("NECESITA SELECCIONAR UNA COLONIA PARA CONTINUAR", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtCalle.Text == "")
            {
                txtCalle.Focus();
                MessageBox.Show("NECESITA INTRODUCIR UNA CALLE PARA CONTINUAR", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cbSexo_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txt_Nombre.Text == "")
            {
                txt_Nombre.Focus();
                MessageBox.Show("NECESITA INTRODUCIR UN NOMBRE PARA CONTINUAR", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtAppa.Text == "")
            {
                txtAppa.Focus();
                MessageBox.Show("NECESITA INTRODUCIR UN APELLIDO PATERNO PARA CONTRINUAR.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtApma.Text == "")
            {
                txtApma.Focus();
                MessageBox.Show("NECESITA INTRODUCIR UN APELLIDO MATERNO PARA CONTINUAR", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txt_cp.Text == "")
            {
                txt_cp.Focus();
                MessageBox.Show("NECESITA INTRODUCIR UN CODIGO POSTAL PARA CONTINUAR", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (cbColonia.Text == null)
            {
                cbColonia.Focus();
                MessageBox.Show("NECESITA SELECCIONAR UNA COLONIA PARA CONTINUAR", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtCalle.Text == "")
            {
                txtCalle.Focus();
                MessageBox.Show("NECESITA INTRODUCIR UNA CALLE PARA CONTINUAR", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txttel.Text == "(___) ___-____")
            {
                txttel.Focus();
                MessageBox.Show("NECESITA INTRODUCIR UN NÚMERO CELULAR PARA CONTINUAR", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtidentif.Text == "")
            {
                txtidentif.Focus();
                MessageBox.Show("NECESITA INTRODUCIR UNA IFENTIFICACIÓN PARA CONTINUAR", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtidentif_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txt_Nombre.Text == "")
            {
                txt_Nombre.Focus();
                MessageBox.Show("NECESITA INTRODUCIR UN NOMBRE PARA CONTINUAR", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtAppa.Text == "")
            {
                txtAppa.Focus();
                MessageBox.Show("NECESITA INTRODUCIR UN APELLIDO PATERNO PARA CONTRINUAR.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtApma.Text == "")
            {
                txtApma.Focus();
                MessageBox.Show("NECESITA INTRODUCIR UN APELLIDO MATERNO PARA CONTINUAR", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txt_cp.Text == "")
            {
                txt_cp.Focus();
                MessageBox.Show("NECESITA INTRODUCIR UN CODIGO POSTAL PARA CONTINUAR", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (cbColonia.Text == null)
            {
                cbColonia.Focus();
                MessageBox.Show("NECESITA SELECCIONAR UNA COLONIA PARA CONTINUAR", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtCalle.Text == "")
            {
                txtCalle.Focus();
                MessageBox.Show("NECESITA INTRODUCIR UNA CALLE PARA CONTINUAR", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txttel.Text == "(___) ___-____")
            {
                txttel.Focus();
                MessageBox.Show("NECESITA INTRODUCIR UN NÚMERO CELULAR PARA CONTINUAR", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtidentif_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.txtidentif.Text != "")
            {
                this.txtidentif.BorderBrush = Brushes.LightGray;
            }
            else
            {
                this.txtidentif.BorderBrush = Brushes.Red;
            }
        }

        public void seleccionar()
        {
            if(this.txt_Telefono.Text == "(___) ___-____")
            {
                this.txt_Telefono.Focus();
            }
            else
            {
                this.txt_Telefono.Focus();
                this.txt_Telefono.SelectAll();
            }
        }

        public void seleccionar2()
        {
            if(this.txtCalle.Text == "")
            {
                this.txt_Nombre.Focus();
            }
        }

        private void txt_Telefono_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                seleccionar();
            }
        }

        private void txtApma_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                seleccionar2();
            }
        }
    }
}
