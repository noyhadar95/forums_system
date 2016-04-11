using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AcceptanceTests.ServerTests
{
    [TestClass]
    public class CreateSubForumTests : UseCaseTestSuite
    {

        public CreateSubForumTests()
            : base()
        {

        }

        // test the success main scenario
        [TestMethod]
        public void TestCreateSubForumSuccess()
        {
            string forumName = "forum1";
            string forumProperties = "";
            string username1 = "user123";
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            moderators.Add(username1, new DateTime());
            string subForumName = "sub forum 123";

            bool res = base.CreateSubForum(forumName, forumProperties, subForumName, moderators);
            Assert.IsTrue(res);

            // check that the sub-forum now exists in the sytem
            Assert.IsTrue(bridge.IsExistForum(forumName));

            // check that every moderator in moderators list is a moderator in the sub-forum
            foreach (KeyValuePair<string, DateTime> mod in moderators)
            {
                Assert.IsTrue(bridge.IsModerator(forumName, subForumName, mod.Key));
            }

            // cleanup
            base.DeleteForum(forumName);
        }

        // test the failure scenario
        [TestMethod]
        public void TestCreateSubForumFailure()
        {
            string forumName = "forum1";
            string forumProperties = "";
            string username1 = "user123";
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            moderators.Add(username1, new DateTime());
            string subForumName = "sub forum 123";

            base.CreateForum(forumName, forumProperties);
            // make sure username is not a valid user in the forum
            bridge.DeleteUser(forumName, username1);

            bool res = bridge.CreateSubForum(this.adminUserName1, forumName, subForumName, moderators);

            Assert.IsTrue(!res);

            // cleanup
            base.DeleteForum(forumName);
        }


        //TODO: test failure with bad props and with bad moderators (i.e. usernames of users that are not in the forum)



    }
}
