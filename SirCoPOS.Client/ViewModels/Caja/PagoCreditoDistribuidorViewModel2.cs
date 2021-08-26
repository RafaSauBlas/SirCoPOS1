﻿using GalaSoft.MvvmLight.Messaging;
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
                }
                else
                {
                    MessageBox.Show("Distribuidor no localizado", "Pago Credito Distribuidor", MessageBoxButton.OK, MessageBoxImage.Error);
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
                PlazosProductos = this.Productos.Select(i => new Common.Entities.ProductoPlazo
                {
                    Serie = i.Item.Serie,
                    Plazos = i.SelectedPlazo,
                    Importe = i.Item.Precio
                }).ToArray()
            };
            Messenger.Default.Send(msg, this.GID);
        }
    }
}
