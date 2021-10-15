using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SirCoPOS.Common.Constants;
using System.Text.RegularExpressions;
using SirCoPOS.Common.Entities;
using System.Data.Entity;
using SirCoPOS.Common.Helpers;
using SirCoPOS.BusinessLogic;
using System.ServiceModel.Configuration;
using System.Windows;

namespace SirCoPOS.Services
{
    public class DataService : Common.ServiceContracts.IDataService
    {
        private static object _sync = new object();
        private readonly BusinessLogic.Sale _sale;
        private readonly BusinessLogic.Return _return;
        private readonly BusinessLogic.Admin _admin;
        private readonly BusinessLogic.Data _helpers;
        public DataAccess.SirCoControlDataContext dem = new DataAccess.SirCoControlDataContext();
        public DataService()
        {
            _sale = new BusinessLogic.Sale();
            _admin = new BusinessLogic.Admin();
            _return = new BusinessLogic.Return();
            _helpers = new BusinessLogic.Data();
        }
        public void test()
        {
            throw new NotImplementedException();
        }
        #region sale
        //public int AddCliente(Cliente model)
        //{
        //    return _sale.AddCliente(model);
        //}
        public IEnumerable<Colonia> FindColonias(string cp)
        {
            return _helpers.FindColonias(cp);
        }

        #endregion
        public Common.Entities.Cupon FindCupon(string folio)
        {
            var ctxpv = new DataAccess.SirCoPVDataContext();

            var cupon = ctxpv.CuponesDetalle.Where(i => i.folio == folio && i.estatus == "ACTIVO").SingleOrDefault();
            if (cupon == null)
                return null;

            var res = new Common.Entities.Cupon
            {
                Folio = cupon.folio,
                Nombre = cupon.Cupon.nombre,
                Descripcion = cupon.Cupon.descripcion,
                Restricciones = cupon.Cupon.restricciones,
            };

            var now = DateTime.Now;
            if (now > cupon.Cupon.fecfin)
                res.Status = CuponStatus.Expirado;
            else if (now < cupon.Cupon.fecini)
                res.Status = CuponStatus.Inactivo;
            else
                res.Status = CuponStatus.Activo;

            if (res.Status != CuponStatus.Activo)
                return res;

            var q = cupon.Cupon.PromocionCupon.Select(i => new Common.Entities.PromocionCupon
            {
                CuponId = i.idcupon,
                PromocionId = i.idpromocion,
                Cupon = cupon.folio,
                Nombre = i.Promocion.nombre, //i.Cupon.nombre,
                Descripcion = i.Cupon.descripcion,
                Restricciones = i.Cupon.restricciones,
                Cliente = cupon.idcliente,
                HasCliente = i.Promocion.clienterequerido ?? false                
            });

            res.Promociones = q;

            return res;
        }
        public void actualizacliente()
        {
        }
        public IEnumerable<Common.Entities.PromocionCupon> FindCuponesByCliente(int clienteId)
        {
            var ctxpv = new DataAccess.SirCoPVDataContext();

            var q = ctxpv.CuponesDetalle.Where(i => i.idcliente == clienteId && i.estatus == "ACTIVO");
            var res = q.Select(cupon => new Common.Entities.PromocionCupon
            {
                CuponId = cupon.idcupon,
                Cupon = cupon.folio,
                Nombre = cupon.Cupon.nombre,
                Descripcion = cupon.Cupon.descripcion,
                Restricciones = cupon.Cupon.restricciones,
                Cliente = cupon.idcliente
            });

            return res;
        }
        public CheckPromocionesCuponesResponse CheckPromociones(CheckPromocionesCuponesRequest request)
        {
            lock (_sync)
            {
                return _sale.CheckPromociones(request);
            }
        }

        public ValeResponse FindDistribuidor(string id)
        {
            var ctx = new DataAccess.SirCoCreditoDataContext();
            var ct = new SirCoPOS.DataAccess.SirCoCredito.Distribuidor();

            //var vale = "abc";
            //var valera = ctx.Valeras.Where(i =>
            //    String.Compare(vale, i.valeini) >= 0 && String.Compare(vale, i.valefin) <= 0).SingleOrDefault();
            //if (valera == null)
            //    return null;

            //var valCancelado = ctx.ValesCancelados.Where(i =>
            //    String.Compare(vale, i.valeini) >= 0 && String.Compare(vale, i.valefin) <= 0).SingleOrDefault();
            //if (valCancelado != null)
            //    return null;

            var item = ctx.Distribuidores.Where(i => i.distrib == id
                //&& i.tipodistrib == Common.Constants.TipoDistribuidor.NORMAL
                && i.clasificacion == Common.Constants.TipoCredito.DISTRIBUIDOR
                ).SingleOrDefault();
            if (item == null)
                return null;

            //var usado = ctx.PlanPagos.Where(i => i.vale == vale).Sum(i => i.saldo);
            //var disponible = Math.Min(item.limitevale.Value, item.disponible.Value) - usado;

            var model = new Common.Entities.Distribuidor
            {
                Id = item.iddistrib,
                Cuenta = item.distrib,
                Nombre = item.nombre,
                ApMaterno = item.appaterno,
                ApPaterno = item.apmaterno,
                Status = item.idestatus.Value,
                Electronica = item.solocalzado == 0,
                ValeExterno = item.negext == 0,
                Plazos = item.idperiodicidad.Value,
                Promocion = item.promocion == 1,
                Number = item.distrib,
                Disponible = item.disponible,
                Date = item.fum
            };
            //var dist = String.Format("{0:000000}", id);
            var q = ctx.DistribuidorFirmas.Where(i => i.distrib == item.distrib);
            model.Firmas = q.Select(i => i.numfirma).ToArray();
            int? cliente = null;
            if (item.clientedi != null)
            {
                cliente = ctx.Clientes.Where(i => i.cliente == item.clientedi).Select(i => i.idcliente).SingleOrDefault();
            }
            if (cliente != null)
            {
                model.ClienteId = cliente;
            }
            var Vale = new ValeResponse
            {
                Distribuidor = model,
                Vale = id,
                Disponible = model.Disponible ?? 0,
                ClienteId = model.ClienteId
            };
            return Vale;
        }

