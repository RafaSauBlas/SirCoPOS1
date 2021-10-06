using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;

namespace SirCoPOS.Client.ViewModels.Tabs
{
    class BonosViewModel : Helpers.TabViewModelBase
    {
        private readonly Common.ServiceContracts.IAdminServiceAsync _proxy;
        private readonly Common.ServiceContracts.ICommonServiceAsync _cnn;
        public BonosViewModel()
        {
            this.PropertyChanged += BonosViewModel_PropertyChanged;
            this.Items = new ObservableCollection<Common.Entities.BonoDetalle>();
            if (!this.IsInDesignMode)
            {
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IAdminServiceAsync>();
                _cnn = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.ICommonServiceAsync>();
            }

            this.SaveCommand = new RelayCommand(() => {
                var pagado = _proxy.PayBono(
                    gerente: this.Gerente.Value, 
                    empleado: Empleado.Value, 
                    importe: this.TotalRound.Value);
                if (pagado) {
                    MessageBox.Show("Pago Efectuado", "Pago de Bonos", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    Messenger.Default.Send(new Utilities.Messages.CloseTab { GID = this.GID });
                }
                else
                {
                    MessageBox.Show("No hay disponible suficiente para el pago", "Pago de Bonos", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }, () => this.Gerente.HasValue);

            this.LoadCommand = new RelayCommand(() => {

                SirCoPOS.Common.Entities.Empleado empleado = _cnn.FindEmpleadoBono(Empleado.Value);
                if (empleado != null)
                {
                    EmpleadoNombre = empleado.Nombre + " " + empleado.ApellidoPaterno + " " + empleado.ApellidoMaterno;
                    var res = _proxy.GetBonos(this.Empleado.Value);
                    this.Items.Clear();
                    res.Detalle.ToList().ForEach(i => this.Items.Add(i));
                    this.RaisePropertyChanged(nameof(this.Total));
                }
                else
                {
                    Empleado = null;
                    EmpleadoNombre = null;
                }
            }, () => this.Empleado.HasValue);

            this.LoadGerente = new RelayCommand(() => {

                SirCoPOS.Common.Entities.Empleado empleado = _cnn.FindEmpleadoBono(Gerente.Value);
                if (empleado != null)
                {
                    GerenteNombre = empleado.Nombre + " " + empleado.ApellidoPaterno + " " + empleado.ApellidoMaterno;
                    this.RaisePropertyChanged(nameof(this.Gerente));
                }
                else
                {
                    Gerente = null;
                    GerenteNombre = null;
                }
            }, () => this.Gerente.HasValue && this.Empleado.HasValue);

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
        private string _empleadonombre;
        public string EmpleadoNombre
        {
            get => _empleadonombre;
            set => this.Set(nameof(this.EmpleadoNombre), ref _empleadonombre, value);
        }
        private string _gerentenombre;
        public string GerenteNombre
        {
            get => _gerentenombre;
            set => this.Set(nameof(this.GerenteNombre), ref _gerentenombre, value);
        }
        public RelayCommand LoadCommand { get; private set; }
        public RelayCommand LoadGerente { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
    }
}
