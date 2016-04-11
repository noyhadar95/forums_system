using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcceptanceTestsBridge
{
    public class ProxyBridge : IBridge
    {
        private static ProxyBridge instance = null;

        private IBridge realBridge;

        private ProxyBridge()
        {
            realBridge = null;
        }

        public static ProxyBridge GetInstance()
        {
            if (instance == null)
            {
                instance = new ProxyBridge();
                // TODO: set real bridge
                instance.SetRealBridge(new RealBridge());
            }
            return instance;
        }

        public void SetRealBridge(IBridge realBridge)
        {
            this.realBridge = realBridge;
        }


        #region Add/Create Methods

        public bool CreateForum(string creator, string forumName, List<UserStub> admins, string forumProperties)
        {
            if (realBridge != null)
                return realBridge.CreateForum(creator, forumName, admins, forumProperties);

            return true;
        }

        public bool CreateSubForum(string creator, string forumName, string subForumName, List<string> moderators, string properties)
        {
            if (realBridge != null)
                return realBridge.CreateSubForum(creator, forumName, subForumName, moderators, properties);

            return true;
        }

        public int AddOpeningPost(string forumName, string subForumName, int threadID, string title, string content)
        {
            if (realBridge != null)
                return realBridge.AddOpeningPost(forumName, subForumName, threadID, title, content);

            return 1;
        }

        public int AddThread(string forumName, string subForumName, string threadName)
        {
            if (realBridge != null)
                return realBridge.AddThread(forumName, subForumName, threadName);

            return 1;
        }

        public int AddReplyPost(string forumName, string subForumName, int threadID, int postID, string title, string content)
        {
            if (realBridge != null)
                return realBridge.AddReplyPost(forumName, subForumName, threadID, postID, title, content);

            return 1;
        }

        public bool AddModerator(string forumName, string subForumName, string username)
        {
            if (realBridge != null)
                return realBridge.AddModerator(forumName, subForumName, username);

            return true;
        }

        #endregion


        #region Delete Methods

        public void DeleteUser(string userName)
        {
            if (realBridge != null)
                realBridge.DeleteUser(userName);

        }

        public void DeleteForum(string forumName)
        {
            if (realBridge != null)
                realBridge.DeleteForum(forumName);

        }

        public bool DeletePost(string forumName, string subForumName, int threadID, int postID)
        {
            if (realBridge != null)
                return realBridge.DeletePost(forumName, subForumName, threadID, postID);

            return true;
        }

        #endregion


        #region Boolean Queries: IsExist, IsRegistered...

        public bool IsExistForum(string forumName)
        {
            if (realBridge != null)
                return realBridge.IsExistForum(forumName);

            return true;
        }

        public bool IsRegisteredToForum(string username, string forumName)
        {
            if (realBridge != null)
                return realBridge.IsRegisteredToForum(username, forumName);

            return true;
        }

        public bool IsAdmin(string username, string forumName)
        {
            if (realBridge != null)
                return realBridge.IsAdmin(username, forumName);

            return true;
        }

        public bool IsModerator(string forumName, string subForumName, string username)
        {
            if (realBridge != null)
                return realBridge.IsModerator(forumName, subForumName, username);

            return true;
        }

        public bool IsExistThread(string forumName, string subForumName, int threadID)
        {
            if (realBridge != null)
                return realBridge.IsExistThread(forumName, subForumName, threadID);

            return true;
        }

        public bool IsMsgReceived(string username, string msgTitle, string msgContent)
        {
            if (realBridge != null)
                return realBridge.IsMsgReceived(username, msgTitle, msgContent);

            return true;
        }

        public bool IsMsgSent(string username, string msgTitle, string msgContent)
        {
            if (realBridge != null)
                return realBridge.IsMsgSent(username, msgTitle, msgContent);

            return true;
        }

        #endregion



        // ---------------------------------- Other Methods


        public bool SetForumProperties(string forumName, string forumProperties)
        {
            if (realBridge != null)
                return realBridge.SetForumProperties(forumName, forumProperties);

            return true;
        }

        public bool RegisterToForum(string forumName, string username, string password, string email, DateTime dateOfBirth)
        {
            if (realBridge != null)
                return realBridge.RegisterToForum(forumName, username, password, email, dateOfBirth);

            return true;
        }

        public int CountNestedReplies(string forumName, string subForumName, int threadID, int postID)
        {
            if (realBridge != null)
                return realBridge.CountNestedReplies(forumName, subForumName, threadID, postID);

            return 1;
        }

        public bool SendPrivateMsg(string forumName, string senderUsername, string receiverUsername, string msgTitle, string msgContent)
        {
            if (realBridge != null)
                return realBridge.SendPrivateMsg(forumName, senderUsername, receiverUsername, msgTitle, msgContent);

            return true;
        }

        public bool EditModeratorExpDate(string forumName, string subForumName, string username, DateTime newDate)
        {
            if (realBridge != null)
                return realBridge.EditModeratorExpDate(forumName, subForumName, username, newDate);

            return true;
        }

        public DateTime GetModeratorExpDate(string forumName, string subForumName, string username)
        {
            if (realBridge != null)
                return realBridge.GetModeratorExpDate(forumName, subForumName, username);

            return DateTime.Now;
        }

        public bool LoginUser(string forumName, string username, string pass)
        {
            if (realBridge != null)
                return realBridge.LoginUser(forumName, username, pass);

            return true;
        }

        public bool LoginSuperAdmin(string username, string pass)
        {
            if (realBridge != null)
                return realBridge.LoginSuperAdmin(username, pass);

            return true;
        }

        public bool InitializeSystem(string username, string pass)
        {
            if (realBridge != null)
                return realBridge.InitializeSystem(username, pass);

            return true;
        }

        public bool ConfirmRegistration(string forumName, string username)
        {
            if (realBridge != null)
                return realBridge.ConfirmRegistration(forumName, username);

            return true;
        }

    }
}