        public ValeResponse FindDistribuidorId(int? id)
        {
            var ctx = new DataAccess.SirCoCreditoDataContext();
            var ct = new SirCoPOS.DataAccess.SirCoCredito.Distribuidor();

            var item = ctx.Distribuidores.Where(i => i.iddistrib == id
                && i.clasificacion == Common.Constants.TipoCredito.DISTRIBUIDOR
                ).SingleOrDefault();
            if (item == null)
                return null;

            var model = new Common.Entities.Distribuidor
            {
                Id = item.iddistrib,
                Cuenta = item.distrib,
                Nombre = item.nombre,
                ApMaterno = item.appaterno,
                ApPaterno = item.apmaterno,
                Status = item.idestatus.Value,
                Electronica = item.solocalzado == 0,
                ValeExterno = item.negext == 0,
                Plazos = item.idperiodicidad.Value,
                Promocion = item.promocion == 1,
                Number = item.distrib,
                Disponible = item.disponible,
                Date = item.fum
            };
            var q = ctx.DistribuidorFirmas.Where(i => i.distrib == item.distrib);
            model.Firmas = q.Select(i => i.numfirma).ToArray();
            return new ValeResponse
            {
                Distribuidor = model,
                Disponible = model.Disponible ?? 0,
                ClienteId = model.ClienteId,
            };
        }

        public PromocionesValeResponse FindPromocionesVale(string sucursal, string tipocredito)
        {
            var now = BusinessLogic.Helpers.Common.GetNow();
            var ctx = new DataAccess.SirCoCreditoDataContext();
            var promocion = ctx.PromocionesCredito.Where(i => i.sucursal == sucursal && i.status == "AC").SingleOrDefault();
            if (promocion == null)
                return null;

            int pagosmax = promocion.pagosmax.Value;
            int dias = -2;
            if (tipocredito == "TARJETAHABIENTE")
            {
                pagosmax = (int)Math.Ceiling((double)promocion.pagosmax.Value / 2);
                dias = -1;
            }


            var q = ctx.Calendarios.Where(i => i.tipo == "CORTE" && i.tipocredito == tipocredito
                            && i.fechaaplicarcorte <= promocion.fechaaplicar && i.fechaaplicarcorte > now)
                .OrderBy(i => i.fechaaplicarcorte).ToArray();

            var total = q.Count();
            int fechas = total;
            if (tipocredito == "TARJETAHABIENTE")
            {
                //Pago inmediato
                fechas = 1;
            }
            var qfechas = ctx.Calendarios.Where(i => i.tipo == "CORTE" && i.tipocredito == tipocredito
                && i.fechaaplicarcorte > now)
                .OrderBy(i => i.fechaaplicarcorte)
                .Take(total + pagosmax).ToArray();

            var blin = _helpers.GetParametro<decimal>(Common.Constants.Parametros.BLINDAJE);

            return new PromocionesValeResponse
            {
                Selected = promocion.pagosmin.Value,
                PagosMax = pagosmax,
                //Plazos = Enumerable.Range(1, promocion.pagosmax.Value),
                Promociones = q.Select(i => i.fechaaplicar.Value.AddDays(dias)).Take(fechas),
                Fechas = qfechas.Select(i => i.fechaaplicar.Value.AddDays(dias)),
                Blindaje = blin
            };
        }

        public PromocionesCreditoResponse FindPromocionesCredito(string sucursal)
        {
            var now = BusinessLogic.Helpers.Common.GetNow();
            var ctx = new DataAccess.SirCoCreditoDataContext();
            var promocion = ctx.PromocionesCredito.Where(i => i.sucursal == sucursal && i.status == "AC").SingleOrDefault();

            if (promocion == null)
                return null;

            var cal = ctx.Calendarios.Where(i => i.tipo == "CORTE" && i.tipocredito == "TARJETAHABIENTE"
                        && i.fechaaplicarcorte > now)
                .OrderBy(i => i.fechaaplicarcorte)
                .First();

            if (cal == null)
                return null;

            var first = ctx.Calendarios.Where(i => i.tipo == "CORTE" && i.tipocredito == "TARJETAHABIENTE"
                && i.fechaaplicarcorte <= promocion.fechaaplicar && i.fechaaplicarcorte > now)
                .OrderBy(i => i.fechaaplicarcorte).FirstOrDefault();

            return new PromocionesCreditoResponse
            {
                Selected = promocion.pagosmin.Value,
                PagosMax = promocion.pagosmax.Value,
                //Plazos = Enumerable.Range(1, promocion.pagosmax.Value),
                Promocion = first?.fechapagocliente
            };
        }

