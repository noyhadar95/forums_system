using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AcceptanceTests.ServerTests
{
    [TestClass]
    public class ForumRegistrationTests : UseCaseTestSuite
    {
        public ForumRegistrationTests()
            : base()
        {

        }

        [TestMethod]
        public void TestForumRegistrationSuccess()
        {
            // test success scenario for registration with valid info

            string forum = "";
            string username = "";
            string pass = "";
            string email = "";

            //TODO: make sure user is not already registered
            int res = bridge.RegisterToForum(forum, username, pass, email);

            Assert.IsTrue(res > 0);
            //TODO: make sure user is registered

        }

        [TestMethod]
        public void TestForumRegistrationFailure()
        {
            // test failure scenario for registration with invalid info

            string forum = "";
            string username = "";
            string pass = "";
            string email = "";

            //TODO: make sure user is not already registered
            int res = bridge.RegisterToForum(forum, username, pass, email);

            Assert.IsTrue(res < 0);
            //TODO: make sure user is registered

        }



    }
}
