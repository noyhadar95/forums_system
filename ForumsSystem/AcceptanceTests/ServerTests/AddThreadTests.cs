using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AcceptanceTests.ServerTests
{
    [TestClass]
    public class AddThreadTests : UseCaseTestSuite
    {
        public AddThreadTests()
            : base()
        {

        }

        // check add thread where both title and content are not empty
        // check that the thread has been added successfully, by checking the return value
        // of the AddThread(..) method
        [TestMethod]
        public void TestAddThreadSuccess()
        {
            string title = "title1";
            string content = "content1";
            string forumName = "forum1";
            string forumProperties = "";
            string username1 = "user1";
            DateTime dateOfBirth1 = DateTime.Now;
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            moderators.Add(username1, new DateTime());
            string subForumName = "sub forum 1";
            string threadPublisher = "publisher1";

            // create a forum, sub-forum and a thread (opening post)
            int threadID = base.AddThread(forumName, forumProperties, subForumName, moderators,
                threadPublisher, title, content);

            // check that res is not negative
            Assert.IsTrue(threadID >= 0);

            // cleanup
            base.DeleteForum(forumName);
        }

        // check that it's possible to add a post with empty content and a valid title
        [TestMethod]
        public void TestAddThreadEmptyContent()
        {
            string title = "title1";
            string content = ""; // empty content
            string forumName = "forum1";
            string forumProperties = "";
            string username1 = "user1";
            DateTime dateOfBirth1 = DateTime.Now;
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            moderators.Add(username1, new DateTime());
            string subForumName = "sub forum 1";
            string threadPublisher = "publisher1";

            // create a forum, sub-forum and a thread (opening post)
            int threadID = base.AddThread(forumName, forumProperties, subForumName, moderators,
                 threadPublisher, title, content);

            // check that res is not negative
            Assert.IsTrue(threadID >= 0);

            // cleanup
            base.DeleteForum(forumName);
        }

        // check that it's possible to add a post with empty title and a valid content
        [TestMethod]
        public void TestAddThreadEmptyTitle()
        {
            string title = ""; // empty title
            string content = "content1";
            string forumName = "forum1";
            string forumProperties = "";
            string username1 = "user1";
            DateTime dateOfBirth1 = DateTime.Now;
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            moderators.Add(username1, new DateTime());
            string subForumName = "sub forum 1";
            string threadPublisher = "publisher1";

            // create a forum, sub-forum and a thread (opening post)
            int threadID = base.AddThread(forumName, forumProperties, subForumName, moderators,
                 threadPublisher, title, content);

            // check that res is not negative
            Assert.IsTrue(threadID >= 0);

            // cleanup
            base.DeleteForum(forumName);
        }

        // check that it's not possible to add a post with empty title and empty content
        [TestMethod]
        public void TestAddThreadEmptyTitleContent()
        {
            string title = ""; // empty title
            string content = ""; // empty content
            string forumName = "forum1";
            string forumProperties = "";
            string username1 = "user1";
            DateTime dateOfBirth1 = DateTime.Now;
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            moderators.Add(username1, new DateTime());
            string subForumName = "sub forum 1";
            string threadPublisher = "publisher1";

            // create a forum, sub-forum and a thread (opening post)
            int threadID = base.AddThread(forumName, forumProperties, subForumName, moderators,
                 threadPublisher, title, content);

            // check that res is negative, which means that the post havn't been added
            Assert.IsTrue(threadID < 0);

            // cleanup
            base.DeleteForum(forumName);
        }


    }
}
