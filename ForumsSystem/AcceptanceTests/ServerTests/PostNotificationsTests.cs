﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcceptanceTestsBridge;
using System.Collections.Generic;

namespace AcceptanceTests.ServerTests
{
    [TestClass]
    public class PostNotificationsTests : UseCaseTestSuite
    {
        public PostNotificationsTests()
            : base()
        {

        }

        // test - the success scenario where the user's friends receive notifications when the user posts a comment
        [TestMethod]
        public void TestCommentFriendsNotificationsSuccess()
        {
            string title = "title1";
            string content = "content1";
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username1 = "user1";
            string friend1 = "friend1";
            string friend2 = "friend2";
            DateTime dateOfBirth1 = DateTime.Now;
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            moderators.Add(username1, DateTime.Today.AddDays(100));
            string subForumName = "sub forum 1";
            string threadPublisher = "publisher1";
            string adminUserName1 = "adm1";
            string adminPass1 = "root1";
            string adminEmail1 = "adm1@gmail.com";
            UserStub user1 = new UserStub(adminUserName1, adminPass1, adminEmail1, forumName);
            List<UserStub> admins = new List<UserStub>();
            admins.Add(user1);
            try
            {
             //   bridge.CreateForum(this.superAdminUsername, forumName,admins, forumPolicy);
                // create a forum, sub-forum and thread.
                int threadID = base.AddThread(forumName, forumPolicy, subForumName, moderators,
                   threadPublisher, title, content);
                int postID = bridge.GetOpenningPostID(forumName, subForumName, threadID);
                bridge.RegisterToForum(forumName, friend1, "passpasspass", friend1 + "@gmail.com", DateTime.Today.AddYears(-30));
                bridge.RegisterToForum(forumName, friend2, "passpasspass", friend2 + "@gmail.com", DateTime.Today.AddYears(-30));

                //add the friends to the user
                bridge.AddFriend(forumName, threadPublisher, friend1);
                bridge.AddFriend(forumName, threadPublisher, friend2);

                // add reply post
                int res = bridge.AddReplyPost(forumName, subForumName, threadID, threadPublisher, postID, title, content);

                //now check that the friends of the user received a notification about the post
                Assert.IsTrue(bridge.IsExistNotificationOfPost(forumName, friend1, res));
                Assert.IsTrue(bridge.IsExistNotificationOfPost(forumName, friend2, res));

            }
            catch (Exception e)
            {
                Assert.Fail();
            }
            finally
            {
                // cleanup
                base.Cleanup(forumName);
            }
        }
        // test - the success scenario where the user's friends receive notifications when the user opens a new thread
        [TestMethod]
        public void TestThreadFriendsNotificationsSuccess()
        {
            string title = "title1";
            string content = "content1";
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username1 = "user1";
            string friend1 = "friend1";
            string friend2 = "friend2";
            DateTime dateOfBirth1 = DateTime.Now;
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            moderators.Add(username1, DateTime.Today.AddDays(100));
            string subForumName = "sub forum 1";
            string threadPublisher = "publisher1";
            try
            {
                //add the friends to the user
                bridge.AddFriend(forumName, threadPublisher, friend1);
                bridge.AddFriend(forumName, threadPublisher, friend2);

                // create a forum, sub-forum and thread.
                int threadID = base.AddThread(forumName, forumPolicy, subForumName, moderators,
                   threadPublisher, title, content);
                int postID = bridge.GetOpenningPostID(forumName, subForumName, threadID);

                //now check that the friends of the user received a notification about the post
                Assert.IsTrue(bridge.IsExistNotificationOfPost(forumName, friend1, postID));//TODO: maybe thread id?
                Assert.IsTrue(bridge.IsExistNotificationOfPost(forumName, friend2, postID));//TODO: same same

            }
            catch (Exception e)
            {
                Assert.Fail();
            }
            finally
            {
                // cleanup
                base.Cleanup(forumName);
            }
        }

        //TODO: test the same with guests


