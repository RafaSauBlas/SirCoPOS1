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

        private System.Windows.Threading.DispatcherTimer _dt;
        private IDictionary<Guid, TabItem> _tabs;
        private ILogger _log;
        ViewModels.Tabs.FondoArqueoViewModel FA;
        

        public FondoArqueo()
        {
            FA = new ViewModels.Tabs.FondoArqueoViewModel();
            InitializeComponent();
            _tabs = new Dictionary<Guid, TabItem>();
            _dt = new System.Windows.Threading.DispatcherTimer();
            _dt.Tick += Dt_Tick;
            _dt.Interval = TimeSpan.FromSeconds(Common.Constants.Inactividad.Segundos);
            _log = CommonServiceLocator.ServiceLocator.Current.GetInstance<ILogger>();
            this.RegisterMessages();
            _dt.Start();
        }

        public void Init()
        {
            this.textBox.IsEnabled = true;
            this.textBox.Focus();
            //var dato = new SirCoPOS.Common.Entities.Empleado();
            //int depto = dato.Depto;
            var vm = (ViewModels.Tabs.FondoArqueoViewModel)this.DataContext;
        }
        public void limpiar()
        {
            this.txtB_Contra.Clear();
            this.txtB_Contra.Focus();
            var vm = (ViewModels.Tabs.FondoArqueoViewModel)this.DataContext;
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            var dt = (System.Windows.Threading.DispatcherTimer)sender;
            dt.Stop();
            Messenger.Default.Send(new Utilities.Messages.LogoutTimeout());
        }

        private void RegisterMessages()
        {
            Messenger.Default.Register<Utilities.Messages.CloseTab>(this,
               m => {
                   Messenger.Default.Send(m, m.GID);
                   Console.WriteLine($"removing: {m.GID}");
                   if (!_tabs.Any())
                   {
                       _dt.Stop();
                   }
               });

            Messenger.Default.Register<Utilities.Messages.LogoutTimeout>(this, m => {
                _dt.Stop();
            });
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password; }
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            _dt.Stop();
        }

        private void UserControl_KeyUp(object sender, KeyEventArgs e)
        {
            _dt.Start();
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _dt.Stop();
        }

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _dt.Start();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _dt.Stop();
        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _dt.Start();
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
                    this.textBox_Copy1.Focus();
                    this.textBox_Copy1.SelectAll();
                }
            }
        }
    }
}
