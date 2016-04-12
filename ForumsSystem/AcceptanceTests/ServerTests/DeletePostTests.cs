using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using AcceptanceTestsBridge;

namespace AcceptanceTests.ServerTests
{
    [TestClass]
    public class DeletePostTests : UseCaseTestSuite
    {
        public DeletePostTests()
            : base()
        {

        }

        // test - delete reply post
        [TestMethod]
        public void TestDeletePostReply()
        {
            string title = "title1";
            string content = "content1";
            string forumName = "forum1";
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username1 = "user1";
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            moderators.Add(username1,  DateTime.Today.AddDays(100));
            string subForumName = "sub forum 1";
            string threadPublisher = "publisher1";

            int threadID = base.AddThread(forumName, forumPolicy, subForumName, moderators, threadPublisher, title, content);
            int postID = bridge.GetOpenningPostID(forumName, subForumName, threadID);

            // add reply post
            int replyPostID = bridge.AddReplyPost(forumName, subForumName, threadID, threadPublisher, postID, title, content);

            bool res = bridge.DeletePost(forumName, subForumName, threadID, threadPublisher, replyPostID);
            // check that the deletion is successfully done
            Assert.IsTrue(res);

            // cleanup
            base.DeleteForum(forumName);
        }

        // test - delete reply post with replies to it, need to delete all of it's replies
        [TestMethod]
        public void TestDeletePostReplyWithReplies()
        {
            string title = "title1";
            string content = "content1";
            string forumName = "forum1";
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username1 = "user1";
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            moderators.Add(username1,  DateTime.Today.AddDays(100));
            string subForumName = "sub forum 1";
            string threadPublisher = "publisher1";

            int threadID = base.AddThread(forumName, forumPolicy, subForumName, moderators, threadPublisher, title, content);
            int postID = bridge.GetOpenningPostID(forumName, subForumName, threadID);

            // add reply post and add 2 replies to it
            int replyPostID = bridge.AddReplyPost(forumName, subForumName, threadID, threadPublisher, postID, title, content);
            int replyReplyPostID1 = bridge.AddReplyPost(forumName, subForumName, threadID, threadPublisher, replyPostID, title, content);
            int replyReplyPostID2 = bridge.AddReplyPost(forumName, subForumName, threadID, threadPublisher, replyPostID, title, content);

            int countNestedRepliesBefore = bridge.CountNestedReplies(forumName, subForumName, threadID, postID);
            int countRepliesToDelete = bridge.CountNestedReplies(forumName, subForumName, threadID, replyPostID);
            // add 1 for the post that is being deleted
            countRepliesToDelete++;

            bool res = bridge.DeletePost(forumName, subForumName, threadID, threadPublisher, replyPostID);
            // check that the deletion is successfully done
            Assert.IsTrue(res);

            int countNestedRepliesAfter = bridge.CountNestedReplies(forumName, subForumName, threadID, postID);
            Assert.IsTrue(countNestedRepliesAfter == countNestedRepliesBefore - countRepliesToDelete);

            // cleanup
            base.DeleteForum(forumName);
        }

        // test - delete opening post, check that the thread of the opening post is deleted too.
        [TestMethod]
        public void TestDeletePostOpening()
        {
            string title = "title1";
            string content = "content1";
            string forumName = "forum1";
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username1 = "user1";
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            moderators.Add(username1,  DateTime.Today.AddDays(100));
            string subForumName = "sub forum 1";
            string threadPublisher = "publisher1";

            int threadID = base.AddThread(forumName, forumPolicy, subForumName, moderators, threadPublisher, title, content);
            int postID = bridge.GetOpenningPostID(forumName, subForumName, threadID);

            bool res = bridge.DeletePost(forumName, subForumName, threadID, threadPublisher, postID);
            // check that the deletion is successfully done
            Assert.IsTrue(res);
            // check that the thread has been deleted
            Assert.IsTrue(bridge.IsExistThread(forumName, subForumName, threadID));

            // cleanup
            base.DeleteForum(forumName);
        }

    }
}