        // test - the success scenario where the users that posted in a thread receive notifications when a user edits a post in it
        [TestMethod]
        public void TestEditPostNotificationsSuccess()
        {
            string title = "title1";
            string content = "content1";
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username1 = "user1";
            string friend1 = "friend1";
            string friend2 = "friend2";
            string friend11 = "friend11";
            DateTime dateOfBirth1 = DateTime.Now;
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            try
            {

                moderators.Add(username1, DateTime.Today.AddDays(100));
                string subForumName = "sub forum 1";
                string threadPublisher = "publisher1";

                // create a forum, sub-forum and thread.
                int threadID = base.AddThread(forumName, forumPolicy, subForumName, moderators,
                   threadPublisher, title, content);
                int postID = bridge.GetOpenningPostID(forumName, subForumName, threadID);

                // add replies posts
                int reply1 = bridge.AddReplyPost(forumName, subForumName, threadID, friend1, postID, title, content);
                int reply2 = bridge.AddReplyPost(forumName, subForumName, threadID, friend2, reply1, title, content);
                int reply11 = bridge.AddReplyPost(forumName, subForumName, threadID, friend2, postID, title, content);

                //edit a post
                bridge.EditPost(forumName,subForumName,threadID, threadPublisher, postID, title, content);

                //now check that the users received a notification about the edition
                Assert.IsTrue(bridge.IsExistNotificationOfPost(forumName,friend1, postID));
                Assert.IsTrue(bridge.IsExistNotificationOfPost(forumName, friend2, postID));
                Assert.IsTrue(bridge.IsExistNotificationOfPost(forumName, friend11, postID));
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
            finally
            {
                // cleanup
                base.Cleanup(forumName);
            }
        }

        // test - the success scenario where the publisher of a thread receives a notification when a user edits a post in it
        [TestMethod]
        public void TestEditPostThreadPublisherNotificationsSuccess()
        {
            string title = "title1";
            string content = "content1";
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username1 = "user1";
            string friend1 = "friend1";
            string friend2 = "friend2";
            DateTime dateOfBirth1 = DateTime.Now;
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            moderators.Add(username1, DateTime.Today.AddDays(100));
            string subForumName = "sub forum 1";
            string threadPublisher = "publisher1";
            try
            {
                // create a forum, sub-forum and thread.
                int threadID = base.AddThread(forumName, forumPolicy, subForumName, moderators,
                   threadPublisher, title, content);
                int postID = bridge.GetOpenningPostID(forumName, subForumName, threadID);

                // add replies posts
                int reply1 = bridge.AddReplyPost(forumName, subForumName, threadID, friend1, postID, title, content);
                int reply2 = bridge.AddReplyPost(forumName, subForumName, threadID, friend2, reply1, title, content);

                //edit a post
                bridge.EditPost(forumName,subForumName,threadID,friend1, reply1, title, content);

                //now check that the thread publisher received a notification about the edition
                Assert.IsTrue(bridge.IsExistNotificationOfPost(forumName, threadPublisher, reply1));

                //edit a post
                bridge.EditPost(forumName, subForumName, threadID,friend2, reply2, title, content);

                //now check that the thread publisher received a notification about the edition
                Assert.IsTrue(bridge.IsExistNotificationOfPost(forumName, threadPublisher, reply2));
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
            finally
            {
                // cleanup
                base.Cleanup(forumName);
            }
        }

        [TestMethod]
        public void TestDeletePostNotificationsSuccess()
        {
            string title = "title1";
            string content = "content1";
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username1 = "user1";
            string friend1 = "friend1";
            string friend2 = "friend2";
            string friend11 = "friend11";
            DateTime dateOfBirth1 = DateTime.Now;
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            try
            {

                moderators.Add(username1, DateTime.Today.AddDays(100));
                string subForumName = "sub forum 1";
                string threadPublisher = "publisher1";

                // create a forum, sub-forum and thread.
                int threadID = base.AddThread(forumName, forumPolicy, subForumName, moderators,
                   threadPublisher, title, content);
                int postID = bridge.GetOpenningPostID(forumName, subForumName, threadID);

                // add replies posts
                int reply1 = bridge.AddReplyPost(forumName, subForumName, threadID, friend1, postID, title, content);
                int reply2 = bridge.AddReplyPost(forumName, subForumName, threadID, friend2, reply1, title, content);
                int reply11 = bridge.AddReplyPost(forumName, subForumName, threadID, friend2, postID, title, content);

                //delete a post
                bridge.DeletePost(forumName,subForumName,threadID,friend1, reply1);

                //now check that the users received a notification about the edition
                Assert.IsFalse(bridge.IsExistNotificationOfPost(forumName, friend2, reply1));//notify only those who commented the deleted post
                Assert.IsFalse(bridge.IsExistNotificationOfPost(forumName, threadPublisher, reply1));
                Assert.IsTrue(bridge.IsExistNotificationOfPost(forumName, friend11, reply1));
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
            finally
            {
                // cleanup
                base.Cleanup(forumName);
            }
        }

        [TestMethod]
        public void TestDeleteOpeningPostNotificationsSuccess()
        {
            string title = "title1";
            string content = "content1";
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username1 = "user1";
            string friend1 = "friend1";
            string friend2 = "friend2";
            string friend11 = "friend11";
            DateTime dateOfBirth1 = DateTime.Now;
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            try
            {

                moderators.Add(username1, DateTime.Today.AddDays(100));
                string subForumName = "sub forum 1";
                string threadPublisher = "publisher1";

                // create a forum, sub-forum and thread.
                int threadID = base.AddThread(forumName, forumPolicy, subForumName, moderators,
                   threadPublisher, title, content);
                int postID = bridge.GetOpenningPostID(forumName, subForumName, threadID);

                // add replies posts
                int reply1 = bridge.AddReplyPost(forumName, subForumName, threadID, friend1, postID, title, content);
                int reply2 = bridge.AddReplyPost(forumName, subForumName, threadID, friend2, reply1, title, content);
                int reply11 = bridge.AddReplyPost(forumName, subForumName, threadID, friend2, postID, title, content);

                //delete a post
                bridge.DeletePost(forumName,subForumName,threadID,threadPublisher, postID);


                //now check that the users received a notification about the edition
                Assert.IsTrue(bridge.IsExistNotificationOfPost(forumName,friend1, reply1));
                Assert.IsTrue(bridge.IsExistNotificationOfPost(forumName, friend2, reply1));
                Assert.IsTrue(bridge.IsExistNotificationOfPost(forumName, friend11, reply1));
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
            finally
            {
                // cleanup
                base.Cleanup(forumName);
            }
        }
    }
}