        public DevolucionResponse FindSale(string sucursal, string folio)
        {
            return _sale.FindSale(sucursal, folio);
        }

        public IEnumerable<Common.Constants.FormaPago> GetFormasPago()
        {
            var ctx = new DataAccess.SirCoPVDataContext();
            var items = ctx.FormasPago.Where(i => i.pos).Select(i => i.formapago).ToArray();
            var list = new List<Common.Constants.FormaPago>();
            foreach (var item in items)
            {
                Common.Constants.FormaPago fp;
                if (Enum.TryParse(item, out fp))
                    list.Add(fp);
            }
            return list;
        }

        public ValeResponse FindTarjetahabiente(string id)
        {
            var ctx = new DataAccess.SirCoCreditoDataContext();
            var ctxc = new DataAccess.SirCoControlDataContext();

            var item = ctx.Distribuidores.Where(i => i.distrib == id
                //&& i.tipodistrib == Common.Constants.TipoDistribuidor.NORMAL
                && i.clasificacion == Common.Constants.TipoCredito.TARJETAHABIENTE
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
                ValeExterno = item.negext == 0,
                Plazos = item.idperiodicidad.Value,
                Promocion = item.promocion == 1
                , Number = item.distrib,
                Cuenta = item.distrib,
                Distrib = item.distrib
            };

            var model = dis;

            var num = int.Parse(item.distrib).ToString();
            var qpp = ctx.PlanPagos.Where(i => i.vale.Trim() == num);
            var qp = qpp.Where(i => i.pagado == "0");
            var usado = qp.Any() ? qp.Sum(i => i.saldo) : 0;
            model.Disponible = Math.Min(item.limitevale.Value, item.disponible.Value);// - usado;
            model.Disponible = model.Disponible < 0 ? 0 : model.Disponible;

            var q = ctx.DistribuidorFirmas.Where(i => i.distrib == item.distrib);
            model.Firmas = q.Select(i => i.numfirma).ToArray();


            //var last = qpp.OrderByDescending(i => i.fum).FirstOrDefault();
            //if (last != null)
            //{
            //    var suc = ctxc.Sucursales.Where(i => i.sucursal == last.succliente).Single();

            //    var cli = ctx.Clientes.Where(i => i.cliente == last.cliente && i.idsucursal == suc.idsucursal).Single();
            //    model.ClienteId = cli.idcliente;
            //}

            if (item.clientedi != null)
            {
                var suc = ctxc.Sucursales.Where(i => i.sucursal == item.succtedi).Single();
                var cli = ctx.Clientes.Where(i => i.idsucursal == suc.idsucursal && i.cliente == item.clientedi).Single();
                model.ClienteId = cli.idcliente;
            }

            return new ValeResponse {
                Distribuidor = model,

                Vale = id,
                Disponible = model.Disponible ?? 0,
                //public bool Cancelado { get; set; }
                //public string CanceladoMotivo { get; set; }
                ClienteId = model.ClienteId,
                //public bool WithLimite { get; set; }
                //public decimal? Limite { get; set; }
            };
        }
        public ValeResponse FindVale(string vale)
        {
            var res = _sale.FindVale(vale);
            return res;
        }
        public DistribuidorObserva FindDistObserva(string dist)
        {
            var res = _sale.FindDistObserva(dist);
            return res;
        }
        public ValeResponse FindValeDigital(string vale)
        {
            var res = _sale.FindValeDigital(vale);
            return res;
        }
        public ValeResponse FindValeDigitalByClient(int id)
        {
            //var res = _sale.FindValeDigitalByClient(id);
            return null;
        }
        public CValeResponse FindContraVale(string sucursal, string vale)
        {
            var res = _sale.FindContraVale(sucursal, vale);
            return res;
        }

