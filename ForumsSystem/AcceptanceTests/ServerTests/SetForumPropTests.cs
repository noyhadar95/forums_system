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

            string forumName = "";
            string adminUserName = "";
            string adminPass = "";
            string forumProperties = "";
            string newForumProperties = "";

            //TODO: make sure forumProperties are valid

            // make sure admin is a valid user in the system
            bridge.AddUser(adminUserName, adminPass);
            Assert.IsTrue(bridge.IsExistUser(adminUserName));
            // make sure the forum forumName exists in the system
            bridge.CreateForum(forumName, adminUserName, forumProperties);
            Assert.IsTrue(bridge.IsExistForum(forumName));

            bool res = bridge.SetForumProperties(forumName, newForumProperties);

            Assert.IsTrue(res);
            // check that the forum props have changed
            // Assert.IsTrue(newForumProperties == bridge.GetForumProps(forumName));

            // clean up
            bridge.DeleteUser(adminUserName);
            bridge.DeleteForum(forumName);
        }


        //TODO: test invalid forum props


    }
}
