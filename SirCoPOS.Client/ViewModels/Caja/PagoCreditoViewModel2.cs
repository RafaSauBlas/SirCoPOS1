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
    class PagoCreditoViewModel2 : PagoValeViewModel
    {
        public override string Title => "Pago Credito Personal";
        public override FormaPago FormaPago => FormaPago.CP;
        protected Common.ServiceContracts.IDataServiceAsync _proxy;
        private Helpers.CommonHelper _common;
        public PagoCreditoViewModel2()
        {
            if (!this.IsInDesignMode)
            {
                _common = new Helpers.CommonHelper();
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
            }

            this.PropertyChanged += PagoCreditoViewModel2_PropertyChanged;
            this.SearchCommand = new GalaSoft.MvvmLight.Command.RelayCommand(async () =>
            {
                valeOK = true;
                var res = _common.PrepareTarjetahabiente(this.Search);
                this.Vale = await _proxy.FindTarjetahabienteAsync(res);                
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
                        foreach (var e in this.Productos)
                        {
                            if (e.GetPlazos() > this.maxPlazosDist)
                            {
                                e.SetPlazos((int)this.maxPlazosDist);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Tarjetahabiente no encontrado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    valeOK = false;
                }
                if (this.valeOK)
                    Messenger.Default.Send<string>("focus", "NextFocus");
            }, () => !String.IsNullOrEmpty(this.Search));
            var settings = CommonServiceLocator.ServiceLocator.Current.GetInstance<Utilities.Models.Settings>();
            this.Promocion = _proxy.FindPromocionesVale(settings.Sucursal.Clave, "TARJETAHABIENTE");
        }

        private void PagoCreditoViewModel2_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }
        protected override void Accept(Utilities.Messages.Pago p)
        {
            var msg = new Utilities.Messages.Pago
            {
                FormaPago = this.FormaPago,
                Importe = this.Pagar.Value,
                Vale = this.Vale.Vale,
                DistribuidorId = this.Vale.Distribuidor.Id,
                Cliente = this.Vale.Distribuidor.ClienteId,
                Plazos = this.Plazos,
                SelectedPlazo = this.SelectedPlazo,
                Promociones = this.Promocion.Promociones,
                SelectedPromocion = this.SelectedPromocion,
                PlazosProductos = p.PlazosProductos
            };
            Messenger.Default.Send(msg, this.GID);
        }
    }
}
