using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SirCoPOS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SirCoPOS.Client.ViewModels.Caja
{
    class LoadVendedorViewModel : Helpers.ModalViewModelBase, Utilities.Interfaces.IModal
    {
        public string Title => "Cargar Vendedor";
        private readonly Common.ServiceContracts.ICommonServiceAsync _proxy;
        public LoadVendedorViewModel()
        {
            if (!IsInDesignMode)
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.ICommonServiceAsync>();

            this.PropertyChanged += LoadVendedorViewModel_PropertyChanged;
            
            this.FindVendedorCommand = new GalaSoft.MvvmLight.Command.RelayCommand(async () => {                
                if (!this.VendedorSearch.HasValue)
                {
                    this.AcceptCommand.Execute(null);
                }
                else
                {
                    this.IsBusy = true;
                    await this.FindVendedor();
                    this.IsBusy = false;
                }
            });

            if (this.IsInDesignMode)
            {
                this.VendedorSearch = 123456;
                this.Vendedor = new Empleado {
                    Id = 1,
                    Clave = "a",
                    Usuario = "user",
                    Nombre = "nombre",
                    ApellidoPaterno = "appaterno",
                    ApellidoMaterno = "apmaterno"
                };
            }
        }
        protected override void Accept()
        {
            Messenger.Default.Send(new Messages.Vendedor() { Success = true, Empleado = this.Vendedor }, this.GID);
        }
        protected override bool CanAccept()
        {
            return this.Vendedor != null;
        }
        protected override void Cancel()
        {
            Messenger.Default.Send(new Messages.Vendedor(), this.GID);
        }
        private void LoadVendedorViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.Result):
                    this.RaisePropertyChanged(nameof(this.NotFound));
                    break;
            }
            this.AcceptCommand.RaiseCanExecuteChanged();
        }

        private async Task FindVendedor()
        {
            this.Result = false;
            this.Vendedor = await _proxy.FindVendedorAsync(this.VendedorSearch.Value);
            if (this.Vendedor != null)
                this.VendedorSearch = null;
            else
                MessageBox.Show("Vendedor no encontrado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            this.Result = true;
        }
        public RelayCommand FindVendedorCommand { get; private set; }
        public bool CloseTab { get { return true; } }
        private int? _vendedorSearch;
        public int? VendedorSearch
        {
            get { return _vendedorSearch; }
            set { Set(nameof(this.VendedorSearch), ref _vendedorSearch, value); }
        }
        private Empleado _vendedor;
        public Empleado Vendedor
        {
            get { return _vendedor; }
            set { Set(nameof(this.Vendedor), ref _vendedor, value); }
        }
        private bool _result;
        public bool Result {
            get => _result;
            set => this.Set(nameof(this.Result), ref _result, value);
        }
        public bool NotFound
        {
            get => this.Result && this.Vendedor == null;
        }
    }
}
