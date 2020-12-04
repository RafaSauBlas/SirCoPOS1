using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using SirCoPOS.Common.Constants;
using System.Linq;
using Microsoft.QualityTools.Testing.Fakes;

namespace SirCoPOS.Tests
{
    [TestClass]
    public class ChangeUnitTest
    {
        public TestContext TestContext { get; set; }
        public ChangeUnitTest()
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
        public void ChangeTest()
        {
            var request = new Common.Entities.ChangeRequest
            {
                Sucursal = "01",
                Folio = "422168",
                Items = new Common.Entities.ChangeItem[] 
                {
                    new Common.Entities.ChangeItem { OldItem = "0000003518445", NewItem = "0000003518435" },
                    new Common.Entities.ChangeItem { OldItem = "0000003583185", NewItem = "0000003579528" },
                    new Common.Entities.ChangeItem { OldItem = "0000003584922", NewItem = "0000003584920" }
                }
            };

            var ctx = new DataAccess.SirCoDataContext();
            ctx.UpdateSerieStatus("0000003518435", Status.CA, Status.AC, 0);
            ctx.UpdateSerieStatus("0000003579528", Status.CA, Status.AC, 0);
            ctx.UpdateSerieStatus("0000003584920", Status.CA, Status.AC, 0);

            var res = _context.Change(request, 0, request.Sucursal);

            Assert.AreEqual("027852", res.Devolucion);
            Assert.AreEqual("434054", res.Venta);
        }
        [TestMethod]
        public void ReturnMismaCorrida2()
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
        }
        [TestMethod]
        public void CambioMismaCorridaCambioPrecioMenor()
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


            var sctx = new DataAccess.SirCoDataContext();
            var cor = sctx.Corridas.Where(i => i.marca == "CTA" && i.estilon.Trim() == "682" && i.corrida == "A").Single();
            cor.precio = 500;
            sctx.SaveChanges();

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
            //Assert.AreEqual(39, dev.disponible);
            Assert.AreEqual(0, dev.disponible);

