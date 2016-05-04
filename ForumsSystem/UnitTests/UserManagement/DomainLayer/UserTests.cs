using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumsSystem.Server.ForumManagement.DomainLayer;
using ForumsSystem.Server.UserManagement.DomainLayer;
using System.Collections.Generic;

namespace UnitTests.UserManagement.DomainLayer
{
    [TestClass]
    public class UserTests
    {
        IForum forum;
        IUser user;
        DateTime year;
        [TestInitialize()]
        public void Initialize()
        {
            DateTime today = DateTime.Today;
            year = today.AddYears(-26);
            forum = new Forum("testUser"); ;
            user = new User("u1", "p1", "e1@gmail.com", forum,year);
            user.Login();
        }


        [TestCleanup()]
        public void Cleanup()
        {
            forum = null;
            user = null;
        }

        [TestMethod]
        public void TestChangeType()
        {
            ForumsSystem.Server.UserManagement.DomainLayer.Type admin = new Admin();
            user.ChangeType(admin);
            Assert.IsTrue(user.getType() == admin);
        }

        [TestMethod]
        public void TestSendPrivateMessage()
        {
            IUser receiver = new User("u2", "p2", "u2@gmail.com", forum, year);
            PrivateMessage privateMessage= user.SendPrivateMessage(receiver.getUsername(), "hi", "sending message");
            Assert.IsTrue(user.getSentMessages().Contains(privateMessage));
            Assert.IsTrue(receiver.getReceivedMessages().Contains(privateMessage));

            PrivateMessage empty = user.SendPrivateMessage(receiver.getUsername(), "", "");
            Assert.IsNull(empty);

            PrivateMessage noContent = user.SendPrivateMessage(receiver.getUsername(), "hi", "");
            Assert.IsNotNull(noContent);

            user.Logout();
            Assert.IsNull(user.SendPrivateMessage(receiver.getUsername(), "hi", ""));
            user.Login();

            IForum forum2 = new Forum("f2");
            IUser receiver2 = new User("cantReceive", "p3", "u3@gmail.com", forum2, year);
            PrivateMessage privateMessage2 = user.SendPrivateMessage(receiver2.getUsername(), "hi", "sending message");
            Assert.IsFalse(user.getSentMessages().Contains(privateMessage2));
            Assert.IsFalse(receiver2.getReceivedMessages().Contains(privateMessage2));
            Assert.IsNull(privateMessage2);

            user.ChangeType(new Guest());
            try
            {
                PrivateMessage excep = user.SendPrivateMessage(receiver2.getUsername(), "hi", "sending message");
                Assert.Fail();
            }
            catch (Exception) { }

            user.ChangeType(new Admin());
            PrivateMessage privateMessagefromAdmin = user.SendPrivateMessage(receiver.getUsername(), "hi", "sending message");
            Assert.IsTrue(user.getSentMessages().Contains(privateMessagefromAdmin));
            Assert.IsTrue(receiver.getReceivedMessages().Contains(privateMessagefromAdmin));
        }

        [TestMethod]
        public void TestRegisterToForum()
        {
            user = new User();
            Assert.IsTrue(user.RegisterToForum("u2", "p2", forum, "u2@gmail.com", year));
            user.Login();
            Assert.IsTrue(forum.isUserMember(user.getUsername()));

            IForum forum2 = new Forum("f2");
            Assert.IsFalse(user.RegisterToForum("u2", "p2", forum2, "u2@gmail.com", year));
            Assert.IsFalse(forum2.isUserMember(user.getUsername()));
        }

