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
    public partial class LoadClienteNewView : UserControl
    {
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
    }
}
