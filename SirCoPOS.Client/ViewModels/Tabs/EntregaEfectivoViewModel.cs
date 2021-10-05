﻿using GalaSoft.MvvmLight.Command;
using SirCoPOS.Utilities.Helpers;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SirCoPOS.Client.ViewModels.Tabs
{
    class EntregaEfectivoViewModel : Helpers.TabViewModelBase
    {
        protected readonly Common.ServiceContracts.IAdminServiceAsync _proxy;
        protected readonly Common.ServiceContracts.IDataServiceAsync _pdata;             
        public EntregaEfectivoViewModel()
        {
            this.FormasPago = new ObservableCollection<Models.CajaFormaPagoEntrega>();
            this.PropertyChanged += EntregaEfectivoViewModel_PropertyChanged;
            if (!this.IsInDesignMode)
            {
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IAdminServiceAsync>();
                _pdata = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
                this.Data = _proxy.GetEntrega(this.Sucursal.Clave, this.Cajero.Id);
                if (this.Data != null)
                {
                    foreach (var item in this.Data.FormasPago)
                    {
                        var fitem = new Models.CajaFormaPagoEntrega
                        {
                            CajaFormaPago = item
                        };
                        fitem.PropertyChanged += (sender, e) =>
                        {
                            this.SaveCommand.RaiseCanExecuteChanged();
                        };
                        this.FormasPago.Add(fitem);
                    }
                }
            }

            this.SaveCommand = new RelayCommand(() => {

                //var code = Microsoft.VisualBasic.Interaction.InputBox("Codigo Auditor:");
                //var isValid = _proxy.ValidarCodigo(this.Auditor.Id, code);
                //if (!isValid)
                //{
                //    MessageBox.Show("Código no valido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //    return;
                //}

                var request = new Common.Entities.EntregaRequest
                {
                    Sucursal = this.Sucursal.Clave, 
                    CajeroId = this.Cajero.Id, 
                    AuditorId = this.Auditor.Id, 
                    Entregar = this.Entrega.Value, 
                    FormasPago = this.FormasPago.Select(i => 
                        new Common.Entities.EntregaFormaPago {
                            FormaPago  = i.CajaFormaPago.FormaPago,
                            Entregar = i.Entregar.Value,
                            Amount = i.EntregarMonto.Value
                        })
                };
                _proxy.Entrega(request);
                string msg = "Entrega se realizó correctamente";
                string info = "Entrega de Efectivo";
                MessageBox.Show(msg, info, MessageBoxButton.OK, MessageBoxImage.Information);
                this.CloseCommand.Execute(null);
            }, () => this.Auditor != null && this.Entrega.HasValue && this.User != null
                && this.FormasPago.All(i => i.Entregar.HasValue && i.EntregarMonto.HasValue));

            this.NadaCommand = new RelayCommand(() => {

            });

            this.LoadAuditorCommand = new RelayCommand(() => {
                if (this.SearchAuditor.Value == this.Cajero.Id)
                {
                    MessageBox.Show("No puedes utilizar tu propio ID como auditor, por favor introduce el ID de un auditor valido", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Messenger.Default.Send<string>("focus", "FocusRecibir");
                }
                else
                {
                    this.Auditor = _pdata.FindAuditorEntrega(this.SearchAuditor.Value, this.Cajero.Id);

                    if (this.Auditor != null)
                    {
                        this.SearchAuditor = null;
                    }
                }
            }, () => this.SearchAuditor.HasValue);
            this.LoadUserCommand = new RelayCommand(() =>
            {
                this.User = _pdata.AuditorPassword(this.Auditor.Id, this.Password);
                if (this.User != null)
                {
                    this.SearchUser = null;
                }
                else
                {
                    MessageBox.Show("Password Invalido", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }, () => this.Password != null && this.Auditor != null);
            if (this.IsInDesignMode)
            {
                this.SearchAuditor = 100;
                this.Auditor = new Common.Entities.Empleado
                {
                    Nombre = "nombre",
                    ApellidoMaterno = "materno",
                    ApellidoPaterno = "paterno",
                    Puesto = (int)Common.Constants.Puesto.SUP
                };
                this.Data = new Common.Entities.CajaFormas {
                    Efectivo = 1099m, 
                    FormasPago = new Common.Entities.CajaFormaPago[] {
                        new Common.Entities.CajaFormaPago { FormaPago = Common.Constants.FormaPago.TC, Unidades = 3, Monto = 299m },
                        new Common.Entities.CajaFormaPago { FormaPago = Common.Constants.FormaPago.VA, Unidades = 100, Monto = 10999m },
                        new Common.Entities.CajaFormaPago { FormaPago = Common.Constants.FormaPago.CP, Unidades = 1000, Monto = 1000000m }
                    }
                };
                foreach (var item in this.Data.FormasPago)
                {
                    this.FormasPago.Add(new Models.CajaFormaPagoEntrega
                    {
                        CajaFormaPago = item
                    });
                }                
                this.Entrega = 456.7m;
            }
        }
        protected override bool IsReady()
        {
            return true;
        }
        private void EntregaEfectivoViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.Auditor):
                case nameof(this.User):
                    this.SaveCommand.RaiseCanExecuteChanged();
                    break;
                case nameof(this.Entrega):
                    this.SaveCommand.RaiseCanExecuteChanged();
                    this.RaisePropertyChanged(nameof(this.EfectivoFaltante));
                    break;
            }
        }

        #region properties
        public ObservableCollection<Models.CajaFormaPagoEntrega> FormasPago { get; private set; }
        public Common.Entities.CajaFormas _data;
        public Common.Entities.CajaFormas Data
        {
            get => _data;
            set => this.Set(nameof(this.Data), ref _data, value);
        }
        public decimal? _entrega;
        public decimal? Entrega
        {
            get => _entrega;
            set => this.Set(nameof(this.Entrega), ref _entrega, value);
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
            private set
            {
                Set(nameof(this.UserOK), ref _UserOK, value);
            }
        }
        #endregion
        #region computed      
        public decimal? EfectivoFaltante
        {
            get {
                if (this.Data != null && this.Entrega.HasValue)
                {
                    return this.Data.Efectivo - this.Entrega.Value;
                }
                return null;
            }
        }
        #endregion
        #region commands
        public RelayCommand SaveCommand { get; protected set; }
        public RelayCommand NadaCommand { get; protected set; }
        public RelayCommand LoadAuditorCommand { get; protected set; }
        public RelayCommand LoadUserCommand { get; private set; }
        #endregion
    }
}
