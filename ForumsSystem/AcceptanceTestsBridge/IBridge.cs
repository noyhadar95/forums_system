﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcceptanceTestsBridge
{
    public interface IBridge
    {

        #region Add/Create Methods

        bool CreateForum(string creator, string forumName, List<UserStub> admins, string forumProperties);

        bool CreateSubForum(string creator, string forumName, string subForumName, Dictionary<string, DateTime> moderators);

        // add thread also means adding the opening post of the thread
        int AddThread(string forumName, string subForumName, string publisher, string title, string content);

        int AddReplyPost(string forumName, string subForumName, int threadID, int postID, string title, string content);

        bool AddModerator(string forumName, string subForumName, string adminUsername, KeyValuePair<string, DateTime> newMod);

        #endregion


        #region Delete Methods

        void DeleteUser(string forumName, string userName);

        void DeleteForum(string forumName);

        bool DeletePost(string forumName, string subForumName, int threadID, int postID);

        #endregion


        #region Boolean Queries: IsExist, IsRegistered...

        bool IsExistForum(string forumName);

        bool IsRegisteredToForum(string username, string forumName);

        bool IsAdmin(string username, string forumName);

        bool IsModerator(string forumName, string subForumName, string username);

        bool IsExistThread(string forumName, string subForumName, int threadID);

        bool IsMsgReceived(string username, string msgTitle, string msgContent);

        bool IsMsgSent(string username, string msgTitle, string msgContent);

        #endregion


        // Other Methods

        bool SetForumProperties(string forumName, string forumProperties);

        bool RegisterToForum(string forumName, string username, string password, string email, DateTime dateOfBirth);

        // count the number of replies to a post recursively (i.e. including replies of replies adn so on)
        int CountNestedReplies(string forumName, string subForumName, int threadID, int postID);

        bool SendPrivateMsg(string forumName, string senderUsername, string receiverUsername, string msgTitle, string msgContent);

        bool EditModeratorExpDate(string forumName, string subForumName, string admin, string moderator, DateTime newDate);

        DateTime GetModeratorExpDate(string forumName, string subForumName, string username);

        bool LoginUser(string forumName, string username, string pass);

        bool LoginSuperAdmin(string username, string pass);

        // initializes the sytem with username, pass as the login info for the super admin
        bool InitializeSystem(string username, string pass);

        bool ConfirmRegistration(string forumName, string username);


    }
}
