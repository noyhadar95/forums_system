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

        [TestMethod]
        public void TestCreateSubForumSuccess()
        {
            // test the success main scenario

            string forumName = "";
            List<string> moderatos = new List<string>();
            string properties = "";

            bool res = bridge.CreateSubForum(forumName, moderatos, properties);

            Assert.IsTrue(res);

            //TODO: make sure the new sub-forum is created with all moderators
        }

        [TestMethod]
        public void TestCreateSubForumFailure()
        {
            // test the success main scenario

            string forumName = "";
            List<string> moderatos = new List<string>();
            string properties = "";

            bool res = bridge.CreateSubForum(forumName, moderatos, properties);

            Assert.IsTrue(!res);

            //TODO: make sure the new sub-forum is created with all moderators
        }

        //TODO: test failure with bad props and with bad moderators (i.e. usernames of users that are not in the forum)

    }
}
