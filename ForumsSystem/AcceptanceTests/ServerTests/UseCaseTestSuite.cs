using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcceptanceTestsBridge;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace AcceptanceTests.ServerTests
{
    /* Class Description:
     * The base class for all Use Case tests classes. This class contains
     * methods for creating forum, sub forum and thread. It also has a method for deleting a forum
     * every child class that inherits this one should call the base constructor in order to make 
     * sure that the system is initialized, and that the proxy bridge instance is initialized too.
     */
    [TestClass]
    public class UseCaseTestSuite
    {
        protected IBridge bridge;
        protected string superAdminUsername = "superadmin";
        protected string superAdminPass = "superadminpass";
        protected string adminUserName1 = "admin1"; // the username of the admin used to create sub forums for the tests
        protected string adminPass1 = "adminPasswd"; // the password of the admin used to create sub forums for the tests
        protected string adminEmail1 = "admin1@gmail.com"; // the email of the admin used to create sub forums for the tests
        protected static int forumIndex = 1;

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
            return bridge.CreateForum(superAdminUsername, this.superAdminPass, forumName, admins, forumPolicy);
        }

        protected bool CreateForum(string forumName, PoliciesStub forumPolicy, params object[] policyParams)
        {
            List<UserStub> admins = new List<UserStub>();
            UserStub user1 = new UserStub(adminUserName1, adminPass1, adminEmail1, forumName);
            admins.Add(user1);

            // create the forum
            return bridge.CreateForum(superAdminUsername, this.superAdminPass, forumName, admins, forumPolicy,policyParams);
        }


        private void DeleteForum(string forumName)
        {
            bridge.DeleteForum(forumName);
        }
        protected void Cleanup(string forumName)
        {
            StackTrace stackTrace = new System.Diagnostics.StackTrace();
            StackFrame frame = stackTrace.GetFrames()[1];
            MethodBase method = frame.GetMethod();
            string methodName = method.Name;
            string className = method.DeclaringType.Name;
          //  string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
         //  string className = this.GetType().Name;
            if (bridge.ShouldCleanup(className, methodName))
                DeleteForum(forumName);
        }

        // create a new forum called forumName, register all moderators to the forum and create 
        // a new sub-forum called subForumName with the given moderators list.
        protected bool CreateSubForumByAdmin1(string forumName, PoliciesStub forumPolicy, string subForumName, Dictionary<string, DateTime> moderators)
        {
            if (!bridge.IsExistForum(forumName))
            {
                // create a forum
                CreateForum(forumName, forumPolicy);


                // register all moderators-to-be to the forum
                foreach (KeyValuePair<string, DateTime> mod in moderators)
                {
                    string username = mod.Key, pass = mod.Key + "passwd", email = mod.Key + "@gmail.com";
                    DateTime dateOfBirth = new DateTime(1995, 8, 2);

                    bridge.RegisterToForum(forumName, username, pass, email, dateOfBirth);
                    bridge.ConfirmRegistration(forumName, username);
                    //.AddModerator(forumName, subForumName, this.adminUserName1, mod);
                }
            }
            bridge.LoginUser(forumName, adminUserName1, adminPass1);
            return bridge.CreateSubForum(adminUserName1, forumName, subForumName, moderators);
        }

        // create a new forum called forumName, a new sub-forum called subForumName and than
        // a new thread in it.
        protected int AddThread(string forumName, PoliciesStub forumPolicy, string subForumName, Dictionary<string, DateTime> moderators,
             string publisher, string title, string content)
        {
            CreateSubForumByAdmin1(forumName, forumPolicy, subForumName, moderators);
            string publisherPass = "publisherpassword", publisherEmail = "publisher1@gmail.com";
            DateTime publisherDateOfBirth = DateTime.Today.AddYears(-25);
            if (!bridge.IsRegisteredToForum(publisher, forumName))
            {
                bridge.RegisterToForum(forumName, publisher, publisherPass, publisherEmail, publisherDateOfBirth);
                bridge.LoginUser(forumName, publisher, publisherPass);
            }
            return bridge.AddThread(forumName, subForumName, publisher, title, content);

        }
        protected static string GetNextForum()
        {
            return "forum" + forumIndex++;
        }

    }
}
