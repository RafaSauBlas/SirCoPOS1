﻿using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Command;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace SirCoPOS.Win.Views
{
    public partial class MainView : UserControl, IDisposable
    {
        private Utilities.Models.Settings _settings;
        private ILogger _log;
        private Helpers.PlugInServiceLocator _plugins;
        public void Dispose()
        {
            Messenger.Default.Unregister<Messages.MenuItem>(this);
            Messenger.Default.Unregister<Utilities.Messages.CloseTab>(this);
            Messenger.Default.Unregister<Utilities.Messages.LogoutTimeout>(this);
            Messenger.Default.Unregister<Utilities.Messages.ShortcutMessage>(this);

            Console.WriteLine("disposed");
        }
        public MainView()
        {
            InitializeComponent();

            _plugins = Utilities.Helpers.Singleton<Helpers.PlugInServiceLocator>.Instance;
            _tabs = new Dictionary<Guid, TabItem>();                        

            if (!GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                _settings = CommonServiceLocator.ServiceLocator.Current.GetInstance<Utilities.Models.Settings>();
                _log = CommonServiceLocator.ServiceLocator.Current.GetInstance<ILogger>();
            }

            var view = CollectionViewSource.GetDefaultView(this.tabControl.Items);
            view.CollectionChanged += (o, e) => 
            {
                this.tabControl.Visibility = this.tabControl.HasItems ? Visibility.Visible : Visibility.Hidden;
            };

            this.RegisterMessages();
        }

        private void RegisterMessages()
        {
            Messenger.Default.Register<Messages.MenuItem>(this,
                    m => {
                        OpenMenu(m);
                        this.menuView.Visibility = Visibility.Hidden;
                    });

            Messenger.Default.Register<Utilities.Messages.CloseTab>(this,
                m => {
                    Messenger.Default.Send(m, m.GID);
                    Console.WriteLine($"removing: {m.GID}");
                    if (!_tabs.ContainsKey(m.GID) && Debugger.IsAttached)
                    {
                        Debugger.Break();
                    }
                    var ti = _tabs[m.GID];
                    _tabs.Remove(m.GID);
                    Console.WriteLine($"remcount: {_tabs.Count}");
                    _log.Debug($"Remove: {m.GID}");
                    this.tabControl.Items.Remove(ti);

                    if (!_tabs.Any())
                    {
                        this.menuView.Visibility = Visibility.Visible;
                    }
                });

            Messenger.Default.Register<Utilities.Messages.LogoutTimeout>(this, m => {
            });
            Messenger.Default.Register<Utilities.Messages.ShortcutMessage>(this, m => {
                if (!_tabs.Any())
                {
                    switch (m.Key)
                    {
                        case Key.F1:
                            
                            this.OpenMenu(new Messages.MenuItem { Name = Utilities.Constants.TabType.Caja });
                            break;
                        case Key.F2:
                            
                            this.OpenMenu(new Messages.MenuItem { Name = Utilities.Constants.TabType.Cambio });
                            break;
                        case Key.F3:
                            
                            this.OpenMenu(new Messages.MenuItem { Name = Utilities.Constants.TabType.Devolucion });
                            break;
                        case Key.F4:
                            
                            this.OpenMenu(new Messages.MenuItem { Name = Utilities.Constants.TabType.Cancelacion });
                            break;
                        case Key.F5:
                            
                            this.OpenMenu(new Messages.MenuItem { Name = Utilities.Constants.TabType.CancelacionDevolucion });
                            break;
                        case Key.F6:
                            
                            this.OpenMenu(new Messages.MenuItem { Name = Utilities.Constants.TabType.VerificarVale });
                            break;
                        case Key.F7:
                           
                            this.OpenMenu(new Messages.MenuItem { Name = Utilities.Constants.TabType.Efectivo });
                            break;
                        case Key.F8:
                         
                            this.OpenMenu(new Messages.MenuItem { Name = Utilities.Constants.TabType.Corte });
                            break;
                        case Key.F9:

                            this.OpenMenu(new Messages.MenuItem { Name = Utilities.Constants.TabType.FondoArqueo });
                            break;
                    }
                }
                else
                {
                    var uc = (UserControl)this.tabControl.SelectedContent;
                    var tab = (Utilities.Interfaces.ITabViewModel)uc.DataContext;
                    Messenger.Default.Send(m, tab.GID);
                }
            });
        }

        public void Detener(string msg)
        {
            if (msg == "stop")
            {

            }
        }

        private void OpenMenu(Messages.MenuItem mi, Guid? gid = null)
        {
            if (!_settings.MultiCaja)
            {
                var q = _tabs.Where(i => ((Utilities.Interfaces.ITabViewModel)((UserControl)i.Value.Content).DataContext).TabType == mi.Name);
                if (q.Any())
                    return;
            }            



            UserControl uc = null;
            switch (mi.Name)
            {                                
                case Utilities.Constants.TabType.CreditoPersonal:
                case Utilities.Constants.TabType.DineroElectronico:                    
                case Utilities.Constants.TabType.Administracion:
                    throw new NotSupportedException();
                default:
                    uc = _plugins.GetView(mi.Name);
                    break;
            }

            OpenTab(mi, uc, gid);
        }

        private IDictionary<Guid, TabItem> _tabs;
        private void OpenTab(Messages.MenuItem mi, UserControl uc, Guid? id = null)
        {
            var gid = id ?? Guid.NewGuid();
            var tabv = uc as Utilities.Interfaces.ITabView;
            var tabvm = (Utilities.Interfaces.ITabViewModel)uc.DataContext;
            tabvm.TabType = mi.Name;
            var header = new Views.TabHeaderView();
            var vm = (ViewModels.TabHeaderViewModel)header.DataContext;
            vm.Title = $"{mi.Name}";
            if (vm.Title == "FondoArqueo")
                vm.Title = "Arqueo";
            if (vm.Title == "Corte")
                vm.Title = "Entrega Caja";
            if (vm.Title == "Efectivo")
                vm.Title = "Entrega Efectivo";
            vm.GID = gid;
            var ti = new TabItem { Header = header, Content = uc };
            Console.WriteLine($"added: {gid}");
            _tabs.Add(gid, ti);
            Console.WriteLine($"count: {_tabs.Count}");
            ti.Tag = gid;
            ti.Loaded += (s, e) => {
                var sti = (TabItem)s;
                var gi = (Guid)sti.Tag;
                Console.WriteLine($"init: {gi}");
                tabvm.Init(gi);
                tabv?.Init();
            };            
            var ind = this.tabControl.Items.Add(ti);
            this.tabControl.SelectedIndex = ind;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Register<string>(this, "Detener", Detener);

            if (GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
                return;

            var ctx = new DataAccess.DataContext();
            var q = ctx.Ventas.Where(i => i.CajeroId == _settings.Cajero.Id);
            foreach (var item in q)
            {
                this.OpenMenu(new Messages.MenuItem { Name = Utilities.Constants.TabType.Caja }, item.Id);
            }
        }

        private void Set(string v, ref string title, string value)
        {
            throw new NotImplementedException();
        }

        public RelayCommand CloseCommand { get; private set; }


        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menuView_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void menuView_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

        private void menuView_Loaded_2(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {

        }
    }
}
