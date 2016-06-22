using ForumsSystemClient.Resources.ForumManagement.DomainLayer;
using ForumsSystemClient.Resources.UserManagement.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.CommunicationLayer
{
    public class CL : ICL
    {

        bool serverWorks = false;
        public CL()
        {
            StartSecuredConnection();
        }

        private void StartSecuredConnection()
        {
            Client.StartSecuredConnection();
              
        }

        public List<string> GetForumsList()
        {
            return (List<string>)Client.SendRequest("GetForumsList");
        }

        public List<string> GetSubForumsList(string forumName)
        {

            return (List<string>)Client.SendRequest("GetSubForumsList", forumName);

            
        }

        // return a list of titles of all threads in the subforum.
        public List<string> GetThreadsList(string forumName, string subForumName)
        {
            return (List<string>)Client.SendRequest("GetThreadsList", forumName, subForumName);
        }
        public Dictionary<int,string> GetThreads(string forumName, string subForumName)
        {
            return (Dictionary<int, string>)Client.SendRequest("GetThreads", forumName, subForumName);
        }

        public List<string> GetUsersInForum(string forumName)
        {
            return (List<string>)Client.SendRequest("GetForumMembers", forumName);
        }

        public SuperAdmin GetSuperAdmin()
        {
            return (SuperAdmin)Client.SendRequest("GetSuperAdmin");
        }

        public bool IsInitialized()
        {
            return (bool)Client.SendRequest("IsInitialized");
        }

        public bool LoginSuperAdmin(string username, string password)
        {
            return (bool)Client.SendRequest("LoginSuperAdmin", username, password);
        }

        public bool RegisterToForum(string forumName, string guestName, string password, string email, DateTime dob)
        {
            return (bool)Client.SendRequest("RegisterToForum", forumName, guestName, password, email, dob);

        }

        public bool InitializeSystem(string username, string password)
        {
            bool res = (bool)Client.SendRequest("InitializeSystem", username, password);
            return res;
        }

        public Tuple<User,string> MemberLogin(string forumName, string username, string password)
        {
            return MemberLogin(forumName, username, password, "");
        }
        public Tuple<User, string> MemberLogin(string forumName, string username, string password, string sessionToken)
        {
            if (sessionToken == null)
                sessionToken = "";
            User user = (User)Client.SendRequest("MemberLogin", username, password,forumName, sessionToken);
            string sessionKey = null;
            if (user != null)
                sessionKey = GetSessionKey(username, forumName);

            else
            {
                //check if password is expired
                bool passwordValid = (bool)Client.SendRequest("CheckPasswordValidity", forumName, username);
                if (!passwordValid)
                    sessionKey = "-1";
            }

            Tuple<User, string> res = new Tuple<User, string>(user, sessionKey);
            return res;
        }

        public string GetSessionKey(string username, string forumName)
        {
            return (string)Client.SendRequest("GetSessionKey", username, forumName);
        }

        public bool IsExistUser(string username, string forumName)
        {
            return (bool)Client.SendRequest("IsExistUser", forumName, username);
        }

        public List<Post> GetPosts(string forumName, string subforumName,int threadID)
        {
            List<Post> posts = (List<Post>)Client.SendRequest("GetPosts", forumName, subforumName, threadID);
            return posts;
        }

        public Forum CreateForum(string userName, string password, string forumName, Policy policy, List<User> newAdmins)
        {

            Forum f= (Forum)Client.SendRequest("CreateForum", userName, password, forumName, policy, newAdmins);
            return f;
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

        public int AddThread(string forumName, string subForumName, string publisher, string title, string content)
        {
            return (int)Client.SendRequest("AddThread", forumName, subForumName, publisher, title, content);
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
            return (bool)Client.SendRequest("DeletePost", forumName, subForumName,deleter, threadID, postID);
        }

        public bool IsExistForum(string forumName)
        {
                return (bool)Client.SendRequest("IsExistForum", forumName);
        }

        public bool IsRegisteredToForum(string username, string forumName)
        {
            return (bool)Client.SendRequest("IsRegisteredToForum", username, forumName);
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

        public bool ConfirmRegistration(string forumName, string username, string token)
        {
            return (bool)Client.SendRequest("ConfirmRegistration", forumName, username, token);
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

        public List<Post> ReportPostsByMember(string forumName, string adminUserName, string username)
        {
            return (List<Post>)Client.SendRequest("ReportPostsByMember", forumName, adminUserName, username);
        }

        public int GetNumOfForums(string username, string password)
        {
            return (int)Client.SendRequest("GetNumOfForums", username, password);
        }

        public Dictionary<string, List<Tuple<string, string>>> GetMultipleUsersInfoBySuperAdmin(string userName, string password)
        {
            return (Dictionary<string, List<Tuple<string, string>>>)Client.SendRequest("GetMultipleUsersInfoBySuperAdmin", userName, password);
        }

        public List<PrivateMessage> GetNotifications(string forumName, string username)
        {
            List<PrivateMessage> notifications=(List<PrivateMessage>) Client.SendRequest("GetNotifications",forumName, username);
            return notifications;
          /*
             List<string> res = new List<string>();
            foreach(PrivateMessage msg in notifications)
            {
                res.Add(msg.title);
            }
            return res;
            */
        }

        public Tuple<string, string, DateTime, string> GetModeratorAppointmentsDetails(string forumName, string subForumName, string adminUserName1, string username1)
        {
            return (Tuple<string, string, DateTime, string>)Client.SendRequest("GetModeratorAppointmentsDetails", forumName, subForumName, adminUserName1, username1);
        }

        public bool SendPrivateMessage(string forumName, string senderUsername, string receiverUsername, string msgTitle, string msgContent)
        {
            return (bool)Client.SendRequest("SendPrivateMessage", forumName, senderUsername, receiverUsername, msgTitle, msgContent);
        }

        public string GetUserType(string forumName, string username)
        {
            return (string)Client.SendRequest("GetUserType", forumName, username);
        }

        public bool ChangeForumProperties(string username, string forumName, Policy properties)
        {
            return (bool)Client.SendRequest("ChangeForumProperties", username, forumName, properties);
        }

        public bool DeleteForumProperties(string deleter, string forumName, List<Policies> properties)
        {
            return (bool)Client.SendRequest("DeleteForumProperties",deleter, forumName, properties);
        }

        public bool AddModerator(string forumName, string subForumName, string adminUsername, string username, DateTime expiratoinDate)
        {
            return (bool)Client.SendRequest("AddModerator", forumName, subForumName, adminUsername,username,expiratoinDate);
        }

        public List<Tuple<string, string, DateTime, string>> ReportModeratorsDetails(string forumName, string adminUserName1)
        {
            return (List<Tuple<string, string, DateTime, string>>)Client.SendRequest("ReportModeratorsDetails", forumName, adminUserName1);
        }

        public void MemberLogout(string forumName, string username)
        {
            Client.SendRequest("MemberLogout", forumName, username);
        }

        public List<string> GetForumMembers(string forumName)
        {
            return (List<string>)Client.SendRequest("GetForumMembers", forumName);
        }

        public bool IgnoreFriend(string forumName, string userName, string userToIgnore)
        {
            return (bool)Client.SendRequest("IgnoreFriend", forumName,userName,userToIgnore);
        }

        public void AcceptFriendRequest(string forumName, string accepter, string toAccept)
        {
            Client.SendRequest("AcceptFriendRequest", forumName, accepter, toAccept);
        }

        public void SendFriendRequest(string forumName, string sender, string reciever)
        {
            Client.SendRequest("SendFriendRequest", forumName, sender, reciever);
        }
        public List<string> GetUsersNotFriends(string forumName, string username)
        {
            return (List<string>)Client.SendRequest("GetUsersNotFriends", forumName, username);
        }

        public List<string> GetFriendRequests(string forumName, string username)
        {            
            return (List<string>)Client.SendRequest("GetWaitingFriendsList", forumName, username);
        }

        public bool AddSecurityQuestion(string forumName, string username, SecurityQuestionsEnum question, string answer)
        {
            return (bool)Client.SendRequest("AddSecurityQuestion", forumName, username,question,answer);

        }

        public bool RemoveSecurityQuestion(string forumName, string username, SecurityQuestionsEnum question)
        {
            return (bool)Client.SendRequest("RemoveSecurityQuestion", forumName, username, question);

        }

        public bool CheckSecurityQuestion(string forumName, string username, SecurityQuestionsEnum question, string answer)
        {
            return (bool)Client.SendRequest("CheckSecurityQuestion", forumName, username, question, answer);

        }
        public bool SetUserPassword(string forumName, string username, string oldPassword, string newPassword)
        {
            return (bool)Client.SendRequest("SetUserPassword", forumName, username, oldPassword, newPassword);
        }
        public List<PrivateMessageNotification> GetPrivateMessageNotifications(string forumName, string username)
        {
            return (List<PrivateMessageNotification>)Client.SendRequest("GetPrivateMessageNotifications", forumName, username);

        }
        public void AddComplaint(string forumName, string subforum, string username)
        {
            Client.SendRequest("AddComplaint", forumName, subforum, username);
        }
        public void DeactivateUser(string forumName, string username)
        {
            Client.SendRequest("DeactivateUser", forumName, username);
        }
    }
}
