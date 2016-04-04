﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumsSystem.Server.ForumManagement.DomainLayer;
using ForumsSystem.Server.UserManagement.DomainLayer;

namespace UnitTests.ServerUnitTests.DomainLayer
{
    [TestClass]
    public class ModeratorTests
    {
        IForum forum;
        IUser user;
        Moderator mod;
        [TestInitialize()]
        public void Initialize()
        {
            forum = new Forum("testForum"); ;
            user = new User("u1", "p1", "e1@gmail.com", forum);
            DateTime expirationDate = DateTime.Today.AddMonths(1);
            mod = new Moderator(user, expirationDate);
        }


        [TestCleanup()]
        public void Cleanup()
        {
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