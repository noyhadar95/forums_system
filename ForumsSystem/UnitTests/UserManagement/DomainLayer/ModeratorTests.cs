﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumsSystem.Server.ForumManagement.DomainLayer;
using ForumsSystem.Server.UserManagement.DomainLayer;
using ForumsSystem.Server.ForumManagement.Data_Access_Layer;

namespace UnitTests.ServerUnitTests.DomainLayer
{
    [TestClass]
    public class ModeratorTests
    {
        IForum forum;
        IUser user;
        IUser admin;
        Moderator mod;
        DateTime year;
        DAL_Forum dal_forum = new DAL_Forum();

        [TestInitialize()]
        public void Initialize()
        {
            DateTime today = DateTime.Today;
            year = today.AddYears(-26);
            forum = new Forum("testForum"); ;
            user = new User("u1", "p1", "e1@gmail.com", forum, year);
            admin = new User("admin", "admin", "admin@gmail.com", forum, year);

            DateTime expirationDate = DateTime.Today.AddMonths(1);
            mod = new Moderator(admin,user, expirationDate);
        }


        [TestCleanup()]
        public void Cleanup()
        {
            dal_forum.DeleteForum("testForum");
            forum = null;
            user = null;
            mod = null;
        }


        [TestMethod]
        public void TestModExpirationDate()
        {
            DateTime expirationDate = DateTime.Today.AddMonths(2);
            mod.changeExpirationDate(expirationDate);
            Assert.AreEqual(mod.expirationDate, expirationDate);
        }
    }
}
