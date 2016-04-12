using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using AcceptanceTestsBridge;

namespace AcceptanceTests.ServerTests
{
    [TestClass]
    public class AddReplyPostTests : UseCaseTestSuite
    {
        public AddReplyPostTests()
            : base()
        {

        }

        # region test - add reply to opening, nesting level = 1 (reply id 1.1)

        // check add opening post where both title and content are not empty
        // check that the reply post has been posted successfully, by checking the return value
        // of the AddReplyPost(..) method
        [TestMethod]
        public void TestAddReplyPost()
        {
            string title = "title1";
            string content = "content1";
            string forumName = "forum1";
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username1 = "user1";
            DateTime dateOfBirth1 = DateTime.Now;
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            moderators.Add(username1,  DateTime.Today.AddDays(100));
            string subForumName = "sub forum 1";
            string threadPublisher = "publisher1";

            // create a forum, sub-forum and thread.
            int threadID = base.AddThread(forumName, forumPolicy, subForumName, moderators,
               threadPublisher, title, content);
            int postID = bridge.GetOpenningPostID(forumName, subForumName, threadID);

            // add reply post
            int res = bridge.AddReplyPost(forumName, subForumName, threadID, threadPublisher, postID, title, content);
            // check that res is not negative, which means that the post have been added
            Assert.IsTrue(res >= 0);

            // cleanup
            base.DeleteForum(forumName);
        }

        // check that it's possible to add a post with empty content and a valid title
        [TestMethod]
        public void TestAddReplyPostEmptyContent()
        {
            string title = "title1";
            string content = ""; // empty content
            string forumName = "forum1";
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username1 = "user1";
            DateTime dateOfBirth1 = DateTime.Now;
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            moderators.Add(username1,  DateTime.Today.AddDays(100));
            string subForumName = "sub forum 1";
            string threadPublisher = "publisher1";

            // create a forum, sub-forum and thread.
            int threadID = base.AddThread(forumName, forumPolicy, subForumName, moderators,
                threadPublisher, title, content);
            // add opening post
            int postID = bridge.GetOpenningPostID(forumName, subForumName, threadID);

            // add reply post
            int res = bridge.AddReplyPost(forumName, subForumName, threadID, threadPublisher, postID, title, content);
            // check that res is not negative, which means that the post have been added
            Assert.IsTrue(res >= 0);

            // cleanup
            base.DeleteForum(forumName);
        }

        // check that it's possible to add a post with empty title and a valid content
        [TestMethod]
        public void TestAddReplyPostEmptyTitle()
        {
            string title = ""; // empty title
            string content = "content1";
            string forumName = "forum1";
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username1 = "user1", pass1 = "passwd", email = "user1@gmail.com";
            DateTime dateOfBirth1 = DateTime.Now;
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            moderators.Add(username1,  DateTime.Today.AddDays(100));
            string subForumName = "sub forum 1";
            string threadPublisher = "publisher1";

            // create a forum, sub-forum and thread.
            int threadID = base.AddThread(forumName, forumPolicy, subForumName, moderators,
                 threadPublisher, title, content);
            // add opening post
            int postID = bridge.GetOpenningPostID(forumName, subForumName, threadID);

            // add reply post
            int res = bridge.AddReplyPost(forumName, subForumName, threadID, threadPublisher, postID, title, content);
            // check that res is not negative, which means that the post have been added
            Assert.IsTrue(res >= 0);

            // cleanup
            base.DeleteForum(forumName);
        }

        // check that it's not possible to add a post with empty title and empty content
        [TestMethod]
        public void TestAddReplyPostEmptyTitleContent()
        {
            string title = ""; // empty title
            string content = ""; // empty content
            string forumName = "forum1";
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username1 = "user1", pass1 = "passwd", email = "user1@gmail.com";
            DateTime dateOfBirth1 = DateTime.Now;
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            moderators.Add(username1,  DateTime.Today.AddDays(100));
            string subForumName = "sub forum 1";
            string threadPublisher = "publisher1";

            // create a forum, sub-forum and thread.
            int threadID = base.AddThread(forumName, forumPolicy, subForumName, moderators,
                threadPublisher, "thread title", "cont");
            int postID = bridge.GetOpenningPostID(forumName, subForumName, threadID);

            // add reply post
            int res = bridge.AddReplyPost(forumName, subForumName, threadID, threadPublisher, postID, title, content);
            // check that res is negative, which means that the post havn't been added
            Assert.IsTrue(res < 0);

            // cleanup
            base.DeleteForum(forumName);
        }

        # endregion


        // test - add reply to reply, nesting level = 2 (reply id 1.1.1)
        // check add opening post where both title and content are not empty
        [TestMethod]
        public void TestAddReplyPostNest2()
        {
            string title = "title1";
            string content = "content1";
            string forumName = "forum1";
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username1 = "user1";
            DateTime dateOfBirth1 = DateTime.Now;
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            moderators.Add(username1,  DateTime.Today.AddDays(100));
            string subForumName = "sub forum 1";
            string threadPublisher = "publisher1";

            // create a forum, sub-forum and a thread to add a post to.
            int threadID = base.AddThread(forumName, forumPolicy, subForumName, moderators,
                 threadPublisher, title, content);
            int postID = bridge.GetOpenningPostID(forumName, subForumName, threadID);


            // add reply post
            int replyPostID = bridge.AddReplyPost(forumName, subForumName, threadID, threadPublisher, postID, title, content);

            // add reply post to the first reply
            int res = bridge.AddReplyPost(forumName, subForumName, threadID, threadPublisher, replyPostID, title, content);
            Assert.IsTrue(res >= 0);

            // cleanup
            base.DeleteForum(forumName);
        }



        // test - add reply to second reply, nesting level = 2 (reply id 1.2.1)
        // check add reply post to a second reply to the opening post where both title and content are not empty
        [TestMethod]
        public void TestAddReplyPostSecondReplyNest2()
        {
            string title = "title1";
            string content = "content1";
            string forumName = "forum1";
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username1 = "user1";
            DateTime dateOfBirth1 = DateTime.Now;
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            moderators.Add(username1,  DateTime.Today.AddDays(100));
            string subForumName = "sub forum 1";
            string threadPublisher = "publisher1";

            // create a forum, sub-forum and a thread to add a post to.
            int threadID = base.AddThread(forumName, forumPolicy, subForumName, moderators,
                threadPublisher, title, content);
            // add opening post
            int postID = bridge.GetOpenningPostID(forumName, subForumName, threadID);

            // add reply post
            bridge.AddReplyPost(forumName, subForumName, threadID, threadPublisher, postID, title, content);

            // add second reply post to the opening reply
            int replyPostID = bridge.AddReplyPost(forumName, subForumName, threadID, threadPublisher, postID, title, content);

            // add reply nest 2 to the second reply to the opening
            int res = bridge.AddReplyPost(forumName, subForumName, threadID, threadPublisher, replyPostID, title, content);
            Assert.IsTrue(res >= 0);

            // cleanup
            base.DeleteForum(forumName);
        }



    }
}
