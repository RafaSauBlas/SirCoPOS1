using GalaSoft.MvvmLight.Messaging;
using SirCoPOS.Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Deployment.Application;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SirCoPOS.Win
{
    /// <summary>
    /// Interaction logic for ShellWindow.xaml
    /// </summary>
    public partial class ShellWindow : Window
    {
        public int opcion = 0;
        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32.dll")]
        private static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);
        private const uint MF_BYCOMMAND = 0x00000000;
        private const uint MF_GRAYED = 0x00000001;
        private const uint SC_CLOSE = 0xF060;
        private const int WM_SHOWWINDOW = 0x00000018;
        public ShellWindow()
        {
            InitializeComponent();

            var b = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
            this.Top = b.Top;
            this.Left = b.Left;
            if (ApplicationDeployment.IsNetworkDeployed)
                this.WindowState = WindowState.Maximized;

        Messenger.Default.Register<Utilities.Messages.LogoutTimeout>(this, m => {
                if (this.frame.NavigationService.CanGoBack)
                {
                    if (this.frame.Content is Pages.MenuPage)
                    {
                        var mp = this.frame.Content as Pages.MenuPage;
                        var mv = (Views.MainView)mp.Content;
                        mp.Content = null;
                        mv.Dispose();
                    }

                    this.frame.NavigationService.GoBack();
                }
            });

            Messenger.Default.Register<Messages.CloseMenu>(this, m => {
                while (this.frame.NavigationService.CanGoBack)
                {
                    this.frame.NavigationService.GoBack();
                }
            });            

            Messenger.Default.Register<Utilities.Messages.MessageBox>(this, m => {
                MessageBox.Show(owner: this, messageBoxText: m.Text, caption: "Error", 
                    button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
            });

            Messenger.Default.Register<Messages.RequestApproval>(this, ra => {
                var win = new Windows.ApproveWindow();
                win.Owner = this;
                var res = win.ShowDialog();
                if (res ?? false)
                {
                    Messenger.Default.Send(new Utilities.Messages.ApprovedResponse { }, ra.GID);
                }
            });

            //ESTA ES LA FUNCION QUE HACE EL CAMBIO DE VENTANA (CARGAR CLIENTE)
            Messenger.Default.Register<Utilities.Messages.OpenModal>(this, true, m =>
            {
                var win = new Windows.ModalWindow(m.GID);
                win.Owner = this;
                UserControl uc = null;
                UserControl au = null;
                var plugins = Utilities.Helpers.Singleton<Helpers.PlugInServiceLocator>.Instance;
                switch (m.Name)
                {
                    default:
                        uc = plugins.GetModalView(m.Name);

                        if (m.opcion == 1)
                        {
                            au = new SirCoPOS.Client.Views.Caja.LoadClienteInfo();
                        }
                        else
                        {
                            au = uc;
                        }
                        break;
                }
                var vm = (Utilities.Interfaces.IModal)uc.DataContext;
                vm.GID = m.GID;
                win.DataContext = au.DataContext;
                win.mainContent.Content = au;
                win.Title = vm.Title;

                if (m is Utilities.Messages.OpenModalItem)
                {
                    var mi = (Utilities.Messages.OpenModalItem)m;
                    //var vmi = (Utilities.Interfaces.IModalItem<Utilities.Interfaces.IProducto>)vm;
                    //var vmi = vm as Utilities.Interfaces.IModalItem<Utilities.Interfaces.IProducto>;
                    var vmi = vm as Utilities.Interfaces.IModalItem;
                    vmi.Item = mi.Item;
                }
                if (m is Utilities.Messages.OpenModalDevolucionItem)
                {
                    var mi = (Utilities.Messages.OpenModalDevolucionItem)m;
                    var vmi = (Utilities.Interfaces.IModalDevolucionItem)vm;
                    vmi.Item = mi.Item;
                }
                

                var res = win.ShowDialog();
  
                if (!(res ?? false) && vm.CloseTab && m.Close)
                {
                    Messenger.Default.Send(new Utilities.Messages.CloseTab { GID = vm.GID });
                }
            });

            Messenger.Default.Register<Utilities.Messages.OpenPago>(this, p => {
                var win = new Windows.ModalWindow(p.GID);
                win.Owner = this;
                UserControl uc = null;
                var plugins = Utilities.Helpers.Singleton<Helpers.PlugInServiceLocator>.Instance;
                switch (p.FormaPago)
                {
                    default:
                        uc = plugins.GetPagoView(p.FormaPago);
                        break;
                }
                var vm = (Utilities.Interfaces.IPago)uc.DataContext;
                win.DataContext = uc.DataContext;
                vm.GID = p.GID;
                vm.Caja = p.Caja;
                vm.ClientId = p.ClientId;
                vm.HasPromocionPlazos = p.HasPromocionPlazo;
                if (p.ProductosPlazos != null)
                {
                    foreach (var item in p.ProductosPlazos)
                    {
                        vm.Productos.Add(new Utilities.Models.ProductoPlazoOpciones(item));
                    }
                }
                //vm.Total = p.Total;
                //vm.Pagar = p.Total;
                if (uc is Utilities.Interfaces.ITabView)
                {
                    var twin = (Utilities.Interfaces.ITabView)uc;
                    vm.Ready.Then(() => twin.Init());
                }
                win.mainContent.Content = uc;
                win.Title = vm.Title;
                win.ShowDialog();
            });
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = true;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var hWnd = new WindowInteropHelper(this);
            var sysMenu = GetSystemMenu(hWnd.Handle, false);
            EnableMenuItem(sysMenu, SC_CLOSE, MF_BYCOMMAND | MF_GRAYED);
        }
    }
}
