using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SirCoPOS.Client.ViewModels.Tabs
{
    class BonosViewModel : Helpers.TabViewModelBase
    {
        private readonly Common.ServiceContracts.IAdminServiceAsync _proxy;
        public BonosViewModel()
        {
            this.PropertyChanged += BonosViewModel_PropertyChanged;
            this.Items = new ObservableCollection<Common.Entities.BonoDetalle>();
            if (!this.IsInDesignMode)
            {
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IAdminServiceAsync>();
            }

            this.SaveCommand = new RelayCommand(() => {
                _proxy.PayBono(
                    gerente: this.Gerente.Value, 
                    empleado: Empleado.Value, 
                    importe: this.TotalRound.Value);
                MessageBox.Show("ready");
            }, () => this.Gerente.HasValue);

            this.LoadCommand = new RelayCommand(() => {

                var res = _proxy.GetBonos(this.Empleado.Value);
                this.Items.Clear();
                res.Detalle.ToList().ForEach(i => this.Items.Add(i));
                this.RaisePropertyChanged(nameof(this.Total));

            }, () => this.Empleado.HasValue);

            if (this.IsInDesignMode)
            {
                this.Items.Add(new Common.Entities.BonoDetalle { Unidades = 1, Descripcion = "a", Importe = 99 });
                this.Items.Add(new Common.Entities.BonoDetalle { Unidades = 2, Descripcion = "b", Importe = 100 });
                this.Items.Add(new Common.Entities.BonoDetalle { Unidades = 3, Descripcion = "c", Importe = 199 });
            }
        }

        private void BonosViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.Gerente):
                    this.SaveCommand.RaiseCanExecuteChanged();
                    break;
                case nameof(this.Total):
                    this.RaisePropertyChanged(nameof(this.TotalRound));                    
                    break;
            }
        }

        public ObservableCollection<Common.Entities.BonoDetalle> Items { get; private set; }
        public decimal? Total
        {
            get => this.Items.Sum(i => i.Importe);
        }
        public decimal? TotalRound
        {
            get => Math.Round(this.Total ?? 0, 1);
        }
        private int? _empleado;
        public int? Empleado
        {
            get => _empleado;
            set => this.Set(nameof(this.Empleado), ref _empleado, value);
        }
        private int? _gerente;
        public int? Gerente
        {
            get => _gerente;
            set => this.Set(nameof(this.Gerente), ref _gerente, value);
        }
        public RelayCommand LoadCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
    }
}
