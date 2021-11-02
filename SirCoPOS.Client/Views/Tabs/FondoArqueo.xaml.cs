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
    /// Interaction logic for FondoArqueo.xaml
    /// </summary>
    [Utilities.Extensions.ExportView]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Utilities.Extensions.MetadataTab(Utilities.Constants.TabType.FondoArqueo)]
    public partial class FondoArqueo : UserControl, Utilities.Interfaces.ITabView
    {
        private IDictionary<Guid, TabItem> _tabs;
        ViewModels.Tabs.FondoArqueoViewModel FA;
        private ILogger _log;
        

        public FondoArqueo()
        {
            FA = new ViewModels.Tabs.FondoArqueoViewModel();
            InitializeComponent();
            _tabs = new Dictionary<Guid, TabItem>();
            _log = CommonServiceLocator.ServiceLocator.Current.GetInstance<ILogger>();
            Messenger.Default.Register<string>(this, "FocusAuditor", FocusAuditor);
        }

        public void Init()
        {
            this.txt_importe.IsEnabled = true;
            txt_importe.Text = "0";
            this.txt_importe.Focus();
            //var dato = new SirCoPOS.Common.Entities.Empleado();
            //int depto = dato.Depto;
            //var vm = (ViewModels.Tabs.FondoArqueoViewModel)this.DataContext;
        }



        public void limpiar()
        {
            this.txtB_Contra.Clear();
            this.txtB_Contra.Focus();
            //var vm = (ViewModels.Tabs.FondoArqueoViewModel)this.DataContext;
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

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
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
        public void Seleccionar()
        {
            this.txtB_Contra.Focus();
            this.txtB_Contra.SelectAll();
        }

        private void txtB_Contra_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if(this.pass.Text != "")
                {
                    this.btnguardar.Focus();
                }
                else
                {
                    Seleccionar();
                }
            }
        }

        public void cambiar()
        {
            this.pass.Text = "SI";
        }

        private void textBox_Copy1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if(AuditorName.Text != "")
                {
                    txtB_Contra.Focus();
                }
                else
                {
                    this.tbAudId.Focus();
                    this.tbAudId.SelectAll();
                }
            }
        }

        public void FocusAuditor(string msg)
        {
            if (msg == "focus")
            {
                this.tbAudId.Focus();
                this.tbAudId.SelectAll();
            }

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

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
        }

        private void tbAudId_KeyDown(object sender, KeyEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
        }

        private void txtB_Contra_KeyDown(object sender, KeyEventArgs e)
        {
            Messenger.Default.Send<string>("reiniciar", "reiniciar");
        }
    }
}
