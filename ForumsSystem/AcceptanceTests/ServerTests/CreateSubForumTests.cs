using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using AcceptanceTestsBridge;

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
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username1 = "user123";
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            moderators.Add(username1, DateTime.Today.AddDays(100));
            string subForumName = "sub forum 123";

            bool res = base.CreateSubForumByAdmin1(forumName, forumPolicy, subForumName, moderators);
            Assert.IsTrue(res);

            // check that the sub-forum now exists in the sytem
            Assert.IsTrue(bridge.IsExistForum(forumName));

            // check that every moderator in moderators list is a moderator in the sub-forum
            foreach (KeyValuePair<string, DateTime> mod in moderators)
            {
                Assert.IsTrue(bridge.IsModerator(forumName, subForumName, mod.Key));
            }

            // cleanup
            base.Cleanup(forumName);
        }

        // test the failure scenario - with bad moderators (i.e. usernames of users that are not in the forum)
        [TestMethod]
        public void TestCreateSubForumFailure()
        {
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username1 = "user123";
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            moderators.Add(username1, DateTime.Today.AddDays(100));
            string subForumName = "sub forum 123";

            base.CreateForum(forumName, forumPolicy);
            // make sure username is not a valid user in the forum
            bridge.DeleteUser(forumName, username1);

            bool res = bridge.CreateSubForum(this.adminUserName1, forumName, subForumName, moderators);

            Assert.IsTrue(!res);

            // cleanup
            base.Cleanup(forumName);
        }


    }
}
