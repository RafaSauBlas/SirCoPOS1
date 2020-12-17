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
    class CorteViewModel : Helpers.TabViewModelBase
    {
        private readonly Common.ServiceContracts.IAdminServiceAsync _proxy;
        private readonly Common.ServiceContracts.IDataServiceAsync _data;
        private readonly Helpers.CommonHelper _common;
        public CorteViewModel()
        {
            this.PropertyChanged += CorteViewModel_PropertyChanged;
            this.Items = new ObservableCollection<Models.ItemCorte>();
            this.Series = new ObservableCollection<Models.ItemCorteSerie>();
            _common = new Helpers.CommonHelper();
            if (!this.IsInDesignMode)
            {
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IAdminServiceAsync>();
                _data = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
                this.Data = _proxy.GetCorteCaja(this.Sucursal.Clave, this.Cajero.Id);
                if (this.Data != null)
                {
                    this.Data.FormaPagoTotales.ToList().ForEach(i => {
                        var nitem = new Models.ItemCorte { Item = i };
                        nitem.PropertyChanged += Nitem_PropertyChanged;
                        this.Items.Add(nitem);
                    });
                    this.Data.Series.ToList().ForEach(i => this.Series.Add(new Models.ItemCorteSerie { Item = i }));
                }
            }

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
            //this.RemoveMontoCommand = new RelayCommand(() =>
            //{
            //    this.SelectedItemCorte.Detalle.Remove(this.SelectedDetalle);
            //}, () => this.SelectedItemCorte != null && this.SelectedDetalle != null);
            this.AddMontoCommand = new RelayCommand(() =>
            {
                this.SelectedItemCorte.Detalle.Add(new Models.ItemCorteDetalle { Amount = this.MontoTicket });
                this.MontoTicket = null;
            }, () => this.SelectedItemCorte != null && this.MontoTicket.HasValue);
            this.ScanCommand = new RelayCommand(() =>
            {
                var ser = _common.PrepareSerie(this.Scan);
                var item = this.Series.Where(i => i.Item.Serie == ser).SingleOrDefault();
                if (item != null)
                {
                    item.Reportado = true;
                    this.Scan = null;
                }
                this.SaveCommand.RaiseCanExecuteChanged();
            }, () => !String.IsNullOrEmpty(this.Scan));
            this.SaveCommand = new RelayCommand(() =>
            {
                var code = Microsoft.VisualBasic.Interaction.InputBox("Codigo Auditor:");
                var isValid = _proxy.ValidarCodigo(this.Auditor.Id, code);
                if (!isValid)
                {
                    MessageBox.Show("Código no valido", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var request = new Common.Entities.CorteRequest
                {
                    Sucursal = this.Sucursal.Clave,
                    CajeroId = this.Cajero.Id,
                    AuditorId = this.Auditor.Id,
                    FormasPago = this.Items.Select(i => 
                        new Common.Entities.ItemCorte { 
                            FormaPago = i.Item.FormaPago, 
                            Entregar = i.Entrega.Value,
                            Amount = i.Monto.Value
                        }),
                    Series = this.Series.Where(i => i.Reportado).Select(i => i.Item.Serie),
                    Entregar = this.Entregar.Value                    
                };
                _proxy.Corte(request);
                MessageBox.Show("Corte generado exitosamente", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Utilities.Messages.FondoAperturaCierre { Open = false });
                this.CloseCommand.Execute(null);
            }, () => this.Auditor != null
                && this.Entregar.HasValue
                && (!this.Items.Any() || this.Items.All(i => i.Entrega.HasValue && i.Monto.HasValue)));

            this.CajeroFinger = new RelayCommand(/*async*/ () => {
                //var fh = new Helpers.FingerPrintHelper();
                //if (fh.Connect())
                //{
                //    var finger = await fh.Scan();
                //    fh.Close();
                //    if (finger != null)
                //    {
                //        var huella = _proxy.GetHuella(this.Cajero.Id);
                //        if (huella != null)
                //        {
                //            this.CajeroValido = fh.Verify(finger, huella);
                //            this.SaveCommand.RaiseCanExecuteChanged();
                //        }
                //    }
                //}

                //this.CajeroValido = !this.CajeroValido;
            });
            this.SupervisorFinger = new RelayCommand(() => {
                //var fh = new Helpers.FingerPrintHelper();
                //if (fh.Connect())
                //{
                //    var finger = await fh.Scan();
                //    fh.Close();
                //    if (finger != null)
                //    {
                //        this.Auditor = _proxy.IdentificarSupervisor(this.Sucursal.Clave, finger);
                //        this.SupervisorValido = this.Auditor.HasValue;
                //        this.SaveCommand.RaiseCanExecuteChanged();
                //    }
                //}
                //if(this.Auditor != null)
                //    this.SupervisorValido = !this.SupervisorValido;
            });

            if (this.IsInDesignMode)
            {
                this.Scan = "scan";
                this.Data = new Common.Entities.CorteResponse
                {
                    FormaPagoTotales = new Common.Entities.FormaPagoCorte[] {
                        new Common.Entities.FormaPagoCorte { FormaPago = Common.Constants.FormaPago.VA, Count = 4, Total = 600 },
                        new Common.Entities.FormaPagoCorte { FormaPago = Common.Constants.FormaPago.TC, Count = 2, Total = 200 },
                        new Common.Entities.FormaPagoCorte { FormaPago = Common.Constants.FormaPago.TD, Count = 3 },
                        new Common.Entities.FormaPagoCorte { FormaPago = Common.Constants.FormaPago.DV, Count = 4 }
                    }, Series = new Common.Entities.SeriePrecio[] {
                        new Common.Entities.SeriePrecio { Serie = "a", Importe = 1 },
                        new Common.Entities.SeriePrecio { Serie = "b", Importe = 2 },
                        new Common.Entities.SeriePrecio { Serie = "c", Importe = 3 }
                    }, 
                    Importe = 3000m
                    //Ventas = 100,
                    //Caja = 99                    
                };
                this.Entregar = 1600m;
                this.Data.FormaPagoTotales.ToList().ForEach(i => this.Items.Add(new Models.ItemCorte { Item = i }));
                this.Items[1].Detalle.Add(new Models.ItemCorteDetalle { Amount = 100 });
                this.Items[1].Detalle.Add(new Models.ItemCorteDetalle { Amount = 100 });
                this.SelectedItemCorte = this.Items[0];
                this.SelectedItemCorte.Detalle.Add(new Models.ItemCorteDetalle { Amount = 100 });
                this.SelectedItemCorte.Detalle.Add(new Models.ItemCorteDetalle { Amount = 200 });
                this.SelectedItemCorte.Detalle.Add(new Models.ItemCorteDetalle { Amount = 300 });
                this.Data.Series.ToList().ForEach(i => this.Series.Add(new Models.ItemCorteSerie { Item = i }));
                this.Series[2].Reportado = true;
                this.Auditor = new Common.Entities.Empleado
                {
                    Nombre = "nombre",
                    ApellidoPaterno = "appat",
                    ApellidoMaterno = "apmat"
                };
                this.MontoTicket = 199.99m;
            }
        }
        protected override bool IsReady()
        {
            return true;
        }
        private void Nitem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Models.ItemCorte.Entrega):
                case nameof(Models.ItemCorte.Monto):
                    this.SaveCommand.RaiseCanExecuteChanged();
                    break;
            }
        }

        private void CorteViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                //case nameof(this.CajeroValido):
                //case nameof(this.SupervisorValido):
                //    this.SaveCommand.RaiseCanExecuteChanged();
                //    break;
                case nameof(this.Auditor):
                    this.SaveCommand.RaiseCanExecuteChanged();
                    break;
                case nameof(this.Entregar):
                    this.SaveCommand.RaiseCanExecuteChanged();
                    this.RaisePropertyChanged(nameof(this.EfectivoFaltante));
                    break;
                case nameof(this.Data):
                    this.RaisePropertyChanged(nameof(this.EfectivoFaltante));
                    break;                
            }
        }

        //private bool _cajeroValido;
        //public bool CajeroValido {
        //    get => _cajeroValido;
        //    set => this.Set(nameof(this.CajeroValido), ref _cajeroValido, value);
        //}
        //private bool _supervisorValido;
        //public bool SupervisorValido {
        //    get => _supervisorValido;
        //    set => this.Set(nameof(this.SupervisorValido), ref _supervisorValido, value);
        //}
        private decimal? _entregar;
        public decimal? Entregar
        {
            get => _entregar;
            set => this.Set(nameof(this.Entregar), ref _entregar, value);
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
        public Common.Entities.CorteResponse _Data;
        public Common.Entities.CorteResponse Data
        {
            get => _Data;
            set => this.Set(nameof(this.Data), ref _Data, value);
        }
        public string _scan;
        public string Scan
        {
            get => _scan;
            set => this.Set(nameof(this.Scan), ref _scan, value);
        }
        #region commands
        public RelayCommand ScanCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CajeroFinger { get; private set; }
        public RelayCommand SupervisorFinger { get; private set; }
        public RelayCommand LoadAuditorCommand { get; private set; }
        public RelayCommand AddMontoCommand { get; private set; }
        public RelayCommand RemoveMontoCommand { get; private set; }
        #endregion        
        public ObservableCollection<Models.ItemCorte> Items { get; private set; }
        public ObservableCollection<Models.ItemCorteSerie> Series { get; private set; }
        private Models.ItemCorte _SelectedItemCorte;
        public Models.ItemCorte SelectedItemCorte
        {
            get { return _SelectedItemCorte; }
            set { Set(nameof(this.SelectedItemCorte), ref _SelectedItemCorte, value); }
        }

        public decimal? EfectivoFaltante
        {
            get => this.Data.Importe - this.Entregar;
        }
        private decimal? _MontoTicket;
        public decimal? MontoTicket
        {
            get { return _MontoTicket; }
            set { Set(nameof(this.MontoTicket), ref _MontoTicket, value); }
        }
        private Models.ItemCorteDetalle _SelectedDetalle;
        public Models.ItemCorteDetalle SelectedDetalle
        {
            get { return _SelectedDetalle; }
            set { Set(nameof(this.SelectedDetalle), ref _SelectedDetalle, value); }
        }

    }
}
