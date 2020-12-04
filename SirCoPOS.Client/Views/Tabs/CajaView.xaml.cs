using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SirCoPOS.Client.Views.Tabs
{
    /// <summary>
    /// Interaction logic for CajaView.xaml
    /// </summary>
    public partial class CajaView : UserControl, Utilities.Interfaces.ITabView
    {
        public CajaView()
        {
            InitializeComponent();
         
            _screen = System.Windows.Forms.Screen.AllScreens.Where(i => !i.Primary).FirstOrDefault();
            this.dualButton.IsEnabled = _screen != null;

            //this.test.UpdateLayout();
            //this.test.Show();
        }

        private System.Windows.Forms.Screen _screen;

        private void DualButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new Windows.DualWindow();
            var view = new Views.CajaDualView();
            view.DataContext = this.DataContext;
            win.Content = view;
            win.Top = _screen.Bounds.Top;
            win.Left = _screen.Bounds.Left;
            win.Loaded += (se, ev) =>
            {
                win.WindowState = WindowState.Maximized;
            };
            win.Show();

            //this.test.UpdateLayout();
            //this.test.Show();
        }

        public void Init()
        {
            this.scanTextBox.Focus();
            var vm = (ViewModels.Tabs.CajaViewModel)this.DataContext;
            vm.Pagos.CollectionChanged += Pagos_CollectionChanged;
        }

        private void Pagos_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var vm = (ViewModels.Tabs.CajaViewModel)this.DataContext;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        if (vm.Pagos.Count == 1)
                        {
                            var sb = (Storyboard)this.FindResource("Storyboard1");
                            sb.Begin();
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    {
                        if (vm.Pagos.Count == 0)
                        {
                            var sb = (Storyboard)this.FindResource("Storyboard2");
                            sb.Begin();
                        }
                    }
                    break;
            }
        }

        //private void Vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
            
            
        //    switch (e.PropertyName)
        //    {
        //        case nameof(ViewModels.Tabs.CajaViewModel.HasPagos):
        //            {
        //                if (vm.HasPagos && vm.Pagos.Count == 1)
        //                {
                            
        //                }
        //                else if(!vm.HasPagos)
        //                {
                            
        //                }
        //            }
        //            break;
        //    }
        //}
    }
}
