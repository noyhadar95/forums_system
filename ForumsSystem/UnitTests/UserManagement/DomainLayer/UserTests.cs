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

        [TestInitialize()]
        public void Initialize()
        {
            forum = new Forum("testUser"); ;
            user = new User("u1", "p1", "e1@gmail.com", forum);
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
            IUser receiver = new User("u2", "p2", "u2@gmail.com", forum);
            PrivateMessage privateMessage= user.SendPrivateMessage(receiver.getUsername(), "hi", "sending message");
            Assert.IsTrue(user.getSentMessages().Contains(privateMessage));
            Assert.IsTrue(receiver.getReceivedMessages().Contains(privateMessage));

            PrivateMessage empty = user.SendPrivateMessage(receiver.getUsername(), "", "");
            Assert.IsNull(empty);

            PrivateMessage noContent = user.SendPrivateMessage(receiver.getUsername(), "hi", "");
            Assert.IsNotNull(noContent);

            IForum forum2 = new Forum("f2");
            IUser receiver2 = new User("cantReceive", "p3", "u3@gmail.com", forum2);
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
            Assert.IsTrue(user.RegisterToForum("u2", "p2", forum, "u2@gmail.com"));
            Assert.IsTrue(forum.isUserMember(user.getUsername()));

            IForum forum2 = new Forum("f2");
            Assert.IsFalse(user.RegisterToForum("u2", "p2", forum2, "u2@gmail.com"));
            Assert.IsFalse(forum2.isUserMember(user.getUsername()));
        }

        [TestMethod]
        public void TestCreateSubForum()
        {
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            IUser user1 = new User("m1", "mp1", "m1@gmail.com", forum);
            IUser user2 = new User("m2", "mp2", "m2@gmail.com", forum);
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




    }  
}
