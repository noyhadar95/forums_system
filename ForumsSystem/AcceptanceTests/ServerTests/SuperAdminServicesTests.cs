using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcceptanceTestsBridge;
using System.Collections.Generic;

namespace AcceptanceTests.ServerTests
{
    [TestClass]
    public class SuperAdminServicesTests : UseCaseTestSuite
    {
        public SuperAdminServicesTests()
            : base()
        {

        }

        [TestMethod]
        public void TestNumberOfForums()
        {
            int numOfForums;
            string forumName1 = GetNextForum(), forumName2 = GetNextForum();
            string adminUserName1 = "adm1", adminUserName2 = "adm2";
            string adminPass1 = "root1", adminPass2 = "root2";
            string adminEmail1 = "adm1@gmail.com", adminEmail2 = "adm2@gmail.com";
            PoliciesStub forumPolicy = PoliciesStub.Password;
            List<UserStub> admins = new List<UserStub>();
            UserStub user1 = new UserStub(adminUserName1, adminPass1, adminEmail1, forumName1);
            UserStub user2 = new UserStub(adminUserName2, adminPass2, adminEmail2, forumName1);
            admins.Add(user1);
            admins.Add(user2);
            try
            {
                numOfForums = bridge.GetNumOfForums(this.superAdminUsername,this.superAdminPass);
                Assert.IsTrue(numOfForums == 0);
                // create the forum
                bridge.CreateForum(this.superAdminUsername, this.superAdminPass, forumName1, admins, forumPolicy);
                numOfForums = bridge.GetNumOfForums(this.superAdminUsername, this.superAdminPass);
                Assert.IsTrue(numOfForums == 1);
                bridge.CreateForum(this.superAdminUsername, this.superAdminPass, forumName2, admins, forumPolicy);
                numOfForums = bridge.GetNumOfForums(this.superAdminUsername, this.superAdminPass);
                Assert.IsTrue(numOfForums == 2);
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
            finally
            {
                base.Cleanup(forumName1);
                base.Cleanup(forumName2);
            }

        }
        [TestMethod]
        public void TestNumberOfForumsSameForum()//test a case when trying to register twice the same forum
        {
            int numOfForums;
            string forumName1 = GetNextForum();
            string adminUserName1 = "adm1", adminUserName2 = "adm2";
            string adminPass1 = "root1", adminPass2 = "root2";
            string adminEmail1 = "adm1@gmail.com", adminEmail2 = "adm2@gmail.com";
            PoliciesStub forumPolicy = PoliciesStub.Password;
            List<UserStub> admins = new List<UserStub>();
            UserStub user1 = new UserStub(adminUserName1, adminPass1, adminEmail1, forumName1);
            UserStub user2 = new UserStub(adminUserName2, adminPass2, adminEmail2, forumName1);
            admins.Add(user1);
            admins.Add(user2);
            try
            {
                numOfForums = bridge.GetNumOfForums(this.superAdminUsername, this.superAdminPass);
                Assert.IsTrue(numOfForums == 0);
                // create the forum
                bridge.CreateForum(this.superAdminUsername, this.superAdminPass, forumName1, admins, forumPolicy);
                numOfForums = bridge.GetNumOfForums(this.superAdminUsername, this.superAdminPass);
                Assert.IsTrue(numOfForums == 1);
                bridge.CreateForum(this.superAdminUsername, this.superAdminPass, forumName1, admins, forumPolicy);
                numOfForums = bridge.GetNumOfForums(this.superAdminUsername, this.superAdminPass);
                Assert.IsTrue(numOfForums == 1);
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
            finally
            {
                base.Cleanup(forumName1);
            }

        }
        [TestMethod]
        public void TestInfoAboutMultipleForumsUsers()
        {
            Dictionary<string, List<Tuple<string, string>>> multipleUsersInfo;//<email,List<forum,username>>
            List<Tuple<string, string>> temp;
                string forumName1 = GetNextForum(), forumName2 = GetNextForum();
            string adminUserName1 = "adm1", adminUserName2 = "adm2";
            string adminPass1 = "root1", adminPass2 = "root2";
            string adminEmail1 = "adm1@gmail.com", adminEmail2 = "adm2@gmail.com";
            PoliciesStub forumPolicy = PoliciesStub.Password;
            List<UserStub> admins = new List<UserStub>();
            UserStub admin1 = new UserStub(adminUserName1, adminPass1, adminEmail1, forumName1);
            UserStub admin2 = new UserStub(adminUserName2, adminPass2, adminEmail2, forumName1);
            admins.Add(admin1);
            admins.Add(admin2);
            string user11 = "user11", user12 = "user12", pass1 = "pass1", email1 = "email1@gmail.com";
            DateTime dob1 = DateTime.Today.AddYears(-50);
            string user21 = "user21", user22 = "user22", pass2 = "pass2", email2 = "email2@gmail.com";
            DateTime dob2 = DateTime.Today.AddYears(-50);
            try
            {
                // create the forums
                bridge.CreateForum(this.superAdminUsername, this.superAdminPass, forumName1, admins, forumPolicy);
                bridge.CreateForum(this.superAdminUsername, this.superAdminPass, forumName2, admins, forumPolicy);
                //register user 1 to forums 1 and 2
                bridge.RegisterToForum(forumName1, user11, pass1, email1, dob1);
                bridge.ConfirmRegistration(forumName1, user11);
                bridge.RegisterToForum(forumName2, user12, pass1, email1, dob1);
                bridge.ConfirmRegistration(forumName2, user12);
                //check the info
                multipleUsersInfo = bridge.GetMultipleUsersInfo(this.superAdminUsername,this.superAdminPass);
                Assert.IsTrue(multipleUsersInfo.ContainsKey(email1));
                Assert.IsTrue(multipleUsersInfo.TryGetValue(email1, out temp));
                Assert.IsTrue(temp.Count == 2);
                Assert.IsTrue(temp.Contains(new Tuple<string, string>(forumName1, user11)));//TODO: check this (contains?)
                Assert.IsTrue(temp.Contains(new Tuple<string, string>(forumName2, user12)));//TODO: check this (contains?)
                temp = null;
                //register user 2 to forums 1 and 2
                bridge.RegisterToForum(forumName1, user21, pass2, email2, dob2);
                bridge.ConfirmRegistration(forumName1, user21);
                bridge.RegisterToForum(forumName2, user22, pass2, email2, dob2);
                bridge.ConfirmRegistration(forumName2, user22);
                //check the info
                //first check that info about user 1 is still there
                multipleUsersInfo = bridge.GetMultipleUsersInfo(this.superAdminUsername, this.superAdminPass);
                Assert.IsTrue(multipleUsersInfo.ContainsKey(email1));
                Assert.IsTrue(multipleUsersInfo.TryGetValue(email1, out temp));
                Assert.IsTrue(temp.Count == 2);
                Assert.IsTrue(temp.Contains(new Tuple<string, string>(forumName1, user11)));//TODO: check this (contains?)
                Assert.IsTrue(temp.Contains(new Tuple<string, string>(forumName2, user12)));//TODO: check this (contains?)
                temp = null;
                //now check the info about user 2
                multipleUsersInfo = bridge.GetMultipleUsersInfo(this.superAdminUsername, this.superAdminPass);
                Assert.IsTrue(multipleUsersInfo.ContainsKey(email2));
                Assert.IsTrue(multipleUsersInfo.TryGetValue(email2, out temp));
                Assert.IsTrue(temp.Count == 2);
                Assert.IsTrue(temp.Contains(new Tuple<string, string>(forumName1, user21)));//TODO: check this (contains?)
                Assert.IsTrue(temp.Contains(new Tuple<string, string>(forumName2, user22)));//TODO: check this (contains?)
                temp = null;
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
            finally
            {
                base.Cleanup(forumName1);
                base.Cleanup(forumName2);
            }
        }
    }
}
