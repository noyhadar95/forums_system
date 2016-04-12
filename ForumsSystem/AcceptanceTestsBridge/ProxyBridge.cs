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
                // set real bridge
                instance.SetRealBridge(new RealBridge());
            }
            return instance;
        }

        public void SetRealBridge(IBridge realBridge)
        {
            this.realBridge = realBridge;
        }


        #region Add/Create Methods

        public bool CreateForum(string creator, string forumName, List<UserStub> admins, PoliciesStub forumPolicies)
        {
            if (realBridge != null)
                return realBridge.CreateForum(creator, forumName, admins, forumPolicies);

            return true;
        }

        public bool CreateSubForum(string creator, string forumName, string subForumName, Dictionary<string, DateTime> moderators)
        {
            if (realBridge != null)
                return realBridge.CreateSubForum(creator, forumName, subForumName, moderators);

            return true;
        }

        public int AddThread(string forumName, string subForumName, string publisher, string title, string content)
        {
            if (realBridge != null)
                return realBridge.AddThread(forumName, subForumName, publisher, title, content);

            return 1;
        }

        public int AddReplyPost(string forumName, string subForumName, int threadID, string publisher, int postID, string title, string content)
        {
            if (realBridge != null)
                return realBridge.AddReplyPost(forumName, subForumName, threadID, publisher, postID, title, content);

            return 1;
        }

        public bool AddModerator(string forumName, string subForumName, string adminUsername, KeyValuePair<string, DateTime> newMod)
        {
            if (realBridge != null)
                return realBridge.AddModerator(forumName, subForumName, adminUsername, newMod);

            return true;
        }

        #endregion


        #region Delete Methods

        public void DeleteUser(string forumName, string userName)
        {
            if (realBridge != null)
                realBridge.DeleteUser(forumName, userName);

        }

        public void DeleteForum(string forumName)
        {
            if (realBridge != null)
                realBridge.DeleteForum(forumName);

        }

        public bool DeletePost(string forumName, string subForumName, int threadID, string deleter, int postID)
        {
            if (realBridge != null)
                return realBridge.DeletePost(forumName, subForumName, threadID, deleter, postID);

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

        public bool IsMsgReceived(string forumName, string username, string msgTitle, string msgContent)
        {
            if (realBridge != null)
                return realBridge.IsMsgReceived(forumName, username, msgTitle, msgContent);

            return true;
        }

        public bool IsMsgSent(string forumName, string username, string msgTitle, string msgContent)
        {
            if (realBridge != null)
                return realBridge.IsMsgSent(forumName, username, msgTitle, msgContent);

            return true;
        }

        public bool IsForumHasPolicy(string forumName, PoliciesStub forumPolicy)
        {
            if (realBridge != null)
                return realBridge.IsForumHasPolicy(forumName, forumPolicy);

            return true;
        }

        #endregion



        // ---------------------------------- Other Methods


        public bool SetForumProperties(string forumName, string username, PoliciesStub forumPolicies)
        {
            if (realBridge != null)
                return realBridge.SetForumProperties(forumName, username, forumPolicies);

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

        public bool EditModeratorExpDate(string forumName, string subForumName, string admin, string moderator, DateTime newDate)
        {
            if (realBridge != null)
                return realBridge.EditModeratorExpDate(forumName, subForumName, admin, moderator, newDate);

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

        public int GetOpenningPostID(string forumName, string subForumName, int threadID)
        {
            if (realBridge != null)
                return realBridge.GetOpenningPostID(forumName, subForumName, threadID);

            return 1;
        }


       


    }
}
