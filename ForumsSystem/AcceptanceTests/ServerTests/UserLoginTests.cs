using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcceptanceTestsBridge;

namespace AcceptanceTests.ServerTests
{
    [TestClass]
    public class UserLoginTests : UseCaseTestSuite
    {
        public UserLoginTests()
            : base()
        {

        }

        // test - the success scenario where the user is registered to a forum in the system
        [TestMethod]
        public void TestUserLoginSuccess()
        {
            string forumName = "forum1";
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username = "user1", pass = "passwd", email = "user1@gmail.com";
            DateTime dateOfBirth = new DateTime(1995, 8, 2);

            base.CreateForum(forumName, forumPolicy);
            bridge.RegisterToForum(forumName, username, pass, email, dateOfBirth);
            bool res = bridge.LoginUser(forumName, username, pass);
            Assert.IsTrue(res);

        }

        // test - the failure scenario where the user is not registered to a forum in the system
        [TestMethod]
        public void TestUserLoginNotRegistered()
        {
            string forumName = "forum1";
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username = "user1", pass = "passwd", email = "user1@gmail.com";

            base.CreateForum(forumName, forumPolicy);
            // user is not registered
            bool res = bridge.LoginUser(forumName, username, pass);
            Assert.IsTrue(!res);

        }


    }
}
