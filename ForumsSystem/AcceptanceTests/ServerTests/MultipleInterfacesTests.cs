using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcceptanceTestsBridge;
using System.Collections.Generic;

namespace AcceptanceTests.ServerTests
{
    [TestClass]
    public class MultipleInterfacesTests : UseCaseTestSuite
    {
        protected IBridge guiBridge;
        protected IBridge webBridge;
        public MultipleInterfacesTests()
            : base()
        {
            #region bridges
            guiBridge = new RealBridge();
            webBridge = new RealBridge();
            #endregion
        }





        // test - the success scenario where the user is registered to a forum in the system
        [TestMethod]
        public void TestUserRegisterGuiLoginWebSuccess()
        {
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username = "user1", pass = "passwd", email = "user1@gmail.com";
            DateTime dateOfBirth = new DateTime(1995, 8, 2);

            ((ProxyBridge)bridge).SetRealBridge(guiBridge);
            base.CreateForum(forumName, forumPolicy);
            bridge.RegisterToForum(forumName, username, pass, email, dateOfBirth);


            ((ProxyBridge)bridge).SetRealBridge(webBridge);
            bool res = bridge.LoginUser(forumName, username, pass);
            Assert.IsTrue(res);

            // cleanup
            base.Cleanup(forumName);
        }


        [TestMethod]
        public void TestUserRegisterWebLoginGuiSuccess()
        {
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username = "user1", pass = "passwd", email = "user1@gmail.com";
            DateTime dateOfBirth = new DateTime(1995, 8, 2);

            ((ProxyBridge)bridge).SetRealBridge(webBridge);
            base.CreateForum(forumName, forumPolicy);
            bridge.RegisterToForum(forumName, username, pass, email, dateOfBirth);


            ((ProxyBridge)bridge).SetRealBridge(guiBridge);
            bool res = bridge.LoginUser(forumName, username, pass);
            Assert.IsTrue(res);

            // cleanup
            base.Cleanup(forumName);
        }


        

        [TestMethod]
        public void TestAddModeratorGUIcheckWeb()
        {
            ForumsSystem.Server.ForumManagement.Data_Access_Layer.DAL_Forum d = new ForumsSystem.Server.ForumManagement.Data_Access_Layer.DAL_Forum();
            d.DeleteAll();
            string forumName = GetNextForum();
            //string forumName = "dddd";

            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username1 = "user1";
            string username2 = "user2", pass2 = "passwd2", email2 = "user2@gmail.com";
            DateTime dateOfBirth1 = DateTime.Now, dateOfBirth2 = DateTime.Now;
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            moderators.Add(username1, DateTime.Today.AddDays(100));
            string subForumName = "sub forum 1";

            // create a forum, sub-forum and a thread to add a post to.
            ((ProxyBridge)bridge).SetRealBridge(guiBridge);
            bool flag = base.CreateSubForumByAdmin1(forumName, forumPolicy, subForumName, moderators);
            bridge.RegisterToForum(forumName, username2, pass2, email2, dateOfBirth2);
            KeyValuePair<string, DateTime> newMod = new KeyValuePair<string, DateTime>(username2, DateTime.Today.AddDays(100));
            bool res = bridge.AddModerator(forumName, subForumName, this.adminUserName1, newMod);
            Assert.IsTrue(res);
            ((ProxyBridge)bridge).SetRealBridge(webBridge);
            Assert.IsTrue(bridge.IsModerator(forumName, subForumName, username2));

            // cleanup
            //string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            //string className = this.GetType().Name;
            //if (bridge.ShouldTear(className, methodName))
            base.Cleanup(forumName);
        }


