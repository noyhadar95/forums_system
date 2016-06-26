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
            Client.StartSecuredConnection(false);

        }

        public void startTesting()
        {
            Client.setTesting(true);
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
            try { 
            return (List<string>)Client.SendRequest("GetSubForumsList", forumName);
            }
            catch (Exception)
            {
                return null;
            }

        }

        // return a list of titles of all threads in the subforum.
        public List<string> GetThreadsList(string forumName, string subForumName)
        {
            try { 
            return (List<string>)Client.SendRequest("GetThreadsList", forumName, subForumName);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Dictionary<int, string> GetThreads(string forumName, string subForumName)
        {
            try {
            return (Dictionary<int, string>)Client.SendRequest("GetThreads", forumName, subForumName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<string> GetUsersInForum(string forumName)
        {
            try { 
            return (List<string>)Client.SendRequest("GetForumMembers", forumName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public SuperAdmin GetSuperAdmin()
        {
            try { 
            return (SuperAdmin)Client.SendRequest("GetSuperAdmin");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool IsInitialized()
        {
            try
            {
                return (bool)Client.SendRequest("IsInitialized");
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool LoginSuperAdmin(string username, string password)
        {
            try { 
            return (bool)Client.SendRequest("LoginSuperAdmin", username, password);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RegisterToForum(string forumName, string guestName, string password, string email, DateTime dob, int question, string answer)
        {
            try
            {
                return (bool)Client.SendRequest("RegisterToForum", forumName, guestName, password, email, dob)  &&
                    this.AddSecurityQuestion(forumName,guestName,question, answer);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool InitializeSystem(string username, string password)
        {
            try
            {
                bool res = (bool)Client.SendRequest("InitializeSystem", username, password);
                return res;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Tuple<User, string> MemberLogin(string forumName, string username, string password)
        {
            try { 
            return MemberLogin(forumName, username, password, "");
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Tuple<User, string> MemberLogin(string forumName, string username, string password, string sessionToken)
        {
            try { 
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
                return new Tuple<User, string>(null,null);
            }
        }

        public string GetSessionKey(string username, string forumName)
        {
            try { 
            return (string)Client.SendRequest("GetSessionKey", username, forumName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool IsExistUser(string username, string forumName)
        {
            try { 
            return (bool)Client.SendRequest("IsExistUser", forumName, username);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Post> GetPosts(string forumName, string subforumName, int threadID)
        {
            try { 
            List<Post> posts = (List<Post>)Client.SendRequest("GetPosts", forumName, subforumName, threadID);
            return posts;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Forum CreateForum(string userName, string password, string forumName, Policy policy, List<User> newAdmins)
        {
            try { 
            Forum f = (Forum)Client.SendRequest("CreateForum", userName, password, forumName, policy, newAdmins);
            return f;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Forum GetForum(string forumName)
        {
            try { 
            return (Forum)Client.SendRequest("GetForum", forumName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Post AddReply(string forumName, string subForumName, int threadID, string publisher, int postID, string title, string content)
        {
            try { 
            return (Post)Client.SendRequest("AddReply", forumName, subForumName, threadID, publisher, postID, title, content);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public SubForum CreateSubForum(string creator, string forumName, string subForumName, Dictionary<string, DateTime> moderators)
        {
            try { 
            return (SubForum)Client.SendRequest("CreateSubForum", creator, forumName, subForumName, moderators);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int AddThread(string forumName, string subForumName, string publisher, string title, string content)
        {
            try { 
            return (int)Client.SendRequest("AddThread", forumName, subForumName, publisher, title, content);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public bool AddModerator(string forumName, string subForumName, string adminUsername, KeyValuePair<string, DateTime> newMod)
        {
            try { 
            return (bool)Client.SendRequest("AddModerator", forumName, subForumName, adminUsername, newMod.Key, newMod.Value);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void DeleteUser(string userName, string forumName)
        {
            try { 
            Client.SendRequest("DeleteUser", userName, forumName);
            }
            catch (Exception)
            {
                return;
            }
        }

        public void DeleteForum(string forumName)
        {
            try { 
            Client.SendRequest("DeleteForum", forumName);
            }
            catch (Exception)
            {
                return;
            }
        }

        public bool DeletePost(string forumName, string subForumName, int threadID, string deleter, int postID)
        {
            try { 
            return (bool)Client.SendRequest("DeletePost", forumName, subForumName, deleter, threadID, postID);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsExistForum(string forumName)
        {
            try { 
            return (bool)Client.SendRequest("IsExistForum", forumName);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsRegisteredToForum(string username, string forumName)
        {
            try { 
            return (bool)Client.SendRequest("IsRegisteredToForum", username, forumName);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsAdmin(string username, string forumName)
        {
            try { 
            return (bool)Client.SendRequest("IsAdmin", username, forumName);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsModerator(string forumName, string subForumName, string username)
        {
            try { 
            return (bool)Client.SendRequest("IsModerator", forumName, subForumName, username);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsExistThread(string forumName, string subForumName, int threadID)
        {
            try { 
            return (bool)Client.SendRequest("IsExistThread", forumName, subForumName, threadID);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsMsgReceived(string forumName, string username, string msgTitle, string msgContent)
        {
            try { 
            return (bool)Client.SendRequest("IsMsgReceived", forumName, username, msgTitle, msgContent);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsMsgSent(string forumName, string username, string msgTitle, string msgContent)
        {
            try { 
            return (bool)Client.SendRequest("IsMsgSent", forumName, username, msgTitle, msgContent);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CheckIfPolicyExists(string forumName, Policies expectedPolicy)
        {
            try { 
            return (bool)Client.SendRequest("CheckIfPolicyExists", forumName, expectedPolicy);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SetForumProperties(string user, string forum, Policy policy)
        {
            try { 
            return (bool)Client.SendRequest("SetForumProperties", user, forum, policy);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int CountNestedReplies(string forumName, string subForumName, int threadID, int postID)
        {
            try { 
            return (int)Client.SendRequest("CountNestedReplies", forumName, subForumName, threadID, postID);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public bool ChangeExpirationDate(string forumName, string subForumName, string admin, string moderator, DateTime newDate)
        {
            try { 
            return (bool)Client.SendRequest("ChangeExpirationDate", forumName, subForumName, admin, moderator, newDate);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DateTime GetModeratorExpDate(string forumName, string subForumName, string username)
        {
            try { 
            return (DateTime)Client.SendRequest("GetModeratorExpDate", forumName, subForumName, username);
            }
            catch (Exception)
            {
                return DateTime.Now;//??
            }
        }

        public bool ConfirmRegistration(string forumName, string username, string token)
        {
            try { 
            return (bool)Client.SendRequest("ConfirmRegistration", forumName, username, token);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int GetOpenningPostID(string forumName, string subForumName, int threadID)
        {
            try { 
            return (int)Client.SendRequest("GetOpenningPostID", forumName, subForumName, threadID);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public void AddFriend(string forumName, string username1, string username2)
        {
            try { 
            Client.SendRequest("AddFriend", forumName, username1, username2);
            }
            catch (Exception)
            {
                return;
            }
        }

        public List<PostNotification> GetPostNotifications(string forumName, string username)
        {
            try { 
            return (List<PostNotification>)Client.SendRequest("GetPostNotifications", forumName, username);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void EditPost(string forumName, string subForumName, int threadId, string editor, int postId, string newTitle, string newContent)
        {
            try { 
            Client.SendRequest("EditPost", forumName, subForumName, threadId, editor, postId, newTitle, newContent);
            }
            catch (Exception)
            {
                return;
            }
        }

        public bool RemoveModerator(string forumName, string subForumName, string remover, string moderatorName)
        {
            try { 
            return (bool)Client.SendRequest("RemoveModerator", forumName, subForumName, remover, moderatorName);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int ReportNumOfPostsByMember(string forumName, string adminUserName, string username)
        {
            try { 
            return (int)Client.SendRequest("ReportNumOfPostsByMember", adminUserName, forumName, username);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public List<string> GetModeratorsList(string forumName, string subForumName, string adminUserName)
        {
            try { 
            return (List<string>)Client.SendRequest("GetModeratorsList", forumName, subForumName, adminUserName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Post> ReportPostsByMember(string forumName, string adminUserName, string username)
        {
            try { 
            return (List<Post>)Client.SendRequest("ReportPostsByMember", forumName, adminUserName, username);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int GetNumOfForums(string username, string password)
        {
            try { 
            return (int)Client.SendRequest("GetNumOfForums", username, password);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public Dictionary<string, List<Tuple<string, string>>> GetMultipleUsersInfoBySuperAdmin(string userName, string password)
        {
            try { 
            return (Dictionary<string, List<Tuple<string, string>>>)Client.SendRequest("GetMultipleUsersInfoBySuperAdmin", userName, password);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<PrivateMessage> GetNotifications(string forumName, string username)
        {
            try { 
            List<PrivateMessage> notifications = (List<PrivateMessage>)Client.SendRequest("GetNotifications", forumName, username);
            return notifications;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public Tuple<string, string, DateTime, string> GetModeratorAppointmentsDetails(string forumName, string subForumName, string adminUserName1, string username1)
        {
            try { 
            return (Tuple<string, string, DateTime, string>)Client.SendRequest("GetModeratorAppointmentsDetails", forumName, subForumName, adminUserName1, username1);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool SendPrivateMessage(string forumName, string senderUsername, string receiverUsername, string msgTitle, string msgContent)
        {
            try { 
            return (bool)Client.SendRequest("SendPrivateMessage", forumName, senderUsername, receiverUsername, msgTitle, msgContent);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string GetUserType(string forumName, string username)
        {
            try { 
            return (string)Client.SendRequest("GetUserType", forumName, username);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool ChangeForumProperties(string username, string forumName, Policy properties)
        {
            try { 
            return (bool)Client.SendRequest("ChangeForumProperties", username, forumName, properties);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteForumProperties(string deleter, string forumName, List<Policies> properties)
        {
            try { 
            return (bool)Client.SendRequest("DeleteForumProperties", deleter, forumName, properties);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddModerator(string forumName, string subForumName, string adminUsername, string username, DateTime expiratoinDate)
        {
            try { 
            return (bool)Client.SendRequest("AddModerator", forumName, subForumName, adminUsername, username, expiratoinDate);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Tuple<string, string, DateTime, string>> ReportModeratorsDetails(string forumName, string adminUserName1)
        {
            try { 
            return (List<Tuple<string, string, DateTime, string>>)Client.SendRequest("ReportModeratorsDetails", forumName, adminUserName1);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void MemberLogout(string forumName, string username)
        {
            try { 
            Client.SendRequest("MemberLogout", forumName, username);
            }
            catch (Exception)
            {
                return;
            }
        }

        public List<string> GetForumMembers(string forumName)
        {
            try
            {
                return (List<string>)Client.SendRequest("GetForumMembers", forumName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool IgnoreFriend(string forumName, string userName, string userToIgnore)
        {
            try
            {
                return (bool)Client.SendRequest("IgnoreFriend", forumName, userName, userToIgnore);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void AcceptFriendRequest(string forumName, string accepter, string toAccept)
        {
            try
            {
                Client.SendRequest("AcceptFriendRequest", forumName, accepter, toAccept);
            }
            catch (Exception)
            {
                return;
            }
        }

        public void SendFriendRequest(string forumName, string sender, string reciever)
        {
            try
            {
                Client.SendRequest("SendFriendRequest", forumName, sender, reciever);
            }
            catch (Exception)
            {
                return;
            }
        }
        public List<string> GetUsersNotFriends(string forumName, string username)
        {
            try
            {
                return (List<string>)Client.SendRequest("GetUsersNotFriends", forumName, username);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<string> GetFriendRequests(string forumName, string username)
        {
            try
            {
                return (List<string>)Client.SendRequest("GetWaitingFriendsList", forumName, username);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool AddSecurityQuestion(string forumName, string username, int question, string answer)
        {
            try
            {
                return (bool)Client.SendRequest("AddSecurityQuestion", forumName, username, question, answer);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveSecurityQuestion(string forumName, string username, int question)
        {
            try
            {
                return (bool)Client.SendRequest("RemoveSecurityQuestion", forumName, username, question);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CheckSecurityQuestion(string forumName, string username, int question, string answer, string newPassword)
        {
            try
            {
                return (bool)Client.SendRequest("CheckSecurityQuestion", forumName, username, question, answer, newPassword);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool SetUserPassword(string forumName, string username, string oldPassword, string newPassword)
        {
            try
            {
                return (bool)Client.SendRequest("SetUserPassword", forumName, username, oldPassword, newPassword);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<PrivateMessageNotification> GetPrivateMessageNotifications(string forumName, string username)
        {
            try
            {
                return (List<PrivateMessageNotification>)Client.SendRequest("GetPrivateMessageNotifications", forumName, username);
            }
            catch (Exception)
            {
                return null;
            }
        }


        public int getNumOfPostsInSubForum(string forumName, string subForum)
        {
            try
            {
                return (int)Client.SendRequest("getNumOfPostsInSubForum", forumName, subForum);
            }
            catch (Exception)
            {
                return  -1;
            }
        }

        public bool HasSeniorityPriviledge(string forumName, string subForumName, int threadId, string username, string postId)
        {
            try
            {
                return (bool)Client.SendRequest("HasSeniorityPriviledge", forumName, subForumName, threadId, username, postId);
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// subforum will not be used
        /// </summary>
        /// <param name="forumName"></param>
        /// <param name="subforum"></param>
        /// <param name="username"></param>
        public void AddComplaint(string forumName, string subforum, string username)
        {
            try
            {
                Client.SendRequest("AddComplaint", forumName, subforum, username);
            }
            catch (Exception)
            {
                return;
            }
        }
        public void DeactivateUser(string forumName, string username)
        {
            try
            {
                Client.SendRequest("DeactivateUser", forumName, username);
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



        public bool HasSeniorityPriviledge(string forumName, string subForumName, int threadId, string username, int postId)
        {
            try
            {
                return (bool)Client.SendRequest("HasSeniorityPriviledge", forumName, subForumName, threadId, username, postId);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddAdmin(string forumName, string username)
        {
            try
            {
                return (bool)Client.SendRequest("AddAdmin", forumName, username);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public PrivateMessage GetPrivateMsg(string forumName, string msgReceiver, string msgSender, int pmID)
        {
            try
            {
                return (PrivateMessage)Client.SendRequest("GetPrivateMsg", forumName, msgReceiver, msgSender, pmID);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<PrivateMessage> getReceivedMessages(string forumName, string username)
        {
            try
            {
                return (List<PrivateMessage>)Client.SendRequest("getReceivedMessages", forumName, username);
            }
            catch (Exception)
            {
                return new List<PrivateMessage>();
            }
        }
        public void LogoutAll()
        {
            Client.SendRequest("LogoutAll");
        }
    }
}
