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
using GalaSoft.MvvmLight.Messaging;
using NLog;

namespace SirCoPOS.Client.Views.Tabs
{
    /// <summary>
    /// Interaction logic for GastoView.xaml
    /// </summary>
    [Utilities.Extensions.ExportView]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Utilities.Extensions.MetadataTab(Utilities.Constants.TabType.Gasto)]
    public partial class GastoView : UserControl, Utilities.Interfaces.ITabView
    {
        private IDictionary<Guid, TabItem> _tabs;
        Client.MetodoInactividad IN;
        private ILogger _log;

        public GastoView()
        {
            InitializeComponent();
            _tabs = new Dictionary<Guid, TabItem>();
            _log = CommonServiceLocator.ServiceLocator.Current.GetInstance<ILogger>();
        }

        public void Init()
        {
            
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void UserControl_KeyUp(object sender, KeyEventArgs e)
        {
          
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Register<string>(this, "Detener", Detener);
            IN = new Client.MetodoInactividad();
            this.txtMonto.Focus();
        }

        public void Detener(string msg)
        {
            if (msg == "stop")
            {
                IN.detener();
            }
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            IN.reiniciar();
        }

        private void txtMonto_TextChanged(object sender, TextChangedEventArgs e)
        {
            IN.reiniciar();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            IN.reiniciar();
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            IN.reiniciar();
        }
    }
}
