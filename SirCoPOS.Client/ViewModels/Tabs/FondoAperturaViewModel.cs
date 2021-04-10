using GalaSoft.MvvmLight.Command;
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
        public FondoAperturaViewModel()
        {
            this.PropertyChanged += FondoAperturaViewModel_PropertyChanged;
            this.LoadAuditorCommand = new RelayCommand(() =>
            {
                this.Auditor = _data.FindAuditorApertura(this.SearchAuditor.Value, this.Cajero.Id);
                if (this.Auditor != null)
                {
                    this.SearchAuditor = null;
                }
                else
                {
                    MessageBox.Show("Auditor no valido", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }, () => this.SearchAuditor.HasValue);
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
                _proxy.AbrirFondo(request);
                MessageBox.Show("La operación se completó correctamente", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Utilities.Messages.FondoAperturaCierre { Open = true });

                this.CloseCommand.Execute(null);

            }, () => this.Auditor != null && this.Importe.HasValue && this.SelectedCaja != null
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
        #endregion
    }
}
