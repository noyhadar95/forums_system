using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumsSystem.Server.ForumManagement.Data_Access_Layer;

namespace UnitTests.ServerUnitTests.Data_Access_Layer
{
    [TestClass]
    public class ActivityLogsTest
    {
        DAL_ActivityLog da;
        [TestInitialize()]
        public void Initialize()
        {
            da = new DAL_ActivityLog();
        }
        [TestCleanup()]
        public void Cleanup()
        {
            da = null;
        }

        [TestMethod]
        public void Test1()
        {
            DateTime now = DateTime.Now;
            DateTime next = now.AddMinutes(10);
            da.AddActivity(now, "test1");
            da.AddActivity(next, "test2");
            var d = da.GetActivityByDate(next);

        }
    }
}
