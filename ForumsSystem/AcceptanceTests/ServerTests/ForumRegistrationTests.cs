using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcceptanceTestsBridge;

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
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username = "user1";
            string pass = "passwd";
            string email = "user1@gmail.com";
            DateTime dateOfBirth = DateTime.Now;

            base.CreateForum(forumName, forumPolicy);

            bool res = bridge.RegisterToForum(forumName, username, pass, email, dateOfBirth);

            Assert.IsTrue(res);
            // make sure user is registered
            Assert.IsTrue(bridge.IsRegisteredToForum(username, forumName));

            // cleanup
            base.Cleanup(forumName);
        }

        // test failure scenario for registration with invalid info: forum doesn't exist
        [TestMethod]
        public void TestForumRegistrationBadForumName()
        {
            string forumName = GetNextForum();
            string username = "user1";
            string pass = "passwd";
            string email = "user1@gmail.com";
            DateTime dateOfBirth = DateTime.Now;

            // make sure forum doesn't exist
            base.Cleanup(forumName);

            bool res = bridge.RegisterToForum(forumName, username, pass, email, dateOfBirth);
            Assert.IsTrue(!res);

        }


    }
}
