using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumsSystem.Server.ForumManagement.DomainLayer;
using ForumsSystem.Server.UserManagement.DomainLayer;

namespace UnitTests.ServerUnitTests.DomainLayer
{
    [TestClass]
    public class PostTests
    {
        Post post;

        [TestMethod]
        public void TestAddReply()
        {
            IForum forum = new Forum();
            IUser user = new User("username", "1234", "mail.com", forum);
            post = new Post(user, null, "title", "content");
            Post reply = new Post(user, post, "title1", "content1");
            post.AddReply(reply);
            Assert.IsTrue(post.NumOfReplies() == 1);
        }

        [TestMethod]
        public void TestDeleteReply()
        {
            IForum forum = new Forum();
            IUser user = new User("username", "1234", "mail.com", forum);
            post = new Post(user, null, "title", "content");
            Post reply = new Post(user, post, "title1", "content1");
            post.AddReply(reply);
            Assert.IsTrue(post.NumOfReplies() == 1);
            Assert.IsTrue(reply.DeletePost());
            Assert.IsTrue(post.NumOfReplies() == 0);
        }
    }
}
