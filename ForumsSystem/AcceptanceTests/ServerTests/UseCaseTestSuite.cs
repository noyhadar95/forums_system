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
            PoliciesStub forumPolicy = PoliciesStub.Password;
            return CreateForum(forumName, forumPolicy);
        }

        protected bool CreateForum(string forumName, PoliciesStub forumPolicy)
        {
            List<UserStub> admins = new List<UserStub>();
            UserStub user1 = new UserStub(adminUserName1, adminPass1, adminEmail1, forumName);
            admins.Add(user1);

            // create the forum
            return bridge.CreateForum(superAdminUsername, forumName, admins, forumPolicy);
        }

        protected void DeleteForum(string forumName)
        {
            bridge.DeleteForum(forumName);
        }

        // create a new forum called forumName and a new sub-forum called subForumName.
        protected bool CreateSubForum(string forumName, PoliciesStub forumPolicy, string subForumName, Dictionary<string, DateTime> moderators)
        {
            // create a forum
            CreateForum(forumName, forumPolicy);

            // register all moderators-to-be to the forum
            foreach (KeyValuePair<string, DateTime> mod in moderators)
            {
                string username = mod.Key, pass = mod.Key + "passwd", email = mod.Key + "@gmail.com";
                DateTime dateOfBirth = new DateTime(1995, 8, 2);

                bridge.RegisterToForum(forumName, username, pass, email, dateOfBirth);
            }
            bridge.LoginUser(forumName, adminUserName1, adminPass1);
            return bridge.CreateSubForum(adminUserName1, forumName, subForumName, moderators);
        }

        // create a new forum called forumName, a new sub-forum called subForumName and than
        // a new thread in it.
        protected int AddThread(string forumName, PoliciesStub forumPolicy, string subForumName, Dictionary<string, DateTime> moderators,
             string publisher, string title, string content)
        {
            CreateSubForum(forumName, forumPolicy, subForumName, moderators);
            string publisherPass = "publisherpassword", publisherEmail = "publisher1@gmail.com";
            DateTime publisherDateOfBirth = DateTime.Today.AddYears(-25);
            bridge.RegisterToForum(forumName, publisher, publisherPass, publisherEmail, publisherDateOfBirth);
            bridge.LoginUser(forumName, publisher, publisherPass);
            return bridge.AddThread(forumName, subForumName, publisher, title, content);

        }

    }
}
