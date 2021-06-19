using SirCoPOS.Common.Constants;
using SirCoPOS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SirCoPOS.DataAccess.SirCoPV;
using SirCoPOS.DataAccess.SirCo;
using System.Globalization;
using SirCoPOS.BusinessLogic.Entities;
using System.Data.Entity;
using System.Runtime.InteropServices.WindowsRuntime;

namespace SirCoPOS.BusinessLogic
{
    public class Sale
    {
        public Sale()
        {

        }
        public ScanDevolucionResponse ScanProductoDevolucion(string serie, bool cancelacion = false)
        {
            var today = DateTime.Today;
            var max = today.AddDays(-15);
            var ctx = new DataAccess.SirCoDataContext();
            var ctxi = new DataAccess.SirCoImgDataContext();
            var ctxpv = new DataAccess.SirCoPVDataContext();
            var status = new string[] { Common.Constants.Status.ZC.ToString() };
            var q = ctx.Series.Include(i => i.Articulo).Where(i => i.serie == serie
                && i.status == Common.Constants.Status.BA.ToString());
            var item = q.SingleOrDefault();
            if (item != null)
            {
                var corrida = ctx.GetCorrida(item);
                var qimg = ctxi.Imagenes.Where(i => i.Marca == item.marca && i.Estilon == item.estilon);

                var vitem = ctxpv.VentasDetalle.Include(i => i.Header).Where(i => i.serie == serie && i.Header.estatus == "AP")
                    .OrderByDescending(i => i.fum).FirstOrDefault();

                if (vitem == null)
                    return null;

                if (cancelacion)
                {
                    if (vitem.Header.fecha != today)
                        return new ScanDevolucionResponse { Success = false };
                }
                else
                {
                    if (vitem.Header.fecha < max)
                        return new ScanDevolucionResponse
                        {
                            Success = false,
                            Status = (Status)Enum.Parse(typeof(Status), item.status)
                        };
                }

                var IdAgrupa = ctxpv.PromocionesAgrupaciones.Where(i => i.idpromocion == vitem.idpromocion).Select(i => i.idagrupacionpromo).SingleOrDefault();
                return new ScanDevolucionResponse
                {
                    Success = true,
                    Status = (Status)Enum.Parse(typeof(Status), item.status),
                    Producto = new ProductoDevolucion
                    {
                        Sucursal = vitem.Header.sucursal,
                        Folio = vitem.Header.venta,
                        Precio = vitem.precio,
                        Pago = vitem.precdesc,
                        Id = item.ArticuloId,
                        Marca = item.marca,
                        Modelo = item.estilon,
                        //Precio = corrida.precio,
                        Serie = item.serie,
                        Talla = item.medida,
                        //Total = corrida.precio,
                        //HasImage = qimg.Any()
                        Corrida = corrida.corrida,
                        IdPromocion = vitem.idpromocion,
                        IdAgrupacion = IdAgrupa
                    }
                };
            }
            return null;
        }
        public ScanDevolucionResponse ScanProductoFromDevolucion(string serie)
        {
            var today = DateTime.Today;
            var max = today.AddDays(-15);
            var ctx = new DataAccess.SirCoDataContext();
            var ctxi = new DataAccess.SirCoImgDataContext();
            var ctxpv = new DataAccess.SirCoPVDataContext();
            //var status = new string[] { 
            //    Common.Constants.Status.AC.ToString()                
            //};
            var q = ctx.Series.Include(i => i.Articulo).Where(i => i.serie == serie
                && i.status == Common.Constants.Status.AC.ToString());
            var item = q.SingleOrDefault();
            if (item != null)
            {
                var corrida = ctx.GetCorrida(item);
                var qimg = ctxi.Imagenes.Where(i => i.Marca == item.marca && i.Estilon == item.estilon);

                var vitem = ctxpv.DevolucionesDetalle.Include(i => i.Header).Where(i => i.serie == serie && i.Header.estatus == "AP")
                    .OrderByDescending(i => i.fum).FirstOrDefault();

                if (vitem == null)
                    return null;

                //if (cancelacion)
                {
                    if (vitem.Header.fecha != today)
                        return new ScanDevolucionResponse { Success = false };
                }
                //else
                //{
                //    if (vitem.Header.fecha < max)
                //        return new ScanDevolucionResponse { Success = false };
                //}

                return new ScanDevolucionResponse
                {
                    Success = true,
                    Status = (Status)Enum.Parse(typeof(Status), item.status),
                    Producto = new ProductoDevolucion
                    {
                        Sucursal = vitem.Header.sucursal,
                        Folio = vitem.Header.devolvta,
                        Precio = vitem.precio,
                        Pago = vitem.precdesc,
                        Id = item.ArticuloId,
                        Marca = item.marca,
                        Modelo = item.estilon,
                        //Precio = corrida.precio,
                        Serie = item.serie,
                        Talla = item.medida,
                        //Total = corrida.precio,
                        //HasImage = qimg.Any()
                        Corrida = corrida.corrida
                    }
                };
            }
            return null;
        }
        public ScanResponse ScanProducto(string serie, string sucursal)
        {
            if (string.IsNullOrWhiteSpace(serie))
                return null;

            var now = DateTime.Now;

            //if (serie != null)
            //    throw new CustomException("scan invalido");

            var ctx = new DataAccess.SirCoDataContext();
            var ctxi = new DataAccess.SirCoImgDataContext();
            var status = new string[] { Common.Constants.Status.ZC.ToString() };
            var q = ctx.Series.Where(i => i.serie == serie
                && !status.Contains(i.status));
            var item = q.Where(i => i.sucursal == sucursal).SingleOrDefault();
            if (item == null)
            {
                var ctxc = new DataAccess.SirCoControlDataContext();
                var sucs = ctxc.Sucursales.Where(i => i.ordenweb.HasValue)
                    .Select(i => i.sucursal).ToArray();
                item = q.Where(i => sucs.Contains(i.sucursal)).SingleOrDefault();
            }
            if (item != null)
            {
                if (item.ArticuloId > 0)
                {
                    var isElectronica = item.Articulo.iddivisiones == (int)Divisiones.Electronica;
                    var isAccesorio   = item.Articulo.iddivisiones == (int)Divisiones.Accesorio;
                    var isParUnico = this.IsParUnico(item.Articulo.idagrupacion);
                    var corrida = ctx.GetCorrida(item);
                    if (corrida == null)
                        return null;
                    var qimg = ctxi.Imagenes.Where(i => i.Marca == item.marca && i.Estilon == item.estilon);

                    byte? maxPlazos = null;
                    if (isElectronica)
                    {
                        var com = new BusinessLogic.Data();
                        maxPlazos = com.GetParametro<byte?>($"{corrida.marca}{corrida.estilon.Replace(' ', '_')}", sucursal);
                        if (!maxPlazos.HasValue)
                        {
                            maxPlazos = com.GetParametro<byte?>(Common.Constants.Parametros.ELECTRONICA);
                        }
                    }

                    var res = new ScanResponse
                    {
                        Status = (Status)Enum.Parse(typeof(Status), item.status),
                        UsuarioCajaId = item.idusuariocaja,
                        Producto = new Producto
                        {
                            Id = item.ArticuloId,
                            Corrida = corrida.corrida,
                            Marca = item.marca,
                            Modelo = item.estilon,
                            Precio = corrida.precio,
                            Serie = item.serie,
                            Talla = item.medida,
                            Total = corrida.precio,
                            HasImage = qimg.Any(),
                            Electronica = isElectronica,
                            Accesorio = isAccesorio,
                            ParUnico = isParUnico,
                            MaxPlazos = maxPlazos,
                            Sucursal = item.sucursal
                        }
                    };
                    if (res.Status == Status.CA && item.fechacaja.HasValue)
                    {
                        var dif = now - item.fechacaja.Value;
                        if (dif.TotalMinutes > 15)
                        {
                            res.Status = Status.AC;
                            res.UsuarioCajaId = null;
                        }
                    }
                    return res;
                }
                else
                {
                    return new ScanResponse
                    {
                        Status = Status.NA,
                        Producto = new Producto
                        {
                            Id = item.ArticuloId,
                            //Corrida = corrida.corrida,
                            Marca = item.marca,
                            Modelo = item.estilon,
                            //Precio = corrida.precio,
                            Serie = item.serie,
                            Talla = item.medida,
                            //Total = corrida.precio,
                            //HasImage = qimg.Any(),
                            //Electronica = isElectronica,
                            //ParUnico = isParUnico,
                            //MaxPlazos = maxPlazos
                        }
                    };
                }
            }
            return null;
        }

        public bool IsParUnico(int idagrupacion)
        {
            var unicos = new int[] { 108, 109, 110, 103 };
            return unicos.Contains(idagrupacion);
        }

        public DevolucionResponse FindSale(string sucursal, string folio)
        {
            var ctx = new DataAccess.SirCoDataContext();
            var ctxpv = new DataAccess.SirCoPVDataContext();
            var ctxi = new DataAccess.SirCoImgDataContext();
            var ctxc = new DataAccess.SirCoCreditoDataContext();
            var item = ctxpv.Ventas.Where(i => i.sucursal == sucursal && i.venta == folio).SingleOrDefault();
            if (item == null)
                return null;
            
            var model = new DevolucionResponse
            {
                Sucursal = item.sucursal,
                Folio = item.venta,
                CajeroId = item.idcajero,
                VendedorId = item.idvendedor,
                ClienteId = item.idcliente,
                Productos = item.Detalles.Select(i => new ProductoSale
                {
                    //Id
                    Serie = i.serie,
                    Marca = i.marca,
                    Modelo = i.estilon,
                    Talla = i.medida,
                    Precio = i.precio,
                    //Descuento
                    //Total
                    Renglon = i.renglon,

                    Corrida = i.corrida,
                    precdesc = i.precdesc,
                    costomargen = i.costomargen,
                    iva = i.iva,
                    ctd = i.ctd,
                    idpromocion = i.idpromocion
                }).ToArray()
            };
            foreach (var p in model.Productos)
            {
                var ser = ctx.Series.Where(i => i.serie == p.Serie).Single();
                var corrida = ctx.Corridas.Where(i => i.marca == p.Marca && i.estilon == p.Modelo && i.proveedor == ser.proveedors
                    && String.Compare(p.Talla, i.medini) >= 0 && String.Compare(p.Talla, i.medfin) <= 0).SingleOrDefault();
                var qimg = ctxi.Imagenes.Where(i => i.Marca == p.Marca && i.Estilon == p.Modelo);
                p.Id = corrida?.ArticuloId;
                p.HasImage = qimg.Any();
                p.status = ser.status;
                p.idagrupacion = ctxpv.PromocionesAgrupaciones.Where(i => i.idpromocion == p.idpromocion).Select(i=>i.idagrupacionpromo).SingleOrDefault();
            }
            return model;
        }

