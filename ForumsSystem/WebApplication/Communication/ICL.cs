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

        Post AddReply(string forumName, string subForumName, int threadID, string publisherName, int postID, string title, string content);
        int AddThread(string forumName, string subForumName, string publisher, string title, string content);
        
        void MemberLogout(string forumName, string username);
        Tuple<User, string> MemberLogin(string forumName, string username, string password);
        Tuple<User, string> MemberLogin(string forumName, string username, string password, string sessionToken);

        List<Post> GetPosts(string forumName, string subforumName, int threadId);

        Dictionary<int, string> GetThreads(string forumName, string subForumName);
        List<string> GetSubForumsList(string forumName);
        List<string> GetForumsList();

        string GetSessionKey(string username, string forumName);

    }
}
