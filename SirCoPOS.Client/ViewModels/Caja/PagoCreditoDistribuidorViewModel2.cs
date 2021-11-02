using GalaSoft.MvvmLight.Messaging;
using SirCoPOS.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SirCoPOS.Client.ViewModels.Caja
{
    class PagoCreditoDistribuidorViewModel2 : PagoValeViewModel
    {
        public override FormaPago FormaPago => FormaPago.CD;
        public override string Title => "Pago Credito Distribuidor";
        protected Common.ServiceContracts.IDataServiceAsync _proxy;
        private Helpers.CommonHelper _common;
        public PagoCreditoDistribuidorViewModel2()
        {

            if (!this.IsInDesignMode)
            {
                _common = new Helpers.CommonHelper();
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
            }
            this.SearchCommand = new GalaSoft.MvvmLight.Command.RelayCommand(async () =>
            {
                var res = _common.PrepareTarjetahabiente(this.Search);
                this.Vale = await _proxy.FindDistribuidorAsync(res);
                if (this.Vale != null)
                {
                    this.Search = null;
                    if (!this.HasPromocion)
                        this.SelectedPromocion = this.Promocion.Promociones.FirstOrDefault();
                    if (!this.Vale.Distribuidor.Promocion)
                        this.SelectedPromocion = this.Promocion.Promociones.FirstOrDefault();
                    maxPlazosDist = this.Vale.Distribuidor.maxPlazos;
                    if (Vale.Distribuidor.Electronica && this.maxPlazosDist != null)
                    {
                        if (this.maxPlazosElectronica > this.maxPlazosDist)
                        {
                            foreach (var e in this.Productos)
                            {
                                e.SetPlazos((int)this.maxPlazosDist);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Distribuidor no encontrado.", "Pago Credito Distribuidor", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }, () => !String.IsNullOrEmpty(this.Search));
        }
        protected override void Accept(Utilities.Messages.Pago p)
        {
            var msg = new Utilities.Messages.Pago
            {
                FormaPago = this.FormaPago,
                Importe = this.Pagar.Value,
                Vale = this.Vale.Vale,
                Cliente = this.Vale.Distribuidor.ClienteId,
                Plazos = this.Plazos,
                SelectedPlazo = this.SelectedPlazo,
                Promociones = this.Promocion.Promociones,
                SelectedPromocion = this.SelectedPromocion,
                DistribuidorId = this.Vale.Distribuidor.Id,
                PlazosProductos = p.PlazosProductos
            };
            Messenger.Default.Send(msg, this.GID);
        }
    }
}