        //========================= CREO QUE SE TENDRÁ QUE CAMBIAR EL METODO ENTERO ===================================================================
        //ALGO ESTA MAL AQUI, PORQUE EN LA CLASE DE "Process.cs"  SI HACE EL PROCESO (LITERALMENTE ES EL MISMO CODIGO)
        private IEnumerable<Entities.ProductoPromocion> ParseProductos(IEnumerable<Common.Entities.SerieFormasPago> series)
        {
            var ctx = new DataAccess.SirCoDataContext();
            var items = new List<Entities.ProductoPromocion>();
            var count = 0;
            foreach (var sf in series)
            {
                if (sf.Serie != null)
                {
                    //AQUI ES DONDE SURGE EL ERROR DE ACCESO REMOTO
                    var serie = ctx.Series.Include(i => i.Articulo).Where(i => i.serie == sf.Serie).Single();
                    //=============================================
                    var corrida = ctx.GetCorrida(serie);
                    ctx = new DataAccess.SirCoDataContext();
                    items.Add(new Entities.ProductoPromocion
                    {
                        Order = count++,
                        Key = sf.Serie,
                        SerieFormaPago = sf,

                        Serie = serie,
                        Corrida = corrida,
                        AgrupacionId = corrida.Articulo.idagrupacion
                    });
                }
                else
                {
                    var corrida = ctx.Corridas.Where(i => i.ArticuloId == sf.ArticuloId)
                        .OrderByDescending(i => i.ult_vta)
                        .First();
                    ctx = new DataAccess.SirCoDataContext();
                    items.Add(new Entities.ProductoPromocion
                    {
                        Order = count++,
                        Key = sf.Serie,
                        SerieFormaPago = sf,
                        Serie = null,
                        Corrida = corrida,
                        AgrupacionId = corrida.Articulo.idagrupacion
                    });
                }
            }
            return items;
        }
        //===========================================================================================================================================
        public string prueba()
        {
            var now = DateTime.Now;
            var ctx = new DataAccess.SirCoDataContext();
            var item = ctx.Series.Where(i => i.serie == "0000003524047").Single();
            var valid = new string[] {
                Status.AC.ToString(),
                Status.IF.ToString(),
                Status.AB.ToString()
            };
            return "";
        }
        //===========================================================================================================================================
        public IEnumerable<Common.Entities.Promocion> GetPromociones(CheckPromocionesRequest request)
        {
            var today = DateTime.Today;
            var ctxc = new DataAccess.SirCoControlDataContext();
            var ctxpv = new DataAccess.SirCoPVDataContext();

            var items = ParseProductos(request.Productos);

            var suc = ctxc.Sucursales.Where(i => i.sucursal == request.Sucursal).Single();
            var q = ctxpv.Promociones.Where(i => i.estatus == "ACTIVO"
                    && today >= i.iniciopromo
                    && today <= i.finpromo
                    && !i.Cupones.Any())
                .Select(i => new {
                    promo = i,
                    desc = i.Detalle.Max(k => k.descdirecto),
                    hasFijo = i.Detalle.Where(k => k.impfijo > 0).Any(),
                    fijo = i.Detalle.Where(k => k.impfijo > 0).Min(k => k.impfijo),
                    descFijo = i.Detalle.Max(k => k.descfijo),
                    porcDin = i.Detalle.Max(k => k.porcdinelec),
                    bono = i.Detalle.Max(k => k.impbono)
                })
                .OrderByDescending(i => i.hasFijo)
                .ThenBy(i => i.fijo)
                .ThenByDescending(i => i.desc)
                .ThenByDescending(i => i.descFijo)
                .ThenByDescending(i => i.porcDin)
                .ThenByDescending(i => i.bono);

            var res = new List<Common.Entities.Promocion>();
            foreach (var item in q)
            {
                var valid = IsValidPromocion(item.promo, suc, items, any: true);
                if (valid.IsValid)
                    res.Add(new Promocion
                    {
                        PromocionId = item.promo.idpromocion,
                        Nombre = item.promo.nombre,
                        HasCliente = item.promo.clienterequerido ?? false
                    });
            }
            return res;
        }

        private Entities.ValidPromocionResponse IsValidPromocion(Promociones promocion, DataAccess.SirCoControl.Sucursal suc,
            IEnumerable<Entities.ProductoPromocion> items, IDictionary<string, Common.Entities.ProductoPromocion> dic = null, bool any = false)
        {
            var today = Helpers.Common.GetToday();
            var now = Helpers.Common.GetNow();

            if (promocion.estatus != "ACTIVO" || !(today >= promocion.iniciopromo && today <= promocion.finpromo))
                return new Entities.ValidPromocionResponse { IsValid = false };

            if (promocion.Plazas.Any())
            {
                var q = promocion.Plazas.ToArray()
                    .Where(i =>
                        i.plaza == suc.Plaza.plaza
                        && (String.IsNullOrEmpty(i.sucursal) || i.sucursal == suc.sucursal));
                if (!q.Any())
                    return new Entities.ValidPromocionResponse { IsValid = false };
            }

            if (promocion.Recurrencia.Any())
            {
                var cul = new CultureInfo("es-MX");
                var dia = today.ToString("dddd", cul);
                var q = promocion.Recurrencia.Where(i => i.dia.ToLower() == dia);
                var valid = false;
                var format = @"hh\:mm";
                foreach (var item in q)
                {
                    var hi = TimeSpan.ParseExact(item.horainicio, format, cul);
                    var hf = TimeSpan.ParseExact(item.horafin, format, cul);
                    var di = today.Add(hi);
                    var df = today.Add(hf);
                    if (now >= di && now <= df)
                    {
                        valid = true;
                        break;
                    }
                }
                if (!valid)
                    return new Entities.ValidPromocionResponse { IsValid = false };
            }

            var itemsPromo = items.ToList();
            var itemsCompra = items.ToList();
            var validPromo = new List<Entities.ProductoPromocion>();
            var validCompra = new List<Entities.ProductoPromocion>();

            foreach (var item in promocion.Agrupaciones)
            {
                if (item.idagrupacionpromo > 0)
                    ChecarAgrupacion(item.AgrupacionPromo, itemsPromo, validPromo, dic);
                if (item.idagrupacioncompra > 0)
                    ChecarAgrupacion(item.AgrupacionCompra, itemsCompra, validCompra, dic);
            }

            if (promocion.paresunicos != "SI")
            {
                RemoverParesUnicos(itemsPromo, validPromo);
                RemoverParesUnicos(itemsCompra, validCompra);
            }

            foreach (var item in promocion.Exclusiones)
            {
                ChecarExclusion(item, itemsPromo, validPromo);
                ChecarExclusion(item, itemsCompra, validCompra);
            }

            if (any)
            {
                if (promocion.tipo == Common.Constants.TipoPromocion.DIRECTA)
                {
                    if (validPromo.Any())
                        return new Entities.ValidPromocionResponse { IsValid = true };
                }
                if (promocion.tipo == Common.Constants.TipoPromocion.AxB)
                {
                    if (validCompra.Any() || validPromo.Any())
                        return new Entities.ValidPromocionResponse { IsValid = true };
                }
                return new Entities.ValidPromocionResponse { IsValid = false };
            }

            if (promocion.tipo == Common.Constants.TipoPromocion.DIRECTA)
            {
                if (validPromo.Count < promocion.minunicompra)
                    return new Entities.ValidPromocionResponse { IsValid = false };
            }
            if (promocion.tipo == Common.Constants.TipoPromocion.AxB)
            {
                if (validCompra.Count < promocion.minunicompra)
                    return new Entities.ValidPromocionResponse { IsValid = false };
            }

            if ((promocion.minimpcompra ?? 0) > 0)
            {
                if (promocion.tipo == Common.Constants.TipoPromocion.DIRECTA)
                {
                    var total = validPromo.Sum(i => i.SerieFormaPago.Precio ?? i.Corrida.precio);
                    if (total < promocion.minimpcompra)
                        return new Entities.ValidPromocionResponse { IsValid = false };
                }
                if (promocion.tipo == Common.Constants.TipoPromocion.AxB)
                {
                    var total = validCompra.Sum(i => i.SerieFormaPago.Precio ?? i.Corrida.precio);
                    if (total < promocion.minimpcompra)
                        return new Entities.ValidPromocionResponse { IsValid = false };
                }
            }

            return new Entities.ValidPromocionResponse
            {
                IsValid = true,
                ValidPromo = validPromo,
                ValidCompra = validCompra
            };
        }

