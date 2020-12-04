using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.QualityTools.Testing.Fakes;
using System.Threading;
using System.Linq;
using Pose;
using System.Collections.Generic;
using Moq;
using System.Data.Entity;
using SirCoPOS.Common.Constants;

namespace SirCoPOS.Tests
{
    [TestClass]
    public class CancelUnitTest
    {
        public TestContext TestContext { get; set; }
        public CancelUnitTest()
        {
            _context = new BusinessLogic.Process();
            _sale = new BusinessLogic.Sale();
        }

        private BusinessLogic.Process _context;
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
        //[ExpectedException(typeof(NotSupportedException))]
        public void CancelSaleTest()
        {
            var request = new Common.Entities.CancelSaleRequest
            {
                Sucursal = "01",
                Folio = "414628"
            };

            var now = DateTime.Parse("2018-06-16");
            Shim shim = Shim.Replace(() => DateTime.UtcNow).With(() => now.ToUniversalTime());

            using (ShimsContext.Create())
            //PoseContext.Isolate(() =>
            {
                System.Fakes.ShimDateTime.UtcNowGet = () => now.ToUniversalTime();
                _context.CancelSale(request, 0);
            }
            //, shim);
        }

        //[TestMethod]
        //public void CancelReturnTest()
        //{
        //    _context.CancelReturn()
        //}
        
        [TestMethod]
        public void CancelFormaPago_CP()
        {
            var sucursal = "01";
            //var sucursalId = 1;
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => { return DateTime.Parse("2019-09-01"); };

                var data = new List<DataAccess.SirCoControl.Sucursal> {
                    new DataAccess.SirCoControl.Sucursal { idsucursal = 1, sucursal = "01", cajas = 434045 }
                }.AsQueryable();

                var mockSet = new Mock<DbSet<DataAccess.SirCoControl.Sucursal>>();
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

                DataAccess.Fakes.ShimSirCoControlDataContext.AllInstances.SucursalesGet = (sctx) => {
                    return mockSet.Object;
                };

                var pserie = "0000003668367";
                var distrib = "005961";
                var prod = _sale.ScanProducto(pserie, sucursal);
                Assert.IsNotNull(prod);

                var ctx = new DataAccess.SirCoDataContext();
                ctx.UpdateSerieStatus("0000003668367", Status.CA, Status.AC, 0);

                var dis = _sale.FindTarjetahabiente(distrib);

                var ncliente = new Common.Entities.Cliente
                {
                    DistribuidorId = 5730
                };

                //var cid = _process.AddCliente(ncliente);
                //Assert.IsNotNull(cid);

                var model = new Common.Entities.SaleRequest()
                {
                    Sucursal = sucursal,
                    VendedorId = 0,
                    Cliente = ncliente
                };
                model.Productos = new Common.Entities.SerieFormasPago[] {
                    new Common.Entities.SerieFormasPago { Serie = pserie, FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.VA } }
                };
                model.Pagos = new List<Common.Entities.Pago> {
                    new Common.Entities.Pago {
                        FormaPago = FormaPago.CP,
                        Importe = prod.Producto.Precio.Value,
                        Distribuidor = distrib,
                        Plazos = 3
                    }
                };

                var ctxc = new DataAccess.SirCoCreditoDataContext();
                var ds = ctxc.Distribuidores.Where(i => i.distrib == distrib).Single();
                Assert.AreEqual(11564m, ds.disponible);

                var folio = _context.Sale(model, 0);

                Assert.AreEqual("434046", folio.Folio);

                ctxc.Entry(ds).Reload();
                Assert.AreEqual(11564m - prod.Producto.Precio.Value, ds.disponible);

                var crequest = new Common.Entities.CancelSaleRequest
                {
                    Sucursal = sucursal,
                    Folio = "434046"
                };
                _context.CancelSale(crequest, 0);

