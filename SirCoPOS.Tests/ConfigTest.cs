using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SirCoPOS.Tests
{
    [TestClass]
    public class ConfigTest
    {
        [AssemblyInitialize]
        public static void AssemblySetup(TestContext testContext)
        {
            Helpers.TestHelper.Clean("SirCo");
            Helpers.TestHelper.Clean("SirCoPV");
            Helpers.TestHelper.Clean("SirCoControl");
            Helpers.TestHelper.Clean("SirCoNomina");
            Helpers.TestHelper.Clean("SirCoCredito");
            Helpers.TestHelper.Clean("SirCoAPP");
            Helpers.TestHelper.Clean("SirCoPOS");

            Helpers.TestHelper.RunScript("SirCo", @"TestData\SirCo.sql");
            Helpers.TestHelper.RunScript("SirCoControl", @"TestData\SirCoControl.sql");
            Helpers.TestHelper.RunScript("SirCoNomina", @"TestData\SirCoNomina.sql");
            Helpers.TestHelper.RunScript("SirCoPV", @"TestData\SirCoPV.sql");
            Helpers.TestHelper.RunScript("SirCoCredito", @"TestData\SirCoCredito-Pre.sql");
            Helpers.TestHelper.RunScript("SirCoCredito", @"TestData\SirCoCredito.sql");
            Helpers.TestHelper.RunScript("SirCoCredito", @"TestData\SirCoCredito-Post.sql");
            Helpers.TestHelper.RunScript("SirCoAPP", @"TestData\SirCoAPP.sql");
            Helpers.TestHelper.RunScript("SirCoPOS", @"TestData\SirCoPOS.sql");
        }

        public static object Sync;
        static ConfigTest()
        {
            Sync = new object();
        }
    }
}