        [TestMethod]
        public void TestCreateSubForum()
        {
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            IUser user1 = new User("m1", "mp1", "m1@gmail.com", forum, year);
            IUser user2 = new User("m2", "mp2", "m2@gmail.com", forum, year);
            moderators.Add(user1.getUsername(), DateTime.Today.AddMonths(1));
            moderators.Add(user2.getUsername(), DateTime.Today.AddMonths(3));
            try
            {
               user.createSubForum("sub forum1", moderators);
               Assert.Fail();
            }
            catch (Exception) { }

            user.ChangeType(new Guest());
            try
            {
                user.createSubForum("sub forum1", moderators);
                Assert.Fail();
            }
            catch (Exception) { }

            user.ChangeType(new Admin());
            ISubForum subForum = user.createSubForum("sub forum1", moderators);
            Assert.IsNotNull(subForum);
            Assert.IsTrue(subForum.isModerator(user1.getUsername()) && subForum.isModerator(user2.getUsername()));
            Assert.IsNotNull(forum.getSubForum(subForum.getName()));

            Assert.IsNull(user.createSubForum("sub forum1", moderators));

            moderators.Add("guest", DateTime.Today.AddDays(1));
            Assert.IsNull(user.createSubForum("sub forum2", moderators));
                               
        }

        [TestMethod]
        public void TestFirstPost()
        {
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            IUser user1 = new User("m1", "mp1", "m1@gmail.com", forum, year);
            IUser user2 = new User("m2", "mp2", "m2@gmail.com", forum, year);
            moderators.Add(user1.getUsername(), DateTime.Today.AddMonths(1));
            moderators.Add(user2.getUsername(), DateTime.Today.AddMonths(3));
            user.ChangeType(new Admin());
            ISubForum subForum = user.createSubForum("sub forum1", moderators);

            Assert.IsNotNull(user.createThread(subForum, "new thread", "by admin"));
            user1.Login();
            Assert.IsNotNull(user1.createThread(subForum, "", "by member"));

            Assert.IsNull(user.createThread(null, "new thread", "by admin"));
            Assert.IsNull(user.createThread(subForum, "", ""));

            IUser user3 = new User();
            try
            {
                user3.createThread(subForum, "new thread", "by guest");
                Assert.Fail();
            }
            catch (Exception) { }
        }

        [TestMethod]
        public void TestReplyPost()
        {
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            IUser user1 = new User("m1", "mp1", "m1@gmail.com", forum, year);
            IUser user2 = new User("m2", "mp2", "m2@gmail.com", forum, year);
            moderators.Add(user1.getUsername(), DateTime.Today.AddMonths(1));
            moderators.Add(user2.getUsername(), DateTime.Today.AddMonths(3));
            user.ChangeType(new Admin());
            ISubForum subForum = user.createSubForum("sub forum1", moderators);

            Thread thread = user.createThread(subForum, "new thread", "by admin");
            Post openning = thread.GetOpeningPost();
            Post reply = user.postReply(openning, thread, "reply", "by admin");
            Assert.IsNotNull(reply);
            Assert.IsTrue(reply.getPublisher() == user);
            Assert.IsTrue(openning.GetReplies().Contains(reply));

            Assert.IsNull(user.postReply(null, thread, "reply", "by admin"));
            Assert.IsNull(user.postReply(openning, null, "reply", "by admin"));
            Assert.IsNull(user.postReply(openning, thread, "", ""));

            user1.Login();
            Post replyToReply = user1.postReply(reply, thread, "reply", "by admin");
            Assert.IsNotNull(replyToReply);
            Assert.IsTrue(replyToReply.getPublisher() == user1);

            IUser guest = new User();
            try
            {
                guest.postReply(openning, thread, "reply", "by guest");
                Assert.Fail();
            }
            catch (Exception) { }

        }

