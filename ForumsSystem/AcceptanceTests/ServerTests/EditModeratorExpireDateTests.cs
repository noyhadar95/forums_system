using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AcceptanceTests.ServerTests
{
    [TestClass]
    public class EditModeratorExpireDateTests : UseCaseTestSuite
    {
        public EditModeratorExpireDateTests()
            : base()
        {

        }

        // test - check that after the admin edits the expiration date property of 
        // a sub forum moderator is updates accordingly.
        [TestMethod]
        public void TestEditModeratorExpireDate()
        {
            string forumName = "forum1";
            string forumProperties = "";
            string modUsername = "user1";
            DateTime dateOfBirth = DateTime.Now;
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            moderators.Add(modUsername, new DateTime());
            string subForumName = "sub forum 1";
            int year = 2016, month = 5, day = 15;
            DateTime newDate = new DateTime(year, month, day);

            // create a forum, sub-forum and a thread to add a post to.
            base.CreateSubForum(forumName, forumProperties, subForumName, moderators);

            //TODO: maybe make user a moderator

            bool res = bridge.EditModeratorExpDate(forumName, subForumName, this.adminUserName1, modUsername, newDate);
            Assert.IsTrue(res);
            DateTime updatedDate = bridge.GetModeratorExpDate(forumName, subForumName, modUsername);
            // check that the date has been updated successfully
            Assert.IsTrue(updatedDate != null && updatedDate.Equals(newDate));

        }


        //TODO: tests for 
        //      1. nonexistent moderator
        //      2. bad date


    }
}