                ctxc.Entry(ds).Reload();
                Assert.AreEqual(11564m, ds.disponible);
            }
        }
        [TestMethod]
        public void CancelFormaPago_CV()
        {
            var sucursal = "01";
            using (ShimsContext.Create())
            {
                var now = DateTime.Parse("2019-09-01");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.ToUniversalTime();
                System.Fakes.ShimDateTime.NowGet = () => now;

                var data = new List<DataAccess.SirCoControl.Sucursal> {
                    new DataAccess.SirCoControl.Sucursal { idsucursal = 1, sucursal = "01", cajas = 434045 }
                }.AsQueryable();

                var mockSet = new Mock<DbSet<DataAccess.SirCoControl.Sucursal>>();
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

                DataAccess.Fakes.ShimSirCoControlDataContext.AllInstances.SucursalesGet = (sctx) => {
                    return mockSet.Object;
                };

                var pserie = "0000003668367";
                var nvale = "0000001234";

                var ctxc = new DataAccess.SirCoCreditoDataContext();
                var cvale = ctxc.ContraVales.Where(i => i.cvale == nvale && i.sucursal == "01").Single();
                Assert.AreEqual(1000, cvale.saldo);

                var prod = _sale.ScanProducto(pserie, sucursal);
                Assert.IsNotNull(prod);
                //var vale = _context.CheckVale(nvale);

                var ctx = new DataAccess.SirCoDataContext();
                ctx.UpdateSerieStatus("0000003668367", Status.CA, Status.AC, 0);

                var ncliente = new Common.Entities.Cliente
                {
                    Id = 810374
                };

                //var cid = _process.AddCliente(ncliente);
                //Assert.IsNotNull(cid);

                var model = new Common.Entities.SaleRequest()
                {
                    Sucursal = sucursal,
                    VendedorId = 0,
                    Cliente = ncliente
                };
                model.Productos = new Common.Entities.SerieFormasPago[] {
                    new Common.Entities.SerieFormasPago { Serie = pserie, FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.VA } }
                };
                model.Pagos = new List<Common.Entities.Pago> {
                    new Common.Entities.Pago {
                        FormaPago = FormaPago.CV,
                        Importe = prod.Producto.Precio.Value,
                        Sucursal = "01",
                        Vale = nvale,
                        Plazos = 13,
                        FechaAplicar = DateTime.Parse("2019-11-05")
                        //ClienteId = cid,                        
                    }
                };

                var folio = _context.Sale(model, 0);

                Assert.AreEqual("434046", folio.Folio);

                ctxc.Entry(cvale).Reload();
                Assert.AreEqual(1000 - prod.Producto.Precio.Value, cvale.saldo);

                var crequest = new Common.Entities.CancelSaleRequest
                {
                    Sucursal = sucursal,
                    Folio = "434046"
                };
                _context.CancelSale(crequest, 0);

                ctxc.Entry(cvale).Reload();
                Assert.AreEqual(1000, cvale.saldo);
            }
        }
        [TestMethod]
        public void CancelFormaPago_DV()
        {
            /*
            1	NULL	0000003678429	AC	539.00	CTA	    682	16-	A	15	17	324
            2	NULL	0000003522471	AC	539.00	CTA	    682	15	A	15	17	324
            3	NULL	0000003776476	AC	589.00	CTA	    682	19-	B	17-	21	324
            4	NULL	0000003776472	AC	589.00	CTA	    682	18-	B	17-	21	324
            5	NULL	0000003678238	AC	649.00	CTA	    682	24	C	21-	26	324
            6	NULL	0000003676610	AC	649.00	CTA	    682	25	C	21-	26	324
            */

            var model = new Common.Entities.SaleRequest()
            {
                Sucursal = "01",
                VendedorId = 132
            };
            model.Productos = new Common.Entities.SerieFormasPago[] {
                new Common.Entities.SerieFormasPago { Serie = "0000003678429", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF }}
            };
            model.Pagos = new Common.Entities.Pago[] {
                new Common.Entities.Pago { FormaPago = Common.Constants.FormaPago.EF, Importe = 539 }
            };

            _context.RequestProducto("0000003678429", 0);

            Common.Entities.SaleResponse folio = null;
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => { return DateTime.Parse("2019-03-06"); };
                folio = _context.Sale(model, 0);
            }

            Assert.AreEqual("434054", folio.Folio);

            var crequest = new Common.Entities.ChangeRequest
            {
                Sucursal = "01",
                Folio = "434054",
                Items = new Common.Entities.ChangeItem[]
                {
                    new Common.Entities.ChangeItem { OldItem = "0000003678429", NewItem = "0000003522471" }
                }
            };
            _context.RequestProducto("0000003522471", 0);
            Common.Entities.ChangeResponse cres = null;
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => { return DateTime.Parse("2019-03-07"); };
                cres = _context.Change(crequest, 0, "08");
            }

            Assert.AreEqual("295595", cres.Venta);
            Assert.AreEqual("023287", cres.Devolucion);

            var ctx = new DataAccess.SirCoPVDataContext();
            var dev = ctx.Devoluciones.Where(i => i.sucursal == "08" && i.devolvta == "023287").Single();
            Assert.AreEqual(0, dev.disponible);

            var cancel = new Common.Entities.CancelSaleRequest
            {
                Sucursal = "08",
                Folio = "295595"
            };
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => { return DateTime.Parse("2019-03-07"); };
                _context.CancelSale(cancel, 0);
            }

            ctx.Entry(dev).Reload();
            Assert.AreEqual(539, dev.disponible);
        }
        [TestMethod]
        public void CancelFormaPago_DV_CancelDV()
        {
            /*
            1	NULL	0000003678429	AC	539.00	CTA	    682	16-	A	15	17	324
            2	NULL	0000003522471	AC	539.00	CTA	    682	15	A	15	17	324
            3	NULL	0000003776476	AC	589.00	CTA	    682	19-	B	17-	21	324
            4	NULL	0000003776472	AC	589.00	CTA	    682	18-	B	17-	21	324
            5	NULL	0000003678238	AC	649.00	CTA	    682	24	C	21-	26	324
            6	NULL	0000003676610	AC	649.00	CTA	    682	25	C	21-	26	324
            */

            var model = new Common.Entities.SaleRequest()
            {
                Sucursal = "01",
                VendedorId = 132
            };
            model.Productos = new Common.Entities.SerieFormasPago[] {
                new Common.Entities.SerieFormasPago { Serie = "0000003678429", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF }}
            };
            model.Pagos = new Common.Entities.Pago[] {
                new Common.Entities.Pago { FormaPago = Common.Constants.FormaPago.EF, Importe = 539 }
            };

            _context.RequestProducto("0000003678429", 0);

            var sctx = new DataAccess.SirCoDataContext();
            var oserie = sctx.Series.Where(i => i.serie == "0000003678429").Single();
            Assert.AreEqual("CA", oserie.status);

            Common.Entities.SaleResponse folio = null;
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => { return DateTime.Parse("2019-03-06"); };
                folio = _context.Sale(model, 0);
            }

            Assert.AreEqual("434054", folio.Folio);

            sctx.Entry(oserie).Reload();
            Assert.AreEqual("BA", oserie.status);

            var crequest = new Common.Entities.ChangeRequest
            {
                Sucursal = "01",
                Folio = "434054",
                Items = new Common.Entities.ChangeItem[]
                {
                    new Common.Entities.ChangeItem { OldItem = "0000003678429", NewItem = "0000003522471" }
                }
            };
            _context.RequestProducto("0000003522471", 0);
            Common.Entities.ChangeResponse cres = null;
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => { return DateTime.Parse("2019-03-07"); };
                cres = _context.Change(crequest, 0, "08");
            }

            Assert.AreEqual("295595", cres.Venta);
            Assert.AreEqual("023287", cres.Devolucion);

            sctx.Entry(oserie).Reload();
            Assert.AreEqual("AC", oserie.status);

            var ctx = new DataAccess.SirCoPVDataContext();
            var dev = ctx.Devoluciones.Where(i => i.sucursal == "08" && i.devolvta == "023287").Single();
            Assert.AreEqual(0, dev.disponible);

            var cancel = new Common.Entities.CancelSaleRequest
            {
                Sucursal = "08",
                Folio = "295595"
            };
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => { return DateTime.Parse("2019-03-07"); };
                _context.CancelSale(cancel, 0);
            }

            ctx.Entry(dev).Reload();
            Assert.AreEqual(539, dev.disponible);

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => { return DateTime.Parse("2019-03-07"); };
                _context.CancelReturn(dev.sucursal, dev.devolvta, 0);
            }
            ctx.Entry(dev).Reload();
            Assert.AreEqual("ZC", dev.estatus);

            sctx.Entry(oserie).Reload();
            Assert.AreEqual("BA", oserie.status);
        }
        [TestMethod]
        public void CancelFormaPago_EF()
        {
            var compro = "0000003678429";
            var pago = 539m;
            var model = new Common.Entities.SaleRequest()
            {
                Sucursal = "08",
                VendedorId = 132
            };
            model.Productos = new Common.Entities.SerieFormasPago[] {
                new Common.Entities.SerieFormasPago { Serie = compro, FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF }}
            };
            model.Pagos = new Common.Entities.Pago[] {
                new Common.Entities.Pago { FormaPago = Common.Constants.FormaPago.EF, Importe = pago }
            };

            _context.RequestProducto(compro, 0);

            Common.Entities.SaleResponse folio = null;
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => { return DateTime.Parse("2019-03-06"); };
                folio = _context.Sale(model, 0);
            }

            Assert.AreEqual("295595", folio.Folio);

            var request = new Common.Entities.CancelSaleRequest
            {
                Sucursal = "08",
                Folio = "295595"
            };
            _context.CancelSale(request, 0);
        }
        [TestMethod]
        public void CancelFormaPago_MD()
        {
            var sucursal = "01";
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => { return DateTime.Parse("2019-09-01"); };

                var data = new List<DataAccess.SirCoControl.Sucursal> {
                    new DataAccess.SirCoControl.Sucursal { idsucursal = 1, sucursal = "01", cajas = 434045 }
                }.AsQueryable();

                var mockSet = new Mock<DbSet<DataAccess.SirCoControl.Sucursal>>();
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

                DataAccess.Fakes.ShimSirCoControlDataContext.AllInstances.SucursalesGet = (sctx) => {
                    return mockSet.Object;
                };

                var pserie = "0000003668367";
                var prod = _sale.ScanProducto(pserie, sucursal);
                Assert.IsNotNull(prod);
                //var vale = _context.CheckVale(nvale);

                var ctx = new DataAccess.SirCoDataContext();
                ctx.UpdateSerieStatus("0000003668367", Status.CA, Status.AC, 0);

                var ncliente = new Common.Entities.Cliente
                {
                    Id = 810374
                };

                //var cid = _process.AddCliente(ncliente);
                //Assert.IsNotNull(cid);

                var model = new Common.Entities.SaleRequest()
                {
                    Sucursal = sucursal,
                    VendedorId = 0,
                    Cliente = ncliente
                };
                model.Productos = new Common.Entities.SerieFormasPago[] {
                    new Common.Entities.SerieFormasPago { Serie = pserie, FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.VA } }
                };
                model.Pagos = new List<Common.Entities.Pago> {
                    new Common.Entities.Pago {
                        FormaPago = FormaPago.MD,
                        Importe = 149
                    }
                };

                var folio = _context.Sale(model, 0);

                Assert.AreEqual("434046", folio.Folio);

                var ctxa = new DataAccess.SirCoAPPDataContext();
                var dinero = ctxa.Dineros.Where(i => i.idsucursal == 1 && i.cliente == "144666").Single();
                var det = dinero.Detalles.OrderByDescending(i => i.vigencia).ToArray();

                Assert.AreEqual(151, dinero.saldo);
                Assert.AreEqual(0, det[0].saldo);
                Assert.AreEqual(51, det[1].saldo);
                Assert.AreEqual(100, det[2].saldo);
                
                var request = new Common.Entities.CancelSaleRequest
                {
                    Sucursal = sucursal,
                    Folio = "434046"
                };
                _context.CancelSale(request, 0);

                var ctxa2 = new DataAccess.SirCoAPPDataContext();
                dinero = ctxa2.Dineros.Where(i => i.idsucursal == 1 && i.cliente == "144666").Single();
                det = dinero.Detalles.OrderByDescending(i => i.vigencia).ToArray();

                Assert.AreEqual(300, dinero.saldo);
                Assert.AreEqual(100, det[0].saldo);
                Assert.AreEqual(100, det[1].saldo);
                Assert.AreEqual(100, det[2].saldo);
            }            
        }
        [TestMethod]
        public void CancelFormaPago_TC()
        {
            var compro = "0000003678429";
            var pago = 539m;
            var model = new Common.Entities.SaleRequest()
            {
                Sucursal = "08",
                VendedorId = 132
            };
            model.Productos = new Common.Entities.SerieFormasPago[] {
                new Common.Entities.SerieFormasPago { Serie = compro, FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF }}
            };
            model.Pagos = new Common.Entities.Pago[] {
                new Common.Entities.Pago {
                    FormaPago = Common.Constants.FormaPago.TC,
                    Importe = pago,
                    Terminacion = "123"
                }
            };

            _context.RequestProducto(compro, 0);

            Common.Entities.SaleResponse folio = null;
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => { return DateTime.Parse("2019-03-06"); };
                folio = _context.Sale(model, 0);
            }

            Assert.AreEqual("295595", folio.Folio);

            var request = new Common.Entities.CancelSaleRequest
            {
                Sucursal = "08",
                Folio = "295595"
            };
            _context.CancelSale(request, 0);
        }
        [TestMethod]
        public void CancelFormaPago_TD()
        {
            var compro = "0000003678429";
            var pago = 539m;
            var model = new Common.Entities.SaleRequest()
            {
                Sucursal = "08",
                VendedorId = 132
            };
            model.Productos = new Common.Entities.SerieFormasPago[] {
                new Common.Entities.SerieFormasPago { Serie = compro, FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF }}
            };
            model.Pagos = new Common.Entities.Pago[] {
                new Common.Entities.Pago {
                    FormaPago = Common.Constants.FormaPago.TD,
                    Importe = pago,
                    Terminacion = "123"
                }
            };

            _context.RequestProducto(compro, 0);

            Common.Entities.SaleResponse folio = null;
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => { return DateTime.Parse("2019-03-06"); };
                folio = _context.Sale(model, 0);
            }

            Assert.AreEqual("295595", folio.Folio);

            var request = new Common.Entities.CancelSaleRequest
            {
                Sucursal = "08",
                Folio = "295595"
            };
            _context.CancelSale(request, 0);
        }
        [TestMethod]
        public void CancelFormaPago_VA()
        {
            var compro = "0000003678429";
            var pago = 539m;
            var model = new Common.Entities.SaleRequest()
            {
                Sucursal = "08",
                VendedorId = 132,
                Cliente = new Common.Entities.Cliente
                {
                    Id = 810374
                }
            };
            model.Productos = new Common.Entities.SerieFormasPago[] {
                new Common.Entities.SerieFormasPago { Serie = compro, FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF }}
            };
            model.Pagos = new Common.Entities.Pago[] {
                new Common.Entities.Pago {
                    FormaPago = Common.Constants.FormaPago.VA,
                    Importe = pago,
                    Vale = "110472",
                    Plazos = 13,
                    FechaAplicar = DateTime.Parse("2019-11-05")
                }
            };

            _context.RequestProducto(compro, 0);

            Common.Entities.SaleResponse folio = null;
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => { return DateTime.Parse("2019-03-06"); };
                folio = _context.Sale(model, 0);
            }

            Assert.AreEqual("295595", folio.Folio);

            var ctxc = new DataAccess.SirCoCreditoDataContext();
            var dis = ctxc.Distribuidores.Where(i => i.iddistrib == 4281).Single();
            Assert.AreEqual(14732.70m - pago, dis.disponible);
            Assert.AreEqual(32267.30m + pago, dis.saldo);

            var request = new Common.Entities.CancelSaleRequest
            {
                Sucursal = "08",
                Folio = "295595"
            };
            _context.CancelSale(request, 0);

            ctxc.Entry(dis).Reload();
            Assert.AreEqual(14732.70m, dis.disponible);
            Assert.AreEqual(32267.30m, dis.saldo);
        }
        [TestMethod]
        public void CancelFormaPago_VE()
        {
            var compro = "0000003678429";
            var pago = 539m;
            var model = new Common.Entities.SaleRequest()
            {
                Sucursal = "08",
                VendedorId = 132,
                Cliente = new Common.Entities.Cliente
                {
                    Id = 810374
                }
            };
            model.Productos = new Common.Entities.SerieFormasPago[] {
                new Common.Entities.SerieFormasPago { Serie = compro, FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF }}
            };
            model.Pagos = new Common.Entities.Pago[] {
                new Common.Entities.Pago {
                    FormaPago = Common.Constants.FormaPago.VE,
                    Negocio = 217,
                    NoCuenta = "813",
                    Importe = pago,
                    Vale = "110472",
                    Plazos = 13,
                    FechaAplicar = DateTime.Parse("2019-11-05")
                }
            };

            _context.RequestProducto(compro, 0);

            Common.Entities.SaleResponse folio = null;
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => { return DateTime.Parse("2019-03-06"); };
                folio = _context.Sale(model, 0);
            }

            Assert.AreEqual("295595", folio.Folio);

            var ctxc = new DataAccess.SirCoCreditoDataContext();
            var dis = ctxc.Distribuidores.Where(i => i.iddistrib == 4281).Single();
            Assert.AreEqual(14732.70m - pago, dis.disponible);
            Assert.AreEqual(32267.30m + pago, dis.saldo);

            var request = new Common.Entities.CancelSaleRequest
            {
                Sucursal = "08",
                Folio = "295595"
            };
            _context.CancelSale(request, 0);

            ctxc.Entry(dis).Reload();
            Assert.AreEqual(14732.70m, dis.disponible);
            Assert.AreEqual(32267.30m, dis.saldo);
        }
        [TestMethod]
        public void CancelFormaPago_CD()
        {
            var sucursal = "01";
            //var sucursalId = 1;
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => { return DateTime.Parse("2019-09-01"); };

                var data = new List<DataAccess.SirCoControl.Sucursal> {
                    new DataAccess.SirCoControl.Sucursal { idsucursal = 1, sucursal = "01", cajas = 434045 }
                }.AsQueryable();

                var mockSet = new Mock<DbSet<DataAccess.SirCoControl.Sucursal>>();
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

                DataAccess.Fakes.ShimSirCoControlDataContext.AllInstances.SucursalesGet = (sctx) => {
                    return mockSet.Object;
                };

                var pserie = "0000003668367";
                var distrib = "005961";
                var prod = _sale.ScanProducto(pserie, sucursal);
                Assert.IsNotNull(prod);

                var ctx = new DataAccess.SirCoDataContext();
                ctx.UpdateSerieStatus("0000003668367", Status.CA, Status.AC, 0);

                var dis = _sale.FindTarjetahabiente(distrib);

                var ncliente = new Common.Entities.Cliente
                {
                    DistribuidorId = 5730
                };

                //var cid = _process.AddCliente(ncliente);
                //Assert.IsNotNull(cid);

                var model = new Common.Entities.SaleRequest()
                {
                    Sucursal = sucursal,
                    VendedorId = 0,
                    Cliente = ncliente
                };
                model.Productos = new Common.Entities.SerieFormasPago[] {
                    new Common.Entities.SerieFormasPago { Serie = pserie, FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.VA } }
                };
                model.Pagos = new List<Common.Entities.Pago> {
                    new Common.Entities.Pago {
                        FormaPago = FormaPago.CD,
                        Importe = prod.Producto.Precio.Value,
                        Distribuidor = distrib,
                        Plazos = 3
                    }
                };

                var ctxc = new SirCoPOS.DataAccess.SirCoCreditoDataContext();
                var ds = ctxc.Distribuidores.Where(i => i.distrib == distrib).Single();
                Assert.AreEqual(11564.00M, ds.disponible);
                Assert.AreEqual(3436.00M, ds.saldo);

                var folio = _context.Sale(model, 0);

                Assert.AreEqual("434046", folio.Folio);
                ctxc.Entry(ds).Reload();
                Assert.AreEqual(11564.00M - prod.Producto.Precio.Value, ds.disponible);
                Assert.AreEqual(3436.00M + prod.Producto.Precio.Value, ds.saldo);

                var crequest = new Common.Entities.CancelSaleRequest
                {
                    Sucursal = sucursal,
                    Folio = "434046"
                };
                _context.CancelSale(crequest, 0);

                ctxc.Entry(ds).Reload();
                Assert.AreEqual(11564.00M, ds.disponible);
                Assert.AreEqual(3436.00M, ds.saldo);

            }

        }
        [TestMethod]
        public void CancelFormaPago_VD()
        {
            var sucursal = "01";
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => { return DateTime.Parse("2019-09-01"); };

                var data = new List<DataAccess.SirCoControl.Sucursal> {
                    new DataAccess.SirCoControl.Sucursal { idsucursal = 1, sucursal = "01", cajas = 434045 }
                }.AsQueryable();

                var mockSet = new Mock<DbSet<DataAccess.SirCoControl.Sucursal>>();
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

                DataAccess.Fakes.ShimSirCoControlDataContext.AllInstances.SucursalesGet = (sctx) => {
                    return mockSet.Object;
                };

                var pserie = "0000003668367";
                var nvale = "123";
                var prod = _sale.ScanProducto(pserie, sucursal);
                Assert.IsNotNull(prod);
                //var vale = _context.CheckVale(nvale);

                var ctx = new DataAccess.SirCoDataContext();
                ctx.UpdateSerieStatus("0000003668367", Status.CA, Status.AC, 0);

                var ncliente = new Common.Entities.Cliente
                {
                    Id = 810374
                };

                //var cid = _process.AddCliente(ncliente);
                //Assert.IsNotNull(cid);

                var model = new Common.Entities.SaleRequest()
                {
                    Sucursal = sucursal,
                    VendedorId = 0,
                    Cliente = ncliente
                };
                model.Productos = new Common.Entities.SerieFormasPago[] {
                    new Common.Entities.SerieFormasPago { Serie = pserie, FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.VA } }
                };
                model.Pagos = new List<Common.Entities.Pago> {
                    new Common.Entities.Pago {
                        FormaPago = FormaPago.VD,
                        Importe = prod.Producto.Precio.Value,
                        Vale = nvale,
                        Plazos = 13,
                        FechaAplicar = DateTime.Parse("2019-11-05")
                        //ClienteId = cid,                        
                    }
                };

                var ctxa = new DataAccess.SirCoAPPDataContext();
                var vd = ctxa.ValesDigital.Where(i => i.idvaledigital == 2).Single();
                Assert.AreEqual(2000, vd.disponible);
                
                var ctxc = new DataAccess.SirCoCreditoDataContext();
                var dis = ctxc.Distribuidores.Where(i => i.distrib == "003763").Single();
                Assert.AreEqual(24057.39m, dis.saldo);
                Assert.AreEqual(73442.61m, dis.disponible);
                
                var folio = _context.Sale(model, 0);

                Assert.AreEqual("434046", folio.Folio);

                ctxa.Entry(vd).Reload();
                Assert.AreEqual(2000 - prod.Producto.Precio.Value, vd.disponible);
                ctxc.Entry(dis).Reload();
                Assert.AreEqual(24057.39m + prod.Producto.Precio.Value, dis.saldo);
                Assert.AreEqual(73442.61m - prod.Producto.Precio.Value, dis.disponible);

                var crequest = new Common.Entities.CancelSaleRequest
                {
                    Sucursal = sucursal,
                    Folio = "434046"
                };
                _context.CancelSale(crequest, 0);

                ctxa.Entry(vd).Reload();
                Assert.AreEqual(2000, vd.disponible);
                ctxc.Entry(dis).Reload();
                Assert.AreEqual(24057.39m, dis.saldo);
                Assert.AreEqual(73442.61m, dis.disponible);
            }
        }
    }
}
