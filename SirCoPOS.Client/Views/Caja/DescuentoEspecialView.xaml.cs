using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Messaging;
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
    /// Interaction logic for DescuentoEspecialView.xaml
    /// </summary>
    [Utilities.Extensions.ExportModal]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Utilities.Extensions.MetadataModal(Utilities.Constants.Modals.descuento)]
    public partial class DescuentoEspecialView : UserControl
    {
        public DescuentoEspecialView()
        {
            InitializeComponent();
        }

        private void txtDescrip_KeyDown(object sender, KeyEventArgs e)
        {
            txtDescrip.CharacterCasing = CharacterCasing.Upper;
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Messenger.Default.Send<string>("rest", "Reiniciar");
        }

        private void txtDescrip_TextChanged(object sender, TextChangedEventArgs e)
        {
            Messenger.Default.Send<string>("rest", "Reiniciar");
        }
    }
}
