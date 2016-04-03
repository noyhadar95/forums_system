using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AcceptanceTests.ServerTests
{
    [TestClass]
    public class CreateForumTests : UseCaseTestSuite
    {
        public CreateForumTests()
            : base()
        {
        }

        // test the success main scenario with an existing admin
        [TestMethod]
        public void TestCreateForumAdminExist()
        {
            string forumName = "";
            string adminUserName = "";
            string adminPass = "";
            string forumProperties = "";

            // make sure admin is a valid user in the system
            bridge.AddUser(adminUserName, adminPass);
            Assert.IsTrue(bridge.IsExistUser(adminUserName));

            bool res = bridge.CreateForum(forumName, adminUserName, forumProperties);

            Assert.IsTrue(res);
            // check that the forum now exists in the sytem
            Assert.IsTrue(bridge.IsExistForum(forumName));

            //TODO: MAYBE check that the forum has an admin and that he is a member in the forum


            // clean up
            bridge.DeleteUser(adminUserName);
            bridge.DeleteForum(forumName);
        }

        // test the failure scenario, try to create a forum with an admin
        // that is not a user in the system
        [TestMethod]
        public void TestCreateForumAdminNotExist()
        {
            string forumName = "";
            string adminUserName = "";
            string forumProperties = "";

            // make sure admin is not a user in the system
            bridge.DeleteUser(adminUserName);
            Assert.IsTrue(!bridge.IsExistUser(adminUserName));

            bool res = bridge.CreateForum(forumName, adminUserName, forumProperties);

            Assert.IsTrue(!res);
            // check that the forum doesn't exist in the sytem
            bool isExistForum = bridge.IsExistForum(forumName);
            Assert.IsTrue(!isExistForum);


            // clean up
            if (isExistForum)
                bridge.DeleteForum(forumName);
        }


        //TODO: add tests for success/failure forum properties


    }
}
