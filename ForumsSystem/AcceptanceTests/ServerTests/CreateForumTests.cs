﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using AcceptanceTestsBridge;

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
            string forumName = "forum9";
            string adminUserName1 = "adm1", adminUserName2 = "adm2";
            string adminPass1 = "root1", adminPass2 = "root2";
            string adminEmail1 = "adm1@gmail.com", adminEmail2 = "adm2@gmail.com";
            string forumProperties = "";
            List<UserStub> admins = new List<UserStub>();
            UserStub user1 = new UserStub(adminUserName1, adminPass1, adminEmail1, forumName);
            UserStub user2 = new UserStub(adminUserName2, adminPass2, adminEmail2, forumName);
            admins.Add(user1);
            admins.Add(user2);

            // create the forum
            bool res = bridge.CreateForum(forumName, admins, forumProperties);
            Assert.IsTrue(res);
            // check that the forum now exists in the sytem
            Assert.IsTrue(bridge.IsExistForum(forumName));

            // check that every admin in admins list is an admin in the forum
            foreach (UserStub admin in admins)
            {
                Assert.IsTrue(bridge.IsAdmin(admin.Username, forumName));
            }

            // clean up
            bridge.DeleteForum(forumName);
        }

        /*
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
        */

        //TODO: add tests for success/failure forum properties


    }
}