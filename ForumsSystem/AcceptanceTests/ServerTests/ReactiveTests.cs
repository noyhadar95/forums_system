using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcceptanceTestsBridge;
using ForumsSystemClient.CommunicationLayer;

namespace AcceptanceTests.ServerTests
{
    [TestClass]
    public class ReactiveTests : UseCaseTestSuite
    {
        [TestMethod]
        public void TestPrivateMessageNotification()
        {

            string username1 = "username1", username2 = "username2";
            string pass1 = "passwd1", pass2 = "passwd2";
            string msgTitle = "title1", msgContent = "hello user2";
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string email1 = "user1@gmail.com", email2 = "user2@gmail.com";
            DateTime dateOfBirth1 = new DateTime(1995, 8, 2), dateOfBirth2 = new DateTime(1995, 8, 2);

            // create a forum and register 2 users to it
            base.CreateForum(forumName, forumPolicy);
            bridge.RegisterToForum(forumName, username1, pass1, email1, dateOfBirth1);
            bridge.RegisterToForum(forumName, username2, pass2, email2, dateOfBirth2);

            bridge.LoginUser(forumName, username1, pass1);
            bridge.LoginUser(forumName, username2, pass2);

            // send private msg
            bool res = bridge.SendPrivateMsg(forumName, username1, username2, msgTitle, msgContent);
            Assert.IsTrue(res);

            //wait for 2 seconds
            System.Threading.Thread.Sleep(2000);

            res = bridge.recievedNotification(forumName,username1);
            Assert.IsTrue(res);

            NotificationHelper.CleanUp();

            // cleanup
            base.Cleanup(forumName);


        }



        [TestMethod]
        public void TestFriendNotification()
        {

            string username1 = "username1", username2 = "username2";
            string pass1 = "passwd1", pass2 = "passwd2";
            string msgTitle = "title1", msgContent = "hello user2";
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string email1 = "user1@gmail.com", email2 = "user2@gmail.com";
            DateTime dateOfBirth1 = new DateTime(1995, 8, 2), dateOfBirth2 = new DateTime(1995, 8, 2);

            // create a forum and register 2 users to it
            base.CreateForum(forumName, forumPolicy);
            bridge.RegisterToForum(forumName, username1, pass1, email1, dateOfBirth1);
            bridge.RegisterToForum(forumName, username2, pass2, email2, dateOfBirth2);

            bridge.LoginUser(forumName, username1, pass1);
            bridge.LoginUser(forumName, username2, pass2);

            // send private msg
             bridge.AddFriend(forumName, username1, username2);
          

            //wait for 2 seconds
            System.Threading.Thread.Sleep(2000);

           bool res = bridge.recievedNotification(forumName, username1);
            Assert.IsTrue(res);

            NotificationHelper.CleanUp();

            // cleanup
            base.Cleanup(forumName);


        }

    }
}
