using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumsSystem.Server.ForumManagement.DomainLayer;
using ForumsSystem.Server.UserManagement.DomainLayer;
using ForumsSystem.Server.ForumManagement.Data_Access_Layer;

namespace UnitTests.ServerUnitTests.DomainLayer
{
    [TestClass]
    public class PostTests
    {
        DAL_Forum dal_forum = new DAL_Forum();
        Post post;
        DateTime year;

        [TestMethod]
        public void TestAddReply()//1
        {
            DateTime today = DateTime.Today;
            year = today.AddYears(-34);
            IForum forum = new Forum("name");
            IUser user = new User("username", "1234", "mail.com", forum,year);
            ISubForum subForum = new SubForum(forum,user,"name");
            Thread thread = new Thread(subForum);
           
            post = new Post(user,thread, "title", "content");
            Post reply = new Post(user,thread, "title1", "content1");
            post.AddReply(reply);
            Assert.IsTrue(post.NumOfReplies() == 1);

            dal_forum.DeleteForum("name");
        }

        [TestMethod]
        public void TestDeleteReply()//2
        {
            dal_forum.DeleteForum("name");
            IForum forum = new Forum("name");
            IUser user = new User("username", "1234", "mail.com", forum, year);
            ISubForum subForum = new SubForum(forum,user,"name");
            Thread thread = new Thread(subForum);
            
            post = new Post(user,thread, "title", "content");
            Post reply = new Post(user,thread, "title1", "content1");
            post.AddReply(reply);
            Assert.IsTrue(post.NumOfReplies() == 1);
            Assert.IsTrue(reply.DeletePost());
            Assert.IsTrue(post.NumOfReplies() == 0);

            dal_forum.DeleteForum("name");
        }

        [TestMethod]
        public void TestDeletePostWithReplies()//3
        {
            dal_forum.DeleteForum("name");
            IForum forum = new Forum("name");
            IUser user = new User("username", "1234", "mail.com", forum, year);
            ISubForum subForum = new SubForum(forum,user,"name");
            Thread thread = new Thread(subForum);
            
            post = new Post(user, thread, "title", "content");
            Post reply = new Post(user, thread, "title1", "content1");
            post.AddReply(reply);
            Post rep1= new Post(user, thread, "title2", "content2");
            reply.AddReply(rep1);
            Assert.IsTrue(post.NumOfReplies() == 1);
            Assert.IsTrue(reply.DeletePost());
            Assert.IsTrue(post.NumOfReplies() == 0);

            dal_forum.DeleteForum("name");
        }

        [TestMethod]
        public void TestAddReplyToSelf()//4
        {
            IForum forum = new Forum("name");
            IUser user = new User("username", "1234", "mail.com", forum, year);
            ISubForum subForum = new SubForum(forum,user,"name");
            Thread thread = new Thread(subForum);
            
            post = new Post(user, thread, "title", "content");
            Assert.IsFalse(post.AddReply(post));
            Assert.IsTrue(post.NumOfReplies() == 0);
        }

        [TestMethod]
        public void TestGetReply()//5
        {
            dal_forum.DeleteForum("name");
            IForum forum = new Forum("name");
            IUser user = new User("username", "1234", "mail.com", forum, year);
            ISubForum subForum = new SubForum(forum,user,"name");
            Thread thread = new Thread(subForum);
            
            post = new Post(user, thread, "title", "content");
            Post reply = new Post(user, thread, "title1", "content1");
            post.AddReply(reply);
            Assert.IsTrue(post.NumOfReplies() == 1);
            Assert.IsTrue(post.GetReply(0) == reply);

            dal_forum.DeleteForum("name");
        }

        [TestMethod]
        public void TestGetThread()//6
        {
            dal_forum.DeleteForum("name");
            IForum forum = new Forum("name");
            IUser user = new User("username", "1234", "mail.com", forum, year);
            ISubForum subForum = new SubForum(forum,user, "name");
            Thread thread = new Thread(subForum);
            
            post = new Post(user, thread, "title", "content");
            Assert.IsTrue(thread == post.Thread);

            dal_forum.DeleteForum("name");
        }
    }
}
