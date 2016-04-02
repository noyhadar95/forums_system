using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AcceptanceTests.ServerTests
{
    [TestClass]
    public class SetForumPropTests : UseCaseTestSuite
    {
        public SetForumPropTests()
            : base()
        {

        }

        [TestMethod]
        public void TestSetForumPropValid()
        {
            // test the success main scenario with valid forum props

            string forumProperties = "";

            //TODO: make sure forumProperties are valid
            int res = bridge.SetForumProperties(forumProperties);

            Assert.IsTrue(res > 0);
            //TODO: check that the forum props have changed

        }


        //TODO: test invalid forum props


    }
}
