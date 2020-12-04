using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SirCoPOS.Client.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var pc = new Client.Models.ProductoCambio
            {
                OldItem = new Common.Entities.ProductoDevolucion 
                { 
                    Modelo = "CVR", 
                    Marca = "6",
                    Precio = 1099m,
                    Pago = 769.3m
                },
                NewItem = new Client.Models.Producto 
                {
                    Modelo = "CTA",
                    Marca = "774",
                    Precio = 629m
                }
            };

            Assert.AreEqual(.3m, pc.OldItem.DescuentoPorcentaje);
            Assert.AreEqual(-140.3m, pc.Saldo);
            Assert.AreEqual(0, pc.DescuentoPorcentaje);
        }
        [TestMethod]
        public void CambioPromoDifPrecio()
        {
            var pc = new Client.Models.ProductoCambio
            {
                OldItem = new Common.Entities.ProductoDevolucion
                {
                    Modelo = "ADD",
                    Marca = "1226",
                    Corrida = "A",
                    Precio = 1099m,
                    Pago = 549.5m
                },
                NewItem = new Client.Models.Producto
                {
                    Modelo = "ADD",
                    Marca = "1226",
                    Corrida = "A",
                    Precio = 1080m
                }
            };

            Assert.AreEqual(.5m, pc.OldItem.DescuentoPorcentaje);
            //Assert.AreEqual(-9.5m, pc.Saldo);
            Assert.AreEqual(0m, pc.Saldo);

            //Assert.AreEqual(.5m, pc.DescuentoPorcentaje);
            //Assert.AreEqual(549.5m, pc.Usable);
            //Assert.AreEqual(540m, pc.PorPagar);

            Assert.IsNull(pc.DescuentoPorcentaje);
            Assert.IsNull(pc.Usable);
            Assert.IsNull(pc.PorPagar);

            var vm = new Client.ViewModels.Tabs.CambioViewModel();
            vm.Productos.Add(pc);

            //Assert.AreEqual(-9.5m, vm.Devolucion);
            Assert.AreEqual(0, vm.Devolucion ?? 0);
            Assert.AreEqual(0, vm.Pagar ?? 0);
        }
    }
}
