using ForumsSystem.Server.ForumManagement.DomainLayer;
using ForumsSystem.Server.UserManagement.DomainLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests.ServerUnitTests.DomainLayer
{

    [TestClass]
    public class SubForumTests
    {

        IForum forum;
        IUser user;
        ISubForum subForum;

        [TestInitialize()]
        public void Initialize()
        {
            forum = new Forum("testForum"); ;
            user = new User("u1", "p1", "e1@gmail.com", forum);
           
            subForum = new SubForum(forum, "sub1");
        }


        [TestCleanup()]
        public void Cleanup()
        {
            forum = null;
            subForum = null; 
            user = null;
        }

        [TestMethod]
        public void TestNoModerator()
        {
            DateTime expirationDate = DateTime.Today.AddMonths(1);
            subForum.addModerator(user, expirationDate);
            Assert.IsNull(subForum.getModeratorByUserName("wrong"));
        }
        [TestMethod]
        public void TestAddModerator()
        {
            DateTime expirationDate = DateTime.Today.AddMonths(1);
            subForum.addModerator(user, expirationDate);
            Assert.AreEqual(subForum.getModeratorByUserName(user.getUsername()).user,user);
        }
        [TestMethod]
        public void TestChangeModeratorExpirationDate()
        {
            DateTime expirationDate = DateTime.Today.AddMonths(1);
            subForum.addModerator(user, expirationDate);
           // Assert.AreEqual(subForum.getModeratorByUserName(user.getUsername()), user);
            
        }

    }
}