        [TestMethod]
        public void TestAddModeratorWebcheckGUI()
        {
            ForumsSystem.Server.ForumManagement.Data_Access_Layer.DAL_Forum d = new ForumsSystem.Server.ForumManagement.Data_Access_Layer.DAL_Forum();
            d.DeleteAll();
            string forumName = GetNextForum();
            //string forumName = "dddd";

            PoliciesStub forumPolicy = PoliciesStub.Password;
            string username1 = "user1";
            string username2 = "user2", pass2 = "passwd2", email2 = "user2@gmail.com";
            DateTime dateOfBirth1 = DateTime.Now, dateOfBirth2 = DateTime.Now;
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            moderators.Add(username1, DateTime.Today.AddDays(100));
            string subForumName = "sub forum 1";

            // create a forum, sub-forum and a thread to add a post to.
            ((ProxyBridge)bridge).SetRealBridge(webBridge);
            bool flag = base.CreateSubForumByAdmin1(forumName, forumPolicy, subForumName, moderators);
            bridge.RegisterToForum(forumName, username2, pass2, email2, dateOfBirth2);
            KeyValuePair<string, DateTime> newMod = new KeyValuePair<string, DateTime>(username2, DateTime.Today.AddDays(100));
            bool res = bridge.AddModerator(forumName, subForumName, this.adminUserName1, newMod);
            Assert.IsTrue(res);
            ((ProxyBridge)bridge).SetRealBridge(guiBridge);
            Assert.IsTrue(bridge.IsModerator(forumName, subForumName, username2));

            // cleanup
            //string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            //string className = this.GetType().Name;
            //if (bridge.ShouldTear(className, methodName))
            base.Cleanup(forumName);
        }
        [TestMethod]
        public void TestSendPrivateMsgReceiverUpdatedGuiCheckWeb()
        {
            string username1 = "username1", username2 = "username2";
            string pass1 = "passwd1", pass2 = "passwd2";
            string msgTitle = "title1", msgContent = "hello user2";
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string email1 = "user1@gmail.com", email2 = "user2@gmail.com";
            DateTime dateOfBirth1 = new DateTime(1995, 8, 2), dateOfBirth2 = new DateTime(1995, 8, 2);
            ((ProxyBridge)bridge).SetRealBridge(guiBridge);
            // create a forum and register 2 users to it
            base.CreateForum(forumName, forumPolicy);
            bridge.RegisterToForum(forumName, username1, pass1, email1, dateOfBirth1);
            bridge.RegisterToForum(forumName, username2, pass2, email2, dateOfBirth2);

            bridge.LoginUser(forumName, username1, pass1);

            // send private msg
            bool res = bridge.SendPrivateMsg(forumName, username1, username2, msgTitle, msgContent);
            Assert.IsTrue(res);
            ((ProxyBridge)bridge).SetRealBridge(webBridge);
            res = bridge.IsMsgReceived(forumName, username2, msgTitle, msgContent);
            Assert.IsTrue(res);

            // cleanup
            base.Cleanup(forumName);
        }

        // test - send a private message to another user in the same forum, check that
        // the sender's sent msg's list has been updated with the new sent msg.
        [TestMethod]
        public void TestSendPrivateMsgSenderUpdatedGuicheckWeb()
        {
            string username1 = "username1", username2 = "username2";
            string pass1 = "passwd1", pass2 = "passwd2";
            string msgTitle = "title1", msgContent = "hello user2";
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string email1 = "user1@gmail.com", email2 = "user2@gmail.com";
            DateTime dateOfBirth1 = new DateTime(1995, 8, 2), dateOfBirth2 = new DateTime(1995, 8, 2);
            ((ProxyBridge)bridge).SetRealBridge(guiBridge);
            // create a forum and register 2 users to it
            base.CreateForum(forumName, forumPolicy);
            bridge.RegisterToForum(forumName, username1, pass1, email1, dateOfBirth1);
            bridge.RegisterToForum(forumName, username2, pass2, email2, dateOfBirth2);

            bridge.LoginUser(forumName, username1, pass1);

            // send private msg
            bool res = bridge.SendPrivateMsg(forumName, username1, username2, msgTitle, msgContent);
            Assert.IsTrue(res);
            ((ProxyBridge)bridge).SetRealBridge(webBridge);
            res = bridge.IsMsgSent(forumName, username1, msgTitle, msgContent);
            Assert.IsTrue(res);

            // cleanup
            base.Cleanup(forumName);
        }


