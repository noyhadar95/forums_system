using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumsSystem.Server.ForumManagement.Data_Access_Layer;

namespace UnitTests.ServerUnitTests.Data_Access_Layer
{
    [TestClass]
    public class UserTests
    {
        DAL_Forum dl;
        DAL_Users du;
        [TestInitialize()]
        public void Initialize()
        {
            dl = new DAL_Forum();
            du = new DAL_Users();
           
            dl.CreateForum("Test1", -1);
        }
        [TestCleanup()]
        public void Cleanup()
        {
            dl.DeleteForum("Test1");
            dl = null;
        }

        [TestMethod]
        public void Test1()
        {
            du.CreateUser("Test1", "User1", "Pass1", "User1@email.com", DateTime.Today, DateTime.Today.AddYears(-20), 0, UserType.UserTypes.Member, DateTime.Today);
            du.editUser("Test1", "User1", "Pass12", "User12@email.com", DateTime.Today, DateTime.Today.AddYears(-10), 10, UserType.UserTypes.Admin, DateTime.Today);
        }
    }
}
