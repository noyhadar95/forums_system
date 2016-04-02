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

        [TestMethod]
        public void TestCreateForumAdminExist()
        {
            // test the success main scenario with an existing admin

            string adminUserName = "admin1";
            string forumProperties = "";

            //TODO: make sure admin1 is a valid user in the system
            int res = bridge.CreateForum(adminUserName, forumProperties);

            Assert.IsTrue(res > 0);
            //TODO: check that the forum is now in the sytem
            //TODO: MAYBE check that the forum has an admin and that he is a member in the forum

        }

        [TestMethod]
        public void TestCreateForumAdminNotExist()
        {
            // test the failure scenario, try to create a forum with an admin
            // that is not a user in the system

            string adminUserName = "admin2";
            string forumProperties = "";

            //TODO: make sure admin2 not a user in the system
            int res = bridge.CreateForum(adminUserName, forumProperties);

            Assert.IsTrue(res > 0);

        }

        [TestMethod]
        public void TestCreateForumAdminIsMember()
        {
            // checks if the admin of the created forum is also a member on that forum

            string adminUserName = "admin2";
            string forumProperties = "";

            //TODO: make sure admin2 not a user in the system
            int res = bridge.CreateForum(adminUserName, forumProperties);

            Assert.IsTrue(res > 0);

        }


        //TODO: add tests for success/failure forum properties


    }
}
