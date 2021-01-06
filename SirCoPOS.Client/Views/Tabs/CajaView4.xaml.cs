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
using System.Windows.Shapes;
using System.Collections.Generic;


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
        public CajaView4()
        {
            InitializeComponent();
        }

        public void Init()
        {
            var dato = new SirCoPOS.Common.Entities.Empleado();
            int depto = dato.Depto;

            if ( depto <= 2)
            {
                this.scanTextBox.IsEnabled = true;
                this.scanTextBox.Focus();
            }
            var vm = (ViewModels.Tabs.CajaViewModel)this.DataContext;
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
    }
}
