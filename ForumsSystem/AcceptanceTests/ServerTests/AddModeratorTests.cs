using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using AcceptanceTestsBridge;

namespace AcceptanceTests.ServerTests
{
    [TestClass]
    public class AddModeratorTests : UseCaseTestSuite
    {

        public AddModeratorTests()
            : base()
        {

        }

       

        // test - check that add moderator with correct info works successfully
        [TestMethod]
        public void TestAddModerator()
        {
            string forumName = "forum1";
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username1 = "user1";
            string username2 = "user2", pass2 = "passwd2", email2 = "user2@gmail.com";
            DateTime dateOfBirth1 = DateTime.Now, dateOfBirth2 = DateTime.Now;
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            moderators.Add(username1, DateTime.Today.AddDays(100));
            string subForumName = "sub forum 1";

            // create a forum, sub-forum and a thread to add a post to.
            base.CreateSubForum(forumName, forumPolicy, subForumName, moderators);
            bridge.RegisterToForum(forumName, username2, pass2, email2, dateOfBirth2);
            KeyValuePair<string, DateTime> newMod = new KeyValuePair<string, DateTime>(username2, DateTime.Today.AddDays(100));
            bool res = bridge.AddModerator(forumName, subForumName, this.adminUserName1, newMod);
            Assert.IsTrue(res);
            Assert.IsTrue(bridge.IsModerator(forumName, subForumName, username2));

            // cleanup
            //string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            //string className = this.GetType().Name;
            //if (bridge.ShouldTear(className, methodName))
                base.Cleanup(forumName);
        }
    }
}