            var vdet = ctx.VentasDetalle.Where(i => i.sucursal == "08" && i.venta == "295595" && i.serie == "0000003522471").Single();
            //Assert.AreEqual(500, vdet.precio);
            //Assert.AreEqual(500, vdet.precdesc);
            Assert.AreEqual(539, vdet.precio);
            Assert.AreEqual(539, vdet.precdesc);
        }
        [TestMethod]
        public void CambioMismaCorridaCambioPrecioMenorPromocion()
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

            var ctx = new DataAccess.SirCoPVDataContext();
            var det = ctx.VentasDetalle.Where(i => i.sucursal == model.Sucursal
                && i.venta == "434054" && i.serie == "0000003678429").Single();
            det.idpromocion = 18;//prueba
            det.precdesc = 269.5m;//539 / 2 = 50%
            ctx.SaveChanges();

            var sctx = new DataAccess.SirCoDataContext();
            var cor = sctx.Corridas.Where(i => i.marca == "CTA" && i.estilon.Trim() == "682" && i.corrida == "A").Single();
            cor.precio = 500;
            sctx.SaveChanges();

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
            
            var dev = ctx.Devoluciones.Where(i => i.sucursal == "08" && i.devolvta == "023287").Single();
            //Assert.AreEqual(19.5m, dev.disponible);
            Assert.AreEqual(0m, dev.disponible);

            var vdet = ctx.VentasDetalle.Where(i => i.sucursal == "08" && i.venta == "295595" && i.serie == "0000003522471").Single();
            //Assert.AreEqual(500, vdet.precio);
            //Assert.AreEqual(250, vdet.precdesc);
            Assert.AreEqual(539m, vdet.precio);
            Assert.AreEqual(269.5m, vdet.precdesc);
        }
        [TestMethod]
        public void CambioMismaCorridaCambioPrecioMayorPromocion()
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

            var ctx = new DataAccess.SirCoPVDataContext();
            var det = ctx.VentasDetalle.Where(i => i.sucursal == model.Sucursal
                && i.venta == "434054" && i.serie == "0000003678429").Single();
            det.idpromocion = 18;//prueba
            det.precdesc = 269.5m;//539 / 2 = 50%
            ctx.SaveChanges();

            var sctx = new DataAccess.SirCoDataContext();
            var cor = sctx.Corridas.Where(i => i.marca == "CTA" && i.estilon.Trim() == "682" && i.corrida == "A").Single();
            cor.precio = 600;
            sctx.SaveChanges();

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

            var dev = ctx.Devoluciones.Where(i => i.sucursal == "08" && i.devolvta == "023287").Single();
            Assert.AreEqual(0m, dev.disponible);

            var vdet = ctx.VentasDetalle.Where(i => i.sucursal == "08" && i.venta == "295595" && i.serie == "0000003522471").Single();
            Assert.AreEqual(539m, vdet.precio);
            Assert.AreEqual(269.5m, vdet.precdesc);
        }
        [TestMethod]
        public void CambioMismaCorridaCambioPrecioMayor()
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


            var sctx = new DataAccess.SirCoDataContext();
            var cor = sctx.Corridas.Where(i => i.marca == "CTA" && i.estilon.Trim() == "682" && i.corrida == "A").Single();
            cor.precio = 600;
            sctx.SaveChanges();

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

            var vdet = ctx.VentasDetalle.Where(i => i.sucursal == "08" && i.venta == "295595" && i.serie == "0000003522471").Single();
            Assert.AreEqual(539, vdet.precio);
            Assert.AreEqual(539, vdet.precdesc);
        }
        [TestMethod]
        public void ReturnDifCorridaMismoPrecio2()
        {
            /*
            1	NULL	0000003678429	AC	539.00	CTA	    682	16-	A	15	17	324
            2	NULL	0000003522471	AC	539.00	CTA	    682	15	A	15	17	324
            3	NULL	0000003776476	AC	589.00	CTA	    682	19-	B	17-	21	324
            4	NULL	0000003776472	AC	589.00	CTA	    682	18-	B	17-	21	324
            5	NULL	0000003678238	AC	649.00	CTA	    682	24	C	21-	26	324
            6	NULL	0000003676610	AC	649.00	CTA	    682	25	C	21-	26	324
            */

            var ctx = new DataAccess.SirCoPVDataContext();
            var sctx = new DataAccess.SirCoDataContext();

            var cor = sctx.Corridas.Where(i => i.marca == "KLI" && i.estilon == "      1" && i.corrida == "D").Single();
            cor.precio = 539;
            sctx.SaveChanges();

            var model = new Common.Entities.SaleRequest()
            {
                Sucursal = "01",
                VendedorId = 132
            };
            model.Productos = new Common.Entities.SerieFormasPago[] {
                new Common.Entities.SerieFormasPago { Serie = "0000003391871", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF }}
            };
            model.Pagos = new Common.Entities.Pago[] {
                new Common.Entities.Pago { FormaPago = Common.Constants.FormaPago.EF, Importe = 539 }
            };

            _context.RequestProducto("0000003391871", 0);

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
                    new Common.Entities.ChangeItem { OldItem = "0000003391871", NewItem = "0000003678429" }
                }
            };
            _context.RequestProducto("0000003678429", 0);
            Common.Entities.ChangeResponse cres = null;
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => { return DateTime.Parse("2019-03-07"); };
                cres = _context.Change(crequest, 0, "08");
            }

            Assert.AreEqual("295595", cres.Venta);
            Assert.AreEqual("023287", cres.Devolucion);

            var dev = ctx.Devoluciones.Where(i => i.sucursal == "08" && i.devolvta == "023287").Single();
            Assert.AreEqual(0, dev.disponible);
        }
        [TestMethod]
        public void ChangeDifCorridaDev()
        {
            /*
            1	NULL	0000003678429	AC	539.00	CTA	    682	16-	A	15	17	324
            2	NULL	0000003522471	AC	539.00	CTA	    682	15	A	15	17	324
            3	NULL	0000003776476	AC	589.00	CTA	    682	19-	B	17-	21	324
            4	NULL	0000003776472	AC	589.00	CTA	    682	18-	B	17-	21	324
            5	NULL	0000003678238	AC	649.00	CTA	    682	24	C	21-	26	324
            6	NULL	0000003676610	AC	649.00	CTA	    682	25	C	21-	26	324
            */

            var compro = "0000003776476";
            var pago = 589;
            var cambio = "0000003678429";

            var model = new Common.Entities.SaleRequest()
            {
                Sucursal = "01",
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

            Assert.AreEqual("434054", folio.Folio);

            var crequest = new Common.Entities.ChangeRequest
            {
                Sucursal = "01",
                Folio = "434054",
                Items = new Common.Entities.ChangeItem[]
                {
                    new Common.Entities.ChangeItem { OldItem = compro, NewItem = cambio }
                }
            };
            _context.RequestProducto(cambio, 0);
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
            Assert.AreEqual(50, dev.disponible);
        }
        [TestMethod]
        public void ChangeDifCorridaPago()
        {
            /*
            1	NULL	0000003678429	AC	539.00	CTA	    682	16-	A	15	17	324
            2	NULL	0000003522471	AC	539.00	CTA	    682	15	A	15	17	324
            3	NULL	0000003776476	AC	589.00	CTA	    682	19-	B	17-	21	324
            4	NULL	0000003776472	AC	589.00	CTA	    682	18-	B	17-	21	324
            5	NULL	0000003678238	AC	649.00	CTA	    682	24	C	21-	26	324
            6	NULL	0000003676610	AC	649.00	CTA	    682	25	C	21-	26	324
            */

            var compro = "0000003678429";
            var pago = 539;
            var cambio = "0000003776476";

            var model = new Common.Entities.SaleRequest()
            {
                Sucursal = "01",
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

            Assert.AreEqual("434054", folio.Folio);

            var crequest = new Common.Entities.ChangeRequest
            {
                Sucursal = "01",
                Folio = "434054",
                Items = new Common.Entities.ChangeItem[]
                {
                    new Common.Entities.ChangeItem { OldItem = compro, NewItem = cambio }
                },
                Pagos = new Common.Entities.Pago[] {
                    new Common.Entities.Pago { FormaPago = FormaPago.EF, Importe = 50 }
                }
            };
            _context.RequestProducto(cambio, 0);
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
        }
        [TestMethod]
        public void CambioPromoMismaCorridaValidarCliente()
        {
            /*
            1	NULL	0000003678429	AC	539.00	CTA	    682	16-	A	15	17	324
            2	NULL	0000003522471	AC	539.00	CTA	    682	15	A	15	17	324
            3	NULL	0000003776476	AC	589.00	CTA	    682	19-	B	17-	21	324
            4	NULL	0000003776472	AC	589.00	CTA	    682	18-	B	17-	21	324
            5	NULL	0000003678238	AC	649.00	CTA	    682	24	C	21-	26	324
            6	NULL	0000003676610	AC	649.00	CTA	    682	25	C	21-	26	324
            */

            var compro = "0000003776476";
            var pago = 412.3m;//589m;
            var disponible = 0;
            var cambio = "0000003776472";
            var cid = 810374;

            var model = new Common.Entities.SaleRequest()
            {
                Sucursal = "01",
                VendedorId = 132,
                PromocionesCupones = new Common.Entities.PromocionCuponItem[] 
                {
                    new Common.Entities.PromocionCuponItem { PromocionId = 200 }
                },
                Cliente = new Common.Entities.Cliente { Id = cid }
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
                System.Fakes.ShimDateTime.UtcNowGet = () => DateTime.Parse("2019-10-20").ToUniversalTime();
                folio = _context.Sale(model, 0);
            }

            Assert.AreEqual("434054", folio.Folio);


            var ctxpv = new DataAccess.SirCoPVDataContext();
            var venta = ctxpv.Ventas.Where(i => i.sucursal == "01" && i.venta == "434054").Single();
            Assert.AreEqual(cid, venta.idcliente);

            var crequest = new Common.Entities.ChangeRequest
            {
                Sucursal = "01",
                Folio = "434054",
                Items = new Common.Entities.ChangeItem[]
                {
                    new Common.Entities.ChangeItem { OldItem = compro, NewItem = cambio }
                }
            };
            _context.RequestProducto(cambio, 0);
            Common.Entities.ChangeResponse cres = null;
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.UtcNowGet = () => DateTime.Parse("2019-10-21").ToUniversalTime();
                cres = _context.Change(crequest, 0, "08");
            }

            Assert.AreEqual("295595", cres.Venta);
            Assert.AreEqual("023287", cres.Devolucion);

            var ctx = new DataAccess.SirCoPVDataContext();
            var dev = ctx.Devoluciones.Where(i => i.sucursal == "08" && i.devolvta == "023287").Single();
            Assert.AreEqual(disponible, dev.disponible);
            Assert.AreEqual(cid, dev.idcliente);

            var venta2 = ctxpv.Ventas.Where(i => i.sucursal == "08" && i.venta == "295595").Single();
            Assert.AreEqual(cid, venta.idcliente);
        }
        [TestMethod]
        public void CambioPromoDifCorridaMismoPrecio()
        {
            /*
            1	NULL	0000003678429	AC	539.00	CTA	    682	16-	A	15	17	324
            2	NULL	0000003522471	AC	539.00	CTA	    682	15	A	15	17	324
            3	NULL	0000003776476	AC	589.00	CTA	    682	19-	B	17-	21	324
            4	NULL	0000003776472	AC	589.00	CTA	    682	18-	B	17-	21	324
            5	NULL	0000003678238	AC	649.00	CTA	    682	24	C	21-	26	324
            6	NULL	0000003676610	AC	649.00	CTA	    682	25	C	21-	26	324
            */

            var compro = "0000003776476";
            var pago = 412.3m;//589m;
            var disponible = 0;
            var cambio = "0000003678429";

            var sctx = new DataAccess.SirCoDataContext();
            var cor = sctx.Corridas.Where(i => i.marca == "CTA" && i.estilon == "    682" && i.corrida == "A").Single();
            cor.precio = 589m;
            sctx.SaveChanges();

            var model = new Common.Entities.SaleRequest()
            {
                Sucursal = "01",
                VendedorId = 132,
                PromocionesCupones = new Common.Entities.PromocionCuponItem[]
                {
                    new Common.Entities.PromocionCuponItem { PromocionId = 200 }
                }
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
                System.Fakes.ShimDateTime.UtcNowGet = () => DateTime.Parse("2019-10-20").ToUniversalTime();
                folio = _context.Sale(model, 0);
            }

            Assert.AreEqual("434054", folio.Folio);

            var crequest = new Common.Entities.ChangeRequest
            {
                Sucursal = "01",
                Folio = "434054",
                Items = new Common.Entities.ChangeItem[]
                {
                    new Common.Entities.ChangeItem { OldItem = compro, NewItem = cambio }
                }
            };
            _context.RequestProducto(cambio, 0);
            Common.Entities.ChangeResponse cres = null;
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.UtcNowGet = () => DateTime.Parse("2019-10-21").ToUniversalTime();
                cres = _context.Change(crequest, 0, "08");
            }

            Assert.AreEqual("295595", cres.Venta);
            Assert.AreEqual("023287", cres.Devolucion);

            var ctx = new DataAccess.SirCoPVDataContext();
            var dev = ctx.Devoluciones.Where(i => i.sucursal == "08" && i.devolvta == "023287").Single();
            Assert.AreEqual(disponible, dev.disponible);
        }
        [TestMethod]
        public void CambioPromoDifCorridaPrecioMenor()
        {
            /*
            1	NULL	0000003678429	AC	539.00	CTA	    682	16-	A	15	17	324
            2	NULL	0000003522471	AC	539.00	CTA	    682	15	A	15	17	324
            3	NULL	0000003776476	AC	589.00	CTA	    682	19-	B	17-	21	324
            4	NULL	0000003776472	AC	589.00	CTA	    682	18-	B	17-	21	324
            5	NULL	0000003678238	AC	649.00	CTA	    682	24	C	21-	26	324
            6	NULL	0000003676610	AC	649.00	CTA	    682	25	C	21-	26	324
            */

            var compro = "0000003776476";
            var pago = 412.3m;//589m;
            var disponible = 35;
            var cambio = "0000003678429";

            var model = new Common.Entities.SaleRequest()
            {
                Sucursal = "01",
                VendedorId = 132,
                PromocionesCupones = new Common.Entities.PromocionCuponItem[]
                {
                    new Common.Entities.PromocionCuponItem { PromocionId = 200 }
                }
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
                System.Fakes.ShimDateTime.UtcNowGet = () => DateTime.Parse("2019-10-20").ToUniversalTime();
                folio = _context.Sale(model, 0);
            }

            Assert.AreEqual("434054", folio.Folio);

            var crequest = new Common.Entities.ChangeRequest
            {
                Sucursal = "01",
                Folio = "434054",
                Items = new Common.Entities.ChangeItem[]
                {
                    new Common.Entities.ChangeItem { OldItem = compro, NewItem = cambio }
                }
            };
            _context.RequestProducto(cambio, 0);
            Common.Entities.ChangeResponse cres = null;
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.UtcNowGet = () => DateTime.Parse("2019-10-21").ToUniversalTime();
                cres = _context.Change(crequest, 0, "08");
            }

            Assert.AreEqual("295595", cres.Venta);
            Assert.AreEqual("023287", cres.Devolucion);

            var ctx = new DataAccess.SirCoPVDataContext();
            var dev = ctx.Devoluciones.Where(i => i.sucursal == "08" && i.devolvta == "023287").Single();
            Assert.AreEqual(disponible, dev.disponible);
        }
        [TestMethod]
        public void CambioPromoDifCorridaPrecioMayor()
        {
            /*
            1	NULL	0000003678429	AC	539.00	CTA	    682	16-	A	15	17	324
            2	NULL	0000003522471	AC	539.00	CTA	    682	15	A	15	17	324
            3	NULL	0000003776476	AC	589.00	CTA	    682	19-	B	17-	21	324
            4	NULL	0000003776472	AC	589.00	CTA	    682	18-	B	17-	21	324
            5	NULL	0000003678238	AC	649.00	CTA	    682	24	C	21-	26	324
            6	NULL	0000003676610	AC	649.00	CTA	    682	25	C	21-	26	324
            */

            var compro = "0000003776476";
            var pago = 412.3m;//589m;
            var disponible = 0;
            var cambio = "0000003678238";

            var model = new Common.Entities.SaleRequest()
            {
                Sucursal = "01",
                VendedorId = 132,
                PromocionesCupones = new Common.Entities.PromocionCuponItem[]
                {
                    new Common.Entities.PromocionCuponItem { PromocionId = 200 }
                }
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
                System.Fakes.ShimDateTime.UtcNowGet = () => DateTime.Parse("2019-10-20").ToUniversalTime();
                folio = _context.Sale(model, 0);
            }

            Assert.AreEqual("434054", folio.Folio);

            var crequest = new Common.Entities.ChangeRequest
            {
                Sucursal = "01",
                Folio = "434054",
                Items = new Common.Entities.ChangeItem[]
                {
                    new Common.Entities.ChangeItem { OldItem = compro, NewItem = cambio }
                },
                Pagos = new Common.Entities.Pago[] {
                    new Common.Entities.Pago { FormaPago = FormaPago.EF, Importe = 60 }
                }
            };
            _context.RequestProducto(cambio, 0);
            Common.Entities.ChangeResponse cres = null;
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.UtcNowGet = () => DateTime.Parse("2019-10-21").ToUniversalTime();
                cres = _context.Change(crequest, 0, "08");
            }

            Assert.AreEqual("295595", cres.Venta);
            Assert.AreEqual("023287", cres.Devolucion);

            var ctx = new DataAccess.SirCoPVDataContext();
            var dev = ctx.Devoluciones.Where(i => i.sucursal == "08" && i.devolvta == "023287").Single();
            Assert.AreEqual(disponible, dev.disponible);
        }
        [TestMethod]
        public void CambioPromoDifProdMismoPrecio()
        {
            /*
            1	NULL	0000003678429	AC	539.00	CTA	    682	16-	A	15	17	324
            2	NULL	0000003522471	AC	539.00	CTA	    682	15	A	15	17	324
            3	NULL	0000003776476	AC	589.00	CTA	    682	19-	B	17-	21	324
            4	NULL	0000003776472	AC	589.00	CTA	    682	18-	B	17-	21	324
            5	NULL	0000003678238	AC	649.00	CTA	    682	24	C	21-	26	324
            6	NULL	0000003676610	AC	649.00	CTA	    682	25	C	21-	26	324
            */

            var compro = "0000003776476";
            var pago = 412.3m;//589m;
            var disponible = 0;
            var cambio = "0000003391871";

            var sctx = new DataAccess.SirCoDataContext();
            var cor = sctx.Corridas.Where(i => i.marca == "KLI" && i.estilon == "      1" && i.corrida == "D").Single();
            cor.precio = 412.3m;
            sctx.SaveChanges();

            var model = new Common.Entities.SaleRequest()
            {
                Sucursal = "01",
                VendedorId = 132,
                PromocionesCupones = new Common.Entities.PromocionCuponItem[]
                {
                    new Common.Entities.PromocionCuponItem { PromocionId = 200 }
                }
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
                System.Fakes.ShimDateTime.UtcNowGet = () => DateTime.Parse("2019-10-20").ToUniversalTime();
                folio = _context.Sale(model, 0);
            }

            Assert.AreEqual("434054", folio.Folio);

            var crequest = new Common.Entities.ChangeRequest
            {
                Sucursal = "01",
                Folio = "434054",
                Items = new Common.Entities.ChangeItem[]
                {
                    new Common.Entities.ChangeItem { OldItem = compro, NewItem = cambio }
                }
            };
            _context.RequestProducto(cambio, 0);
            Common.Entities.ChangeResponse cres = null;
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.UtcNowGet = () => DateTime.Parse("2019-10-21").ToUniversalTime();
                cres = _context.Change(crequest, 0, "08");
            }

            Assert.AreEqual("295595", cres.Venta);
            Assert.AreEqual("023287", cres.Devolucion);

            var ctx = new DataAccess.SirCoPVDataContext();
            var dev = ctx.Devoluciones.Where(i => i.sucursal == "08" && i.devolvta == "023287").Single();
            Assert.AreEqual(disponible, dev.disponible);
        }
    }
}
