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
            string forumName = GetNextForum();
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
                base.CreateSubForumByAdmin1(forumName, forumPolicy, subForumName, moderators);
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
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username1 = "user1", pass1 = "passwd2", email1 = "user2@gmail.com";
            string username2 = "user2", pass2 = "passwd2", email2 = "user2@gmail.com";
            DateTime dateOfBirth1 = DateTime.Now, dateOfBirth2 = DateTime.Now;
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            moderators.Add(username1, DateTime.Today.AddDays(100));
            string subForumName = "sub forum 1";

            try
            {
                base.CreateForum(forumName, forumPolicy);
                bridge.RegisterToForum(forumName, username1, pass1, email1, dateOfBirth1);
                bridge.LoginUser(forumName, username1, pass1);
                // create a forum, sub-forum and a thread to add a post to.
                base.CreateSubForumByAdmin1(forumName, forumPolicy, subForumName, moderators);
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
        //test an attempt of an admin which didnt appoint the moderator 
        //to remove him from the subforum, which should fail
        [TestMethod]
        public void TestRemoveModeratorBeforeExipartionFailureAdmin()
        {
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username1 = "user1", pass1 = "passwd2", email1 = "user2@gmail.com";
            string username2 = "user2", pass2 = "passwd2", email2 = "user2@gmail.com";
            DateTime dateOfBirth1 = DateTime.Now, dateOfBirth2 = DateTime.Now;
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            moderators.Add(username1, DateTime.Today.AddDays(100));
            moderators.Add(username2, DateTime.Today.AddDays(100));
            string subForumName = "sub forum 1";
            string adminUserName1 = "ad1", adminPass1 = "pass", adminEmail1 = adminUserName1 + "@gmail.com";
            string adminUserName2 = "ad1", adminPass2 = "pass", adminEmail2 = adminUserName2 + "@gmail.com";
            List<UserStub> admins = new List<UserStub>();
            UserStub user1 = new UserStub(adminUserName1, adminPass1, adminEmail1, forumName);
            UserStub user2 = new UserStub(adminUserName2, adminPass2, adminEmail2, forumName);
            admins.Add(user1);
            admins.Add(user2);

            try
            {
                bridge.CreateForum(this.superAdminUsername,this.superAdminPass, forumName, admins, forumPolicy);
                bridge.RegisterToForum(forumName, username1, pass1, email1, dateOfBirth1);
                bridge.LoginUser(forumName, username1, pass1);
                bridge.LoginUser(forumName, adminUserName1, adminPass1);
                bridge.LoginUser(forumName, adminUserName2, adminPass2);
                // create a forum, sub-forum and a thread to add a post to.
                base.CreateSubForumByAdmin1(forumName, forumPolicy, subForumName, moderators);
                bridge.RegisterToForum(forumName, username2, pass2, email2, dateOfBirth2);
                //KeyValuePair<string, DateTime> newMod = new KeyValuePair<string, DateTime>(username2, DateTime.Today.AddDays(100));
                //bridge.AddModerator(forumName, subForumName, adminUserName1, newMod);
                //remove the moderator:
                bool res = bridge.RemoveModerator(forumName, subForumName, adminUserName2, username2);

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
        //test an attempt to remove the only moderator of a subforum
        [TestMethod]
        public void TestRemoveModeratorBeforeExipartionFailureOnlyModerator()
        {

            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username1 = "user1";
            DateTime dateOfBirth1 = DateTime.Now, dateOfBirth2 = DateTime.Now;
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            moderators.Add(username1, DateTime.Today.AddDays(100));
            string subForumName = "sub forum 1";

            try
            {
                // create a forum, sub-forum and a thread to add a post to.
                base.CreateSubForumByAdmin1(forumName, forumPolicy, subForumName, moderators);
                //try to remove the only moderator:
                bool res = bridge.RemoveModerator(forumName, subForumName, this.adminUserName1, username1);

                Assert.IsFalse(res);
                Assert.IsTrue(bridge.IsModerator(forumName, subForumName, username1));
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
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username1 = "user1", pass1 = "passwd1", email1 = "user1@gmail.com";
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
                CreateForum(forumName);
                bridge.RegisterToForum(forumName, username1, pass1, email1, dateOfBirth1);
                bridge.ConfirmRegistration(forumName, username1);
                bridge.LoginUser(forumName, username1, pass1);
                bridge.RegisterToForum(forumName, username2, pass2, email2, dateOfBirth2);
                bridge.ConfirmRegistration(forumName, username2);
                bridge.LoginUser(forumName, username2, pass2);
                // create a forum, sub-forum and a thread to add a post to.
                base.CreateSubForumByAdmin1(forumName, forumPolicy, subForumName, moderators);
                bridge.RegisterToForum(forumName, username2, pass2, email2, dateOfBirth2);
                int numOfposts = bridge.GetNumOfPostsInForumByMember(forumName, this.adminUserName1, username2);
                Assert.IsTrue(numOfposts == 0);
                int threadId=bridge.AddThread(forumName, subForumName, username2, title, content);
                numOfposts = bridge.GetNumOfPostsInForumByMember(forumName, this.adminUserName1, username2);
                Assert.IsTrue(numOfposts == 1);
                int postId = bridge.GetOpenningPostID(forumName, subForumName, threadId);
                bridge.AddReplyPost(forumName, subForumName, threadId, username2, postId, title, content);
                numOfposts = bridge.GetNumOfPostsInForumByMember(forumName, this.adminUserName1, username2);
                Assert.IsTrue(numOfposts == 2);

                //and now check that it works with more than 1 subforum
                base.CreateSubForumByAdmin1(forumName, forumPolicy, subForumName2, moderators);
                bridge.RegisterToForum(forumName, username2, pass2, email2, dateOfBirth2);

                threadId = bridge.AddThread(forumName, subForumName2, username2, title, content);
                numOfposts = bridge.GetNumOfPostsInForumByMember(forumName, this.adminUserName1, username2);
                Assert.IsTrue(numOfposts == 3);
                postId = bridge.GetOpenningPostID(forumName, subForumName2, threadId);
                bridge.AddReplyPost(forumName, subForumName2, threadId, username2, postId, title, content);
                numOfposts = bridge.GetNumOfPostsInForumByMember(forumName, this.adminUserName1, username2);
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
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username1 = "user1";
            string username2 = "user2", pass2 = "passwd2", email2 = "user2@gmail.com";
            string username3 = "user3", pass3 = "passwd3", email3 = "user3@gmail.com";
            DateTime dateOfBirth1 = DateTime.Now, dateOfBirth2 = DateTime.Now, dateOfBirth3 = DateTime.Now;
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            moderators.Add(username1, DateTime.Today.AddDays(100));
            string subForumName = "sub forum 1";

            try
            {
                // create a forum, sub-forum and a thread to add a post to.
                base.CreateSubForumByAdmin1(forumName, forumPolicy, subForumName, moderators);
                bridge.RegisterToForum(forumName, username2, pass2, email2, dateOfBirth2);
                KeyValuePair<string, DateTime> newMod = new KeyValuePair<string, DateTime>(username2, DateTime.Today.AddDays(100));
                bridge.AddModerator(forumName, subForumName, this.adminUserName1, newMod);
                bridge.RegisterToForum(forumName, username3, pass3, email3, dateOfBirth3);
                newMod = new KeyValuePair<string, DateTime>(username3, DateTime.Today.AddDays(100));
                bridge.AddModerator(forumName, subForumName, this.adminUserName1, newMod);
                List<string> moderatorsList = bridge.GetListOfModerators(forumName, subForumName, this.adminUserName1);
                Assert.IsTrue(moderatorsList.Count == 3);
                Assert.IsTrue(moderatorsList.Contains(username1));
                Assert.IsTrue(moderatorsList.Contains(username2));
                Assert.IsTrue(moderatorsList.Contains(username3));
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
        public void TestModeratorsAppointmentsDetails()//date,admin,subforum
        {
             string forumName = GetNextForum();
             PoliciesStub forumPolicy = PoliciesStub.Password;
             string username1 = "user1";
             string username2 = "user2", pass2 = "passwd2", email2 = "user2@gmail.com";
             string username3 = "user3", pass3 = "passwd3", email3 = "user3@gmail.com";
             DateTime dateOfBirth1 = DateTime.Now, dateOfBirth2 = DateTime.Now, dateOfBirth3 = DateTime.Now;
             Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
             moderators.Add(username1, DateTime.Today.AddDays(100));
             string subForumName = "sub forum 1";

             try
             {
                 // create a forum, sub-forum and a thread to add a post to.
                 base.CreateSubForumByAdmin1(forumName, forumPolicy, subForumName, moderators);
                 bridge.RegisterToForum(forumName, username2, pass2, email2, dateOfBirth2);
                 KeyValuePair<string, DateTime> newMod = new KeyValuePair<string, DateTime>(username2, DateTime.Today.AddDays(100));
                 bridge.AddModerator(forumName, subForumName, this.adminUserName1, newMod);
                 bridge.RegisterToForum(forumName, username3, pass3, email3, dateOfBirth3);
                 newMod = new KeyValuePair<string, DateTime>(username3, DateTime.Today.AddDays(100));
                 bridge.AddModerator(forumName, subForumName, this.adminUserName1, newMod);
                 Tuple<string,string,DateTime,string> moderatorsList = bridge.GetModeratorAppointmentsDetails(forumName, subForumName, this.adminUserName1,username1);
                /* Assert.IsTrue(moderatorsList.Count == 3);
                 Assert.IsTrue(moderatorsList.Contains(username1));
                 Assert.IsTrue(moderatorsList.Contains(username2));
                 Assert.IsTrue(moderatorsList.Contains(username3));
                 */
             }
             catch (Exception e)
             {
                 Assert.Fail();
             }
             finally
             {
                 base.Cleanup(forumName);
             }
             
            Assert.Fail("Not Yet Implemented");
        }
        [TestMethod]
        public void TestUserPosts()
        {
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username1 = "user1", pass1 = "passwd1", email1 = "user1@gmail.com";
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
               /* CreateForum(forumName);
                bridge.RegisterToForum(forumName, username1, pass1, email1, dateOfBirth1);
                bridge.ConfirmRegistration(forumName, username1);
                bridge.LoginUser(forumName, username1, pass1);
                bridge.RegisterToForum(forumName, username2, pass2, email2, dateOfBirth2);
                bridge.ConfirmRegistration(forumName, username2);
                bridge.LoginUser(forumName, username2, pass2);*/

                // create a forum, sub-forum and a thread to add a post to.
                base.CreateSubForumByAdmin1(forumName, forumPolicy, subForumName, moderators);
                List<Tuple<int, string, string>> posts = bridge.GetPostsInForumByUser(forumName, this.adminUserName1, username2);
                Assert.IsTrue(posts.Count == 0);
                bridge.LoginUser(forumName, username1, pass1);
                bridge.LoginUser(forumName, username2, pass2);
                List<Tuple<int, string, string>> posts = bridge.GetPostsInForumByUser(forumName, subForumName, this.adminUserName1, username2);
                Assert.IsNull(posts.Count == 0);
                int threadId = bridge.AddThread(forumName, subForumName, username2, title, content);
                posts = bridge.GetPostsInForumByUser(forumName, this.adminUserName1, username2);
                Assert.IsTrue(posts.Count == 1);
                int postId = bridge.GetOpenningPostID(forumName, subForumName, threadId);
                Assert.IsTrue(posts.ToArray()[0].Item1 == postId);
                bridge.AddReplyPost(forumName, subForumName, threadId, username2, postId, title, content);
                posts = bridge.GetPostsInForumByUser(forumName, this.adminUserName1, username2);
                Assert.IsTrue(posts.Count == 2);

                //and now check that it works with more than 1 subforum
                base.CreateSubForumByAdmin1(forumName, forumPolicy, subForumName2, moderators);
                threadId = bridge.AddThread(forumName, subForumName2, username2, title, content);
                posts = bridge.GetPostsInForumByUser(forumName, this.adminUserName1, username2);
                Assert.IsTrue(posts.Count == 3);
                postId = bridge.GetOpenningPostID(forumName, subForumName2, threadId);
                bridge.AddReplyPost(forumName, subForumName2, threadId, username2, postId, title, content);
                posts = bridge.GetPostsInForumByUser(forumName, this.adminUserName1, username2);
                Assert.IsTrue(posts.Count == 4);

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
         public void TestModeratorsPosts()
         {
             string forumName = GetNextForum();
             PoliciesStub forumPolicy = PoliciesStub.Password;
             string username1 = "user1", pass1 = "passwd1", email1 = "user1@gmail.com";
            string username2 = "user2", pass2 = "passwd2", email2 = "user2@gmail.com";
            DateTime dateOfBirth1 = DateTime.Now.AddYears(-30), dateOfBirth2 = DateTime.Now.AddYears(-30);
             Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
             moderators.Add(username1, DateTime.Today.AddDays(100));
             moderators.Add(username2, DateTime.Today.AddDays(100));
             string subForumName = "sub forum 1";
             string subForumName2 = "sub forum 2";
             string title = "title";
             string content = "content";
             try
             {
                CreateForum(forumName);
                bridge.RegisterToForum(forumName, username1, pass1, email1, dateOfBirth1);
                bridge.ConfirmRegistration(forumName, username1);
                bridge.LoginUser(forumName, username1, pass1);
                bridge.RegisterToForum(forumName, username2, pass2, email2, dateOfBirth2);
                bridge.ConfirmRegistration(forumName, username2);
                bridge.LoginUser(forumName, username2, pass2);

                // create a forum, sub-forum and a thread to add a post to.
                base.CreateSubForumByAdmin1(forumName, forumPolicy, subForumName, moderators);
                 List<Tuple<int,string,string>> posts = bridge.GetPostsInForumByUser(forumName, this.adminUserName1, username2);
                 Assert.IsTrue(posts.Count==0);
                 int threadId = bridge.AddThread(forumName, subForumName, username2, title, content);
                 posts = bridge.GetPostsInForumByUser(forumName, this.adminUserName1, username2);
                 Assert.IsTrue(posts.Count == 1);
                 int postId = bridge.GetOpenningPostID(forumName, subForumName, threadId);
                 Assert.IsTrue(posts.ToArray()[0].Item1 == postId);
                 bridge.AddReplyPost(forumName, subForumName, threadId, username2, postId, title, content);
                 posts = bridge.GetPostsInForumByUser(forumName, this.adminUserName1, username2);
                 Assert.IsTrue(posts.Count == 2);

                 //and now check that it works with more than 1 subforum
                 base.CreateSubForumByAdmin1(forumName, forumPolicy, subForumName2, moderators);
                 threadId = bridge.AddThread(forumName, subForumName2, username2, title, content);
                 posts = bridge.GetPostsInForumByUser(forumName, this.adminUserName1, username2);
                 Assert.IsTrue(posts.Count == 3);
                 postId = bridge.GetOpenningPostID(forumName, subForumName2, threadId);
                 bridge.AddReplyPost(forumName, subForumName2, threadId, username2, postId, title, content);
                 posts = bridge.GetPostsInForumByUser(forumName, this.adminUserName1, username2);
                 Assert.IsTrue(posts.Count == 4);

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
    }
}
