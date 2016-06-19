using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Resources.ForumManagement.DomainLayer;
using WebApplication.Resources.UserManagement.DomainLayer;

namespace WebApplication.Communication
{
    public class CL : ICL
    {

        public Post AddReply(string forumName, string subForumName, int threadID, string publisher, int postID, string title, string content)
        {
            return (Post)Client.SendRequest("AddReply", forumName, subForumName, threadID, publisher, postID, title, content);
        }

        public int AddThread(string forumName, string subForumName, string publisher, string title, string content)
        {
            return (int)Client.SendRequest("AddThread", forumName, subForumName, publisher, title, content);
        }
        public List<string> GetForumsList()
        {
            return (List<string>)Client.SendRequest("GetForumsList");
        }

        public List<string> GetSubForumsList(string forumName)
        {

            return (List<string>)Client.SendRequest("GetSubForumsList", forumName);


        }

       
        public Dictionary<int, string> GetThreads(string forumName, string subForumName)
        {
            return (Dictionary<int, string>)Client.SendRequest("GetThreads", forumName, subForumName);
        }



        public User MemberLogin(string forumName, string username, string password)
        {
            User res = (User)Client.SendRequest("MemberLogin", username, password, forumName);
            return res;
        }


        public List<Post> GetPosts(string forumName, string subforumName, int threadID)
        {
            List<Post> posts = (List<Post>)Client.SendRequest("GetPosts", forumName, subforumName, threadID);
            return posts;
        }

        public void MemberLogout(string forumName, string username)
        {
            Client.SendRequest("MemberLogout", forumName, username);
        }
    }
}