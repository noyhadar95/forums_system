using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcceptanceTestsBridge
{
    public interface Bridge
    {

        bool CreateForum(string forumName, List<UserStub> admins, string forumProperties);

        bool SetForumProperties(string forumName, string forumProperties);

        bool RegisterToForum(string forumName, string username, string password, string email);

        bool CreateSubForum(string forumName, string subForumName, List<string> moderators, string properties);



        bool IsExistForum(string forumName);

        void AddUser(string userName, string pass);

        bool IsExistUser(string userName);

        void DeleteUser(string userName);

        void DeleteForum(string forumName);

        bool IsRegisteredToForum(string username, string forumName);

        void AddAdmin(string username);

        bool IsAdmin(string username, string forumName);

        bool IsModerator(string username, string subForumName);

        int AddOpeningPost(string forumName, string subForumName, int threadID, string title, string content);

        int AddThread(string forumName, string subForumName, string threadName);

        int AddReplyPost(string forumName, string subForumName, int threadID, int postID, string title, string content);
    }
}