        [TestMethod]
        public void TestSendPrivateMsgReceiverUpdatedWebcheckGui()
        {
            string username1 = "username1", username2 = "username2";
            string pass1 = "passwd1", pass2 = "passwd2";
            string msgTitle = "title1", msgContent = "hello user2";
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string email1 = "user1@gmail.com", email2 = "user2@gmail.com";
            DateTime dateOfBirth1 = new DateTime(1995, 8, 2), dateOfBirth2 = new DateTime(1995, 8, 2);

            ((ProxyBridge)bridge).SetRealBridge(webBridge);
            // create a forum and register 2 users to it
            base.CreateForum(forumName, forumPolicy);
            bridge.RegisterToForum(forumName, username1, pass1, email1, dateOfBirth1);
            bridge.RegisterToForum(forumName, username2, pass2, email2, dateOfBirth2);

            bridge.LoginUser(forumName, username1, pass1);

            // send private msg
            bool res = bridge.SendPrivateMsg(forumName, username1, username2, msgTitle, msgContent);
            Assert.IsTrue(res);
            ((ProxyBridge)bridge).SetRealBridge(guiBridge);
            res = bridge.IsMsgReceived(forumName, username2, msgTitle, msgContent);
            Assert.IsTrue(res);

            // cleanup
            base.Cleanup(forumName);
        }

        // test - send a private message to another user in the same forum, check that
        // the sender's sent msg's list has been updated with the new sent msg.
        [TestMethod]
        public void TestSendPrivateMsgSenderUpdatedWebcheckGui()
        {
            string username1 = "username1", username2 = "username2";
            string pass1 = "passwd1", pass2 = "passwd2";
            string msgTitle = "title1", msgContent = "hello user2";
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.Password;
            string email1 = "user1@gmail.com", email2 = "user2@gmail.com";
            DateTime dateOfBirth1 = new DateTime(1995, 8, 2), dateOfBirth2 = new DateTime(1995, 8, 2);
            ((ProxyBridge)bridge).SetRealBridge(webBridge);
            // create a forum and register 2 users to it
            base.CreateForum(forumName, forumPolicy);
            bridge.RegisterToForum(forumName, username1, pass1, email1, dateOfBirth1);
            bridge.RegisterToForum(forumName, username2, pass2, email2, dateOfBirth2);

            bridge.LoginUser(forumName, username1, pass1);

            // send private msg
            bool res = bridge.SendPrivateMsg(forumName, username1, username2, msgTitle, msgContent);
            Assert.IsTrue(res);
            ((ProxyBridge)bridge).SetRealBridge(guiBridge);
            res = bridge.IsMsgSent(forumName, username1, msgTitle, msgContent);
            Assert.IsTrue(res);

            // cleanup
            base.Cleanup(forumName);
        }
        




        [TestMethod]
        public void TestCreateForumAdminExistGuitoWeb()
        {
            string forumName = GetNextForum();
            string adminUserName1 = "adm1", adminUserName2 = "adm2";
            string adminPass1 = "root1", adminPass2 = "root2";
            string adminEmail1 = "adm1@gmail.com", adminEmail2 = "adm2@gmail.com";
            PoliciesStub forumPolicy = PoliciesStub.Password;
            List<UserStub> admins = new List<UserStub>();
            UserStub user1 = new UserStub(adminUserName1, adminPass1, adminEmail1, forumName);
            UserStub user2 = new UserStub(adminUserName2, adminPass2, adminEmail2, forumName);
            admins.Add(user1);
            admins.Add(user2);

            ((ProxyBridge)bridge).SetRealBridge(guiBridge);
            // create the forum
            bool res = bridge.CreateForum(this.superAdminUsername, this.superAdminPass, forumName, admins, forumPolicy);
            Assert.IsTrue(res);
            ((ProxyBridge)bridge).SetRealBridge(webBridge);
            // check that the forum now exists in the sytem
            Assert.IsTrue(bridge.IsExistForum(forumName));

            // check that every admin in admins list is an admin in the forum
            foreach (UserStub admin in admins)
            {
                Assert.IsTrue(bridge.IsAdmin(admin.Username, forumName));
            }

            // clean up
            bridge.DeleteForum(forumName);
        }

