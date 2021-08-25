﻿using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SirCoPOS.Client.ViewModels.Tabs
{
    class FondoArqueoViewModel : Helpers.TabViewModelBase
    {
        private readonly Common.ServiceContracts.IAdminServiceAsync _proxy;
        private readonly Common.ServiceContracts.IDataServiceAsync _data;

        public FondoArqueoViewModel()
        {
            this.PropertyChanged += FondoArqueoViewModel_PropertyChanged;
            this.LoadAuditorCommand = new RelayCommand(() =>
                {
                    this.Auditor = _data.FindAuditorApertura(this.SearchAuditor.Value, this.Cajero.Id);
                if (this.Auditor != null)
                {
                    this.SearchAuditor = null;
                }
                else
                {
                    MessageBox.Show("Auditor no valido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }, () => this.SearchAuditor.HasValue);
            this.LoadUserCommand = new RelayCommand(() =>
            {
                this.User = _data.AuditorPassword(this.Auditor.Id, this.Password);
                if (this.User != null)
                {
                    this.SearchUser = null;
                    this.UserOK = true;
                }
                else
                {
                    MessageBox.Show("Password Invalido", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    
                    this.UserOK = false;
                }
            }, () => this.Password != null && this.Auditor != null);
            this.SaveCommand = new RelayCommand(() => {
                //var code = Microsoft.VisualBasic.Interaction.InputBox("Codigo Auditor:");
                //var isValid = _proxy.ValidarCodigo(this.Auditor.Id, code);
                //if (!isValid)
                //{
                //    MessageBox.Show("Código no valido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //    return;
                //}

                var request = new Common.Entities.FondoArqueoRequest
                {
                    Importe = this.Importe.Value,
                    Auditor = this.Auditor.Id,
                    Responsable = this.Cajero.Id
                };
                _proxy.ArqueoFondo(request);
                MessageBox.Show("La operación se completó correctamente", "Fondo Arqueo", MessageBoxButton.OK, MessageBoxImage.Information);
                this.CloseCommand.Execute(null);
            }, () => this.Auditor != null && this.Importe.HasValue && this.Effectivo.HasValue && this.User != null
                && this.Faltante >= 0);
            if (this.IsInDesignMode)
            {
                this.Effectivo = 150;
                this.Auditor = new Common.Entities.Empleado { 
                    Nombre = "nom", 
                    ApellidoMaterno = "mat", 
                    ApellidoPaterno = "pat"
                };
                this.Importe = 100;
                this.SearchAuditor = 99;                
            }
            else
            {
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IAdminServiceAsync>();
                _data = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
                this.Effectivo = _proxy.GetDisponibleFondo(this.Cajero.Id);
            }
        }

        protected override bool IsReady()
        {
            return true;
        }
        private void FondoArqueoViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.Auditor):
                case nameof(this.User):
                    this.SaveCommand.RaiseCanExecuteChanged();
                    break;
                case nameof(this.Importe):
                    this.RaisePropertyChanged(nameof(this.Faltante));
                    this.SaveCommand.RaiseCanExecuteChanged();
                    break;
            }
        }
        private Common.Entities.Empleado _Auditor;
        public Common.Entities.Empleado Auditor
        {
            get { return _Auditor; }
            set { Set(nameof(this.Auditor), ref _Auditor, value); }
        }

        private decimal? _Importe;
        public decimal? Importe
        {
            get { return _Importe; }
            set { Set(nameof(this.Importe), ref _Importe, value); }
        }
        private decimal? _Effectivo;
        public decimal? Effectivo
        {
            get { return _Effectivo; }
            set { Set(nameof(this.Effectivo), ref _Effectivo, value); }
        }

        public decimal? Faltante
        {
            get {
                if (this.Effectivo.HasValue && this.Importe.HasValue)
                {
                    return this.Effectivo - this.Importe;
                }
                return null;
            }
        }
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand LoadAuditorCommand { get; private set; }
        public RelayCommand LoadUserCommand { get; private set; }
        private int? _SearchAuditor;
        public int? SearchAuditor
        {
            get { return _SearchAuditor; }
            set { Set(nameof(this.SearchAuditor), ref _SearchAuditor, value); }
        }

        private int? _Cambio;
        public int? Cambio
        {
            get { return _Cambio; }
            set { Set(nameof(this.Cambio), ref _Cambio, value); }
        }

        private string _SearchUser;
        public string SearchUser
        {
            get { return _SearchUser; }
            set { Set(nameof(this.SearchUser), ref _SearchUser, value); }
        }
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
        public string Password { private get; set; }
    }
}
