using ForumsSystemClient.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.CommunicationLayer
{
    class CL
    {

        bool serverWorks = false;

        public List<string> GetForumsList()
        {
            List<string> res = new List<string>();
            res.Add("forum1");
            res.Add("forum2");
            res.Add("forum3");
            return res;
        }

        public List<string> GetSubForumsList(string forumName)
        {
            if (!serverWorks)
            {
                List<string> res = new List<string>();
                res.Add("subforum1");
                res.Add("subforum2");
                res.Add("subforum3");
                return res;
            }
            else
            {
                Forum f = (Forum)Client.SendRequest("GetForum", forumName);
                
            }
            return null;
        }

        // return a list of titles of all threads in the subforum.
        public List<string> GetThreadsList(string forumName, string subForumName)
        {
            List<string> res = new List<string>();
            res.Add("thread1");
            res.Add("thread2");
            res.Add("thread3");
            return res;
        }

        public List<string> GetUsersInForum(string forumName)
        {
            // TODO: implement

            List<string> list = new List<string>();
            list.Add("user1");
            list.Add("user2");
            list.Add("user3");
            return list;
        }

        public bool IsInitialized()
        {
            // TODO: implement

            return true;
        }

        public bool LoginSuperAdmin(string username, string password)
        {
            // TODO: implement

            return true;
        }

        public bool RegisterToForum(string forumName, string username, string password, string email, DateTime dob)
        {
            // TODO: implement

            return true;
        }

        public void InitializeSystem(string username, string password)
        {
            // TODO: implement
        }

        public bool MemberLogin(string forumName, string username, string password)
        {
            // TODO: implement

            return true;
        }

        public bool IsExistUser(string username, string forumName)
        {
            // TODO: implement

            return true;
        }

        public bool SendPrivateMessage(object sender, string receiver, string title, string content)
        {
            // TODO: implement

            return true;
        }

        public List<Post> GetPosts(string threadID)
        {
            // TODO: implement

            List<Post> list = new List<Post>();
            Post p1 = new Post("title", "content content content content content contentcontent content contentcontent content contentcontent content contentcontent content contentcontent content contentcontent content contentcontent content contentcontent content contentcontent content contentcontent content contentcontent content contentcontent content contentcontent content contentcontent content contentcontent content contentcontent content contentcontent content content");
            Post p12 = new Post("title", "content content content");
            Post p13 = new Post("title", "content content content");
            Post p14 = new Post("title", "content content content");
            p1.AddNestedPost(p12);
            p1.AddNestedPost(p13);
            p1.AddNestedPost(p14);

            Post p121=new Post("title", "content content content");
            p12.AddNestedPost(p121);

            list.Add(p1);
            list.Add(new Post("title", "content content content"));
            list.Add(new Post("title", "content content content"));
            return list;
        }
    }
}
