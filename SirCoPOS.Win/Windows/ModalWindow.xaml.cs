﻿using GalaSoft.MvvmLight.Messaging;
using SirCoPOS.Win.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace SirCoPOS.Win.Windows
{
    /// <summary>
    /// Interaction logic for ModalWindow.xaml
    /// </summary>
    public partial class ModalWindow : Window
    {
        public ModalWindow()
        {
            InitializeComponent();
            _closing = false;
        }
        private bool _closing;
        public ModalWindow(Guid gid)
            : this()
        {
            Messenger.Default.Register<Utilities.Messages.ModalResponse>(this, gid, true, m => {
                if(this.IsLoaded)
                    this.DialogResult = m.Success;
                
                if(!_closing)
                    this.Close();
            });
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (this.DataContext is Utilities.Interfaces.IModalViewModel
                && !this.DialogResult.HasValue)
            {
                var ctx = (Utilities.Interfaces.IModalViewModel)this.DataContext;
                e.Cancel = true;
                
                Task.Run(() =>
                {                    
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ctx.CancelCommand.Execute(null);
                    });
                });
            }
            if (!e.Cancel)
                _closing = true;
        }

        public void cerrar()
        {
            this.Close();
        }
    }
}

