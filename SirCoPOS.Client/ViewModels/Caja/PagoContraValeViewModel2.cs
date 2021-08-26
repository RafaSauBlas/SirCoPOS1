using GalaSoft.MvvmLight.Messaging;
using SirCoPOS.Common.Constants;
using SirCoPOS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SirCoPOS.Client.ViewModels.Caja
{
    class PagoContraValeViewModel2 : PagoValeViewModel
    {
        public override string Title => "Pago Contra Vale";
        public override FormaPago FormaPago => FormaPago.CV;
        private Common.ServiceContracts.IDataServiceAsync _proxy;
        private Helpers.CommonHelper _common;
        public PagoContraValeViewModel2()
        {
            _common = new Helpers.CommonHelper();
            if (!IsInDesignMode)
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
            this.PropertyChanged += PagoContraValeViewModel2_PropertyChanged;
            this.SearchCommand = new GalaSoft.MvvmLight.Command.RelayCommand(async () =>
            {
                var cv = _common.PrepareContraVale(this.Search);
                this.Vale = await _proxy.FindContraValeAsync(this.SucursalSearch, cv);
                if (this.Vale != null)
                {
                    if (this.Vale.Cancelado)
                    {
                        MessageBox.Show("ContraVale Cancelado", "Pago ContraVale", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        this.SucursalSearch = null;
                        this.Search = null;
                        if (!this.HasPromocion)
                            this.SelectedPromocion = this.Promocion.Promociones.FirstOrDefault();
                        if (!this.Vale.Distribuidor.Promocion)
                            this.SelectedPromocion = this.Promocion.Promociones.FirstOrDefault();
                    }
                }
                else
                {
                    MessageBox.Show("ContraVale NO Encontrado", "Pago ContraVale", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }, () => !String.IsNullOrEmpty(this.Search) && !String.IsNullOrEmpty(this.SucursalSearch));

        }

        private void PagoContraValeViewModel2_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.Vale):
                    this.RaisePropertyChanged(nameof(this.Expirado));
                    break;
            }
        }
        protected override bool CanAccept()
        {
            if (base.CanAccept())
            {
                if (this.Expirado)
                    return false;
                return true;
            }
            return false;
        }
        protected override void Accept(Utilities.Messages.Pago p)
        {
            var cvale = (Common.Entities.CValeResponse)this.Vale;
            var msg = 
            new Utilities.Messages.Pago
            {
                FormaPago = FormaPago.CV,
                Importe = this.Pagar.Value,
                Vale = this.Vale.Vale,
                Cliente = this.Vale.ClienteId,
                Plazos = this.Plazos,
                SelectedPlazo = this.SelectedPlazo,
                Promociones = this.Promocion.Promociones,
                SelectedPromocion = this.SelectedPromocion,
                ContraVale = this.GenerateContraVale,
                Sucursal = cvale.Sucursal,
                PlazosProductos = this.Productos
                        .Where(i => i.SelectedPlazo.HasValue
                            && i.Item.FormasPago.Where(k => k.FormaPago == this.FormaPago).Any())
                        .Select(i => new ProductoPlazo
                        {
                            Serie = i.Item.Serie,
                            Plazos = i.SelectedPlazo,
                            Importe = i.Item.FormasPago.Where(k => k.FormaPago == this.FormaPago).Single().Importe
                        }).ToArray()
            };
            msg.PlazosProductos = p.PlazosProductos;
            Messenger.Default.Send(msg, this.GID);
        }
        private string _sucursalSearch;
        public string SucursalSearch
        {
            get { return _sucursalSearch; }
            set
            {
                this.Set(nameof(this.SucursalSearch), ref _sucursalSearch, value);
            }
        }
        public bool Expirado
        {
            get
            {
                if (this.Vale != null)
                {
                    if (this.Vale.Vigencia < DateTime.Now)
                        return true;
                }
                return false;
            }
        }
    }
}
