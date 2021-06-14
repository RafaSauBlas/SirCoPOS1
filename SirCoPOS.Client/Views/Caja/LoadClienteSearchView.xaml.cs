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
    }
}
