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
    /// Interaction logic for ConsultaDevolucionView.xaml
    /// </summary>
    [Utilities.Extensions.ExportView]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Utilities.Extensions.MetadataTab(Utilities.Constants.TabType.ConsultaDevolucion)]
    public partial class ConsultaDevolucionView : UserControl
    {
        private IDictionary<Guid, TabItem> _tabs;
        Client.MetodoInactividad IN;
        private ILogger _log;

        public ConsultaDevolucionView()
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
                IN.detener();
            }
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Grid_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Register<string>(this, "Detener", Detener);
            IN = new Client.MetodoInactividad();
            this.txtdevolucion.Focus();
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            IN.reiniciar();
        }

        private void txtdevolucion_TextChanged(object sender, TextChangedEventArgs e)
        {
            IN.reiniciar();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IN.reiniciar();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            IN.reiniciar();
        }
    }
}
