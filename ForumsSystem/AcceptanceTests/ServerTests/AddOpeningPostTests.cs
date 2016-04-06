﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AcceptanceTests.ServerTests
{
    [TestClass]
    public class AddOpeningPostTests : UseCaseTestSuite
    {
        public AddOpeningPostTests()
            : base()
        {

        }

        // check add opening post where both title and content are not empty
        // check that the opening post has been posted successfully, by checking the return value
        // of the AddOpeningPost(..) method
        [TestMethod]
        public void TestAddOpeningPost()
        {
            string title = "title1";
            string content = "content1";
            string forumName = "forum1";
            string forumProperties = "";
            string username1 = "user1", pass1 = "passwd", email = "user1@gmail.com";
            List<string> moderators = new List<string>();
            moderators.Add(username1);
            string subForumName = "sub forum 1";
            string subForumProps = "";
            string threadName = "thread1";

            // create a forum, sub-forum and a thread to add a post to.
            base.CreateForum(forumName, forumProperties);
            bridge.RegisterToForum(forumName, username1, pass1, email);
            bridge.CreateSubForum(forumName, subForumName, moderators, subForumProps);
            int threadID = bridge.AddThread(forumName, subForumName, threadName);

            // add opening post
            int res = bridge.AddOpeningPost(forumName, subForumName, threadID, title, content);
            // check that res is not negative
            Assert.IsTrue(res >= 0);

            // cleanup
            base.DeleteForum(forumName);
        }

        // check that it's possible to add a post with empty content and a valid title
        [TestMethod]
        public void TestAddOpeningPostEmptyContent()
        {
            string title = "title1";
            string content = ""; // empty content
            string forumName = "forum1";
            string forumProperties = "";
            string username1 = "user1", pass1 = "passwd", email = "user1@gmail.com";
            List<string> moderators = new List<string>();
            moderators.Add(username1);
            string subForumName = "sub forum 1";
            string subForumProps = "";
            string threadName = "thread1";

            // create a forum, sub-forum and a thread to add a post to.
            base.CreateForum(forumName, forumProperties);
            bridge.RegisterToForum(forumName, username1, pass1, email);
            bridge.CreateSubForum(forumName, subForumName, moderators, subForumProps);
            int threadID = bridge.AddThread(forumName, subForumName, threadName);

            // add opening post
            int res = bridge.AddOpeningPost(forumName, subForumName, threadID, title, content);
            // check that res is not negative
            Assert.IsTrue(res >= 0);

            // cleanup
            base.DeleteForum(forumName);
        }

        // check that it's possible to add a post with empty title and a valid content
        [TestMethod]
        public void TestAddOpeningPostEmptyTitle()
        {
            string title = ""; // empty title
            string content = "content1";
            string forumName = "forum1";
            string forumProperties = "";
            string username1 = "user1", pass1 = "passwd", email = "user1@gmail.com";
            List<string> moderators = new List<string>();
            moderators.Add(username1);
            string subForumName = "sub forum 1";
            string subForumProps = "";
            string threadName = "thread1";

            // create a forum, sub-forum and a thread to add a post to.
            base.CreateForum(forumName, forumProperties);
            bridge.RegisterToForum(forumName, username1, pass1, email);
            bridge.CreateSubForum(forumName, subForumName, moderators, subForumProps);
            int threadID = bridge.AddThread(forumName, subForumName, threadName);

            // add opening post
            int res = bridge.AddOpeningPost(forumName, subForumName, threadID, title, content);
            // check that res is not negative
            Assert.IsTrue(res >= 0);

            // cleanup
            base.DeleteForum(forumName);
        }

        // check that it's not possible to add a post with empty title and empty content
        [TestMethod]
        public void TestAddOpeningPostEmptyTitleContent()
        {
            string title = ""; // empty title
            string content = ""; // empty content
            string forumName = "forum1";
            string forumProperties = "";
            string username1 = "user1", pass1 = "passwd", email = "user1@gmail.com";
            List<string> moderators = new List<string>();
            moderators.Add(username1);
            string subForumName = "sub forum 1";
            string subForumProps = "";
            string threadName = "thread1";

            // create a forum, sub-forum and a thread to add a post to.
            base.CreateForum(forumName, forumProperties);
            bridge.RegisterToForum(forumName, username1, pass1, email);
            bridge.CreateSubForum(forumName, subForumName, moderators, subForumProps);
            int threadID = bridge.AddThread(forumName, subForumName, threadName);

            // add opening post
            int res = bridge.AddOpeningPost(forumName, subForumName, threadID, title, content);
            // check that res is negative, which means that the post havn't been added
            Assert.IsTrue(res < 0);

            // cleanup
            base.DeleteForum(forumName);
        }

    }
}