        public ScanResponse ScanProducto(string serie, string sucursal)
        {
            return _sale.ScanProducto(serie, sucursal);
            var res = new Response<ScanResponse>();
            try
            {
                res.Item = _sale.ScanProducto(serie, sucursal);
                res.Success = true;
            }
            catch (BusinessLogic.CustomException ex)
            {
                res.Success = false;
                res.Error = ex.Message;
            }
            //return res;
        }
        public int PrintNumCopias()
        {
            int Valor = 1;
            var ctx = new DataAccess.SirCoControlDataContext();
            var copias = ctx.Parametros.Where(i => i.clave == "NUMCOPIAS" && i.sucursal == "99").SingleOrDefault();
            if (copias != null)
            {
                Valor = Int32.Parse(copias.valor);
            }
            return Valor;
        }
        public int getminPago(string tipo)
        {
            int Valor = 1;
            var ctx = new DataAccess.SirCoControlDataContext();
            var min = ctx.Parametros.Where(i => i.clave == tipo && i.sucursal == "99").SingleOrDefault();
            if (min != null)
            {
                Valor = Int32.Parse(min.valor);
            }
            return Valor;
        }
        public Producto FindProducto(string marca, string modelo, string sucursal)
        {
            var ctx = new DataAccess.SirCoDataContext();
            var ctxi = new DataAccess.SirCoImgDataContext();
            //var status = new string[] { Common.Constants.Status.ZC.ToString() };
            var item = ctx.Corridas.Include(i => i.Articulo)
                .Where(i => i.marca == marca && i.estilon == modelo)
                .OrderByDescending(i => i.ult_vta)
                .FirstOrDefault();
            
            //var q = ctx.Series.Where(i => i.serie == serie
            //    && !status.Contains(i.status));
            //var item = q.Where(i => i.sucursal == sucursal).SingleOrDefault();
            //if (item == null)
            //{
            //    var ctxc = new DataAccess.SirCoControlDataContext();
            //    var sucs = ctxc.Sucursales.Where(i => i.ordenweb.HasValue)
            //        .Select(i => i.sucursal).ToArray();
            //    item = q.Where(i => sucs.Contains(i.sucursal)).SingleOrDefault();
            //}
            if (item != null)
            {
                if (item.ArticuloId > 0)
                {
                    var isElectronica = item.Articulo.iddivisiones == (int)Divisiones.Electronica;
                    var isParUnico = _sale.IsParUnico(item.Articulo.idagrupacion);
                    var corrida = item;
                    //if (corrida == null)
                    //    return null;
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

                    return new Producto
                    {
                        Id = item.ArticuloId,
                        Corrida = corrida.corrida,
                        Marca = item.marca,
                        Modelo = item.estilon,
                        //Precio = corrida.precio,
                        //Serie = item.serie,
                        //Talla = item.medida,
                        Total = corrida.precio,
                        HasImage = qimg.Any(),
                        Electronica = isElectronica,
                        ParUnico = isParUnico,
                        MaxPlazos = maxPlazos,
                        //Sucursal = item.sucursal
                    };
                }
                else
                {
                    return new Producto
                    {
                        Id = item.ArticuloId,
                        //Corrida = corrida.corrida,
                        Marca = item.marca,
                        Modelo = item.estilon
                        //Precio = corrida.precio,

                        //,Serie = item.serie
                        //,Talla = item.medida,

                        //Total = corrida.precio,
                        //HasImage = qimg.Any(),
                        //Electronica = isElectronica,
                        //ParUnico = isParUnico,
                        //MaxPlazos = maxPlazos
                    };
                }
            }
            return null;
        }

        public ScanDevolucionResponse ScanProductoDevolucion(string serie, bool cancelacion)
        {
            return _sale.ScanProductoDevolucion(serie, cancelacion);
        }
        public ScanDevolucionResponse ScanProductoFromDevolucion(string serie)
        {
            return _sale.ScanProductoFromDevolucion(serie);
        }

        //public string FindFolio(string sucursal, string serie)
        //{
        //    var ctx = new DataAccess.SirCoPVDataContext();
        //    var item = ctx.VentasDetalle.Where(i => i.sucursal == sucursal && i.serie == serie
        //        && i.Header.estatus == "AP").SingleOrDefault();
        //    return item?.venta;
        //}

        public Common.Entities.Devolucion FindDevolucion(string sucursal, string folio)
        {
            var ctx = new DataAccess.SirCoPVDataContext();
            var item = ctx.Devoluciones.Where(i => i.sucursal == sucursal && i.devolvta == folio).SingleOrDefault();
            if (item != null)
            {
                return new Devolucion
                {
                    Sucursal = item.sucursal,
                    Folio = item.devolvta,
                    Disponible = item.disponible ?? 0,
                    ClientId = item.idcliente,
                    Estatus = item.estatus
                };
            }
            return null;
        }
        public Common.Entities.Cliente FindClienteByCode(Guid code)
        {
            var now = BusinessLogic.Helpers.Common.GetNow();
            var ctx = new DataAccess.SirCoPOSDataContext();
            var item = ctx.ClienteAccesos.Where(i => i.Codigo == code && i.FechaExpiracion > now).SingleOrDefault();
            if (item != null)
            {
                return this.FindCliente(item.ClienteId, null);
            }
            return null;
        }
        public int Clientexd(string name, string appaterno, string apmaterno, string codigopostal, string calle, int numero, string celular1, string celu, string email, string colonia, int usumodif, string sexo, string identificacion)
        {
            var ctx = new DataAccess.SirCoCreditoDataContext();
            var ctxc = new DataAccess.SirCoControlDataContext();
            var nc = name + " " + appaterno + " " + apmaterno;
            var item = ctx.Clientes.Where(i => i.nombrecompleto == nc).FirstOrDefault();
            var datcol = ctxc.Colonias.Where(i => i.colonia == colonia && i.codigopostal == codigopostal).SingleOrDefault();
            
            if (item == null)
            {
                return 0;
            }
            if(name != "")
            {
                item.nombre = name;
            }
            if(appaterno != "")
            {
                item.appaterno = appaterno;
            }
            if(apmaterno != "")
            {
                item.apmaterno = apmaterno;
            }
            if(codigopostal != "")
            {
                item.codigopostal = codigopostal;
            }
            if(calle != "")
            {
                item.calle = calle;
            }
            if(numero != 0)
            {
                item.numero = Convert.ToInt16(numero);
            }
            if (celular1 != "" && celular1 != null) 
            {
                item.celular1 = celular1;
            }
            if(email != "" && email != null)
            {
                item.email = email;
            }
            if(datcol != null)
            {
                item.idciudad = datcol.idciudad;
                item.idestado = datcol.idestado;
                item.idcolonia = datcol.idcolonia;
            }
            if(identificacion != "")
            {
                item.identificacion = identificacion;
            }
            item.idusuariomodif = usumodif;
            item.celular = celu;
            item.fummodif = DateTime.Now;
            item.sexo = sexo;

            ctx.SaveChanges();
            return Convert.ToInt32(item.idcolonia);
        }
        public Common.Entities.Cliente FinClienteName(string name = null)
        {
            var ctx = new DataAccess.SirCoCreditoDataContext();
            DataAccess.SirCoCredito.Cliente item = null;
            if(name != null)
            {
                item = ctx.Clientes.Where(i => i.nombrecompleto == name).FirstOrDefault();
                if (item == null)
                    return null;
            }

            return new Cliente
            {
                Id = item.idcliente,
                SucursalId = item.idsucursal,
                Nombre = item.nombre,
                ApPaterno = item.appaterno,
                ApMaterno = item.apmaterno,
                Celular = item.celular1,
                CodigoPostal = item.codigopostal,
                Colonia = item.idcolonia,
                Ciudad = item.idciudad,
                Estado = item.idestado,
                Calle = item.calle,
                Numero = item.numero,
                Celular1 = item.celular,
                //public string Referencia { get; set; }
                Email = item.email,
                Sexo = item.sexo,
                Identificacion = item.identificacion
            };

        }
        public int FindColidByName(string name, string cp)
        {
            var ctx = new DataAccess.SirCoControlDataContext();
            var colonias = ctx.Colonias.Where(i => i.colonia == name && i.codigopostal == cp).SingleOrDefault();
            return colonias.idcolonia;
        }
        public Common.Entities.Cliente FindCliente(int? id, string telefono = null, string nombre = null)
        {
            var ctx = new DataAccess.SirCoCreditoDataContext();
            DataAccess.SirCoCredito.Cliente item = null;
            if (id.HasValue)
                item = ctx.Clientes.Where(i => i.idcliente == id).FirstOrDefault();
            else if (!string.IsNullOrWhiteSpace(telefono))
                item = ctx.Clientes.Where(i => i.celular1  == telefono).FirstOrDefault();
            else if (!string.IsNullOrWhiteSpace(nombre))
                item = ctx.Clientes.Where(i => i.nombrecompleto == nombre).FirstOrDefault();
            if (item == null)
                return null;

            return new Cliente
            {
                Id = item.idcliente,
                SucursalId = item.idsucursal,
                Nombre = item.nombre,
                ApPaterno = item.appaterno,
                ApMaterno = item.apmaterno,
                Celular = item.celular1,
                CodigoPostal = item.codigopostal,
                Colonia = item.idcolonia,
                Ciudad = item.idciudad,
                Estado = item.idestado,
                Calle = item.calle,
                Numero = item.numero,
                Celular1 = item.celular,
                //public string Referencia { get; set; }
                Email = item.email,
                Sexo = item.sexo,
                Identificacion = item.identificacion
            };
        }

