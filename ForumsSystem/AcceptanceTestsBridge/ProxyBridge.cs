using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcceptanceTestsBridge
{
    public class ProxyBridge : IBridge
    {
        private IBridge realBridge;

        public ProxyBridge()
        {
            realBridge = null;
        }

        public bool CreateForum(string forumName, List<UserStub> admins, string forumProperties)
        {
            return true;
        }

        public bool SetForumProperties(string forumName, string forumProperties)
        {
            return true;
        }

        public bool RegisterToForum(string forumName, string username, string password, string email)
        {
            return true;
        }

        public bool CreateSubForum(string forumName, string subForumName, List<string> moderators, string properties)
        {
            return true;
        }




        public bool IsExistForum(string forumName)
        {
            return true;
        }

        public void AddUser(string userName, string pass)
        {
        }

        public bool IsExistUser(string userName)
        {
            return true;
        }

        public void DeleteUser(string userName)
        {
        }

        public void DeleteForum(string forumName)
        {

        }

        public bool IsRegisteredToForum(string username, string forumName)
        {
            return true;
        }


        public void AddAdmin(string userName)
        {

        }


        public bool IsAdmin(string username, string forumName)
        {
            return true;
        }


        public bool IsModerator(string forumName, string subForumName, string username)
        {
            return true;
        }





        public int AddOpeningPost(string forumName, string subForumName, int threadID, string title, string content)
        {
            return 0;
        }

        public int AddThread(string forumName, string subForumName, string threadName)
        {
            return 0;
        }


        public int AddReplyPost(string forumName, string subForumName, int threadID, int postID, string title, string content)
        {
            return 0;
        }


        public bool DeletePost(string forumName, string subForumName, int threadID, int postID)
        {
            return true;
        }


        public int CountNestedReplies(string forumName, string subForumName, int threadID, int postID)
        {
            return 0;
        }


        public bool IsExistThread(string forumName, string subForumName, int threadID)
        {
            return true;
        }

        public bool SendPrivateMsg(string senderUsername, string receiverUsername, string msgTitle, string msgContent)
        {
            return true;
        }


        public bool IsMsgReceived(string username, string msgTitle, string msgContent)
        {
            return true;
        }

        public bool IsMsgSent(string username, string msgTitle, string msgContent)
        {
            return true;
        }


        public bool EditModeratorExpDate(string forumName, string subForumName, string username, DateTime newDate)
        {
            return true;
        }


        public DateTime GetModeratorExpDate(string forumName, string subForumName, string username)
        {
            return new DateTime();
        }


        public bool AddModerator(string forumName, string subForumName, string username)
        {
            return true;
        }


        public bool LoginUser(string forumName, string username, string pass)
        {
            return true;
        }


        public bool LoginSuperAdmin(string username, string pass)
        {
            return true;
        }
    }
}