        public CheckPromocionesCuponesResponse CheckPromociones(CheckPromocionesCuponesRequest request)
        {
            var today = Helpers.Common.GetToday();
            var now = Helpers.Common.GetNow();
            var ctx = new DataAccess.SirCoDataContext();
            var ctxc = new DataAccess.SirCoControlDataContext();

            //var ite = prueba(request.Productos);
            var items = ParseProductos(request.Productos);
            var mapping = this.GetFormaPagoMapping();

            var dic = new Dictionary<string, Common.Entities.ProductoPromocion>();
            //var dser = new Dictionary<string, DataAccess.SirCo.Serie>();
            //var dcor = new Dictionary<string, DataAccess.SirCo.Corrida>();
            var dpromos = new Dictionary<int, DataAccess.SirCoPV.Promociones>();
            var dcupones = new Dictionary<int, DataAccess.SirCoPV.Cupones>();
            foreach (var sf in request.Productos)
            {
                //    var serie = ctx.Series.Where(i => i.serie == sf.Serie).Single();
                //    var corrida = ctx.Corridas.Where(i => i.marca == serie.marca && i.estilon == serie.estilon && i.proveedor == serie.proveedors
                //        && String.Compare(serie.medida, i.medini) >= 0 && String.Compare(serie.medida, i.medfin) <= 0).SingleOrDefault();

                dic.Add(sf.Serie, new Common.Entities.ProductoPromocion { Serie = sf.Serie });
                //    dser.Add(sf.Serie, serie);
                //    dcor.Add(sf.Serie, corrida);
            }

            var suc = ctxc.Sucursales.Where(i => i.sucursal == request.Sucursal).Single();
            var ctxpv = new DataAccess.SirCoPVDataContext();
            var promos = new HashSet<int>();
            foreach (var pcup in request.PromocionesCupones)
            {
                if (pcup.Cupon == null)
                {
                    promos.Add(pcup.PromocionId);
                }
                else
                {
                    var cupon = ctxpv.CuponesDetalle.Where(i => i.folio == pcup.Cupon).SingleOrDefault();
                    if (dcupones.ContainsKey(cupon.Cupon.idcupon))
                        continue;
                    dcupones.Add(cupon.Cupon.idcupon, cupon.Cupon);
                    if (cupon == null || cupon.estatus != "ACTIVO")
                        continue;
                    if (cupon.Cupon.estatus != "ACTIVO" || !(today >= cupon.Cupon.fecini && today <= cupon.Cupon.fecfin))
                        continue;
                    if (cupon.Cupon.tipo != "MULTIPLE")//UNICO
                    {
                        //TODO pendiente validar si ya fue usado
                        //continue;
                    }

                    foreach (var prom in cupon.Cupon.PromocionCupon)
                    {
                        promos.Add(prom.idpromocion);
                    }
                }
            }
            var ctxn = new DataAccess.SirCoNominaDataContext();
            decimal monedero = 0;
            foreach (var promo in promos)
            {
                var promocion = ctxpv.Promociones.Where(i => i.idpromocion == promo).Single();
                if (dpromos.ContainsKey(promocion.idpromocion))
                {
                    if (promocion.acumulable == "NO")
                        continue;
                }
                else
                    dpromos.Add(promocion.idpromocion, promocion);

                var duplicados = promocion.duplicados;

                if ((promocion.clienterequerido ?? false)
                    && !(promocion.empleadorequerido ?? false))
                {
                    if (!request.HasCliente)
                        continue;
                    if (request.ClienteId.HasValue && duplicados.HasValue)
                    {
                        //TODO agregar indice de promocion
                        var q = ctxpv.VentasDetalle.Where(i => i.Header.idcliente == request.ClienteId
                            && i.idpromocion == promo);

                        var usados = q.Count();
                        var dup = duplicados.Value - usados;
                        if (dup <= 0)
                            continue;
                        duplicados = (byte)dup;
                    }
                }
                if (promocion.empleadorequerido ?? false)
                {
                    if (!request.HasCliente)
                        continue;
                    if (request.ClienteId.HasValue)
                    {
                        var qemp = ctxn.Empleados.Where(i => i.idcliente == request.ClienteId.Value
                                && i.estatus == "A");
                        if (!qemp.Any())
                            continue;
                    }
                    if (duplicados.HasValue)
                    {
                        DateTime? fecha = null;
                        switch (promocion.empleadocantidadtipo)
                        {
                            case "D":
                                fecha = now.AddDays(-promocion.empleadocantidad.Value);
                                break;
                            case "M":
                                fecha = now.AddMonths(-promocion.empleadocantidad.Value);
                                break;
                            case "MC":
                                fecha = new DateTime(now.Year, now.Month, 1);
                                break;
                            default:
                                continue;
                        }

                        //TODO agregar indice de promocion                        
                        var q = ctxpv.VentasDetalle.Where(i => i.Header.idcliente == request.ClienteId
                            && i.idpromocion == promo
                            && i.Header.estatus != Common.Constants.StatusVenta.Cancelada);

                        if (fecha.HasValue)
                            q = q.Where(i => i.Header.fecha >= fecha);

                        var usados = q.Count();
                        var dup = duplicados.Value - usados;
                        if (dup <= 0)
                            continue;
                        duplicados = (byte)dup;
                    }
                }

                var index = 0;
                var used = false;
                do
                {
                    used = ChecarPromocion(
                        request,
                        promocion,
                        dic,
                        items,
                        suc,
                        index, count: null, mapping: mapping);
                    if (used)
                        index++;

                    if (duplicados.HasValue && duplicados == index)
                        break;
                } while (used && !(promocion.importeticket ?? false));

                if (index > 1 && promocion.tipo == Common.Constants.TipoPromocion.AxB)
                {
                    //clear promo
                    foreach (var key in dic.Where(i => i.Value.PromocionId == promocion.idpromocion).Select(i => i.Key))
                    {
                        dic[key].Promo = null;
                        dic[key].Fijo = null;
                        dic[key].Descuento = null;
                        dic[key].PromocionId = null;
                        dic[key].Monedero = null;
                        dic[key].Index = null;
                    }

                    ChecarPromocion(
                        request,
                        promocion,
                        dic,
                        items,
                        suc,
                        null, count: index, mapping: mapping);
                }
            }

            monedero = dic.Where(i => i.Value.Monedero.HasValue)
                .Sum(i => i.Value.Monedero.Value);

            //var q = ctxpv.Promociones.Where(i => i.estatus == "ACTIVO"
            //    && today >= i.iniciopromo
            //    && today <= i.finpromo
            //    && !i.Cupones.Any());


            //Assert.IsNotNull(cupon);
            //Assert.AreEqual("ACTIVO", cupon.estatus);

            //var q = cupon.Cupon.PromocionCupon;
            //var promocion = q.Single().Promocion;

            //Assert.AreEqual("DIRECTA", promocion.tipo);
            //var det = promocion.Detalle.Single();
            //Assert.AreEqual("TO", det.formapago);

            //foreach (var agr in promocion.Agrupaciones.Select(i => i.AgrupacionPromo))
            //{
            //    foreach (var s in series)
            //    {                    
            //        foreach (var adet in agr.Detalle)
            //        {
            //            var corrida = dcor[s];
            //            switch (adet.nivel)
            //            {
            //                case "Marca":
            //                    {
            //                        if (corrida.marca == adet.marca)
            //                        {
            //                            var desc = corrida.precio * det.descdirecto / 100;
            //                            if (!dic[s].HasValue)
            //                                dic[s] = desc;
            //                            else if (desc > dic[s])
            //                                dic[s] = desc;
            //                        }
            //                    }
            //                    break;
            //                case "Departamento": 
            //                case "División":
            //                case "L1":
            //                case "Linea":                            
            //                default:
            //                    throw new NotSupportedException();
            //            }
            //        }
            //    }
            //}

            return new CheckPromocionesCuponesResponse
            {
                Monedero = monedero,
                Promociones = dic.Select(i => i.Value)
            };
        }

        private decimal? getAmount(Entities.ProductoPromocion pp, string[] tipos,
            IDictionary<Common.Constants.FormaPago, string> mapping)
        {
            if (pp.SerieFormaPago.Pagos == null)
                return null;

            var qtmp = pp.SerieFormaPago.Pagos
                    .Where(i => mapping.ContainsKey(i.Key)
                        && tipos.Contains(mapping[i.Key]));
            //return qtmp.sum
            return qtmp.Sum(i => i.Value);
        }

        private decimal? GetTotalValido(Promociones promocion, string tipo,
            IDictionary<Common.Constants.FormaPago, string> mapping,
            out string[] tipos,
            List<Entities.ProductoPromocion> validItems)
        {
            var qt = promocion.Detalle.Where(i => i.tipo == tipo && i.formapago != FormaPagoPromocion.TO);
            tipos = qt.Select(i => i.formapago).ToArray();
            var hasTipoPago = qt.Any();

            var total = validItems.Sum(i => i.SerieFormaPago.Precio ?? i.Corrida.precio);
            if (hasTipoPago)
            {
                decimal? tot = 0m;
                foreach (var item in validItems)
                {
                    tot += getAmount(item, tipos, mapping) ?? 0;
                }
                total = tot;
            }
            return total;
        }

