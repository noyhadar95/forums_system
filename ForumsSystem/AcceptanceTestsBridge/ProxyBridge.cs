using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcceptanceTestsBridge
{
    public class ProxyBridge : Bridge
    {
        private Bridge realBridge;

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


        public bool IsModerator(string username, string subForumName)
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
    }
}
