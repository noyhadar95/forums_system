using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumsSystem.Server.ForumManagement.Data_Access_Layer;
using System.Data;

namespace UnitTests.ServerUnitTests.Data_Access_Layer
{
    [TestClass]
    public class MessagesTests
    {
        DAL_Forum dl;
        DAL_Users du;
        DAL_Messages dm;

        string forumName = "testMessagesForum";
        [TestInitialize()]
        public void Initialize()
        {
            dl = new DAL_Forum();
            du = new DAL_Users();
            dm = new DAL_Messages();
           
            dl.CreateForum(forumName, -1);
            du.CreateUser(forumName, "User1", "Pass1", "User1@email.com", DateTime.Today, DateTime.Today.AddYears(-20), 0, UserType.UserTypes.Member);
            du.CreateUser(forumName, "User2", "Pass1", "User2@email.com", DateTime.Today, DateTime.Today.AddYears(-20), 0, UserType.UserTypes.Member);

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
        public void TestCreateAndGetMessage()
        {
            int id = dm.CreateMessage(forumName, "User1", "User2","Title1","Much content");
            DataTable d = dm.GetUsersMessages(forumName, "User1");
            Assert.IsTrue(d.Rows.Count == 1);
            d = dm.GetUsersMessages(forumName, "User2");
            Assert.IsTrue(d.Rows.Count == 1);

            dm.DeleteMessage(id);
        }
        [TestMethod]
        public void TestCorrectRecieverAndSender()
        {
            int id = dm.CreateMessage(forumName, "User1", "User2", "Title1", "Much content");
            DataTable d = dm.GetUsersMessages(forumName, "User1");
            Assert.IsTrue(d.Rows.Count == 1);
            d = dm.GetUsersMessages(forumName, "User2");
            Assert.IsTrue(d.Rows.Count == 1);

            d = dm.GetUsersSentMessages(forumName, "User1");
            Assert.IsTrue(d.Rows.Count == 1);
            d = dm.GetUsersRecievedMessages(forumName, "User1");
            Assert.IsTrue(d.Rows.Count == 0);


            d = dm.GetUsersSentMessages(forumName, "User2");
            Assert.IsTrue(d.Rows.Count == 0);
            d = dm.GetUsersRecievedMessages(forumName, "User2");
            Assert.IsTrue(d.Rows.Count == 1);

            dm.DeleteMessage(id);

        }


    }
}
