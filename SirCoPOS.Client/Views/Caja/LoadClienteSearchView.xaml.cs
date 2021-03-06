using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
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
            Messenger.Default.Register<string>(this, "FocusTel", FocusTel);
            Messenger.Default.Register<string>(this, "FocusCol", FocusCol);
            Messenger.Default.Register<string>(this, "MensajeTelefono", MensajeTelefono);
            this.txt_Telefono.Focus();
        }

        private void txt_Nombre_KeyDown(object sender, KeyEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
            txt_Nombre.CharacterCasing = CharacterCasing.Upper;
        }

        private void txtAppa_KeyDown(object sender, KeyEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
            txtAppa.CharacterCasing = CharacterCasing.Upper;
        }

        private void txtApma_KeyDown(object sender, KeyEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
            txtApma.CharacterCasing = CharacterCasing.Upper;
        }

        private void txt_ciudad_KeyDown(object sender, KeyEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
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
            else
            {
                //cbColonia.Items.Clear();
                //cbColonia.SelectedItem = null;
                //cbColonia.Text = "";
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

         string name = this.txt_Nombre.Text;
         string appa = this.txtAppa.Text;
         string apma = this.txtApma.Text;
         string codigopostal = this.txt_cp.Text;
         string calle = this.txtCalle.Text;
         short numero;
            if (txtNumero.Text == "")
            {
             numero = 0;
            }
            else
            {
                numero = Convert.ToInt16(txtNumero.Text);
            }
         string celular = this.txttel.Text;
         string celular1 = this.txttel2.Text;
         string email = this.txtemail.Text;
         string colonia = this.cbColonia.Text;
         string sexo = this.cbSexo.Text;
         string ine = this.txtidentif.Text;

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
        Common.Constants.ClienteDato.identif = ine;
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
                MessageBox.Show("Necesita introducir un nombre para continuar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtAppa.Text == "")
            {
                txtAppa.Focus();
                MessageBox.Show("Necesita introducir un apellido paterno para continuar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtApma.Text == "")
            {
                txtApma.Focus();
                MessageBox.Show("Necesita introducir un apellido materno para continuar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txt_cp.Text == "")
            {
                txt_cp.Focus();
                MessageBox.Show("Necesita introducir un código postal para continuar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (cbColonia.Text == null)
            {
                cbColonia.Focus();
                MessageBox.Show("Necesita seleccionar una colonia para continuar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if(txtCalle.Text == "")
            {
                txtCalle.Focus();
                MessageBox.Show("Necesita introducir una calle para continuar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            txtNumero.SelectAll();
        }

        private void cbSexo_LostFocus(object sender, RoutedEventArgs e)
        {
            ActualizarCliente();
        }

        private void cbSexo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
            if (cbSexo.Text != null)
            {
                cbSexo.BorderBrush = Brushes.LightGray;
                ActualizarCliente();
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
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
            if (this.cbColonia.Text != "")
            {
                this.cbColonia.BorderBrush = Brushes.Gray;
                ActualizarCliente();
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
                ActualizarCliente();
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
                ActualizarCliente();
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
                MessageBox.Show("Necesita introducir un nombre para continuar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtAppa.Text == "")
            {
                txtAppa.Focus();
                MessageBox.Show("Necesita introducir un apellido paterno para continuar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtApma.Text == "")
            {
                txtApma.Focus();
                MessageBox.Show("Necesita introducir un apellido materno para continuar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtCalle_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txt_Nombre.Text == "")
            {
                txt_Nombre.Focus();
                MessageBox.Show("Necesita introducir un nombre para continuar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtAppa.Text == "")
            {
                txtAppa.Focus();
                MessageBox.Show("Necesita introducir un apellido paterno para continuar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtApma.Text == "")
            {
                txtApma.Focus();
                MessageBox.Show("Necesita introducir un apellido materno para continuar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txt_cp.Text == "")
            {
                txt_cp.Focus();
                MessageBox.Show("Necesita introducir un código postal para continuar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (cbColonia.Text == null)
            {
                MessageBox.Show("Necesita seleccionar una colonia para continuar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txttel_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txt_Nombre.Text == "")
            {
                txt_Nombre.Focus();
                MessageBox.Show("Necesita introducir un nombre para continuar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtAppa.Text == "")
            {
                txtAppa.Focus();
                MessageBox.Show("Necesita introducir un apellido paterno para continuar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtApma.Text == "")
            {
                txtApma.Focus();
                MessageBox.Show("Necesita introducir un apellido materno para continuar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txt_cp.Text == "")
            {
                txt_cp.Focus();
                MessageBox.Show("Necesita introducir un código postal para continuar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (cbColonia.Text == null)
            {
                cbColonia.Focus();
                MessageBox.Show("Necesita seleccionar una colonia pra continuar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtCalle.Text == "")
            {
                txtCalle.Focus();
                MessageBox.Show("Necesita introducir una calle para continuar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        int men = 0;
        private void cbSexo_GotFocus(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
            if (txtidentif.Text == "")
            {
                if (men == 0)
                {
                    men = 1;
                    MessageBox.Show("Necesita introducir una identificación valida.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                txtidentif.Focus();
            }
            men = 0;
        }

        private void txtidentif_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txt_Nombre.Text == "")
            {
                txt_Nombre.Focus();
                MessageBox.Show("Necesita introducir un nombre para continuar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtAppa.Text == "")
            {
                txtAppa.Focus();
                MessageBox.Show("Necesita introducir un apellido paterno para continuar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtApma.Text == "")
            {
                txtApma.Focus();
                MessageBox.Show("Necesita introducir un apellido materno para continuar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txt_cp.Text == "")
            {
                txt_cp.Focus();
                MessageBox.Show("Necesita introducir un código postal para continuar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (cbColonia.Text == null)
            {
                cbColonia.Focus();
                MessageBox.Show("Necesita seleccionar una colonia pra continuar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtCalle.Text == "")
            {
                txtCalle.Focus();
                MessageBox.Show("Necesita introducir una calle para continuar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txttel.Text == "(___) ___-____")
            {
                txttel.Focus();
                MessageBox.Show("Necesita introducir un número celular para continuar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void txtidentif_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.txtidentif.Text != "")
            {
                this.txtidentif.BorderBrush = Brushes.LightGray;
                ActualizarCliente();
            }
            else
            {
                this.txtidentif.BorderBrush = Brushes.Red;
            }
        }

        public void FocusTel(string msg)
        {
            if (msg == "focus")
            {
                this.txttel.Focus();
                this.txttel.SelectAll();
            }

            if(msg == "focus2")
            {
                this.txtApma.Focus();
                this.txtApma.SelectAll();
            }
        }

        public void FocusCol(string msg)
        {
            if(msg == "focuscol")
            {
                if(this.cbColonia.Text == "")
                {
                    MessageBox.Show("Debe seleccionar una colonia valida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.cbColonia.Focus();
                }
                else
                {
                    this.cbColonia.Focus();
                }

                
            }
        }

        public void MensajeTelefono(string msg)
        {
            if (msg == "tel")
            {
                MessageBox.Show("Ya existe un cliente registrado con el mismo número de celular.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.txttel.Focus();
                this.txttel.SelectAll();
            }
        }

        //public void seleccionar()
        //{
        //    if(this.txt_Telefono.Text == "(___) ___-____")
        //    {
        //        this.txt_Telefono.Focus();
        //    }
        //    else
        //    {
        //        this.txt_Telefono.Focus();
        //        this.txt_Telefono.SelectAll();
        //    }
        //}

        //public void seleccionar2()
        //{
        //    if(this.txtCalle.Text == "")
        //    {
        //        this.txt_Nombre.Focus();
        //    }
        //}

        private void txt_Telefono_KeyUp(object sender, KeyEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
            if (e.Key == Key.Enter)
            {
                Messenger.Default.Send<string>("focus", "DoFocus");
            }
        }

        private void txtApma_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                ActualizarCliente();
                Messenger.Default.Send<string>("focus", "DoFocus");
            }
        }

        private void txtNumero_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int character = Convert.ToInt32(Convert.ToChar(e.Text));
            if (character >= 48 && character <= 57)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txtNumero_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txtNumero.Text != "")
            {
                ActualizarCliente();
            }
        }

        private void txttel_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
            if (e.Key == Key.Enter)
            {
                ActualizarCliente();
            }
        }

        private void txttel2_KeyDown(object sender, KeyEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
            if (e.Key == Key.Enter)
            {
                ActualizarCliente();
            }
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
        }

        private void txt_cp_KeyDown(object sender, KeyEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
        }

        private void cbColonia_GotFocus(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
            if (this.cbColonia.Text != "")
            {
                this.cbColonia.BorderBrush = Brushes.Gray;
            }
            else
            {
                this.cbColonia.BorderBrush = Brushes.Red;
            }
        }

        private void txttel_KeyDown(object sender, KeyEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    Messenger.Default.Send<string>("cerrar", "Cerrar");
        //}
    }
}
