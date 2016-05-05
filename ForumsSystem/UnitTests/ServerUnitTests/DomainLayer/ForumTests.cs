using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumsSystem.Server.ForumManagement.DomainLayer;
using ForumsSystem.Server.UserManagement.DomainLayer;
using ForumsSystem.Server.ForumManagement.Data_Access_Layer;

namespace UnitTests.ServerUnitTests.DomainLayer
{
    [TestClass]
    public class ForumTests
    {
        IForum forum;
        IUser admin;
        DateTime year;
        DAL_Forum dal_forum = new DAL_Forum();
        DAL_Users dal_users = new DAL_Users();
        [TestInitialize()]
        public void Initialize()
        {
            DateTime today = DateTime.Today;
            year = today.AddYears(-29);
            forum = new Forum("testForum");
            admin = new User("admin", "admin", "admin@gmail.com", forum,year);
        }
        [TestCleanup()]
        public void Cleanup() {
            dal_forum.DeleteForum(forum.getName());
            dal_users.deleteUser(admin.getUsername(), forum.getName());
            forum = null;
        }
        
        [TestMethod]
        public void TestRegister()
        {
            string username = "user1";
            string pass = "pass1";
            string email = "tester@email.com";
            Assert.IsTrue( forum.RegisterToForum(username, pass, email, year));
            IUser user = forum.Login(username, pass);
            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void TestMultipleUserNames()
        {
            string username = "user1";
            string pass = "pass1";
            string email = "tester@email.com";
            Assert.IsTrue(forum.RegisterToForum(username, pass, email, year));
            Assert.IsFalse(forum.RegisterToForum(username, pass + "2", email + "2", year));
         
        }

        [TestMethod]
        public void TestPolicyRegistration()
        {
            string username = "user1";
            string pass = "pass1";
            string email = "tester@email.com";
            Policy policy = new PasswordPolicy(Policies.Password, 8, 100);
            forum.AddPolicy(policy);
            Assert.IsFalse(forum.RegisterToForum(username, pass, email, year));
        }

        [TestMethod]
        public void TestEmailSending()
        {
                string username = "user1";
                string pass = "pass1";
                string email = "noyhada@post.bgu.ac.il";
                Policy policy = new AuthenticationPolicy(Policies.Authentication);
                forum.AddPolicy(policy);
                Assert.IsTrue(forum.RegisterToForum(username, pass, email, year));
            
        }

        [TestMethod]
        public void TestSubForumCreation()
        {
            string subForumName = "sub";
            forum.CreateSubForum(admin,subForumName);
            Assert.IsNotNull(forum.getSubForum(subForumName));
        }

            
    }
}
