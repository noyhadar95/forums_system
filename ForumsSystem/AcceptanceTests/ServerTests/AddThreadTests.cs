using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AcceptanceTests.ServerTests
{
    [TestClass]
    public class AddThreadTests : UseCaseTestSuite
    {
        public AddThreadTests()
            : base()
        {

        }

        // test - success scenario. check that after adding a thread to a sub forum, 
        // the thread is indeed being added to that sub forum.
        [TestMethod]
        public void TestAddThreadSuccess()
        {
            string forumName = "forum1";
            string forumProperties = "";
            string username1 = "user1";
            List<string> moderators = new List<string>();
            moderators.Add(username1);
            string subForumName = "sub forum 1";
            string subForumProps = "";
            string threadName = "thread1";

            int threadID = base.AddThread(forumName, forumProperties, subForumName, moderators, subForumProps, threadName);

            // check that the thread has been added
            Assert.IsTrue(bridge.IsExistThread(forumName, subForumName, threadID));
        }

    }
}
