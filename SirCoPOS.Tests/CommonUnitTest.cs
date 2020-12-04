using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading;

namespace SirCoPOS.Tests
{
    [TestClass]
    public class CommonUnitTest
    {
        public TestContext TestContext { get; set; }
        public CommonUnitTest()
        {
            _helpers = new BusinessLogic.Data();
            _sale = new BusinessLogic.Sale();
            _process = new BusinessLogic.Process();
        }

        private BusinessLogic.Data _helpers;
        private BusinessLogic.Sale _sale;
        private BusinessLogic.Process _process;
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
        public void LoginTest()
        {
            var item = _helpers.Login(user: "956", pass: "01YJC1", sucursal: "01");
            Assert.IsNotNull(item);
        }

        [TestMethod]
        public void AddClienteTest()
        {
            var model = new Common.Entities.Cliente
            {
                Nombre = "NOELIA",
                ApPaterno = "ORTIZ",
                //ApMaterno = "ARELLANO",
                Sexo = "F",
                Colonia = 10169,
                Ciudad = 61,
                Estado = 6,
                CodigoPostal = "27085"
            };

            var id = _process.AddCliente(model);
            Assert.IsNotNull(id);
        }

        [TestMethod]
        public void FindColoniaTest()
        {
            var colonias = _helpers.FindColonias("27085");
            Assert.IsNotNull(colonias);
            Assert.AreEqual(colonias.Count(), 6);
        }        
    }
}
