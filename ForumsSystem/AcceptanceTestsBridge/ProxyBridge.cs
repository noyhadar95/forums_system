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

        public bool CreateForum(string forumName, string adminUserName, string forumProperties)
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

        public bool CreateSubForum(string forumName, List<string> moderators, string properties)
        {
            return true;
        }




        public bool IsExistForum(string forumName)
        {
            return true;
        }

        public void AddUser(string adminUserName, string adminPass)
        {
        }

        public bool IsExistUser(string adminUserName)
        {
            return true;
        }

        public void DeleteUser(string adminUserName)
        {
        }


        public void DeleteForum(string forumName)
        {
           
        }


    }
}
