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
    /// Interaction logic for VerificarValeExternoView.xaml
    /// </summary>
    [Utilities.Extensions.ExportView]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Utilities.Extensions.MetadataTab(Utilities.Constants.TabType.VerificarValeExterno)]
    public partial class VerificarValeExternoView : UserControl, Utilities.Interfaces.ITabView
    {

        private System.Windows.Threading.DispatcherTimer _dt;
        private IDictionary<Guid, TabItem> _tabs;
        private ILogger _log;

        public VerificarValeExternoView()
        {
            InitializeComponent();

            _tabs = new Dictionary<Guid, TabItem>();
            _dt = new System.Windows.Threading.DispatcherTimer();
            _dt.Tick += Dt_Tick;
            _dt.Interval = TimeSpan.FromSeconds(Common.Constants.Inactividad.Segundos);
            _log = CommonServiceLocator.ServiceLocator.Current.GetInstance<ILogger>();
            this.RegisterMessages();
            _dt.Start();
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            var dt = (System.Windows.Threading.DispatcherTimer)sender;
            dt.Stop();
            Messenger.Default.Send(new Utilities.Messages.LogoutTimeout());
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

        public void Init()
        {
            this.tbVale.Focus();
        }

        private void TabControl_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            var tc = (TabControl)sender;
            var tabs = tc.Items;
            if (tc.Items.Count > 0)
                tc.SelectedIndex = 0;
        }

        private void PreviewTextInputOnlyLetters(object sender, TextCompositionEventArgs e)
        {
            int character = Convert.ToInt32(Convert.ToChar(e.Text));
            if ((character >= 65 && character <= 90) || (character >= 97 && character <= 122))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void PreviewTextInputOnlyNumbers(object sender, TextCompositionEventArgs e)

        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));

            if (ascci >= 48 && ascci <= 57) e.Handled = false;

            else e.Handled = true;

        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            _dt.Stop();
        }

        private void UserControl_KeyUp(object sender, KeyEventArgs e)
        {
            _dt.Start();
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _dt.Stop();
        }

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _dt.Start();
        }
    }
}
