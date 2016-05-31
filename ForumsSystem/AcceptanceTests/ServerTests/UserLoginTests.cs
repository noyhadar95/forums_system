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
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username = "user1", pass = "passwd", email = "user1@gmail.com";
            DateTime dateOfBirth = new DateTime(1995, 8, 2);

            base.CreateForum(forumName, forumPolicy);
            bridge.RegisterToForum(forumName, username, pass, email, dateOfBirth);
            bool res = bridge.LoginUser(forumName, username, pass);
            Assert.IsTrue(res);

            // cleanup
                base.Cleanup(forumName);
        }

        // test - the failure scenario where the user is not registered to a forum in the system
        [TestMethod]
        public void TestUserLoginNotRegistered()
        {
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username = "user1", pass = "passwd", email = "user1@gmail.com";

            base.CreateForum(forumName, forumPolicy);
            // user is not registered
            bool res = bridge.LoginUser(forumName, username, pass);
            Assert.IsTrue(!res);

            // cleanup
                base.Cleanup(forumName);
        }


        // test - the success scenario where the user is registered to a forum in the system 
        [TestMethod]
        public void TestUserLoginWithClientSessionSuccess()
        {
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username = "user1", pass = "passwd", email = "user1@gmail.com";
            DateTime dateOfBirth = new DateTime(1995, 8, 2);

            base.CreateForum(forumName, forumPolicy);
            bridge.RegisterToForum(forumName, username, pass, email, dateOfBirth);
            bool res = bridge.LoginUser(forumName, username, pass);
            
            Assert.IsTrue(res);

            string clientServer = bridge.getUserClientServer(forumName, username);
            res = bridge.LoginUserWithClientServer(forumName, username, pass, clientServer);

            Assert.IsTrue(res);
            // cleanup
        }

        // test - the success scenario where the user is registered to a forum in the system but 
        [TestMethod]
        public void TestUserLoginWithWrongClientSessionFail()
        {
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username = "user1", pass = "passwd", email = "user1@gmail.com";
            DateTime dateOfBirth = new DateTime(1995, 8, 2);

            base.CreateForum(forumName, forumPolicy);
            bridge.RegisterToForum(forumName, username, pass, email, dateOfBirth);
            bool res = bridge.LoginUser(forumName, username, pass);

            Assert.IsTrue(res);

            string clientServer = bridge.getUserClientServer(forumName, username);
            res = bridge.LoginUserWithClientServer(forumName, username, pass, clientServer +"bad");

            Assert.IsFalse(res);
            // cleanup
            base.Cleanup(forumName);
        }

        // test - the success scenario where the user is registered to a forum in the system but 
        [TestMethod]
        public void TestUserLoginWithoutClientSessionFail()
        {
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username = "user1", pass = "passwd", email = "user1@gmail.com";
            DateTime dateOfBirth = new DateTime(1995, 8, 2);

            base.CreateForum(forumName, forumPolicy);
            bridge.RegisterToForum(forumName, username, pass, email, dateOfBirth);
            bool res = bridge.LoginUser(forumName, username, pass);

            Assert.IsTrue(res);

            res = bridge.LoginUser(forumName, username, pass);

            Assert.IsFalse(res);
            // cleanup
            base.Cleanup(forumName);
        }





    }
}
