using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AcceptanceTests.ServerTests
{
    [TestClass]
    public class InitializeSystemTests : UseCaseTestSuite
    {
        public InitializeSystemTests()
            : base()
        {

        }

        // test - check that the initialization process succeed
        [TestMethod]
        public void TestInitializeSystem()
        {
            string username = "superadmin";
            string pass = "superadminpass";
            // initialize system
            bool res = bridge.InitializeSystem(username, pass);
            Assert.IsTrue(res);
        }

        // test - fail with empty username
        [TestMethod]
        public void TestInitializeSystemEmptyUsername()
        {
            string username = "";
            string pass = "superadminpass";
            // initialize system
            bool res = bridge.InitializeSystem(username, pass);
            Assert.IsTrue(!res);
        }

        // test - fail with empty password
        [TestMethod]
        public void TestInitializeSystemEmptyPassword()
        {
            string username = "superadmin";
            string pass = "";
            // initialize system
            bool res = bridge.InitializeSystem(username, pass);
            Assert.IsTrue(!res);
        }

    }
}
