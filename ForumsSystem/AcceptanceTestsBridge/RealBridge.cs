using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.ServiceLayer;
using ForumsSystem.Server.ForumManagement.DomainLayer;
using ForumsSystem.Server.UserManagement.DomainLayer;

namespace AcceptanceTestsBridge
{
    class RealBridge : IBridge
    {

        private IServiceLayer sl;

        public RealBridge()
        {
            sl = new ServiceLayer();
        }


        #region Add/Create Methods

        public bool CreateForum(string creator, string forumName, List<UserStub> admins, string forumProperties)
        {
            IForum forum = sl.GetForum(forumName);
            List<IUser> newAdmins = new List<IUser>();

            foreach (UserStub user in admins)
            {
                IUser u = forum.getUser(user.Username);
                newAdmins.Add(u);
            }

            sl.CreateForum(creator, forumName, properties, newAdmins);
        }

        public bool CreateSubForum(string creator, string forumName, string subForumName, Dictionary<string, DateTime> moderators)
        {
            IForum forum = sl.GetForum(forumName);
            IUser user = forum.getUser(creator);
            ISubForum subForum = sl.CreateSubForum(user, subForumName, moderators);
            return subForum != null;
        }

        public int AddThread(string forumName, string subForumName, string publisher, string title, string content)
        {
            IForum forum = sl.GetForum(forumName);
            IUser threadPublisher = forum.getUser(publisher);
            ISubForum subForum = forum.getSubForum(subForumName);
            Thread thread = sl.AddThread(subForum, threadPublisher, title, content);
            return thread.id;
        }

        public int AddReplyPost(string forumName, string subForumName, int threadID, int postID, string title, string content)
        {
            sl.AddReply();
        }

        public bool AddModerator(string forumName, string subForumName, string adminUsername, KeyValuePair<string, DateTime> newMod)
        {
            IForum forum = sl.GetForum(forumName);
            IUser admin = forum.getUser(adminUsername);
            ISubForum subForum = forum.getSubForum(subForumName);
            return sl.AddModerator(admin, subForum, newMod.Key, newMod.Value);
        }

        #endregion

        #region Delete Methods

        public void DeleteUser(string forumName, string userName)
        {
            sl.DeleteUser(forumName, userName);
        }

        public void DeleteForum(string forumName)
        {
            sl.DeleteForum(forumName)
        }

        public bool DeletePost(string forumName, string subForumName, int threadID, int postID)
        {
            sl.DeletePost();
        }

        #endregion


        #region Boolean Queries: IsExist, IsRegistered...

        public bool IsExistForum(string forumName)
        {
            return sl.IsExistForum(forumName);
        }

        public bool IsRegisteredToForum(string username, string forumName)
        {
            return sl.IsRegisteredToForum(username, forumName);
        }

        public bool IsAdmin(string username, string forumName)
        {

        }

        public bool IsModerator(string forumName, string subForumName, string username)
        {
            return sl.IsModerator(forumName, subForumName, username);
        }

        public bool IsExistThread(string forumName, string subForumName, int threadID)
        {

        }

        public bool IsMsgReceived(string username, string msgTitle, string msgContent)
        {
            return sl.IsMsgReceived(username, msgTitle, msgContent);
        }

        public bool IsMsgSent(string username, string msgTitle, string msgContent)
        {
            return sl.IsMsgSent(username, msgTitle, msgContent);
        }

        #endregion



        // ---------------------------------- Other Methods


        public bool SetForumProperties(string forumName, string forumProperties)
        {
            IForum forum = sl.GetForum(forumName);
            sl.SetForumProperties(forum, properties);
        }

        public bool RegisterToForum(string forumName, string username, string password, string email, DateTime dateOfBirth)
        {
            IForum forum = sl.GetForum(forumName);
            return sl.RegisterToForum(new User(), forum, username, password, email, dateOfBirth);
        }

        public int CountNestedReplies(string forumName, string subForumName, int threadID, int postID)
        {
            return sl.CountNestedReplies(forumName, subForumName, threadID, postID);
        }

        public bool SendPrivateMsg(string forumName, string senderUsername, string receiverUsername, string msgTitle, string msgContent)
        {
            IForum forum = sl.GetForum(forumName);
            IUser sender = forum.getUser(senderUsername);
            PrivateMessage pm = sl.SendPrivateMessage(sender, receiverUsername, msgTitle, msgContent);
            return pm != null;
        }

        public bool EditModeratorExpDate(string forumName, string subForumName, string admin, string moderator, DateTime newDate)
        {
            IForum forum = sl.GetForum(forumName);
            IUser adminUser = forum.getUser(admin);
            ISubForum subForum = forum.getSubForum(subForumName);
            return sl.ChangeExpirationDate(adminUser, newDate, moderator, subForum);
        }

        public DateTime GetModeratorExpDate(string forumName, string subForumName, string username)
        {
            return sl.GetModeratorExpDate(forumName, subForumName, username);
        }

        public bool LoginUser(string forumName, string username, string pass)
        {
            IForum forum = sl.GetForum(forumName);
            IUser user = sl.MemberLogin(username, pass, forum);
            return user != null;
        }

        public bool LoginSuperAdmin(string username, string pass)
        {
            return sl.LoginSuperAdmin(username, pass);
        }

        public bool InitializeSystem(string username, string pass)
        {
            return sl.InitializeSystem(username, pass);
        }

        public bool ConfirmRegistration(string forumName, string username)
        {
            return sl.ConfirmRegistration(forumName, username);
        }


    }
}
