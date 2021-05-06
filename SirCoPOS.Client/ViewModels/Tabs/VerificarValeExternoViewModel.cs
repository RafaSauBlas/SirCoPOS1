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
    class VerificarValeExternoViewModel : Helpers.TabViewModelBase
    {
        private readonly Common.ServiceContracts.IDataServiceAsync _proxy;
        public VerificarValeExternoViewModel()
        {
            this.PropertyChanged += VerificarValeExternoViewModel_PropertyChanged;
            if (!IsInDesignMode)
            {
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();

                this.Negocios = _proxy.GetNegocios();                
            }
            this.SearchCommand = new GalaSoft.MvvmLight.Command.RelayCommand(() =>
            {

                this.Vale = _proxy.FindDistribuidorExterno(this.SelectedNegocio.Value, this.Cuenta, this.ValeSearch);
                if (this.Vale == null)
                    MessageBox.Show("Distribuidor no encontrado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }, () => !string.IsNullOrEmpty(this.ValeSearch) && this.SelectedNegocio.HasValue && !string.IsNullOrEmpty(this.Cuenta));

            if (this.IsInDesignMode)
            {
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
            }
        }

        private void VerificarValeExternoViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.Negocio):
                    if (!String.IsNullOrEmpty(this.Negocio))
                    {
                        var q = this.Negocios.Where(i => i.Negocio.ToLower().StartsWith(this.Negocio.ToLower()))
                            .OrderBy(i => i.Negocio);
                        var count = q.Count();
                        if (count == 1)
                        {
                            this.SelectedNegocio = q.Single().Id;
                        }
                    }
                    break;
            }
        }
        private string _cuenta;
        public string Cuenta
        {
            get => _cuenta;
            set => this.Set(nameof(this.Cuenta), ref _cuenta, value);
        }
        private int? _selectedNegocio;
        public int? SelectedNegocio
        {
            get => _selectedNegocio;
            set => this.Set(nameof(this.SelectedNegocio), ref _selectedNegocio, value);
        }
        private string _Negocio;
        public string Negocio
        {
            get { return _Negocio; }
            set { Set(nameof(this.Negocio), ref _Negocio, value); }
        }

        private IEnumerable<Common.Entities.NegocioExterno> _negocios;
        public IEnumerable<Common.Entities.NegocioExterno> Negocios
        {
            get => _negocios;
            set => this.Set(nameof(this.Negocios), ref _negocios, value);
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
        public RelayCommand SearchCommand { get; private set; }

    }
}
