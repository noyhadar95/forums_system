using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumsSystem.Server.ForumManagement.Data_Access_Layer;
using System.Data;

namespace UnitTests.ServerUnitTests.Data_Access_Layer
{
    [TestClass]
    public class FriendsTests
    {
        DAL_Forum dl;
        DAL_Users du;
        DAL_Friends df;
        string forumName = "testFriendForum";
        [TestInitialize()]
        public void Initialize()
        {
            dl = new DAL_Forum();
            du = new DAL_Users();
            df = new DAL_Friends();

            dl.CreateForum(forumName, -1);
            du.CreateUser(forumName, "User1", "Pass1", "User1@email.com", DateTime.Today, DateTime.Today.AddYears(-20), 0, UserType.UserTypes.Member,DateTime.Today, "t1");
            du.CreateUser(forumName, "User2", "Pass1", "User2@email.com", DateTime.Today, DateTime.Today.AddYears(-20), 0, UserType.UserTypes.Member,DateTime.Today, "t2");

        }
        [TestCleanup()]
        public void Cleanup()
        {
            dl.DeleteForum(forumName);

            dl = null;
            du = null;
            df = null;
        }

        [TestMethod]
        public void TestAddFriendAndGetFriend()
        {
            df.addFriend(forumName, "User1", "User2");
            DataTable d = df.GetUsersFriends(forumName, "User1");
            Assert.IsTrue(d.Rows.Count == 0);

            df.AcceptFriend(forumName, "User1", "User2");
            d = df.GetUsersFriends(forumName, "User1");
            Assert.IsTrue(d.Rows.Count == 1);
        }
        [TestMethod]
        public void TestAcceptFriend()
        {
            df.addFriend(forumName, "User1", "User2");
            df.AcceptFriend(forumName, "User1", "User2");
            DataTable d = df.GetUsersFriends(forumName, "User1");
            Assert.IsTrue((bool)d.Rows[0][3] == true);
        }


    }
}