        public string FindColonia(int id)
        {
            var ctxco = new DataAccess.SirCoControlDataContext();
            var nombre = "";
            System.Collections.Generic.IEnumerable<SirCoPOS.DataAccess.SirCoControl.Colonia> colon = null;

            colon = ctxco.Colonias.Where(i => i.idcolonia == id);

            foreach (var co in colon)
            {
                nombre = co.colonia;
                return nombre;
            }
            return "";
        }



        public string FindCiudad(int id)
        {
            var ctxc = new DataAccess.SirCoControlDataContext();
            var nombre = "";
            System.Collections.Generic.IEnumerable<SirCoPOS.DataAccess.SirCoControl.Ciudad> ciu = null;
            ciu = ctxc.Ciudades.Where(i => i.idciudad == id);

            foreach (var ci in ciu)
            {
                nombre = ci.ciudad;
                return nombre;
            }

            return "";
        }

        public string FindEstado(int id)
        {
            var ctxe = new DataAccess.SirCoControlDataContext();
            var nombre = "";
            System.Collections.Generic.IEnumerable<SirCoPOS.DataAccess.SirCoControl.Estado> est = null;

            est = ctxe.Estados.Where(i => i.idestado == id);

            foreach (var es in est)
            {
                nombre = es.estado;
                return nombre;
            }

            return "";
        }
        public string findcol(int? col)
        {
            var ctxc = new DataAccess.SirCoControlDataContext();
            var colonias = ctxc.Colonias.Where(i => i.idcolonia == col).SingleOrDefault();
            return colonias.colonia;
        }

