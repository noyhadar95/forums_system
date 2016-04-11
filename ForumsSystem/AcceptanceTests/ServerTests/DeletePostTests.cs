using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
            string forumProperties = "";
            string username1 = "user1";
            List<string> moderators = new List<string>();
            moderators.Add(username1);
            string subForumName = "sub forum 1";
            string subForumProps = "";
            string threadPublisher = "publisher1";

            int threadID = base.AddThread(forumName, forumProperties, subForumName, moderators, subForumProps, threadPublisher);
            int postID = bridge.AddOpeningPost(forumName, subForumName, threadID, title, content);
            // add reply post
            int replyPostID = bridge.AddReplyPost(forumName, subForumName, threadID, postID, title, content);

            bool res = bridge.DeletePost(forumName, subForumName, threadID, replyPostID);
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
            string forumProperties = "";
            string username1 = "user1";
            List<string> moderators = new List<string>();
            moderators.Add(username1);
            string subForumName = "sub forum 1";
            string subForumProps = "";
            string threadPublisher = "publisher1";

            int threadID = base.AddThread(forumName, forumProperties, subForumName, moderators, subForumProps, threadPublisher);
            int postID = bridge.AddOpeningPost(forumName, subForumName, threadID, title, content);
            // add reply post and add 2 replies to it
            int replyPostID = bridge.AddReplyPost(forumName, subForumName, threadID, postID, title, content);
            int replyReplyPostID1 = bridge.AddReplyPost(forumName, subForumName, threadID, replyPostID, title, content);
            int replyReplyPostID2 = bridge.AddReplyPost(forumName, subForumName, threadID, replyPostID, title, content);

            int countNestedRepliesBefore = bridge.CountNestedReplies(forumName, subForumName, threadID, postID);
            int countRepliesToDelete = bridge.CountNestedReplies(forumName, subForumName, threadID, replyPostID);
            // add 1 for the post that is being deleted
            countRepliesToDelete++;

            bool res = bridge.DeletePost(forumName, subForumName, threadID, replyPostID);
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
            string forumProperties = "";
            string username1 = "user1";
            List<string> moderators = new List<string>();
            moderators.Add(username1);
            string subForumName = "sub forum 1";
            string subForumProps = "";
            string threadPublisher = "publisher1";

            int threadID = base.AddThread(forumName, forumProperties, subForumName, moderators, subForumProps, threadPublisher);
            int postID = bridge.AddOpeningPost(forumName, subForumName, threadID, title, content);

            bool res = bridge.DeletePost(forumName, subForumName, threadID, postID);
            // check that the deletion is successfully done
            Assert.IsTrue(res);
            // check that the thread has been deleted
            Assert.IsTrue(bridge.IsExistThread(forumName, subForumName, threadID));

            // cleanup
            base.DeleteForum(forumName);
        }

    }
}
