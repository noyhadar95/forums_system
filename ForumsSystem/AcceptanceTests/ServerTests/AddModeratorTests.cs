using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AcceptanceTests.ServerTests
{
    [TestClass]
    public class AddModeratorTests : UseCaseTestSuite
    {
        public AddModeratorTests()
            : base()
        {

        }

        [TestMethod]
        public void TestAddModerator()
        {
            string forumName = "forum1";
            string forumProperties = "";
            string username1 = "user1", pass1 = "passwd", email1 = "user1@gmail.com";
            string username2 = "user2", pass2 = "passwd2", email2 = "user2@gmail.com";
            List<string> moderators = new List<string>();
            moderators.Add(username1);
            string subForumName = "sub forum 1";
            string subForumProps = "";

            // create a forum, sub-forum and a thread to add a post to.
            CreateForum(forumName, forumProperties);
            bridge.RegisterToForum(forumName, username1, pass1, email1);
            bridge.CreateSubForum(forumName, subForumName, moderators, subForumProps);
            bridge.RegisterToForum(forumName, username2, pass2, email2);

            bool res = bridge.AddModerator(forumName, subForumName, username2);
            Assert.IsTrue(res);
            Assert.IsTrue(bridge.IsModerator(forumName, subForumName, username2));

        }
    }
}
