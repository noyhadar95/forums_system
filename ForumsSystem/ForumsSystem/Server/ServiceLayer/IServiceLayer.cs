﻿using System;
using ForumsSystem.Server.UserManagement.DomainLayer;
using ForumsSystem.Server.ForumManagement.DomainLayer;
using System.Collections.Generic;

namespace ForumsSystem.Server.ServiceLayer
{
    public interface IServiceLayer
    {

        #region Use Cases Methods

         Post AddReply(string forumName, string subForumName, int threadID, string publisherName, int postID, string title, string content);

        Thread AddThread(string forumName, string subForumName, string publisher, string title, string content);
        bool ChangeExpirationDate(IUser admin, DateTime newDate, string moderator, ISubForum subforum);

        bool ChangeForumProperties(IUser user, IForum forum, Policy properties);

        IForum CreateForum(string creatorName, string password, string name, Policy properties, List<IUser> adminUsername);

        ISubForum CreateSubForum(string creator, string forumName, string subforumName, Dictionary<string, DateTime> moderators);

        bool DeletePost(IUser deleter, Post post);

        bool InitializeSystem(string username, string pass);

        IUser MemberLogin(string username, string password, string forum);

        bool RegisterToForum(IUser guest, IForum forum, string userName, string password, string email, DateTime dateOfBirth);


        PrivateMessage SendPrivateMessage(IUser from, string to, string title, string content);

        bool SetForumProperties(IUser user, IForum forum, Policy properties);

        #endregion


        // other methods
        bool DeleteForumProperties(IUser user,IForum forum, List<Policies> properties);

        IForum GetForum(string forumName);

        bool AddModerator(string forumName, string subForumName, string adminUsername, string username, DateTime expiratoinDate);
        void removeForum(string forumName);
        bool ConfirmRegistration(string forumName, string username);
        bool LoginSuperAdmin(string username, string pass);
        DateTime GetModeratorExpDate(ISubForum subForum, string username);
        int CountNestedReplies(ISubForum subforum, int threadID, int postID);
        bool IsMsgSent(string forumName, string username, string msgTitle, string msgContent);
        bool IsMsgReceived(string forumName, string username, string msgTitle, string msgContent);
        bool IsModerator(string forumName, string subForumName, string username);
        bool IsRegisteredToForum(string username, string forumName);
        bool IsExistForum(string forumName);
        bool IsExistThread(string forumName, string subForumName, int threadID);
        bool DeletePost(string forumName, string subForumName,string deleter, int threadID, int postID);
        void DeleteForum(string forumName);
        void DeleteUser(string userName, string forumName);
        int GetOpenningPostID(string forumName, string subForumName, int threadID);

        bool IsAdmin(string username, string forumName);
        //Dictionary<string, List<Tuple<string, string>>> GetMultipleUsersInfo();
       // int GetNumOfForums();
        List<PrivateMessage> GetNotifications(string forumName, string username);
        int GetNumOfForums(string userName, string password);

        Dictionary<string, List<Tuple<string, string>>> GetMultipleUsersInfoBySuperAdmin(string userName, string password);
        void AddFriend(string forumName, string username1, string username2);
        Tuple<string, string, DateTime, string> GetModeratorAppointmentsDetails(string forumName, string subForumName, string adminUserName1, string username1);
        List<Post> GetPosts(string forumName, string subforumName, int threadId);
        bool CheckIfPolicyExists(string forumName, Policies policy);
    }
}
