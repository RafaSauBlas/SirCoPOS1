﻿using System;
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

namespace SirCoPOS.Client.Views.Tabs
{
    /// <summary>
    /// Interaction logic for CambioView1.xaml
    /// </summary>
    [Utilities.Extensions.ExportView]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Utilities.Extensions.MetadataTab(Utilities.Constants.TabType.Cambio)]
    public partial class CambioView1 : UserControl, Utilities.Interfaces.ITabView
    {
        public CambioView1()
        {
            InitializeComponent();
        }

        public void Init()
        {

            
            var depto = (int)Common.Constants.Departamento.TDA;
            if (depto <= 2)
            {
                this.scanTextBox.IsEnabled = true;
                this.scanTextBox.Focus();
            }
            var vm = (ViewModels.Tabs.CambioViewModel)this.DataContext;


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var viewModel = (ViewModels.Tabs.CambioViewModel)DataContext;
            if (viewModel.PrintCommand.CanExecute(null))
                viewModel.PrintCommand.Execute(null);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (ViewModels.Tabs.CambioViewModel)DataContext;
            if (viewModel.PrintCommand.CanExecute(null))
                viewModel.PrintCommand.Execute(null);
        }

        private void scanTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var vm = (ViewModels.Tabs.CambioViewModel)this.DataContext;
            if (vm.Cajero.Depto == 3)
            {
                System.Windows.Clipboard.Clear();
            }
        }
    }
}
