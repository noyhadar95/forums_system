using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            string forumProperties = "";
            string username = "user1", pass = "passwd", email = "user1@gmail.com";

            base.CreateForum(forumName, forumProperties);
            bridge.RegisterToForum(forumName, username, pass, email);
            bool res = bridge.LoginUser(forumName, username, pass);
            Assert.IsTrue(res);

        }

        // test - the failure scenario where the user is not registered to a forum in the system
        [TestMethod]
        public void TestUserLoginNotRegistered()
        {
            string forumName = "forum1";
            string forumProperties = "";
            string username = "user1", pass = "passwd", email = "user1@gmail.com";

            base.CreateForum(forumName, forumProperties);
            // user is not registered
            bool res = bridge.LoginUser(forumName, username, pass);
            Assert.IsTrue(!res);

        }


    }
}
