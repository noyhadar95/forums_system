using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcceptanceTestsBridge;
using System.Collections.Generic;

namespace AcceptanceTests.ServerTests
{
    [TestClass]
    public class UseCaseTestSuite
    {
        protected Bridge bridge;

        public UseCaseTestSuite()
        {
            bridge = new ProxyBridge();

        }

        protected bool CreateForum(string forumName)
        {
            string forumProperties = "";
            return CreateForum(forumName, forumProperties);
        }

        protected bool CreateForum(string forumName, string forumProperties)
        {
            string adminUserName1 = "";
            string adminPass1 = "";
            string adminEmail1 = "";
            List<UserStub> admins = new List<UserStub>();
            UserStub user1 = new UserStub(adminUserName1, adminPass1, adminEmail1, forumName);
            admins.Add(user1);

            // create the forum
            return bridge.CreateForum(forumName, admins, forumProperties);
        }

        protected void DeleteForum(string forumName)
        {
            bridge.DeleteForum(forumName);
        }

        // create a new forum called forumName, a new sub-forum called subForumName and than
        // a new thread in it.
        protected int AddThread(string forumName, string forumProperties, string subForumName, List<string> moderators,
            string subForumProps, string threadName)
        {
            // create a forum, sub-forum and a thread to add a post to.
            CreateForum(forumName, forumProperties);
            bridge.CreateSubForum(forumName, subForumName, moderators, subForumProps);
            return bridge.AddThread(forumName, subForumName, threadName);

        }

    }
}
