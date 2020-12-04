using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.QualityTools.Testing.Fakes;
using System.Threading;
using System.Linq;

namespace SirCoPOS.Tests
{
    [TestClass]
    public class ReturnUnitTest
    {
        public TestContext TestContext { get; set; }
        public ReturnUnitTest()
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
        public void ReturnTest()
        {
            var items = new string[] {
                "0000003445034",
                "0000003506774"
            };

            var request = new Common.Entities.ReturnRequest
            {
                Sucursal = "01",
                Folio = "414628",
                Items = items,
                Comments = "CAMBIO POR MODELO"
            };
            string folio = null;
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => { return DateTime.Parse("2018-06-16"); };
                folio = _context.Return(request, 0, request.Sucursal);
            }
            Assert.IsNotNull(folio);
        }

        [TestMethod]
        public void ReturnTest2()
        {
            var items = new string[] {
                "0000003487207"
            };

            var request = new Common.Entities.ReturnRequest
            {
                Sucursal = "01",
                Folio = "424749",
                Items = items,
                Comments = "POR MODELO"
            };
            string folio = null;
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => { return DateTime.Parse("2018-12-18"); };
                folio = _context.Return(request, 0, request.Sucursal);
            }
            Assert.IsNotNull(folio);
        }
        [TestMethod]
        public void ReturnClienteTest()
        {
            var items = new string[] {
                "0000003445034",
                "0000003506774"
            };

            var request = new Common.Entities.ReturnRequest
            {
                Sucursal = "01",
                Folio = "414628",
                Items = items,
                Comments = "CAMBIO POR MODELO",
                Cliente = new Common.Entities.Cliente { Id = 810374 }
            };
            string folio = null;
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => { return DateTime.Parse("2018-06-16"); };
                folio = _context.Return(request, 0, request.Sucursal);
            }
            Assert.IsNotNull(folio);
        }

        [TestMethod]
        public void ReturnMismaCorrida()
        {
            /*
('0000003391871', 'venta')
,('0000003391872', 'misma corrida')
,('0000003485731', 'dif corrida, mismo precio')
,('0000003556980', 'dif corrida, precio mayor')
             */

            var model = new Common.Entities.SaleRequest()
            {
                Sucursal = "01",
                VendedorId = 132
            };
            model.Productos = new Common.Entities.SerieFormasPago[] {
                new Common.Entities.SerieFormasPago { Serie = "0000003391871", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF }}                
            };
            model.Pagos = new Common.Entities.Pago[] {
                new Common.Entities.Pago { FormaPago = Common.Constants.FormaPago.EF, Importe = 329 }
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
                    new Common.Entities.ChangeItem { OldItem = "0000003391871", NewItem = "0000003391872" }
                }
            };
            _context.RequestProducto("0000003391872", 0);
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
        public void ReturnDifCorridaMismoPrecio()
        {
            /*
('0000003391871', 'venta')
,('0000003391872', 'misma corrida')
,('0000003485731', 'dif corrida, mismo precio')
,('0000003556980', 'dif corrida, precio mayor')
 */

            var ctx = new DataAccess.SirCoPVDataContext();
            var sctx = new DataAccess.SirCoDataContext();

            var cor = sctx.Corridas.Where(i => i.marca == "KLI" && i.estilon == "      1" && i.corrida == "D").Single();
            cor.precio = 339;
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
                new Common.Entities.Pago { FormaPago = Common.Constants.FormaPago.EF, Importe = 339 }
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
                    new Common.Entities.ChangeItem { OldItem = "0000003391871", NewItem = "0000003485731" }
                }
            };
            _context.RequestProducto("0000003485731", 0);
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
        public void ReturnDifCorridaDifPrecioPago()
        {
            /*
('0000003391871', 'venta')
,('0000003391872', 'misma corrida')
,('0000003485731', 'dif corrida, mismo precio')
,('0000003556980', 'dif corrida, precio mayor')
             */

            var sctx = new DataAccess.SirCoDataContext();
            sctx.Database.ExecuteSqlCommand("update serie set status = 'AC' where serie = @serie", 
                new System.Data.SqlClient.SqlParameter("@serie", "0000003556980"));            

            var model = new Common.Entities.SaleRequest()
            {
                Sucursal = "01",
                VendedorId = 132
            };
            model.Productos = new Common.Entities.SerieFormasPago[] {
                new Common.Entities.SerieFormasPago { Serie = "0000003391871", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF }}
            };
            model.Pagos = new Common.Entities.Pago[] {
                new Common.Entities.Pago { FormaPago = Common.Constants.FormaPago.EF, Importe = 329 }
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
                    new Common.Entities.ChangeItem { OldItem = "0000003391871", NewItem = "0000003556980" }
                },
                Pagos = new Common.Entities.Pago[] {
                    new Common.Entities.Pago { FormaPago = Common.Constants.FormaPago.EF, Importe = 30 }
                }
            };
            _context.RequestProducto("0000003556980", 0); 
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
        public void ReturnDifCorridaDifPrecioDevolucion()
        {
            /*
('0000003391871', 'venta')
,('0000003391872', 'misma corrida')
,('0000003485731', 'dif corrida, mismo precio')
,('0000003556980', 'dif corrida, precio mayor')
             */             
            var sctx = new DataAccess.SirCoDataContext();
            sctx.Database.ExecuteSqlCommand("update serie set status = 'AC' where serie = @serie",
                new System.Data.SqlClient.SqlParameter("@serie", "0000003556980"));

            var model = new Common.Entities.SaleRequest()
            {
                Sucursal = "01",
                VendedorId = 132
            };
            model.Productos = new Common.Entities.SerieFormasPago[] {
                new Common.Entities.SerieFormasPago { Serie = "0000003556980", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF }}
            };
            model.Pagos = new Common.Entities.Pago[] {
                new Common.Entities.Pago { FormaPago = Common.Constants.FormaPago.EF, Importe = 359 }
            };

            _context.RequestProducto("0000003556980", 0);

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
                    new Common.Entities.ChangeItem { OldItem = "0000003556980", NewItem = "0000003391871" }
                }
            };
            _context.RequestProducto("0000003391871", 0);
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
            Assert.AreEqual(30, dev.disponible);
        }
    }
}
