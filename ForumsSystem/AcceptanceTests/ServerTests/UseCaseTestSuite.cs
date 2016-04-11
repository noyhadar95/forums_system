using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcceptanceTestsBridge;
using System.Collections.Generic;

namespace AcceptanceTests.ServerTests
{
    [TestClass]
    public class UseCaseTestSuite
    {
        protected IBridge bridge;
        protected string superAdminUsername = "superadmin";
        protected string superAdminPass = "superadminpass";
        protected string adminUserName1 = "admin1"; // the username of the admin used to create sub forums for the tests
        protected string adminPass1 = "adminPasswd"; // the password of the admin used to create sub forums for the tests
        protected string adminEmail1 = "admin1@gmail.com"; // the email of the admin used to create sub forums for the tests


        public UseCaseTestSuite()
        {
            bridge = ProxyBridge.GetInstance();
            bridge.InitializeSystem(superAdminUsername, superAdminPass);
        }

        protected bool CreateForum(string forumName)
        {
            string forumProperties = "";
            return CreateForum(forumName, forumProperties);
        }

        protected bool CreateForum(string forumName, string forumProperties)
        {
            List<UserStub> admins = new List<UserStub>();
            UserStub user1 = new UserStub(adminUserName1, adminPass1, adminEmail1, forumName);
            admins.Add(user1);

            // create the forum
            return bridge.CreateForum(superAdminUsername, forumName, admins, forumProperties);
        }

        protected void DeleteForum(string forumName)
        {
            bridge.DeleteForum(forumName);
        }

        // create a new forum called forumName and a new sub-forum called subForumName.
        protected bool CreateSubForum(string forumName, string forumProperties, string subForumName, List<string> moderators,
            string subForumProps)
        {
            // create a forum
            CreateForum(forumName, forumProperties);

            // register all moderators-to-be to the forum
            foreach (string mod in moderators)
            {
                string username = mod, pass = mod + "passwd", email = mod + "@gmail.com";
                DateTime dateOfBirth = new DateTime(1995, 8, 2);

                bridge.RegisterToForum(forumName, mod, pass, email, dateOfBirth);
            }

            return bridge.CreateSubForum(adminUserName1, forumName, subForumName, moderators, subForumProps);
        }

        // create a new forum called forumName, a new sub-forum called subForumName and than
        // a new thread in it.
        protected int AddThread(string forumName, string forumProperties, string subForumName, List<string> moderators,
            string subForumProps, string publisher)
        {
            CreateSubForum(forumName, forumProperties, subForumName, moderators, subForumProps);
            return bridge.AddThread(forumName, subForumName, publisher);

        }

    }
}