        [TestMethod]
        public void TestDeletePost()
        {
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            IUser user1 = new User("m1", "mp1", "m1@gmail.com", forum, year);
            IUser user2 = new User("m2", "mp2", "m2@gmail.com", forum, year);
            moderators.Add(user1.getUsername(), DateTime.Today.AddMonths(1));
            moderators.Add(user2.getUsername(), DateTime.Today.AddMonths(3));
            user.ChangeType(new Admin());
            ISubForum subForum = user.createSubForum("sub forum1", moderators);

            Thread thread = user.createThread(subForum, "new thread", "by admin");
            Post openning = thread.GetOpeningPost();
            Post reply = user.postReply(openning, thread, "reply", "by admin");
            Assert.IsFalse(user1.deletePost(openning));

            Assert.IsTrue(user.deletePost(reply));
            Assert.IsFalse(thread.GetOpeningPost().GetReplies().Contains(reply));

            Post reply2 = user1.postReply(openning, thread, "reply2", "by member");
            IUser guest = new User();
            try
            {
                guest.deletePost(reply2);
                Assert.Fail();
            }
            catch (Exception) { }

            Assert.IsTrue(user.deletePost(openning));
            Assert.IsNull(thread.GetOpeningPost());
        }

        [TestMethod]
        public void TestEditPost()
        {
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            IUser user1 = new User("m1", "mp1", "m1@gmail.com", forum, year);
            IUser user2 = new User("m2", "mp2", "m2@gmail.com", forum, year);
            moderators.Add(user1.getUsername(), DateTime.Today.AddMonths(1));
            moderators.Add(user2.getUsername(), DateTime.Today.AddMonths(3));
            user.ChangeType(new Admin());
            ISubForum subForum = user.createSubForum("sub forum1", moderators);

            Thread thread = user.createThread(subForum, "new thread", "by admin");
            Post openning = thread.GetOpeningPost();
            Post reply = user.postReply(openning, thread, "reply", "by admin");
            Assert.IsFalse(user1.editPost("edit", "not the publisher", reply));
            Assert.IsTrue(reply.Title == "reply");
            Assert.IsTrue(reply.Content == "by admin");

            Assert.IsTrue(user.editPost("edit", "by publisher", reply));
            Assert.IsTrue(reply.Title == "edit");
            Assert.IsTrue(reply.Content == "by publisher");

            Post reply2 = user1.postReply(openning, thread, "reply2", "by member");
            IUser guest = new User();
            try
            {
                guest.editPost("edit", "by publisher", reply2);
                Assert.Fail();
            }
            catch (Exception) { }
        }

        [TestMethod]
        public void TestEditExpirationTimeOfModerator()
        {
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            IUser user1 = new User("m1", "mp1", "m1@gmail.com", forum, year);
            IUser user2 = new User("m2", "mp2", "m2@gmail.com", forum, year);
            moderators.Add(user1.getUsername(), DateTime.Today.AddMonths(1));
            moderators.Add(user2.getUsername(), DateTime.Today.AddMonths(3));

            user.ChangeType(new Admin());
            ISubForum subForum = user.createSubForum("sub forum1", moderators);

            try
            {
                user2.editExpirationTimeOfModerator(user1.getUsername(), DateTime.Today.AddMonths(2), subForum);
                Assert.Fail();
            }
            catch (Exception) { }

            Assert.IsTrue(user.editExpirationTimeOfModerator(user1.getUsername(), DateTime.Today.AddMonths(2), subForum));
            Assert.AreEqual(subForum.getModeratorByUserName(user1.getUsername()).expirationDate, DateTime.Today.AddMonths(2));

            Assert.IsFalse(user.editExpirationTimeOfModerator(user2.getUsername(), DateTime.Today.AddMonths(2), null));
            Assert.IsFalse(user.editExpirationTimeOfModerator("", DateTime.Today.AddMonths(2), subForum));

            IUser admin = new User("a1", "ap1", "a1@gmail.com", forum, year);
            admin.ChangeType(new Admin());
            Assert.IsFalse(admin.editExpirationTimeOfModerator(user2.getUsername(), DateTime.Today.AddMonths(2), subForum));
            Assert.AreEqual(subForum.getModeratorByUserName(user2.getUsername()).expirationDate, DateTime.Today.AddMonths(3));
        }

    }
}
