using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AcceptanceTests.ServerTests
{
    [TestClass]
    public class SuperAdminLoginTests : UseCaseTestSuite
    {
        public SuperAdminLoginTests()
            : base()
        {

        }
        /*
        // test - the success scenario where the user is registered to a forum in the system
        [TestMethod]
        public void TestSuperAdminLoginSuccess()
        {
            // initialize system
            bool res = bridge.LoginSuperAdmin(username, pass);
            Assert.IsTrue(res);

        }

        // test - the failure scenario where the user is not registered to a forum in the system
        [TestMethod]
        public void TestSuperAdminLoginNotRegistered()
        {
            //TODO: make the test use a badd username for super admin
            string forumName = "forum1";
            string forumProperties = "";
            string username = "user1", pass = "passwd", email = "user1@gmail.com";

            base.CreateForum(forumName, forumProperties);
            // user is not registered
            bool res = bridge.LoginSuperAdmin(forumName, username, pass);
            Assert.IsTrue(!res);

        }

        */
    }
}
