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
        public CL()
        {
            StartSecuredConnection();
        }

        private void StartSecuredConnection()
        {
            Client.StartSecuredConnection(false);

        }

        public Post AddReply(string forumName, string subForumName, int threadID, string publisher, int postID, string title, string content)
        {
            try
            {
                return (Post)Client.SendRequest("AddReply", forumName, subForumName, threadID, publisher, postID, title, content);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int AddThread(string forumName, string subForumName, string publisher, string title, string content)
        {
            try
            {
                return (int)Client.SendRequest("AddThread", forumName, subForumName, publisher, title, content);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public List<string> GetForumsList()
        {
            try
            {
                return (List<string>)Client.SendRequest("GetForumsList");
            }
            catch (Exception)
            {
                return null;
            }
        }


        public List<string> GetSubForumsList(string forumName)
        {
            try
            {
                return (List<string>)Client.SendRequest("GetSubForumsList", forumName);
            }
            catch (Exception)
            {
                return null;
            }

        }

        public Dictionary<int, string> GetThreads(string forumName, string subForumName)
        {
            try
            {
                return (Dictionary<int, string>)Client.SendRequest("GetThreads", forumName, subForumName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Tuple<User, string> MemberLogin(string forumName, string username, string password)
        {
            try
            {
                return MemberLogin(forumName, username, password, "");
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Tuple<User, string> MemberLogin(string forumName, string username, string password, string sessionToken)
        {
            try
            {
                if (sessionToken == null)
                    sessionToken = "";
                User user = (User)Client.SendRequest("MemberLogin", username, password, forumName, sessionToken);
                string sessionKey = null;
                if (user != null)
                    sessionKey = GetSessionKey(username, forumName);

                else
                {
                    //check if user is banned
                    if (isBanned(forumName, username))
                        sessionKey = "-2";
                    else
                    {
                        //check if password is expired
                        bool passwordValid = (bool)Client.SendRequest("CheckPasswordValidity", forumName, username);
                        if (!passwordValid)
                            sessionKey = "-1";
                    }
                }

                Tuple<User, string> res = new Tuple<User, string>(user, sessionKey);
                return res;
            }
            catch (Exception)
            {
                return new Tuple<User, string>(null, null);
            }
        }

        public string GetSessionKey(string username, string forumName)
        {
            try
            {
                return (string)Client.SendRequest("GetSessionKey", username, forumName);
            }
            catch (Exception)
            {
                return null;
            }
        }


        public List<Post> GetPosts(string forumName, string subforumName, int threadID)
        {
            try
            {
                List<Post> posts = (List<Post>)Client.SendRequest("GetPosts", forumName, subforumName, threadID);
                return posts;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void MemberLogout(string forumName, string username)
        {
            try
            {
                Client.SendRequest("MemberLogout", forumName, username);
            }
            catch (Exception)
            {
                return;
            }
        }


        public bool isBanned(string forumName, string userName)
        {
            try
            {
                return (bool)Client.SendRequest("isBanned", forumName, userName);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}