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
    /// Interaction logic for BonosView.xaml
    /// </summary>
    [Utilities.Extensions.ExportView]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Utilities.Extensions.MetadataTab(Utilities.Constants.TabType.Bonos)]
    public partial class BonosView : UserControl
    {

        private System.Windows.Threading.DispatcherTimer _dt;
        private IDictionary<Guid, TabItem> _tabs;
        private ILogger _log;

        public BonosView()
        {
            InitializeComponent();
            this.Loaded += BonosView_Loaded;
            Messenger.Default.Register<string>(this, "SetFocus", doFocusBtn);
            _tabs = new Dictionary<Guid, TabItem>();
            _dt = new System.Windows.Threading.DispatcherTimer();
            _dt.Tick += Dt_Tick;
            _dt.Interval = TimeSpan.FromSeconds(Common.Constants.Inactividad.Segundos);
            _log = CommonServiceLocator.ServiceLocator.Current.GetInstance<ILogger>();
            this.RegisterMessages();
            _dt.Start();


        }
        public void doFocusBtn(string msg)
        {
            if (msg == "next")
                this.btnPagar.Focus();
        }

        private void BonosView_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtEmp.Focus();
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

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _dt.Stop();
        }

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _dt.Start();
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            _dt.Stop();
        }

        private void UserControl_KeyUp(object sender, KeyEventArgs e)
        {
            _dt.Start();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password; }
        }


        private void Text_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox s = e.Source as TextBox;
                if (s != null)
                {
                    Client.ViewModels.Tabs.BonosViewModel vm = this.DataContext as Client.ViewModels.Tabs.BonosViewModel;
                    if (vm != null && s.Text != "")
                    {

                        switch (s.Name)
                        {
                            case "txtEmp":
                                vm.Empleado = Int32.Parse(s.Text);
                                txtPwd.Password = "";
                                vm.LoadCommand.CanExecute(null);
                                vm.LoadCommand.Execute(null);
                                break;
                            case "txtGte":
                                vm.Gerente = Int32.Parse(s.Text);
                                vm.LoadCommand.CanExecute(null);
                                vm.LoadGerente.Execute(null);
                                break;
                        }
                    }
                    if (s.Text != "")
                        s.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                }
                e.Handled = true;
            }
        }

        private void SelectAll(object sender, RoutedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            if (tb != null)
            {
                tb.SelectAll();
            }
        }
        private void SelectivelyIgnoreMouseButton(object sender,
    MouseButtonEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            if (tb != null)
            {
                if (!tb.IsKeyboardFocusWithin)
                {
                    e.Handled = true;
                    tb.Focus();
                }
            }
        }

    }
}
