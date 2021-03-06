﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using AcceptanceTestsBridge;

namespace AcceptanceTests.ServerTests
{
    // this class tests the Set Forum Properties Use Case
    // i.e. check that after calling CreateForum(...) the forum properties have been set
    [TestClass]
    public class SetForumPropTests : UseCaseTestSuite
    {

        public SetForumPropTests()
            : base()
        {

        }

        // test the success main scenario with valid forum props
        [TestMethod]
        public void TestSetForumPropValid()
        {
            string forumName = GetNextForum();
            string adminUserName1 = "admin123";
            string adminPass1 = "root123";
            string adminEmail1 = "admin123@gmail.com";
            PoliciesStub forumPolicy = PoliciesStub.Password;
            List<UserStub> admins = new List<UserStub>();
            UserStub user1 = new UserStub(adminUserName1, adminPass1, adminEmail1, forumName);
            admins.Add(user1);

            // create the forum with the specified properties
            bool res = bridge.CreateForum(this.superAdminUsername, this.superAdminPass, forumName, admins, forumPolicy);
            Assert.IsTrue(res);
            // check that the forum now exists in the sytem
            Assert.IsTrue(bridge.IsExistForum(forumName));

            // check that the forum props have been set to forumPolicy
            Assert.IsTrue(bridge.IsForumHasPolicy(forumName,forumPolicy));

            // cleanup
            base.Cleanup(forumName);
        }


    }
}
