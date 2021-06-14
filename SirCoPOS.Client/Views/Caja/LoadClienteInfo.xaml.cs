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
    /// Interaction logic for LoadClienteNewView.xaml
    /// </summary>
    public partial class LoadClienteInfo : UserControl
    {
        SirCoPOS.Client.ViewModels.Caja.LoadClienteViewModel CLI = new SirCoPOS.Client.ViewModels.Caja.LoadClienteViewModel();
        public int opci = 0;
        SirCoPOS.Services.DataService DS = new SirCoPOS.Services.DataService();
        public LoadClienteInfo()
        {
            InitializeComponent();
            CargaInfo();
            SirCoPOS.Common.Constants.ClienteInfo.opcion = 0;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.tbNombre.Focus();
        }

        public void CargaInfo()
        {
            this.tbNombre.Text = SirCoPOS.Common.Constants.ClienteInfo.nombre;
            this.tbApPa.Text = SirCoPOS.Common.Constants.ClienteInfo.appa;
            this.tbApMa.Text = SirCoPOS.Common.Constants.ClienteInfo.apma;
            this.tbCalle.Text = SirCoPOS.Common.Constants.ClienteInfo.calle;
            this.tbNumero.Text = SirCoPOS.Common.Constants.ClienteInfo.numero;
            this.tbCP.Text = SirCoPOS.Common.Constants.ClienteInfo.cp;
            this.tbCelular.Text = SirCoPOS.Common.Constants.ClienteInfo.celular;
            this.lblCiudad.Text = SirCoPOS.Common.Constants.ClienteInfo.ciudad;
            this.lblEstado.Text = SirCoPOS.Common.Constants.ClienteInfo.estado;
            this.tbEmail.Text = SirCoPOS.Common.Constants.ClienteInfo.email;
            CLI.TraerColonias();

            foreach (var col in SirCoPOS.Common.Constants.ClienteInfo.Colonias)
            {
                cbColonia.Items.Add(col.Nombre.ToString());
            }
            cbSexo.Items.Add("M");
            cbSexo.Items.Add("F");
            cbColonia.SelectedIndex = cbColonia.Items.IndexOf(SirCoPOS.Common.Constants.ClienteInfo.colonia);
            cbSexo.SelectedIndex = cbSexo.Items.IndexOf(SirCoPOS.Common.Constants.ClienteInfo.sexo);

            this.tbNombre.IsEnabled = false;
            this.tbApPa.IsEnabled = false;
            this.tbApMa.IsEnabled = false;
            this.tbCalle.IsEnabled = false;
            this.tbNumero.IsEnabled = false;
            this.tbCP.IsEnabled = false;
            this.tbCelular.IsEnabled = false;
            this.lblCiudad.IsEnabled = false;
            this.lblEstado.IsEnabled = false;
            this.tbRef.IsEnabled = false;
            this.tbEmail.IsEnabled = false;
            this.cbColonia.IsEnabled = false;
            this.cbSexo.IsEnabled = false;
        }
        public class Filas
        {
            public string Nombre { set; get; }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (SirCoPOS.Common.Constants.ClienteInfo.opcion == 0)
            {
                this.tbNombre.IsEnabled = true;
                this.tbApPa.IsEnabled = true;
                this.tbApMa.IsEnabled = true;
                this.tbCalle.IsEnabled = true;
                this.tbNumero.IsEnabled = true;
                this.tbCP.IsEnabled = true;
                this.tbCelular.IsEnabled = true;
                this.lblCiudad.IsEnabled = true;
                this.lblEstado.IsEnabled = true;
                this.tbRef.IsEnabled = true;
                this.tbEmail.IsEnabled = true;
                this.cbColonia.IsEnabled = true;
                this.cbSexo.IsEnabled = true;

                chbOpcion.IsChecked = true;
            }
            else
            {
                this.tbNombre.IsEnabled = false;
                this.tbApPa.IsEnabled = false;
                this.tbApMa.IsEnabled = false;
                this.tbCalle.IsEnabled = false;
                this.tbNumero.IsEnabled = false;
                this.tbCP.IsEnabled = false;
                this.tbCelular.IsEnabled = false;
                this.lblCiudad.IsEnabled = false;
                this.lblEstado.IsEnabled = false;
                this.tbRef.IsEnabled = false;
                this.tbEmail.IsEnabled = false;
                this.cbColonia.IsEnabled = false;
                this.cbSexo.IsEnabled = false;

                chbOpcion.IsChecked = false;
            }
        }

        private void tbCP_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void tbCP_LostFocus(object sender, RoutedEventArgs e)
        {
            cbColonia.Items.Clear();
            CLI.RefrescarColonias(tbCP.Text);
            foreach (var col in SirCoPOS.Common.Constants.ClienteInfo.Colonias)
            {
                cbColonia.Items.Add(col.Nombre.ToString());
            }
            lblCiudad.Text = "";
            lblEstado.Text = "";
            lblCiudad.Text = SirCoPOS.Common.Constants.ClienteInfo.ciudad;
            lblEstado.Text = SirCoPOS.Common.Constants.ClienteInfo.estado;
        }

        private void chbOpcion_Checked(object sender, RoutedEventArgs e)
        {
            SirCoPOS.Common.Constants.ClienteInfo.opcion = 1;
        }

        private void chbOpcion_Unchecked(object sender, RoutedEventArgs e)
        {
            SirCoPOS.Common.Constants.ClienteInfo.opcion = 2;
        }
    }
}