        [TestMethod]
        public void TestCreateForumAdminExistWebToGui()
        {
            string forumName = GetNextForum();
            string adminUserName1 = "adm1", adminUserName2 = "adm2";
            string adminPass1 = "root1", adminPass2 = "root2";
            string adminEmail1 = "adm1@gmail.com", adminEmail2 = "adm2@gmail.com";
            PoliciesStub forumPolicy = PoliciesStub.Password;
            List<UserStub> admins = new List<UserStub>();
            UserStub user1 = new UserStub(adminUserName1, adminPass1, adminEmail1, forumName);
            UserStub user2 = new UserStub(adminUserName2, adminPass2, adminEmail2, forumName);
            admins.Add(user1);
            admins.Add(user2);
            ((ProxyBridge)bridge).SetRealBridge(guiBridge);
            // create the forum
            bool res = bridge.CreateForum(this.superAdminUsername, this.superAdminPass, forumName, admins, forumPolicy);
            Assert.IsTrue(res);
            ((ProxyBridge)bridge).SetRealBridge(webBridge);
            // check that the forum now exists in the sytem
            Assert.IsTrue(bridge.IsExistForum(forumName));

            // check that every admin in admins list is an admin in the forum
            foreach (UserStub admin in admins)
            {
                Assert.IsTrue(bridge.IsAdmin(admin.Username, forumName));
            }

            // clean up
            bridge.DeleteForum(forumName);
        }



        [TestMethod]
        public void TestEmailConfirmationSecureForumGuiToWeb()
        {
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.Authentication;
            string username = "user1";
            string pass = "passwd";
            string email = "user1@gmail.com";
            DateTime dateOfBirth = DateTime.Now;

            // make sure the forum is defined as "secured forum".
            ((ProxyBridge)bridge).SetRealBridge(guiBridge);
            base.CreateForum(forumName, forumPolicy);
            bool res = bridge.RegisterToForum(forumName, username, pass, email, dateOfBirth);
            Assert.IsTrue(res);

            // check that the user is not yet considered registered to the forum
            Assert.IsTrue(!bridge.IsRegisteredToForum(username, forumName));
            bridge.ConfirmRegistration(forumName, username);

            ((ProxyBridge)bridge).SetRealBridge(webBridge);
            // check that the user is now registered to the forum
            Assert.IsTrue(bridge.IsRegisteredToForum(username, forumName));

            // cleanup
            base.Cleanup(forumName);
        }


        [TestMethod]
        public void TestEmailConfirmationSecureForumWebToGui()
        {
            string forumName = GetNextForum();
            PoliciesStub forumPolicy = PoliciesStub.Authentication;
            string username = "user1";
            string pass = "passwd";
            string email = "user1@gmail.com";
            DateTime dateOfBirth = DateTime.Now;

            // make sure the forum is defined as "secured forum".
            ((ProxyBridge)bridge).SetRealBridge(webBridge);
            base.CreateForum(forumName, forumPolicy);
            bool res = bridge.RegisterToForum(forumName, username, pass, email, dateOfBirth);
            Assert.IsTrue(res);

            // check that the user is not yet considered registered to the forum
            Assert.IsTrue(!bridge.IsRegisteredToForum(username, forumName));
            bridge.ConfirmRegistration(forumName, username);

            ((ProxyBridge)bridge).SetRealBridge(guiBridge);
            // check that the user is now registered to the forum
            Assert.IsTrue(bridge.IsRegisteredToForum(username, forumName));

            // cleanup
            base.Cleanup(forumName);
        }


    }
}
