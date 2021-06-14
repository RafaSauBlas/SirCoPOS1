using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SirCoPOS.Common.Entities;

namespace SirCoPOS.Client.ViewModels.Tabs
{
    public class VerificarValeViewModel : Helpers.TabViewModelBase
    {
        private readonly Common.ServiceContracts.IDataServiceAsync _proxy;
        private readonly Common.ServiceContracts.ICreditoValeServiceAsync _pcredito;
        private readonly Helpers.ServiceClient _client;
        public VerificarValeViewModel()
        {            
            this.PropertyChanged += VerificarValeViewModel_PropertyChanged;
            if (!IsInDesignMode)
            {
                _client = new Helpers.ServiceClient();
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
                _pcredito = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.ICreditoValeServiceAsync>();
            }
            this.FindValeCommand = new RelayCommand(async () => {
                this.IsBusy = true;
                this.Vale = await _proxy.FindValeAsync(this.ValeSearch);
                if (this.Vale == null) 
                {
                    DistObserva = null;
                    MessageBox.Show("Vale no encontrado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error); 
                } else
                {
                    this.TotalVale = null;
                    this.Cliente = null;
                    this.NuevoCliente = null;
                    if (this.Vale.WithLimite)
                    {
                        this.TotalVale = this.Vale.Limite;
                        this.Cliente = new Cliente
                        {
                            Id = this.Vale.ClienteId
                        };
                    }
                    this.DistObserva = await _proxy.FindDistObservaAsync(this.Vale.Distribuidor.Cuenta);
                }
                this.IsBusy = false;
            }, () => !String.IsNullOrEmpty(this.ValeSearch));
            this.RegistrarValeCommand = new RelayCommand(async () =>
            {
                this.IsBusy = true;
                var request = new Common.Entities.RegisterValeRequest
                {
                    Vale = this.Vale.Vale, 
                    Cantidad = this.TotalVale, 
                    Electronica = false,
                    Cliente = Helpers.Parsers.PaseCliente(this.NuevoCliente, this.Cliente, this.Sucursal)
                };
                var res = await _client.RegisterValeAsync(request);
                this.IsBusy = false;
            }, () => this.Vale != null && this.TotalVale.HasValue
                && (this.Cliente != null || this.NuevoCliente != null)
                && !this.Vale.WithLimite);
            this.SolicitarCreditoCommand = new RelayCommand(async () =>
            {
                this.IsBusy = true;
                var request = new Common.Entities.SolicitudCreditoRequest
                {
                    Electronica = this.SolicitudElectronica, 
                    Vale = this.Vale.Vale, 
                    idusuario = this.Cajero.Id, 
                    Monto = this.Solicitud.Value,
                    Sucursal = this.Sucursal.Clave
                };
                var res = await _pcredito.RequestAsync(request);
                if (res.Processing.HasValue)
                {
                    MessageBox.Show($"Result: {res.Processing}");
                }
                this.IsBusy = false;
            }, () => this.Vale != null && this.Solicitud.HasValue);
            this.CreditoCommand = new RelayCommand(() =>
            {
                this.Credito = true;
            }, () => !this.Credito);
            this.LoadClienteCommand = new RelayCommand(() =>
            {
                Messenger.Default.Send(
                            new Utilities.Messages.OpenModal
                            {
                                Name = Utilities.Constants.Modals.cliente,
                                GID = this.GID
                            });
            }, () => {
                if (this.Vale != null)
                {
                    return !this.Vale.WithLimite;
                }
                return false;
            });
            this.ClearClienteCommand = new RelayCommand(() =>
            {
                this.NuevoCliente = null;
                this.Cliente = null;
            }, () => {
                if (this.Vale != null)
                {
                    return !this.Vale.WithLimite;
                }
                return false;
            });

            if (this.IsInDesignMode)
            {
                this.NuevoCliente = new Models.NuevoCliente
                {
                    Nombre = "nom", 
                    ApMaterno = "mat", 
                    ApPaterno = "pat", 
                    Id = 999
                };
                this.ValeSearch = "search";
                this.Vale = new ValeResponse
                {
                    Cancelado = true,
                    CanceladoMotivo = "motivo",
                    Disponible = 100,
                    Vale = "123",
                    Distribuidor = new Distribuidor
                    {
                        Id = 1,
                        Nombre = "nombre",
                        ApMaterno = "materno",
                        ApPaterno = "paterno",
                        Status = Common.Constants.StatusDistribuidor.SOBREGIRADO,
                        Electronica = true,
                        Firmas = new short[] { 1, 2, 3 }
                    }
                };

                this.Solicitud = 30000m;
                this.TotalVale = 10000m;
            }
        }

        protected override void RegisterMessages()
        {
            Messenger.Default.Register<Messages.ClienteMessage>(this, this.GID, m => {
                this.Cliente = m.Cliente;
                this.NuevoCliente = null;
                if (this.Cliente != null)
                {
                    this.NuevoCliente = new Models.NuevoCliente
                    {
                        Id = this.Cliente.Id,
                        Nombre = this.Cliente.Nombre,
                        ApPaterno = this.Cliente.ApPaterno,
                        ApMaterno = this.Cliente.ApMaterno
                    };                    
                }                
            });
            Messenger.Default.Register<Messages.NuevoClienteMessage>(this, this.GID, m => {
                this.NuevoCliente = m.Cliente;
                this.Cliente = null;                
            });
        }

        private void VerificarValeViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.Solicitud):
                    RaisePropertyChanged(nameof(this.FaltanteSolicitud));
                    this.SolicitarCreditoCommand.RaiseCanExecuteChanged();
                    break;
                case nameof(this.Cliente):
                case nameof(this.NuevoCliente):
                case nameof(this.TotalVale):
                    this.RegistrarValeCommand.RaiseCanExecuteChanged();                    
                    break;
                case nameof(this.Vale):
                case nameof(this.DistObserva):
                    this.ClearClienteCommand.RaiseCanExecuteChanged();
                    this.LoadClienteCommand.RaiseCanExecuteChanged();
                    this.RegistrarValeCommand.RaiseCanExecuteChanged();
                    break;
                default:
                    break;
            }
        }

        private string _valeSearch;
        public string ValeSearch
        {
            get { return _valeSearch; }
            set { this.Set(nameof(this.ValeSearch), ref _valeSearch, value); }
        }

        
        private Common.Entities.ValeResponse _vale;
        public Common.Entities.ValeResponse Vale
        {
            get { return _vale; }
            set { this.Set(nameof(this.Vale), ref _vale, value); }
        }
        public RelayCommand FindValeCommand { get; private set; }
        private decimal? _TotalVale;
        public decimal? TotalVale
        {
            get { return _TotalVale; }
            set { Set(nameof(this.TotalVale), ref _TotalVale, value); }
        }

        private decimal? _Solicitud;
        public decimal? Solicitud
        {
            get { return _Solicitud; }
            set { Set(nameof(this.Solicitud), ref _Solicitud, value); }
        }
        private bool _SolicitudElectronica;
        public bool SolicitudElectronica
        {
            get { return _SolicitudElectronica; }
            set { Set(nameof(this.SolicitudElectronica), ref _SolicitudElectronica, value); }
        }

        public decimal? FaltanteSolicitud
        {
            get {
                if (this.Vale != null)
                {
                    var fal = this.Solicitud - this.TotalVale.Value;
                    return fal;
                }
                return null;
            }
        }
        private bool _Credito;
        public bool Credito
        {
            get { return _Credito; }
            set { Set(nameof(this.Credito), ref _Credito, value); }
        }

        private DistribuidorObserva _distObserva;
        public DistribuidorObserva DistObserva
        {
            get { return _distObserva; }
            set { this.Set(nameof(this.DistObserva), ref _distObserva, value); }
        }

        private string _ContVale;
        public string ContVale
        {
            get { return _ContVale; }
            set { Set(nameof(this.ContVale), ref _ContVale, value); }
        }

        public RelayCommand RegistrarValeCommand { get; private set; }
        public RelayCommand SolicitarCreditoCommand { get; private set; }
        public RelayCommand CreditoCommand { get; private set; }
        public RelayCommand LoadClienteCommand { get; private set; }
        public RelayCommand ClearClienteCommand { get; private set; }
        private Common.Entities.Cliente _cliente;
        public Common.Entities.Cliente Cliente
        {
            get => _cliente;
            set => this.Set(nameof(this.Cliente), ref _cliente, value);
        }
        private Models.NuevoCliente _nuevoCliente;
        public Models.NuevoCliente NuevoCliente
        {
            get => _nuevoCliente;
            set => this.Set(nameof(this.NuevoCliente), ref _nuevoCliente, value);
        }
    }
}
