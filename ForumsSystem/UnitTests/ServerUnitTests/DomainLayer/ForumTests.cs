using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumsSystem.Server.ForumManagement.DomainLayer;
using ForumsSystem.Server.UserManagement.DomainLayer;

namespace UnitTests.ServerUnitTests.DomainLayer
{
    [TestClass]
    public class ForumTests
    {
        IForum forum;
        [TestInitialize()]
        public void Initialize()
        {
            forum = new Forum("testForum");
        }
        [TestCleanup()]
        public void Cleanup() {
            forum = null;
        }
        
        [TestMethod]
        public void TestRegister()
        {
            string username = "user1";
            string pass = "pass1";
            string email = "tester@email.com";
            Assert.IsTrue( forum.RegisterToForum(username, pass, email));
            IUser user = forum.Login(username, pass);
            Assert.IsNotNull(user);
        }
    }
}
