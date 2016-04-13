using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using AcceptanceTestsBridge;

namespace AcceptanceTests.ServerTests
{
    [TestClass]
    public class SendPrivateMsgTests : UseCaseTestSuite
    {
        public SendPrivateMsgTests()
            : base()
        {

        }

        // test - send a private message to another user in the same forum, check that
        // the receiver received the msg (i.e. it has been updated in his received msg's list).
        [TestMethod]
        public void TestSendPrivateMsgReceiverUpdated()
        {
            string username1 = "username1", username2 = "username2";
            string pass1 = "passwd1", pass2 = "passwd2";
            string msgTitle = "title1", msgContent = "hello user2";
            string forumName = "forum1";
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string email1 = "user1@gmail.com", email2 = "user2@gmail.com";
            DateTime dateOfBirth1 = new DateTime(1995, 8, 2), dateOfBirth2 = new DateTime(1995, 8, 2);

            // create a forum and register 2 users to it
            base.CreateForum(forumName, forumPolicy);
            bridge.RegisterToForum(forumName, username1, pass1, email1, dateOfBirth1);
            bridge.RegisterToForum(forumName, username2, pass2, email2, dateOfBirth2);

            bridge.LoginUser(forumName, username1, pass1);

            // send private msg
            bool res = bridge.SendPrivateMsg(forumName, username1, username2, msgTitle, msgContent);
            Assert.IsTrue(res);

            res = bridge.IsMsgReceived(forumName, username2, msgTitle, msgContent);
            Assert.IsTrue(res);

            // cleanup
            base.DeleteForum(forumName);
        }

        // test - send a private message to another user in the same forum, check that
        // the sender's sent msg's list has been updated with the new sent msg.
        [TestMethod]
        public void TestSendPrivateMsgSenderUpdated()
        {
            string username1 = "username1", username2 = "username2";
            string pass1 = "passwd1", pass2 = "passwd2";
            string msgTitle = "title1", msgContent = "hello user2";
            string forumName = "forum1";
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string email1 = "user1@gmail.com", email2 = "user2@gmail.com";
            DateTime dateOfBirth1 = new DateTime(1995, 8, 2), dateOfBirth2 = new DateTime(1995, 8, 2);

            // create a forum and register 2 users to it
            base.CreateForum(forumName, forumPolicy);
            bridge.RegisterToForum(forumName, username1, pass1, email1, dateOfBirth1);
            bridge.RegisterToForum(forumName, username2, pass2, email2, dateOfBirth2);

            bridge.LoginUser(forumName, username1, pass1);

            // send private msg
            bool res = bridge.SendPrivateMsg(forumName, username1, username2, msgTitle, msgContent);
            Assert.IsTrue(res);

            res = bridge.IsMsgSent(forumName, username1, msgTitle, msgContent);
            Assert.IsTrue(res);

            // cleanup
            base.DeleteForum(forumName);
        }

    }
}
