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

        private void txt_cp_TextChanged(object sender, TextChangedEventArgs e)
        {
            SirCoPOS.Client.ViewModels.Caja.LoadClienteViewModel Cl = new SirCoPOS.Client.ViewModels.Caja.LoadClienteViewModel();
            cbColonia.SelectedIndex = cbColonia.Items.IndexOf(Cl.coloniaid);
        }
    }
}
