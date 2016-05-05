using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystemClient.Resources;

namespace ForumsSystemClient.CommunicationLayer
{
    public class CL
    {

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
            List<string> res = new List<string>();
            res.Add("subforum1");
            res.Add("subforum2");
            res.Add("subforum3");
            return res;
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

        public SuperAdmin GetSuperAdmin()
        {
            throw new NotImplementedException();
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

        public bool InitializeSystem(string username, string password)
        {
            bool res = (bool)Client.SendRequest("InitializeSystem", username, password);
            return res;
        }

        public bool MemberLogin(string forumName, string username, string password)
        {
            User res = (User)Client.SendRequest("MemberLogin", username, password,forumName);
            return res!=null;
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

        public List<Post> GetPosts(string forumName, string subforumName,string threadID)
        {
            List<Post> posts = (List<Post>)Client.SendRequest("GetPosts", forumName, subforumName, threadID);
            return posts;
        }

        public Forum CreateForum(string userName, string password, string forumName, Policy policy, List<User> newAdmins)
        {
            return (Forum)Client.SendRequest("CreateForum", userName, password, forumName, policy, newAdmins);
        }

        public Forum GetForum(string forumName)
        {
            return (Forum)Client.SendRequest("GetForum", forumName);
        }

        public Post AddReply(string forumName, string subForumName, int threadID, string publisher, int postID, string title, string content)
        {
            return (Post)Client.SendRequest("AddReply", forumName, subForumName, threadID, publisher, postID, title, content);
        }

        public SubForum CreateSubForum(string creator, string forumName, string subForumName, Dictionary<string, DateTime> moderators)
        {
            return (SubForum)Client.SendRequest("CreateSubForum", creator, forumName, subForumName, moderators);
        }

        public Thread AddThread(string forumName, string subForumName, string publisher, string title, string content)
        {
            return (Thread)Client.SendRequest("AddThread", forumName, subForumName, publisher, title, content);
        }

        public bool AddModerator(string forumName, string subForumName, string adminUsername, KeyValuePair<string, DateTime> newMod)
        {
            return (bool)Client.SendRequest("AddModerator", forumName, subForumName, adminUsername, newMod.Key, newMod.Value);
        }

        public void DeleteUser(string userName, string forumName)
        {
            Client.SendRequest("DeleteUser", userName, forumName);
        }

        public void DeleteForum(string forumName)
        {
            Client.SendRequest("DeleteForum", forumName);
        }

        public bool DeletePost(string forumName, string subForumName, int threadID, string deleter, int postID)
        {
            //TODO: deleter!!!
            return (bool)Client.SendRequest("DeletePost", forumName, subForumName,deleter, threadID, postID);
        }

        public bool IsExistForum(string forumName)
        {
                return (bool)Client.SendRequest("IsExistForum", forumName);
        }

        public bool IsRegisteredToForum(string username, string forumName)
        {
            return (bool)Client.SendRequest("IsExistForum", username, forumName);
        }

        public bool IsAdmin(string username, string forumName)
        {
            return (bool)Client.SendRequest("IsAdmin", username, forumName);
        }

        public bool IsModerator(string forumName, string subForumName, string username)
        {
            return (bool)Client.SendRequest("IsModerator", forumName,subForumName,username);
        }

        public bool IsExistThread(string forumName, string subForumName, int threadID)
        {
            return (bool)Client.SendRequest("IsExistThread", forumName, subForumName, threadID);
        }

        public bool IsMsgReceived(string forumName, string username, string msgTitle, string msgContent)
        {
            return (bool)Client.SendRequest("IsMsgReceived", forumName, username, msgTitle, msgContent);
        }

        public bool IsMsgSent(string forumName, string username, string msgTitle, string msgContent)
        {
            return (bool)Client.SendRequest("IsMsgSent", forumName, username, msgTitle, msgContent);
        }

        public bool CheckIfPolicyExists(string forumName, Policies expectedPolicy)
        {
            return (bool)Client.SendRequest("CheckIfPolicyExists", forumName, expectedPolicy);
        }

        public bool SetForumProperties(string user, string forum, Policy policy)
        {
            return (bool)Client.SendRequest("SetForumProperties",user,forum,policy);
        }

        public int CountNestedReplies(string forumName, string subForumName, int threadID, int postID)
        {
            return (int)Client.SendRequest("CountNestedReplies", forumName, subForumName, threadID,postID);
        }

        public bool ChangeExpirationDate(string forumName, string subForumName, string admin, string moderator, DateTime newDate)
        {
            return (bool)Client.SendRequest("ChangeExpirationDate", forumName, subForumName, admin,moderator,newDate);
        }

        public DateTime GetModeratorExpDate(string forumName, string subForumName, string username)
        {
            return (DateTime)Client.SendRequest("GetModeratorExpDate", forumName, subForumName, username);
        }

        public bool ConfirmRegistration(string forumName, string username)
        {
            return (bool)Client.SendRequest("ConfirmRegistration", forumName, username);
        }

        public int GetOpenningPostID(string forumName, string subForumName, int threadID)
        {
            return (int)Client.SendRequest("GetOpenningPostID", forumName, subForumName,threadID);
        }

        public void AddFriend(string forumName, string username1, string username2)
        {
            Client.SendRequest("AddFriend", forumName, username1, username2);
        }

        public List<Post> GetPostNotifications(string forumName, string username)
        {
            return (List<Post>)Client.SendRequest("GetPostNotifications", forumName, username);
        }

        public void EditPost(string forumName, string subForumName, int threadId, string editor, int postId, string newTitle, string newContent)
        {
            Client.SendRequest("EditPost", forumName, subForumName, threadId, editor, postId, newTitle, newContent);
        }

        public bool RemoveModerator(string forumName, string subForumName, string remover, string moderatorName)
        {
            return (bool)Client.SendRequest("RemoveModerator",forumName, subForumName, remover, moderatorName);
        }

        public int ReportNumOfPostsByMember(string forumName, string adminUserName, string username)
        {
            return (int)Client.SendRequest("ReportNumOfPostsByMember",adminUserName, forumName, username);
        }

        public List<string> GetModeratorsList(string forumName, string subForumName, string adminUserName)
        {
            return (List<string>)Client.SendRequest("GetModeratorsList",forumName, subForumName, adminUserName);
        }

        public List<Post> ReportPostsByMember(string forumName, string adminUserName, string moderatorName)
        {
            throw new NotImplementedException();
        }

        public int GetNumOfForums(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, List<Tuple<string, string>>> GetMultipleUsersInfoBySuperAdmin(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public List<string> GetNotifications(string forumName, string username)
        {
            throw new NotImplementedException();
        }

        public Tuple<string, string, DateTime, string> GetModeratorAppointmentsDetails(string forumName, string subForumName, string adminUserName1, string username1)
        {
            throw new NotImplementedException();
        }

        public bool SendPrivateMessage(string forumName, string senderUsername, string receiverUsername, string msgTitle, string msgContent)
        {
            return (bool)Client.SendRequest("SendPrivateMessage", forumName, senderUsername, receiverUsername, msgTitle, msgContent);
        }
    }
}
