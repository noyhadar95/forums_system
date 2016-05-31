using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcceptanceTestsBridge;

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
            guiBridge = new ClientBridge();
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




    }
}