        private bool ChecarPromocion(
            CheckPromocionesRequest request,
            Promociones promocion,
            Dictionary<string, Common.Entities.ProductoPromocion> dic,
            IEnumerable<Entities.ProductoPromocion> items,
            DataAccess.SirCoControl.Sucursal suc, int? index, int? count,
            IDictionary<Common.Constants.FormaPago, string> mapping)
        {
            var today = Helpers.Common.GetToday();
            var now = Helpers.Common.GetNow();
            //decimal? monedero = 0;            

            var valid = IsValidPromocion(promocion, suc, items, dic);
            if (!valid.IsValid)
                return false;

            //var qdic = items.Where(i => !dic[i.SerieFormaPago.Serie].PromocionId.HasValue);
            //var itemsPromo = qdic.ToList();
            //var itemsCompra = qdic.ToList();

            //var validPromo = valid.ValidPromo;
            //var validCompra = valid.ValidCompra;

            int? maxItems = null;
            if ((promocion.minunicompra ?? 0) > 0)
            {
                if (promocion.tipo == Common.Constants.TipoPromocion.DIRECTA)
                {
                    maxItems = promocion.minunicompra.Value;
                }
            }

            if (promocion.tipo == Common.Constants.TipoPromocion.DIRECTA)
            {
                //int? mul = null;
                if ((promocion.minimpcompra ?? 0) > 0 && (promocion.importeticket ?? false))
                {
                    //string[] tipos;
                    //var total = GetTotalValido(promocion, tipo: "PROMO", mapping: mapping, tipos: out tipos, validItems: valid.ValidPromo);
                    //if (total < promocion.minimpcompra.Value)
                    //    return false;
                    //mul = (int)(total.Value / promocion.minimpcompra.Value);
                    foreach (var item in valid.ValidPromo)
                    {
                        item.CustomOrder = 0;
                        item.Group = 0;
                    }
                }
                else if ((promocion.minimpcompra ?? 0) > 0 && (promocion.minunicompra ?? 0) > 0)
                {
                    AgruparUnidadesImporte(valid.ValidPromo, promocion.minimpcompra.Value, promocion.minunicompra.Value, promocion.tipo);
                    if (!valid.ValidPromo.Where(i => i.CustomOrder.HasValue).Any())
                        return false;
                }
                else if ((promocion.minimpcompra ?? 0) > 0)
                {
                    AgruparUnidadesImporte(valid.ValidPromo, promocion.minimpcompra.Value, 0, promocion.tipo);
                    if (!valid.ValidPromo.Where(i => i.CustomOrder.HasValue).Any())
                        return false;
                }
                if (valid.ValidPromo.Where(i => i.CustomOrder.HasValue).Any())
                {
                    var remove = valid.ValidPromo.Where(i => !i.Group.HasValue).ToList();
                    remove.ForEach(i => valid.ValidPromo.Remove(i));
                }
                var isValid = AplicaPromocionDirecta(promocion, dic, maxItems, valid.ValidPromo, index, mapping: mapping);

                /*if (isValid && (promocion.importeticket ?? false) && promocion.minimpcompra.HasValue)
                {
                    bool take = false;
                    do
                    {
                        //if (take)
                        //{
                        //    var item = tmp.First();
                        //    valid.ValidCompra.Add(item);
                        //    tmp.Remove(item);
                        //    AplicaPromocionAxB_Compra(promocion, dic, valid.ValidCompra, valid.ValidPromo, index: ++lindex, mapping: mapping);
                        //}
                        take = false;
                        decimal? tot = 0;
                        foreach (var com in valid.ValidPromo)
                        {
                            var item = dic[com.Serie.serie];
                            tot += (com.SerieFormaPago.Precio ?? com.Corrida.precio) - (item.Descuento ?? 0);
                        }

                        if (tot < (promocion.minimpcompra.Value * mul.Value))
                        {
                            //if (tmp.Any())
                            //{
                            //    take = true;
                            //}
                            //else
                            {
                                isValid = false;
                                if ((count ?? 0) > 0)
                                    index = count - 1;
                            }
                        }
                    } while (take);

                    if (!isValid)
                    {
                        foreach (var key in dic.Where(i => i.Value.PromocionId == promocion.idpromocion && i.Value.Index == index).Select(i => i.Key))
                        {
                            dic[key].Promo = null;
                            dic[key].Fijo = null;
                            dic[key].Descuento = null;
                            dic[key].PromocionId = null;
                            dic[key].Monedero = null;
                            dic[key].Index = null;
                        }
                    }
                }*/
                return isValid;
            }
            if (promocion.tipo == Common.Constants.TipoPromocion.AxB)
            {
                var tmp = new List<Entities.ProductoPromocion>();
                if ((promocion.minimpcompra ?? 0) > 0)
                {
                    if (!(promocion.importeticket ?? false))
                    {
                        AgruparUnidadesImporte(valid.ValidCompra, promocion.minimpcompra.Value, promocion.minunicompra.Value, promocion.tipo);
                        if (!valid.ValidCompra.Where(i => i.CustomOrder.HasValue).Any())
                            return false;
                    }
                    else
                    {
                        string[] tipos;
                        var total = GetTotalValido(promocion, tipo: "COMPRA", mapping: mapping, tipos: out tipos, validItems: valid.ValidCompra);
                        count = (int)(total.Value / promocion.minimpcompra.Value);
                        var exp = promocion.minimpcompra.Value * count;
                        if (exp > 0)
                        {
                            var qlist = valid.ValidCompra
                                .OrderByDescending(i => i.CustomOrder.HasValue)
                                .ThenBy(i => i.CustomOrder)
                                .ThenByDescending(i => i.Corrida.precio)
                                .ThenBy(i => i.Order)
                                .ToList();
                            decimal sum = 0;
                            var reached = false;
                            foreach (var item in qlist)
                            {
                                if (!reached)
                                {
                                    if (!tipos.Any())
                                        sum += item.Corrida.precio.Value;
                                    else
                                        sum += getAmount(item, tipos, mapping) ?? 0;
                                }
                                else
                                {
                                    valid.ValidCompra.Remove(item);
                                    tmp.Add(item);
                                }
                                if (sum >= exp)
                                    reached = true;
                            }
                        }
                        else
                            return false;
                    }
                }

                var isValid = false;
                int mul;
                var lindex = 0;
                if ((count ?? 0) > 0)
                {
                    AplicaPromocionAxB_Promo(promocion, dic, valid.ValidCompra, valid.ValidPromo, index: null, count: count.Value, mapping: mapping);
                    for (int i = 0; i < count; i++)
                    {
                        AplicaPromocionAxB_Compra(promocion, dic, valid.ValidCompra, valid.ValidPromo, index: i, mapping: mapping);
                        lindex = i;
                    }
                    isValid = true;
                    mul = count.Value;
                }
                else
                {
                    var vpromo = AplicaPromocionAxB_Promo(promocion, dic, valid.ValidCompra, valid.ValidPromo, index, mapping: mapping);
                    var vcompra = AplicaPromocionAxB_Compra(promocion, dic, valid.ValidCompra, valid.ValidPromo, index, mapping: mapping);
                    if (!vcompra || !vpromo)
                        AplicaPromocionAxB_Clear(promocion, dic, index);
                    isValid = vcompra && vpromo;
                    mul = index ?? 1;
                }

                if (isValid && (promocion.importeticket ?? false) && promocion.minimpcompra.HasValue)
                {
                    bool take = false;
                    do
                    {
                        if (take)
                        {
                            var item = tmp.First();
                            valid.ValidCompra.Add(item);
                            tmp.Remove(item);
                            AplicaPromocionAxB_Compra(promocion, dic, valid.ValidCompra, valid.ValidPromo, index: ++lindex, mapping: mapping);
                        }
                        take = false;
                        decimal? tot = 0;
                        foreach (var com in valid.ValidCompra)
                        {
                            var item = dic[com.Serie.serie];
                            tot += (com.SerieFormaPago.Precio ?? com.Corrida.precio) - (item.Descuento ?? 0);
                        }

                        if (tot < (promocion.minimpcompra.Value * mul))
                        {
                            if (tmp.Any())
                            {
                                take = true;
                            }
                            else
                            {
                                isValid = false;
                                if ((count ?? 0) > 0)
                                    index = count - 1;
                            }
                        }
                    } while (take);

                    if (!isValid)
                    {
                        foreach (var key in dic.Where(i => i.Value.PromocionId == promocion.idpromocion && i.Value.Index == index).Select(i => i.Key))
                        {
                            dic[key].Promo = null;
                            dic[key].Fijo = null;
                            dic[key].Descuento = null;
                            dic[key].PromocionId = null;
                            dic[key].Monedero = null;
                            dic[key].Index = null;
                        }
                    }
                }
                return isValid;
            }

            return false;

            //if (promocion.tipo == Common.Constants.TipoPromocion.AxB)
            //{
            //    foreach (var pitem in validCompra)
            //    {
            //        var item = pitem.SerieFormaPago;

            //        if (dic[item.Serie].PromocionId.HasValue)
            //            continue;

            //        var promCompra = CheckFormasPago(item, promocion, "COMPRA");
            //        //var promPromo = CheckFormasPago(item, promocion, "PROMO");

            //        //if (!promCompra.DescuentoPorcentaje.HasValue && !promCompra.DescuentoFijo.HasValue
            //        //    /*&& !promPromo.DescuentoPorcentaje.HasValue && !promPromo.DescuentoFijo.HasValue*/)
            //        //    continue;


            //        if (promocion.tipo == Common.Constants.TipoPromocion.AxB)
            //        {
            //            //if ((promPromo.DescuentoPorcentaje ?? 0) > 0 || (promPromo.DescuentoFijo ?? 0) > 0)
            //            {
            //                var desc = pitem.Corrida.precio * promCompra.DescuentoPorcentaje;
            //                if (!dic[pitem.Key].PromocionId.HasValue)
            //                {
            //                    dic[pitem.Key].Fijo = promCompra.DescuentoFijo;
            //                    dic[pitem.Key].Descuento = desc;
            //                    dic[pitem.Key].PromocionId = promocion.idpromocion;

            //                    if (promCompra.MonederoFijo.HasValue)
            //                        monedero += promCompra.MonederoFijo;
            //                    if (promCompra.MonederoPorcentaje.HasValue)
            //                    {
            //                        monedero += pitem.Corrida.precio * promCompra.MonederoPorcentaje;
            //                    }
            //                }
            //                //else if (dic[item.Key].Descuento < desc)
            //                //{
            //                //    dic[item.Key].Descuento = desc;
            //                //    dic[item.Key].PromocionId = promocion.idpromocion;
            //                //}
            //            }
            //        }
            //    }

            //    //    if ((promCompra.DescuentoPorcentaje ?? 0) > 0 || (promCompra.DescuentoFijo ?? 0) > 0)
            //    //    {
            //    //        var minImp = promocion.minimpcompra ?? 0;
            //    //        var validImp = true;
            //    //        decimal imp = 0;
            //    //        if (minImp > 0)
            //    //        {
            //    //            validImp = false;
            //    //            var q = validCompra.Where(k => !dic[k.Key].PromocionId.HasValue)
            //    //                    .OrderByDescending(k => k.Corrida.precio);
            //    //            foreach (var citem in q)
            //    //            {
            //    //                imp += citem.Corrida.precio ?? 0;

            //    //                var desc = citem.Corrida.precio * promCompra.DescuentoPorcentaje;
            //    //                dic[citem.Key].PromocionId = promocion.idpromocion;
            //    //                dic[citem.Key].Descuento = desc;
            //    //                dic[citem.Key].Fijo = promCompra.DescuentoFijo;

            //    //                if (imp >= minImp)
            //    //                {
            //    //                    validImp = true;
            //    //                    break;
            //    //                }
            //    //            }
            //    //        }
            //    //        var min = promocion.minunicompra ?? 0;
            //    //        int i = 0;
            //    //        if (validImp)
            //    //        {
            //    //            if (imp == 0)
            //    //            {
            //    //                for (i = 0; i < min; i++)
            //    //                {
            //    //                    var q = validCompra.Where(k => !dic[k.Key].PromocionId.HasValue)
            //    //                        .OrderByDescending(k => k.Corrida.precio);
            //    //                    if (q.Any())
            //    //                    {
            //    //                        var fi = q.First();
            //    //                        var desc = fi.Corrida.precio * promCompra.DescuentoPorcentaje;
            //    //                        dic[fi.Key].PromocionId = promocion.idpromocion;
            //    //                        dic[fi.Key].Descuento = desc;
            //    //                        dic[fi.Key].Fijo = promCompra.DescuentoFijo;
            //    //                    }
            //    //                    else
            //    //                        break;
            //    //                }
            //    //            }
            //    //            else
            //    //            {
            //    //                var q = validCompra.Where(k => dic[k.Key].PromocionId == promocion.idpromocion)
            //    //                            .OrderByDescending(k => k.Corrida.precio);
            //    //                i = q.Count();
            //    //            }
            //    //        }

            //    //        var valid = true;
            //    //        if (i >= min && validImp)
            //    //        {
            //    //            var promo = promocion.unipromo ?? 0;
            //    //            for (i = 0; i < promo; i++)
            //    //            {
            //    //                var q = validPromo.Where(k => !dic[k.Key].PromocionId.HasValue)
            //    //                    .OrderBy(k => k.Corrida.precio);
            //    //                if (q.Any())
            //    //                {
            //    //                    var fi = q.First();
            //    //                    var desc = fi.Corrida.precio * promPromo.DescuentoPorcentaje;
            //    //                    dic[fi.Key].PromocionId = promocion.idpromocion;
            //    //                    dic[fi.Key].Descuento = desc;
            //    //                    dic[fi.Key].Fijo = promPromo.DescuentoFijo;
            //    //                }
            //    //                else
            //    //                    break;
            //    //            }
            //    //            if (i != promo)
            //    //                valid = false;
            //    //        }
            //    //        else
            //    //            valid = false;
            //    //        if (!valid)
            //    //        {
            //    //            foreach (var citem in dic.Where(k => k.Value.PromocionId == promocion.idpromocion))
            //    //            {
            //    //                citem.Value.Fijo = null;
            //    //                citem.Value.PromocionId = null;
            //    //                citem.Value.Descuento = null;
            //    //            }
            //    //        }
            //    //    }
            //}


            //Dictionary<string, DataAccess.SirCo.Corrida> validChecar = null;
            //decimal? porcentajeChecar = null;
            //if (promocion.tipo == Common.Constants.TipoPromocion.DIRECTA)
            //{
            //    validChecar = validPromo;
            //    porcentajeChecar = porcentajePromo;
            //}
            //if (promocion.tipo == Common.Constants.TipoPromocion.AxB)
            //{
            //    validChecar = validCompra;
            //    porcentajeChecar = porcentajeCompra;
            //}

            /*if (promocion.tipo == Common.Constants.TipoPromocion.AxB)
            {
                int i;
                decimal imp = 0;
                var minImp = promocion.minimpcompra ?? 0;
                var validImp = true;
                if (minImp > 0)
                {
                    validImp = false;
                    var q = validCompra.Where(k => !dic[k.Key].PromocionId.HasValue)
                            .OrderByDescending(k => k.Corrida.precio);
                    foreach (var item in q)
                    {
                        imp += item.Corrida.precio ?? 0;

                        var desc = item.Corrida.precio * porcentajeCompra;
                        dic[item.Key].PromocionId = promocion.idpromocion;
                        dic[item.Key].Descuento = desc;
                        dic[item.Key].Fijo = fijoCompra;

                        if (imp >= minImp)
                        {
                            validImp = true;
                            break;
                        }
                    }
                }
                i = 0;
                var min = promocion.minunicompra ?? 0;
                if (validImp)
                {
                    if (imp == 0)
                    {
                        for (i = 0; i < min; i++)
                        {
                            var q = validCompra.Where(k => !dic[k.Key].PromocionId.HasValue)
                                .OrderByDescending(k => k.Corrida.precio);
                            if (q.Any())
                            {
                                var fi = q.First();
                                var desc = fi.Corrida.precio * porcentajeCompra;
                                dic[fi.Key].PromocionId = promocion.idpromocion;
                                dic[fi.Key].Descuento = desc;
                                dic[fi.Key].Fijo = fijoCompra;
                            }
                            else
                                break;
                        }
                    }
                    else
                    {
                        var q = validCompra.Where(k => dic[k.Key].PromocionId == promocion.idpromocion)
                                    .OrderByDescending(k => k.Corrida.precio);
                        i = q.Count();
                    }
                }
                var valid = true;
                if (i >= min && validImp)
                {
                    var promo = promocion.unipromo ?? 0;
                    for (i = 0; i < promo; i++)
                    {
                        var q = validPromo.Where(k => !dic[k.Key].PromocionId.HasValue)
                            .OrderBy(k => k.Corrida.precio);
                        if (q.Any())
                        {
                            var fi = q.First();
                            var desc = fi.Corrida.precio * porcentajePromo;
                            dic[fi.Key].PromocionId = promocion.idpromocion;
                            dic[fi.Key].Descuento = desc;
                            dic[fi.Key].Fijo = fijoPromo;
                        }
                        else
                            break;
                    }
                    if (i != promo)
                        valid = false;
                }
                else
                    valid = false;
                if (!valid)
                {
                    foreach (var item in dic.Where(k => k.Value.PromocionId == promocion.idpromocion))
                    {
                        item.Value.Fijo = null;
                        item.Value.PromocionId = null;
                        item.Value.Descuento = null;
                    }
                }
            }*/


            //return monedero;
        }