        public List<Cliente> FindCliente2(string telefono = null, string nombre = null, string appa = null, string apma = null)
        {
            var ctx = new DataAccess.SirCoCreditoDataContext();
            var ctxc = new DataAccess.SirCoControlDataContext();
            
            List<Colonia> coloni = new List<Colonia>();

            DataAccess.SirCoCredito.Cliente item = null;
            DataAccess.SirCoCredito.Cliente[] itemm = null;

            System.Collections.Generic.IEnumerable<SirCoPOS.DataAccess.SirCoCredito.Cliente> atala = null;
            
            //if (id.HasValue)
            //    item = ctx.Clientes.Where(i => i.idcliente == id).SingleOrDefault();
            if (!string.IsNullOrWhiteSpace(telefono))
                item = ctx.Clientes.Where(i => i.celular1 == telefono).SingleOrDefault();
            // Busquedas por nombre 
            else if (!string.IsNullOrWhiteSpace(nombre) && appa == null && apma == null)
                itemm = ctx.Clientes.Where(i => i.nombre == nombre).ToArray();
            // Busqueda por nombre y apellido paterno
            else if (!string.IsNullOrWhiteSpace(nombre) && appa != null && apma == null)
                itemm = ctx.Clientes.Where(i => i.nombre == nombre && i.appaterno == appa).ToArray();
            // Busqueda por nombre, apellido paterno y apellido materno
            else if (!string.IsNullOrWhiteSpace(nombre) && appa != null && apma != null)
                itemm = ctx.Clientes.Where(i => i.nombre == nombre && i.appaterno == appa && i.apmaterno == apma).ToArray();
            //( Busqueda por nombre y apellido materno
            else if (!string.IsNullOrWhiteSpace(nombre) && appa == null && apma != null)
                itemm = ctx.Clientes.Where(i => i.nombre == nombre && i.apmaterno == apma).ToArray();
            // Busqueda por apellido paterno y apellido materno
            else if (nombre == null && appa != null && apma != null)
                itemm = ctx.Clientes.Where(i => i.appaterno == appa && i.apmaterno == apma).ToArray();
            // Busqueda por apellido paterno
            else if (nombre == null && appa != null && apma == null)
                itemm = ctx.Clientes.Where(i => i.appaterno == appa).ToArray();
            // Busqueda por apellido materno
            else if (nombre == null && appa == null && apma != null)
                itemm = ctx.Clientes.Where(i => i.apmaterno == apma).ToArray();


            if (itemm == null)
                return null;
         
            //variable que limita la cantidad de registros que trae la consulta (con el fin de que no marque error de sobrecarga)
            atala = itemm.OrderByDescending(i => i.idcliente).Take(127);

            

            List<Cliente> liscliente = new List<Cliente>();

            foreach(var nic in atala)
            {
                
                var cliente = new Cliente
                {
                    Nombre = nic.nombre,
                    ApPaterno = nic.appaterno,
                    ApMaterno = nic.apmaterno,
                    Celular = nic.celular1,
                    CodigoPostal = nic.codigopostal,
                    Colonia = nic.idcolonia,
                    Ciudad = nic.idciudad,
                    Estado = nic.idestado,
                    Calle = nic.calle,
                    Numero = nic.numero,
                    Email = nic.email,
                    Sexo = nic.sexo
                };
                liscliente.Add(cliente);
            };
            return liscliente;
        }

        public IEnumerable<Common.Entities.Promocion> GetPromociones(CheckPromocionesRequest request)
        {
            return _sale.GetPromociones(request);
        }

        //public Common.Entities.CValeResponse FindContraVale(string sucursal, string folio)
        //{
        //    var ctx = new DataAccess.SirCoCreditoDataContext();
        //    var item = ctx.ContraVales.Where(i => i.sucursal == sucursal && i.cvale == folio).SingleOrDefault();
        //    if (item == null)
        //        return null;

        //    var cli = ctx.Clientes.Where(i => i.cliente == item.cliente).Single();
        //    var dist = ctx.Distribuidores.Where(i => i.distrib == item.distrib).Single();

        //    return new CValeResponse
        //    {
        //        Cliente = cli.idcliente, 
        //        Folio = item.cvale, 
        //        Importe = item.saldo, 
        //        Sucursal = item.sucursal, 
        //        Distribuidor = new Distribuidor 
        //        {
        //            Id = dist.iddistrib, 
        //            Nombre = dist.nombrecompleto,
        //            //public string ApPaterno { get; set; }
        //            //public string ApMaterno { get; set; }
        //            Status = dist.tipodistrib,
        //            SoloCalzado = dist.solocalzado == 1,
        //            //public IEnumerable<short> Firmas { get; set; }
        //            //public bool ContraVale { get; set; }
        //            Promocion = dist.promocion == 1
        //        }
        //    };
        //}

        public IDictionary<int, ICollection<byte[]>> Approve(string sucursal)
        {
            return _helpers.Approve(sucursal);
        }

