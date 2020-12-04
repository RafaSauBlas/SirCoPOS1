using SirCoPOS.Common.Constants;
using SirCoPOS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.DesignMode.ViewModels
{
    class CajaViewModel : Client.ViewModels.Tabs.CajaViewModel
    {
        public CajaViewModel()
        {
            this.Vendedor = new Empleado { Clave = "0031", Nombre = "Perla Erika", ApellidoPaterno = "Martos", ApellidoMaterno = "Corral" };
            this.SerieSearch = "0000003343805";
            this.Productos.Add(new Client.Models.Producto { MaxPlazos = 30, Id = 1, Serie = "001", Marca = "a", Modelo = "b", Talla = "c", PrecioOriginal = 100, Precio = 120, DescuentoDirecto = null, HasImage = true, Electronica = true });
            this.Productos.Add(new Client.Models.Producto { Id = 2, Serie = "002", Marca = "a", Modelo = "b", Talla = "c", PrecioOriginal = 100, Precio = 100, DescuentoDirecto = null, HasImage = true, Pago = 100 });
            this.Productos.Add(new Client.Models.Producto { MaxPlazos = 20, Id = 3, Serie = "003", Marca = "a", Modelo = "b", Talla = "c", Precio = 100, DescuentoDirecto = 10 });
            this.Productos.Add(new Client.Models.Producto { Id = 4, Serie = "004", Marca = "a", Modelo = "b", Talla = "c", PrecioOriginal = 100, Precio = 100, DescuentoDirecto = null, Pago = 100 });
            this.Productos.Add(new Client.Models.Producto { Id = 5, Serie = "005", Marca = "a", Modelo = "b", Talla = "c", Precio = 100, DescuentoDirecto = null });
            this.Productos.Add(new Client.Models.Producto { Id = 6, Serie = "006", Marca = "a", Modelo = "b", Talla = "c", Precio = 100, DescuentoDirecto = 15, Pago = 85 });
            this.Productos[0].FormasPago.Add(new FormaPagoImporte { FormaPago = FormaPago.EF });
            this.Productos[0].FormasPago.Add(new FormaPagoImporte { FormaPago = FormaPago.TC });
            this.SelectedItem = this.Productos[1];
            this.Pagos.Add(new Client.Models.Pagos.Pago { FormaPago = FormaPago.EF, Importe = 100 });
            this.Pagos.Add(new Models.Pagos.PagoTarjeta { FormaPago = FormaPago.TC, Importe = 30 });
            this.Pagos.Add(new Client.Models.Pagos.PagoDevolucion { FormaPago = FormaPago.DV, Importe = 20 });
            this.Pagos.Add(new Models.Pagos.PagoVale { FormaPago = FormaPago.VA, Importe = 20, Vale = "000123" });
            this.SelectedPago = this.Pagos[1];
            this.CuponSearch = "cupon123";
            this.PromocionesCupones = new ObservableCollection<Promocion>()
            {
                new Promocion{ Enabled = true, PromocionId = 3, Nombre = "promo 3", HasCliente = true },
                new PromocionCupon { Used = true, PromocionId = 4, CuponId = 1, Cupon = "ABC123", Nombre = "cupon1", Descripcion = "desc", Restricciones = "rest", HasCliente = true },
                new Promocion{ PromocionId = 1, Nombre = "promo 1", Used = true },
                new PromocionCupon { PromocionId = 5, CuponId = 2, Cupon = "aa", Nombre = "cupon2", Descripcion = "desc2", Restricciones = "rest2" },
                new PromocionCupon { PromocionId = 6, CuponId = 3, Cupon = "vvv", Nombre = "cupon3", Descripcion = "des3c3", Restricciones = "rest3" },
                new Promocion{ PromocionId = 2, Nombre = "promo 2" }                
            };
            this.Cupones = new ObservableCollection<Cupon>()
            {
                new Cupon { Folio = "0000000005", Nombre = "CUPON GRADUACION 50% 2DO" },
                new Cupon { Folio = "0000000236", Nombre = "CUPON 15 % DSCTO CALZ ESC" }
            };
            this.SaleResponse = new SaleResponse 
            {
                Folio = "fol123",
                Cliente = 123,
                ContraVales = new ContraValeResponse[] {
                    new ContraValeResponse { ContraVale = "a1", Importe = 123m },
                    new ContraValeResponse { ContraVale = "b2", Importe = 456m }
                }
            };
            this.NuevoCliente = new Client.Models.NuevoCliente { Nombre = "nom", ApPaterno = "ap pa", ApMaterno = "ap ma" };
        }
    }
}
