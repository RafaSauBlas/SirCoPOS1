using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SirCoPOS.Client.ViewModels.Tabs
{
    class FondoAperturaViewModel : Helpers.TabViewModelBase, IValidatableObject
    {
        SirCoPOS.Services.AdminService DEM = new SirCoPOS.Services.AdminService();
        private readonly Common.ServiceContracts.IAdminServiceAsync _proxy;
        private readonly Common.ServiceContracts.IDataServiceAsync _data;
        private Utilities.Models.Settings settings;
        public FondoAperturaViewModel()
        {
            this.UserOK = false;
            this.PropertyChanged += FondoAperturaViewModel_PropertyChanged;
            settings = CommonServiceLocator.ServiceLocator.Current.GetInstance<Utilities.Models.Settings>();
            this.LoadAuditorCommand = new RelayCommand(() =>
            {
                if (this.SearchAuditor.Value == this.Cajero.Id)
                {
                    MessageBox.Show("No puedes utilizar tu propio ID como auditor, por favor introduce el ID de un auditor valido", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Messenger.Default.Send<string>("focus", "FocusAuditor");
                }
                else
                {
                    try
                    {
                        this.Auditor = _data.FindAuditorApertura(settings.Sucursal.Clave, this.SearchAuditor.Value, this.Cajero.Id);
                    }
                    catch (System.Exception e)
                    {
                        MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                        return;
                    }
                    if (this.Auditor != null)
                    {
                        if (this.Auditor.Depto == (int)Common.Constants.Departamento.ADM || this.Auditor.Depto == (int)Common.Constants.Departamento.SIS)
                        {
                            this.SearchAuditor = null;
                        }
                        else
                        {
                            if (this.Auditor.Sucursal == settings.Sucursal.Clave)
                            {
                                if (this.Auditor.Puesto == (int)Common.Constants.Puesto.ENC || this.Auditor.Puesto == (int)Common.Constants.Puesto.SUP)
                                {
                                    if (this.Auditor.Disponible >= this.Importe )
                                    {
                                        this.SearchAuditor = null;
                                    }
                                    else
                                    {
                                        MessageBox.Show(String.Format("El Disponible del auditor es {0:C}", Auditor.Disponible), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Auditor no es Gerente o Suplente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Auditor no pertenece a la misma sucursal", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Auditor no valido", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                    
            }, () => this.SearchAuditor.HasValue);
            this.LoadUserCommand = new RelayCommand(() =>
            {
                this.User = _data.AuditorPassword(this.Auditor.Id, this.Password);
                if (this.User != null )
                {
                    this.SearchUser = null;
                    this.UserOK = true;
                } else
                {
                    MessageBox.Show("Password Invalido", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.UserOK = false;
                }
            }, () => this.Password !=null && this.Auditor != null);
            this.SaveCommand = new RelayCommand(() => {
                //var code = Microsoft.VisualBasic.Interaction.InputBox("Codigo Auditor:");
                //var isValid = _proxy.ValidarCodigo(this.Auditor.Id, code);
                //if (!isValid)
                //{
                //    MessageBox.Show("Código no valido", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //    return;
                //}
                var request = new Common.Entities.FondoRequest
                {
                    Importe = this.Importe.Value,
                    Auditor = this.Auditor.Id,
                    Responsable = this.Cajero.Id,
                    Sucursal = this.Sucursal.Clave,
                    Numero = this.SelectedCaja.Numero,
                    Tipo = (Common.Constants.TipoFondo)this.SelectedCaja.Tipo
                };
                try 
                { 
                    _proxy.AbrirFondo(request);
                }
                catch (System.Exception e)
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.UserOK = false;
                    this.SearchUser = null;
                    this.SearchUser = null;
                    return;
                }
                MessageBox.Show("La operación se completó correctamente", "Fondo Apertura", MessageBoxButton.OK, MessageBoxImage.Information);
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Utilities.Messages.FondoAperturaCierre { Open = true });

                this.CloseCommand.Execute(null);

            }, () => this.Auditor != null && this.Importe.HasValue && this.SelectedCaja != null && this.User != null
                && this.IsValid());
            if (this.IsInDesignMode)
            {
                this.Auditor = new Common.Entities.Empleado
                {
                    Nombre = "nombre",
                    ApellidoPaterno = "appat",
                    ApellidoMaterno = "apmat"
                };
                this.SearchAuditor = 2;
                this.Importe = 100;
                this.Cajas = new Common.Entities.Caja[] {
                    new Common.Entities.Caja { Tipo = 1, Numero = 1, Importe = 1000m },
                    new Common.Entities.Caja { Tipo = 0, Numero = 2 },
                    new Common.Entities.Caja { Tipo = 1, Numero = 3 }
                };
                this.SelectedCaja = this.Cajas.First();
            }
            else
            {
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IAdminServiceAsync>();
                _data = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
                this.Cajas = _proxy.GetCajas(this.Sucursal.Clave, this.Cajero.Id);
            }
        }
        protected override bool IsReady()
        {
            return true;
        }
        private void FondoAperturaViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
           switch (e.PropertyName)
            {
                case nameof(this.Auditor):
                case nameof(this.SelectedCaja):
                case nameof(this.Importe):
                case nameof(this.User):
                this.SaveCommand.RaiseCanExecuteChanged();
                break;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Importe.HasValue && this.Importe < 0)
                yield return new ValidationResult("Monto no valido", new string[] { "Importe" });
            if (this.Auditor != null && this.Auditor.Disponible.HasValue && this.Importe.HasValue
                && this.Importe > this.Auditor.Disponible)
            {
                yield return new ValidationResult("Sin Fondos", new string[] { "Importe" });
            }
        }
        
        private int? _SearchAuditor;
        public int? SearchAuditor
        {
            get { return _SearchAuditor; }
            set { Set(nameof(this.SearchAuditor), ref _SearchAuditor, value); }
        }
        private Common.Entities.Empleado _Auditor;
        public Common.Entities.Empleado Auditor
        {
            get { return _Auditor; }
            set { Set(nameof(this.Auditor), ref _Auditor, value); }
        }
        private string _SearchUser;
        public string SearchUser
        {
            get { return _SearchUser; }
            set { Set(nameof(this.SearchUser), ref _SearchUser, value); }
        }
        public string Password { private get; set; }
        private Common.Entities.Empleado _User;
        public Common.Entities.Empleado User
        {
            get { return _User; }
            set { Set(nameof(this.User), ref _User, value); }
        }
        private bool _UserOK;
        public bool UserOK
        {
            get { return _UserOK; }
            private set { 
                Set(nameof(this.UserOK), ref _UserOK, value);
            }
        }

        private decimal? _importe;
        public decimal? Importe
        {
            get => _importe;
            set => this.Set(nameof(this.Importe), ref _importe, value);
        }
        public RelayCommand SaveCommand { get; private set; }
        private IEnumerable<Common.Entities.Caja> _cajas;
        public IEnumerable<Common.Entities.Caja> Cajas
        {
            get => _cajas;
            set => this.Set(nameof(this.Cajas), ref _cajas, value);
        }
        private Common.Entities.Caja _selectedCaja;
        public Common.Entities.Caja SelectedCaja
        {
            get => _selectedCaja;
            set => this.Set(nameof(this.SelectedCaja), ref _selectedCaja, value);
        }
        #region computed        
        #endregion
        #region commands
        public RelayCommand LoadAuditorCommand { get; private set; }
        public RelayCommand LoadUserCommand { get; private set; }
        #endregion
    }
}