        private void AgruparUnidadesImporte(List<Entities.ProductoPromocion> items, decimal importe, int count, string tipo)
        {
            var h = new Helpers.Combinations<Entities.ProductoPromocion>();
            var res = h.Generate(items.ToArray(), count, importe, p => (p.SerieFormaPago.Precio ?? p.Corrida.precio).Value);
            var hash = new HashSet<string>();
            foreach (var item in res.ToArray())
            {
                var sum = item.Sum(i => i.SerieFormaPago.Precio ?? i.Corrida.precio);
                if (sum < importe)
                    res.Remove(item);
                var aid = item.Select(i => i.Serie.serie).ToList();
                aid.Sort();
                var id = String.Join("-", aid);
                if (!hash.Add(id))
                    res.Remove(item);
            }
            var q = from e in res
                    select new
                    {
                        items = tipo == Common.Constants.TipoPromocion.DIRECTA ? e.OrderBy(i => i.SerieFormaPago.Precio ?? i.Corrida.precio).ThenBy(i => i.Order)
                            : e.OrderByDescending(i => i.SerieFormaPago.Precio ?? i.Corrida.precio).ThenBy(i => i.Order),
                        total = e.Sum(i => i.SerieFormaPago.Precio ?? i.Corrida.precio)
                    };
            var qf = q.OrderBy(i => i.total);
            var index = 0;
            var group = 0;
            foreach (var item in qf)
            {
                if (item.items.Where(i => i.CustomOrder.HasValue).Any())
                    continue;
                foreach (var pitem in item.items)
                {
                    pitem.CustomOrder = index++;
                    pitem.Group = group;
                }
                group++;
            }
        }

        private void RemoverParesUnicos(List<Entities.ProductoPromocion> items, List<Entities.ProductoPromocion> valid)
        {
            var ctx = new DataAccess.SirCoDataContext();
            foreach (var item in valid.ToArray())
            {
                //if (this.IsParUnico(item.Serie.Articulo.idagrupacion))
                if (this.IsParUnico(item.AgrupacionId))
                {
                    valid.Remove(item);
                    items.Add(item);
                }
            }
        }

        public Distribuidor FindTarjetahabiente(string distrib)
        {
            var ctx = new DataAccess.SirCoCreditoDataContext();
            var item = ctx.Distribuidores.Where(i => i.distrib == distrib).SingleOrDefault();
            if (item == null)
                return null;

            var model = new Distribuidor
            {
                Nombre = item.nombrecompleto,
                //ApMaterno, ApPaterno,
                Id = item.iddistrib,
                Electronica = item.solocalzado == 0,
                Status = item.idestatus.Value
            };

            var qpp = ctx.PlanPagos.Where(i => i.distrib == item.distrib);
            var qp = qpp.Where(i => i.pagado == "0");
            var usado = qp.Any() ? qp.Sum(i => i.saldo) : 0;
            model.Disponible = Math.Min(item.limitevale.Value, item.disponible.Value) - usado;
            model.Disponible = model.Disponible < 0 ? 0 : model.Disponible;

            var q = ctx.DistribuidorFirmas.Where(i => i.distrib == item.distrib);
            model.Firmas = q.Select(i => i.numfirma).ToArray();

            if (item.clientedi != null)
            {
                var ctxc = new DataAccess.SirCoControlDataContext();
                var suc = ctxc.Sucursales.Where(i => i.sucursal == item.succtedi).Single();
                var cli = ctx.Clientes.Where(i => i.idsucursal == suc.idsucursal && i.cliente == item.clientedi).Single();
                model.ClienteId = cli.idcliente;
            }
            return model;
        }
        private IDictionary<Common.Constants.FormaPago, string> GetFormaPagoMapping()
        {
            var ctx = new DataAccess.SirCoPVDataContext();
            var q = ctx.FormasPago.Where(i => i.pos).Select(i => new { i.idformapago, i.formapago, i.promocion });
            var dic = new Dictionary<Common.Constants.FormaPago, string>();
            foreach (var item in q)
            {
                var fp = (Common.Constants.FormaPago)Enum.Parse(typeof(Common.Constants.FormaPago), item.formapago);
                dic.Add(fp, item.promocion ?? fp.ToString());
            }
            return dic;
        }
        private Entities.PromocionValores CheckFormasPago(SerieFormasPago item, Promociones promocion, string tipo,
            IDictionary<Common.Constants.FormaPago, string> mapping, int? num = null)
        {
            decimal? descFijo = null;
            decimal? descImpFijo = null;
            decimal? monFijo = null;
            decimal? monPorcentaje = null;
            decimal? descPorcentaje = null;
            //var valid = false;

            var q = promocion.Detalle.Where(i => i.tipo == tipo);
            if (num.HasValue)
                q = q.Where(i => i.numunidad == num);
            //var qdetalle = q
            //    .Select(i => new
            //    {
            //        formapago = i.formapago,
            //        descdirecto = i.descdirecto,
            //        impbono = i.impbono,
            //        impfijo = i.impfijo,
            //        porcdinelec = i.porcdinelec
            //    }).Distinct()
            //    .Select(i => new DataAccess.SirCoPV.PromocionesDetalle
            //    {
            //        formapago = i.formapago,
            //        descdirecto = i.descdirecto,
            //        impbono = i.impbono,
            //        impfijo = i.impfijo,
            //        porcdinelec = i.porcdinelec
            //    }).Distinct();
            //var detalle = qdetalle.ToDictionary(i => i.formapago, i => i);
            var detalle = q.ToDictionary(i => i.formapago, i => i);
            var dic = new Dictionary<Common.Constants.FormaPago, DataAccess.SirCoPV.PromocionesDetalle>();
            //var mapping = new Dictionary<Common.Constants.FormaPago, string>();
            //{
            //    { Common.Constants.FormaPago.EF, FormaPagoPromocion.EF },
            //    { Common.Constants.FormaPago.TC, FormaPagoPromocion.EF },
            //    { Common.Constants.FormaPago.TD, FormaPagoPromocion.EF },
            //    { Common.Constants.FormaPago.VA, FormaPagoPromocion.VA },
            //    { Common.Constants.FormaPago.CP, FormaPagoPromocion.VA },
            //    { Common.Constants.FormaPago.CV, FormaPagoPromocion.VA },
            //    { Common.Constants.FormaPago.MD, FormaPagoPromocion.VA },
            //    { Common.Constants.FormaPago.VD, FormaPagoPromocion.VA },
            //    { Common.Constants.FormaPago.DV, FormaPagoPromocion.VA },
            //    { Common.Constants.FormaPago.CD, FormaPagoPromocion.VA },
            //    //public const string CS = "CS";no se usa
            //    //public const string DP = "DP";                
            //    //public const string VS = "VS";no se usa
            //    //VD - vale digital
            //};            

            foreach (var sfpt in item.FormasPago.Distinct())
            {
                var sfp = sfpt;
                if (sfp == Common.Constants.FormaPago.VA
                    && item.Promociones != null
                    && item.Promociones.ContainsKey(sfp)
                    && !item.Promociones[sfp])
                {

                }
                if (!mapping.ContainsKey(sfp))
                    continue;
                if (detalle.ContainsKey(mapping[sfp]))
                    dic.Add(sfp, detalle[mapping[sfp]]);
                else
                {
                    if (detalle.ContainsKey(FormaPagoPromocion.TO))
                        dic.Add(sfp, detalle[FormaPagoPromocion.TO]);
                    else
                    {
                        dic.Clear();
                        break;
                    }
                }
            }

            var pdet = dic
                .OrderBy(i => i.Value.descdirecto)
                .ThenByDescending(i => i.Value.impfijo)
                .ThenBy(i => i.Value.descfijo)
                .ThenBy(i => i.Value.porcdinelec)
                .ThenBy(i => i.Value.impbono)
                .Select(i => i.Value)
                .FirstOrDefault();

            if (pdet == null
                && !item.FormasPago.Any()
                && detalle.ContainsKey(FormaPagoPromocion.TO))
                pdet = detalle[FormaPagoPromocion.TO];

            var success = false;
            if (pdet != null)
            {
                success = true;
                if ((pdet.impfijo ?? 0) > 0)
                    descFijo = pdet.impfijo;
                if ((pdet.descdirecto ?? 0) > 0)
                    descPorcentaje = pdet.descdirecto / 100;

                if ((pdet.impbono ?? 0) > 0)
                    monFijo = pdet.impbono;
                if ((pdet.porcdinelec ?? 0) > 0)
                    monPorcentaje = pdet.porcdinelec / 100;

                if ((pdet.descfijo ?? 0) > 0)
                    descImpFijo = pdet.descfijo;
            }

            return new Entities.PromocionValores
            {
                Success = success,
                DescuentoPorcentaje = descPorcentaje,
                DescuentoFijo = descFijo,
                MonederoPorcentaje = monPorcentaje,
                MonederoFijo = monFijo,
                DescuentoImporteFijo = descImpFijo
            };
        }

