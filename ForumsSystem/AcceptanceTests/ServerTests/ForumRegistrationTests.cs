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

            string forumName = "";
            string username = "";
            string pass = "";
            string email = "";

            //TODO: make sure user is not already registered
            bool res = bridge.RegisterToForum(forumName, username, pass, email);

            Assert.IsTrue(res);
            //TODO: make sure user is registered

        }

        [TestMethod]
        public void TestForumRegistrationFailure()
        {
            // test failure scenario for registration with invalid info

            string forumName = "";
            string username = "";
            string pass = "";
            string email = "";

            //TODO: make sure user is not already registered
            bool res = bridge.RegisterToForum(forumName, username, pass, email);

            Assert.IsTrue(!res);
            //TODO: make sure user is registered

        }

        //TODO: make tests for failure: bad forum, bad username, bad pass, bad email

    }
}
