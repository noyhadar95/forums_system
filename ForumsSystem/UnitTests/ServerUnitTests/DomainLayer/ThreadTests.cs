using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumsSystem.Server.ForumManagement.DomainLayer;
using ForumsSystem.Server.UserManagement.DomainLayer;

namespace UnitTests.ServerUnitTests.DomainLayer
{
    [TestClass]
    public class ThreadTests
    {
        [TestMethod]
        public void TestAddOpeningPost()//1
        {
            Forum forum = new Forum("forum");
            User user = new User("a", "1234", "mail", forum);
            SubForum subForum = new SubForum(forum,user, "sub forum");
            Thread thread = new Thread(subForum);
            
            Assert.IsTrue(thread.GetTiltle().Equals(""));
            Assert.IsTrue(thread.AddOpeningPost(new Post(user, thread, "title", "content")));
            Assert.IsTrue(thread.GetTiltle().Equals("title"));
            Assert.IsFalse(thread.AddOpeningPost(new Post(user, thread, "new one", "content")));
            Assert.IsTrue(thread.GetTiltle().Equals("title"));
        }

        [TestMethod]
        public void TestGetPostById()//2
        {
            Forum forum = new Forum("forum");
            User user = new User("a", "1234", "mail", forum);
            SubForum subForum = new SubForum(forum,user, "sub forum");
            Thread thread = new Thread(subForum);
            
            Assert.IsTrue(thread.GetTiltle().Equals(""));
            Post post1 = new Post(user, thread, "title", "content");
            Post rep1 = new Post(user, thread, "rep", "content");
            Post rep2 = new Post(user, thread, "rep", "content");
            Post rep21 = new Post(user, thread, "rep", "content");
            Post rep22 = new Post(user, thread, "rep", "content");
            Post rep23 = new Post(user, thread, "rep", "content");
            Assert.IsTrue(thread.GetPostById("1") == null);
            Assert.IsTrue(thread.AddOpeningPost(post1));
            Assert.IsTrue(thread.GetTiltle().Equals("title"));
            thread.GetOpeningPost().AddReply(rep1);
            Post test = thread.GetPostById("1.1");
            Assert.IsTrue(test == rep1);
            test = thread.GetPostById("3.1");
            Assert.IsTrue(test == null);
            test = thread.GetPostById("1.4");
            Assert.IsTrue(test == null);
            thread.GetPostById("1").AddReply(rep2);
            thread.GetPostById("1.2").AddReply(rep21);
            thread.GetPostById("1.2").AddReply(rep22);
            thread.GetPostById("1.2").AddReply(rep23);
            test = thread.GetPostById("1.2.3");
            Assert.IsTrue(test == rep23);
        }
    }
}
