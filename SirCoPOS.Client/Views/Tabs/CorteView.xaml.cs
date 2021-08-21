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
        private System.Windows.Threading.DispatcherTimer _dt;
        private IDictionary<Guid, TabItem> _tabs;
        private ILogger _log;

        public CorteView()
        {
            _tabs = new Dictionary<Guid, TabItem>();
            _dt = new System.Windows.Threading.DispatcherTimer();
            _dt.Tick += Dt_Tick;
            _dt.Interval = TimeSpan.FromSeconds(Common.Constants.Inactividad.Segundos);
            _log = CommonServiceLocator.ServiceLocator.Current.GetInstance<ILogger>();
            InitializeComponent();
            this.RegisterMessages();
            _dt.Start();
        }

        private void RegisterMessages()
        {

            Messenger.Default.Register<Utilities.Messages.CloseTab>(this,
               m => {
                   Messenger.Default.Send(m, m.GID);
                   Console.WriteLine($"removing: {m.GID}");
                   if (!_tabs.Any())
                   {
                       _dt.Stop();
                   }
               });

            Messenger.Default.Register<Utilities.Messages.LogoutTimeout>(this, m => {
                _dt.Stop();
            });

        }

        public void space()
        {

        }
        public void Init()
        {
            this.tbEntregar.Focus();
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
            _dt.Stop();
        }

        private void Grid_KeyUp(object sender, KeyEventArgs e)
        {
            _dt.Start();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _dt.Stop();
        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _dt.Start();
        }
    }
}