        public decimal? FindMonedero(int cliente)
        {
            var now = BusinessLogic.Helpers.Common.GetNow();
            var ctxc = new DataAccess.SirCoCreditoDataContext();
            var ctxa = new DataAccess.SirCoDataContext();

            var cli = ctxc.Clientes.Where(i => i.idcliente == cliente).SingleOrDefault();
            if (cli == null)
                return null;

            var md = ctxa.Dineros.Where(i => i.idsucursal == cli.idsucursal && i.cliente == cli.cliente).SingleOrDefault();
            if (md == null)
                return null;

            if (md.vigencia.HasValue && md.vigencia < now)
                return null;

            return md.saldo;
        }
        public IEnumerable<Common.Entities.NegocioExterno> GetNegocios()
        {
            var ctx = new DataAccess.SirCoControlDataContext();
            var q = ctx.NegociosExternos
                .OrderBy(i => i.descripcion)
                .Select(i => new Common.Entities.NegocioExterno
                {
                    Id = i.idnegexterno,
                    Negocio = i.negocio,
                    Descripcion = i.descripcion
                });
            return q;
        }
        public Common.Entities.ValeResponse FindDistribuidorExterno(int idnegocio, string nocuenta, string vale)
        {
            var ctx = new DataAccess.SirCoCreditoDataContext();
            var ditem = ctx.DistribuidorComerciales.Where(i => i.idnegexterno == idnegocio && i.nocuenta == nocuenta).SingleOrDefault();
            if (ditem == null)
                return null;
            var item = ctx.Distribuidores.Where(i => i.iddistrib == ditem.iddistrib
                //&& i.tipodistrib == Common.Constants.TipoDistribuidor.NORMAL
                && i.clasificacion == Common.Constants.TipoCredito.DISTRIBUIDOR
            ).Single();

            var dis = new Common.Entities.Distribuidor
            {
                Id = item.iddistrib,
                Cuenta = item.distrib,
                //Nombre = item.nombre,
                //ApPaterno = item.appaterno,
                //ApMaterno = item.apmaterno,
                Disponible = item.disponible.Value,
                Nombre = item.nombrecompleto,
                Status = item.idestatus.Value,
                Electronica = item.solocalzado == 0,
                ValeExterno = item.negext == 1,
                ContraVale = item.contravale == 1,
                Promocion = item.promocion == 1
            };

            var model = new ValeResponse
            {
                Vale = vale,
                Disponible = item.disponible.Value,
                Cancelado = false,
                Distribuidor = dis
            };

            var q = ctx.DistribuidorFirmas.Where(i => i.distrib == item.distrib);
            model.Distribuidor.Firmas = q.Select(i => i.numfirma).ToArray();

            return model;
        }
        public IEnumerable<Common.Entities.DescuentoAdicional> GetDescuentoAdicionals()
        {
            var ctx = new DataAccess.SirCoPVDataContext();
            var items = ctx.DescuentoEspeciales.OrderBy(i => i.razon)
                .Select(i => new Common.Entities.DescuentoAdicional
                {
                    Id = i.iddescuentoespecial,
                    Descripcion = i.razon,
                    Descuento = i.descuento / 100m, 
                    Devolucion = i.devolucion
                });
            return items;
        }
        public IEnumerable<Common.Entities.RazonNotaDevolucion> GetRazonesDevolucion()
        {
            var ctx = new DataAccess.SirCoPVDataContext();
            var items = ctx.DevolucionRazones.OrderBy(i => i.descripcion)
                .Select(i => new Common.Entities.RazonNotaDevolucion
                {
                    Id = i.iddevolucionrazon,
                    Descripcion = i.descripcion,
                    Comentarios = i.comentarios
                });
            return items;
        }
        public IEnumerable<Common.Entities.RazonNotaDevolucion> GetRazonesNotas()
        {
            var ctx = new DataAccess.SirCoPVDataContext();
            var items = ctx.NotaRazones.OrderBy(i => i.descripcion)
                .Select(i => new Common.Entities.RazonNotaDevolucion
                {
                    Id = i.idnotarazon,
                    Descripcion = i.descripcion,
                    Comentarios = i.comentarios
                });
            return items;
        }
        public Common.Entities.VentaView FindVentaView(string sucursal, string folio, int idcajero)
        {
            var ctx = new DataAccess.SirCoPVDataContext();
            DataAccess.SirCoPV.Venta item;
            if (folio != null)
                item = ctx.Ventas.Where(i => i.sucursal == sucursal && i.venta == folio).SingleOrDefault();
            else
            {
                item = ctx.Ventas.Where(i => i.sucursal == sucursal && i.idcajero == idcajero)
                    .OrderByDescending(i => i.fum).FirstOrDefault();
            }
            if (item == null)
                return null;
            var res = new Common.Entities.VentaView
            {
                Sucursal = item.sucursal,
                Folio = item.venta,
                Productos = item.Detalles.Select(i =>
                    new ProductoView
                    {
                        Serie = i.serie,
                        Precio = i.precio
                    })
            };
            return res;
        }
        public Common.Entities.DevolucionView FindDevolucionView(string sucursal, string folio, int idcajero)
        {
            var ctx = new DataAccess.SirCoPVDataContext();
            var ctxs = new DataAccess.SirCoDataContext();
            SirCoPOS.DataAccess.SirCoPV.Devolucion item;
            if(folio != null)
                item = ctx.Devoluciones.Where(i => i.sucursal == sucursal && i.devolvta == folio).SingleOrDefault();
            else
                item = ctx.Devoluciones.Where(i => i.sucursal == sucursal && i.idcajero == idcajero)
                    .OrderByDescending(i => i.fum).FirstOrDefault();
            if (item == null)
                return null;
            var res = new Common.Entities.DevolucionView
            {
                Sucursal = item.sucursal,
                Folio = item.devolvta,
                Productos = item.Detalles.Select(i =>
                    new ProductoView
                    {
                        Serie = i.serie,
                        Precio = i.precio,                         
                        Marca = i.marca, 
                        Modelo = i.estilon, 
                        Medida = i.medida                        
                    }).ToArray()
            };
            foreach (var det in res.Productos)
            {
                var ser = ctxs.Series.Where(i => i.serie == det.Serie).Single();
                det.ArticuloId = ser.ArticuloId;
            }
            return res;
        }
        public IEnumerable<Common.Entities.NoteHeader> GetNotes()
        {
            var ctx = new SirCoPOS.DataAccess.SirCoPOSDataContext();
            var q = ctx.Notas.Where(i => i.Venta == null).OrderBy(i => i.Date);
            var items = q.Select(i => new Common.Entities.NoteHeader
            {
                Id = i.Id,
                Date = i.Date,
                Sucursal = i.Sucursal,
                CajeroId = i.CajeroId,
                Total = i.Items.Sum(k => k.Amount)
            });
            return items;
        }
        public IEnumerable<Common.Entities.NoteDetalle> GetNoteDetails(int id)
        {
            var pctx = new SirCoPOS.DataAccess.SirCoPOSDataContext();
            var ctx = new SirCoPOS.DataAccess.SirCoDataContext();
            var q = pctx.NotasDetalle.Where(i => i.NotaId == id);
            var res = q.Select(i => new Common.Entities.NoteDetalle
            {
                Serie = i.Serie,
                Amount = i.Amount,
                Comments = i.Coments
            }).ToArray();
            foreach (var item in res)
            {
                var sitem = ctx.Series.Where(i => i.serie == item.Serie).Single();
                var corrida = ctx.GetCorrida(sitem);
                item.AmountOriginal = corrida.precio.Value;
            }
            return res;
        }
        public bool CheckCelular(string celular)
        {
            var ctxc = new DataAccess.SirCoCreditoDataContext();
            var q = ctxc.Clientes.Where(i => i.celular1 == celular);
            return q.Any();
        }
        public bool CheckNombreC(string nombrec)
        {
            var ctxc = new DataAccess.SirCoCreditoDataContext();
            var q = ctxc.Clientes.Where(i => i.nombrecompleto == nombrec);
            return q.Any();
        }
        public Common.Entities.MedidasCorridas GetPrecios(int id)
        {
            var ctx = new DataAccess.SirCoDataContext();
            var corridas = ctx.Corridas.Where(i => i.ArticuloId == id)
                .Select(i => new Common.Entities.TallaPrecio
                {
                    Corrida = i.corrida,
                    MedidaInicio = i.medini,
                    MedidaFin = i.medfin,
                    Precio = i.precio
                }).Distinct();


            var ctxc = new DataAccess.SirCoControlDataContext();
            var sucs = ctxc.Sucursales.Where(i => i.ordenweb.HasValue)
                .Select(i => i.sucursal).ToArray();

            var status = new string[] { Common.Constants.Status.ZC.ToString() };
            var medidas = ctx.Series.Where(i => i.ArticuloId == id 
                && !status.Contains(i.status))
                .OrderBy(i => i.medida)
                .Select(i => i.medida)
                .Distinct();

            var res = new Common.Entities.MedidasCorridas
            {
                Corridas = corridas,
                Medidas = medidas
            };
            return res;
        }

