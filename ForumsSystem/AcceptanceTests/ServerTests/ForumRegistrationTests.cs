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


        // test success scenario for registration with valid info
        [TestMethod]
        public void TestForumRegistrationSuccess()
        {
            string forumName = "forum1";
            string forumProperties = "";
            string username = "user1";
            string pass = "passwd";
            string email = "user1@gmail.com";

            base.CreateForum(forumName, forumProperties);

            bool res = bridge.RegisterToForum(forumName, username, pass, email);

            Assert.IsTrue(res);
            // make sure user is registered
            Assert.IsTrue(bridge.IsRegisteredToForum(username, forumName));

            // cleanup
            base.DeleteForum(forumName);
        }

        // test failure scenario for registration with invalid info: forum doesn't exist
        [TestMethod]
        public void TestForumRegistrationBadForumName()
        {
            string forumName = "forum1";
            string username = "user1";
            string pass = "passwd";
            string email = "user1@gmail.com";

            // make sure forum doesn't exist
            base.DeleteForum(forumName);

            bool res = bridge.RegisterToForum(forumName, username, pass, email);
            Assert.IsTrue(!res);

        }

        

        //TODO: make tests for failure: bad username, bad pass

    }
}
