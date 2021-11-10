using System;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
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
using System.IO;
using System.Windows.Shapes;
using NLog;
using System.Diagnostics;
using System.Runtime.InteropServices;



namespace SirCoPOS.Client.Views.Tabs
{
    /// <summary>
    /// Interaction logic for CajaView4.xaml
    /// </summary>
    [Utilities.Extensions.ExportView]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    //[ExportMetadata("View", Utilities.Constants.TabType.Caja)]
    [Utilities.Extensions.MetadataTab(Utilities.Constants.TabType.Caja)]
    public partial class CajaView4 : UserControl, Utilities.Interfaces.ITabView
    {
        public string FTP = "http://201.148.82.174/FOTOS/";
        public string IPP = @"\\10.10.1.1\Sistema\ZT\Fotos\";
        private IDictionary<Guid, TabItem> _tabs;
        private ILogger _log;

        public CajaView4()
        {
            InitializeComponent();
            SirCoPOS.Common.Constants.ProductoDatos.opcion = false;
            _tabs = new Dictionary<Guid, TabItem>();
            Messenger.Default.Register<string>(this, "DoFocus", doFocus);
            _log = CommonServiceLocator.ServiceLocator.Current.GetInstance<ILogger>();
            this.RegisterMessages();
        }

        private void RegisterMessages()
        {

            Messenger.Default.Register<Utilities.Messages.CloseTab>(this,
               m => {
                   Messenger.Default.Send(m, m.GID);
                   Console.WriteLine($"removing: {m.GID}");
                   if (!_tabs.Any())
                   {
                       Messenger.Default.Send<string>("detener", "detener");
                   }
               });

            Messenger.Default.Register<Utilities.Messages.OpenModal>(this,
                m => {
                    Messenger.Default.Send(m, m.GID);
                    Console.WriteLine($"removing: {m.GID}");
                    if (!_tabs.Any())
                    {
                        Messenger.Default.Send<string>("detener", "detener");
                    }
                });

            Messenger.Default.Register<Utilities.Messages.LogoutTimeout>(this, m => {
                Messenger.Default.Send<string>("detener", "detener");
            });

            Messenger.Default.Register<Utilities.Messages.OpenModal>(this,
               m => {
                   Messenger.Default.Send(m, m.GID);
                   Console.WriteLine($"open: {m.GID}");
                   Messenger.Default.Send<string>("detener", "detener");
               });

            Messenger.Default.Register<Utilities.Messages.OpenModalItem>(this,
                m =>
                {
                    Messenger.Default.Send(m, m.GID);
                    Console.WriteLine($"open: {m.GID}");

                });
        }

        public void doFocus(string msg)
        {
            if (msg == "focus")
                this.scanTextBox.Focus();
        }
        public void Init()
        {
            this.scanTextBox.IsEnabled = true;
            this.scanTextBox.Focus();
            //var dato = new SirCoPOS.Common.Entities.Empleado();
            //int depto = dato.Depto;
            var vm = (ViewModels.Tabs.CajaViewModel)this.DataContext;
            if (vm.Cajero.Depto == 3)
                this.scanTextBox.ContextMenu = new ContextMenu();
        }

        private void scanTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
         {
            //if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 |)
            //    e.Handled = false;
            //else
            //    e.Handled = true;


            var vm = (ViewModels.Tabs.CajaViewModel)this.DataContext;
            if (vm.Cajero.Depto == 3)
            {
                System.Windows.Clipboard.Clear();
            }
            if(e.Key == Key.Enter)
            {

            }
        }

        private void DualButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new Windows.DualWindow();
            var view = new Views.CajaDualView();
            view.DataContext = this.DataContext;
            win.Content = view;
            //win.Top = _screen.Bounds.Top;
            //win.Left = _screen.Bounds.Left;
            win.Loaded += (se, ev) =>
            {
                //win.WindowState = WindowState.Maximized;
            };
            win.Show();

            //this.test.UpdateLayout();
            //this.test.Show();
        }

        public void Detener(string msg)
        {
            if (msg == "stop")
            {
                Messenger.Default.Send<string>("detener", "detener");
            }
        }

        public void Reiniciar(string msg)
        {
            if (msg == "rest")
            {
                Messenger.Default.Send<string>("detener", "detener");
            }
        }


        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Common.Constants.ClienteDato.opcion = 0;
            var Marca = "";
            var Modelo = "";
            var Modelo2 = "";

