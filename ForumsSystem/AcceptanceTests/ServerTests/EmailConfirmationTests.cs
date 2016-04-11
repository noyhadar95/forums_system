using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AcceptanceTests.ServerTests
{
    [TestClass]
    public class EmailConfirmationTests : UseCaseTestSuite
    {
        public EmailConfirmationTests()
            : base()
        {

        }

        // test - check that email confirmation succeeds when the forum is defined "secured forum".
        [TestMethod]
        public void TestEmailConfirmationSecureForum()
        {
            string forumName = "forum1";
            string forumProperties = "";
            string username = "user1";
            string pass = "passwd";
            string email = "user1@gmail.com";
            DateTime dateOfBirth = DateTime.Now;

            // make sure the forum is defined as "secured forum".
            base.CreateForum(forumName, forumProperties);
            bool res = bridge.RegisterToForum(forumName, username, pass, email, dateOfBirth);
            Assert.IsTrue(res);

            // check that the user is not yet considered registered to the forum
            Assert.IsTrue(!bridge.IsRegisteredToForum(username, forumName));
            bridge.ConfirmRegistration(forumName, username);
            // check that the user is now registered to the forum
            Assert.IsTrue(bridge.IsRegisteredToForum(username, forumName));

            // cleanup
            base.DeleteForum(forumName);
        }

        // test - check that email confirmation fails with username that didnt ask to 
        // register to the forum (the forum is defined "secured forum")
        [TestMethod]
        public void TestEmailConfirmationBadUsername()
        {
            string forumName = "forum1";
            string forumProperties = "";
            string username = "user1", badUsername = "fakeuser";
            string pass = "passwd";
            string email = "user1@gmail.com";
            DateTime dateOfBirth = DateTime.Now;

            // make sure the forum is defined as "secured forum".
            base.CreateForum(forumName, forumProperties);
            bool res = bridge.RegisterToForum(forumName, username, pass, email, dateOfBirth);
            Assert.IsTrue(res);

            // check that the user is not yet considered registered to the forum
            Assert.IsTrue(!bridge.IsRegisteredToForum(username, forumName));
            bridge.ConfirmRegistration(forumName, badUsername);

            // check that the user is stil not registered to the forum, and that bad user is not registered too
            Assert.IsTrue(!bridge.IsRegisteredToForum(username, forumName));
            Assert.IsTrue(!bridge.IsRegisteredToForum(badUsername, forumName));

            // cleanup
            base.DeleteForum(forumName);
        }




    }
}
