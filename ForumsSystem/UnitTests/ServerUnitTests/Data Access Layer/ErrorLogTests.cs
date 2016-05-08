﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumsSystem.Server.ForumManagement.Data_Access_Layer;

namespace UnitTests.ServerUnitTests.Data_Access_Layer
{
    [TestClass]
    public class ErrorLogTests
    {
        DAL_ErrorLogs de;
        [TestInitialize()]
        public void Initialize()
        {
            DAL_Forum df = new DAL_Forum();
            df.DeleteAll();
            de = new DAL_ErrorLogs();
        }
        [TestCleanup()]
        public void Cleanup()
        {
            de = null;
        }

        [TestMethod]
        public void Test1()
        {
            DateTime now = DateTime.Now;
            DateTime next = now.AddMinutes(10);
            de.AddLog(now, "test1");
            de.AddLog(next, "test2");
            var d = de.GetLogByDate(next);

            Assert.IsTrue(d.Rows.Count >= 1);

            de.DeleteLog(now);
            de.DeleteLog(next);

        }
    }
}