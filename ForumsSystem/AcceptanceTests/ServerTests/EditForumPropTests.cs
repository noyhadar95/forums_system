using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

        // test the success main scenario with valid forum props
        [TestMethod]
        public void TestEditForumPropValid()
        {
            string forumName = "";
            string forumProperties = "";
            string newForumProperties = "";

            base.CreateForum(forumName, forumProperties);

            //TODO: make sure forumProperties are valid

            bool res = bridge.SetForumProperties(forumName, newForumProperties);

            Assert.IsTrue(res);
            //TODO: check that the forum props have changed
            // Assert.IsTrue(newForumProperties == bridge.GetForumProps(forumName));

            // cleanup
            base.DeleteForum(forumName);
        }

        //TODO: test invalid forum props


    }
}
