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

namespace SirCoPOS.Win.Tests.Pages
{
    /// <summary>
    /// Interaction logic for MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
            _tp = new TabsPage();
        }
        TabsPage _tp;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            var v = new Views.View1();
            _tp.SetView(v);
            this.NavigationService.Navigate(_tp);
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            var v = new Views.View2();
            _tp.SetView(v);
            this.NavigationService.Navigate(_tp);
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            var v = new Views.View3();
            _tp.SetView(v);
            this.NavigationService.Navigate(_tp);
        }
    }
}
