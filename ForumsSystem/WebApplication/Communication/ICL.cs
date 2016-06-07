using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Resources.ForumManagement.DomainLayer;
using WebApplication.Resources.UserManagement.DomainLayer;

namespace WebApplication.Communication
{
    public interface ICL
    {


        // other methods
        Post AddReply(string forumName, string subForumName, int threadID, string publisherName, int postID, string title, string content);
        int AddThread(string forumName, string subForumName, string publisher, string title, string content);
        Forum GetForum(string forumName);
        string GetUserType(string forumName, string username);
        bool ConfirmRegistration(string forumName, string username);
        bool LoginSuperAdmin(string username, string pass);
        DateTime GetModeratorExpDate(string forumName, string subForumName, string username);
        int CountNestedReplies(string forumName, string subForumName, int threadID, int postID);
        bool IsModerator(string forumName, string subForumName, string username);
        bool IsRegisteredToForum(string username, string forumName);
        bool IsExistForum(string forumName);
        bool IsExistThread(string forumName, string subForumName, int threadID);
        bool IsExistUser(string forumName, string username);
        bool DeletePost(string forumName, string subForumName, int threadID, string deleter, int postID);
        int GetOpenningPostID(string forumName, string subForumName, int threadID);
        bool IsAdmin(string username, string forumName);
        //Dictionary<string, List<Tuple<string, string>>> GetMultipleUsersInfo();
        // int GetNumOfForums();
        int GetNumOfForums(string userName, string password);
        Dictionary<string, List<Tuple<string, string>>> GetMultipleUsersInfoBySuperAdmin(string userName, string password);
        void MemberLogout(string forumName, string username);
        User MemberLogin(string forumName, string username, string password);
        //  Tuple<string, string, DateTime, string> GetModeratorAppointmentsDetails(string forumName, string subForumName, string adminUserName1, string username1);
        List<Post> GetPosts(string forumName, string subforumName, int threadId);
        //bool CheckIfPolicyExists(string forumName, Policies policy);
        void EditPost(string forumName, string subForumName, int threadId, string editor, int postId, string newTitle, string newContent);
        List<string> GetModeratorsList(string forumName, string subForumName, string adminUserName);
        //List<Post> ReportPostsByMember(string forumName, string adminUserName, string username);
        //SuperAdmin GetSuperAdmin();
        List<string> GetForumMembers(string forumName);
        List<string> GetThreadsList(string forumName, string subForumName);
        Dictionary<int, string> GetThreads(string forumName, string subForumName);
        List<string> GetSubForumsList(string forumName);
        List<string> GetForumsList();
        List<string> GetUsersNotFriends(string forumName, string username);
    }
}
