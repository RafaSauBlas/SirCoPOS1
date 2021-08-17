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

namespace SirCoPOS.Client.Views.Tabs
{
    /// <summary>
    /// Interaction logic for CancelacionDevolucionView.xaml
    /// </summary>
    [Utilities.Extensions.ExportView]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Utilities.Extensions.MetadataTab(Utilities.Constants.TabType.CancelacionDevolucion)]
    public partial class CancelacionDevolucionView : UserControl
    {
        public CancelacionDevolucionView()
        {
            InitializeComponent();
            this.scanTextBox.Focus();
        }

        private void scanTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var vm = (ViewModels.Tabs.CancelacionDevolucionViewModel)this.DataContext;
            if (vm.Cajero.Depto == 3)
            {
                System.Windows.Clipboard.Clear();
            }
        }
    }
}
