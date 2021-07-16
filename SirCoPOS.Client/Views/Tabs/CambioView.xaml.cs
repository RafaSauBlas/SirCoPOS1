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

namespace SirCoPOS.Client.Views.Tabs
{
    /// <summary>
    /// Interaction logic for CambioView.xaml
    /// </summary>
    public partial class CambioView : UserControl
    {
        public CambioView()
        {
            InitializeComponent();
        }

        private void scanTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var vm = (ViewModels.Tabs.CambioViewModel)this.DataContext;
            if (vm.Cajero.Depto == 3)
            {
                System.Windows.Clipboard.Clear();
            }
        }

    }

}

