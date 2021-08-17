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

namespace SirCoPOS.Client.Views.Caja
{
    /// <summary>
    /// Interaction logic for NotaView.xaml
    /// </summary>
    [Utilities.Extensions.ExportModal]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Utilities.Extensions.MetadataModal(Utilities.Constants.Modals.nota)]
    public partial class NotaView : UserControl
    {
        public NotaView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.tbPrecio.Focus();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
           txtRazon.CharacterCasing = CharacterCasing.Upper;
        }
    }
}
