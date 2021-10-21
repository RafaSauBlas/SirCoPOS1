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
    /// Interaction logic for CancelacionView.xaml
    /// </summary>
    [Utilities.Extensions.ExportView]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Utilities.Extensions.MetadataTab(Utilities.Constants.TabType.Cancelacion)]
    public partial class CancelacionView : UserControl, Utilities.Interfaces.ITabView
    {
        private IDictionary<Guid, TabItem> _tabs;
        Client.MetodoInactividad IN;
        private ILogger _log;

        public CancelacionView()
        {
            InitializeComponent();
            _tabs = new Dictionary<Guid, TabItem>();
            _log = CommonServiceLocator.ServiceLocator.Current.GetInstance<ILogger>();
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            var dt = (System.Windows.Threading.DispatcherTimer)sender;
            dt.Stop();
            Messenger.Default.Send(new Utilities.Messages.LogoutTimeout());
        }

        public void Detener(string msg)
        {
            if (msg == "stop")
            {
                IN.detener();
            }
        }

        public void Init()
        {
            var depto = (int)Common.Constants.Departamento.TDA;
            if (depto <= 2)
            {
                this.scanTextBox.IsEnabled = true;
                this.scanTextBox.Focus();
            }
            this.scanTextBox.Focus();
        }

        private void scanTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var vm = (ViewModels.Tabs.CancelacionViewModel)this.DataContext;
            if (vm.Cajero.Depto == 3)
            {
                System.Windows.Clipboard.Clear();
            }
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
           
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

        public void Seleccionar()
        {
            this.scanTextBox.Focus();
            this.scanTextBox.SelectAll();
        }

        private void scanTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Seleccionar();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Register<string>(this, "Detener", Detener);
            IN = new Client.MetodoInactividad();
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            IN.reiniciar();
        }

        private void scanTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            IN.reiniciar();
        }

        private void motivo_KeyDown(object sender, KeyEventArgs e)
        {
            IN.reiniciar();
        }
    }
}
