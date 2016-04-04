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
        public void TestAddReply()//1
        {
            IForum forum = new Forum("name");
            ISubForum subForum = new SubForum("name");
            Thread thread = new Thread(subForum);
            IUser user = new User("username", "1234", "mail.com", forum);
            post = new Post(user,thread, null, "title", "content");
            Post reply = new Post(user,thread, post, "title1", "content1");
            post.AddReply(reply);
            Assert.IsTrue(post.NumOfReplies() == 1);
        }

        [TestMethod]
        public void TestDeleteReply()//2
        {
            IForum forum = new Forum("name");
            ISubForum subForum = new SubForum("name");
            Thread thread = new Thread(subForum);
            IUser user = new User("username", "1234", "mail.com", forum);
            post = new Post(user,thread, null, "title", "content");
            Post reply = new Post(user,thread, post, "title1", "content1");
            post.AddReply(reply);
            Assert.IsTrue(post.NumOfReplies() == 1);
            Assert.IsTrue(reply.DeletePost());
            Assert.IsTrue(post.NumOfReplies() == 0);
        }
    }
}
