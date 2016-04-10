using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AcceptanceTests.ServerTests
{
    // all tests in this class assume that system is already initialized in base constructor
    [TestClass]
    public class SuperAdminLoginTests : UseCaseTestSuite
    {
        public SuperAdminLoginTests()
            : base()
        {

        }

        // test - the success scenario where username,pass are the correct login info
        // for the super admin.
        [TestMethod]
        public void TestSuperAdminLoginSuccess()
        {
            bool res = bridge.LoginSuperAdmin(this.superAdminUsername, this.superAdminPass);
            Assert.IsTrue(res);

        }

        // test - the failure scenario, try to login to super admin with bad username
        [TestMethod]
        public void TestSuperAdminLoginBadUsername()
        {
            string badUsername = "notsuperadmin";
            bool res = bridge.LoginSuperAdmin(badUsername, this.superAdminPass);
            Assert.IsTrue(!res);

        }

        // test - the failure scenario, try to login to super admin with bad password
        [TestMethod]
        public void TestSuperAdminLoginBadPass()
        {
            string badPass = "badpass";
            bool res = bridge.LoginSuperAdmin(this.superAdminUsername, badPass);
            Assert.IsTrue(!res);

        }


    }
}
