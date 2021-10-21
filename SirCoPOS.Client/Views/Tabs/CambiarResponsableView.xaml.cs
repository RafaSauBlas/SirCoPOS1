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
    /// Interaction logic for CambiarResponsableView.xaml
    /// </summary>
    [Utilities.Extensions.ExportView]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Utilities.Extensions.MetadataTab(Utilities.Constants.TabType.CambiarResponsable)]
    public partial class CambiarResponsableView : UserControl
    {

        private IDictionary<Guid, TabItem> _tabs;
        Client.MetodoInactividad IN;
        private ILogger _log;

        public CambiarResponsableView()
        {
            InitializeComponent();
            this.txtEntrega.Focus();
            _tabs = new Dictionary<Guid, TabItem>();
            _log = CommonServiceLocator.ServiceLocator.Current.GetInstance<ILogger>();
            Messenger.Default.Register<string>(this, "FocusResponsable", FocusResponsable);
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

        public void seleccionar()
        {
            this.txtB_Contra.Focus();
            this.txtB_Contra.SelectAll();
        }

        private void txtB_Contra_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                seleccionar();
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
            this.txtEntrega.Focus();
        }

        public void FocusResponsable(string msg)
        {
            if (msg == "focus")
            {
                this.tbResponsableId.Focus();
                this.tbResponsableId.SelectAll();
            }

        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            IN.reiniciar();
        }

        private void txtEntrega_TextChanged(object sender, TextChangedEventArgs e)
        {
            IN.reiniciar();
        }

        private void tbResponsableId_TextChanged(object sender, TextChangedEventArgs e)
        {
            IN.reiniciar();
        }

        private void txtB_Contra_KeyDown(object sender, KeyEventArgs e)
        {
            IN.reiniciar();
        }
    }
}
