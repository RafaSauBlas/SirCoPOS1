using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using GalaSoft.MvvmLight.Messaging;
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
using NLog;

namespace SirCoPOS.Client.Views.Tabs
{
    /// <summary>
    /// Interaction logic for CorteView.xaml
    /// </summary>
    [Utilities.Extensions.ExportView]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Utilities.Extensions.MetadataTab(Utilities.Constants.TabType.Corte)]
    public partial class CorteView : UserControl, Utilities.Interfaces.ITabView
    {
        private IDictionary<Guid, TabItem> _tabs;
        Client.MetodoInactividad IN;
        private ILogger _log;

        public CorteView()
        {
            _tabs = new Dictionary<Guid, TabItem>();
            _log = CommonServiceLocator.ServiceLocator.Current.GetInstance<ILogger>();
            InitializeComponent();
        }

        public void space()
        {

        }
        public void Init()
        {
            this.tbEntregar.Focus();
            var vm = (ViewModels.Tabs.CajaViewModel)this.DataContext;
            if (vm.Cajero.Depto == 3)
                this.scanSerie.ContextMenu = new ContextMenu();
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            var dt = (System.Windows.Threading.DispatcherTimer)sender;
            dt.Stop();
            Messenger.Default.Send(new Utilities.Messages.LogoutTimeout());
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password; }
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void Grid_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
          
        }

        public void seleccionar()
        {
            this.txtB_Contra.Focus();
            this.txtB_Contra.SelectAll();
        }

        private void txtB_Contra_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if(this.usuarioid.Text != "")
                {
                    this.button.Focus();
                }
                else
                {
                    seleccionar();
                }
            }
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if (this.auditorname.Text != "")
                {
                    this.txtB_Contra.Focus();
                }
                else
                {
                    this.txtidaudit.Focus();
                    this.txtidaudit.SelectAll();
                }
            }
        }

        public void Detener(string msg)
        {
            if (msg == "stop")
            {
                IN.detener();
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

        private void tbEntregar_KeyDown(object sender, KeyEventArgs e)
        {
            IN.reiniciar();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            IN.reiniciar();
        }

        private void TextBox_KeyDown_1(object sender, KeyEventArgs e)
        {
            IN.reiniciar();
        }

        private void txtidaudit_KeyDown(object sender, KeyEventArgs e)
        {
            IN.reiniciar();
        }

        private void txtB_Contra_KeyDown(object sender, KeyEventArgs e)
        {
            IN.reiniciar();
        }
    }
}
