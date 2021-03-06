﻿using ForumsSystem.Server.ForumManagement.Data_Access_Layer;
using ForumsSystem.Server.ForumManagement.DomainLayer;
using ForumsSystem.Server.UserManagement.DomainLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests.ServerUnitTests.DomainLayer
{

    [TestClass]
    public class SubForumTests
    {

        IForum forum;
        IUser admin;
        IUser user;
        ISubForum subForum;
        DateTime year;
        DAL_Forum dal_forum = new DAL_Forum();

        [TestInitialize()]
        public void Initialize()
        {
            dal_forum.DeleteForum("testForum");
            DateTime today = DateTime.Today;
            year = today.AddYears(-24);
            forum = new Forum("testForum"); ;
            user = new User("u1", "p1", "e1@gmail.com", forum,year);
            admin = new User("admin", "admin", "admin@gmail.com", forum, year);
            subForum = new SubForum(forum, admin,"sub1");
        }


        [TestCleanup()]
        public void Cleanup()
        {
            dal_forum.DeleteForum("testForum");
            forum = null;
            subForum = null; 
            user = null;
        }

        [TestMethod]
        public void TestNoModerator()
        {
            DateTime expirationDate = DateTime.Today.AddMonths(1);
            subForum.addModerator(admin, user, expirationDate);
            Assert.IsNull(subForum.getModeratorByUserName("wrong"));
        }
        [TestMethod]
        public void TestAddModerator()
        {
            DateTime expirationDate = DateTime.Today.AddMonths(1);
            subForum.addModerator(admin,user, expirationDate);
            Assert.AreEqual(subForum.getModeratorByUserName(user.getUsername()).user,user);
        }
        [TestMethod]
        public void TestChangeModeratorExpirationDate()
        {
            DateTime expirationDate = DateTime.Today.AddMonths(1);
            subForum.addModerator(admin,user, expirationDate);
            Moderator mod =  subForum.getModeratorByUserName(user.getUsername());
            Assert.AreEqual(mod.expirationDate, expirationDate);

            DateTime newExpirationDate = expirationDate.AddMonths(2);
            subForum.changeModeratorExpirationDate(user, newExpirationDate);
            Assert.AreEqual(mod.expirationDate, newExpirationDate);

        }

        [TestMethod]
        public void TestName()
        {
            Assert.AreEqual(subForum.getName(), "sub1");
        }

        [TestMethod]
        public void TestCreator()
        {
            Assert.AreEqual(subForum.getCreator(), admin);
        }

        [TestMethod]
        public void TestThreads()
        {
            Thread thr = subForum.createThread();
            Assert.AreEqual(subForum.getThread(thr.id), thr);
        }

    }
}