            if(dgView.SelectedItem == null)
            {
                PB.Source = null;
            }
            else
            {
                Marca = ((Client.Models.Producto)dgView.SelectedItem).Marca;
                Modelo = ((Client.Models.Producto)dgView.SelectedItem).Modelo;
                Modelo2 = Modelo.Replace(" ", "_");

                var imagen = new BitmapImage(new Uri(FTP + Marca + Modelo2 + "F3.png"));

                if (imagen.CanFreeze == false)
                {
                    if (!File.Exists(IPP + Marca + Modelo2 + "F1.jpg"))
                    {
                        imagen = null;
                    }
                    else
                    {
                        imagen = new BitmapImage(new Uri(IPP + Marca + Modelo2 + "F1.jpg"));
                    }
                }
                PB.Source = imagen;

                PB.Stretch = Stretch.Fill;
            }
        }

        private void ListBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                Common.Constants.ClienteDato.opcion = 0;
            }
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            //if (Keyboard.Modifiers == ModifierKeys.Control)
            //{
            //    switch (e.Key)
            //    {
            //        case Key.F: //Efectivo
            //        case Key.R: //Tarjeta de crédito
            //        case Key.T: //Tardeja de debito
            //        case Key.D: //Devolución
            //        case Key.M: //Monedero electrónico
            //        case Key.G: //GoPlazos
            //        case Key.K: //KueskiPay
            //        case Key.V: //Vale
            //        case Key.C: //Contra vale
            //        case Key.P: //Credito personal
            //        case Key.O: //Credito distribuidor
            //        case Key.I: //Vale digital
            //        case Key.E: //vale externo
            //            Messenger.Default.Send(new Utilities.Messages.ShortcutMessage { Key = e.Key });
            //            break;
            //    }
            //}

            if (e.Key == Key.F5)
            {
                SirCoPOS.Common.Constants.ProductoDatos.opcion = true;
            }
            Messenger.Default.Send<string>("detener", "detener");
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
        }

        

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
        }

        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
        }


        private void dgView_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        public void seleccionar()
        {
            this.scanTextBox.Focus();
            this.scanTextBox.SelectAll();
        }

        private void scanTextBox_KeyUp(object sender, KeyEventArgs e)
        {
                if (e.Key == Key.Enter)
                {
                    seleccionar();
                }            
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbox.Items.Count == 0)
            {
                lblError.Text = "";
            }
            else
            {
                foreach (var co in lbox.Items)
                {
                    if (co.ToString() == "SirCoPOS.Client.Models.Pagos.PagoVale" && lblcli2.Text == "")
                    {
                        if(Common.Constants.ClienteDato.error == "Cliente Requerido")
                        {
                            lblError.Text = Common.Constants.ClienteDato.error;
                        }
                    }
                    else if (co.ToString() == "SirCoPOS.Client.Models.Pagos.PagoVale" && lblcli2.Text != "")
                    {

                    }
                    else if (co.ToString() != "SirCoPOS.Client.Models.Pagos.PagoVale")
                    {
                        lblError.Text = "";
                    }
                    else if(co.ToString() == "SirCoPOS.Client.Models.Pagos.Credito")
                    {
                        Common.Constants.ClienteDato.opcion = 0;
                    }
                    else
                    {
                        lblError.Text = "";
                    }
                }
            }

            var ddo = lblcliente.Text;
            var ddi = lblcli2.Text;
        }

        private void lblcliente_TextInput(object sender, TextCompositionEventArgs e)
        {
                lblError.Text = "";
        }

        private void lblcli2_SizeChanged(object sender, SizeChangedEventArgs e)
        {
             if(lblcli2.Text != "")
            {
                lblError.Text = "";
            }
            else if (lblcli2.Text == "" && lbox.Items.Count > 0)
            {
                if (Common.Constants.ClienteDato.error == "Cliente Requerido")
                {
                    lblError.Text = Common.Constants.ClienteDato.error;
                }
            }
        }

        private void lblcliente_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(lblcli2.Text != "" && lbox.Items.Count > 0)
            {
                lblError.Text = "";
            }
        }

        private void scanTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if(lbox.Items.Count < 1)
            {
                lblError.Text = "";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SirCoPOS.Common.Constants.ProductoDatos.opcion = false;
            Messenger.Default.Send<string>("detener", "detener");
            if (lbox.Items.Count == 0)
            {
                Common.Constants.ClienteDato.opcion = 0;
            }
            else
            {
                foreach (var co in lbox.Items)
                {
                    if (co.ToString() == "SirCoPOS.Client.Models.Pagos.PagoCredito")
                    {
                        Common.Constants.ClienteDato.opcion = 1;
                    }
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Register<string>(this, "Detener", Detener);
            Messenger.Default.Register<string>(this, "Reiniciar", Reiniciar);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Send<string>("detener", "detener");
        }

        private void scanTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            SirCoPOS.Common.Constants.ProductoDatos.opcion = false;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            SirCoPOS.Common.Constants.ProductoDatos.opcion = false;
            var Marca = "";
            var Modelo = "";
            var Modelo2 = "";

            Marca = ((Client.Models.Producto)dgView.SelectedItem).Marca;
            Modelo = ((Client.Models.Producto)dgView.SelectedItem).Modelo;
            Modelo2 = Modelo.Replace(" ", "_");

            Common.Constants.ProductoDatos.marca = Marca;
            Common.Constants.ProductoDatos.modelo2 = Modelo2;
        }

        private void dgView_KeyDown_1(object sender, KeyEventArgs e)
        {
            //if(e.Key == Key.F5)
            //{
            //    SirCoPOS.Common.Constants.ProductoDatos.opcion = true;
            //}
        }

        private void Grid_KeyDown_1(object sender, KeyEventArgs e)
        {
           
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            SirCoPOS.Common.Constants.ProductoDatos.opcion = false;
        }

        private void UserControl_KeyUp(object sender, KeyEventArgs e)
        {
            if(Keyboard.Modifiers == ModifierKeys.Control)
            {
                switch (e.Key)
                {
                    case Key.F: //Efectivo
                    case Key.R: //Tarjeta de crédito
                    case Key.T: //Tardeja de debito
                    case Key.D: //Devolución
                    case Key.M: //Monedero electrónico
                    case Key.G: //GoPlazos
                    case Key.K: //KueskiPay
                    case Key.V: //Vale
                    case Key.C: //Contra vale
                    case Key.P: //Credito personal
                    case Key.O: //Credito distribuidor
                    case Key.I: //Vale digital
                    case Key.E: //vale externo
                        Messenger.Default.Send(new Utilities.Messages.ShortcutMessage { Key = e.Key });
                        this.scanTextBox.Text = "";
                    break;
                }
            }
        }

        private void Grid_KeyDown_2(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                switch (e.Key)
                {
                    case Key.F: //Efectivo
                    case Key.R: //Tarjeta de crédito
                    case Key.T: //Tardeja de debito
                    case Key.D: //Devolución
                    case Key.M: //Monedero electrónico
                    case Key.G: //GoPlazos
                    case Key.K: //KueskiPay
                    case Key.V: //Vale
                    case Key.C: //Contra vale
                    case Key.P: //Credito personal
                    case Key.O: //Credito distribuidor
                    case Key.I: //Vale digital
                    case Key.E: //vale externo
                        Messenger.Default.Send(new Utilities.Messages.ShortcutMessage { Key = e.Key });
                        break;
                }
            }
        }

        private void Grid_KeyUp(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                switch (e.Key)
                {
                    case Key.F: //Efectivo
                    case Key.R: //Tarjeta de crédito
                    case Key.T: //Tardeja de debito
                    case Key.D: //Devolución
                    case Key.M: //Monedero electrónico
                    case Key.G: //GoPlazos
                    case Key.K: //KueskiPay
                    case Key.V: //Vale
                    case Key.C: //Contra vale
                    case Key.P: //Credito personal
                    case Key.O: //Credito distribuidor
                    case Key.I: //Vale digital
                    case Key.E: //vale externo
                        Messenger.Default.Send(new Utilities.Messages.ShortcutMessage { Key = e.Key });
                        break;
                }
            }
        }

        private void dgView_KeyUp(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                switch (e.Key)
                {
                    case Key.F: //Efectivo
                    case Key.R: //Tarjeta de crédito
                    case Key.T: //Tardeja de debito
                    case Key.D: //Devolución
                    case Key.M: //Monedero electrónico
                    case Key.G: //GoPlazos
                    case Key.K: //KueskiPay
                    case Key.V: //Vale
                    case Key.C: //Contra vale
                    case Key.P: //Credito personal
                    case Key.O: //Credito distribuidor
                    case Key.I: //Vale digital
                    case Key.E: //vale externo
                        Messenger.Default.Send(new Utilities.Messages.ShortcutMessage { Key = e.Key });
                    break;
                }
            }
        }

        private void scanTextBox_PreviewTextInput_2(object sender, TextCompositionEventArgs e)
        {
            int character = Convert.ToInt32(Convert.ToChar(e.Text));
            if (character >= 48 && character <= 57)
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}
