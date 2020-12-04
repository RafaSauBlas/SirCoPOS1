using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.QualityTools.Testing.Fakes;
using Moq;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using SirCoPOS.Common.Entities;
using SirCoPOS.Common.Constants;

namespace SirCoPOS.Tests
{
    [TestClass]
    public class UnitTest1
    {
        public TestContext TestContext { get; set; }

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            
        }

        public UnitTest1()
        {
            _context = new BusinessLogic.Process();
        }

        private BusinessLogic.Process _context;

        [TestInitialize]
        public void TestInit()
        {
            Monitor.Enter(ConfigTest.Sync);
            ConfigTest.AssemblySetup(this.TestContext);            
        }
        [TestCleanup]
        public void TestClean()
        {
            Monitor.Exit(ConfigTest.Sync);
        }

        [TestMethod]
        public void TestParrilla()
        {            
            using (ShimsContext.Create())
            {                
                System.Fakes.ShimDateTime.NowGet = () => { return DateTime.Parse("2019-09-01"); };

                var sucursal = "01";
                var ctx = new DataAccess.SirCoCreditoDataContext();
                var now = DateTime.Now;

                var promocion = ctx.PromocionesCredito.Where(i => i.sucursal == sucursal && i.status == "AC").SingleOrDefault();
                Assert.IsNotNull(promocion);

                var cal = ctx.Calendarios.Where(i => i.tipo == "CORTE" && i.tipocredito == "DISTRIBUIDOR"
                    && i.fechaaplicarcorte == promocion.fechaaplicar).SingleOrDefault();

                Assert.IsNotNull(cal);

                var q = ctx.Calendarios.Where(i => i.tipo == "CORTE" && i.tipocredito == "DISTRIBUIDOR"
                    && i.fechaaplicarcorte <= promocion.fechaaplicar && i.fechaaplicarcorte > now)
                    .OrderBy(i => i.fechaaplicarcorte).ToArray();

                Assert.AreEqual(5, q.Length);

                var last = q.Last();
                var plazos = 10;

                var items = ctx.Calendarios.Where(i => i.tipo == "CORTE" && i.tipocredito == "DISTRIBUIDOR"
                    && i.fechaaplicarcorte >= last.fechaaplicarcorte)
                    .OrderBy(i => i.fechaaplicarcorte).Take(plazos).ToArray();

                Assert.AreEqual(plazos, items.Length);

                var total = 100m;
                var part = total / plazos;
                var p1 = Math.Ceiling(part);
                var res = p1 * (plazos - 1);
                var faltante = total - res;
            }
        }

        [TestMethod]
        public void TestPago()
        {            
            var request = new Common.Entities.PagoRequest
            {
                Distribuidor = "004461",
                Sucursal = "01",
                Importe = 1000m,                
                Cajero = 0
            };
            var id = _context.Abono(request);            
        }

