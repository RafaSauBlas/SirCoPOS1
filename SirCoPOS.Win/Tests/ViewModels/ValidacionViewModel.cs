using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SirCoPOS.Win.Tests.ViewModels
{
    class ValidacionViewModel
    {
        public ValidacionViewModel()
        {
            this.Text = "Hello world!";
            this.Prueba = new Models.PruebaValidacion() { Campo1 = "a" };

            this.ValidarCommand = new GalaSoft.MvvmLight.Command.RelayCommand(() => {
                MessageBox.Show($"{this.Prueba.IsValid()}");
            });

            this.SaveCommand = new GalaSoft.MvvmLight.Command.RelayCommand(() => {
                this.Prueba.CommitEdit();
            }, () => this.Prueba.IsDirty());

            this.CancelCommand = new GalaSoft.MvvmLight.Command.RelayCommand(() => {
                this.Prueba.CancelEdit();
            }, () => this.Prueba.IsDirty());

            this.Prueba.PropertyChanged += Prueba_PropertyChanged;
        }

        private void Prueba_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.SaveCommand.RaiseCanExecuteChanged();
            this.CancelCommand.RaiseCanExecuteChanged();
        }

        public GalaSoft.MvvmLight.Command.RelayCommand ValidarCommand { get; private set; }
        public GalaSoft.MvvmLight.Command.RelayCommand SaveCommand { get; private set; }
        public GalaSoft.MvvmLight.Command.RelayCommand CancelCommand { get; private set; }

        public string Text { get; set; }
        public Models.PruebaValidacion Prueba { get; set; }
    }
}
