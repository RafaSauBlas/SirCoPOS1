using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Collections.Generic;
using Microsoft.QualityTools.Testing.Fakes;
using SirCoPOS.Common.Constants;
using Moq;
using System.Linq;
using System.Data.Entity;
using SirCoPOS.Common.Entities;

namespace SirCoPOS.Tests
{
    [TestClass]
    public class PromocionesUnitTest
    {
        public TestContext TestContext { get; set; }
        public PromocionesUnitTest()
        {
            _process = new BusinessLogic.Process();
            _sale = new BusinessLogic.Sale();
        }
        private BusinessLogic.Process _process;
        private BusinessLogic.Sale _sale;
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
        public void SalePagoCuponAdidasTest()
        {
            //sucursal = '01' and venta = '434045'
            var model = new Common.Entities.SaleRequest()
            {
                Sucursal = "01",
                VendedorId = 0,
                Cliente = new Common.Entities.Cliente
                {
                    Id = 810374
                }
            };
            model.PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                new Common.Entities.PromocionCuponItem { Cupon = "0000000101", PromocionId = 10 }
            };

            model.Productos = new Common.Entities.SerieFormasPago[] {
                new Common.Entities.SerieFormasPago { Serie = "0000003632625", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } },
                new Common.Entities.SerieFormasPago { Serie = "0000003635956", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF} },
                new Common.Entities.SerieFormasPago { Serie = "0000003668367", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } }//no adidas
            };
            model.Pagos = new List<Common.Entities.Pago> {
                new Common.Entities.Pago { FormaPago = FormaPago.EF, Importe = 2017.3m }
            };

            var ctx = new DataAccess.SirCoDataContext();
            ctx.UpdateSerieStatus("0000003632625", Status.CA, Status.AC, 0);
            ctx.UpdateSerieStatus("0000003635956", Status.CA, Status.AC, 0);
            ctx.UpdateSerieStatus("0000003668367", Status.CA, Status.AC, 0);

            Common.Entities.SaleResponse folio = null;
            using (ShimsContext.Create())
            {
                var data = new List<DataAccess.SirCoControl.Sucursal> {
                    new DataAccess.SirCoControl.Sucursal { idsucursal = 1, sucursal = "01", cajas = 434045, idplaza = 1,
                        Plaza = new DataAccess.SirCoControl.Plaza {
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

                var now = DateTime.Parse("2019-07-04");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.AddHours(5);
                System.Fakes.ShimDateTime.NowGet = () => now;
                folio = _process.Sale(model, 0);

                Assert.AreEqual(20, folio.Monedero);
            }
            Assert.AreEqual("434046", folio.Folio);
            var sale = _sale.FindSale(model.Sucursal, folio.Folio);
            Assert.IsNotNull(sale);
        }

        [TestMethod]
        public void SalePagoCuponAdidasMulitplePagoTest()
        {
            //sucursal = '01' and venta = '434045'
            var model = new Common.Entities.SaleRequest()
            {
                Sucursal = "01",
                VendedorId = 0,
                Cliente = new Common.Entities.Cliente
                {
                    Id = 810374
                }
            };
            model.PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                new Common.Entities.PromocionCuponItem { Cupon = "0000000101", PromocionId = 10 }
            };

            model.Productos = new Common.Entities.SerieFormasPago[] {
                new Common.Entities.SerieFormasPago { Serie = "0000003632625", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } },
                new Common.Entities.SerieFormasPago { Serie = "0000003635956", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF, FormaPago.TC } },
                new Common.Entities.SerieFormasPago { Serie = "0000003668367", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.TC } }//no adidas
            };
            model.Pagos = new List<Common.Entities.Pago> {
                new Common.Entities.Pago { FormaPago = FormaPago.EF, Importe = 1500m },
                new Common.Entities.Pago { FormaPago = FormaPago.TC, Importe = 617.2m }
            };

            var ctx = new DataAccess.SirCoDataContext();
            ctx.UpdateSerieStatus("0000003632625", Status.CA, Status.AC, 0);
            ctx.UpdateSerieStatus("0000003635956", Status.CA, Status.AC, 0);
            ctx.UpdateSerieStatus("0000003668367", Status.CA, Status.AC, 0);

            Common.Entities.SaleResponse folio = null;
            using (ShimsContext.Create())
            {
                var data = new List<DataAccess.SirCoControl.Sucursal> {
                    new DataAccess.SirCoControl.Sucursal { idsucursal = 1, sucursal = "01", cajas = 434045, idplaza = 1,
                        Plaza = new DataAccess.SirCoControl.Plaza {
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

                var now = DateTime.Parse("2019-07-04");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.ToUniversalTime();                
                folio = _process.Sale(model, 0);

                Assert.AreEqual(10 + 99.9m, folio.Monedero);
            }
            Assert.AreEqual("434046", folio.Folio);
            var sale = _sale.FindSale(model.Sucursal, folio.Folio);
            Assert.AreEqual(1199-179.85m, sale.Productos.Where(i => i.Serie == "0000003632625").Single().precdesc);
            Assert.AreEqual(999-49.95m, sale.Productos.Where(i => i.Serie == "0000003635956").Single().precdesc);
            Assert.AreEqual(149, sale.Productos.Where(i => i.Serie == "0000003668367").Single().precdesc);
            Assert.IsNotNull(sale);
        }

        [TestMethod]
        public void PromocionSandalia2x1()
        {
            /*
0000003257708	469.00
0000003258011	469.00
0000003468580	579.00
0000003469475	389.00
0000003469476	389.00
             */
            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                Sucursal = "01",
                PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                    new Common.Entities.PromocionCuponItem { PromocionId = 15 }
                },
                Productos = new Common.Entities.SerieFormasPago[] {
                    new Common.Entities.SerieFormasPago { Serie = "0000003257708", FormasPago = new FormaPago[] { FormaPago.EF } }, //469.00
                    new Common.Entities.SerieFormasPago { Serie = "0000003258011", FormasPago = new FormaPago[] {  } }, //469.00
                }
            };

            var sale = new BusinessLogic.Sale();

            CheckPromocionesCuponesResponse res;
            using (ShimsContext.Create())
            {
                var now = DateTime.Parse("2019-12-30");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.AddHours(5);
                System.Fakes.ShimDateTime.NowGet = () => now;
                System.Fakes.ShimDateTime.TodayGet = () => now;

                res = sale.CheckPromociones(request);
            }
            Assert.IsNotNull(res.Promociones);
            var dic = res.Promociones.ToDictionary(i => i.Serie, i => i);
            Assert.AreEqual(469m, dic["0000003257708"].Descuento ?? 0);//compra
            Assert.AreEqual(0, dic["0000003258011"].Descuento);//promo 100%            
        }

        [TestMethod]
        public void PromocionSandalia2x1_precio()
        {
            /*
0000003257708	469.00
0000003258011	469.00
0000003468580	579.00
0000003469475	389.00
0000003469476	389.00
             */
            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                Sucursal = "01",
                PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                    new Common.Entities.PromocionCuponItem { PromocionId = 15 }
                },
                Productos = new Common.Entities.SerieFormasPago[] {
                    new Common.Entities.SerieFormasPago { Serie = "0000003469476", FormasPago = new FormaPago[] {  } }, //389.00
                    new Common.Entities.SerieFormasPago { Serie = "0000003257708", FormasPago = new FormaPago[] { FormaPago.EF } }, //469.00
                }
            };

            var sale = new BusinessLogic.Sale();

            CheckPromocionesCuponesResponse res;
            using (ShimsContext.Create())
            {
                var now = DateTime.Parse("2019-12-30");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.AddHours(5);
                System.Fakes.ShimDateTime.NowGet = () => now;
                System.Fakes.ShimDateTime.TodayGet = () => now;

                res = sale.CheckPromociones(request);
            }
            Assert.IsNotNull(res.Promociones);
            var dic = res.Promociones.ToDictionary(i => i.Serie, i => i);
            Assert.AreEqual(0, dic["0000003257708"].Descuento ?? 0);//compra
            Assert.AreEqual(389m, dic["0000003469476"].Descuento);//promo 100%            
        }

        [TestMethod]
        public void PromocionSandalia2x1_precio_editado()
        {
            /*
0000003257708	469.00
0000003258011	469.00
0000003468580	579.00
0000003469475	389.00
0000003469476	389.00
             */
            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                Sucursal = "01",
                PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                    new Common.Entities.PromocionCuponItem { PromocionId = 15 }
                },
                Productos = new Common.Entities.SerieFormasPago[] {
                    new Common.Entities.SerieFormasPago { Serie = "0000003469476", Precio = 600, FormasPago = new FormaPago[] {  } }, //389.00
                    new Common.Entities.SerieFormasPago { Serie = "0000003257708", Precio = 300, FormasPago = new FormaPago[] { FormaPago.EF } }, //469.00
                }
            };

            var sale = new BusinessLogic.Sale();

            CheckPromocionesCuponesResponse res;
            using (ShimsContext.Create())
            {
                var now = DateTime.Parse("2019-12-30");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.AddHours(5);
                System.Fakes.ShimDateTime.NowGet = () => now;
                System.Fakes.ShimDateTime.TodayGet = () => now;

                res = sale.CheckPromociones(request);
            }
            Assert.IsNotNull(res.Promociones);
            var dic = res.Promociones.ToDictionary(i => i.Serie, i => i);
            Assert.AreEqual(300m, dic["0000003257708"].Descuento ?? 0);//promo 100%
            Assert.AreEqual(0, dic["0000003469476"].Descuento ?? 0);//compra
        }

        [TestMethod]
        public void Promocion3x1_medio_multiple()
        {
            /*
0000003542508	589.00
0000003542555	659.00
0000003620734	409.00
0000003648807	1599.00
0000003648808	1599.00
0000003696060	1499.00
             */
            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                Sucursal = "08",
                PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                    new Common.Entities.PromocionCuponItem { PromocionId = 180 }
                },
                Productos = new Common.Entities.SerieFormasPago[] {
                    new SerieFormasPago { Serie = "0000003542508", FormasPago = new FormaPago[] { FormaPago.TC } }, //589.00
                    new SerieFormasPago { Serie = "0000003542555", FormasPago = new FormaPago[] { FormaPago.TC } }, //659.00
                    new SerieFormasPago { Serie = "0000003620734", FormasPago = new FormaPago[] { FormaPago.TC } }, //409.00
                    new SerieFormasPago { Serie = "0000003648807", FormasPago = new FormaPago[] { FormaPago.TC } }, //1599.00
                    new SerieFormasPago { Serie = "0000003648808", FormasPago = new FormaPago[] { FormaPago.TC } }, //1599.00
                    new SerieFormasPago { Serie = "0000003696060", FormasPago = new FormaPago[] { FormaPago.TC } }, //1499.00
                }
            };

            var sale = new BusinessLogic.Sale();

            CheckPromocionesCuponesResponse res;
            using (ShimsContext.Create())
            {
                var data = new List<DataAccess.SirCoControl.Sucursal> {
                    new DataAccess.SirCoControl.Sucursal { idsucursal = 8, sucursal = "08", idplaza = 1,
                        Plaza = new DataAccess.SirCoControl.Plaza {
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

                var now = DateTime.Parse("2019-12-30");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.ToUniversalTime();
                
                res = sale.CheckPromociones(request);
            }
            Assert.IsNotNull(res.Promociones);
            var dic = res.Promociones.ToDictionary(i => i.Serie, i => i);
            Assert.AreEqual(589m, dic["0000003542508"].Descuento ?? 0);//promo 100% - 2.2
            Assert.AreEqual(659m / 2, dic["0000003542555"].Descuento ?? 0);//promo 50% - 1.3
            Assert.AreEqual(409m, dic["0000003620734"].Descuento ?? 0);//promo 100% - 1.2
            Assert.AreEqual(0, dic["0000003648807"].Descuento ?? 0);//compra 1.1 - OK
            Assert.AreEqual(0, dic["0000003648808"].Descuento ?? 0);//compra 2.1 - OK
            Assert.AreEqual(1499m / 2, dic["0000003696060"].Descuento ?? 0);//promo 50% - 2.3 - OK
        }
        [TestMethod]
        public void PromocionChaleco1()
        {
            /*
1	NULL	0000003566932	AC	439.00	360	    109	26	A	26	29	611
2	NULL	0000003705435	BA	1099.00	ADD	   1267	28	A	25	30	675
3	NULL	0000003743585	AC	1599.00	ADD	   1329	25-	A	22	27	707
4	NULL	0000003574010	AC	499.00	CRE	      1	CH	A	CH	XX5	691

             */
            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                Sucursal = "08",
                PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                    new Common.Entities.PromocionCuponItem { PromocionId = 120 }
                },
                Productos = new Common.Entities.SerieFormasPago[] {
                    new SerieFormasPago { Serie = "0000003566932", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 439m } } }, //439
                    new SerieFormasPago { Serie = "0000003705435", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 1099m } } }, //1099
                    new SerieFormasPago { Serie = "0000003743585", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 1599m } } }, //1599
                    new SerieFormasPago { Serie = "0000003574010", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 249.5m } } }, //499
                }
            };

            var sale = new BusinessLogic.Sale();

            CheckPromocionesCuponesResponse res;
            using (ShimsContext.Create())
            {
                var data = new List<DataAccess.SirCoControl.Sucursal> {
                    new DataAccess.SirCoControl.Sucursal { idsucursal = 8, sucursal = "08", idplaza = 1,
                        Plaza = new DataAccess.SirCoControl.Plaza {
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

                var now = DateTime.Parse("2019-12-30");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.AddHours(5);
                System.Fakes.ShimDateTime.NowGet = () => now;
                System.Fakes.ShimDateTime.TodayGet = () => now;

                res = sale.CheckPromociones(request);
            }
            Assert.IsNotNull(res.Promociones);
            var dic = res.Promociones.ToDictionary(i => i.Serie, i => i);
            Assert.AreEqual(0, dic["0000003566932"].PromocionId ?? 0);//
            Assert.AreEqual(120, dic["0000003705435"].PromocionId ?? 0);//compra
            Assert.AreEqual(120, dic["0000003743585"].PromocionId ?? 0);//compra
            Assert.AreEqual(499m / 2, dic["0000003574010"].Descuento ?? 0);//promo
        }

        [TestMethod]
        public void PromocionImporteTicket()
        {
            /*
1	NULL	0000003416006	CA	499.00	CRE	      1	GDE	A	CH	XX5	691
2	NULL	0000003574010	CA	499.00	CRE	      1	CH	A	CH	XX5	691
3	NULL	0000003617909	CA	1399.00	NIK	   2002	27	A	25	30	675
4	NULL	0000003753200	CA	1449.00	NIK	   2064	28-	A	25	30	581
5	NULL	0000003755159	CA	1449.00	NIK	   2064	27-	A	25	30	581
             */
            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                Sucursal = "08",
                PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                    new Common.Entities.PromocionCuponItem { PromocionId = 120 }
                },
                Productos = new Common.Entities.SerieFormasPago[] {
                    new SerieFormasPago { Serie = "0000003416006", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 249.5m } } }, //499
                    new SerieFormasPago { Serie = "0000003574010", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 249.5m } } }, //499
                    new SerieFormasPago { Serie = "0000003617909", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 1399m } } }, //1399
                    new SerieFormasPago { Serie = "0000003753200", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 1449m } } }, //1449
                    new SerieFormasPago { Serie = "0000003755159", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 1449m } } }, //1449
                }
            };

            var sale = new BusinessLogic.Sale();

            CheckPromocionesCuponesResponse res;
            using (ShimsContext.Create())
            {
                var data = new List<DataAccess.SirCoControl.Sucursal> {
                    new DataAccess.SirCoControl.Sucursal { idsucursal = 8, sucursal = "08", idplaza = 1,
                        Plaza = new DataAccess.SirCoControl.Plaza {
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

                var now = DateTime.Parse("2019-12-30");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.AddHours(5);
                System.Fakes.ShimDateTime.NowGet = () => now;
                System.Fakes.ShimDateTime.TodayGet = () => now;

                res = sale.CheckPromociones(request);
            }
            Assert.IsNotNull(res.Promociones);
            var dic = res.Promociones.ToDictionary(i => i.Serie, i => i);
            Assert.AreEqual(249.5m, dic["0000003416006"].Descuento ?? 0);//promo 50%
            Assert.AreEqual(249.5m, dic["0000003574010"].Descuento ?? 0);//promo 50%
            Assert.AreEqual(120, dic["0000003617909"].PromocionId ?? 0);//compra
            Assert.AreEqual(120, dic["0000003753200"].PromocionId ?? 0);//compra
            Assert.AreEqual(120, dic["0000003755159"].PromocionId ?? 0);//compra
        }

        [TestMethod]
        public void PromocionImporteTicketSobrante()
        {
            /*
1	NULL	0000003416006	CA	499.00	CRE	      1	GDE	A	CH	XX5	691
2	NULL	0000003574010	CA	499.00	CRE	      1	CH	A	CH	XX5	691
3	NULL	0000003617909	CA	1399.00	NIK	   2002	27	A	25	30	675
4	NULL	0000003753200	CA	1449.00	NIK	   2064	28-	A	25	30	581
5	NULL	0000003755159	CA	1449.00	NIK	   2064	27-	A	25	30	581
1	NULL	0000003659360	AC	1059.00	ADA	      2	28	A	25	30	711
             */
            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                Sucursal = "08",
                PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                    new Common.Entities.PromocionCuponItem { PromocionId = 120 }
                },
                Productos = new Common.Entities.SerieFormasPago[] {
                    new SerieFormasPago { Serie = "0000003416006", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 249.5m } } }, //499
                    new SerieFormasPago { Serie = "0000003574010", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 249.5m } } }, //499
                    new SerieFormasPago { Serie = "0000003617909", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 1399m } } }, //1399
                    new SerieFormasPago { Serie = "0000003753200", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 1449m } } }, //1449
                    new SerieFormasPago { Serie = "0000003755159", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 1449m } } }, //1449
                    new SerieFormasPago { Serie = "0000003659360", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 1059m } } }, //1059
                }
            };

            var sale = new BusinessLogic.Sale();

            CheckPromocionesCuponesResponse res;
            using (ShimsContext.Create())
            {
                var data = new List<DataAccess.SirCoControl.Sucursal> {
                    new DataAccess.SirCoControl.Sucursal { idsucursal = 8, sucursal = "08", idplaza = 1,
                        Plaza = new DataAccess.SirCoControl.Plaza {
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

                var now = DateTime.Parse("2019-12-30");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.AddHours(5);
                System.Fakes.ShimDateTime.NowGet = () => now;
                System.Fakes.ShimDateTime.TodayGet = () => now;

                res = sale.CheckPromociones(request);
            }
            Assert.IsNotNull(res.Promociones);
            var dic = res.Promociones.ToDictionary(i => i.Serie, i => i);
            Assert.AreEqual(249.5m, dic["0000003416006"].Descuento ?? 0);//promo 50%
            Assert.AreEqual(249.5m, dic["0000003574010"].Descuento ?? 0);//promo 50%
            Assert.AreEqual(120, dic["0000003617909"].PromocionId ?? 0);//compra
            Assert.AreEqual(120, dic["0000003753200"].PromocionId ?? 0);//compra
            Assert.AreEqual(120, dic["0000003755159"].PromocionId ?? 0);//compra
            Assert.AreEqual(0, dic["0000003659360"].PromocionId ?? 0);//no usado
        }

        [TestMethod]
        public void PromocionDirectaMinCompra()
        {
            /*
1	NULL	0000003564979	AC	439.00	360	    108	29	A	26	29	611
2	NULL	0000003806434	CA	699.00	ADD	   1241	25	A	25	30	675
3	NULL	0000003806438	CA	699.00	ADD	   1241	25	A	25	30	675
4	NULL	0000003759589	CA	1099.00	ADD	   1197	26-	A	25	30	707
5	NULL	0000003806590	CA	1099.00	ADD	   1197	27-	A	25	30	675
6	NULL	0000003762985	CA	1099.00	ADD	   1197	26	A	25	30	707
             */
            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                Sucursal = "08",
                PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                    new Common.Entities.PromocionCuponItem { PromocionId = 140 }
                },
                Productos = new Common.Entities.SerieFormasPago[] {
                    new SerieFormasPago { Serie = "0000003564979", FormasPago = new FormaPago[] { FormaPago.EF } }, //439.00	     
                    new SerieFormasPago { Serie = "0000003806434", FormasPago = new FormaPago[] { FormaPago.EF } }, //699.00	
                    new SerieFormasPago { Serie = "0000003806438", FormasPago = new FormaPago[] { FormaPago.EF } }, //699.00	
                    new SerieFormasPago { Serie = "0000003759589", FormasPago = new FormaPago[] { FormaPago.EF } }, //1099.00
                    new SerieFormasPago { Serie = "0000003806590", FormasPago = new FormaPago[] { FormaPago.EF } }, //1099.00
                    new SerieFormasPago { Serie = "0000003762985", FormasPago = new FormaPago[] { FormaPago.EF } }, //1099.00
                }
            };

            var sale = new BusinessLogic.Sale();

            CheckPromocionesCuponesResponse res;
            using (ShimsContext.Create())
            {
                var data = new List<DataAccess.SirCoControl.Sucursal> {
                    new DataAccess.SirCoControl.Sucursal { idsucursal = 8, sucursal = "08", idplaza = 1,
                        Plaza = new DataAccess.SirCoControl.Plaza {
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

                var now = DateTime.Parse("2019-12-30");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.AddHours(5);
                System.Fakes.ShimDateTime.NowGet = () => now;
                System.Fakes.ShimDateTime.TodayGet = () => now;

                res = sale.CheckPromociones(request);
            }
            Assert.IsNotNull(res.Promociones);
            var dic = res.Promociones.ToDictionary(i => i.Serie, i => i);
            Assert.AreEqual(0, dic["0000003564979"].PromocionId ?? 0);//
            Assert.AreEqual(140, dic["0000003806434"].PromocionId ?? 0);//promo
            Assert.AreEqual(140, dic["0000003806438"].PromocionId ?? 0);//promo
            Assert.AreEqual(140, dic["0000003759589"].PromocionId ?? 0);//promo
            Assert.AreEqual(140, dic["0000003806590"].PromocionId ?? 0);//promo
            Assert.AreEqual(0, dic["0000003762985"].PromocionId ?? 0);//
        }

        [TestMethod]
        public void ImporteUnidades3x2enCALZADO()
        {
            /*ID=24 => 3x2 en CALZADO => min 2800
1	NULL	0000003776380	AC	629.00	CTA	     83	22	E	21-	27	324
2	NULL	0000003821104	AC	849.00	FFF	   2037	28-	A	25	31	048
3	NULL	0000003791442	AC	1099.00	CVR	      5	25	A	23	30	732
4	NULL	0000003676237	AC	1399.00	NIK	   1695	27-	A	25	30	675
5	NULL	0000003702119	AC	1129.00	DCK	     43	30	A	25	30	010
6	NULL	0000003811664	AC	1399.00	RBK	    258	23-	A	22	27	675
7	NULL	0000003692446	AC	1399.00	NIK	   1695	30	A	25	30	581
             */

            var ctxpv = new DataAccess.SirCoPVDataContext();
            var item = ctxpv.Promociones.Where(i => i.idpromocion == 24).Single();
            item.clienterequerido = false;
            ctxpv.SaveChanges();

            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                Sucursal = "08",
                PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                    new Common.Entities.PromocionCuponItem { PromocionId = 24 }
                },
                Productos = new Common.Entities.SerieFormasPago[] {
                    new SerieFormasPago { Serie = "0000003776380", FormasPago = new FormaPago[] { } }, //629.00
                    new SerieFormasPago { Serie = "0000003821104", FormasPago = new FormaPago[] { } }, //849.00
                    new SerieFormasPago { Serie = "0000003791442", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 1099m } }  }, //1099.00
                    new SerieFormasPago { Serie = "0000003676237", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 1399m } }  }, //1399.00
                    new SerieFormasPago { Serie = "0000003702119", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 1129m } }  }, //1129.00
                    new SerieFormasPago { Serie = "0000003811664", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 1399m } }  }, //1399.00
                    new SerieFormasPago { Serie = "0000003692446", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 1399m } }  }, //1399.00
                    //5,734
                }
            };

            var sale = new BusinessLogic.Sale();

            CheckPromocionesCuponesResponse res;
            using (ShimsContext.Create())
            {
                var data = new List<DataAccess.SirCoControl.Sucursal> {
                    new DataAccess.SirCoControl.Sucursal { idsucursal = 8, sucursal = "08", idplaza = 1,
                        Plaza = new DataAccess.SirCoControl.Plaza {
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

                var now = DateTime.Parse("2020-08-25");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.ToUniversalTime();                

                res = sale.CheckPromociones(request);
            }
            Assert.IsNotNull(res.Promociones);
            var dic = res.Promociones.ToDictionary(i => i.Serie, i => i);
            Assert.AreEqual(629,   dic["0000003776380"].Descuento ?? 0);//promo
            Assert.AreEqual(849, dic["0000003821104"].Descuento ?? 0);//promo

            Assert.AreEqual(0, dic["0000003791442"].Descuento ?? 0);//
            Assert.AreEqual(0, dic["0000003676237"].Descuento ?? 0);//compra
            Assert.AreEqual(0, dic["0000003702119"].Descuento ?? 0);//compra
            Assert.AreEqual(0, dic["0000003811664"].Descuento ?? 0);//compra
            Assert.AreEqual(0, dic["0000003692446"].Descuento ?? 0);//compra

            Assert.AreEqual(0, dic["0000003791442"].PromocionId ?? 0);//
            Assert.AreEqual(24, dic["0000003676237"].PromocionId ?? 0);//compra
            Assert.AreEqual(24, dic["0000003702119"].PromocionId ?? 0);//compra
            Assert.AreEqual(24, dic["0000003811664"].PromocionId ?? 0);//compra
            Assert.AreEqual(24, dic["0000003692446"].PromocionId ?? 0);//compra            
        }

        [TestMethod]
        public void PromoCompraIgualImporteTicketInvalido()
        {
            /*ID=28	CHALECO $99 x $1800 CALZADO, min compra 2000
1	NULL	0000003123795	AC	629.00	CTA	     83	25-	E	21-	27	324
2	NULL	0000003144574	AC	629.00	CTA	     83	24	E	21-	27	324
3	NULL	0000003333626	AC	629.00	CTA	     83	23-	E	21-	27	324
4	NULL	0000003333634	AC	629.00	CTA	     83	23-	E	21-	27	324
            */

            var ctxpv = new DataAccess.SirCoPVDataContext();
            var item = ctxpv.Promociones.Where(i => i.idpromocion == 24).Single();
            item.clienterequerido = false;
            ctxpv.SaveChanges();

            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                Sucursal = "08",
                PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                    new Common.Entities.PromocionCuponItem { PromocionId = 28 }
                },
                Productos = new Common.Entities.SerieFormasPago[] {
                    new SerieFormasPago { Serie = "0000003123795", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 629m } } }, //629.00
                    new SerieFormasPago { Serie = "0000003144574", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 629m } } }, //629.00
                    new SerieFormasPago { Serie = "0000003333626", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 629m } } }, //629.00
                    new SerieFormasPago { Serie = "0000003333634", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 629m } } }, //629.00
                    //2,516
                }
            };

            var sale = new BusinessLogic.Sale();

            CheckPromocionesCuponesResponse res;
            using (ShimsContext.Create())
            {
                var data = new List<DataAccess.SirCoControl.Sucursal> {
                    new DataAccess.SirCoControl.Sucursal { idsucursal = 8, sucursal = "08", idplaza = 1,
                        Plaza = new DataAccess.SirCoControl.Plaza {
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

                var now = DateTime.Parse("2020-08-25");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.ToUniversalTime();

                res = sale.CheckPromociones(request);
            }
            Assert.IsNotNull(res.Promociones);
            var dic = res.Promociones.ToDictionary(i => i.Serie, i => i);
            Assert.AreEqual(0, dic["0000003123795"].Descuento ?? 0);//compra
            Assert.AreEqual(0, dic["0000003144574"].Descuento ?? 0);//compra            
            Assert.AreEqual(0, dic["0000003333626"].Descuento ?? 0);//compra
            Assert.AreEqual(0, dic["0000003333634"].Descuento ?? 0);//compra            
        }
        [TestMethod]
        public void PromoCompraIgualImporteTicketValido()
        {
            /*ID=28	CHALECO $99 x $1800 CALZADO, min compra 2000
1	NULL	0000003123795	AC	629.00	CTA	     83	25-	E	21-	27	324
2	NULL	0000003144574	AC	629.00	CTA	     83	24	E	21-	27	324
3	NULL	0000003333626	AC	629.00	CTA	     83	23-	E	21-	27	324
4	NULL	0000003333634	AC	629.00	CTA	     83	23-	E	21-	27	324
5	NULL	0000003333660	AC	629.00	CTA	     83	26	E	21-	27	324
            */

            var ctxpv = new DataAccess.SirCoPVDataContext();
            var item = ctxpv.Promociones.Where(i => i.idpromocion == 24).Single();
            item.clienterequerido = false;
            ctxpv.SaveChanges();

            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                Sucursal = "08",
                PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                    new Common.Entities.PromocionCuponItem { PromocionId = 28 }
                },
                Productos = new Common.Entities.SerieFormasPago[] {
                    new SerieFormasPago { Serie = "0000003123795", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 629m } } }, //629.00
                    new SerieFormasPago { Serie = "0000003144574", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 629m } } }, //629.00
                    new SerieFormasPago { Serie = "0000003333626", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 629m } } }, //629.00
                    new SerieFormasPago { Serie = "0000003333634", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 629m } } }, //629.00
                    new SerieFormasPago { Serie = "0000003333660", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 629m } } }, //629.00
                    //3,145
                }
            };

            var sale = new BusinessLogic.Sale();

            CheckPromocionesCuponesResponse res;
            using (ShimsContext.Create())
            {
                var data = new List<DataAccess.SirCoControl.Sucursal> {
                    new DataAccess.SirCoControl.Sucursal { idsucursal = 8, sucursal = "08", idplaza = 1,
                        Plaza = new DataAccess.SirCoControl.Plaza {
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

                var now = DateTime.Parse("2020-08-25");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.ToUniversalTime();

                res = sale.CheckPromociones(request);
            }
            Assert.IsNotNull(res.Promociones);
            var dic = res.Promociones.ToDictionary(i => i.Serie, i => i);
            Assert.AreEqual(629m, dic["0000003123795"].Descuento ?? 0);//promo
            Assert.AreEqual(0, dic["0000003144574"].Descuento ?? 0);//compra            
            Assert.AreEqual(0, dic["0000003333626"].Descuento ?? 0);//compra
            Assert.AreEqual(0, dic["0000003333634"].Descuento ?? 0);//compra
            Assert.AreEqual(0, dic["0000003333660"].Descuento ?? 0);//compra
        }

        [TestMethod]
        public void PromoCompraIgualImporteTicketDoble()
        {
            /*ID=28	CHALECO $99 x $1800 CALZADO, min compra 2000
1	NULL	0000003123795	AC	629.00	CTA	     83	25-	E	21-	27	324
2	NULL	0000003144574	AC	629.00	CTA	     83	24	E	21-	27	324
3	NULL	0000003333626	AC	629.00	CTA	     83	23-	E	21-	27	324
4	NULL	0000003333634	AC	629.00	CTA	     83	23-	E	21-	27	324

1	NULL	0000003333660	AC	629.00	CTA	     83	26	E	21-	27	324
2	NULL	0000003776386	AC	629.00	CTA	     83	23	E	21-	27	324
3	NULL	0000003776392	AC	629.00	CTA	     83	26-	E	21-	27	324     
            */

            var ctxpv = new DataAccess.SirCoPVDataContext();
            var item = ctxpv.Promociones.Where(i => i.idpromocion == 24).Single();
            item.clienterequerido = false;
            ctxpv.SaveChanges();

            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                Sucursal = "08",
                PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                    new Common.Entities.PromocionCuponItem { PromocionId = 28 }
                },
                Productos = new Common.Entities.SerieFormasPago[] {
                    new SerieFormasPago { Serie = "0000003123795", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 629m } } }, //629.00
                    new SerieFormasPago { Serie = "0000003144574", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 629m } } }, //629.00
                    new SerieFormasPago { Serie = "0000003333626", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 629m } } }, //629.00
                    new SerieFormasPago { Serie = "0000003333634", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 629m } } }, //629.00

                    new SerieFormasPago { Serie = "0000003333660", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 629m } } }, //629.00
                    new SerieFormasPago { Serie = "0000003776386", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 629m } } }, //629.00
                    new SerieFormasPago { Serie = "0000003776392", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 629m } } }, //629.00
                    //4,403
                }
            };

            var sale = new BusinessLogic.Sale();

            CheckPromocionesCuponesResponse res;
            using (ShimsContext.Create())
            {
                var data = new List<DataAccess.SirCoControl.Sucursal> {
                    new DataAccess.SirCoControl.Sucursal { idsucursal = 8, sucursal = "08", idplaza = 1,
                        Plaza = new DataAccess.SirCoControl.Plaza {
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

                var now = DateTime.Parse("2020-08-25");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.ToUniversalTime();

                res = sale.CheckPromociones(request);
            }
            Assert.IsNotNull(res.Promociones);
            var dic = res.Promociones.ToDictionary(i => i.Serie, i => i);
            Assert.AreEqual(629, dic["0000003123795"].Descuento ?? 0);//promo

            Assert.AreEqual(0, dic["0000003144574"].PromocionId ?? 0);
            Assert.AreEqual(28, dic["0000003333626"].PromocionId ?? 0);//compra
            Assert.AreEqual(28, dic["0000003333634"].PromocionId ?? 0);//compra
            Assert.AreEqual(28, dic["0000003333660"].PromocionId ?? 0);//compra
            Assert.AreEqual(28, dic["0000003776386"].PromocionId ?? 0);//compra
            Assert.AreEqual(0, dic["0000003776392"].PromocionId ?? 0);//

            Assert.AreEqual(0, dic["0000003144574"].PromocionId ?? 0);
            Assert.AreEqual(0, dic["0000003333626"].Descuento ?? 0);//compra
            Assert.AreEqual(0, dic["0000003333634"].Descuento ?? 0);//compra
            Assert.AreEqual(0, dic["0000003333660"].Descuento ?? 0);//compra
            Assert.AreEqual(0, dic["0000003776386"].Descuento ?? 0);//compra
            Assert.AreEqual(0, dic["0000003776392"].PromocionId ?? 0);//
        }
        [TestMethod]
        public void directaMinImporteTicket()
        {
            /* ID=114	20% DSCTO CALZADO CABALLERO -> min 3,500, imp ticket
            1	NULL	0000003795973	AC	849.00	FFF	   2037	27-	A	25	31	048
            2	NULL	0000003821099	AC	849.00	FFF	   2037	27	A	25	31	048
            3	NULL	0000003821100	AC	849.00	FFF	   2037	27	A	25	31	048
            4	NULL	0000003821101	AC	849.00	FFF	   2037	27	A	25	31	048
            5	NULL	0000003821102	AC	849.00	FFF	   2037	27-	A	25	31	048
            6	NULL	0000003821103	AC	849.00	FFF	   2037	28	A	25	31	048
            */

            var ctxpv = new DataAccess.SirCoPVDataContext();
            var item = ctxpv.Promociones.Where(i => i.idpromocion == 114).Single();
            foreach (var ex in item.Exclusiones.ToArray())
            {
                ctxpv.PromocionesExclusiones.Remove(ex);
            }
            ctxpv.SaveChanges();

            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                Sucursal = "08",
                PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                    new Common.Entities.PromocionCuponItem { PromocionId = 114 }
                },
                Productos = new Common.Entities.SerieFormasPago[] {
                    new SerieFormasPago { Serie = "0000003795973", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 849m } } }, //849.00
                    new SerieFormasPago { Serie = "0000003821099", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 849m } } }, //849.00
                    new SerieFormasPago { Serie = "0000003821100", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 849m } } }, //849.00
                    new SerieFormasPago { Serie = "0000003821101", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 849m } } }, //849.00
                    new SerieFormasPago { Serie = "0000003821102", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 849m } } }, //849.00
                    new SerieFormasPago { Serie = "0000003821103", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 849m } } }, //849.00
                    //5,094
                }
            };

            var sale = new BusinessLogic.Sale();

            CheckPromocionesCuponesResponse res;
            using (ShimsContext.Create())
            {
                var data = new List<DataAccess.SirCoControl.Sucursal> {
                    new DataAccess.SirCoControl.Sucursal { idsucursal = 8, sucursal = "08", idplaza = 1,
                        Plaza = new DataAccess.SirCoControl.Plaza {
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

                var now = DateTime.Parse("2020-08-25");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.ToUniversalTime();

                res = sale.CheckPromociones(request);
            }
            Assert.IsNotNull(res.Promociones);
            var dic = res.Promociones.ToDictionary(i => i.Serie, i => i);
            Assert.AreEqual(169.8m, dic["0000003795973"].Descuento ?? 0);//promo
            Assert.AreEqual(169.8m, dic["0000003821099"].Descuento ?? 0);//promo
            Assert.AreEqual(169.8m, dic["0000003821100"].Descuento ?? 0);//promo
            Assert.AreEqual(169.8m, dic["0000003821101"].Descuento ?? 0);//promo
            Assert.AreEqual(169.8m, dic["0000003821102"].Descuento ?? 0);//promo
            Assert.AreEqual(169.8m, dic["0000003821103"].Descuento ?? 0);//promo
        }

        [TestMethod]
        public void directaMinImporteTicketExactoValida()
        {
            /* ID=114	20% DSCTO CALZADO CABALLERO -> min 3,500, imp ticket
1	NULL	0000003675656	AC	1399.00	NIK	   1695	28	A	25	30	675
2	NULL	0000003692434	AC	1399.00	NIK	   1695	28	A	25	30	581
3	NULL	0000003701174	AC	879.00	GCH	    209	29-	A	25	30	127
            */

            var ctxpv = new DataAccess.SirCoPVDataContext();
            var item = ctxpv.Promociones.Where(i => i.idpromocion == 114).Single();
            item.importeticket = true;
            ctxpv.SaveChanges();

            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                Sucursal = "08",
                PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                    new Common.Entities.PromocionCuponItem { PromocionId = 114 }
                },
                Productos = new Common.Entities.SerieFormasPago[] {
                    new SerieFormasPago { Serie = "0000003675656", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 1399m } } }, //1399.00
                    new SerieFormasPago { Serie = "0000003692434", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 1399m } } }, //1399.00
                    new SerieFormasPago { Serie = "0000003701174", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 879m } } }, //879.00                    
                    //3,677 -> 2,941.6
                }
            };

            var sale = new BusinessLogic.Sale();

            CheckPromocionesCuponesResponse res;
            using (ShimsContext.Create())
            {
                var data = new List<DataAccess.SirCoControl.Sucursal> {
                    new DataAccess.SirCoControl.Sucursal { idsucursal = 8, sucursal = "08", idplaza = 1,
                        Plaza = new DataAccess.SirCoControl.Plaza {
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

                var now = DateTime.Parse("2020-08-25");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.ToUniversalTime();

                res = sale.CheckPromociones(request);
            }
            Assert.IsNotNull(res.Promociones);
            var dic = res.Promociones.ToDictionary(i => i.Serie, i => i);
            Assert.AreEqual(279.8m, dic["0000003675656"].Descuento ?? 0);//promo
            Assert.AreEqual(279.8m, dic["0000003692434"].Descuento ?? 0);//promo
            Assert.AreEqual(175.8m, dic["0000003701174"].Descuento ?? 0);//promo

            //Assert.AreEqual(0, dic["0000003675656"].Descuento ?? 0);//promo
            //Assert.AreEqual(0, dic["0000003692434"].Descuento ?? 0);//promo
            //Assert.AreEqual(0, dic["0000003701174"].Descuento ?? 0);//promo            
        }
        [TestMethod]
        public void directaMinImporteTicketExactoUpdate()
        {
            /* ID=114	20% DSCTO CALZADO CABALLERO -> min 3,500, imp ticket
1	NULL	0000003675656	AC	1399.00	NIK	   1695	28	A	25	30	675
2	NULL	0000003692434	AC	1399.00	NIK	   1695	28	A	25	30	581
3	NULL	0000003701174	AC	879.00	GCH	    209	29-	A	25	30	127
            */

            var ctxpv = new DataAccess.SirCoPVDataContext();
            var item = ctxpv.Promociones.Where(i => i.idpromocion == 114).Single();
            item.importeticket = false;            
            ctxpv.SaveChanges();

            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                Sucursal = "08",
                PromocionesCupones = new Common.Entities.PromocionCuponItem[] {
                    new Common.Entities.PromocionCuponItem { PromocionId = 114 }
                },
                Productos = new Common.Entities.SerieFormasPago[] {
                    new SerieFormasPago { Serie = "0000003675656", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 1119.2m } } }, //1399.00
                    new SerieFormasPago { Serie = "0000003692434", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 1119.2m } } }, //1399.00
                    new SerieFormasPago { Serie = "0000003701174", FormasPago = new FormaPago[] { FormaPago.EF }, Pagos = new Dictionary<FormaPago, decimal?>() { { FormaPago.EF, 703.2m } } }, //879.00                    
                    //3,677 -> 2,941.6
                }
            };

            var sale = new BusinessLogic.Sale();

            CheckPromocionesCuponesResponse res;
            using (ShimsContext.Create())
            {
                var data = new List<DataAccess.SirCoControl.Sucursal> {
                    new DataAccess.SirCoControl.Sucursal { idsucursal = 8, sucursal = "08", idplaza = 1,
                        Plaza = new DataAccess.SirCoControl.Plaza {
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

                var now = DateTime.Parse("2020-08-25");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.ToUniversalTime();

                res = sale.CheckPromociones(request);
            }
            Assert.IsNotNull(res.Promociones);
            var dic = res.Promociones.ToDictionary(i => i.Serie, i => i);
            Assert.AreEqual(279.8m, dic["0000003675656"].Descuento ?? 0);//promo
            Assert.AreEqual(279.8m, dic["0000003692434"].Descuento ?? 0);//promo
            Assert.AreEqual(175.8m, dic["0000003701174"].Descuento ?? 0);//promo            
        }
    }
}
