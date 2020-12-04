//using SirCoPOS.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SirCoPOS.WebServices
{
    public class Service //: Contracts.Services.IService
    {
        private readonly BusinessLogic.Sale _sale;
        private readonly BusinessLogic.Process _process;
        public Service()
        {
            _sale = new BusinessLogic.Sale();
            _process = new BusinessLogic.Process();
        }
        //public ScanResponse ScanProducto(string serie, string sucursal)
        //{
        //    var res = _sale.ScanProducto(serie, sucursal);
        //    if (res != null)
        //    {
        //        return new Contracts.Entities.ScanResponse
        //        {
        //            Id = res.Producto.Id.Value,
        //            Serie = res.Producto.Serie,
        //            Precio = res.Producto.Precio.Value,
        //            HasImage = res.Producto.HasImage,
        //            Status = res.Status.ToString()
        //        };
        //    }
        //    return null;
        //}
        //public SaleResponse Sale(SaleRequest request)
        //{
        //    foreach (var item in request.Series)
        //    {
        //        _process.RequestProducto(item, 0);
        //    }

        //    var srequest = new Common.Entities.SaleRequest
        //    {
        //        VendedorId = 0,
        //        Sucursal = request.Sucursal,
        //        Productos = request.Series.Select(i => new Common.Entities.SerieFormasPago
        //        {
        //            FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF },
        //            Serie = i
        //        }),
        //        Pagos = new Common.Entities.Pago[] {
        //            new Common.Entities.Pago
        //            {
        //                FormaPago = Common.Constants.FormaPago.EF,
        //                Importe = request.Pagar.Value
        //            }
        //        }
        //    };
        //    var res = _process.Sale(srequest, 0);
        //    if (res != null)
        //    {
        //        return new SaleResponse
        //        {
        //            Folio = res.Folio
        //        };
        //    }
        //    return null;
        //}
    }
}