        public IEnumerable<Common.Entities.SucursalExistencia> GetExistencias(int id, string medida)
        {
            var ctx = new DataAccess.SirCoDataContext();

            var status = new string[] { 
                Common.Constants.Status.AC.ToString(),
                Common.Constants.Status.IF.ToString()
            };

            var ctxc = new DataAccess.SirCoControlDataContext();
            var sucs = ctxc.Sucursales.Where(i => i.ordenweb.HasValue)
                .Select(i => i.sucursal).ToArray();
            
            var res = ctx.Series.Where(i => i.ArticuloId == id && i.medida == medida
                && status.Contains(i.status) && sucs.Contains(i.sucursal))
                .GroupBy(i => i.sucursal)
                .Select(i => new Common.Entities.SucursalExistencia
                {
                    Sucursal = i.Key, 
                    Count = i.Count()
                });
            return res;
        }
        public IDictionary<DateTime, decimal> GenerarPlanPagosFechas(int iddist, Pago item)
        {
            var _process = new BusinessLogic.Process();
            var ctxcr = new DataAccess.SirCoCreditoDataContext();
            var dist = ctxcr.Distribuidores.Where(i => i.iddistrib == iddist).Single();
            var fechas = new HashSet<DataAccess.SirCoCredito.Calendario>();
            var helper = new BusinessLogic.Data();
            var blindaje = helper.GetParametro<decimal?>(Parametros.BLINDAJE);
            var detalle = _process.GenerarPlanPagosFechas(DateTime.Now, item, dist, fechas, blindaje);
            return detalle;
        }
        public Empleado FindAuditorApertura(int id, int idcajero)
        {
            var _data = new BusinessLogic.Data();
            return _data.FindAuditorApertura(id, idcajero);
        }
        public Empleado AuditorPassword(int id, string pwd)
        {
            var _data = new BusinessLogic.Data();
            return _data.AuditorPassword(id, pwd);
        }
        public Empleado FindAuditorEntrega(int id, int idcajero)
        {
            var _data = new BusinessLogic.Data();
            return _data.FindAuditorEntrega(id, idcajero);
        }
        public Empleado FindAuditorTransferir(int id, int idcajero)
        {
            var _data = new BusinessLogic.Data();
            return _data.FindAuditorTransferir(id, idcajero);
        }
        public int ContabilizaReimpresion(string Operacion, string Sucursal, string Folio)
        {
            var ctx = new DataAccess.SirCoPOSDataContext();
            DataAccess.SirCoPOS.Reimpresion item;

            item = ctx.Reimpresiones.Where(i => i.Operacion == Operacion && i.Sucursal == Sucursal && i.Folio == Folio).SingleOrDefault();

            if (item != null)
            {
                item.Numero++;
            }
            else
            {
                var reimpresion = new SirCoPOS.DataAccess.SirCoPOS.Reimpresion
                {
                    Operacion = Operacion,
                    Sucursal = Sucursal,
                    Folio = Folio,
                    Numero = 1
                };
                ctx.Reimpresiones.Add(reimpresion);
            }
            return ctx.SaveChanges();
        }
        public IEnumerable<Common.Entities.PorcentajeFormaPago> GetPorcentajeFPago(string sucursal, string devolucion)
        {
            var _data = new BusinessLogic.Data();
            return _data.GetPorcentajeFPago(sucursal, devolucion);
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
    }
}
