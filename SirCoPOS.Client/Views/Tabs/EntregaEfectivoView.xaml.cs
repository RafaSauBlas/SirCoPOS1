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
    /// Interaction logic for EntregaEfectivoView.xaml
    /// </summary>
    [Utilities.Extensions.ExportView]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Utilities.Extensions.MetadataTab(Utilities.Constants.TabType.Efectivo)]
    public partial class EntregaEfectivoView : UserControl, Utilities.Interfaces.ITabView
    {
        private IDictionary<Guid, TabItem> _tabs;
        private ILogger _log;

        public EntregaEfectivoView()
        {
            InitializeComponent();
            _tabs = new Dictionary<Guid, TabItem>();
            _log = CommonServiceLocator.ServiceLocator.Current.GetInstance<ILogger>();
            Messenger.Default.Register<string>(this, "FocusRecibir", FocusRecibir);
            Messenger.Default.Register<string>(this, "FocusBoton", FocusBoton);
        }

        public void FocusBoton(string msg)
        {
            if (msg == "focusbtn")
            {
                this.btn_aceptar.Focus();
            }
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            var dt = (System.Windows.Threading.DispatcherTimer)sender;
            dt.Stop();
            Messenger.Default.Send(new Utilities.Messages.LogoutTimeout());
        }

        public void FocusRecibir(string msg)
        {
            if (msg == "focus")
            {
                this.tbRecibe.Focus();
                this.tbRecibe.SelectAll();
            }

        }

        public void Init()
        {
            tbEntrega.Text = "0";
            this.tbEntrega.Focus();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password; }
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
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
        }

        private void tbEntrega_KeyDown(object sender, KeyEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
        }

        private void tbRecibe_KeyDown(object sender, KeyEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
        }

        private void txtB_Contra_KeyDown(object sender, KeyEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
        }

        private void DataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
        }

        private void DataGrid_MouseMove(object sender, MouseEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
        }

        private void DataGrid_KeyUp(object sender, KeyEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
        }
    }
}
