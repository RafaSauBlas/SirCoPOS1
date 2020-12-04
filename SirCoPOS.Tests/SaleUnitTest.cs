using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Microsoft.QualityTools.Testing.Fakes;
using Moq;
using System.Data.Entity;
using SirCoPOS.Common.Constants;
using System.Threading;
using SirCoPOS.Common.Entities;

namespace SirCoPOS.Tests
{
    [TestClass]
    public class SaleUnitTest
    {
        public TestContext TestContext { get; set; }
        public SaleUnitTest()
        {
            //_context = new Services.DataService();
            _process = new BusinessLogic.Process();
            _sale = new BusinessLogic.Sale();
        }

        //private Common.ServiceContracts.IDataService _context;
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
        [ExpectedException(typeof(NotSupportedException))]
        public void SaleTest()
        {
            //Isolate.WhenCalled(() => DateTime.Now).WillReturn(new DateTime(2000, 1, 1));

            //Smock.Run(context =>
            //{
            //    context.Setup(() => DateTime.Now).Returns(DateTime.Parse("2019-03-06"));                
            //});            

            //var a = DateTime.Now;
            var model = new Common.Entities.SaleRequest()
            {
                Sucursal = "01",
                VendedorId = 132
            };
            model.Productos = new Common.Entities.SerieFormasPago[] {
                new Common.Entities.SerieFormasPago { Serie = "0000003296973", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF }},
                new Common.Entities.SerieFormasPago { Serie = "0000003410054", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF }},
                new Common.Entities.SerieFormasPago { Serie = "0000003458525", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF }},
                new Common.Entities.SerieFormasPago { Serie = "0000003459970", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF }},
                new Common.Entities.SerieFormasPago { Serie = "0000003461664", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF }},
            };
            
            //var dateTimeShim = Shim.Replace(() => DateTime.Now).With(() => DateTime.Parse("2019-03-06"));
            //PoseContext.Isolate(() =>
            //{
            //    var aa = DateTime.Now;


            //}, dateTimeShim);
            Common.Entities.SaleResponse folio = null;
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => { return DateTime.Parse("2019-03-06"); };
                folio = _process.Sale(model, 0);
            }

            var sale = _sale.FindSale(model.Sucursal, folio.Folio);
            Assert.IsNotNull(sale);
        }

        //http://vaideeswaranr.blogspot.com/2013/02/using-microsoft-fakes-to-unit-test.html
        //https://entityframework.net/knowledge-base/37768386/shim-dbcontext-ctor-for-effort-unit-testing
        [TestMethod]
        public void SalePagoTest()
        {
            //https://docs.microsoft.com/en-us/ef/ef6/fundamentals/testing/mocking
            //sucursal = '01' and venta = '434045'
            var model = new Common.Entities.SaleRequest()
            {
                Sucursal = "01",
                VendedorId = 0
            };
            model.Productos = new Common.Entities.SerieFormasPago[] {
                new Common.Entities.SerieFormasPago { Serie = "0000003668367", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } }
            };
            model.Pagos = new List<Common.Entities.Pago> {
                new Common.Entities.Pago { FormaPago = FormaPago.EF, Importe = 149 }
            };

            var ctx = new DataAccess.SirCoDataContext();
            ctx.UpdateSerieStatus("0000003668367", Status.CA, Status.AC, 0);

            Common.Entities.SaleResponse folio = null;
            using (ShimsContext.Create())
            {
                var data = new List<DataAccess.SirCoControl.Sucursal> {
                    new DataAccess.SirCoControl.Sucursal { sucursal = "01", cajas = 434045 }
                }.AsQueryable();

                var mockSet = new Mock<DbSet<DataAccess.SirCoControl.Sucursal>>();
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

                DataAccess.Fakes.ShimSirCoControlDataContext.AllInstances.SucursalesGet = (sctx) => {
                    return mockSet.Object;
                };

                System.Fakes.ShimDateTime.NowGet = () => { return DateTime.Parse("2019-07-04"); };
                folio = _process.Sale(model, 0);
            }
            Assert.AreEqual("434046", folio.Folio);
            var sale = _sale.FindSale(model.Sucursal, folio.Folio);
            Assert.IsNotNull(sale);
        }

        [TestMethod]
        public void SalePagoCancelacionTest()
        {
            //sucursal = '01' and venta = '434045'
            var model = new Common.Entities.SaleRequest()
            {
                Sucursal = "01",
                VendedorId = 0
            };
            model.Productos = new Common.Entities.SerieFormasPago[] {
                new Common.Entities.SerieFormasPago { Serie = "0000003668367", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.EF } }
            };
            model.Pagos = new List<Common.Entities.Pago> {
                new Common.Entities.Pago { FormaPago = FormaPago.EF, Importe = 149 }
            };

            var ctx = new DataAccess.SirCoDataContext();
            ctx.UpdateSerieStatus("0000003668367", Status.CA, Status.AC, 0);

            var data = new List<DataAccess.SirCoControl.Sucursal> {
                    new DataAccess.SirCoControl.Sucursal { sucursal = "01", cajas = 434045 }
                }.AsQueryable();

            var mockSet = new Mock<DbSet<DataAccess.SirCoControl.Sucursal>>();
            mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());


            Common.Entities.SaleResponse folio = null;
            using (ShimsContext.Create())
            {                
                DataAccess.Fakes.ShimSirCoControlDataContext.AllInstances.SucursalesGet = (sctx) => {
                    return mockSet.Object;
                };

                var now = DateTime.Parse("2019-07-04T10:10:00");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.AddHours(5);
                System.Fakes.ShimDateTime.NowGet = () => { return now; };
                System.Fakes.ShimDateTime.TodayGet = () => { return DateTime.Parse("2019-07-04"); };
                folio = _process.Sale(model, 0);
            }
            Assert.AreEqual("434046", folio.Folio);
            var sale = _sale.FindSale(model.Sucursal, folio.Folio);
            Assert.IsNotNull(sale);

            var cmodel = new CancelSaleRequest
            {
                Folio = "434046",
                Sucursal = "01"
            };

            using (ShimsContext.Create())
            {
                DataAccess.Fakes.ShimSirCoControlDataContext.AllInstances.SucursalesGet = (sctx) => {
                    return mockSet.Object;
                };

                var now = DateTime.Parse("2019-07-04T10:10:00");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.AddHours(5);
                System.Fakes.ShimDateTime.NowGet = () => { return now; };
                System.Fakes.ShimDateTime.TodayGet = () => { return DateTime.Parse("2019-07-04"); };

                _process.CancelSale(cmodel, 0);
            }

            var serie = ctx.Series.Where(i => i.serie == "0000003668367").Single();
            Assert.AreEqual("AB", serie.status);
        }

        [TestMethod]
        public void VentaConValeNuevoCliente()
        {
            var sucursal = "01";
            var sucursalId = 1;
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
                var nvale = "110472";
                var prod = _sale.ScanProducto(pserie, sucursal);
                Assert.IsNotNull(prod);
                //var vale = _context.CheckVale(nvale);

                var ctx = new DataAccess.SirCoDataContext();
                ctx.UpdateSerieStatus("0000003668367", Status.CA, Status.AC, 0);

                var ncliente = new Common.Entities.Cliente
                {
                    SucursalId = sucursalId,
                    Nombre = "NOELIA",
                    ApPaterno = "ORTIZ",
                    //ApMaterno = "ARELLANO",
                    Sexo = "F",
                    Colonia = 10169,
                    Ciudad = 61,
                    Estado = 6,
                    CodigoPostal = "27085"
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
                        FormaPago = FormaPago.VA,
                        Importe = prod.Producto.Precio.Value,
                        Vale = nvale,
                        Plazos = 13,
                        FechaAplicar = DateTime.Parse("2019-11-05")                       
                        //ClienteId = cid,                        
                    }
                };

                var folio = _process.Sale(model, 0);

                Assert.AreEqual("434046", folio.Folio);
            }            
        }

        [TestMethod]
        public void VentaConMultipleValeNuevoCliente()
        {
            var sucursal = "01";
            var sucursalId = 1;
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
                    SucursalId = sucursalId,
                    Nombre = "NOELIA",
                    ApPaterno = "ORTIZ",
                    //ApMaterno = "ARELLANO",
                    Sexo = "F",
                    Colonia = 10169,
                    Ciudad = 61,
                    Estado = 6,
                    CodigoPostal = "27085"
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
                        FormaPago = FormaPago.VA,
                        Importe = 100,
                        Vale = "110472",
                        Plazos = 13,
                        FechaAplicar = DateTime.Parse("2019-11-05")                       
                        //ClienteId = cid,                        
                    },
                    new Common.Entities.Pago {
                        FormaPago = FormaPago.VA,
                        Importe = 49,
                        Vale = "310752",
                        Plazos = 9,
                        FechaAplicar = DateTime.Parse("2019-10-20")                       
                        //ClienteId = cid,                        
                    }
                };

                var folio = _process.Sale(model, 0);

                Assert.AreEqual("434046", folio.Folio);
            }
        }

        [TestMethod]
        public void VentaConValeClienteExistente()
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
                var nvale = "110472";
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
                        FormaPago = FormaPago.VA,
                        Importe = prod.Producto.Precio.Value,
                        Vale = nvale,
                        Plazos = 13,
                        FechaAplicar = DateTime.Parse("2019-11-05")
                        //ClienteId = cid,                        
                    }
                };

                var folio = _process.Sale(model, 0);

                Assert.AreEqual("434046", folio.Folio);
            }
        }
        [TestMethod]
        public void VentaConValeClienteExistenteNoElectronica()
        {
            /*
             * 0000003632625	1199.00	ADD	   1195
            1	NULL	0000003668367	AC	149.00	ARA	    174	13	A	12	16	218
            2	NULL	0000003736086	AC	13470.00	DLT	    335	01	A	01	01	391
            */

            var ctxc = new DataAccess.SirCoCreditoDataContext();
            var dis = ctxc.Distribuidores.Where(i => i.iddistrib == 4281).Single();
            dis.solocalzado = 1;
            ctxc.SaveChanges();

            var sucursal = "01";
            using (ShimsContext.Create())
            {
                var now = DateTime.Parse("2019-09-01");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.ToUniversalTime();

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

                var pserie = "0000003632625";
                var pseriePlazos = "0000003736086";
                var nvale = "110472";
                var prod = _sale.ScanProducto(pserie, sucursal);
                Assert.IsNotNull(prod);
                //var vale = _context.CheckVale(nvale);

                var ctx = new DataAccess.SirCoDataContext();
                ctx.UpdateSerieStatus("0000003632625", Status.CA, Status.AC, 0);
                ctx.UpdateSerieStatus("0000003736086", Status.CA, Status.AC, 0);

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
                    new Common.Entities.SerieFormasPago { Serie = pseriePlazos, FormasPago = new Common.Constants.FormaPago[] { FormaPago.EF, FormaPago.TC } },
                    new Common.Entities.SerieFormasPago { Serie = pserie, FormasPago = new Common.Constants.FormaPago[] { FormaPago.VA, FormaPago.TC } }              
                };
                model.Pagos = new List<Common.Entities.Pago> {
                    new Pago {
                        FormaPago = FormaPago.EF,
                        Importe = 10000
                    },
                    new Common.Entities.Pago {
                        FormaPago = FormaPago.VA,
                        Importe = 1000,
                        Vale = nvale,
                        Plazos = 5,
                        FechaAplicar = DateTime.Parse("2019-10-20"),
                        //ClienteId = cid,                        
                        //ProductosPlazos = new ProductoPlazo[] { }
                    },
                    new Pago {
                        FormaPago = FormaPago.TC,
                        Importe = 3669
                    },
                };

                var folio = _process.Sale(model, 0);


                Assert.AreEqual("434046", folio.Folio);

                //var ctxc = new DataAccess.SirCoCreditoDataContext();
                var pp = ctxc.PlanPagos.Where(i => i.sucursal == sucursal && i.nota == folio.Folio).Single();
                var detalle = pp.Detalle.OrderBy(i => i.pago).ToArray();
                Assert.AreEqual(5, detalle.Count());
                Assert.AreEqual(200 + 10, detalle[0].importe);
                Assert.AreEqual(200, detalle[1].importe);
                Assert.AreEqual(200, detalle[2].importe);//2019-10-17
                Assert.AreEqual(200, detalle[3].importe);
                Assert.AreEqual(200, detalle[4].importe);                
            }
        }
        [TestMethod]
        public void VentaConValeClienteExistenteMultiplePlazos()
        {
            /*
            1	NULL	0000003668367	AC	149.00	ARA	    174	13	A	12	16	218
            2	NULL	0000003736086	AC	13470.00	DLT	    335	01	A	01	01	391
            */

            var sucursal = "01";
            using (ShimsContext.Create())
            {
                var now = DateTime.Parse("2019-09-01");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.ToUniversalTime();

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
                var pseriePlazos = "0000003736086";
                var nvale = "110472";
                var prod = _sale.ScanProducto(pserie, sucursal);
                Assert.IsNotNull(prod);
                //var vale = _context.CheckVale(nvale);

                var ctx = new DataAccess.SirCoDataContext();
                ctx.UpdateSerieStatus("0000003668367", Status.CA, Status.AC, 0);
                ctx.UpdateSerieStatus("0000003736086", Status.CA, Status.AC, 0);

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
                    new Common.Entities.SerieFormasPago { Serie = pserie, FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.VA } },
                    new Common.Entities.SerieFormasPago { Serie = pseriePlazos, FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.VA } }
                };
                model.Pagos = new List<Common.Entities.Pago> {
                    new Common.Entities.Pago {
                        FormaPago = FormaPago.VA,
                        Importe = 13619,
                        Vale = nvale,
                        Plazos = 4,
                        FechaAplicar = DateTime.Parse("2019-10-20"),
                        //ClienteId = cid,                        
                        ProductosPlazos = new ProductoPlazo[] {
                            new ProductoPlazo {
                                Serie = pseriePlazos,
                                Plazos = 9,
                                Importe = 13470
                            }
                        }
                    }
                };

                var folio = _process.Sale(model, 0);


                Assert.AreEqual("434046", folio.Folio);

                var ctxc = new DataAccess.SirCoCreditoDataContext();
                var pp = ctxc.PlanPagos.Where(i => i.sucursal == sucursal && i.nota == folio.Folio).Single();
                var detalle = pp.Detalle.OrderBy(i => i.pago).ToArray();
                Assert.AreEqual(9, detalle.Count());
                Assert.AreEqual(1497 + 10, detalle[0].importe);
                Assert.AreEqual(1497, detalle[1].importe);
                Assert.AreEqual(38 + 1497, detalle[2].importe);//2019-10-17
                Assert.AreEqual(38 + 1497, detalle[3].importe);
                Assert.AreEqual(38 + 1497, detalle[4].importe);
                Assert.AreEqual(35 + 1497, detalle[5].importe);
                Assert.AreEqual(1497, detalle[6].importe);
                Assert.AreEqual(1497, detalle[7].importe);
                Assert.AreEqual(1494, detalle[8].importe);                
            }
        }
        [TestMethod]
        public void VentaConValeClienteExistenteMultiplePlazosSinCalzado()
        {
            var sucursal = "01";
            using (ShimsContext.Create())
            {
                var now = DateTime.Parse("2019-09-01");
                System.Fakes.ShimDateTime.UtcNowGet = () => now.ToUniversalTime();

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
                var pseriePlazos = "0000003736086";
                var nvale = "110472";
                var prod = _sale.ScanProducto(pserie, sucursal);
                Assert.IsNotNull(prod);
                //var vale = _context.CheckVale(nvale);

                var ctx = new DataAccess.SirCoDataContext();
                ctx.UpdateSerieStatus("0000003668367", Status.CA, Status.AC, 0);
                ctx.UpdateSerieStatus("0000003736086", Status.CA, Status.AC, 0);

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
                    //new Common.Entities.SerieFormasPago { Serie = pserie, FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.VA } },
                    new Common.Entities.SerieFormasPago { Serie = pseriePlazos, FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.VA } }
                };
                model.Pagos = new List<Common.Entities.Pago> {
                    new Common.Entities.Pago {
                        FormaPago = FormaPago.VA,
                        Importe = 13470,
                        Vale = nvale,
                        Plazos = null,
                        FechaAplicar = null,
                        //ClienteId = cid,                        
                        ProductosPlazos = new ProductoPlazo[] {
                            new ProductoPlazo {
                                Serie = pseriePlazos,
                                Plazos = 9,
                                Importe = 13470
                            }
                        }
                    }
                };

                var folio = _process.Sale(model, 0);


                Assert.AreEqual("434046", folio.Folio);

                var ctxc = new DataAccess.SirCoCreditoDataContext();
                var pp = ctxc.PlanPagos.Where(i => i.sucursal == sucursal && i.nota == folio.Folio).Single();
                var detalle = pp.Detalle.OrderBy(i => i.pago).ToArray();
                Assert.AreEqual(9, detalle.Count());
                Assert.AreEqual(1497 + 10, detalle[0].importe);
                Assert.AreEqual(1497, detalle[1].importe);
                Assert.AreEqual(1497, detalle[3].importe);
                Assert.AreEqual(1497, detalle[6].importe);
                Assert.AreEqual(1497, detalle[7].importe);
                Assert.AreEqual(1494, detalle[8].importe);
            }
        }
        [TestMethod]
        public void VentaConMonedero()
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

                var folio = _process.Sale(model, 0);

                Assert.AreEqual("434046", folio.Folio);
            }

            var ctxa = new DataAccess.SirCoAPPDataContext();
            var dinero = ctxa.Dineros.Where(i => i.idsucursal == 1 && i.cliente == "144666").Single();
            var det = dinero.Detalles.OrderByDescending(i => i.vigencia).ToArray();

            Assert.AreEqual(151, dinero.saldo);
            Assert.AreEqual(0, det[0].saldo);
            Assert.AreEqual(51, det[1].saldo);
            Assert.AreEqual(100, det[2].saldo);
        }

        [TestMethod]
        public void VentaConValeDigital()
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

                var folio = _process.Sale(model, 0);

                Assert.AreEqual("434046", folio.Folio);
            }
        }
        [TestMethod]
        public void VentaContraVale()
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

                var folio = _process.Sale(model, 0);

                Assert.AreEqual("434046", folio.Folio);
            }
        }
        [TestMethod]
        public void VentaConValeClienteExistenteContraVale()
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
                var nvale = "110472";
                //var prod = _sale.ScanProducto(pserie, sucursal);
                //Assert.IsNotNull(prod);
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
                        FormaPago = FormaPago.VA,
                        Importe = 149,
                        Vale = nvale,
                        Plazos = 13,
                        FechaAplicar = DateTime.Parse("2019-11-05"),
                        //ClienteId = cid,
                        ContraVale = true,
                        Limite = 500
                    }
                };

                var folio = _process.Sale(model, 0);

                Assert.AreEqual("434046", folio.Folio);
                Assert.IsNotNull(folio.ContraVales);
                Assert.IsTrue(folio.ContraVales.Any());
                var item = folio.ContraVales.Single();
                //Assert.AreEqual("0000000001", item.ContraVale);
                Assert.AreEqual("000001", item.ContraVale);                
                Assert.AreEqual(351m, item.Importe);
            }
        }

        [TestMethod]
        public void SalePagoTarjetaTest()
        {
            //https://docs.microsoft.com/en-us/ef/ef6/fundamentals/testing/mocking
            //sucursal = '01' and venta = '434045'
            var model = new Common.Entities.SaleRequest()
            {
                Sucursal = "01",
                VendedorId = 0
            };
            model.Productos = new Common.Entities.SerieFormasPago[] {
                new Common.Entities.SerieFormasPago { Serie = "0000003668367", FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.TC } }
            };
            model.Pagos = new List<Common.Entities.Pago> {
                new Common.Entities.Pago {
                    FormaPago = FormaPago.TC,
                    Importe = 149,
                    Terminacion = "123",
                    Referencia = "456"
                }
            };

            var ctx = new DataAccess.SirCoDataContext();
            ctx.UpdateSerieStatus("0000003668367", Status.CA, Status.AC, 0);

            Common.Entities.SaleResponse folio = null;
            using (ShimsContext.Create())
            {
                var data = new List<DataAccess.SirCoControl.Sucursal> {
                    new DataAccess.SirCoControl.Sucursal { sucursal = "01", cajas = 434045 }
                }.AsQueryable();

                var mockSet = new Mock<DbSet<DataAccess.SirCoControl.Sucursal>>();
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<DataAccess.SirCoControl.Sucursal>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

                DataAccess.Fakes.ShimSirCoControlDataContext.AllInstances.SucursalesGet = (sctx) => {
                    return mockSet.Object;
                };

                System.Fakes.ShimDateTime.NowGet = () => { return DateTime.Parse("2019-07-04"); };
                folio = _process.Sale(model, 0);
            }
            Assert.AreEqual("434046", folio.Folio);
            var sale = _sale.FindSale(model.Sucursal, folio.Folio);
            Assert.IsNotNull(sale);

            var ctxpv = new DataAccess.SirCoPVDataContext();
            var item = ctxpv.PagosDetalle.Where(i => i.sucursal == "01" && i.pago == folio.Folio).Single();
            Assert.AreEqual("123", item.terminacion);
            Assert.AreEqual("456", item.transaccion);
        }

        [TestMethod]
        public void VentaCreditoNuevoCliente()
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

                var folio = _process.Sale(model, 0);

                Assert.AreEqual("434046", folio.Folio);
            }
        }

        [TestMethod]
        public void VentaCreditoDistribuidorNuevoCliente()
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

                var folio = _process.Sale(model, 0);

                Assert.AreEqual("434046", folio.Folio);
            }
        }
    }
}
