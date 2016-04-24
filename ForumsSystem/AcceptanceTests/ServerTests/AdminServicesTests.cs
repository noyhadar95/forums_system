using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using AcceptanceTestsBridge;

namespace AcceptanceTests.ServerTests
{
    [TestClass]
    public class AdminServicesTests : UseCaseTestSuite
    {
        public AdminServicesTests()
            : base()
        {

        }

        [TestMethod]
        public void TestRemoveModeratorBeforeExipartionSuccess()
        {
            string forumName = "forum1";
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username1 = "user1";
            string username2 = "user2", pass2 = "passwd2", email2 = "user2@gmail.com";
            DateTime dateOfBirth1 = DateTime.Now, dateOfBirth2 = DateTime.Now;
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            moderators.Add(username1, DateTime.Today.AddDays(100));
            string subForumName = "sub forum 1";

            try
            {
                // create a forum, sub-forum and a thread to add a post to.
                base.CreateSubForum(forumName, forumPolicy, subForumName, moderators);
                bridge.RegisterToForum(forumName, username2, pass2, email2, dateOfBirth2);
                KeyValuePair<string, DateTime> newMod = new KeyValuePair<string, DateTime>(username2, DateTime.Today.AddDays(100));
                bridge.AddModerator(forumName, subForumName, this.adminUserName1, newMod);
                //remove the moderator:
                bool res = bridge.RemoveModerator(forumName, subForumName, this.adminUserName1, username2);

                Assert.IsTrue(res);
                Assert.IsFalse(bridge.IsModerator(forumName, subForumName, username2));
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
            finally
            {
                base.Cleanup(forumName);
            }
        }

        //test an attempt of a moderator to remove another moderator from the subforum, which should fail
        [TestMethod]
        public void TestRemoveModeratorBeforeExipartionFailure()
        {
            string forumName = "forum1";
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username1 = "user1";
            string username2 = "user2", pass2 = "passwd2", email2 = "user2@gmail.com";
            DateTime dateOfBirth1 = DateTime.Now, dateOfBirth2 = DateTime.Now;
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            moderators.Add(username1, DateTime.Today.AddDays(100));
            string subForumName = "sub forum 1";

            try
            {
                // create a forum, sub-forum and a thread to add a post to.
                base.CreateSubForum(forumName, forumPolicy, subForumName, moderators);
                bridge.RegisterToForum(forumName, username2, pass2, email2, dateOfBirth2);
                KeyValuePair<string, DateTime> newMod = new KeyValuePair<string, DateTime>(username2, DateTime.Today.AddDays(100));
                bridge.AddModerator(forumName, subForumName, this.adminUserName1, newMod);
                //remove the moderator:
                bool res = bridge.RemoveModerator(forumName, subForumName, username1, username2);

                Assert.IsFalse(res);
                Assert.IsTrue(bridge.IsModerator(forumName, subForumName, username2));
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
            finally
            {
                base.Cleanup(forumName);
            }
        }

        [TestMethod]
        public void TestGetNumOfPostsOfMemberInForum()
        {
            string forumName = "forum1";
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username1 = "user1";
            string username2 = "user2", pass2 = "passwd2", email2 = "user2@gmail.com";
            DateTime dateOfBirth1 = DateTime.Now, dateOfBirth2 = DateTime.Now;
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            moderators.Add(username1, DateTime.Today.AddDays(100));
            string subForumName = "sub forum 1";
            string subForumName2 = "sub forum 2";
            string title = "title";
            string content = "content";
            try
            {
                // create a forum, sub-forum and a thread to add a post to.
                base.CreateSubForum(forumName, forumPolicy, subForumName, moderators);
                bridge.RegisterToForum(forumName, username2, pass2, email2, dateOfBirth2);
                int numOfposts = bridge.GetNumOfPostsInForumByMember(forumName, this.adminUserName1, email2);
                Assert.IsTrue(numOfposts == 0);
                int threadId=bridge.AddThread(forumName, subForumName, username2, title, content);
                numOfposts = bridge.GetNumOfPostsInForumByMember(forumName, this.adminUserName1, email2);
                Assert.IsTrue(numOfposts == 1);
                int postId = bridge.GetOpenningPostID(forumName, subForumName, threadId);
                bridge.AddReplyPost(forumName, subForumName, threadId, username2, postId, title, content);
                numOfposts = bridge.GetNumOfPostsInForumByMember(forumName, this.adminUserName1, email2);
                Assert.IsTrue(numOfposts == 2);

                //and now check that it works with more than 1 subforum
                base.CreateSubForum(forumName, forumPolicy, subForumName2, moderators);
                bridge.RegisterToForum(forumName, username2, pass2, email2, dateOfBirth2);

                threadId = bridge.AddThread(forumName, subForumName2, username2, title, content);
                numOfposts = bridge.GetNumOfPostsInForumByMember(forumName, this.adminUserName1, email2);
                Assert.IsTrue(numOfposts == 3);
                postId = bridge.GetOpenningPostID(forumName, subForumName2, threadId);
                bridge.AddReplyPost(forumName, subForumName2, threadId, username2, postId, title, content);
                numOfposts = bridge.GetNumOfPostsInForumByMember(forumName, this.adminUserName1, email2);
                Assert.IsTrue(numOfposts == 4);

            }
            catch (Exception e)
            {
                Assert.Fail();
            }
            finally
            {
                base.Cleanup(forumName);
            }
        }

        [TestMethod]
        public void TestModeratorsList()
        {
        }

        [TestMethod]
        public void TestModeratorsAppointmentsDetails()//date,admin,subforum
        {
        }

        [TestMethod]
        public void TestModeratorsPosts()
        {
        }
    }
}
