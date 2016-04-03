using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcceptanceTestsBridge
{
    public interface Bridge
    {

        bool CreateForum(string forumName, string adminUserName, string forumProperties);

        bool SetForumProperties(string forumName, string forumProperties);

        bool RegisterToForum(string forumName, string username, string password, string email);

        bool CreateSubForum(string forumName, List<string> moderators, string properties);



        bool IsExistForum(string forumName);

        void AddUser(string adminUserName, string adminPass);

        bool IsExistUser(string adminUserName);

        void DeleteUser(string adminUserName);

        void DeleteForum(string forumName);
    }
}
