using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AcceptanceTests.ServerTests
{
    [TestClass]
    public class AddOpeningPost : UseCaseTestSuite
    {
        public AddOpeningPost()
            : base()
        {

        }

        [TestMethod]
        public void TestAddOpeningPost()
        {
            string title = "";
            string content = "";
            string forumName = "";
            string forumProperties = "";
            string username1 = "";
            List<string> moderators = new List<string>();
            moderators.Add(username1);
            string subForumName = "";
            string subForumProps = "";

            // create a forum, sub-forum and a thread to add a post to.
            base.CreateForum(forumName, forumProperties);
            bridge.CreateSubForum(forumName, subForumName, moderators, subForumProps);
            //bridge.AddThread(subForumName);

            // add opening post
            bool res = bridge.AddOpeningPost(title, content);
            Assert.IsTrue(res);

        }
    }
}
