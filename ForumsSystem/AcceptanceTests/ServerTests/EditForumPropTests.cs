using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcceptanceTestsBridge;

namespace AcceptanceTests.ServerTests
{
    // this class tests the Edit Forum Properties Use Case
    // i.e. check that after calling SetForumProperties(...) the forum properties have been set
    [TestClass]
    public class EditForumPropTests : UseCaseTestSuite
    {
        public EditForumPropTests()
            : base()
        {

        }

        // test - edit forum props to MinimumAge policy
        [TestMethod]
        public void TestEditForumPropMinAge()
        {
            string forumName = "";
            PoliciesStub forumPolicy = PoliciesStub.Password;
            PoliciesStub newForumPolicy = PoliciesStub.MinimumAge;

            base.CreateForum(forumName, forumPolicy);

            bool res = bridge.SetForumProperties(forumName, this.adminUserName1, newForumPolicy);

            Assert.IsTrue(res);
            // check that the forum props have been changed to newForumPolicy
            Assert.IsTrue(bridge.IsForumHasPolicy(forumName, newForumPolicy));

            // cleanup
            base.Cleanup(forumName);
        }

        // test - edit forum props to MaxModerators policy
        [TestMethod]
        public void TestEditForumPropMaxModerators()
        {
            string forumName = "";
            PoliciesStub forumPolicy = PoliciesStub.Password;
            PoliciesStub newForumPolicy = PoliciesStub.MaxModerators;

            base.CreateForum(forumName, forumPolicy);

            bool res = bridge.SetForumProperties(forumName, this.adminUserName1, newForumPolicy);

            Assert.IsTrue(res);
            // check that the forum props have been changed to newForumPolicy
            Assert.IsTrue(bridge.IsForumHasPolicy(forumName, newForumPolicy));

            // cleanup
            base.Cleanup(forumName);
        }

        // // test - edit forum props to MemberSuspension policy
        [TestMethod]
        public void TestEditForumPropMemberSuspension()
        {
            string forumName = "";
            PoliciesStub forumPolicy = PoliciesStub.Password;
            PoliciesStub newForumPolicy = PoliciesStub.MemberSuspension;

            base.CreateForum(forumName, forumPolicy);

            bool res = bridge.SetForumProperties(forumName, this.adminUserName1, newForumPolicy);

            Assert.IsTrue(res);
            // check that the forum props have been changed to newForumPolicy
            Assert.IsTrue(bridge.IsForumHasPolicy(forumName, newForumPolicy));

            // cleanup
            base.Cleanup(forumName);
        }



    }
}