        [TestMethod]
        public void PromocionAdidasTest()
        {
            var request = new Common.Entities.CheckPromocionesCuponesRequest {
                Sucursal = "01",
                PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                    new Common.Entities.PromocionCuponItem { Cupon = "0000000101", PromocionId = 10 }
                },
                Productos = new Common.Entities.SerieFormasPago[] {
                    new Common.Entities.SerieFormasPago { Serie = "0000003632625", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } },
                    new Common.Entities.SerieFormasPago { Serie = "0000003635956", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } },
                    new Common.Entities.SerieFormasPago { Serie = "0000003668367", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } },                    
                }
            };

            var sale = new BusinessLogic.Sale();

            CheckPromocionesCuponesResponse res;
            using (ShimsContext.Create())
            {
                var now = DateTime.Parse("2019-06-25");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.AddHours(5);
                System.Fakes.ShimDateTime.NowGet = () => now;
                System.Fakes.ShimDateTime.TodayGet = () => now;
                res = sale.CheckPromociones(request);
            }            
            Assert.IsNotNull(res.Promociones);
            Assert.AreEqual(3, res.Promociones.Count());
            Assert.IsNull(res.Promociones.Where(i => i.Serie == "0000003668367").Single().Descuento);
            Assert.AreEqual(179.85m, res.Promociones.Where(i => i.Serie == "0000003632625").Single().Descuento);//1199.00
            Assert.AreEqual(149.85m, res.Promociones.Where(i => i.Serie == "0000003635956").Single().Descuento);//999.00            
        }
        [TestMethod]
        public void Promocion3x249_importe()
        {
            /*
239.00	0000003282389
329.00	0000003391871
329.00	0000003391872
359.00	0000003391948
399.00	0000003251820
289.00	0000003446549

239.00 + 329.00 + 329.00 = 897
239.00 + 329.00 + 359.00 = 927
239.00 + 359.00 + 399.00 = 997
329.00 + 329.00 + 359.00 = 1017 <-- esperado
329.00 + 329.00 + 399.00 = 1057
329.00 + 359.00 + 399.00 = 1087
             */
            var ctx = new DataAccess.SirCoPVDataContext();
            var prom = ctx.Promociones.Where(i => i.idpromocion == 4).Single();
            prom.minimpcompra = 1000m;
            ctx.SaveChanges();

            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                Sucursal = "01",
                PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                    new Common.Entities.PromocionCuponItem { PromocionId = 4 }
                },
                Productos = new Common.Entities.SerieFormasPago[] {
                    new SerieFormasPago { Serie = "0000003282389", FormasPago = new FormaPago[] { FormaPago.EF } }, //239.00
                    new SerieFormasPago { Serie = "0000003391871", FormasPago = new FormaPago[] { FormaPago.EF } }, //329.00
                    new SerieFormasPago { Serie = "0000003391872", FormasPago = new FormaPago[] { FormaPago.EF } }, //329.00
                    new SerieFormasPago { Serie = "0000003391948", FormasPago = new FormaPago[] { FormaPago.EF } }, //359.00
                    new SerieFormasPago { Serie = "0000003251820", FormasPago = new FormaPago[] { FormaPago.EF } }, //399.00
                    //new Common.Entities.SerieFormasPago { Serie = "0000003391948", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } }, //359.00
                }
            };

            var sale = new BusinessLogic.Sale();

            CheckPromocionesCuponesResponse res;
            using (ShimsContext.Create())
            {
                var now = DateTime.Parse("2019-12-02");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.ToUniversalTime();
                System.Fakes.ShimDateTime.NowGet = () => now;
                System.Fakes.ShimDateTime.TodayGet = () => now;
                res = sale.CheckPromociones(request);
            }
            Assert.IsNotNull(res.Promociones);
            var dic = res.Promociones.ToDictionary(i => i.Serie, i => i);
            Assert.IsNull(dic["0000003282389"].Fijo);
            Assert.AreEqual(83, dic["0000003391871"].Fijo);
            Assert.AreEqual(83, dic["0000003391872"].Fijo);
            Assert.AreEqual(83, dic["0000003391948"].Fijo);
            Assert.IsNull(dic["0000003251820"].Fijo);
        }

        [TestMethod]
        public void Promocion3x249_importe_sinunidades()
        {
            /*
239.00	0000003282389+
329.00	0000003391871+
329.00	0000003391872
359.00	0000003391948+
399.00	0000003251820
289.00	0000003446549+
             */
            var ctx = new DataAccess.SirCoPVDataContext();
            var prom = ctx.Promociones.Where(i => i.idpromocion == 4).Single();
            prom.minunicompra = 0;
            prom.minimpcompra = 1200m;
            var q = prom.Detalle.Where(i => i.numunidad > 1).ToList();
            q.ForEach(i => ctx.PromocionesDetalle.Remove(i));
            ctx.SaveChanges();

            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                Sucursal = "01",
                PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                    new Common.Entities.PromocionCuponItem { PromocionId = 4 }
                },
                Productos = new Common.Entities.SerieFormasPago[] {
                    new SerieFormasPago { Serie = "0000003282389", FormasPago = new FormaPago[] { FormaPago.EF } }, //239.00
                    new SerieFormasPago { Serie = "0000003391871", FormasPago = new FormaPago[] { FormaPago.EF } }, //329.00
                    new SerieFormasPago { Serie = "0000003391872", FormasPago = new FormaPago[] { FormaPago.EF } }, //329.00
                    new SerieFormasPago { Serie = "0000003391948", FormasPago = new FormaPago[] { FormaPago.EF } }, //359.00
                    new SerieFormasPago { Serie = "0000003251820", FormasPago = new FormaPago[] { FormaPago.EF } }, //399.00
                    new SerieFormasPago { Serie = "0000003446549", FormasPago = new FormaPago[] { FormaPago.EF } }, //289.00
                }
            };

            var sale = new BusinessLogic.Sale();

            CheckPromocionesCuponesResponse res;
            using (ShimsContext.Create())
            {
                var now = DateTime.Parse("2019-12-02");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.ToUniversalTime();
                System.Fakes.ShimDateTime.NowGet = () => now;
                System.Fakes.ShimDateTime.TodayGet = () => now;
                res = sale.CheckPromociones(request);
            }
            Assert.IsNotNull(res.Promociones);
            var dic = res.Promociones.ToDictionary(i => i.Serie, i => i);
            Assert.AreEqual(83, dic["0000003282389"].Fijo);
            Assert.AreEqual(83, dic["0000003391871"].Fijo);
            Assert.IsNull(dic["0000003391872"].Fijo);
            Assert.AreEqual(83, dic["0000003391948"].Fijo);
            Assert.IsNull(dic["0000003251820"].Fijo);
            Assert.AreEqual(83, dic["0000003446549"].Fijo);
        }

        [TestMethod]
        public void Promocion_chaleco_importe()
        {            
            /*
compra
0000003457845	AC	449.00	360	     89
0000003564641	AC	419.00	360	     92
0000003565021	AC	439.00	360	    108
0000003629990	AC	519.00	360	    115
0000003726993	AC	999.00	ADA	      1
0000003736481	AC	1029.00	ADA	      2

promo
0000003726768	AC	45.00	TOY	      6
0000003313845	AC	105.00	TOY	     25
0000003726746	AC	95.00	TOY	     27
             */
             
            var ctx = new DataAccess.SirCoPVDataContext();
            var prom = ctx.Promociones.Where(i => i.idpromocion == 20).Single();
            prom.minunicompra = 0;
            ctx.SaveChanges();

            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                Sucursal = "01",
                PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                    new Common.Entities.PromocionCuponItem { PromocionId = 20 }
                },
                Productos = new Common.Entities.SerieFormasPago[] {
                    new SerieFormasPago { Serie = "0000003457845", FormasPago = new FormaPago[] { FormaPago.EF } }, //449.00
                    new SerieFormasPago { Serie = "0000003564641", FormasPago = new FormaPago[] { FormaPago.EF } }, //419.00
                    new SerieFormasPago { Serie = "0000003565021", FormasPago = new FormaPago[] { FormaPago.EF } }, //439.00
                    new SerieFormasPago { Serie = "0000003629990", FormasPago = new FormaPago[] { FormaPago.EF } }, //519.00
                    //new SerieFormasPago { Serie = "0000003726993", FormasPago = new FormaPago[] { FormaPago.EF } }, //999.00
                    new SerieFormasPago { Serie = "0000003736481", FormasPago = new FormaPago[] { FormaPago.EF } }, //1029.00

                    new SerieFormasPago { Serie = "0000003726768", FormasPago = new FormaPago[] { } }, //45.00
                    new SerieFormasPago { Serie = "0000003313845", FormasPago = new FormaPago[] { } }, //405.00
                }
            };

            var sale = new BusinessLogic.Sale();

            CheckPromocionesCuponesResponse res;
            using (ShimsContext.Create())
            {
                var now = DateTime.Parse("2020-01-01");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.AddHours(5);
                System.Fakes.ShimDateTime.NowGet = () => now;
                System.Fakes.ShimDateTime.TodayGet = () => now;
                res = sale.CheckPromociones(request);
            }
            Assert.IsNotNull(res.Promociones);
            var dic = res.Promociones.ToDictionary(i => i.Serie, i => i);
            //compra
            Assert.AreEqual(20, dic["0000003457845"].PromocionId);
            Assert.IsNull(dic["0000003564641"].PromocionId);
            Assert.AreEqual(20, dic["0000003565021"].PromocionId);
            Assert.IsNull(dic["0000003629990"].PromocionId);
            //Assert.AreEqual(20, dic["0000003726993"].PromocionId);
            Assert.AreEqual(20, dic["0000003736481"].PromocionId);
            //promo
            Assert.AreEqual(45m, dic["0000003726768"].Descuento);
            Assert.IsNull(dic["0000003313845"].Descuento);
        }

        [TestMethod]
        public void Promocion_chaleco_importe_ticket()
        {
            /*
compra
0000003633753	AC	1499.00	ADD	   1304
0000003633754	AC	1499.00	ADD	   1304
0000003633758	AC	1499.00	ADD	   1304
0000003633759	AC	1499.00	ADD	   1304

0000003457845	AC	449.00	360	     89
0000003564641	AC	419.00	360	     92
0000003565021	AC	439.00	360	    108
0000003629990	AC	519.00	360	    115
0000003726993	AC	999.00	ADA	      1
0000003736481	AC	1029.00	ADA	      2

promo
0000003726768	AC	45.00	TOY	      6
0000003313845	AC	105.00	TOY	     25
0000003726746	AC	95.00	TOY	     27
             */

            var ctx = new DataAccess.SirCoPVDataContext();
            var prom = ctx.Promociones.Where(i => i.idpromocion == 20).Single();
            prom.minunicompra = 0;
            prom.importeticket = true;
            ctx.SaveChanges();

            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                Sucursal = "01",
                PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                    new Common.Entities.PromocionCuponItem { PromocionId = 20 }
                },
                Productos = new Common.Entities.SerieFormasPago[] {
                    new SerieFormasPago { Serie = "0000003633753", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 1499m } } }, //1499.00
                    new SerieFormasPago { Serie = "0000003633754", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 1499m } } }, //1499.00
                    new SerieFormasPago { Serie = "0000003633758", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 1499m } } }, //1499.00
                    
                    new SerieFormasPago { Serie = "0000003726768", FormasPago = new FormaPago[] { } }, //45.00
                    new SerieFormasPago { Serie = "0000003313845", FormasPago = new FormaPago[] { } }, //105.00
                }
            };

            var sale = new BusinessLogic.Sale();

            CheckPromocionesCuponesResponse res;
            using (ShimsContext.Create())
            {
                var now = DateTime.Parse("2020-01-01");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.ToUniversalTime();
                System.Fakes.ShimDateTime.NowGet = () => now;
                System.Fakes.ShimDateTime.TodayGet = () => now;
                res = sale.CheckPromociones(request);
            }
            Assert.IsNotNull(res.Promociones);
            var dic = res.Promociones.ToDictionary(i => i.Serie, i => i);
            //compra
            Assert.AreEqual(20, dic["0000003633753"].PromocionId);
            Assert.AreEqual(20, dic["0000003633754"].PromocionId);
            Assert.AreEqual(20, dic["0000003633758"].PromocionId);            
            //promo
            Assert.AreEqual(45m, dic["0000003726768"].Descuento);
            Assert.AreEqual(105m, dic["0000003313845"].Descuento);            
        }
        [TestMethod]
        public void Promocion_chaleco_importe_ticket_incompleto()
        {
            /*
compra
0000003633753	AC	1499.00	ADD	   1304
0000003633754	AC	1499.00	ADD	   1304
0000003633758	AC	1499.00	ADD	   1304
0000003633759	AC	1499.00	ADD	   1304

0000003457845	AC	449.00	360	     89
0000003564641	AC	419.00	360	     92
0000003565021	AC	439.00	360	    108
0000003629990	AC	519.00	360	    115
0000003726993	AC	999.00	ADA	      1
0000003736481	AC	1029.00	ADA	      2

promo
0000003726768	AC	45.00	TOY	      6
0000003313845	AC	105.00	TOY	     25
0000003726746	AC	95.00	TOY	     27
             */

            var ctx = new DataAccess.SirCoPVDataContext();
            var prom = ctx.Promociones.Where(i => i.idpromocion == 20).Single();
            prom.minunicompra = 0;
            prom.importeticket = true;
            ctx.SaveChanges();

            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                Sucursal = "01",
                PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                    new Common.Entities.PromocionCuponItem { PromocionId = 20 }
                },
                Productos = new Common.Entities.SerieFormasPago[] {
                    new SerieFormasPago { Serie = "0000003633753", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 1499m } } }, //1499.00
                    //new SerieFormasPago { Serie = "0000003633754", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 1499m } } }, //1499.00
                    //new SerieFormasPago { Serie = "0000003633758", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 1499m } } }, //1499.00
                    
                    new SerieFormasPago { Serie = "0000003726768", FormasPago = new FormaPago[] { } }, //45.00
                    new SerieFormasPago { Serie = "0000003313845", FormasPago = new FormaPago[] { } }, //105.00
                }
            };

            var sale = new BusinessLogic.Sale();

            CheckPromocionesCuponesResponse res;
            using (ShimsContext.Create())
            {
                var now = DateTime.Parse("2020-01-01");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.ToUniversalTime();
                System.Fakes.ShimDateTime.NowGet = () => now;
                System.Fakes.ShimDateTime.TodayGet = () => now;
                res = sale.CheckPromociones(request);
            }
            Assert.IsNotNull(res.Promociones);
            var dic = res.Promociones.ToDictionary(i => i.Serie, i => i);
            //compra
            Assert.AreEqual(0, dic["0000003633753"].PromocionId ?? 0);
            //Assert.AreEqual(20, dic["0000003633754"].PromocionId);
            //Assert.AreEqual(20, dic["0000003633758"].PromocionId);
            //promo
            Assert.AreEqual(0, dic["0000003726768"].Descuento ?? 0);
            Assert.AreEqual(0, dic["0000003313845"].Descuento ?? 0);
        }

        [TestMethod]
        public void Promocion_chaleco_importe_ticket_formapago()
        {
            /*
compra
0000003633753	AC	1499.00	ADD	   1304
0000003633754	AC	1499.00	ADD	   1304
0000003633758	AC	1499.00	ADD	   1304
0000003633759	AC	1499.00	ADD	   1304

0000003457845	AC	449.00	360	     89
0000003564641	AC	419.00	360	     92
0000003565021	AC	439.00	360	    108
0000003629990	AC	519.00	360	    115
0000003726993	AC	999.00	ADA	      1
0000003736481	AC	1029.00	ADA	      2

promo
0000003726768	AC	45.00	TOY	      6
0000003313845	AC	105.00	TOY	     25
0000003726746	AC	95.00	TOY	     27
             */

            var ctx = new DataAccess.SirCoPVDataContext();
            var prom = ctx.Promociones.Where(i => i.idpromocion == 20).Single();
            prom.minunicompra = 0;
            prom.importeticket = true;
            var det = prom.Detalle.Where(i => i.tipo == "COMPRA" && i.formapago != "EF");
            foreach (var item in det.ToArray())
            {
                ctx.PromocionesDetalle.Remove(item);
            }
            ctx.SaveChanges();

            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                Sucursal = "01",
                PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                    new Common.Entities.PromocionCuponItem { PromocionId = 20 }
                },
                Productos = new Common.Entities.SerieFormasPago[] {
                    new SerieFormasPago { Serie = "0000003633753", FormasPago = new FormaPago[] { FormaPago.VA }, Pagos = new Dictionary<Common.Constants.FormaPago, decimal?>() { { FormaPago.VA, 1499m } } }, //1499.00
                    new SerieFormasPago { Serie = "0000003633754", FormasPago = new FormaPago[] { FormaPago.VA }, Pagos = new Dictionary<Common.Constants.FormaPago, decimal?>() { { FormaPago.VA, 1499m } } }, //1499.00
                    new SerieFormasPago { Serie = "0000003633758", FormasPago = new FormaPago[] { FormaPago.VA }, Pagos = new Dictionary<Common.Constants.FormaPago, decimal?>() { { FormaPago.VA, 1499m } } }, //1499.00
                    
                    new SerieFormasPago { Serie = "0000003726768", FormasPago = new FormaPago[] { } }, //45.00
                    new SerieFormasPago { Serie = "0000003313845", FormasPago = new FormaPago[] { } }, //105.00
                }
            };

            var sale = new BusinessLogic.Sale();

            CheckPromocionesCuponesResponse res;
            using (ShimsContext.Create())
            {
                var now = DateTime.Parse("2020-01-01");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.AddHours(5);
                System.Fakes.ShimDateTime.NowGet = () => now;
                System.Fakes.ShimDateTime.TodayGet = () => now;
                res = sale.CheckPromociones(request);
            }
            Assert.IsNotNull(res.Promociones);
            var dic = res.Promociones.ToDictionary(i => i.Serie, i => i);
            //compra
            Assert.IsNull(dic["0000003633753"].PromocionId);
            Assert.IsNull(dic["0000003633754"].PromocionId);
            Assert.IsNull(dic["0000003633758"].PromocionId);
            //promo
            Assert.IsNull(dic["0000003726768"].Descuento);
            Assert.IsNull(dic["0000003313845"].Descuento);
        }

        [TestMethod]
        public void Promocion3x249_importe2()
        {
            /*
239.00	0000003282389
329.00	0000003391871
329.00	0000003391872
359.00	0000003391948
399.00	0000003251820
289.00	0000003446549

239.00 + 329.00 + 329.00 = 897
239.00 + 329.00 + 359.00 = 927
239.00 + 359.00 + 399.00 = 997
329.00 + 329.00 + 359.00 = 1017 <-- esperado
329.00 + 329.00 + 399.00 = 1057
329.00 + 359.00 + 399.00 = 1087
             */
            var ctx = new DataAccess.SirCoPVDataContext();
            var prom = ctx.Promociones.Where(i => i.idpromocion == 4).Single();
            prom.minimpcompra = 1000m;
            ctx.SaveChanges();

            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                Sucursal = "01",
                PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                    new Common.Entities.PromocionCuponItem { PromocionId = 4 }
                },
                Productos = new Common.Entities.SerieFormasPago[] {
                    //new SerieFormasPago { Serie = "0000003282389", FormasPago = new FormaPago[] { FormaPago.EF } }, //239.00
                    new SerieFormasPago { Serie = "0000003391871", FormasPago = new FormaPago[] { FormaPago.EF } }, //329.00
                    new SerieFormasPago { Serie = "0000003391872", FormasPago = new FormaPago[] { FormaPago.EF } }, //329.00
                    //new SerieFormasPago { Serie = "0000003391948", FormasPago = new FormaPago[] { FormaPago.EF } }, //359.00
                    new SerieFormasPago { Serie = "0000003251820", FormasPago = new FormaPago[] { FormaPago.EF } }, //399.00
                    //new Common.Entities.SerieFormasPago { Serie = "0000003391948", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } }, //359.00
                }
            };

            var sale = new BusinessLogic.Sale();

            CheckPromocionesCuponesResponse res;
            using (ShimsContext.Create())
            {
                var now = DateTime.Parse("2019-12-02");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.ToUniversalTime();
                System.Fakes.ShimDateTime.NowGet = () => now;
                System.Fakes.ShimDateTime.TodayGet = () => now;
                res = sale.CheckPromociones(request);
            }
            Assert.IsNotNull(res.Promociones);
            var dic = res.Promociones.ToDictionary(i => i.Serie, i => i);
            //Assert.IsNull(dic["0000003282389"].Fijo);
            Assert.AreEqual(83, dic["0000003391871"].Fijo);
            Assert.AreEqual(83, dic["0000003391872"].Fijo);
            //Assert.AreEqual(83, dic["0000003391948"].Fijo);
            Assert.AreEqual(83, dic["0000003251820"].Fijo);
        }

        [TestMethod]
        public void Promocion14_30desc_importe()
        {
            /*
1	NULL	0000003705505	CA	1099.00	ADD	   1267
2	NULL	0000003705497	CA	1099.00	ADD	   1267
3	NULL	0000003762046	CA	1199.00	ADD	   1195
4	NULL	0000003695594	CA	1549.00	NIK	   1990
             */
            var ctx = new DataAccess.SirCoPVDataContext();
            
            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                Sucursal = "08",
                PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                    new Common.Entities.PromocionCuponItem { PromocionId = 14 }
                },
                Productos = new Common.Entities.SerieFormasPago[] {
                    new SerieFormasPago { Serie = "0000003705505", FormasPago = new FormaPago[] { FormaPago.EF } }, //1099.00
                    new SerieFormasPago { Serie = "0000003705497", FormasPago = new FormaPago[] { FormaPago.EF } }, //1099.00
                    new SerieFormasPago { Serie = "0000003762046", FormasPago = new FormaPago[] { FormaPago.EF } }, //1199.00
                    new SerieFormasPago { Serie = "0000003695594", FormasPago = new FormaPago[] { FormaPago.EF } }, //1549.00                    
                }
            };

            var sale = new BusinessLogic.Sale();

            CheckPromocionesCuponesResponse res;
            using (ShimsContext.Create())
            {
                //var ctx = new DataAccess.SirCoDataContext();
                var data = new List<DataAccess.SirCoControl.Sucursal> {
                    new DataAccess.SirCoControl.Sucursal { idsucursal = 1, sucursal = "08", cajas = 100001, devolvta = 200001,
                        Plaza = new DataAccess.SirCoControl.Plaza { plaza = "01" }
                    }
                }.AsQueryable();

                var mockSet = new Mock<DbSet<DataAccess.SirCoControl.Sucursal>>();
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

                DataAccess.Fakes.ShimSirCoControlDataContext.AllInstances.SucursalesGet = (sctx) => {
                    return mockSet.Object;
                };
                var now = DateTime.Parse("2020-01-01");
                System.Fakes.ShimDateTime.NowGet = () => now;
                System.Fakes.ShimDateTime.TodayGet = () => now;
                res = sale.CheckPromociones(request);
            }
            Assert.IsNotNull(res.Promociones);
            var dic = res.Promociones.ToDictionary(i => i.Serie, i => i);
            Assert.AreEqual(0, dic["0000003705505"].Descuento ?? 0);
            Assert.AreEqual(0, dic["0000003705497"].Descuento ?? 0);
            Assert.AreEqual(0, dic["0000003762046"].Descuento ?? 0);
            Assert.AreEqual(0, dic["0000003695594"].Descuento ?? 0);
        }

        [TestMethod]
        public void Promocion3DescuentoyAccesorioGratis()
        {
            /*
             * compra
            0000003243487	599.00	ALX	      3
            0000003243568	599.00	ALX	      3
            0000003244194	599.00	ALX	      3
            0000003266838	729.00	ARE	      7
            * promo
            0000003008310	49.00	TOY	      4
            0000003313843	105.00	TOY	     25
            */

            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                Sucursal = "01",
                PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                    new Common.Entities.PromocionCuponItem { PromocionId = 17, Cupon = "0000000251" }
                },
                Productos = new Common.Entities.SerieFormasPago[] {
                    new SerieFormasPago { Serie = "0000003243487", FormasPago = new FormaPago[] { FormaPago.EF } }, //599
                    new SerieFormasPago { Serie = "0000003243568", FormasPago = new FormaPago[] { FormaPago.EF } }, //599
                    new SerieFormasPago { Serie = "0000003244194", FormasPago = new FormaPago[] { FormaPago.EF } }, //599
                    new SerieFormasPago { Serie = "0000003008310", FormasPago = new FormaPago[] { FormaPago.EF } }, //49
                    //new SerieFormasPago { Serie = "", FormasPago = new FormaPago[] { FormaPago.EF } }, //399.00
                    //new SerieFormasPago { Serie = "", FormasPago = new FormaPago[] { FormaPago.EF } }, //359.00
                }
            };

            var sale = new BusinessLogic.Sale();

            CheckPromocionesCuponesResponse res;
            using (ShimsContext.Create())
            {
                var now = DateTime.Parse("2019-12-31T11:00:00");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.AddHours(5);
                System.Fakes.ShimDateTime.NowGet = () => now;
                System.Fakes.ShimDateTime.TodayGet = () => DateTime.Parse("2019-12-31");
                res = sale.CheckPromociones(request);
            }
            Assert.IsNotNull(res.Promociones);
            var dic = res.Promociones.ToDictionary(i => i.Serie, i => i);
            //descuentos
            Assert.AreEqual(59.9m, dic["0000003243487"].Descuento ?? 0);
            Assert.AreEqual(59.9m, dic["0000003243568"].Descuento ?? 0);
            Assert.AreEqual(0, dic["0000003244194"].Descuento ?? 0);
            Assert.AreEqual(49m, dic["0000003008310"].Descuento ?? 0);
            //dinero
            Assert.AreEqual(50m, dic["0000003243487"].Monedero ?? 0);
            Assert.AreEqual(59.9m, dic["0000003243568"].Monedero ?? 0);
            Assert.AreEqual(59.9m, dic["0000003244194"].Monedero ?? 0);
            Assert.AreEqual(100m, dic["0000003008310"].Monedero ?? 0);
        }
        [TestMethod]
        public void Promocion3x249_1()
        {
            /*
1	NULL	0000003259802	BA	439.00	MAE	      7	28-	A	25	30	660
2	NULL	0000003259791	BA	439.00	MAE	      7	26-	A	25	30	660
3	NULL	0000003251820	BA	399.00	MLA	     21	26-	A	22	26-	618
4	NULL	0000003282841	BA	239.00	KOA	      4	13-	A	12	14-	617
5	NULL	0000003391872	BA	329.00	KLI	      1	21-	D	18	21-	380
             */

            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                Sucursal = "01",
                PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                    new Common.Entities.PromocionCuponItem { PromocionId = 4 }
                },
                Productos = new Common.Entities.SerieFormasPago[] {
                    new Common.Entities.SerieFormasPago { Serie = "0000003259802", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } }, //439.00
                    new Common.Entities.SerieFormasPago { Serie = "0000003259791", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } }, //439.00
                    new Common.Entities.SerieFormasPago { Serie = "0000003251820", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } }, //399.00
                    new Common.Entities.SerieFormasPago { Serie = "0000003282841", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } }, //239.00
                    new Common.Entities.SerieFormasPago { Serie = "0000003391872", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } }, //329.00
                    //new Common.Entities.SerieFormasPago { Serie = "0000003391948", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } }, //359.00
                }
            };

            var sale = new BusinessLogic.Sale();

            CheckPromocionesCuponesResponse res;
            using (ShimsContext.Create())
            {
                var now = DateTime.Parse("2019-12-02");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.ToUniversalTime();
                System.Fakes.ShimDateTime.NowGet = () => now;
                System.Fakes.ShimDateTime.TodayGet = () => now;
                res = sale.CheckPromociones(request);
            }
            Assert.IsNotNull(res.Promociones);
            var dic = res.Promociones.ToDictionary(i => i.Serie, i => i);
            Assert.IsNull(dic["0000003259802"].Fijo);
            Assert.IsNull(dic["0000003259791"].Fijo);
            Assert.AreEqual(83, dic["0000003251820"].Fijo);
            Assert.AreEqual(83, dic["0000003282841"].Fijo);
            Assert.AreEqual(83, dic["0000003391872"].Fijo);
        }
        [TestMethod]
        public void Promocion_149_CONT_259_CREDIT()
        {
            /*
0000003479582	CA	349.00	NIN	   1149
0000003479771	CA	349.00	NIN	   1149
0000003484777	CA	269.00	MNP	    175
             */

            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                Sucursal = "01",
                PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                    new Common.Entities.PromocionCuponItem { PromocionId = 3 }
                },
                Productos = new Common.Entities.SerieFormasPago[] {
                    new SerieFormasPago { Serie = "0000003479771", FormasPago = new FormaPago[] { FormaPago.EF } },//349
                    new SerieFormasPago { Serie = "0000003479582", FormasPago = new FormaPago[] { FormaPago.EF, FormaPago.VA } }, //349
                    new SerieFormasPago { Serie = "0000003484777", FormasPago = new FormaPago[] { FormaPago.VA } }, //269
                }
            };

            var sale = new BusinessLogic.Sale();

            CheckPromocionesCuponesResponse res;
            using (ShimsContext.Create())
            {
                var now = DateTime.Parse("2019-12-02");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.AddHours(5);
                System.Fakes.ShimDateTime.NowGet = () => now;
                System.Fakes.ShimDateTime.TodayGet = () => now;
                res = sale.CheckPromociones(request);
            }
            Assert.IsNotNull(res.Promociones);
            var dic = res.Promociones.ToDictionary(i => i.Serie, i => i);
            Assert.AreEqual(149m, dic["0000003479771"].Fijo);
            Assert.AreEqual(259m, dic["0000003479582"].Fijo);
            Assert.AreEqual(259m, dic["0000003484777"].Fijo);
        }
        [TestMethod]
        public void Promocion3x249_doble()
        {
            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                Sucursal = "01",
                PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                    new Common.Entities.PromocionCuponItem { PromocionId = 4 }
                },
                Productos = new Common.Entities.SerieFormasPago[] {
                    new Common.Entities.SerieFormasPago { Serie = "0000003259802", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } },
                    new Common.Entities.SerieFormasPago { Serie = "0000003259791", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } },
                    new Common.Entities.SerieFormasPago { Serie = "0000003251820", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } },
                    new Common.Entities.SerieFormasPago { Serie = "0000003282841", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } },
                    new Common.Entities.SerieFormasPago { Serie = "0000003391872", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } },
                    new Common.Entities.SerieFormasPago { Serie = "0000003391948", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } },
                }
            };

            var sale = new BusinessLogic.Sale();

            CheckPromocionesCuponesResponse res;
            using (ShimsContext.Create())
            {
                var now = DateTime.Parse("2019-12-02");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.ToUniversalTime();
                System.Fakes.ShimDateTime.NowGet = () => now;
                System.Fakes.ShimDateTime.TodayGet = () => now;
                res = sale.CheckPromociones(request);
            }
            Assert.IsNotNull(res.Promociones);
            var dic = res.Promociones.ToDictionary(i => i.Serie, i => i);
            Assert.AreEqual(83, dic["0000003259802"].Fijo);
            Assert.AreEqual(83, dic["0000003259791"].Fijo);
            Assert.AreEqual(83, dic["0000003251820"].Fijo);
            Assert.AreEqual(83, dic["0000003282841"].Fijo);
            Assert.AreEqual(83, dic["0000003391872"].Fijo);
            Assert.AreEqual(83, dic["0000003391948"].Fijo);
        }

        [TestMethod]
        public void Promocion3x249_Cambio()
        {
            var ctx = new DataAccess.SirCoDataContext();
            var data = new List<DataAccess.SirCoControl.Sucursal> {
                    new DataAccess.SirCoControl.Sucursal { idsucursal = 1, sucursal = "01", cajas = 100001, devolvta = 200001,
                        Plaza = new DataAccess.SirCoControl.Plaza { plaza = "01" }
                    }
                }.AsQueryable();

            var mockSet = new Mock<DbSet<DataAccess.SirCoControl.Sucursal>>();
            mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var request = new SaleRequest
            {
                Sucursal = "01",
                PromocionesCupones = new PromocionCuponItem[] {
                    new PromocionCuponItem { PromocionId = 4 }
                },
                Productos = new SerieFormasPago[] {
                    new SerieFormasPago { Serie = "0000003391872", FormasPago = new FormaPago[] { FormaPago.EF } },
                    new SerieFormasPago { Serie = "0000003282841", FormasPago = new FormaPago[] { FormaPago.EF } },
                    new SerieFormasPago { Serie = "0000003259802", FormasPago = new FormaPago[] { FormaPago.EF } }                    
                },
                Pagos = new Pago[] {
                    new Pago { FormaPago = FormaPago.EF, Importe = 249 }
                }
            };

            var serie = ctx.Series.Where(i => i.serie == "0000003259802").Single();
            Assert.AreEqual("AC", serie.status);

            ctx.UpdateSerieStatus("0000003391872", Status.CA, Status.AC, 0);
            ctx.UpdateSerieStatus("0000003282841", Status.CA, Status.AC, 0);
            ctx.UpdateSerieStatus("0000003259802", Status.CA, Status.AC, 0);

            var sale = new BusinessLogic.Sale();
            var process = new BusinessLogic.Process();            

            SaleResponse res;
            using (ShimsContext.Create())
            {
                var now = DateTime.Parse("2019-12-02");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.ToUniversalTime();
                System.Fakes.ShimDateTime.NowGet = () => now;
                System.Fakes.ShimDateTime.TodayGet = () => now;

                DataAccess.Fakes.ShimSirCoControlDataContext.AllInstances.SucursalesGet = (sctx) => {
                    return mockSet.Object;
                };

                res = process.Sale(request, 0);
            }
            var ctxpv = new DataAccess.SirCoPVDataContext();

            var venta = ctxpv.Ventas.Where(i => i.sucursal == "01" && i.venta == "100002").Single();
            var dic = venta.Detalles.ToDictionary(i => i.serie, i => i);
            Assert.AreEqual(83, dic["0000003259802"].precdesc);
            Assert.AreEqual(83, dic["0000003282841"].precdesc);
            Assert.AreEqual(83, dic["0000003259802"].precdesc);

            ctx.Entry(serie).Reload();
            Assert.AreEqual("BA", serie.status);

            var crequest = new Common.Entities.ChangeRequest
            {
                Sucursal = "01",
                Folio = "100002",
                Items = new ChangeItem[] {
                    new ChangeItem { OldItem = "0000003259802", NewItem = "0000003259791" }
                }
            };

            ctx.UpdateSerieStatus("0000003259791", Status.CA, Status.AC, 0);

            using (ShimsContext.Create())
            {
                var now = DateTime.Parse("2019-12-02");
                System.Fakes.ShimDateTime.NowGet = () => now;
                System.Fakes.ShimDateTime.TodayGet = () => now;

                DataAccess.Fakes.ShimSirCoControlDataContext.AllInstances.SucursalesGet = (sctx) => {
                    return mockSet.Object;
                };

                var cres = process.Change(crequest, 0, request.Sucursal);
            }

            venta = ctxpv.Ventas.Where(i => i.sucursal == "01" && i.venta == "100003").Single();
            dic = venta.Detalles.ToDictionary(i => i.serie, i => i);
            Assert.AreEqual(83, dic["0000003259791"].precdesc);

            ctx.Entry(serie).Reload();
            Assert.AreEqual("AC", serie.status);
            serie = ctx.Series.Where(i => i.serie == "0000003259791").Single();
            Assert.AreEqual("BA", serie.status);
        }

        [TestMethod]
        public void VentaPromocion3x249_2()
        {
            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                Sucursal = "01",
                PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                    new Common.Entities.PromocionCuponItem { PromocionId = 4 }
                },
                Productos = new Common.Entities.SerieFormasPago[] {
                    new Common.Entities.SerieFormasPago { Serie = "0000003259802", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } },
                    new Common.Entities.SerieFormasPago { Serie = "0000003259791", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } },
                    new Common.Entities.SerieFormasPago { Serie = "0000003251820", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } },
                    new Common.Entities.SerieFormasPago { Serie = "0000003282841", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } },
                    new Common.Entities.SerieFormasPago { Serie = "0000003391872", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } },
                    new Common.Entities.SerieFormasPago { Serie = "0000003391948", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } },
                }
            };

            var ctx = new DataAccess.SirCoDataContext();
            ctx.UpdateSerieStatus("0000003259802", Status.CA, Status.AC, 0);
            ctx.UpdateSerieStatus("0000003259791", Status.CA, Status.AC, 0);
            ctx.UpdateSerieStatus("0000003251820", Status.CA, Status.AC, 0);
            ctx.UpdateSerieStatus("0000003282841", Status.CA, Status.AC, 0);
            ctx.UpdateSerieStatus("0000003391872", Status.CA, Status.AC, 0);
            ctx.UpdateSerieStatus("0000003391948", Status.CA, Status.AC, 0);

            var bl = new BusinessLogic.Process();

            var srequest = new SaleRequest
            {
                Productos = request.Productos,
                VendedorId = 0,
                Sucursal = "01",                
                PromocionesCupones = request.PromocionesCupones,
                Cliente = null,
                Pagos = new Common.Entities.Pago[] 
                {
                    new Pago { FormaPago = Common.Constants.FormaPago.EF, Importe = 249 + 249 }
                }
            };
            
            using (ShimsContext.Create())
            {
                var now = DateTime.Parse("2019-12-02");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.ToUniversalTime();
                System.Fakes.ShimDateTime.NowGet = () => now;
                System.Fakes.ShimDateTime.TodayGet = () => now;

                var data = new List<DataAccess.SirCoControl.Sucursal> {
                    new DataAccess.SirCoControl.Sucursal {
                        sucursal = "01",
                        cajas = 434045,
                        Plaza = new DataAccess.SirCoControl.Plaza
                        {
                            idplaza = 1,
                            plaza = "01"
                        }
                    }
                }.AsQueryable();

                var mockSet = new Mock<DbSet<DataAccess.SirCoControl.Sucursal>>();
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

                DataAccess.Fakes.ShimSirCoControlDataContext.AllInstances.SucursalesGet = (sctx) => {
                    return mockSet.Object;
                };

                var folio = bl.Sale(srequest, 0);
            }

            var ctxpv = new DataAccess.SirCoPVDataContext();
            var venta = ctxpv.Ventas.Where(i => i.sucursal == "01" && i.venta == "434046").Single();

            var dic = venta.Detalles.ToDictionary(i => i.serie, i => i);
            Assert.AreEqual(83, dic["0000003259802"].precdesc ?? 0);
            Assert.AreEqual(83, dic["0000003259791"].precdesc ?? 0);
            Assert.AreEqual(83, dic["0000003251820"].precdesc ?? 0);
            Assert.AreEqual(83, dic["0000003282841"].precdesc ?? 0);
            Assert.AreEqual(83, dic["0000003391872"].precdesc ?? 0);
            Assert.AreEqual(83, dic["0000003391948"].precdesc ?? 0);
        }

        [TestMethod]
        public void CheckPromocion30_20_NoDevolucion()
        {
            /*
0000003021989	849.00	PPP	     12
0000003033003	529.00	YUY	    100
0000003034403	329.00	TOK	   1381
0000003034408	329.00	TOK	   1381
             */

            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                Sucursal = "01",
                PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                    new Common.Entities.PromocionCuponItem { PromocionId = 2 }
                },
                Productos = new Common.Entities.SerieFormasPago[] {
                    new Common.Entities.SerieFormasPago { Serie = "0000003034403", FormasPago = new FormaPago[] { FormaPago.EF, FormaPago.DV } }
                }
            };

            var bl = new BusinessLogic.Sale();

            CheckPromocionesCuponesResponse res = null;
            using (ShimsContext.Create())
            {
                var now = DateTime.Parse("2019-12-02");
                System.Fakes.ShimDateTime.NowGet = () => now;
                System.Fakes.ShimDateTime.TodayGet = () => now;

                res = bl.CheckPromociones(request);
            }            

            var dic = res.Promociones.ToDictionary(i => i.Serie, i => i);
            Assert.AreEqual(0, dic["0000003034403"].Descuento ?? 0);
            Assert.AreEqual(0, dic["0000003034403"].Fijo ?? 0);
        }

        [TestMethod]
        public void VentaPromocion3x249_Devolucion()
        {
            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                Sucursal = "01",
                PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                    new Common.Entities.PromocionCuponItem { PromocionId = 4 }
                },
                Productos = new Common.Entities.SerieFormasPago[] {
                    new Common.Entities.SerieFormasPago { Serie = "0000003259802", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } },
                    new Common.Entities.SerieFormasPago { Serie = "0000003259791", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } },
                    new Common.Entities.SerieFormasPago { Serie = "0000003251820", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } },
                    new Common.Entities.SerieFormasPago { Serie = "0000003282841", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } },
                    new Common.Entities.SerieFormasPago { Serie = "0000003391872", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } },
                    new Common.Entities.SerieFormasPago { Serie = "0000003391948", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } },
                }
            };

            var ctx = new DataAccess.SirCoDataContext();
            ctx.UpdateSerieStatus("0000003259802", Status.CA, Status.AC, 0);
            ctx.UpdateSerieStatus("0000003259791", Status.CA, Status.AC, 0);
            ctx.UpdateSerieStatus("0000003251820", Status.CA, Status.AC, 0);
            ctx.UpdateSerieStatus("0000003282841", Status.CA, Status.AC, 0);
            ctx.UpdateSerieStatus("0000003391872", Status.CA, Status.AC, 0);
            ctx.UpdateSerieStatus("0000003391948", Status.CA, Status.AC, 0);

            var bl = new BusinessLogic.Process();

            var srequest = new SaleRequest
            {
                Productos = request.Productos,
                VendedorId = 0,
                Sucursal = "01",
                PromocionesCupones = request.PromocionesCupones,
                Cliente = null,
                Pagos = new Common.Entities.Pago[]
                {
                    new Pago { FormaPago = Common.Constants.FormaPago.EF, Importe = 249 + 249 }
                }
            };

            using (ShimsContext.Create())
            {
                var now = DateTime.Parse("2019-12-02");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.ToUniversalTime();
                System.Fakes.ShimDateTime.NowGet = () => now;
                System.Fakes.ShimDateTime.TodayGet = () => now;

                var data = new List<DataAccess.SirCoControl.Sucursal> {
                    new DataAccess.SirCoControl.Sucursal {
                        sucursal = "01",
                        cajas = 434045,
                        Plaza = new DataAccess.SirCoControl.Plaza
                        {
                            idplaza = 1,
                            plaza = "01"
                        }
                    }
                }.AsQueryable();

                var mockSet = new Mock<DbSet<DataAccess.SirCoControl.Sucursal>>();
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

                DataAccess.Fakes.ShimSirCoControlDataContext.AllInstances.SucursalesGet = (sctx) => {
                    return mockSet.Object;
                };

                var folio = bl.Sale(srequest, 0);
            }

            var ctxpv = new DataAccess.SirCoPVDataContext();
            var venta = ctxpv.Ventas.Where(i => i.sucursal == "01" && i.venta == "434046").Single();

            var rrequest = new Common.Entities.ReturnRequest
            {
                Items = new string[] {
                    "0000003259802",
                    "0000003259791"
                },
                Comments = "devolucion",
                Sucursal = "01",
                Folio = "434046"
            };

            using (ShimsContext.Create())
            {
                var now = DateTime.Parse("2019-12-12");
                System.Fakes.ShimDateTime.NowGet = () => now;
                System.Fakes.ShimDateTime.TodayGet = () => now;

                var data = new List<DataAccess.SirCoControl.Sucursal> {
                    new DataAccess.SirCoControl.Sucursal {
                        sucursal = "01",
                        cajas = 434045,
                        devolvta = 12345,
                        Plaza = new DataAccess.SirCoControl.Plaza
                        {
                            idplaza = 1,
                            plaza = "01"
                        }
                    }
                }.AsQueryable();

                var mockSet = new Mock<DbSet<DataAccess.SirCoControl.Sucursal>>();
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

                DataAccess.Fakes.ShimSirCoControlDataContext.AllInstances.SucursalesGet = (sctx) => {
                    return mockSet.Object;
                };

                var folio = bl.Return(rrequest, 0, request.Sucursal);
            }

            var devolucion = ctxpv.Devoluciones.Where(i => i.sucursal == "01" && i.devolvta == "012346").Single();

            var dic = devolucion.Detalles.ToDictionary(i => i.serie, i => i);
            Assert.AreEqual(83 + 83m, devolucion.disponible);
            Assert.AreEqual(83m, dic["0000003259802"].precdesc);
            Assert.AreEqual(83m, dic["0000003259791"].precdesc);
        }

        [TestMethod]
        public void Promocion3x1_mas_medio()
        {
            /*
            1	NULL	0000003025615	AC	2799.00	TIM	     10	22	A	22	25	548
            2	NULL	0000003182573	AC	449.00	JOZ	      3	24	A	23	27	624
            3	NULL	0000003183694	AC	449.00	JOZ	      3	24-	A	23	27	624
            4	NULL	0000003216267	AC	459.00	DOR	      3	23	A	22	27	646
            5	NULL	0000003216281	AC	459.00	DOR	      3	27	A	22	27	646
            6	NULL	0000003218340	AC	629.00	ZIA	    148	23	A	22	27	110
            */
            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                Sucursal = "01",
                PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                    new Common.Entities.PromocionCuponItem { PromocionId = 18 }
                },
                Productos = new Common.Entities.SerieFormasPago[] {
                    new Common.Entities.SerieFormasPago { Serie = "0000003025615", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } }, //2799.00 - compra
                    new Common.Entities.SerieFormasPago { Serie = "0000003182573", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } }, //449.00 - promo 100%
                    new Common.Entities.SerieFormasPago { Serie = "0000003183694", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.TC } }, //449.00 - promo 50%
                    new Common.Entities.SerieFormasPago { Serie = "0000003216267", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } }, //459.00 - compra
                    new Common.Entities.SerieFormasPago { Serie = "0000003216281", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } }, //459.00
                    new Common.Entities.SerieFormasPago { Serie = "0000003218340", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } }, //629.00 - compra
                }
            };

            var sale = new BusinessLogic.Sale();

            CheckPromocionesCuponesResponse res;
            using (ShimsContext.Create())
            {
                var now = DateTime.Parse("2019-12-30");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.ToUniversalTime();                

                res = sale.CheckPromociones(request);
            }
            Assert.IsNotNull(res.Promociones);
            var dic = res.Promociones.ToDictionary(i => i.Serie, i => i);
            Assert.AreEqual(0, dic["0000003025615"].Descuento);//compra
            Assert.AreEqual(449, dic["0000003182573"].Descuento);//promo 100%
            Assert.AreEqual(449.00m / 2, dic["0000003183694"].Descuento);//promo 50%
            Assert.AreEqual(0, dic["0000003216267"].Descuento);//compra
            Assert.IsNull(dic["0000003216281"].Descuento);//no aplica
            Assert.AreEqual(0, dic["0000003218340"].Descuento);//compra
        }

        [TestMethod]
        public void Promocion3x2Test()
        {
            /*
0000003311472	559.00	PLT	     72
0000003540070	609.00	PAU	    638
0000003540074	609.00	PAU	    638
0000003632625	1199.00	ADD	   1195
0000003635956	999.00	ADD	   1207
0000003668367	149.00	ARA	    174
             */

            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                Sucursal = "01",
                PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                    new Common.Entities.PromocionCuponItem { Cupon = "0000000181", PromocionId = 11 }
                },
                Productos = new Common.Entities.SerieFormasPago[] {
                    new SerieFormasPago { Serie = "0000003540070", FormasPago = new FormaPago[] { FormaPago.EF } },//609
                    new SerieFormasPago { Serie = "0000003540074", FormasPago = new FormaPago[] { FormaPago.EF } },//609
                    new SerieFormasPago { Serie = "0000003311472", FormasPago = new FormaPago[] { FormaPago.EF } },//559

                    new SerieFormasPago { Serie = "0000003632625", FormasPago = new FormaPago[] { FormaPago.EF } },//1199
                    new SerieFormasPago { Serie = "0000003635956", FormasPago = new FormaPago[] { FormaPago.EF } },//999
                    new SerieFormasPago { Serie = "0000003668367", FormasPago = new FormaPago[] { FormaPago.EF } }//149
                },                
            };

            var check = new Common.Entities.CheckPromocionesRequest
            {

            };

            var sale = new BusinessLogic.Sale();
            CheckPromocionesCuponesResponse res;
            using (ShimsContext.Create())
            {
                var now = DateTime.Parse("2019-08-01");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.ToUniversalTime();                

                //sale.GetPromociones()

                res = sale.CheckPromociones(request);
            }
            Assert.IsNotNull(res.Promociones);
            Assert.AreEqual(6, res.Promociones.Count());
            var dic = res.Promociones.ToDictionary(i => i.Serie, i => i);            
            Assert.AreEqual(0, dic["0000003540070"].Descuento ?? 0);//compra 1
            Assert.AreEqual(0, dic["0000003540074"].Descuento ?? 0);//compra 1
            Assert.AreEqual(559m, dic["0000003311472"].Descuento ?? 0);//promo 1
            Assert.AreEqual(0, dic["0000003632625"].Descuento ?? 0);//sin promo
            Assert.AreEqual(0, dic["0000003635956"].Descuento ?? 0);//sin promo
            Assert.AreEqual(0, dic["0000003668367"].Descuento ?? 0);//sin promo
        }

        [TestMethod]
        public void TestSerieCorridaRopa()
        {
            var ctx = new DataAccess.SirCoDataContext();
            var serie = ctx.Series.Where(i => i.serie == "0000003714112").Single();
            var corrida = ctx.Corridas.Where(i => i.marca == serie.marca && i.estilon == serie.estilon && i.proveedor == serie.proveedors
                    && String.Compare(ctx.ValorMedida(serie.medida), ctx.ValorMedida(i.medini)) >= 0 
                    && String.Compare(ctx.ValorMedida(serie.medida), ctx.ValorMedida(i.medfin)) <= 0)
                    .SingleOrDefault();

            Assert.IsNotNull(corrida);
        }


        [TestMethod]
        public void CheckPromocion30_20_VS_SinPromocion()
        {
            /*
0000003021989	849.00	PPP	     12
0000003033003	529.00	YUY	    100
0000003034403	329.00	TOK	   1381
0000003034408	329.00	TOK	   1381
             */

            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                Sucursal = "01",
                PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                    new Common.Entities.PromocionCuponItem { PromocionId = 2 }
                },
                Productos = new Common.Entities.SerieFormasPago[] {
                    new Common.Entities.SerieFormasPago {
                        Serie = "0000003034403", //329.00
                        FormasPago = new FormaPago[] { FormaPago.VA },
                        Promociones = new Dictionary<FormaPago, bool> { { FormaPago.VA, false } }
                    }
                }
            };

            var bl = new BusinessLogic.Sale();

            CheckPromocionesCuponesResponse res = null;
            using (ShimsContext.Create())
            {
                var now = DateTime.Parse("2019-12-02");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.ToUniversalTime();                

                res = bl.CheckPromociones(request);
            }

            var dic = res.Promociones.ToDictionary(i => i.Serie, i => i);
            Assert.AreEqual(164.5m, dic["0000003034403"].Descuento ?? 0); //329.00 / 2 = 164.5
            Assert.AreEqual(0, dic["0000003034403"].Fijo ?? 0);
        }
        [TestMethod]
        public void CheckPromocion30_20_VS_ConPromocion()
        {
            /*
0000003021989	849.00	PPP	     12
0000003033003	529.00	YUY	    100
0000003034403	329.00	TOK	   1381
0000003034408	329.00	TOK	   1381
             */

            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                Sucursal = "01",
                PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                    new Common.Entities.PromocionCuponItem { PromocionId = 2 }
                },
                Productos = new Common.Entities.SerieFormasPago[] {
                    new Common.Entities.SerieFormasPago {
                        Serie = "0000003034403", //329.00
                        FormasPago = new FormaPago[] { FormaPago.VA },
                        Promociones = new Dictionary<FormaPago, bool> { { FormaPago.VA, true } }
                    }
                }
            };

            var bl = new BusinessLogic.Sale();

            CheckPromocionesCuponesResponse res = null;
            using (ShimsContext.Create())
            {
                var now = DateTime.Parse("2019-12-02");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.ToUniversalTime();

                res = bl.CheckPromociones(request);
            }

            var dic = res.Promociones.ToDictionary(i => i.Serie, i => i);
            Assert.AreEqual(65.8m, dic["0000003034403"].Descuento ?? 0); //20%
            Assert.AreEqual(0, dic["0000003034403"].Fijo ?? 0);
        }
    }
}
