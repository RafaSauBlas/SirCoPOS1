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
    /// Interaction logic for DevolucionView.xaml
    /// </summary>
    [Utilities.Extensions.ExportView]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Utilities.Extensions.MetadataTab(Utilities.Constants.TabType.Devolucion)]
    public partial class DevolucionView : UserControl, Utilities.Interfaces.ITabView
    {
        public DevolucionView()
        {
            InitializeComponent();
        }

        public void Init()
        {
            this.searchTextBox.Focus();
        }
    }
}
