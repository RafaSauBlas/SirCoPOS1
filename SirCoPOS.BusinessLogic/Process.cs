using SirCoPOS.Common.Constants;
using SirCoPOS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.BusinessLogic
{
    public class Process
    {
        private readonly DateTime _empty;
        private readonly Admin _admin;
        private readonly Data _data;
        private readonly Sale _sale;
        
        public Process()
        {
            _empty = DateTime.Parse("1900-01-01T00:00:00.000");
            _sale = new BusinessLogic.Sale();
            _admin = new Admin();
            _data = new Data();
        }
        public void ReleaseProducto(string serie, int idusuario)
        {
            var ctx = new DataAccess.SirCoDataContext();
            ctx.UpdateSerieStatus(serie, Status.AC, Status.CA, idusuario: idusuario);
        }
        public IEnumerable<Common.Entities.Agrupacion> GetAgrupacionesPorSerie(string serie)
        {
            DataAccess.SirCoPVDataContext ctxpv = new DataAccess.SirCoPVDataContext();

            IEnumerable<Common.Entities.Agrupacion> agrupaciones = ctxpv.GetAgrupacionesPorSerie(serie);

            return agrupaciones;
        }

        //public IEnumerable<Common.Entities.PorcentajeFormaPago> GetPorcentajePorFpago(string sucursal, string devolucion)
        //{
        //    DataAccess.SirCoPVDataContext ctxpv = new DataAccess.SirCoPVDataContext();

        //    IEnumerable<Common.Entities.PorcentajeFormaPago> porcentajes = ctxpv.GetPorcentajeFPago(sucursal, devolucion);

        //    return porcentajes;
        //}
        //============================================================================================================================================
        public bool RequestProducto(string serie, int idusuario)
        {
            var now = DateTime.Now;
            var ctx = new DataAccess.SirCoDataContext();
            var item = ctx.Series.Where(i => i.serie == serie).Single();
            ctx.SaveChanges();
            var valid = new string[] {
                Status.AC.ToString(),
                Status.IF.ToString(),
                Status.AB.ToString()
            };
            var dif = now.AddMinutes(-15);
            if (valid.Contains(item.status)
                || (item.status == Status.CA.ToString() && item.idusuariocaja.HasValue && item.idusuariocaja == idusuario)
                || (item.status == Status.CA.ToString() && item.fechacaja.HasValue && item.fechacaja < dif))
            {
                var current = (Status)Enum.Parse(typeof(Status), item.status);
                var count = ctx.UpdateSerieStatus(serie, Status.CA, current, idusuario: idusuario);
                
                return count == 1;
            }
            return false;
        }
        //public bool UpdClienteInfo()
        //{
            //var ctx = new DataAccess.Procedimientos();
            //var count = ctx.UpdateCliInfo();
            //return count == 1;
        //}
        //============================================================================================================================================
        public int AddCliente(Common.Entities.Cliente model)
        {
            var now = Helpers.Common.GetNow();
            var ctx = new DataAccess.SirCoCreditoDataContext();
            var max = ctx.Clientes.Any() ? ctx.Clientes.Max(i => i.cliente) : "0";
            var nuevo = int.Parse(max) + 1;
            var item = new DataAccess.SirCoCredito.Cliente
            {
                idsucursal = model.SucursalId,
                cliente = nuevo.ToString(Formats.CLIENTE),
                nombrecompleto = model.NombreCompleto ?? $"{model.Nombre} {model.ApPaterno} {model.ApMaterno}",
                nombre = model.Nombre,
                appaterno = model.ApPaterno,
                apmaterno = model.ApMaterno,
                sexo = model.Sexo,
                idestado = model.Estado,
                idciudad = model.Ciudad,
                idcolonia = model.Colonia,
                codigopostal = model.CodigoPostal,
                calle = model.Calle,
                numero = model.Numero,
                celular1 = model.Celular1,
                email = model.Email,
                fecalta = now,
                //public int? idusuario { get; set; }
                idusuario = model.Idusuario,
                fum = now,
                idusuariomodif = 0,
                fummodif = DateTime.Parse(Formats.DATE_EMPTY),
                sistema = null,
                celular = model.Celular,
                identificacion = model.Identificacion
            };
            ctx.Clientes.Add(item);

            try
            {
                ctx.SaveChanges();
            }
            catch (Exception)
            {
                throw new AltaClienteExcepcion();
            }
            return item.idcliente;
        }

        public string DEMO()
        {
            var ztx = new DataAccess.SirCoDataContext();
            var q = ztx.Series.Where(i => i.serie == "0000003524085").Single();
            return "";
        }


        //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        public SaleResponse Sale2(SaleRequest model, int idcajero, IEnumerable<ChangeItem> change = null)
        {
            var now = Helpers.Common.GetNow();
            var ctxc = new DataAccess.SirCoControlDataContext();
            var ctxcr = new DataAccess.SirCoCreditoDataContext();
            var cvales = new List<ContraValeResponse>();
            var ctxpv = new DataAccess.SirCoPVDataContext();
            var suc = ctxc.Sucursales.Where(i => i.sucursal == model.Sucursal).Single();
            var folio = suc.cajas + 1;
            var venta = new VentaP();
            var sale = new Sale();

            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                PromocionesCupones = model.PromocionesCupones,
                Productos = model.Productos.Select(i =>
                    new SerieFormasPago
                    {
                        Serie = i.Serie,
                        Precio = i.Precio,
                        FormasPago = i.FormasPago,
                        Pagos = i.Pagos,
                        Promociones = i.Promociones
                    }),
                Sucursal = model.Sucursal
            };

            var header = new DataAccess.SirCoPV.Venta
            {
                sucursal = model.Sucursal,
                idcajero = idcajero,
                idvendedor = model.VendedorId,
                fecha = now,
                venta = folio.Value.ToString("000000"),
                estatus = Common.Constants.StatusVenta.Aplicada,
                idusuario = idcajero,
                fumcancela = _empty,
                idusuariocancela = 0,
                fum = now
            };

            var pago = new DataAccess.SirCoPV.Pago
            {
                sucursal = header.sucursal,
                pago = header.venta,
                fecha = now,
                estatus = "AP",
                idcajero = idcajero,
                idvendedor = model.VendedorId,
                idusuario = idcajero,
                fum = now,
                idusuariocancela = 0,
                fumcancela = _empty,
                Venta = header
            };


            var vdet = new DataAccess.SirCoPV.VentaDetalle
            {
                sucursal = header.sucursal,
                venta = header.venta,
                serie = "0000003524022",
                marca = "CTA",
                estilon = "    786",
                medida = "18-",

                renglon = 0,
                corrida = "A",
                idpromocion = 0,
                idpromocionnumero = 0,
                idtipo = 0,
                ctd = 1,
                precio = 529,
                precdesc = 0,
                costomargen = 529,
                iva = 16,
                idusuario = 0,
                fum = now,
                notas = "A",
                idrazon = 0
            };

            header.Detalles = new HashSet<DataAccess.SirCoPV.VentaDetalle>();

            header.Detalles.Add(new DataAccess.SirCoPV.VentaDetalle
            {
                sucursal = header.sucursal,
                venta = header.venta,
                serie = "0000003524022",
                marca = "CTA",
                estilon = "    786",
                medida = "18-",

                renglon = 0,
                corrida = "A",
                idpromocion = null,
                idtipo = 0,
                ctd = 1,
                //precio = old.precio,
                //precdesc = old.precdesc,
                precio = 529,//cor.precio,
                precdesc = 500,//cor.precio,
                costomargen = 270,
                iva = 16,
                idusuario = 0,
                fum = now,
            });


            var importe = model.Pagos.Sum(i => i.Importe);

            pago.Detalle = new HashSet<DataAccess.SirCoPV.PagoDetalle>();

            header.Detalles.Add(vdet);
            ctxpv.Ventas.Add(header);
            ctxpv.Pagos.Add(pago);
            //ctxpv.SaveChanges();


            return new SaleResponse
            {
                Folio = header.venta,
                Cliente = header.idcliente,
                ContraVales = cvales,
                // Monedero = promos.Monedero
            };
        }
        //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        public SaleResponse Sale(SaleRequest model, int idcajero, IEnumerable<ChangeItem> change = null)
        {
            if (model.Pagos == null)
                throw new ModeloPagosExcepcion();
            var gid = Guid.NewGuid();
            var iva = 16;
            var now = Helpers.Common.GetNow();
            var cvales = new List<ContraValeResponse>();
            var ctxpv = new DataAccess.SirCoPVDataContext();
            var ctxc = new DataAccess.SirCoControlDataContext();
            var ctxcr = new DataAccess.SirCoCreditoDataContext();
            var ctx = new DataAccess.SirCoDataContext();


            var helper = new BusinessLogic.Data();

            var suc = ctxc.Sucursales.Where(i => i.sucursal == model.Sucursal).Single();
            var folio = suc.cajas + 1;
            suc.cajas = folio;
            var header = new DataAccess.SirCoPV.Venta
            {
                sucursal = model.Sucursal,
                idcajero = idcajero,
                idvendedor = model.VendedorId,
                fecha = now,
                venta = folio.Value.ToString("000000"),
                estatus = Common.Constants.StatusVenta.Aplicada,
                idusuario = idcajero,
                fumcancela = _empty,
                idusuariocancela = 0,
                fum = now
            };

            header.Detalles = new HashSet<DataAccess.SirCoPV.VentaDetalle>();
            short count = 0;
            var valid = new string[] {
                //Status.AC.ToString(),
                Status.CA.ToString(),
                //Status.IF.ToString()
            };
            var sale = new Sale();
            var venta = new VentaP();
            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                PromocionesCupones = model.PromocionesCupones,
                Productos = model.Productos.Select(i =>
                    new SerieFormasPago
                    {
                        Serie = i.Serie,
                        Precio = i.Precio,
                        FormasPago = i.FormasPago,
                        Pagos = i.Pagos,
                        Promociones = i.Promociones
                    }),
                Sucursal = model.Sucursal
            };
            //=============================================================
            var req = new Common.Entities.SaleRequest
            {
                Productos = model.Productos.Select(i => new SerieFormasPago
                {
                    Serie = i.Serie,
                    Precio = i.Precio,
                    FormasPago = i.FormasPago,
                    Pagos = i.Pagos,
                    Promociones = i.Promociones
                })
            };
            //=============================================================
            if (model.Cliente != null)
            {
                request.HasCliente = true;
                request.ClienteId = model.Cliente.Id;
            }
            if (request.PromocionesCupones == null)
                request.PromocionesCupones = new Common.Entities.PromocionCuponItem[] { };
            //AQUI APARECE OTRO ERROR DE CONTROL REMOTO
            //===================================================================================================================
            //SirCoPOS.Common.Entities.SeriesFormaPago
            //var promo2 = venta.prueba("0000003524047", "01");
            //var promoz = sale.prueba();
            var devol = model.Pagos.Where(i => i.FormaPago == Common.Constants.FormaPago.DV).Select(m => new { m.Devolucion, m.Sucursal }).SingleOrDefault();
            if (devol != null)
            {
                request.TipoFPago = _data.GetPorcentajeFPago(devol.Sucursal, devol.Devolucion).Select(i => i.FormaPago).SingleOrDefault();
            }
            var promos = sale.CheckPromociones(request);

            DataAccess.SirCoPV.Venta dvet = null;
            if (change != null)
            {
                var pdev = model.Pagos.First();
                var dev = ctxpv.Devoluciones.Where(i => i.sucursal == pdev.Sucursal && i.devolvta == pdev.Devolucion).Single();
                var dsuc = dev.referencia.Substring(0, 2);
                var dvta = dev.referencia.Substring(2);
                dvet = ctxpv.Ventas.Where(i => i.sucursal == dsuc && i.venta == dvta).Single();
            }
            
            header.multiple = false;
            foreach (var sf in model.Productos)
            {
                var promo = promos.Promociones.Where(i => i.Serie == sf.Serie).SingleOrDefault();
                var det = ctx.Series.Where(i => i.serie == sf.Serie).Single();

                if (det.sucursal != request.Sucursal)
                {
                    header.multiple = true;
                }

                if (!valid.Contains(det.status))
                    throw new StatusSerieExcepcion();
                var modified = ctx.UpdateSerieStatus(sf.Serie, Status.BA, Status.CA, idusuario: idcajero);
                if (modified != 1)
                    throw new StatusSerieNoActualizadoExcepcion();
                var cor = ctx.GetCorrida(det);
                cor.ult_vta = now;

                var precio = sf.Precio ?? cor.precio;
                if (change != null)
                {
                    var citem = change.Where(i => i.NewItem == sf.Serie).Single();
                    var old = dvet.Detalles.Where(i => i.serie == citem.OldItem).Single();

                    header.Detalles.Add(new DataAccess.SirCoPV.VentaDetalle
                    {
                        sucursal = header.sucursal,
                        venta = header.venta,
                        serie = det.serie,
                        marca = det.marca,
                        estilon = det.estilon,
                        medida = det.medida,

                        renglon = ++count,
                        corrida = cor.corrida,
                        idpromocion = null,
                        idtipo = 0,
                        ctd = 1,
                        //precio = old.precio,
                        //precdesc = old.precdesc,
                        precio = precio,//cor.precio,
                        precdesc = citem.Corrida ? citem.Pago : precio,//cor.precio,
                        costomargen = cor.costomargen,
                        iva = iva,
                        idusuario = idcajero,
                        fum = now,

                    });
                }
                else
                {
                    var vdet = new DataAccess.SirCoPV.VentaDetalle
                    {
                        sucursal = header.sucursal,
                        venta = header.venta,
                        serie = det.serie,
                        marca = det.marca,
                        estilon = det.estilon,
                        medida = det.medida,

                        renglon = ++count,
                        corrida = cor.corrida,
                        idpromocion = promo?.PromocionId,
                        idpromocionnumero = promo?.Index,
                        idtipo = 0,
                        ctd = 1,
                        precio = precio,
                        precdesc = promo != null && promo.Fijo.HasValue ? promo.Fijo : precio - (promo?.Descuento ?? 0),
                        costomargen = cor.costomargen,
                        iva = iva,
                        idusuario = idcajero,
                        fum = now,
                        notas = sf.Notas,
                        idrazon = sf.NotaRazon
                    };
                    if (sf.AdicionalId.HasValue)
                    {
                        var ad = ctxpv.DescuentoEspeciales.Where(i => i.iddescuentoespecial == sf.AdicionalId).Single();
                        var desc = vdet.precio * (ad.descuento / 100m);
                        vdet.precdesc -= desc;
                        vdet.iddescuentoespecial = sf.AdicionalId;
                        vdet.descuentoespecialdesc = sf.AdicionalDesc;
                    }
                    header.Detalles.Add(vdet);
                    if (promo != null && promo.PromocionId.HasValue)
                    {
                        //rebaja = si la clasificacion de la promo es "GASTO" --> costomargen, si es "REBAJA" -> es precio - precdesc
                        var pro = ctxpv.Promociones.Where(i => i.idpromocion == promo.PromocionId).Single();
                        switch (pro.clasificacion)
                        {
                            case "GASTO":
                                vdet.rebaja = vdet.costomargen;
                                break;
                            case "REBAJA":
                                vdet.rebaja = vdet.precio - vdet.precdesc;
                                break;
                        }
                    }
                }
            }

            //===========================AQUI ES DONDE SE SUPONER QUE SE DEBE REGISTRAR LA VENTA======================================
            ctxpv.Ventas.Add(header);
            var total = header.Detalles.ToArray().Sum(i => (i.precdesc ?? 0));
            if (model.Pagos == null || !model.Pagos.Any())
                throw new ModeloPagosExcepcion();

            var pago = new DataAccess.SirCoPV.Pago
            {
                sucursal = header.sucursal,
                pago = header.venta,
                fecha = now,
                estatus = "AP",
                idcajero = idcajero,
                idvendedor = model.VendedorId,
                idusuario = idcajero,
                fum = now,
                idusuariocancela = 0,
                fumcancela = _empty,
                Venta = header
            };
            pago.Detalle = new HashSet<DataAccess.SirCoPV.PagoDetalle>();
            ctxpv.Pagos.Add(pago);

            var importe = model.Pagos.Sum(i => i.Importe);
            if (total != importe)
                throw new TotalNoCoincideExcepcion();

            var blindaje = helper.GetParametro<decimal?>(Parametros.BLINDAJE);

            DataAccess.SirCoCredito.Cliente cliente = null;
            DataAccess.SirCoControl.Sucursal succli = null;
            if (model.Cliente != null)
            {
                header.idcliente = this.AddClienteFromModel(model.Cliente, suc.idsucursal);
                cliente = ctxcr.Clientes.Where(i => i.idcliente == header.idcliente).Single();
                succli = ctxc.Sucursales.Where(i => i.idsucursal == cliente.idsucursal).Single();
            }

            foreach (var item in model.Pagos)
            {
                var detalle = new DataAccess.SirCoPV.PagoDetalle
                {
                    sucursal = header.sucursal,
                    pago = header.venta,
                    idformapago = (int)item.FormaPago,
                    idvaledigital = 0,
                    importe = item.Importe,
                    efectivo = item.Efectivo,
                    comision = 0,//??
                    observaciones = "",//??
                    iva = iva,
                    idusuario = idcajero,
                    fum = now,
                };
                pago.Detalle.Add(detalle);
                if (!(suc.web ?? false) && idcajero > 0)
                {
                    detalle.movimiento = gid;
                    _admin.Sale(idcajero, model.Sucursal, item.Importe, now, gid, item.FormaPago);
                }
                switch (item.FormaPago)
                {
                    case FormaPago.CI:
                        break;
                    case FormaPago.EF:
                    case FormaPago.GO:
                    case FormaPago.KU:
                        break;
                    case FormaPago.TC:
                    case FormaPago.TD:
                        {
                            detalle.terminacion = item.Terminacion;
                            detalle.transaccion = item.Referencia;
                            detalle.observaciones = $"Term: {item.Terminacion}, Tran: {item.Referencia}";
                        }
                        break;
                    case FormaPago.DV:
                        {
                            var dev = ctxpv.Devoluciones.Where(i => i.sucursal == item.Sucursal && i.devolvta == item.Devolucion).Single();
                            if (dev.disponible < item.Importe)
                                throw new DisponibleDevNoSuficienteExcepcion();
                            dev.disponible -= item.Importe;
                            if (dev.disponible == 0)
                                dev.estatus = "YA";
                            detalle.observaciones = $"{dev.sucursal}{dev.devolvta} DEVOLUCION";
                            detalle.referencia = $"{dev.sucursal}{dev.devolvta}";
                        }
                        break;
                    case FormaPago.MD:
                        {
                            var mheader = ctx.Dineros.Where(i => i.idsucursal == cliente.idsucursal && i.cliente == cliente.cliente && i.saldo > 0)
                                .Single();
                            decimal start = 0;
                            decimal porAsignar = item.Importe;
                            foreach (var m in mheader.Detalles.Where(i => i.estatus == "AC").OrderBy(i => i.vigencia))
                            {
                                if (porAsignar >= m.saldo)
                                {
                                    start += m.saldo.Value;
                                    porAsignar -= m.saldo.Value;
                                    mheader.saldo -= m.saldo.Value;
                                    m.saldo = 0;
                                }
                                else if (porAsignar < m.saldo)
                                {
                                    start += porAsignar;
                                    m.saldo -= porAsignar;
                                    mheader.saldo -= porAsignar;
                                    porAsignar = 0;
                                }
                                if (porAsignar == 0)
                                    break;
                            }
                            if (porAsignar != 0)
                                throw new SaldoPendienteExcepcion();
                        }
                        break;
                    case FormaPago.VA:
                        {
                            detalle.observaciones = item.Vale;
                            detalle.vale = item.Vale;

                            var valera = ctxcr.Valeras.Where(i =>
                                String.Compare(item.Vale, i.valeini) >= 0 && String.Compare(item.Vale, i.valefin) <= 0
                                && i.valeini.Length == item.Vale.Length).SingleOrDefault();
                            if (valera == null)
                                throw new NoExisteValeExcepcion();

                            var valCancelado = ctxcr.ValesCancelados.Where(i =>
                                String.Compare(item.Vale, i.valeini) >= 0 && String.Compare(item.Vale, i.valefin) <= 0
                                && i.valeini.Length == item.Vale.Length).SingleOrDefault();
                            if (valCancelado != null)
                                throw new ValeCanceladoExcepcion();

                            var dist = ctxcr.Distribuidores.Where(i => i.distrib == valera.distrib
                                //&& i.tipodistrib == Common.Constants.TipoDistribuidor.NORMAL
                                && i.clasificacion == Common.Constants.TipoCredito.DISTRIBUIDOR
                            ).SingleOrDefault();
                            if (dist == null)
                                throw new NoExisteDistibuidorExcepcion();

                            var qplans = ctxcr.PlanPagos.Where(i => i.vale.Trim() == item.Vale && i.status == "AP");
                            qplans = qplans.Where(i => i.pagado == "0");
                            var usado = qplans.Any() ? qplans.Sum(i => i.saldo) : 0;
                            var disponible = Math.Min(dist.limitevale.Value, dist.disponible.Value) - usado;
                            disponible = disponible < 0 ? 0 : disponible;

                            if (item.Importe > disponible)
                                throw new NoDisponibleExcepcion();

                            
                            dist.disponible = dist.disponible - item.Importe - blindaje; 
                            dist.saldo = dist.saldo + item.Importe + blindaje;

                            this.GenerarPlanPagos(now, model, idcajero, item, header, cvales, cliente, succli, dist);
                            //var cliente = ctxcr.Clientes.Where(i => i.idcliente == header.idcliente).Single();
                            //var succ = ctxc.Sucursales.Where(i => i.idsucursal == cliente.idsucursal).Single();

                            //var blindaje = helper.GetParametro<decimal?>(Parametros.BLINDAJE);

                            //var plan = new DataAccess.SirCoCredito.PlanPagos
                            //{
                            //    distrib = dist.distrib,
                            //    sucursal = model.Sucursal,
                            //    nota = header.venta,
                            //    negocio = "TO",
                            //    vale = item.Vale,
                            //    desctoori = dist.desctoori,
                            //    succliente = succ.sucursal,
                            //    cliente = cliente.cliente,
                            //    idcliente = cliente.idcliente,
                            //    fechaaplicarcorte = item.FechaAplicar,
                            //    fechacompra = now,
                            //    status = "AP",
                            //    importe = item.Importe,
                            //    saldo = item.Importe,
                            //    pagos = item.Plazos.Value,
                            //    pagado = "0",
                            //    observacion = "",
                            //    idusuario = model.CajeroId,
                            //    fum = now,
                            //    blindaje = blindaje
                            //};
                            //plan.Detalle = new HashSet<DataAccess.SirCoCredito.PlanPagosDetalle>();

                            //var h = new Common.Helpers.CommonHelper();
                            //var dps = new List<Tuple<int, decimal>>();
                            //var quitar = item.ProductosPlazos?.Sum(i => i.Importe.Value);
                            //dps.Add(new Tuple<int, decimal>(item.Plazos.Value, item.Importe - (quitar ?? 0)));
                            //if (item.ProductosPlazos != null)
                            //{
                            //    foreach (var pps in item.ProductosPlazos)
                            //    {
                            //        dps.Add(new Tuple<int, decimal>(pps.Plazos.Value, pps.Importe.Value));
                            //    }
                            //}
                            //var plazos = dps.Max(i => i.Item1);
                            //var detallePagos = h.GetPlazos(dps.ToArray());

                            //var fechas = ctxcr.Calendarios.Where(i => i.tipo == "CORTE" && i.tipocredito == "DISTRIBUIDOR"
                            //    && i.fechaaplicarcorte >= item.FechaAplicar)
                            //    .OrderBy(i => i.fechaaplicarcorte).Take(plazos).ToArray();

                            //for (int i = 0; i < plazos; i++)
                            //{
                            //    var fecha = fechas[i];
                            //    var det = new DataAccess.SirCoCredito.PlanPagosDetalle
                            //    {
                            //        sucursal = model.Sucursal,
                            //        nota = header.venta,
                            //        fechaaplicar = fecha.fechaaplicarcorte.Value,
                            //        pago = (i + 1),
                            //        pagos = plazos,
                            //        fechavencimiento = fecha.fechaaplicar.Value,
                            //        importe = detallePagos[i],
                            //        abono = 0,
                            //        descuento = 0,
                            //        interes = 0,
                            //        gastoscobranza = 0,
                            //        pagado = "0",
                            //        fechapago = _empty,
                            //        tipopago = ' ',
                            //        cobrador = 0,
                            //        idpago = 0,
                            //        idconvenio = 0,
                            //        idusuario = model.CajeroId,
                            //        fum = now,

                            //        negocio = plan.negocio,
                            //        vale = plan.vale,
                            //        distrib = plan.distrib
                            //    };
                            //    if (i == 0 && plan.blindaje.HasValue)
                            //        det.importe += plan.blindaje.Value;
                            //    plan.Detalle.Add(det);
                            //}

                            //ctxcr.PlanPagos.Add(plan);

                            //var sobrante = (item.Limite ?? 0) - item.Importe;
                            //if (dist.contravale == 1 && item.ContraVale && item.Importe < (item.Limite ?? 0))
                            //{                                
                            //    var cvale = (suc.cvale ?? 0) + 1;
                            //    suc.cvale = cvale;

                            //    var cv = new DataAccess.SirCoCredito.ContraVale
                            //    {
                            //        sucursal = header.sucursal,
                            //        cvale = cvale.ToString("0000000000"),
                            //        status = "GE",
                            //        fecha = now,
                            //        distrib = dist.distrib,
                            //        succte = succ.sucursal,
                            //        cliente = cliente.cliente,
                            //        caduca = now.AddDays(15), //TODO tomar de parametro
                            //        importe = sobrante,
                            //        saldo = sobrante,
                            //        referenc = header.venta,
                            //        observa = item.Vale, //????
                            //        idusuario = header.idusuario,
                            //        fum = now
                            //    };
                            //    ctxcr.ContraVales.Add(cv);

                            //    cvales.Add(new ContraValeResponse {
                            //        Vale = item.Vale,
                            //        Caducidad = cv.caduca.Value,
                            //        ContraVale = cv.cvale,
                            //        Importe = cv.importe.Value
                            //    });
                            //}
                        }
                        break;
                    case FormaPago.CP:
                        {
                            var dist = ctxcr.Distribuidores.Where(i => i.distrib == item.Distribuidor
                                //&& i.tipodistrib == Common.Constants.TipoDistribuidor.NORMAL
                                && i.clasificacion == Common.Constants.TipoCredito.TARJETAHABIENTE
                            ).SingleOrDefault();
                            if (dist == null)
                                throw new NoExisteDistibuidorExcepcion();

                            var disponible = dist.disponible ?? 0;
                            disponible = disponible < 0 ? 0 : disponible;

                            if (item.Importe > disponible)
                                throw new NoDisponibleExcepcion();

                            if (dist.clientedi == null)
                            {
                                dist.succtedi = succli.sucursal;
                                dist.clientedi = cliente.cliente;
                            }

                            item.Vale = int.Parse(item.Distribuidor).ToString();
                            item.FechaAplicar = now;
                            
                            dist.disponible = dist.disponible - item.Importe - blindaje;
                            dist.saldo = dist.saldo + item.Importe + blindaje;
                            this.GenerarPlanPagos(now, model, idcajero, item, header, cvales, cliente, succli, dist);

                            //var plan = new DataAccess.SirCoCredito.PlanPagos
                            //{
                            //    distrib = dist.distrib,
                            //    sucursal = model.Sucursal,
                            //    nota = header.venta,
                            //    negocio = "TO",
                            //    vale = int.Parse(item.Distribuidor).ToString(),
                            //    desctoori = dist.desctoori,
                            //    succliente = succ.sucursal,
                            //    cliente = cliente.cliente,
                            //    idcliente = cliente.idcliente,
                            //    fechaaplicarcorte = item.FechaAplicar,
                            //    fechacompra = now,
                            //    status = "AP",
                            //    importe = item.Importe,
                            //    saldo = item.Importe,
                            //    pagos = item.Plazos.Value,
                            //    pagado = "0",
                            //    observacion = "",
                            //    idusuario = model.CajeroId,
                            //    fum = now
                            //};
                            //plan.Detalle = new HashSet<DataAccess.SirCoCredito.PlanPagosDetalle>();

                            //var part = item.Importe / item.Plazos.Value;
                            //var pagos = Math.Ceiling(part);
                            //var restante = pagos * (item.Plazos.Value - 1);
                            //var faltante = item.Importe - restante;

                            //var fechas = ctxcr.Calendarios.Where(i => i.tipo == "CORTE" && i.tipocredito == "DISTRIBUIDOR"
                            //    && i.fechaaplicarcorte >= now)
                            //    .OrderBy(i => i.fechaaplicarcorte).Take(item.Plazos.Value).ToArray();

                            //for (int i = 0; i < item.Plazos; i++)
                            //{
                            //    var fecha = fechas[i];
                            //    var det = new DataAccess.SirCoCredito.PlanPagosDetalle
                            //    {
                            //        sucursal = model.Sucursal,
                            //        nota = header.venta,
                            //        fechaaplicar = fecha.fechaaplicarcorte.Value,
                            //        pago = (i + 1),
                            //        pagos = item.Plazos.Value,
                            //        fechavencimiento = fecha.fechaaplicar.Value,
                            //        importe = (i + 1) == item.Plazos ? faltante : pagos,
                            //        abono = 0,
                            //        descuento = 0,
                            //        interes = 0,
                            //        gastoscobranza = 0,
                            //        pagado = "0",
                            //        fechapago = _empty,
                            //        tipopago = ' ',
                            //        cobrador = 0,
                            //        idpago = 0,
                            //        idconvenio = 0,
                            //        idusuario = model.CajeroId,
                            //        fum = now
                            //    };
                            //    plan.Detalle.Add(det);
                            //}

                            //ctxcr.PlanPagos.Add(plan);
                        }
                        break;
                    case FormaPago.VD:
                        {
                            item.FormaPago = FormaPago.VA;
                            var vd = ctxcr.ValesDigital.Where(i => i.codigoqr == item.Vale).SingleOrDefault();
                            if (vd == null)
                                throw new NoExisteValeExcepcion();

                            if (header.idcliente != vd.idcliente)
                                throw new ClienteNOCoincideExcepcion();

                            if (vd.vigencia.HasValue && now > vd.vigencia.Value.AddDays(1))
                                throw new VigenciaVencidaExcepcion();

                            var dist = ctxcr.Distribuidores.Where(i => i.distrib == vd.distrib).Single();

                            var disponible = Math.Min(vd.disponible.Value, dist.disponible.Value);

                            if (item.Importe > disponible)
                                throw new NoDisponibleExcepcion();

                            detalle.idvaledigital = vd.idvaledigital;
                            vd.disponible -= item.Importe;
                            
                            dist.disponible = dist.disponible - item.Importe - blindaje;
                            dist.saldo = dist.saldo + item.Importe + blindaje;
                            this.GenerarPlanPagos(now, model, idcajero, item, header, cvales, cliente, succli, dist);

                            //var plan = new DataAccess.SirCoCredito.PlanPagos
                            //{
                            //    distrib = dist.distrib,
                            //    sucursal = model.Sucursal,
                            //    nota = header.venta,
                            //    negocio = "TO",
                            //    vale = item.Vale,
                            //    desctoori = dist.desctoori,
                            //    succliente = succ.sucursal,
                            //    cliente = cliente.cliente,
                            //    idcliente = cliente.idcliente,
                            //    fechaaplicarcorte = item.FechaAplicar,
                            //    fechacompra = now,
                            //    status = "AP",
                            //    importe = item.Importe,
                            //    saldo = item.Importe,
                            //    pagos = item.Plazos.Value,
                            //    pagado = "0",
                            //    observacion = "",
                            //    idusuario = model.CajeroId,
                            //    fum = now
                            //};
                            //plan.Detalle = new HashSet<DataAccess.SirCoCredito.PlanPagosDetalle>();

                            //var part = item.Importe / item.Plazos.Value;
                            //var pagos = Math.Ceiling(part);
                            //var restante = pagos * (item.Plazos.Value - 1);
                            //var faltante = item.Importe - restante;

                            //var fechas = ctxcr.Calendarios.Where(i => i.tipo == "CORTE" && i.tipocredito == "DISTRIBUIDOR"
                            //    && i.fechaaplicarcorte >= item.FechaAplicar)
                            //    .OrderBy(i => i.fechaaplicarcorte).Take(item.Plazos.Value).ToArray();

                            //for (int i = 0; i < item.Plazos; i++)
                            //{
                            //    var fecha = fechas[i];
                            //    var det = new DataAccess.SirCoCredito.PlanPagosDetalle
                            //    {
                            //        sucursal = model.Sucursal,
                            //        nota = header.venta,
                            //        fechaaplicar = fecha.fechaaplicarcorte.Value,
                            //        pago = (i + 1),
                            //        pagos = item.Plazos.Value,
                            //        fechavencimiento = fecha.fechaaplicar.Value,
                            //        importe = (i + 1) == item.Plazos ? faltante : pagos,
                            //        abono = 0,
                            //        descuento = 0,
                            //        interes = 0,
                            //        gastoscobranza = 0,
                            //        pagado = "0",
                            //        fechapago = _empty,
                            //        tipopago = ' ',
                            //        cobrador = 0,
                            //        idpago = 0,
                            //        idconvenio = 0,
                            //        idusuario = model.CajeroId,
                            //        fum = now
                            //    };
                            //    plan.Detalle.Add(det);
                            //}

                            //ctxcr.PlanPagos.Add(plan);

                        }
                        break;
                    case FormaPago.VE:
                        {
                            item.FormaPago = FormaPago.VA;
                            detalle.observaciones = item.Vale;
                            detalle.vale = item.Vale;

                            var cdis = ctxcr.DistribuidorComerciales.Where(i => i.idnegexterno == item.Negocio && i.nocuenta == item.NoCuenta).Single();

                            //var valera = ctxcr.Valeras.Where(i =>
                            //    String.Compare(item.Vale, i.valeini) >= 0 && String.Compare(item.Vale, i.valefin) <= 0).SingleOrDefault();
                            //if (valera == null)
                            //    throw new NotSupportedException();

                            //var valCancelado = ctxcr.ValesCancelados.Where(i =>
                            //    String.Compare(item.Vale, i.valeini) >= 0 && String.Compare(item.Vale, i.valefin) <= 0).SingleOrDefault();
                            //if (valCancelado != null)
                            //    throw new NotSupportedException();

                            var dist = ctxcr.Distribuidores.Where(i => i.distrib == cdis.distrib
                                //&& i.tipodistrib == Common.Constants.TipoDistribuidor.NORMAL
                                && i.clasificacion == Common.Constants.TipoCredito.DISTRIBUIDOR
                            ).SingleOrDefault();
                            if (dist == null)
                                throw new NoExisteDistibuidorExcepcion();

                            //var qplans = ctxcr.PlanPagos.Where(i => i.vale.Trim() == item.Vale && i.status == "AP");
                            //qplans = qplans.Where(i => i.pagado == "0");
                            //var usado = qplans.Any() ? qplans.Sum(i => i.saldo) : 0;
                            var disponible = Math.Min(dist.limitevale.Value, dist.disponible.Value) /*- usado*/;
                            disponible = disponible < 0 ? 0 : disponible;

                            if (item.Importe > disponible)
                                throw new NoDisponibleExcepcion();
                            
                            dist.disponible = dist.disponible - item.Importe - blindaje;
                            dist.saldo = dist.saldo + item.Importe + blindaje;
                            this.GenerarPlanPagos(now, model, idcajero, item, header, cvales, cliente, succli, dist);
                            //var cliente = ctxcr.Clientes.Where(i => i.idcliente == header.idcliente).Single();
                            //var succ = ctxc.Sucursales.Where(i => i.idsucursal == cliente.idsucursal).Single();

                            //var blindaje = helper.GetParametro<decimal?>(Parametros.BLINDAJE);

                            //var plan = new DataAccess.SirCoCredito.PlanPagos
                            //{
                            //    distrib = dist.distrib,
                            //    sucursal = model.Sucursal,
                            //    nota = header.venta,
                            //    negocio = "TO",
                            //    vale = item.Vale,
                            //    desctoori = dist.desctoori,
                            //    succliente = succ.sucursal,
                            //    cliente = cliente.cliente,
                            //    idcliente = cliente.idcliente,
                            //    fechaaplicarcorte = item.FechaAplicar,
                            //    fechacompra = now,
                            //    status = "AP",
                            //    importe = item.Importe,
                            //    saldo = item.Importe,
                            //    pagos = item.Plazos.Value,
                            //    pagado = "0",
                            //    observacion = "",
                            //    idusuario = model.CajeroId,
                            //    fum = now,
                            //    blindaje = blindaje
                            //};
                            //plan.Detalle = new HashSet<DataAccess.SirCoCredito.PlanPagosDetalle>();

                            //var h = new Common.Helpers.CommonHelper();
                            //var dps = new List<Tuple<int, decimal>>();
                            //var quitar = item.ProductosPlazos?.Sum(i => i.Importe.Value);
                            //dps.Add(new Tuple<int, decimal>(item.Plazos.Value, item.Importe - (quitar ?? 0)));
                            //if (item.ProductosPlazos != null)
                            //{
                            //    foreach (var pps in item.ProductosPlazos)
                            //    {
                            //        dps.Add(new Tuple<int, decimal>(pps.Plazos.Value, pps.Importe.Value));
                            //    }
                            //}
                            //var plazos = dps.Max(i => i.Item1);
                            //var detallePagos = h.GetPlazos(dps.ToArray());

                            //var fechas = ctxcr.Calendarios.Where(i => i.tipo == "CORTE" && i.tipocredito == "DISTRIBUIDOR"
                            //    && i.fechaaplicarcorte >= item.FechaAplicar)
                            //    .OrderBy(i => i.fechaaplicarcorte).Take(plazos).ToArray();

                            //for (int i = 0; i < plazos; i++)
                            //{
                            //    var fecha = fechas[i];
                            //    var det = new DataAccess.SirCoCredito.PlanPagosDetalle
                            //    {
                            //        sucursal = model.Sucursal,
                            //        nota = header.venta,
                            //        fechaaplicar = fecha.fechaaplicarcorte.Value,
                            //        pago = (i + 1),
                            //        pagos = plazos,
                            //        fechavencimiento = fecha.fechaaplicar.Value,
                            //        importe = detallePagos[i],
                            //        abono = 0,
                            //        descuento = 0,
                            //        interes = 0,
                            //        gastoscobranza = 0,
                            //        pagado = "0",
                            //        fechapago = _empty,
                            //        tipopago = ' ',
                            //        cobrador = 0,
                            //        idpago = 0,
                            //        idconvenio = 0,
                            //        idusuario = model.CajeroId,
                            //        fum = now,

                            //        negocio = plan.negocio,
                            //        vale = plan.vale,
                            //        distrib = plan.distrib
                            //    };
                            //    if (i == 0 && plan.blindaje.HasValue)
                            //        det.importe += plan.blindaje.Value;
                            //    plan.Detalle.Add(det);
                            //}

                            //ctxcr.PlanPagos.Add(plan);

                            //var sobrante = (item.Limite ?? 0) - item.Importe;
                            //if (dist.contravale == 1 && item.ContraVale && item.Importe < (item.Limite ?? 0))
                            //{                                
                            //    var cvale = (suc.cvale ?? 0) + 1;
                            //    suc.cvale = cvale;

                            //    var cv = new DataAccess.SirCoCredito.ContraVale
                            //    {
                            //        sucursal = header.sucursal,
                            //        cvale = cvale.ToString("0000000000"),
                            //        status = "GE",
                            //        fecha = now,
                            //        distrib = dist.distrib,
                            //        succte = succ.sucursal,
                            //        cliente = cliente.cliente,
                            //        caduca = now.AddDays(15), //TODO tomar de parametro
                            //        importe = sobrante,
                            //        saldo = sobrante,
                            //        referenc = header.venta,
                            //        observa = item.Vale, //????
                            //        idusuario = header.idusuario,
                            //        fum = now
                            //    };
                            //    ctxcr.ContraVales.Add(cv);

                            //    cvales.Add(new ContraValeResponse {
                            //        Vale = item.Vale,
                            //        Caducidad = cv.caduca.Value,
                            //        ContraVale = cv.cvale,
                            //        Importe = cv.importe.Value
                            //    });
                            //}
                        }
                        break;
                    case FormaPago.CV:
                        {
                            var cvale = ctxcr.ContraVales.Where(i => i.sucursal == item.Sucursal && i.cvale == item.Vale).Single();
                            detalle.cvale = $"{cvale.sucursal}{cvale.cvale}";

                            if (now > cvale.caduca)
                                throw new VigenciaVencidaExcepcion();
                            
                            var dist = ctxcr.Distribuidores.Where(i => i.distrib == cvale.distrib).Single();

                            var disponible = cvale.saldo ?? 0;
                            disponible = Math.Min(cvale.saldo.Value, dist.disponible.Value);

                            if (item.Importe > disponible)
                                throw new  NoDisponibleExcepcion();

                            cvale.saldo -= item.Importe;

                            dist.disponible = dist.disponible - item.Importe - blindaje;
                            dist.saldo = dist.saldo + item.Importe + blindaje;
                            this.GenerarPlanPagos(now, model, idcajero, item, header, cvales, cliente, succli, dist);

                            //var plan = new DataAccess.SirCoCredito.PlanPagos
                            //{
                            //    distrib = dist.distrib,
                            //    sucursal = model.Sucursal,
                            //    nota = header.venta,
                            //    negocio = "TO",
                            //    vale = item.Vale,
                            //    desctoori = dist.desctoori,
                            //    succliente = succ.sucursal,
                            //    cliente = cliente.cliente,
                            //    idcliente = cliente.idcliente,
                            //    fechaaplicarcorte = item.FechaAplicar,
                            //    fechacompra = now,
                            //    status = "AP",
                            //    importe = item.Importe,
                            //    saldo = item.Importe,
                            //    pagos = item.Plazos.Value,
                            //    pagado = "0",
                            //    observacion = "",
                            //    idusuario = model.CajeroId,
                            //    fum = now
                            //};
                            //plan.Detalle = new HashSet<DataAccess.SirCoCredito.PlanPagosDetalle>();

                            //var part = item.Importe / item.Plazos.Value;
                            //var pagos = Math.Ceiling(part);
                            //var restante = pagos * (item.Plazos.Value - 1);
                            //var faltante = item.Importe - restante;

                            //var fechas = ctxcr.Calendarios.Where(i => i.tipo == "CORTE" && i.tipocredito == "DISTRIBUIDOR"
                            //    && i.fechaaplicarcorte >= item.FechaAplicar)
                            //    .OrderBy(i => i.fechaaplicarcorte).Take(item.Plazos.Value).ToArray();

                            //for (int i = 0; i < item.Plazos; i++)
                            //{
                            //    var fecha = fechas[i];
                            //    var det = new DataAccess.SirCoCredito.PlanPagosDetalle
                            //    {
                            //        sucursal = model.Sucursal,
                            //        nota = header.venta,
                            //        fechaaplicar = fecha.fechaaplicarcorte.Value,
                            //        pago = (i + 1),
                            //        pagos = item.Plazos.Value,
                            //        fechavencimiento = fecha.fechaaplicar.Value,
                            //        importe = (i + 1) == item.Plazos ? faltante : pagos,
                            //        abono = 0,
                            //        descuento = 0,
                            //        interes = 0,
                            //        gastoscobranza = 0,
                            //        pagado = "0",
                            //        fechapago = _empty,
                            //        tipopago = ' ',
                            //        cobrador = 0,
                            //        idpago = 0,
                            //        idconvenio = 0,
                            //        idusuario = model.CajeroId,
                            //        fum = now
                            //    };
                            //    plan.Detalle.Add(det);
                            //}

                            //ctxcr.PlanPagos.Add(plan);
                        }
                        break;
                    case FormaPago.CD:
                        {
                            item.FormaPago = FormaPago.VA;
                            var dist = ctxcr.Distribuidores.Where(i => i.distrib == item.Distribuidor).Single();

                            if (item.Importe > dist.disponible)
                                throw new NoDisponibleExcepcion();

                            if (dist.clientedi == null)
                            {
                                dist.succtedi = succli.sucursal;
                                dist.clientedi = cliente.cliente;
                            }

                            item.Vale = int.Parse(item.Distribuidor).ToString();
                            //item.FechaAplicar = now;
                            dist.disponible = dist.disponible - item.Importe - blindaje;
                            dist.saldo = dist.saldo + item.Importe + blindaje;

                            this.GenerarPlanPagos(now, model, idcajero, item, header, cvales, cliente, succli, dist);

                            //var plan = new DataAccess.SirCoCredito.PlanPagos
                            //{
                            //    distrib = dist.distrib,
                            //    sucursal = model.Sucursal,
                            //    nota = header.venta,
                            //    negocio = "TO",
                            //    vale = int.Parse(item.Distribuidor).ToString(),
                            //    desctoori = dist.desctoori,
                            //    succliente = succ.sucursal,
                            //    cliente = cliente.cliente,
                            //    idcliente = cliente.idcliente,
                            //    fechaaplicarcorte = item.FechaAplicar,
                            //    fechacompra = now,
                            //    status = "AP",
                            //    importe = item.Importe,
                            //    saldo = item.Importe,
                            //    pagos = item.Plazos.Value,
                            //    pagado = "0",
                            //    observacion = "",
                            //    idusuario = model.CajeroId,
                            //    fum = now
                            //};
                            //plan.Detalle = new HashSet<DataAccess.SirCoCredito.PlanPagosDetalle>();

                            //var part = item.Importe / item.Plazos.Value;
                            //var pagos = Math.Ceiling(part);
                            //var restante = pagos * (item.Plazos.Value - 1);
                            //var faltante = item.Importe - restante;

                            //var fechas = ctxcr.Calendarios.Where(i => i.tipo == "CORTE" && i.tipocredito == "DISTRIBUIDOR"
                            //    && i.fechaaplicarcorte >= now)
                            //    .OrderBy(i => i.fechaaplicarcorte).Take(item.Plazos.Value).ToArray();

                            //for (int i = 0; i < item.Plazos; i++)
                            //{
                            //    var fecha = fechas[i];
                            //    var det = new DataAccess.SirCoCredito.PlanPagosDetalle
                            //    {
                            //        sucursal = model.Sucursal,
                            //        nota = header.venta,
                            //        fechaaplicar = fecha.fechaaplicarcorte.Value,
                            //        pago = (i + 1),
                            //        pagos = item.Plazos.Value,
                            //        fechavencimiento = fecha.fechaaplicar.Value,
                            //        importe = (i + 1) == item.Plazos ? faltante : pagos,
                            //        abono = 0,
                            //        descuento = 0,
                            //        interes = 0,
                            //        gastoscobranza = 0,
                            //        pagado = "0",
                            //        fechapago = _empty,
                            //        tipopago = ' ',
                            //        cobrador = 0,
                            //        idpago = 0,
                            //        idconvenio = 0,
                            //        idusuario = model.CajeroId,
                            //        fum = now
                            //    };
                            //    plan.Detalle.Add(det);
                            //}

                            //ctxcr.PlanPagos.Add(plan);
                        }
                        break;
                    default:
                        throw new FormaPagoNoSoportadaExcepcion();
                }
            }

            try
            {
                ctxc.SaveChanges();
            }
            catch (Exception)
            {
                throw new ActualizaFolioSucExcepcion();
            }
            try
            {
                ctxpv.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ActualizaVentaExcepcion(ex.Message);
            }
            try
            {
                ctxcr.SaveChanges();
            }
            catch (Exception)
            {

                throw new ActualizaCreditoExcepcion();
            }
            try
            {
                ctx.SaveChanges();
            }
            catch (Exception)
            {
                throw new ActualizaSerieExcepcion();
            }
            
            if ((promos.Monedero ?? 0) > 0 && cliente != null)
            {
                var md = ctx.Dineros.Where(i => i.idsucursal == cliente.idsucursal && i.cliente == cliente.cliente).SingleOrDefault();
                if (md == null)
                {
                    md = new DataAccess.SirCo.Dinero
                    {
                        idsucursal = cliente.idsucursal.Value,
                        cliente = cliente.cliente,
                        vigencia = null,
                        importe = null,
                        saldo = 0,
                        idusuario = idcajero,
                        fum = now
                    };
                    ctx.Dineros.Add(md);
                }
                if (!md.saldo.HasValue)
                    md.saldo = 0;
                md.saldo += promos.Monedero;
                var mdet = new DataAccess.SirCo.DineroDetalle
                {
                    idsucursal = md.idsucursal,
                    cliente = md.cliente,
                    sucnota = header.sucursal,
                    nota = header.venta,
                    descrip = "PROMOCION",
                    vigencia = null, //TODO duda ????????????
                    importe = promos.Monedero,
                    saldo = promos.Monedero,
                    tipo = null, //TODO duda ??????????????
                    estatus = "AC",
                    idusuario = idcajero,
                    fum = now
                };
                ctx.DinerosDetalle.Add(mdet);
                ctx.SaveChanges();
            }

            return new SaleResponse
            {
                Folio = header.venta,
                Cliente = header.idcliente,
                ContraVales = cvales,
                Monedero = promos.Monedero
            };
        }



        private int? AddClienteFromModel(Cliente cliente, int idsucursal)
        {
            if (cliente == null)
                return null;

            if (!cliente.Id.HasValue)
            {
                if (cliente.DistribuidorId.HasValue)
                {
                    var ctxcr = new DataAccess.SirCoCreditoDataContext();
                    var dist = ctxcr.Distribuidores.Where(i => i.iddistrib == cliente.DistribuidorId).Single();
                    var ncliente = new Cliente
                    {
                        SucursalId = idsucursal,
                        NombreCompleto = dist.nombrecompleto,
                        Nombre = dist.nombre,
                        ApPaterno = dist.appaterno,
                        ApMaterno = dist.apmaterno,
                        Sexo = dist.sexo,
                        Estado = dist.idestado,
                        Ciudad = dist.idciudad,
                        Colonia = dist.idcolonia,
                        CodigoPostal = dist.codigopostal.ToString(),
                        Calle = dist.calle,
                        Numero = dist.numero,
                        Celular = dist.celular1,
                        Email = dist.email
                    };
                    return this.AddCliente(ncliente);
                }
                else
                    return this.AddCliente(cliente);
            }
            else
                return cliente.Id;
        }
        private void GenerarPlanPagos(DateTime now, SaleRequest model, int idcajero,
            Pago item, DataAccess.SirCoPV.Venta header, ICollection<ContraValeResponse> cvales,
            DataAccess.SirCoCredito.Cliente cliente, DataAccess.SirCoControl.Sucursal succli,
            DataAccess.SirCoCredito.Distribuidor dist)
        {
            var ctxcr = new DataAccess.SirCoCreditoDataContext();
            var ctxc = new DataAccess.SirCoControlDataContext();
            var helper = new BusinessLogic.Data();
            var blindaje = helper.GetParametro<decimal?>(Parametros.BLINDAJE);

            var negocio = "TO";
            if (item.Negocio.HasValue)
            {
                negocio = ctxc.NegociosExternos.Where(i => i.idnegexterno == item.Negocio)
                    .Single().negocio;
            }
            string observa = "";
            if (item.ProductosPlazos.Sum(i=>i.Importe).Value > 0  && item.Importe > 0) {
                observa = "VentaCombinada";
            }
            var plan = new DataAccess.SirCoCredito.PlanPagos
            {
                distrib = dist.distrib,
                sucursal = model.Sucursal,
                nota = header.venta,
                //negocio = "TO", // negocio para vales externos
                negocio = negocio,
                vale = item.Vale,
                desctoori = dist.desctoori,
                succliente = succli.sucursal,
                cliente = cliente.cliente,
                idcliente = cliente.idcliente,
                //fechaaplicarcorte = item.FechaAplicar,
                fechacompra = DateTime.Parse(now.ToString("yyyy-MM-dd")) ,
                status = "AP",
                importe = item.Importe,
                saldo = item.Importe + (blindaje ?? 0),
                //pagos = item.Plazos.Value,
                pagado = "0",


                observacion = observa,



                idusuario = idcajero,
                fum = now,
                blindaje = blindaje
            };
            plan.Detalle = new HashSet<DataAccess.SirCoCredito.PlanPagosDetalle>();
            /*
            var h = new Common.Helpers.CommonHelper();
            var dps = new List<Tuple<int, decimal>>();
            var quitar = item.ProductosPlazos?.Sum(i => i.Importe.Value);

            DataAccess.SirCoCredito.Calendario[] fechasPromocion = null;
            IDictionary<DateTime, decimal> detallePagosPromocion = null;
            if (item.Plazos.HasValue)
            {
                dps.Add(new Tuple<int, decimal>(item.Plazos.Value, item.Importe - (quitar ?? 0)));

                var primero = ctxcr.Calendarios.Where(i => i.tipo == "CORTE" && i.tipocredito == dist.clasificacion
                    && i.fechaaplicarcorte < item.FechaAplicar)
                    .OrderByDescending(i => i.fechaaplicarcorte).Take(1).First();

                var list = ctxcr.Calendarios.Where(i => i.tipo == "CORTE" && i.tipocredito == dist.clasificacion
                    && i.fechaaplicarcorte >= item.FechaAplicar)
                    .OrderBy(i => i.fechaaplicarcorte).Take(item.Plazos.Value - 1).ToList();

                list.Insert(0, primero);
                fechasPromocion = list.ToArray();

                detallePagosPromocion = h.GetPlazos(
                    fechas: fechasPromocion.Select(i => i.fechaaplicar.Value).ToArray(),
                    ip: dps.ToArray());
            }
            IDictionary<DateTime, decimal> detallePagos = new Dictionary<DateTime, decimal>();
            var fechas = new HashSet<DataAccess.SirCoCredito.Calendario>();
            if (item.ProductosPlazos?.Any() ?? false)
            {
                var plazosNoPromocion = item.ProductosPlazos.Max(i => i.Plazos.Value);
                var fechasNoPromocion = ctxcr.Calendarios.Where(i => i.tipo == "CORTE" && i.tipocredito == dist.clasificacion
                    && i.fechaaplicarcorte >= now)
                    .OrderBy(i => i.fechaaplicarcorte).Take(plazosNoPromocion).ToArray();
                dps.Clear();
                foreach (var pps in item.ProductosPlazos)
                {
                    dps.Add(new Tuple<int, decimal>(pps.Plazos.Value, pps.Importe.Value));
                }

                var detallePagosNoPromocion = h.GetPlazos(
                    fechas: fechasNoPromocion.Select(i => i.fechaaplicar.Value).ToArray(),
                    ip: dps.ToArray());

                
                foreach (var dp in detallePagosNoPromocion)
                {
                    if (!detallePagos.ContainsKey(dp.Key))
                        detallePagos.Add(dp.Key, 0);
                    detallePagos[dp.Key] += dp.Value;
                }
                foreach (var f in fechasNoPromocion)
                {
                    fechas.Add(f);
                }
            }
            if (item.Plazos.HasValue)
            {
                foreach (var dp in detallePagosPromocion)
                {
                    if (!detallePagos.ContainsKey(dp.Key))
                        detallePagos.Add(dp.Key, 0);
                    detallePagos[dp.Key] += dp.Value;
                }
                foreach (var f in fechasPromocion)
                {
                    fechas.Add(f);
                }
            }
            */
            var fechas = new HashSet<DataAccess.SirCoCredito.Calendario>();
            var detallePagos = this.GenerarPlanPagosFechas(now, item, dist, fechas, blindaje);

            var index = 0;
            foreach (var dp in detallePagos)
            {
                var fecha = fechas.Where(i => i.fechaaplicar == dp.Key).Single();
                if (index == 0)
                {
                    plan.pagos = detallePagos.Count;
                    plan.fechaaplicarcorte = fecha.fechaaplicarcorte.Value;
                }
                var det = new DataAccess.SirCoCredito.PlanPagosDetalle
                {
                    sucursal = model.Sucursal,
                    nota = header.venta,
                    fechaaplicar = fecha.fechaaplicar.Value,
                    pago = index + 1,
                    pagos = detallePagos.Count,
                    fechavencimiento = GetFechaVencimiento(fecha.fechaaplicar.Value),
                    importe = dp.Value,
                    abono = 0,
                    descuento = 0,
                    interes = 0,
                    gastoscobranza = 0,
                    pagado = "0",
                    fechapago = _empty,
                    tipopago = ' ',
                    cobrador = 0,
                    idpago = 0,
                    idconvenio = 0,
                    idusuario = idcajero,
                    fum = now,

                    negocio = plan.negocio,
                    vale = plan.vale,
                    distrib = plan.distrib
                };
                plan.Detalle.Add(det);
                index++;
            }

            ctxcr.PlanPagos.Add(plan);

            var sobrante = (item.Limite ?? 0) - item.Importe;
            if (dist.contravale == 1 && item.ContraVale && item.Importe < (item.Limite ?? 0)
                && item.FormaPago == FormaPago.VA)
            {
                var suc = ctxc.Sucursales.Where(i => i.sucursal == model.Sucursal).Single();

                var cvale = (suc.cvale ?? 0) + 1;
                suc.cvale = cvale;

                var cv = new DataAccess.SirCoCredito.ContraVale
                {
                    sucursal = header.sucursal,
                    cvale = cvale.ToString("000000"),
                    status = "GE",
                    fecha = now,
                    distrib = dist.distrib,
                    succte = succli.sucursal,
                    cliente = cliente.cliente,
                    caduca = now.AddDays(15), //TODO tomar de parametro
                    importe = sobrante,
                    saldo = sobrante,
                    referenc = header.venta,
                    observa = item.Vale, //????
                    idusuario = header.idusuario,
                    fum = now
                };
                ctxcr.ContraVales.Add(cv);

                cvales.Add(new ContraValeResponse
                {
                    Vale = item.Vale,
                    Caducidad = cv.caduca.Value,
                    ContraVale = cv.cvale,
                    Importe = cv.importe.Value
                });
            }
            ctxc.SaveChanges();

            try
            {
                ctxcr.SaveChanges();
            }
            catch (Exception)
            {
                throw new AgregandoPlanPagosExcepcion();
            }
        }
        public IDictionary<DateTime, decimal> GenerarPlanPagosFechas(DateTime now, Pago item,
            DataAccess.SirCoCredito.Distribuidor dist,
            HashSet<DataAccess.SirCoCredito.Calendario> fechas,
            decimal? blindaje)
        {
            IDictionary<DateTime, decimal> detallePagosTotal = new Dictionary<DateTime, decimal>();
            IDictionary<DateTime, decimal> detallePagosPromocion = null;
            IDictionary<DateTime, decimal> detallePagosNoPromocion = null;

            var ctxcr = new DataAccess.SirCoCreditoDataContext();
            var h = new Common.Helpers.CommonHelper();
            var dps = new List<Tuple<int, decimal>>();

            var SumImporteSinPromo = item.ProductosPlazos?.Sum(i => i.Importe.Value);

            DataAccess.SirCoCredito.Calendario[] fechasPromocion = null;
            string tipocredito = "DISTRIBUIDOR";
            if (item.FormaPago == FormaPago.CP)
            {
                tipocredito = "TARJETAHABIENTE";
            }

            // Genera Fechas Pago (Parrilla) Normal con Promocion (No Electronica)
                if (item.Plazos.HasValue)
            {
                double plazosPromocion = item.Plazos.Value ;

                dps.Add(new Tuple<int, decimal>((int)plazosPromocion, item.Importe - (SumImporteSinPromo ?? 0)));

                var primero = ctxcr.Calendarios.Where(i => i.tipo == "CORTE" && i.tipocredito == tipocredito
                    && i.fechaaplicarcorte <= item.FechaAplicar && i.fechaaplicarcorte > DateTime.Now)
                    .OrderByDescending(i => i.fechaaplicarcorte).Take(1).FirstOrDefault();

                if (primero != null)
                {
                    plazosPromocion--;
                }

                var ultimo = ctxcr.Calendarios.Where(i => i.tipo == "CORTE" && i.tipocredito == tipocredito
                    && i.fechaaplicarcorte > item.FechaAplicar)
                    .OrderBy(i => i.fechaaplicarcorte).Take((int)plazosPromocion).ToList();

                if (primero != null) {   
                    ultimo.Insert(0, primero);
                }

                fechasPromocion = ultimo.ToArray();

                detallePagosPromocion = h.GetPlazos(
                    fechas: fechasPromocion.Select(i => i.fechaaplicar.Value).ToArray(),
                    ip: dps.ToArray());
            }

            // Genera Plan de Pagos NO Promocion (Parrilla Electronica)
            if (item.ProductosPlazos?.Where(i=>i.Plazos!=null).Any() ?? false)
            {
                double plazosNoPromocion = item.ProductosPlazos.Where(i=>i.Plazos !=null).Max(i => i.Plazos.Value);
                var fechasNoPromocion = ctxcr.Calendarios.Where(i => i.tipo == "CORTE" && i.tipocredito == tipocredito
                    && i.fechaaplicarcorte >= now)
                    .OrderBy(i => i.fechaaplicarcorte).Take((int)plazosNoPromocion).ToArray();

                dps.Clear();
                var prods = item.ProductosPlazos.Where(i => i.Plazos != null);
                foreach (var pps in prods)
                {
                    double plazosElectronica = pps.Plazos.Value;

                    dps.Add(new Tuple<int, decimal>((int)plazosElectronica, pps.Importe.Value));
                }

                detallePagosNoPromocion = h.GetPlazos(
                    fechas: fechasNoPromocion.Select(i => i.fechaaplicar.Value).ToArray(),
                    ip: dps.ToArray());

                foreach (var dp in detallePagosNoPromocion)
                {
                    if (!detallePagosTotal.ContainsKey(dp.Key))
                        detallePagosTotal.Add(dp.Key, 0);
                    detallePagosTotal[dp.Key] += dp.Value;
                }
                foreach (var f in fechasNoPromocion)
                {
                    fechas.Add(f);
                }
            }

            // Genera Parrilla NO Electronica y los acumula en la Parrilla Electronica
            if (item.Plazos.HasValue)
            {
                foreach (var dp in detallePagosPromocion)
                {
                    if (!detallePagosTotal.ContainsKey(dp.Key))
                        detallePagosTotal.Add(dp.Key, 0);
                    detallePagosTotal[dp.Key] += dp.Value;
                }
                foreach (var f in fechasPromocion)
                {
                    fechas.Add(f);
                }
            }

            if (blindaje.HasValue && detallePagosTotal.Any())
            {
                var first = detallePagosTotal.First();
                detallePagosTotal[first.Key] += blindaje.Value;
            }
            return detallePagosTotal;
        }

        private DateTime GetFechaVencimiento(DateTime current)
        {
            int find = 0;
            if (current.Day == 17)
                find = 2;
            if (current.Day == 2)
                find = 17;
            if (find == 0)
                return current.AddDays(15);
            do
            {
                current = current.AddDays(1);
            }
            while (current.Day != find);
            return current;
        }

         public string Return(ReturnRequest model, int idcajero, string sucursal)
        {
            var ctx = new DataAccess.SirCoDataContext();
            var ctxpv = new DataAccess.SirCoPVDataContext();
            var ctxc = new DataAccess.SirCoControlDataContext();
            var sale = ctxpv.Ventas.Where(i => i.sucursal == model.Sucursal && i.venta == model.Folio).SingleOrDefault();
            if (sale == null)
                throw new VentaNoExisteExcepcion();
            var now = Helpers.Common.GetNow();

            var suc = ctxc.Sucursales.Where(i => i.sucursal == sucursal).Single();
            var folio = suc.devolvta + 1;
            suc.devolvta = folio;
            ctxc.SaveChanges();

            var devolucion = new DataAccess.SirCoPV.Devolucion
            {
                sucursal = sucursal,
                devolvta = folio.Value.ToString("000000"),
                tipo = "DEVOLUCION",
                fecha = now,
                estatus = "AP",
                referencia = $"{model.Sucursal}{model.Folio}",
                comentarios = model.Comments,
                idcajero = idcajero,
                idvendedor = sale.idvendedor,
                idusuario = idcajero,
                fum = now,
                idusuariocancela = 0,
                //fumcancela = null ??
                //disponible                 
            };
            if (model.Cliente != null)
                devolucion.idcliente = this.AddClienteFromModel(model.Cliente, suc.idsucursal);
            devolucion.Detalles = new HashSet<DataAccess.SirCoPV.DevolucionDetalle>();

            short renglon = 0;
            foreach (var serie in model.Items)
            {
                var prod = sale.Detalles.Where(i => i.serie == serie).Single();

                //TODO se quita restriccion de sucursal para permitir cambiar un producto del cual aun no se hace traspaso
                var pitem = ctx.Series.Where(i => /*i.sucursal == sale.sucursal &&*/ i.serie == prod.serie).Single();
                if (pitem.status != Status.BA.ToString())
                    throw new StatusSerieExcepcion();

                ctx.UpdateSerieStatus(prod.serie, Status.AC, Status.BA, idusuario: idcajero);
                var dd = new DataAccess.SirCoPV.DevolucionDetalle
                {
                    //sucursal = null,
                    //venta = null,
                    renglon = ++renglon,
                    marca = prod.marca,
                    estilon = prod.estilon,
                    corrida = prod.corrida,
                    medida = prod.medida,
                    serie = prod.serie,
                    idpromocion = prod.idpromocion,
                    ctd = prod.ctd,
                    precio = prod.precio,
                    precdesc = prod.precdesc,
                    costomargen = prod.costomargen,
                    iva = prod.iva,
                    idusuario = idcajero,
                    fum = now
                };

                if (model.Razones != null)
                {
                    dd.idrazon = model.Razones[prod.serie].TipoRazon;
                    dd.notas = model.Razones[prod.serie].Notas;
                }
                devolucion.Detalles.Add(dd);
            }
            devolucion.disponible = devolucion.Detalles.Sum(i => i.precdesc.Value);
            ctxpv.Devoluciones.Add(devolucion);
            ctxpv.SaveChanges();
            return devolucion.devolvta;
        }
        //public string ReturnMonedero(ReturnRequest model)
        //{
        //    var ctx = new DataAccess.SirCoDataContext();
        //    var ctxpv = new DataAccess.SirCoPVDataContext();
        //    var ctxc = new DataAccess.SirCoControlDataContext();
        //    var sale = ctxpv.Ventas.Where(i => i.sucursal == model.Sucursal && i.venta == model.Folio).SingleOrDefault();
        //    if (sale == null)
        //        return null;
        //    var now = Helpers.Common.GetNow();

        //    var suc = ctxc.Sucursales.Where(i => i.sucursal == model.Sucursal).Single();
        //    var folio = suc.devolvta + 1;
        //    suc.devolvta = folio;
        //    ctxc.SaveChanges();

        //    var monedero = new DataAccess.SirCoAPP.Dinero
        //    {
        //        //public int idsucursal { get; set; }
        //        //public string cliente { get; set; }
        //        //public DateTime? vigencia { get; set; }
        //        //public decimal? importe { get; set; }
        //        //public decimal? saldo { get; set; }
        //        //public int? idusuario { get; set; }
        //        //public DateTime? fum { get; set; }
        //    };
        //    monedero.Detalles = new HashSet<DataAccess.SirCoAPP.DineroDetalle>();

        //    short renglon = 0;
        //    foreach (var serie in model.Items)
        //    {
        //        var prod = sale.Detalles.Where(i => i.serie == serie).Single();

        //        var pitem = ctx.Series.Where(i => i.sucursal == sale.sucursal && i.serie == prod.serie).Single();
        //        if (pitem.status != Status.BA.ToString())
        //            throw new NotSupportedException();

        //        ctx.UpdateSerieStatus(prod.serie, Status.AC, Status.BA);
        //        //devolucion.Detalles.Add(new DataAccess.SirCoPV.DevolucionDetalle
        //        //{
        //        //    //sucursal = null,
        //        //    //venta = null,
        //        //    renglon = ++renglon,
        //        //    marca = prod.marca,
        //        //    estilon = prod.estilon,
        //        //    corrida = prod.corrida,
        //        //    medida = prod.medida,
        //        //    serie = prod.serie,
        //        //    idpromocion = prod.idpromocion,
        //        //    ctd = prod.ctd,
        //        //    precio = prod.precio,
        //        //    precdesc = prod.precdesc,
        //        //    costomargen = prod.costomargen,
        //        //    iva = prod.iva,
        //        //    idusuario = 0,
        //        //    fum = now
        //        //});
        //    }
        //    //devolucion.disponible = devolucion.Detalles.Sum(i => i.precio.Value);
        //    //ctxpv.Devoluciones.Add(devolucion);
        //    //ctxpv.SaveChanges();
        //    //return devolucion.devolvta;
        //    throw new NotSupportedException();
        //}
        public void CancelSale(CancelSaleRequest model, int idcajero)
        {
            var gid = Guid.NewGuid();
            var today = Helpers.Common.GetToday();
            var now = Helpers.Common.GetNow();
            var ctx = new DataAccess.SirCoDataContext();
            var ctxpv = new DataAccess.SirCoPVDataContext();
            var ctxc = new DataAccess.SirCoCreditoDataContext();
            var venta = ctxpv.Ventas.Where(i => i.sucursal == model.Sucursal && i.venta == model.Folio).Single();
            if (venta.estatus != Common.Constants.StatusVenta.Aplicada || venta.fecha.Value != today)
                throw new CancelacionNoValidaExcepcion();
            venta.estatus = Common.Constants.StatusVenta.Cancelada;
            venta.fumcancela = now;
            venta.idusuariocancela = idcajero;
            venta.motivocancela = model.Motivo;
            var cvale = ctxc.ContraVales.Where(i => i.sucursal == venta.sucursal && i.referenc == venta.venta).SingleOrDefault();
            var pago = ctxpv.Pagos.Where(i => i.sucursal == model.Sucursal && i.pago == model.Folio).Single();
            pago.estatus = Common.Constants.StatusPago.Cancelada;
            foreach (var d in pago.Detalle)
            {
                d.movimientocancela = gid;
                _admin.Cancel(idcajero, model.Sucursal, d.importe.Value, now, gid, (Common.Constants.FormaPago)d.idformapago);

                switch (d.idformapago)
                {
                    case (int)Common.Constants.FormaPago.VE:
                    case (int)Common.Constants.FormaPago.VA:
                        {
                            var plan = ctxc.PlanPagos.Where(i => i.sucursal == model.Sucursal && i.nota == model.Folio).Single();
                            if (plan.pagado == "1")
                                throw new VentaConPagosExcepcion();

                            var planDet = ctxc.PlanPagosDetalle.Where(i => i.sucursal == model.Sucursal && i.nota == model.Folio).Select(i => new { i.pagado, i.abono });
                            if (planDet != null)
                            {
                                foreach (var det in planDet)
                                {
                                    if (det.pagado == "1" || det.abono > 0)
                                        throw new VentaConPagosExcepcion();
                                }
                            }
                            plan.status = "ZC";
                            var cliente = ctxc.Distribuidores.Where(i => i.distrib == plan.distrib).Single();
                            cliente.saldo -= (plan.importe + plan.blindaje);
                            cliente.disponible += (plan.importe + plan.blindaje);
                        }
                        break;
                    case (int)Common.Constants.FormaPago.VD:
                        {
                            var ctxa = new SirCoPOS.DataAccess.SirCoCreditoDataContext();
                            var dev = ctxa.ValesDigital.Where(i => i.idvaledigital == d.idvaledigital).Single();
                            dev.disponible += d.importe;
                            ctxa.SaveChanges();

                            var plan = ctxc.PlanPagos.Where(i => i.sucursal == model.Sucursal && i.nota == model.Folio).Single();
                            if (plan.pagado == "1")
                                throw new VentaConPagosExcepcion();
                            var planDet = ctxc.PlanPagosDetalle.Where(i => i.sucursal == model.Sucursal && i.nota == model.Folio).Select(i => new { i.pagado, i.abono });
                            if (planDet != null)
                            {
                                foreach (var det in planDet)
                                {
                                if (det.pagado == "1" || det.abono > 0)
                                    throw new VentaConPagosExcepcion();
                                }
                            }
                            plan.status = "ZC";
                            var cliente = ctxc.Distribuidores.Where(i => i.distrib == plan.distrib).Single();
                            cliente.saldo -= (plan.importe + plan.blindaje);
                            cliente.disponible += (plan.importe + plan.blindaje);
                        }
                        break;
                    case (int)Common.Constants.FormaPago.CP:
                    case (int)Common.Constants.FormaPago.CD:
                        {
                            var plan = ctxc.PlanPagos.Where(i => i.sucursal == model.Sucursal && i.nota == model.Folio).Single();
                            if (plan.pagado == "1")
                                throw new VentaConPagosExcepcion();
                            var planDet = ctxc.PlanPagosDetalle.Where(i => i.sucursal == model.Sucursal && i.nota == model.Folio).Select(i => new { i.pagado, i.abono });
                            if (planDet != null)
                            {
                                foreach (var det in planDet)
                                {
                                if (det.pagado == "1" || det.abono > 0)
                                    throw new VentaConPagosExcepcion();
                                }
                            }
                            plan.status = "ZC";
                            var cliente = ctxc.Distribuidores.Where(i => i.distrib == plan.distrib).Single();
                            cliente.saldo -= (plan.importe + plan.blindaje);
                            cliente.disponible += (plan.importe + plan.blindaje);
                        }
                        break;
                    case (int)Common.Constants.FormaPago.CV:
                        {
                            var suc = d.cvale.Substring(0, 2);
                            var folio = d.cvale.Substring(2);
                            var dev = ctxc.ContraVales.Where(i => i.sucursal == suc && i.cvale == folio).Single();
                            dev.saldo += d.importe;
                            ctxpv.SaveChanges();

                            var plan = ctxc.PlanPagos.Where(i => i.sucursal == model.Sucursal && i.nota == model.Folio).Single();
                            if (plan.pagado == "1")
                                throw new VentaConPagosExcepcion();
                            var planDet = ctxc.PlanPagosDetalle.Where(i => i.sucursal == model.Sucursal && i.nota == model.Folio).Select(i => new { i.pagado, i.abono });
                            if (planDet != null)
                            {
                                foreach (var det in planDet)
                                {
                                if (det.pagado == "1" || det.abono > 0)
                                    throw new VentaConPagosExcepcion();
                                }
                            }
                            plan.status = "ZC";
                            var cliente = ctxc.Distribuidores.Where(i => i.distrib == plan.distrib).Single();
                            cliente.saldo -= (plan.importe + plan.blindaje);
                            cliente.disponible += (plan.importe + plan.blindaje);
                        }
                        break;
                    case (int)Common.Constants.FormaPago.DV:
                        {
                            var suc = d.referencia.Substring(0, 2);
                            var folio = d.referencia.Substring(2);
                            var dev = ctxpv.Devoluciones.Where(i => i.sucursal == suc && i.devolvta == folio).Single();
                            dev.estatus = "AP";
                            dev.disponible += d.importe;
                            ctxpv.SaveChanges();
                        }
                        break;
                    case (int)Common.Constants.FormaPago.TC:
                    case (int)Common.Constants.FormaPago.TD:
                        //TODO registrar folio de cancelacion/rembolso
                        break;
                    case (int)Common.Constants.FormaPago.GO:
                    case (int)Common.Constants.FormaPago.EF:
                    case (int)Common.Constants.FormaPago.KU:
                        //d.movimientocancela = _admin.Cancel(idcajero, model.Sucursal, d.importe.Value, now);
                        break;
                    case (int)Common.Constants.FormaPago.MD:
                        {
                            var ctxcc = new DataAccess.SirCoControlDataContext();
                            //var suc = ctxcc.Sucursales.Where(i => i.sucursal == model.Sucursal).Single();
                            var cli = ctxc.Clientes.Where(i => i.idcliente == venta.idcliente).Single();
                            var ctxa = new DataAccess.SirCoDataContext();
                            var dinero = ctxa.Dineros.Where(i => i.idsucursal == cli.idsucursal && i.cliente == cli.cliente).Single();
                            dinero.saldo += d.importe;
                            var qdet = dinero.Detalles.Where(i => i.importe > i.saldo).OrderByDescending(i => i.vigencia);
                            var count = 0m;
                            foreach (var item in qdet)
                            {
                                var mis = d.importe.Value - count;
                                var rem = item.importe - item.saldo;
                                if (mis > rem)
                                {
                                    item.saldo += rem.Value;
                                    count += rem.Value;
                                }
                                else
                                {
                                    item.saldo += mis;
                                    count += mis;
                                }
                                if (count == d.importe.Value)
                                    break;
                            }
                            ctxa.SaveChanges();
                        }
                        break;
                    default:
                        throw new FormaPagoNoSoportadaExcepcion();
                }
            }
            var dif = venta.fumcancela - venta.fum;
            foreach (var d in venta.Detalles)
            {
                if (dif.Value.TotalMinutes > 15)
                    ctx.UpdateSerieStatus(d.serie, Status.AC, Status.BA, idusuario: idcajero);
                else
                    ctx.UpdateSerieStatus(d.serie, Status.AB, Status.BA, idusuario: idcajero);
            }
            if (cvale !=null)
            {
                cvale.status = "ZC";
            }
            
            ctxpv.SaveChanges();
            ctxc.SaveChanges();

        }
        public void CancelReturn(string sucursal, string folio, int idusuario)
        {
            var ctx = new DataAccess.SirCoDataContext();
            var ctxpv = new DataAccess.SirCoPVDataContext();
            var devol = ctxpv.Devoluciones.Where(i => i.sucursal == sucursal && i.devolvta == folio).Single();
            devol.estatus = "ZC";
            devol.idusuariocancela = idusuario;
            devol.fumcancela = DateTime.Now;
            foreach (var item in devol.Detalles)
            {
                ctx.UpdateSerieStatus(item.serie, Status.BA, Status.AC, idusuario: idusuario);
            }
            ctxpv.SaveChanges();
        }

        public Common.Entities.ChangeResponse Change(Common.Entities.ChangeRequest request, int idcajero, string sucursal)
        {
            var ctx = new DataAccess.SirCoDataContext();
            var ctxpv = new DataAccess.SirCoPVDataContext();
            decimal pago = 0, total = 0, desc = 0;
            foreach (var item in request.Items)
            {
                item.Corrida = false;
                item.Pago = null;
                var oitem = ctx.Series.Where(i => i.serie == item.OldItem).Single();
                var nitem = ctx.Series.Where(i => i.serie == item.NewItem).Single();

                //var ocor = ctx.Corridas.Where(i => i.marca == oitem.marca && i.estilon == oitem.estilon && i.proveedor == oitem.proveedors
                //    && String.Compare(oitem.medida, i.medini) >= 0 && String.Compare(oitem.medida, i.medfin) <= 0).Single();
                //var ncor = ctx.Corridas.Where(i => i.marca == nitem.marca && i.estilon == nitem.estilon && i.proveedor == nitem.proveedors
                //    && String.Compare(nitem.medida, i.medini) >= 0 && String.Compare(nitem.medida, i.medfin) <= 0).Single();
                var ocor = ctx.GetCorrida(oitem);
                var ncor = ctx.GetCorrida(nitem);

                var det = ctxpv.VentasDetalle.Where(i => i.sucursal == request.Sucursal && i.venta == request.Folio
                    && i.serie == item.OldItem).Single();
                //pago += ncor.precio.Value;
                pago += det.precdesc.Value;
                total += ncor.precio.Value;

                //mismo modelo
                if (ocor.marca == ncor.marca
                    && ocor.estilon == ncor.estilon)
                {
                    item.Corrida = true;
                    if (ocor.corrida == ncor.corrida)
                    {
                        //opcion de permitir disponible
                        //if (det.precio < ncor.precio)
                        //{
                        //    item.Pago = det.precdesc.Value;
                        //    item.Precio = det.precio;
                        //}
                        //else if (det.precio > ncor.precio)
                        //{
                        //    var des = det.precio - det.precdesc;
                        //    var per = des / det.precio;

                        //    var ndesc = ncor.precio.Value * per;
                        //    var npre = ncor.precio.Value - (ndesc ?? 0);

                        //    item.Pago = npre;
                        //}
                        //else
                        //    item.Pago = det.precdesc.Value;

                        //sin permitir disponible
                        item.Pago = det.precdesc.Value;
                        item.Precio = det.precio;
                    }
                    else if (det.precio > ncor.precio)
                    {
                        var des = det.precio - det.precdesc.Value;
                        var per = des / det.precio;
                        var pagar = ncor.precio - ncor.precio * per;
                        item.Pago = pagar.Value;
                    }
                    else if (det.precio < ncor.precio)
                    {
                        var dif = ncor.precio - det.precio;
                        item.Pago = det.precdesc.Value + dif;
                    }
                    else
                    {
                        item.Pago = det.precdesc.Value;
                    }
                    //continue;
                }
                else
                {
                    item.Pago = ncor.precio.Value;
                }
                desc += item.Pago.Value;
                ////misma corrida
                //if (ocor.marca == ncor.marca
                //    && ocor.estilon == ncor.estilon
                //    && ocor.proveedor == ncor.proveedor
                //    && ocor.corrida == ncor.corrida
                //    && ocor.medini == ncor.medini
                //    && ocor.medfin == ncor.medfin)
                //{
                //    continue;
                //}
                ////corrida equivalente
                //if (ocor.marca == ncor.marca
                //    && ocor.estilon == ncor.estilon
                //    && ocor.corrida == ncor.corrida)
                //{
                //    continue;
                //}
                ////mismo modelo, dif corrida, mismo precio
                //if (ocor.marca == ncor.marca
                //    && ocor.estilon == ncor.estilon
                //    && ocor.precio == ncor.precio)
                //{
                //    continue;
                //}
                ////mismo modelo, dif corrida, precio menor
                //if (ocor.marca == ncor.marca
                //    && ocor.estilon == ncor.estilon
                //    && ocor.precio > ncor.precio)
                //{
                //    continue;
                //}
                ////dif producto, mismo precio, sin descuento
                //var ditem = ctxpv.VentasDetalle.Where(i => i.Header.estatus == "AP"
                //    && i.sucursal == request.Sucursal && i.venta == request.Folio
                //    && i.serie == item.OldItem).Single();
                //if (ditem.precio == ditem.precdesc
                //    && ocor.precio == ncor.precio)
                //{
                //    continue;
                //}
                //throw new NotSupportedException();
            }

            var sale = ctxpv.Ventas.Where(i => i.sucursal == request.Sucursal && i.venta == request.Folio).Single();

            if (request.Cliente == null && sale.idcliente.HasValue)
                request.Cliente = new Cliente { Id = sale.idcliente };

            var ret = new Common.Entities.ReturnRequest
            {
                Sucursal = request.Sucursal,
                Folio = request.Folio,
                Comments = "CAMBIO TALLA",
                Items = request.Items.Select(i => i.OldItem),
                Cliente = request.Cliente,
                Razones = request.Razones
            };
            var rid = this.Return(ret, idcajero, sucursal);

            var dev = ctxpv.Devoluciones.Where(i => i.devolvta == rid && i.sucursal == sucursal).Single();

            decimal pagoDev = 0;
            if (dev.disponible < desc)
                pagoDev = dev.disponible.Value;
            else
                pagoDev = desc;
            var pagos = new List<Common.Entities.Pago>();
            pagos.Add(new Common.Entities.Pago
            {
                FormaPago = FormaPago.DV,
                Sucursal = sucursal,
                Devolucion = rid,
                //Importe = dev.disponible ?? 0 // para dejarlo siempre en 0
                Importe = pagoDev,
                Efectivo = 0,
            });
            if (request.Pagos != null && request.Pagos.Any())
                pagos.AddRange(request.Pagos);
            //else if (dev.disponible < desc) //TODO tmp code
            //{
            //    pagos.Add(new Common.Entities.Pago
            //    {
            //        FormaPago = FormaPago.EF,
            //        Importe = desc - pagoDev
            //    });
            //}

            var sal = new Common.Entities.SaleRequest
            {
                Productos = request.Items.Select(i => new SerieFormasPago
                {
                    Serie = i.NewItem,
                    Precio = i.Precio
                }).ToArray(),
                VendedorId = sale.idvendedor,//?
                Sucursal = sucursal,
                Cliente = request.Cliente,
                Pagos = pagos
            };
            var sid = this.Sale(sal, idcajero, request.Items);
            return new ChangeResponse
            {
                Devolucion = rid,
                Venta = sid.Folio,
                Cliente = sid.Cliente
            };
        }
        public string RegisterNote(int id, int uid)
        {
            var ctxp = new DataAccess.SirCoPOSDataContext();
            var ctx = new DataAccess.SirCoDataContext();
            var item = ctxp.Notas.Where(i => i.Id == id).Single();
            var items = item.Items.Select(i => new Common.Entities.SerieFormasPago
            {
                Serie = i.Serie,
                FormasPago = new Common.Constants.FormaPago[] { FormaPago.EF }
            });
            var total = 0m;
            foreach (var pitem in items)
            {
                var sitem = ctx.Series.Where(i => i.serie == pitem.Serie).Single();
                var corrida = ctx.GetCorrida(sitem);
                total += corrida.precio.Value;
            }
            var request = new Common.Entities.SaleRequest
            {
                VendedorId = item.VendedorId,
                Sucursal = item.Sucursal,
                Productos = items,
                Pagos = new Common.Entities.Pago[] {
                    new Pago { FormaPago = FormaPago.EF, Importe = total }
                }
            };
            var res = this.Sale(request, item.CajeroId);
            item.Venta = res.Folio;
            ctxp.SaveChanges();
            return item.Venta;
        }
        public int Abono(Common.Entities.PagoRequest model)
        {
            //var distrib = "004461";
            //var suc = "01";
            //var pagar = 1000m;
            //var cajero = 0;

            var now = Helpers.Common.GetNow();
            var today = Helpers.Common.GetToday();

            var ctxc = new DataAccess.SirCoControlDataContext();
            var suc = ctxc.Sucursales.Where(i => i.sucursal == model.Sucursal).Single();
            var folio = (suc.abonos ?? 0) + 1;
            suc.abonos = folio;

            var ctx = new DataAccess.SirCoCreditoDataContext();
            var dis = ctx.Distribuidores.Where(i => i.distrib == model.Distribuidor).Single();
            dis.disponible += model.Importe; // se aplica directo o se le suma el descuento?, lo que se descuenta se considera como parte del pago
            dis.saldo -= model.Importe; //misma duda

            var desc = ctx.DescuentosAdicionales.Where(i =>
                String.Compare(model.Distribuidor, i.distribini) >= 0 && String.Compare(model.Distribuidor, i.distribfin) <= 0
                && today >= i.fechaini && today <= i.fechafin
            ).OrderByDescending(i => i.descto).FirstOrDefault();

            var pago = new DataAccess.SirCoCredito.Pagos
            {
                //idpagos <= identity
                sucursal = model.Sucursal,
                folio = folio.ToString("000000"), //??? campo de abonos
                distrib = model.Distribuidor,
                status = "AP",
                fecha = now,
                subtotal = model.Importe, //pagar - importe de la parrilla?
                descuento = 0, //buscar en adicional o plan de pagos - en la parrilla viene y es porcentaje, en la header ,para todo lo que no es vencido
                //descuentoadicional = desc?.descto, // total???
                interes = 0,
                interesmoratorio = 0,
                gastoscobranza = 0,
                //importe = ??, // subtotal menos descuentos
                vencido = 0,
                descuentovencido = 0,

                cobrador = 0,
                idconvenio = 0,
                idusuario = model.Cajero,
                fum = now,
                idusuariocancela = null,
                fumcancela = null,
                idusuarioautoriza = null,
                fumautoriza = null
            };
            pago.Detalle = new HashSet<DataAccess.SirCoCredito.PagosDetalle>();
            ctx.Pagos.Add(pago);

            var p = ctx.PlanPagos.Where(i => i.distrib == model.Distribuidor && i.pagado == "0")
                .SelectMany(i => i.Detalle).Where(i => i.pagado == "0").OrderBy(i => i.fechaaplicar);
            var importe = model.Importe;
            var count = 0;
            foreach (var pd in p)
            {
                decimal nimporte;
                if (importe > pd.importe)
                {
                    nimporte = pd.importe;
                    pd.pagado = "1";
                }
                else
                    nimporte = importe;
                pd.abono = nimporte;
                importe -= nimporte;
                var pdet = new DataAccess.SirCoCredito.PagosDetalle
                {
                    //idpagosdet <= identity
                    //idpagos <= parent
                    sucursal = model.Sucursal,
                    sucnot = pd.sucursal,
                    nota = pd.nota,
                    pago = ++count,
                    //public decimal? subtotal { get; set; }
                    //public decimal? descuento { get; set; }
                    //public decimal? descuentoadicional { get; set; }

                    interes = 0,
                    interesmoratorio = 0,
                    gastoscobranza = 0,
                    //public decimal? importe { get; set; }
                    vencido = 0,
                    descuentovencido = 0,
                    numpago = (int)pd.pago,
                    pagado = 1,
                    //public decimal? porcdescto { get; set; } - descuento original de la parrilla
                    //public decimal? porcdesctoadicional { get; set; } - el desc adicional en porcentaje
                    porcdesctovencido = 0,
                    idusuario = model.Cajero,
                    fum = now
                };
                pdet.importe = pdet.subtotal - pdet.descuento - pdet.descuentoadicional;
                pago.Detalle.Add(pdet);
                if (importe == 0)
                    break;
            }

            pago.importe = pago.Detalle.Sum(i => i.subtotal)
                - pago.Detalle.Sum(i => i.descuento)
                - pago.Detalle.Sum(i => i.descuentoadicional);
            ctx.SaveChanges();

            var qcheck = p.Select(i => i.PlanPago).Distinct();
            foreach (var plan in qcheck)
            {
                if (!plan.Detalle.Where(i => i.pagado == "0").Any())
                    plan.pagado = "1";
            }

            ctx.SaveChanges();
            ctxc.SaveChanges();

            return pago.idpagos;
        }
        public Common.Entities.RegisterValeResponse RegistrarVale(Common.Entities.RegisterValeRequest request,
            int uid, string suc)
        {
            var ctx = new DataAccess.SirCoPOSDataContext();
            var ctxc = new DataAccess.SirCoControlDataContext();
            var sitem = ctxc.Sucursales.Where(i => i.sucursal == suc).Single();
            var idcliente = this.AddClienteFromModel(request.Cliente, sitem.idsucursal);

            var vale = _sale.FindVale(request.Vale);

            var item = new DataAccess.SirCoPOS.ValeCliente
            {
                vale = vale.Vale,
                iddistrib = vale.Distribuidor.Id,
                idcliente = idcliente.Value,
                cantidad = request.Cantidad.Value,
                electronica = request.Electronica,
                date = DateTime.Now,
                idusuario = uid
            };
            ctx.ValesCliente.Add(item);
            ctx.SaveChanges();
            return new RegisterValeResponse { Success = true };
        }
    }
 
}
