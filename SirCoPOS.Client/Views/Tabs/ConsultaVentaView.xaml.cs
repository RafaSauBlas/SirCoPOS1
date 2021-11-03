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
    /// Interaction logic for ConsultaVentaView.xaml
    /// </summary>
    [Utilities.Extensions.ExportView]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Utilities.Extensions.MetadataTab(Utilities.Constants.TabType.ConsultaVenta)]
    public partial class ConsultaVentaView : UserControl
    {
        private IDictionary<Guid, TabItem> _tabs;
        private ILogger _log;

        public ConsultaVentaView()
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

        public void Detener(string msg)
        {
            if (msg == "stop")
            {
                Messenger.Default.Send<string>("detener", "detener");
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Register<string>(this, "Detener", Detener);
            this.txtNoVenta.Focus();
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
        }

        private void txtNoVenta_KeyDown(object sender, KeyEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
        }
        private void txtNoVenta_TextChanged(object sender, TextChangedEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
        }
    }
}
