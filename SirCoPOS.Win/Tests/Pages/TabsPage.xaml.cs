using System;
using System.Collections.Generic;
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
using SirCoPOS.Win.Tests.Views;

namespace SirCoPOS.Win.Tests.Pages
{
    /// <summary>
    /// Interaction logic for TabsPage.xaml
    /// </summary>
    public partial class TabsPage : Page
    {
        public TabsPage()
        {
            InitializeComponent();
        }
        int count = 0;
        internal void SetView(UserControl v)
        {
            var ti = new TabItem();
            ti.Header = $"view {++count}";
            ti.Content = v;
            if (v is View1)
            {
                var vv = (View1)v;
                vv.button.Click += (s, e) => {
                    this.tabControl.Items.Remove(ti);
                    if (this.tabControl.Items.Count == 0)
                        this.NavigationService.GoBack();
                };
            }
            if (v is View2)
            {
                var vv = (View2)v;
                vv.button.Click += (s, e) => {
                    this.tabControl.Items.Remove(ti);
                    if (this.tabControl.Items.Count == 0)
                        this.NavigationService.GoBack();
                };
            }
            if (v is View3)
            {
                var vv = (View3)v;
                vv.button.Click += (s, e) => {
                    this.tabControl.Items.Remove(ti);
                    if (this.tabControl.Items.Count == 0)
                        this.NavigationService.GoBack();
                };
            }
            var ind = this.tabControl.Items.Add(ti);
            this.tabControl.SelectedIndex = ind;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            SetView(new Views.View1());
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            SetView(new Views.View2());
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            SetView(new Views.View3());
        }
    }
}
