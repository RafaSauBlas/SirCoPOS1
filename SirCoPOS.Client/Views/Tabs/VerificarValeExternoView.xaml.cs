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
    /// Interaction logic for VerificarValeExternoView.xaml
    /// </summary>
    [Utilities.Extensions.ExportView]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Utilities.Extensions.MetadataTab(Utilities.Constants.TabType.VerificarValeExterno)]
    public partial class VerificarValeExternoView : UserControl, Utilities.Interfaces.ITabView
    {
        public VerificarValeExternoView()
        {
            InitializeComponent();
        }

        public void Init()
        {
            this.tbVale.Focus();
        }

        private void TabControl_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            var tc = (TabControl)sender;
            var tabs = tc.Items;
            if (tc.Items.Count > 0)
                tc.SelectedIndex = 0;
        }

        private void PreviewTextInputOnlyLetters(object sender, TextCompositionEventArgs e)
        {
            int character = Convert.ToInt32(Convert.ToChar(e.Text));
            if ((character >= 65 && character <= 90) || (character >= 97 && character <= 122))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void PreviewTextInputOnlyNumbers(object sender, TextCompositionEventArgs e)

        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));

            if (ascci >= 48 && ascci <= 57) e.Handled = false;

            else e.Handled = true;

        }
    }
}
