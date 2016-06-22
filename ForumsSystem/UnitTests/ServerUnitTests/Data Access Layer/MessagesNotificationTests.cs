using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumsSystem.Server.ForumManagement.Data_Access_Layer;
using System.Data;

namespace UnitTests.ServerUnitTests.Data_Access_Layer
{
    [TestClass]
    public class MessagesNotificationTests
    {
        DAL_Forum dl;
        DAL_Users du;
        DAL_Messages dm;
        DAL_MessagesNotification dmn;

        string forumName = "testMessagesNotificationForum";
        [TestInitialize()]
        public void Initialize()
        {
            dl = new DAL_Forum();
            du = new DAL_Users();
            dm = new DAL_Messages();
            dmn = new DAL_MessagesNotification();


            dl.CreateForum(forumName, -1);
            du.CreateUser(forumName, "User1", "Pass1", "User1@email.com", DateTime.Today, DateTime.Today.AddYears(-20), 0, UserType.UserTypes.Member,DateTime.Today, "t1", true);
            du.CreateUser(forumName, "User2", "Pass1", "User2@email.com", DateTime.Today, DateTime.Today.AddYears(-20), 0, UserType.UserTypes.Member, DateTime.Today, "t2", true);

        }
        [TestCleanup()]
        public void Cleanup()
        {
            dl.DeleteForum(forumName);

            dl = null;
            du = null;
            dm = null;
        }

        [TestMethod]
        public void TestAddNotification()
        {
            int id = dm.CreateMessage(forumName, "User1", "User2", "Title1", "Much content");

            dmn.AddNotification(id);
            Assert.IsTrue(dmn.GetUsersNotifications(forumName, "User2").Rows.Count == 1);


            dm.DeleteMessage(id);
        }
        [TestMethod]
        public void TestRemoveNotification()
        {
            int id = dm.CreateMessage(forumName, "User1", "User2", "Title1", "Much content");

            dmn.AddNotification(id);
            Assert.IsTrue(dmn.GetUsersNotifications(forumName, "User2").Rows.Count == 1);

            //    dmn.RemoveNotification(id);
            dmn.RemoveAllNotifications(forumName, "User2");
            Assert.IsTrue(dmn.GetUsersNotifications(forumName, "User2").Rows.Count == 0);


            dm.DeleteMessage(id);

        }

    }
}
