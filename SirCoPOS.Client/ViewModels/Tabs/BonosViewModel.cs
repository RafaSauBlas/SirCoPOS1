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
        protected readonly Common.ServiceContracts.IDataServiceAsync _pdata;
        private Utilities.Models.Settings settings;
        public BonosViewModel()
        {
            this.PropertyChanged += BonosViewModel_PropertyChanged;
            this.Items = new ObservableCollection<Common.Entities.BonoDetalle>();
            settings = CommonServiceLocator.ServiceLocator.Current.GetInstance<Utilities.Models.Settings>();
            _pdata = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();

            if (!this.IsInDesignMode)
            {
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IAdminServiceAsync>();
                _cnn = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.ICommonServiceAsync>();
            }

            this.SaveCommand = new RelayCommand(() => {
                var pagado = _proxy.PayBono(
                    cajero: this.Cajero.Id, 
                    empleado: Empleado.Value, 
                    gerente: Gerente.Value,
                    importe: this.TotalRound.Value);
                if (pagado) {
                    MessageBox.Show("Pago Efectuado", "Pago de Bonos", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    Messenger.Default.Send(new Utilities.Messages.CloseTab { GID = this.GID });
                }
                else
                {
                    MessageBox.Show("No hay disponible suficiente para el pago", "Pago de Bonos", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }, () => this.Gerente.HasValue && this.Empleado.HasValue && this.Total.HasValue && passwordOK);

            this.LoadCommand = new RelayCommand(() => {
                Items.Clear();
                SirCoPOS.Common.Entities.Empleado empleado = _cnn.FindEmpleadoBono(Empleado.Value);
                if (empleado != null)
                {
                    EmpleadoNombre = empleado.Nombre + " " + empleado.ApellidoPaterno + " " + empleado.ApellidoMaterno;
                    if (empleado.Depto == (int)Common.Constants.Departamento.TDA)
                    {
                        if (empleado.Clave.Substring(0, 2) == settings.Sucursal.Clave)
                        {
                            var res = _proxy.GetBonos(this.Empleado.Value);
                            if (res.Detalle.Count() == 0)
                            {
                                MessageBox.Show("El empleado no tiene registros para pago" , "Pago de Bonos", MessageBoxButton.OK, MessageBoxImage.Error);
                                ClearItems();
                            }
                            else
                            {
                                res.Detalle.ToList().ForEach(i => this.Items.Add(i));
                                this.RaisePropertyChanged(nameof(this.Total));
                            }
                        }
                        else
                        {
                            MessageBox.Show("El empleado no pertenece a la Sucursal " + settings.Sucursal.Descripcion, "Pago de Bonos", MessageBoxButton.OK, MessageBoxImage.Error);
                            ClearItems();
                        }
                    }
                    else
                    {
                        MessageBox.Show("El empleado no pertenece al departamento TIENDA", "Pago de Bonos", MessageBoxButton.OK, MessageBoxImage.Error);
                        ClearItems();
                    }
                }
                else
                {
                    MessageBox.Show("No se encontró Empleado", "Pago de Bonos", MessageBoxButton.OK, MessageBoxImage.Error);
                    ClearItems();
                }
            }, () => this.Empleado.HasValue);

            this.LoadGerente = new RelayCommand(() => {

                SirCoPOS.Common.Entities.Empleado empleado = _cnn.FindEmpleadoBono(Gerente.Value);
                if (empleado != null)
                {
                    if (empleado.Depto == (int)Common.Constants.Departamento.TDA)
                    {
                        if (Common.Constants.Puestos.Gerentes.Contains(empleado.Puesto))
                        {
                            if (empleado.Clave.Substring(0, 2) == settings.Sucursal.Clave)
                            {
                                GerenteNombre = empleado.Nombre + " " + empleado.ApellidoPaterno + " " + empleado.ApellidoMaterno;
                                this.RaisePropertyChanged(nameof(this.Gerente));
                            }
                            else
                            {
                                MessageBox.Show("El Gerente no pertenece a la Sucursal " + settings.Sucursal.Descripcion, "Pago de Bonos", MessageBoxButton.OK, MessageBoxImage.Error);
                                ClearItems(true);
                            }
                        }
                        else
                        {
                            MessageBox.Show("El Gerente no es Encargado o Suplente" , "Pago de Bonos", MessageBoxButton.OK, MessageBoxImage.Error);
                            ClearItems(true);
                        }
                    }
                    else
                    {
                        MessageBox.Show("El Gerente no pertenece al departamento TIENDA", "Pago de Bonos", MessageBoxButton.OK, MessageBoxImage.Error);
                        ClearItems(true);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontró el gerente", "Pago de Bonos", MessageBoxButton.OK, MessageBoxImage.Error);
                    ClearItems(true);
                }
            }, () => this.Gerente.HasValue && this.Empleado.HasValue);
            this.LoadUserCommand = new RelayCommand(() =>
            {
                this.User = _pdata.AuditorPassword((int)Gerente, this.Password);
                if (this.User != null)
                {
                    passwordOK = true;
                    Messenger.Default.Send<string>("next", "SetFocus");
                }
                else
                {
                    MessageBox.Show("Password Invalido", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }, () => this.Password != null && this.Gerente.HasValue);

            if (this.IsInDesignMode)
            {
                this.Items.Add(new Common.Entities.BonoDetalle { Unidades = 1, Descripcion = "a", Importe = 99 });
                this.Items.Add(new Common.Entities.BonoDetalle { Unidades = 2, Descripcion = "b", Importe = 100 });
                this.Items.Add(new Common.Entities.BonoDetalle { Unidades = 3, Descripcion = "c", Importe = 199 });
            }
        }

        private void ClearItems(bool Gte = false)
        {
            if (!Gte)
            {
                Items.Clear();
                Empleado = null;
                EmpleadoNombre = null;
                Gerente = null;
                GerenteNombre = null;
                Password = null;
                passwordOK = false;
                this.RaisePropertyChanged(nameof(this.Total));
            }
            else
            {
                Gerente = null;
                GerenteNombre = null;
            }
        }

        private void BonosViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.passwordOK):
                    this.SaveCommand.RaiseCanExecuteChanged();
                    break;
                case nameof(this.Gerente):
                case nameof(this.User):
                    this.passwordOK = false;
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
            get { 
                if (this.Items.Sum(i => i.Importe ) < 0)
                {
                    return 0;
                }
                return this.Items.Sum(i => i.Importe); 

            }
        }
        public decimal? TotalRound
        {
            get => Math.Round(this.Total ?? 0, 1);
        }
        private int? _empleado;
        public int? Empleado
        {
            get => _empleado;
            set {
                this.Set(nameof(this.Empleado), ref _empleado, value);
            }
        }
        private int? _gerente;
        public int? Gerente
        {
            get => _gerente;
            //set => this.Set(nameof(this.Gerente), ref _gerente, value);
            set
            {
                this.Set(nameof(this.Gerente), ref _gerente, value);
                this.RaisePropertyChanged(nameof(this.Gerente));
            }
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
        private bool _passwordok;
        public bool passwordOK
        {
            get => _passwordok;
            set => this.Set(nameof(this.passwordOK),ref _passwordok, value);
        }
        private Common.Entities.Empleado _User;
        public Common.Entities.Empleado User
        {
            get { return _User; }
            set { Set(nameof(this.User), ref _User, value); }
        }
        public string Password { private get; set; }
        public RelayCommand LoadCommand { get; private set; }
        public RelayCommand LoadGerente { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand LoadUserCommand { get; private set; }
    }
}
