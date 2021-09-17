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
    class PagoValeDigitalViewModel2 : PagoValeViewModel
    {
        public override string Title => "Pago Vale Digital";
        private Common.ServiceContracts.IDataServiceAsync _proxy;
        public override FormaPago FormaPago => FormaPago.VD;
        public PagoValeDigitalViewModel2()
        {
            if (!this.IsInDesignMode)
            {                
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
            }
            this.PropertyChanged += PagoValeDigitalViewModel2_PropertyChanged;
            this.SearchCommand = new GalaSoft.MvvmLight.Command.RelayCommand(async () =>
            {
                this.Vale = await _proxy.FindValeDigitalAsync(this.Search);
                if (this.Vale != null)
                {
                    if (Expirado)
                    {
                        MessageBox.Show("Vale Expirado.", "Pago Vale Digital", MessageBoxButton.OK, MessageBoxImage.Error);
                        this.RaisePropertyChanged(nameof(this.Expirado));
                    }
                    else
                    {
                        this.Search = null;
                        if (!this.HasPromocion)
                            this.SelectedPromocion = this.Promocion.Promociones.FirstOrDefault();
                        if (!this.Vale.Distribuidor.Promocion)
                            this.SelectedPromocion = this.Promocion.Promociones.FirstOrDefault();
                        }
                }
                else
                {
                    MessageBox.Show("Vale no encontrado.", "Pago Vale Digital", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }, () => !String.IsNullOrEmpty(this.Search));
        }

        private void PagoValeDigitalViewModel2_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.ClientId):
                    {
                        this.Vale = _proxy.FindValeDigitalByClient(this.ClientId.Value);
                        if (this.Vale != null)
                        {
                            var Cliente= _proxy.FindCliente(this.ClientId.Value);
                            if (Cliente != null )
                            {
                                this.NombreCliente = Cliente.NombreCompleto;
                            }
                            this.Search = null;
                            if (!this.Vale.Distribuidor.Promocion)
                                this.SelectedPromocion = this.Promocion.Promociones.FirstOrDefault();
                        }
                    }
                    break;
            }
        }

        protected override bool CanAccept()
        {

            if (this.Vale != null)
            {
                if (this.Vale.Cancelado)
                    return false;

                if (this.Expirado)
                    return false;

                this.SkipPropertyChanged = true;
                var isvalid = this.IsValid();
                this.SkipPropertyChanged = false;
                if (!isvalid)
                    return false;

                if (this.Pagar > this.Vale.Disponible)
                    return false;

                if (this.Limite.HasValue)
                {
                    if (this.Pagar > this.Limite)
                        return false;
                    if (this.Limite > this.Vale.Distribuidor.LimiteVale)
                        return false;
                }

                if (this.Vale.Distribuidor.Status != Common.Constants.StatusDistribuidor.ACTIVO)
                    return false;

                if (this.Vale.Distribuidor.Promocion)
                {
                    if (!this.SelectedPromocion.HasValue)
                        return false;
                }
                if (this.HasPromocionPlazos)
                {
                    if (!this.SelectedPlazo.HasValue)
                        return false;
                }

                if (this.Productos.Any() && this.Vale.Distribuidor.Electronica)
                {
                    var q = this.ProductosView.Cast<Utilities.Models.ProductoPlazoOpciones>()
                        .Where(i => !i.SelectedPlazo.HasValue);
                    if (q.Any())
                        return false;
                }

                if (this.Pagar > this.TotalCalzado && !this.Vale.Distribuidor.Electronica
                    && this.Productos.Any())
                {
                    return false;
                }
                if (this.Pagar > this.Total)
                {
                    return false;
                }

                return true;
            }
            return false;
        }

        protected override void Accept(Utilities.Messages.Pago p)
        {
            var msg = new Utilities.Messages.Pago
            {
                FormaPago = this.FormaPago,
                Importe = this.Pagar.Value,
                Vale = this.Vale.Vale,
                Cliente = this.Vale.ClienteId,
                NombreCliente = this.NombreCliente,
                Plazos = this.Plazos,
                SelectedPlazo = this.SelectedPlazo,
                Promociones = this.Promocion.Promociones,
                SelectedPromocion = this.SelectedPromocion,
                PlazosProductos = this.Productos
                        .Where(i => i.SelectedPlazo.HasValue
                            && i.Item.FormasPago.Where(k => k.FormaPago == this.FormaPago).Any())
                        .Select(i => new Common.Entities.ProductoPlazo
                        {
                            Serie = i.Item.Serie,
                            Plazos = i.SelectedPlazo,
                            Importe = i.Item.FormasPago.Where(k => k.FormaPago == this.FormaPago).Single().Importe
                        }).ToArray()
            };
            Messenger.Default.Send(msg, this.GID);
        }

        private string _nombrecliente;
        public string NombreCliente
        {
            get { return _nombrecliente; }
            set
            {
                this.Set(nameof(this.NombreCliente), ref _nombrecliente, value);
            }
        }
        public bool Expirado
        {
            get
            {
                if (this.Vale != null)
                {
                    if (this.Vale.Vigencia < DateTime.Today)
                        return true;
                }
                return false;
            }
        }
    }

}
