using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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
using NLog;

namespace SirCoPOS.Client.Views.Tabs
{
    /// <summary>
    /// Interaction logic for VerificarValeView.xaml
    /// </summary>
    [Utilities.Extensions.ExportView]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Utilities.Extensions.MetadataTab(Utilities.Constants.TabType.VerificarVale)]
    public partial class VerificarValeView : UserControl, Utilities.Interfaces.ITabView
    {

        private IDictionary<Guid, TabItem> _tabs;
        private ILogger _log;


        public VerificarValeView()
        {
            InitializeComponent();
            _tabs = new Dictionary<Guid, TabItem>();
            _log = CommonServiceLocator.ServiceLocator.Current.GetInstance<ILogger>();
        }

        public void Init()
        {
            this.txt_buscar.Focus();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Register<string>(this, "Detener", Detener);
        }

        private void TabControl_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            var tc = (TabControl)sender;
            var tabs = tc.Items;
            if (tc.Items.Count > 0)
                tc.SelectedIndex = 0;
        }

        public void Detener(string msg)
        {
            if (msg == "stop")
            {
                Messenger.Default.Send<string>("detener", "detener");
            }
        }

        private void PreviewTextInputOnlyNumbers(object sender, TextCompositionEventArgs e)

        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));

            if (ascci >= 48 && ascci <= 57) e.Handled = false;

            else e.Handled = true;

        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void UserControl_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
          
        }

        private void TabControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void TabControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
           
        }

        public void Seleccionar()
        {
            this.txt_buscar.Focus();
            this.txt_buscar.SelectAll();
        }

        private void txt_buscar_KeyUp(object sender, KeyEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
            if (e.Key == Key.Enter)
            {
                Seleccionar();
            }
        }

        

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
           
        }
    }
}