        private void ChecarExclusion(PromocionesExclusiones item,
            ICollection<Entities.ProductoPromocion> items,
            ICollection<Entities.ProductoPromocion> valid)
        {
            var q = valid.Where(i => i.Corrida.marca == item.marca);
            if (!string.IsNullOrEmpty(item.estilon))
            {
                q = valid.Where(i => i.Corrida.marca == item.marca && i.Corrida.estilon == item.estilon);
            }
            foreach (var p in q.ToArray())
            {
                valid.Remove(p);
                items.Add(p);
            }
        }

        private void ChecarAgrupacion(Agrupaciones item,
            ICollection<Entities.ProductoPromocion> items,
            ICollection<Entities.ProductoPromocion> valid,
            IDictionary<string, Common.Entities.ProductoPromocion> dic = null)
        {
            Action<IEnumerable<Entities.ProductoPromocion>> updateItems = kvs =>
            {
                var list = kvs.ToList();
                list.ForEach(i =>
                {
                    if (dic != null && dic[i.Key].PromocionId.HasValue)
                        return;

                    valid.Add(i);
                    items.Remove(i);
                });
            };
            foreach (var adet in item.Detalle)
            {
                if (!items.Any())
                    break;

                switch (adet.nivel)
                {
                    case "Marca":
                        {
                            var q = items.Where(i => i.Corrida.marca == adet.marca);
                            updateItems(q);
                        }
                        break;
                    case "L1":
                        {
                            var q = items.Where(i =>
                                i.Corrida.idl1 == adet.idl1
                                && i.Corrida.idlinea == adet.idlinea
                                && i.Corrida.idfamilia == adet.idfamilia
                                && i.Corrida.iddepto == adet.iddepto
                                && i.Corrida.iddivisiones == adet.iddivisiones);
                            if (!String.IsNullOrEmpty(adet.marca))
                                q = q.Where(i => i.Corrida.marca == adet.marca);
                            updateItems(q);
                        }
                        break;
                    case "L2":
                        {
                            var q = items.Where(i =>
                                i.Corrida.idl1 == adet.idl1
                                && i.Corrida.idl2 == adet.idl2
                                && i.Corrida.idlinea == adet.idlinea
                                && i.Corrida.idfamilia == adet.idfamilia
                                && i.Corrida.iddepto == adet.iddepto
                                && i.Corrida.iddivisiones == adet.iddivisiones);
                            if (!String.IsNullOrEmpty(adet.marca))
                                q = q.Where(i => i.Corrida.marca == adet.marca);
                            updateItems(q);
                        }
                        break;
                    case "L3":
                        {
                            var q = items.Where(i =>
                                i.Corrida.idl1 == adet.idl1
                                && i.Corrida.idl2 == adet.idl2
                                && i.Corrida.idl3 == adet.idl3
                                && i.Corrida.idlinea == adet.idlinea
                                && i.Corrida.idfamilia == adet.idfamilia
                                && i.Corrida.iddepto == adet.iddepto
                                && i.Corrida.iddivisiones == adet.iddivisiones);
                            if (!String.IsNullOrEmpty(adet.marca))
                                q = q.Where(i => i.Corrida.marca == adet.marca);
                            updateItems(q);
                        }
                        break;
                    case "L4":
                        {
                            var q = items.Where(i =>
                                i.Corrida.idl1 == adet.idl1
                                && i.Corrida.idl2 == adet.idl2
                                && i.Corrida.idl3 == adet.idl3
                                && i.Corrida.idl4 == adet.idl4
                                && i.Corrida.idlinea == adet.idlinea
                                && i.Corrida.idfamilia == adet.idfamilia
                                && i.Corrida.iddepto == adet.iddepto
                                && i.Corrida.iddivisiones == adet.iddivisiones);
                            if (!String.IsNullOrEmpty(adet.marca))
                                q = q.Where(i => i.Corrida.marca == adet.marca);
                            updateItems(q);
                        }
                        break;
                    case "L5":
                        {
                            var q = items.Where(i =>
                                i.Corrida.idl1 == adet.idl1
                                && i.Corrida.idl2 == adet.idl2
                                && i.Corrida.idl3 == adet.idl3
                                && i.Corrida.idl4 == adet.idl4
                                && i.Corrida.idl5 == adet.idl5
                                && i.Corrida.idlinea == adet.idlinea
                                && i.Corrida.idfamilia == adet.idfamilia
                                && i.Corrida.iddepto == adet.iddepto
                                && i.Corrida.iddivisiones == adet.iddivisiones);
                            if (!String.IsNullOrEmpty(adet.marca))
                                q = q.Where(i => i.Corrida.marca == adet.marca);
                            updateItems(q);
                        }
                        break;
                    case "L6":
                        {
                            var q = items.Where(i =>
                                i.Corrida.idl1 == adet.idl1
                                && i.Corrida.idl2 == adet.idl2
                                && i.Corrida.idl3 == adet.idl3
                                && i.Corrida.idl4 == adet.idl4
                                && i.Corrida.idl5 == adet.idl5
                                && i.Corrida.idl6 == adet.idl6
                                && i.Corrida.idlinea == adet.idlinea
                                && i.Corrida.idfamilia == adet.idfamilia
                                && i.Corrida.iddepto == adet.iddepto
                                && i.Corrida.iddivisiones == adet.iddivisiones);
                            if (!String.IsNullOrEmpty(adet.marca))
                                q = q.Where(i => i.Corrida.marca == adet.marca);
                            updateItems(q);
                        }
                        break;
                    case "Departamento":
                        {
                            var q = items.Where(i =>
                                i.Corrida.iddepto == adet.iddepto
                                && i.Corrida.iddivisiones == adet.iddivisiones);
                            if (!String.IsNullOrEmpty(adet.marca))
                                q = q.Where(i => i.Corrida.marca == adet.marca);
                            updateItems(q);
                        }
                        break;
                    case "División":
                        {
                            var q = items.Where(i => i.Corrida.iddivisiones == adet.iddivisiones);
                            if (!String.IsNullOrEmpty(adet.marca))
                                q = q.Where(i => i.Corrida.marca == adet.marca);
                            updateItems(q);
                        }
                        break;
                    case "Linea":
                        {
                            var q = items.Where(i =>
                                i.Corrida.idlinea == adet.idlinea
                                && i.Corrida.idfamilia == adet.idfamilia
                                && i.Corrida.iddepto == adet.iddepto
                                && i.Corrida.iddivisiones == adet.iddivisiones);
                            if (!String.IsNullOrEmpty(adet.marca))
                                q = q.Where(i => i.Corrida.marca == adet.marca);
                            updateItems(q);
                        }
                        break;
                    case "Modelo":
                        {
                            var q = items.Where(i =>
                                i.Corrida.marca == adet.marca
                                && i.Corrida.estilon == adet.estilon);
                            updateItems(q);
                        }
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        private bool AplicaPromocionDirecta(
            Promociones promocion,
            Dictionary<string, Common.Entities.ProductoPromocion> dic,
            int? maxItems, List<Entities.ProductoPromocion> validPromo, int? index,
            IDictionary<Common.Constants.FormaPago, string> mapping)
        {
            var currentCount = 0;
            var q = validPromo
                .OrderByDescending(i => i.CustomOrder.HasValue)
                .ThenBy(i => i.CustomOrder)
                .ThenBy(i => i.SerieFormaPago.Precio ?? i.Corrida.precio)
                .ThenBy(i => i.Order);
            foreach (var pitem in q)
            {
                var item = pitem.SerieFormaPago;

                if (dic[item.Serie].PromocionId.HasValue)
                    continue;

                //var promCompra = CheckFormasPago(item, promocion, "COMPRA");
                var promPromo = CheckFormasPago(item, promocion, "PROMO", mapping: mapping);

                if (!promPromo.Success /*!promCompra.DescuentoPorcentaje.HasValue && !promCompra.DescuentoFijo.HasValue
                        && */
                        /*!promPromo.DescuentoPorcentaje.HasValue && !promPromo.DescuentoFijo.HasValue*/)
                    continue;


                if (promPromo.Success /*(promPromo.DescuentoPorcentaje ?? 0) > 0 || (promPromo.DescuentoFijo ?? 0) > 0*/)
                {
                    if (!dic[pitem.Key].PromocionId.HasValue)
                    {
                        currentCount++;
                        AsignarPromocion(promocion, dic, index, pitem, promPromo, promo: true);

                        if (maxItems.HasValue && maxItems == currentCount)
                        {
                            return true;
                        }
                    }
                    //else if (dic[item.Key].Descuento < desc)
                    //{
                    //    dic[item.Key].Descuento = desc;
                    //    dic[item.Key].PromocionId = promocion.idpromocion;
                    //}
                }
            }

            if (!maxItems.HasValue && currentCount > 0)
                return true;

            foreach (var key in dic.Where(i => i.Value.PromocionId == promocion.idpromocion
                && i.Value.Index == index)
                .Select(i => i.Key))
            {
                dic[key].Fijo = null;
                dic[key].Descuento = null;
                dic[key].PromocionId = null;
                dic[key].Monedero = null;
                dic[key].Index = null;
            }
            return false;
        }

        private static void AsignarPromocion(Promociones promocion,
            Dictionary<string, Common.Entities.ProductoPromocion> dic, int? index,
            Entities.ProductoPromocion pitem,
            PromocionValores promPromo,
            bool promo)
        {
            var desc = (pitem.SerieFormaPago.Precio ?? pitem.Corrida.precio) * (promPromo.DescuentoPorcentaje ?? 0);
            dic[pitem.Key].Fijo = promPromo.DescuentoFijo;
            dic[pitem.Key].Descuento = desc + (promPromo.DescuentoImporteFijo ?? 0);
            dic[pitem.Key].PromocionId = promocion.idpromocion;
            dic[pitem.Key].Promo = promo;
            dic[pitem.Key].Monedero = 0;
            dic[pitem.Key].Index = index.HasValue ? (byte)index.Value : (byte?)null;
            if (promPromo.MonederoFijo.HasValue)
                dic[pitem.Key].Monedero += promPromo.MonederoFijo;
            if (promPromo.MonederoPorcentaje.HasValue)
                dic[pitem.Key].Monedero += (pitem.SerieFormaPago.Precio ?? pitem.Corrida.precio) * promPromo.MonederoPorcentaje;
        }

        private bool AplicaPromocionAxB_Compra(Promociones promocion,
            Dictionary<string, Common.Entities.ProductoPromocion> dic,
            List<Entities.ProductoPromocion> validCompra, List<Entities.ProductoPromocion> validPromo, int? index,
            IDictionary<Common.Constants.FormaPago, string> mapping)
        {
            var nums = promocion.Detalle.Where(i => i.tipo == "COMPRA")
                    .OrderBy(i => i.numunidad).Select(i => i.numunidad).Distinct();

            var applied = true;
            if (promocion.minunicompra == 0 && promocion.minimpcompra > 0)
            {
                var qlist = validCompra
                        .OrderByDescending(i => i.CustomOrder.HasValue)
                        .ThenBy(i => i.CustomOrder)
                        .ThenByDescending(i => i.SerieFormaPago.Precio ?? i.Corrida.precio)
                        .ThenBy(i => i.Order)
                        .ToList();

                var valid = false;
                foreach (var pitem in qlist)
                {
                    var item = pitem.SerieFormaPago;

                    if (dic[item.Serie].PromocionId.HasValue)
                        continue;

                    var promCompra = CheckFormasPago(item, promocion, "COMPRA", mapping: mapping, num: 1);

                    if (!promCompra.Success /*!promCompra.DescuentoPorcentaje.HasValue && !promCompra.DescuentoFijo.HasValue*/)
                        continue;

                    if (!dic[pitem.Key].PromocionId.HasValue)
                    {
                        valid = true;
                        AsignarPromocion(promocion, dic, index, pitem, promCompra, promo: false);

                        var q = dic.Where(i => i.Value.PromocionId == promocion.idpromocion
                            && i.Value.Index == index
                            && !i.Value.Promo.Value);
                        decimal sum = 0;
                        foreach (var prod in q)
                        {
                            var p = qlist.Where(i => i.Serie.serie == prod.Key).Single();
                            sum += p.Corrida.precio.Value;
                        }
                        if (sum >= promocion.minimpcompra)
                        {
                            break;
                        }
                    }
                }
                if (!valid)
                {
                    applied = false;
                }
            }
            else
            {
                foreach (var num in nums)
                {
                    var valid = false;
                    var q = validCompra
                        .OrderByDescending(i => i.CustomOrder.HasValue)
                        .ThenBy(i => i.CustomOrder)
                        .ThenByDescending(i => i.SerieFormaPago.Precio ?? i.Corrida.precio)
                        .ThenBy(i => i.Order);
                    foreach (var pitem in q)
                    {
                        var item = pitem.SerieFormaPago;

                        if (dic[item.Serie].PromocionId.HasValue)
                            continue;

                        var promCompra = CheckFormasPago(item, promocion, "COMPRA", mapping: mapping, num: num);

                        if (!promCompra.Success /*!promCompra.DescuentoPorcentaje.HasValue && !promCompra.DescuentoFijo.HasValue*/)
                            continue;

                        //if ((promCompra.DescuentoPorcentaje ?? 0) > 0 || (promCompra.DescuentoFijo ?? 0) > 0)
                        {
                            if (!dic[pitem.Key].PromocionId.HasValue)
                            {
                                valid = true;
                                //currentCount++;
                                AsignarPromocion(promocion, dic, index, pitem, promCompra, promo: false);

                                //if (maxItems.HasValue && maxItems == currentCount)
                                //{
                                //    return true;
                                //}
                                break;
                            }
                            //else if (dic[item.Key].Descuento < desc)
                            //{
                            //    dic[item.Key].Descuento = desc;
                            //    dic[item.Key].PromocionId = promocion.idpromocion;
                            //}
                        }
                    }
                    if (!valid)
                    {
                        applied = false;
                        break;
                    }
                }
            }

            return applied;
        }
        private bool AplicaPromocionAxB_Promo(Promociones promocion,
            Dictionary<string, Common.Entities.ProductoPromocion> dic,
            List<Entities.ProductoPromocion> validCompra, List<Entities.ProductoPromocion> validPromo, int? index,
            IDictionary<Common.Constants.FormaPago, string> mapping, int count = 1)
        {

            var applied = true;

            var nums = promocion.Detalle.Where(i => i.tipo == "PROMO")
                .OrderBy(i => i.numunidad).Select(i => i.numunidad).Distinct();

            var rapproved = new Dictionary<int, bool>[count];
            for (int i = 0; i < count; i++)
            {
                rapproved[i] = nums.ToDictionary(k => k, k => false);
            }

            var cindex = 0;
            foreach (var num in nums)
            {
                var valid = false;
                for (int k = 0; k < count; k++)
                {
                    foreach (var pitem in validPromo.OrderBy(i => i.SerieFormaPago.Precio ?? i.Corrida.precio).ThenBy(i => i.Order))
                    {
                        var item = pitem.SerieFormaPago;

                        if (dic[item.Serie].PromocionId.HasValue)
                            continue;

                        var promPromo = CheckFormasPago(item, promocion, "PROMO", mapping: mapping, num: num);

                        if (!promPromo.Success /*!promCompra.DescuentoPorcentaje.HasValue && !promCompra.DescuentoFijo.HasValue*/)
                            continue;

                        //if ((promCompra.DescuentoPorcentaje ?? 0) > 0 || (promCompra.DescuentoFijo ?? 0) > 0)
                        {
                            if (!dic[pitem.Key].PromocionId.HasValue)
                            {
                                rapproved[k][num] = true;
                                valid = true;
                                //currentCount++;
                                AsignarPromocion(promocion, dic, index ?? (cindex++), pitem, promPromo, promo: true);

                                //if (maxItems.HasValue && maxItems == currentCount)
                                //{
                                //    return true;
                                //}
                                break;
                            }
                            //else if (dic[item.Key].Descuento < desc)
                            //{
                            //    dic[item.Key].Descuento = desc;
                            //    dic[item.Key].PromocionId = promocion.idpromocion;
                            //}
                        }
                    }
                    if (!valid)
                    {
                        applied = false;
                        break;
                    }
                }
                if (!valid)
                {
                    break;
                }
            }

            if (!applied && promocion.promosrequeridas.HasValue)
            {
                foreach (var d in rapproved)
                {
                    var total = d.Where(i => i.Value).Count();
                    if (total < promocion.promosrequeridas)
                        return false;
                }
                return true;
            }

            return applied;
        }
        public void AplicaPromocionAxB_Clear(Promociones promocion,
            Dictionary<string, Common.Entities.ProductoPromocion> dic,
            int? index)
        {
            foreach (var key in dic.Where(i => i.Value.PromocionId == promocion.idpromocion
                && i.Value.Index == index)
                .Select(i => i.Key))
            {
                dic[key].Fijo = null;
                dic[key].Descuento = null;
                dic[key].PromocionId = null;
                dic[key].Monedero = null;
                dic[key].Index = null;
            }
        }

        public DistribuidorObserva FindDistObserva(string dist)
        {
            var ctx = new DataAccess.SirCoCreditoDataContext();
            var item = ctx.Distribuidores.Where(i => i.distrib == dist).SingleOrDefault();
            if (item == null)
                return null;

            var model = new DistribuidorObserva
            {
                Observa01 = item.observ01,
                Observa02 = item.observ02,
                Observa03 = item.observ03,
                Observa04 = item.observ04,
                Observa05 = item.observ05,
            };
            return model;
        }

        public ValeResponse FindVale(string vale)
        {
            var ctx = new DataAccess.SirCoCreditoDataContext();
            var ctxc = new DataAccess.SirCoControlDataContext();
            var ctxp = new DataAccess.SirCoPOSDataContext();

            var valera = ctx.Valeras.Where(i =>
                String.Compare(vale, i.valeini) >= 0 && String.Compare(vale, i.valefin) <= 0
                && vale.Length == i.valeini.Length).SingleOrDefault();
            if (valera == null)
                return null;

            //var valCancelado = ctx.ValesCancelados.Where(i =>
            //                    String.Compare(vale, i.valeini) >= 0 && String.Compare(vale, i.valefin) <= 0).SingleOrDefault();
            //if (valCancelado != null)
            //    return null;

            var item = ctx.Distribuidores.Where(i => i.iddistrib == valera.iddistrib
                //&& i.tipodistrib == Common.Constants.TipoDistribuidor.NORMAL
                && i.clasificacion == Common.Constants.TipoCredito.DISTRIBUIDOR
                ).SingleOrDefault();
            if (item == null)
                return null;

            var dis = new Common.Entities.Distribuidor
            {
                Id = item.iddistrib,
                Cuenta = item.distrib,
                //Nombre = item.nombre,
                //ApPaterno = item.appaterno,
                //ApMaterno = item.apmaterno,
                Nombre = item.nombrecompleto,
                Status = item.idestatus.Value,
                Electronica = item.solocalzado == 0,
                ContraVale = item.contravale == 1,
                Promocion = item.promocion == 1,
                LimiteVale = item.limitevale,
                ValeExterno = item.negext == 1,
            };

            var model = new ValeResponse
            {
                Vale = vale,
                Cancelado = false,
                Distribuidor = dis
            };

            var valCancelado = ctx.ValesCancelados.Where(i =>
                String.Compare(vale, i.valeini) >= 0 && String.Compare(vale, i.valefin) <= 0).SingleOrDefault();
            if (valCancelado != null)
            {
                model.Cancelado = true;
                if (valCancelado.idmotivo.HasValue)
                {
                    var motivo = ctxc.MotivosCancelacion.Where(i => i.idmotivo == valCancelado.idmotivo).SingleOrDefault();
                    model.CanceladoMotivo = motivo?.motivo;
                }
                return model;
            }

            var qpp = ctx.PlanPagos.Where(i => i.vale.Trim() == vale && i.status == "AP" && i.negocio == "TO");

            model.Usado = false;
            var ultcompra = qpp.Where(x => x.fechacompra < DateTime.Now).OrderByDescending(x => x.fechacompra).FirstOrDefault();
            if (ultcompra != null)
            {
                if (ultcompra.fechacompra.ToString("yyyyMMdd") != DateTime.Now.ToString("yyyyMMdd")) 
                {
                    model.Usado = true;
                    model.SucursalUsado = ultcompra.sucursal;
                    model.NotaUsado = ultcompra.nota;
                    model.FechaUsado = ultcompra.fechacompra;
                }
            }
            
            var qp = qpp.Where(i => i.pagado == "0");
            
            var usado = qp.Any() ? qp.Sum(i => i.saldo) : 0;
            model.Disponible = Math.Min(item.limitevale.Value, item.disponible.Value) - usado;
            model.Disponible = model.Disponible < 0 ? 0 : model.Disponible;

            var q = ctx.DistribuidorFirmas.Where(i => i.distrib == item.distrib);
            model.Distribuidor.Firmas = q.Select(i => i.numfirma).ToArray();


            var vitem = ctxp.ValesCliente.Where(i => i.vale == model.Vale).SingleOrDefault();
            if (vitem != null)
            {
                model.ClienteId = vitem.idcliente;
                model.Limite = vitem.cantidad;
                model.WithLimite = true;
            }
            else
            {
                var last = qpp.OrderByDescending(i => i.fum).FirstOrDefault();
                if (last != null)
                {
                    var suc = ctxc.Sucursales.Where(i => i.sucursal == last.succliente).Single();

                    var cli = ctx.Clientes.Where(i => i.cliente == last.cliente && i.idsucursal == suc.idsucursal).Single();
                    model.ClienteId = cli.idcliente;
                }
            }

            return model;
        }
        public ValeResponse FindValeDigital(string vale)
        {
            var ctx = new DataAccess.SirCoCreditoDataContext();
            var ctxc = new DataAccess.SirCoControlDataContext();
            var ctxa = new DataAccess.SirCoAPPDataContext();

            var vitem = ctxa.ValesDigital.Where(i => i.codigoqr == vale).SingleOrDefault();
            if (vitem == null)
                return null;

            //var valCancelado = ctx.ValesCancelados.Where(i =>
            //                    String.Compare(vale, i.valeini) >= 0 && String.Compare(vale, i.valefin) <= 0).SingleOrDefault();
            //if (valCancelado != null)
            //    return null;

            var item = ctx.Distribuidores.Where(i => i.distrib == vitem.distrib
                //&& i.tipodistrib == Common.Constants.TipoDistribuidor.NORMAL
                && i.clasificacion == Common.Constants.TipoCredito.DISTRIBUIDOR
                ).SingleOrDefault();
            if (item == null)
                return null;

            var dis = new Common.Entities.Distribuidor
            {
                Id = item.iddistrib,
                //Nombre = item.nombre,
                //ApPaterno = item.appaterno,
                //ApMaterno = item.apmaterno,
                Nombre = item.nombrecompleto,
                Status = item.idestatus.Value,
                ContraVale = item.contravale == 1,
                //SoloCalzado = item.solocalzado == 1,
                Electronica = (vitem.electronica ?? false),
                //Promocion = item.promocion == 1
                Promocion = vitem.promocion ?? false
            };

            var model = new ValeResponse
            {
                Vale = vale,
                Vigencia = vitem.vigencia,
                Cancelado = vitem.estatus != "AC",
                Distribuidor = dis,
                ClienteId = vitem.idcliente
            };

            //var valCancelado = ctx.ValesCancelados.Where(i =>
            //    String.Compare(vale, i.valeini) >= 0 && String.Compare(vale, i.valefin) <= 0).SingleOrDefault();
            //if (valCancelado != null)
            //{
            //    model.Cancelado = true;
            //    return model;
            //}

            //var qpp = ctx.PlanPagos.Where(i => i.vale.Trim() == vale && i.status == "AP");
            //var qp = qpp.Where(i => i.pagado == "0");
            //var usado = qp.Any() ? qp.Sum(i => i.saldo) : 0;
            model.Disponible = Math.Min(vitem.disponible.Value, item.disponible.Value) /*- usado;
            model.Disponible = model.Disponible < 0 ? 0 : model.Disponible;

            var q = ctx.DistribuidorFirmas.Where(i => i.distrib == item.distrib);
            model.Distribuidor.Firmas = q.Select(i => i.numfirma).ToArray();


            //var last = qpp.OrderByDescending(i => i.fum).FirstOrDefault();
            //if (last != null)
            //{
            //    var suc = ctxc.Sucursales.Where(i => i.sucursal == last.succliente).Single();

            //    var cli = ctx.Clientes.Where(i => i.cliente == last.cliente && i.idsucursal == suc.idsucursal).Single();
            //    model.ClienteId = cli.idcliente;
            //}

            return model;
        }
        public ValeResponse FindValeDigitalByClient(int id)
        {
            var ctx = new DataAccess.SirCoCreditoDataContext();
            var ctxc = new DataAccess.SirCoControlDataContext();
            var ctxa = new DataAccess.SirCoAPPDataContext();

            var vitem = ctxa.ValesDigital.Where(i => i.idcliente == id
                && (!i.vigencia.HasValue || i.vigencia >= DateTime.Today)
                && i.disponible > 0)

                .FirstOrDefault();
            if (vitem == null)
                return null;

            //var valCancelado = ctx.ValesCancelados.Where(i =>
            //                    String.Compare(vale, i.valeini) >= 0 && String.Compare(vale, i.valefin) <= 0).SingleOrDefault();
            //if (valCancelado != null)
            //    return null;

            var item = ctx.Distribuidores.Where(i => i.distrib == vitem.distrib
                //&& i.tipodistrib == Common.Constants.TipoDistribuidor.NORMAL
                && i.clasificacion == Common.Constants.TipoCredito.DISTRIBUIDOR
                ).SingleOrDefault();
            if (item == null)
                return null;

            var dis = new Common.Entities.Distribuidor
            {
                Id = item.iddistrib,
                //Nombre = item.nombre,
                //ApPaterno = item.appaterno,
                //ApMaterno = item.apmaterno,
                Nombre = item.nombrecompleto,
                Status = item.idestatus.Value,
                ContraVale = item.contravale == 1,
                //SoloCalzado = item.solocalzado == 1,
                Electronica = (vitem.electronica ?? false),
                //Promocion = item.promocion == 1
                Promocion = vitem.promocion ?? false
            };

            var model = new ValeResponse
            {
                Vale = vitem.codigoqr,
                Cancelado = vitem.estatus != "AC",
                Vigencia = vitem.vigencia,
                Distribuidor = dis,
                ClienteId = vitem.idcliente
            };

            //var valCancelado = ctx.ValesCancelados.Where(i =>
            //    String.Compare(vale, i.valeini) >= 0 && String.Compare(vale, i.valefin) <= 0).SingleOrDefault();
            //if (valCancelado != null)
            //{
            //    model.Cancelado = true;
            //    return model;
            //}

            //var qpp = ctx.PlanPagos.Where(i => i.vale.Trim() == vale && i.status == "AP");
            //var qp = qpp.Where(i => i.pagado == "0");
            //var usado = qp.Any() ? qp.Sum(i => i.saldo) : 0;
            model.Disponible = Math.Min(vitem.disponible.Value, item.disponible.Value) /*- usado*/;
            model.Disponible = model.Disponible < 0 ? 0 : model.Disponible;

            var q = ctx.DistribuidorFirmas.Where(i => i.distrib == item.distrib);
            model.Distribuidor.Firmas = q.Select(i => i.numfirma).ToArray();


            //var last = qpp.OrderByDescending(i => i.fum).FirstOrDefault();
            //if (last != null)
            //{
            //    var suc = ctxc.Sucursales.Where(i => i.sucursal == last.succliente).Single();

            //    var cli = ctx.Clientes.Where(i => i.cliente == last.cliente && i.idsucursal == suc.idsucursal).Single();
            //    model.ClienteId = cli.idcliente;
            //}

            return model;
        }
        public CValeResponse FindContraVale(string sucursal, string vale)
        {
            var ctx = new DataAccess.SirCoCreditoDataContext();
            var ctxc = new DataAccess.SirCoControlDataContext();

            var cvale = ctx.ContraVales.Where(i => i.sucursal == sucursal && i.cvale == vale).SingleOrDefault();
            if (cvale == null)
                return null;

            //var valCancelado = ctx.ValesCancelados.Where(i =>
            //                    String.Compare(vale, i.valeini) >= 0 && String.Compare(vale, i.valefin) <= 0).SingleOrDefault();
            //if (valCancelado != null)
            //    return null;

            var item = ctx.Distribuidores.Where(i => i.distrib == cvale.distrib
                //&& i.tipodistrib == Common.Constants.TipoDistribuidor.NORMAL
                && i.clasificacion == Common.Constants.TipoCredito.DISTRIBUIDOR
                ).SingleOrDefault();
            if (item == null)
                return null;

            var dis = new Common.Entities.Distribuidor
            {
                Id = item.iddistrib,
                //Nombre = item.nombre,
                //ApPaterno = item.appaterno,
                //ApMaterno = item.apmaterno,
                Nombre = item.nombrecompleto,
                Status = item.idestatus.Value,
                Electronica = item.solocalzado == 0,
                ContraVale = item.contravale == 1,
                Promocion = item.promocion == 1
            };

            var model = new CValeResponse
            {
                Vale = vale,
                Vigencia = cvale.caduca,
                Cancelado = false,
                Distribuidor = dis,
                Sucursal = sucursal
            };

            if (cvale.status.Contains(Common.Constants.Status.ZC.ToString()))
            {
                model.Cancelado = true;
                return model;
            }
            //var valCancelado = ctx.ValesCancelados.Where(i =>
            //    String.Compare(vale, i.valeini) >= 0 && String.Compare(vale, i.valefin) <= 0).SingleOrDefault();
            //if (valCancelado != null)
            //{
            //    model.Cancelado = true;
            //    return model;
            //}

            var qpp = ctx.PlanPagos.Where(i => i.vale.Trim() == vale && i.status == "AP");
            //var qp = qpp.Where(i => i.pagado == "0");
            //var usado = qp.Any() ? qp.Sum(i => i.saldo) : 0;
            //model.Disponible = Math.Min(item.limitevale.Value, item.disponible.Value) /*- usado*/;
            //model.Disponible = model.Disponible < 0 ? 0 : model.Disponible;
            model.Disponible = cvale.saldo ?? 0;

            model.Usado = false;
            var ultcompra = qpp.Where(x => x.fechacompra < DateTime.Now).OrderByDescending(x => x.fechacompra).FirstOrDefault();
            if (ultcompra != null)
            {
                if (ultcompra.fechacompra.ToString("yyyyMMdd") != DateTime.Now.ToString("yyyyMMdd"))
                {
                    model.Usado = true;
                    model.SucursalUsado = ultcompra.sucursal;
                    model.NotaUsado = ultcompra.nota;
                    model.FechaUsado = ultcompra.fechacompra;
                }
            }

            //var q = ctx.DistribuidorFirmas.Where(i => i.distrib == item.distrib);
            //model.Distribuidor.Firmas = q.Select(i => i.numfirma).ToArray();


            //var last = qpp.OrderByDescending(i => i.fum).FirstOrDefault();
            //if (last != null)
            //{
            //    var suc = ctxc.Sucursales.Where(i => i.sucursal == last.succliente).Single();

            //    var cli = ctx.Clientes.Where(i => i.cliente == last.cliente && i.idsucursal == suc.idsucursal).Single();
            //    model.ClienteId = cli.idcliente;
            //}

            var suc = ctxc.Sucursales.Where(i => i.sucursal == cvale.succte).Single();
            var cli = ctx.Clientes.Where(i => i.cliente == cvale.cliente && i.idsucursal == suc.idsucursal).Single();
            model.ClienteId = cli.idcliente;

            return model;
        }
    }
}
