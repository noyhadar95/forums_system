using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumsSystem.Server.ForumManagement.Data_Access_Layer;
namespace UnitTests.ServerUnitTests.Data_Access_Layer
{
    [TestClass]
    public class ForumTests
    {
        DAL_Forum dl;
        [TestInitialize()]
        public void Initialize()
        {
            dl = new DAL_Forum();
        }
        [TestCleanup()]
        public void Cleanup()
        {
            dl = null;
        }

        [TestMethod]
        public void Test1()
        {
            dl.CreateForum("test", 1);
        }



    }
}
