using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcceptanceTestsBridge;

namespace AcceptanceTests.ServerTests
{
    [TestClass]
    public class PolicyTests : UseCaseTestSuite
    {
        [TestMethod]
        public void TestMinPassPolicySuccess()
        {
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username = "user1";
            string pass = "passwd";
            string email = "user1@gmail.com";
            DateTime dateOfBirth = DateTime.Now;

            int minPassLength = 5;

            base.CreateForum(forumName, forumPolicy,minPassLength,100);

            bool res = bridge.RegisterToForum(forumName, username, pass, email, dateOfBirth);

            Assert.IsTrue(res);
            // make sure user is registered
            Assert.IsTrue(bridge.IsRegisteredToForum(username, forumName));

            // cleanup
            base.Cleanup(forumName);
        }

        [TestMethod]
        public void TestMinPassPolicyFail()
        {
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username = "user1";
            string pass = "passwd";
            string email = "user1@gmail.com";
            DateTime dateOfBirth = DateTime.Now;

            int minPassLength = 8;

            base.CreateForum(forumName, forumPolicy, minPassLength, 100);

            bool res = bridge.RegisterToForum(forumName, username, pass, email, dateOfBirth);

            Assert.IsFalse(res);
            // make sure user is registered
            Assert.IsFalse(bridge.IsRegisteredToForum(username, forumName));

            // cleanup
            base.Cleanup(forumName);
        }




        [TestMethod]
        public void TestAuthenticationPolicySuccess()
        {
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.Authentication;
            string username = "user1";
            string pass = "passwd";
            string email = "user1@gmail.com";
            DateTime dateOfBirth = DateTime.Now;


            base.CreateForum(forumName, forumPolicy);

            bool res = bridge.RegisterToForum(forumName, username, pass, email, dateOfBirth);
            Assert.IsTrue(res);

            res = bridge.ConfirmRegistration(forumName, username);
            Assert.IsTrue(res);
            // make sure user is registered
            Assert.IsTrue(bridge.IsRegisteredToForum(username, forumName));

            // cleanup
            base.Cleanup(forumName);
        }

        [TestMethod]
        public void TestAuthenticationPolicyFail()
        {
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.Authentication;
            string username = "user1";
            string pass = "passwd";
            string email = "user1@gmail.com";
            DateTime dateOfBirth = DateTime.Now;


            base.CreateForum(forumName, forumPolicy);

            bool res = bridge.RegisterToForum(forumName, username, pass, email, dateOfBirth);
            Assert.IsTrue(res);

            // make sure user is registered
            Assert.IsFalse(bridge.IsRegisteredToForum(username, forumName));

            // cleanup
            base.Cleanup(forumName);
        }



        [TestMethod]
        public void TestMinAgePolicySuccess()
        {
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.MinimumAge;
            string username = "user1";
            string pass = "passwd";
            string email = "user1@gmail.com";
            DateTime dateOfBirth = DateTime.Now.AddYears(-20);

            int minAge = 18;

            base.CreateForum(forumName, forumPolicy, minAge);

            bool res = bridge.RegisterToForum(forumName, username, pass, email, dateOfBirth);

            Assert.IsTrue(res);
            // make sure user is registered
            Assert.IsTrue(bridge.IsRegisteredToForum(username, forumName));

            // cleanup
            base.Cleanup(forumName);
        }


        [TestMethod]
        public void TestMinAgePolicyFail()
         {
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.MinimumAge;
            string username = "user1";
            string pass = "passwd";
            string email = "user1@gmail.com";
            DateTime dateOfBirth = DateTime.Now.AddYears(-20);

            int minAge = 23;

            base.CreateForum(forumName, forumPolicy, minAge);

            bool res = bridge.RegisterToForum(forumName, username, pass, email, dateOfBirth);

            Assert.IsFalse(res);
            // make sure user is registered
            Assert.IsFalse(bridge.IsRegisteredToForum(username, forumName));

            // cleanup
            base.Cleanup(forumName);
        }




        [TestMethod]
        public void TestEditPolicyWithFail()
        {
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.MinimumAge;
            string username = "user1";
            string pass = "passwd";
            string email = "user1@gmail.com";
            DateTime dateOfBirth = DateTime.Now.AddYears(-20);

            int minAge = 18;

            base.CreateForum(forumName, forumPolicy, minAge);

            bool res = bridge.RegisterToForum(forumName, username, pass, email, dateOfBirth);

            Assert.IsTrue(res);
            // make sure user is registered
            Assert.IsTrue(bridge.IsRegisteredToForum(username, forumName));



             forumPolicy = PoliciesStub.Password;
             username = "user2";
             pass = "passwd";
             email = "user2@gmail.com";
             dateOfBirth = DateTime.Now;

            int minPassLength = 8;

           res= bridge.SetForumProperties(forumName, "superadmin", forumPolicy,minPassLength,100);

            Assert.IsTrue(res);

            res = bridge.RegisterToForum(forumName, username, pass, email, dateOfBirth);

            Assert.IsFalse(res);
            // make sure user isnt registered
            Assert.IsFalse(bridge.IsRegisteredToForum(username, forumName));

            // cleanup
            base.Cleanup(forumName);
        }



        [TestMethod]
        public void TestEditPolicyWithSuccess()
        {
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.MinimumAge;
            string username = "user1";
            string pass = "password";
            string email = "user1@gmail.com";
            DateTime dateOfBirth = DateTime.Now.AddYears(-20);

            int minAge = 18;

            base.CreateForum(forumName, forumPolicy, minAge);

            bool res = bridge.RegisterToForum(forumName, username, pass, email, dateOfBirth);

            Assert.IsTrue(res);
            // make sure user is registered
            Assert.IsTrue(bridge.IsRegisteredToForum(username, forumName));



            forumPolicy = PoliciesStub.Password;
            username = "user2";
            pass = "password";
            email = "user2@gmail.com";
            dateOfBirth = DateTime.Now;

            int minPassLength = 8;

            res = bridge.SetForumProperties(forumName, "superadmin", forumPolicy, minPassLength, 100);

            Assert.IsTrue(res);

            res = bridge.RegisterToForum(forumName, username, pass, email, dateOfBirth);

            Assert.IsTrue(res);
            // make sure user isnt registered
            Assert.IsTrue(bridge.IsRegisteredToForum(username, forumName));

            // cleanup
            base.Cleanup(forumName);
        }





    }




}
