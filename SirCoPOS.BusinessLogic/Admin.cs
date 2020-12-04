using SirCoPOS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.BusinessLogic
{
    public class Admin
    {
        public decimal? VentasEfectivo(string sucursal, byte num)
        {
            var ctxpos = new DataAccess.SirCoPOSDataContext();
            var ctxpv = new DataAccess.SirCoPVDataContext();
            var fondo = ctxpos.Fondos.Where(i => i.CajaSucursal == sucursal 
                && i.Tipo == Common.Constants.TipoFondo.Cajon
                && i.CajaNumero == num
                && !i.FechaCierre.HasValue).SingleOrDefault();
            if (fondo == null)
                return null;

            var fecha = fondo.FechaApertura;
            var last = fondo.Arqueos.OrderByDescending(i => i.Fecha).FirstOrDefault();
            if (last != null)
                fecha = last.Fecha;

            var q = from h in ctxpv.Pagos
                    where h.sucursal == fondo.CajaSucursal
                        && h.idcajero == fondo.ResponsableId
                        && h.estatus == "AP" && h.fum > fecha
                    from d in h.Detalle
                    where d.idformapago == (int)Common.Constants.FormaPago.EF
                    select d.importe;
            var sum = q.Sum();
            return sum;
        }
        public CorteResponse GetCorteCaja(string sucursal, int idcajero)
        {
            var ctxpv = new DataAccess.SirCoPVDataContext();
            var ctxpos = new DataAccess.SirCoPOSDataContext();

            var fondo = ctxpos.Fondos.Where(i => i.ResponsableId == idcajero && i.CajaSucursal == sucursal
                && !i.FechaCierre.HasValue).SingleOrDefault();
            if (fondo == null)
                return null;

            //var fecha = fondo.FechaApertura;
            //var fechaDocs = fecha;
            //var arqueo = fondo.Arqueos.OrderByDescending(i => i.Fecha).FirstOrDefault();
            //if (arqueo != null)
            //    fecha = arqueo.Fecha;

            //var q = from h in ctxpv.Pagos
            //        where h.sucursal == sucursal
            //            && h.idcajero == idcajero
            //            && h.estatus == "AP" && h.fum > fechaDocs
            //        from d in h.Detalle
            //        //where d.idformapago != (int)Common.Constants.FormaPago.EF
            //        group d by d.idformapago into g
            //        select new { 
            //            FormaPago = g.Key,
            //            Count = g.Count(),
            //            Total = g.Sum(i => i.importe)
            //        };

            var res = new CorteResponse();
            //res.FormaPagoTotales = q.Where(i => i.FormaPago != (int)Common.Constants.FormaPago.EF)
            //    .ToArray().Select(i => new Common.Entities.FormaPagoCorte 
            //{
            //    FormaPago = (Common.Constants.FormaPago)i.FormaPago,
            //    Count = i.Count,
            //    Total = i.Total
            //}).ToArray();

            //var ef = q.Where(i => i.FormaPago == (int)Common.Constants.FormaPago.EF).SingleOrDefault();
            //res.Ventas = ef?.Total ?? 0;
            //res.Caja = fondo.Disponible;

            //res.Importe = fondo.Disponible;
            res.Importe = fondo.Caja.Disponible;
            res.FormaPagoTotales = fondo.Caja.FormasPago.Where(i => i.Unidades > 0).Select(i => 
                new FormaPagoCorte { 
                    FormaPago = (Common.Constants.FormaPago)i.FormaPago, 
                    Count = i.Unidades, 
                    Total = i.Monto
                });

            //var qq = ctxpos.Cortes.Where(i => i.CajeroId == idcajero && i.Fecha > fondo.FechaApertura);
            //var formasPago = res.FormaPagoTotales.ToList();
            //foreach (var doc in qq)
            //{
            //    if(!ctxpos.Entry(doc).Collection(i => i.FormasPago).IsLoaded)
            //        ctxpos.Entry(doc).Collection(i => i.FormasPago).Load();
            //    foreach (var fp in doc.FormasPago)
            //    {
            //        var item = formasPago.Where(i => (int)i.FormaPago == fp.FormaPago).Single();
            //        item.Count -= fp.Entregado;
            //        item.Total -= fp.Monto;
            //        if (item.Count == 0)
            //        {
            //            formasPago.Remove(item);
            //        }
            //    }
            //}
            //res.FormaPagoTotales = formasPago;

            var ctx = new DataAccess.SirCoDataContext();
            var qs = ctx.Series.Where(i => i.status == "AB" && i.sucursal == sucursal);
            var list = new List<Common.Entities.SeriePrecio>();
            foreach (var det in qs)
            {
                var cor = ctx.GetCorrida(det);
                list.Add(new SeriePrecio { Serie = det.serie, Importe = cor.precio });
            }
            res.Series = list;
            return res;
        }

        //public decimal? GetEfectivoCaja(string sucursal, int idcajero)
        //{
        //    var ctx = new DataAccess.SirCoPVDataContext();

        //    //obtener ultima fecha o id de la ultima entrega
        //    var fecha = DateTime.Today.AddDays(-1);

        //    var q = from h in ctx.Pagos
        //            where h.sucursal == sucursal
        //                && h.idcajero == idcajero
        //                && h.estatus == "AP" && h.fum > fecha
        //            from d in h.Detalle
        //            where d.idformapago == (int)Common.Constants.FormaPago.EF
        //            select d.importe;
        //    var res = q.Sum();
        //    return res;
        //}
        public void IniciaFondo()
        {
            throw new NotImplementedException();
        }
        public void Transfer(int from, int to, decimal amount)
        {
            var now = Helpers.Common.GetNow();
            var ctx = new DataAccess.SirCoPOSDataContext();
            var gid = Guid.NewGuid();

            var cfrom = ctx.Fondos.Where(i => i.ResponsableId == from && i.Tipo != Common.Constants.TipoFondo.Cajon).Single();
            var cto = ctx.Fondos.Where(i => i.ResponsableId == to && i.Tipo != Common.Constants.TipoFondo.Cajon).Single();

            cfrom.Movimientos.Add(new DataAccess.SirCoPOS.FondoMovimiento 
            { 
                Entrada = false, 
                Fecha = now, 
                Importe = amount, 
                Referencia = gid, 
                UsuarioId = to
            });
            cfrom.Disponible -= amount;
            cto.Movimientos.Add(new DataAccess.SirCoPOS.FondoMovimiento
            {
                Entrada = true,
                Fecha = now,
                Importe = amount,
                Referencia = gid,
                UsuarioId = from
            });
            cto.Disponible += amount;
            ctx.SaveChanges();
        }
        public void Transfer(byte from, byte to, decimal amount, int userId)
        {
            var now = Helpers.Common.GetNow();
            var ctx = new DataAccess.SirCoPOSDataContext();
            var gid = Guid.NewGuid();

            var cfrom = ctx.Fondos.Where(i => i.ResponsableId == userId && i.Tipo == (Common.Constants.TipoFondo)from).Single();
            var cto = ctx.Fondos.Where(i => i.ResponsableId == userId && i.Tipo == (Common.Constants.TipoFondo)to).Single();

            cfrom.Movimientos.Add(new DataAccess.SirCoPOS.FondoMovimiento
            {
                Entrada = false,
                Fecha = now,
                Importe = amount,
                Referencia = gid,
                UsuarioId = userId
            });
            cfrom.Disponible -= amount;
            cto.Movimientos.Add(new DataAccess.SirCoPOS.FondoMovimiento
            {
                Entrada = true,
                Fecha = now,
                Importe = amount,
                Referencia = gid,
                UsuarioId = userId
            });
            cto.Disponible += amount;
            ctx.SaveChanges();
        }
        //public void GenerateCorte(Common.Entities.RequestCorte request)
        //{
        //    var now = Helpers.Common.GetNow();
        //    var ctx = new DataAccess.SirCoPOSDataContext();

        //    var corte = new DataAccess.SirCoPOS.Corte
        //    {
        //        CajeroId = request.CajeroId,
        //        Fecha = now,
        //        AuditorId = request.AuditorId,
        //        //public bool Arqueo { get; set; }
        //        Reportado = request.Reportado
        //    };
        //    corte.FormasPago = new HashSet<DataAccess.SirCoPOS.CorteFormaPago>();
        //    foreach (var item in request.FormasPago)
        //    {
        //        corte.FormasPago.Add(new DataAccess.SirCoPOS.CorteFormaPago
        //        {
        //            FormaPago = item.FormaPago,
        //            Entregado = item.Reportado
        //        });
        //    }
        //    corte.Series = new HashSet<DataAccess.SirCoPOS.CorteSerie>();
        //    foreach (var item in request.Series)
        //    {
        //        corte.Series.Add(new DataAccess.SirCoPOS.CorteSerie
        //        {
        //            Serie = item
        //        });
        //    }
        //    ctx.Cortes.Add(corte);
        //    ctx.SaveChanges();
        //}

        public void Sale(int idcajero, string sucursal, decimal importe, DateTime now, Guid gid, Common.Constants.FormaPago formaPago)
        {
            var ctx = new DataAccess.SirCoPOSDataContext();

            var fondo = ctx.Fondos.Where(i => i.ResponsableId == idcajero && !i.FechaCierre.HasValue).Single();
            if (formaPago == Common.Constants.FormaPago.EF)
            {
                fondo.Movimientos.Add(
                    new DataAccess.SirCoPOS.FondoMovimiento
                    {
                        Importe = importe,
                        UsuarioId = null,
                        Entrada = true,
                        Fecha = now,
                        Referencia = gid,
                        Tipo = "Venta"
                    });
                fondo.Disponible += importe;
                fondo.Caja.Disponible += importe;
            }
            else
            {
                var fp = fondo.Caja.FormasPago.Where(i => i.FormaPago == (int)formaPago).SingleOrDefault();
                if (fp == null)
                {
                    fp = new DataAccess.SirCoPOS.CajaFormaPago
                    {
                        FormaPago = (int)formaPago, 
                        Unidades = 0, 
                        Monto = 0
                    };
                    fondo.Caja.FormasPago.Add(fp);                    
                }
                fp.Unidades++;
                fp.Monto += importe;
                fondo.FormasPago.Add(new DataAccess.SirCoPOS.FondoFormaPago
                {
                    Entrada = true, 
                    Fecha = now, 
                    FormaPago = (int)formaPago, 
                    Monto = importe, 
                    Cantidad = 1, 
                    UsuarioId = null, 
                    Referencia = gid
                });
            }
            ctx.SaveChanges();            
        }
        public void Cancel(int idcajero, string sucursal, decimal importe, DateTime now, Guid gid, Common.Constants.FormaPago formaPago)
        {
            var ctx = new DataAccess.SirCoPOSDataContext();

            var fondo = ctx.Fondos.Where(i => i.ResponsableId == idcajero && !i.FechaCierre.HasValue).Single();
            if (formaPago == Common.Constants.FormaPago.EF)
            {
                fondo.Movimientos.Add(
                    new DataAccess.SirCoPOS.FondoMovimiento
                    {
                        Importe = importe,
                        UsuarioId = null,
                        Entrada = false,
                        Fecha = now,
                        Referencia = gid,
                        Tipo = "VentaCancela"
                    });
                fondo.Disponible -= importe;
                fondo.Caja.Disponible -= importe;
            }
            else
            {
                var fp = fondo.Caja.FormasPago.Where(i => i.FormaPago == (int)formaPago).SingleOrDefault();
                if (fp == null)
                {
                    fp = new DataAccess.SirCoPOS.CajaFormaPago
                    {
                        FormaPago = (int)formaPago,
                        Unidades = 0,
                        Monto = 0
                    };
                    fondo.Caja.FormasPago.Add(fp);
                }
                fp.Unidades--;
                fp.Monto -= importe;
                fondo.FormasPago.Add(new DataAccess.SirCoPOS.FondoFormaPago
                {
                    Entrada = false,
                    Fecha = now,
                    FormaPago = (int)formaPago,
                    Monto = importe,
                    Cantidad = 1,
                    UsuarioId = null,
                    Referencia = gid
                });
            }
            ctx.SaveChanges();            
        }
    }
}
