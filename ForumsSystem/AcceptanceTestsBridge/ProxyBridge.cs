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
            }
            return instance;
        }

        public void SetRealBridge(IBridge realBridge)
        {
            this.realBridge = realBridge;
        }


        #region Add/Create Methods

        public bool CreateForum(string forumName, List<UserStub> admins, string forumProperties)
        {
            throw new NotImplementedException();
        }

        public bool CreateSubForum(string forumName, string subForumName, List<string> moderators, string properties)
        {
            throw new NotImplementedException();
        }

        public int AddOpeningPost(string forumName, string subForumName, int threadID, string title, string content)
        {
            throw new NotImplementedException();
        }

        public int AddThread(string forumName, string subForumName, string threadName)
        {
            throw new NotImplementedException();
        }

        public int AddReplyPost(string forumName, string subForumName, int threadID, int postID, string title, string content)
        {
            throw new NotImplementedException();
        }

        public bool AddModerator(string forumName, string subForumName, string username)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Delete Methods

        public void DeleteUser(string userName)
        {
            throw new NotImplementedException();
        }

        public void DeleteForum(string forumName)
        {
            throw new NotImplementedException();
        }

        public bool DeletePost(string forumName, string subForumName, int threadID, int postID)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region Boolean Queries: IsExist, IsRegistered...

        public bool IsExistForum(string forumName)
        {
            throw new NotImplementedException();
        }

        public bool IsRegisteredToForum(string username, string forumName)
        {
            throw new NotImplementedException();
        }

        public bool IsAdmin(string username, string forumName)
        {
            throw new NotImplementedException();
        }

        public bool IsModerator(string forumName, string subForumName, string username)
        {
            throw new NotImplementedException();
        }

        public bool IsExistThread(string forumName, string subForumName, int threadID)
        {
            throw new NotImplementedException();
        }

        public bool IsMsgReceived(string username, string msgTitle, string msgContent)
        {
            throw new NotImplementedException();
        }

        public bool IsMsgSent(string username, string msgTitle, string msgContent)
        {
            throw new NotImplementedException();
        }

        #endregion



        // ---------------------------------- Other Methods


        public bool SetForumProperties(string forumName, string forumProperties)
        {
            throw new NotImplementedException();
        }

        public bool RegisterToForum(string forumName, string username, string password, string email)
        {
            throw new NotImplementedException();
        }

        public int CountNestedReplies(string forumName, string subForumName, int threadID, int postID)
        {
            throw new NotImplementedException();
        }

        public bool SendPrivateMsg(string forumName, string senderUsername, string receiverUsername, string msgTitle, string msgContent)
        {
            throw new NotImplementedException();
        }

        public bool EditModeratorExpDate(string forumName, string subForumName, string username, DateTime newDate)
        {
            throw new NotImplementedException();
        }

        public DateTime GetModeratorExpDate(string forumName, string subForumName, string username)
        {
            throw new NotImplementedException();
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
