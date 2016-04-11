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

        public bool CreateSubForum(string creator, string forumName, string subForumName, List<string> moderators, string properties)
        {
            sl.CreateSubForum(forum,creator,)
        }

        public int AddOpeningPost(string forumName, string subForumName, int threadID, string title, string content)
        {
            throw new NotImplementedException();
        }

        public int AddThread(string forumName, string subForumName, string threadName)
        {
            sl.AddThread();
        }

        public int AddReplyPost(string forumName, string subForumName, int threadID, int postID, string title, string content)
        {
            throw new NotImplementedException();
        }

        public bool AddModerator(string forumName, string subForumName, string username)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Delete Methods

        public void DeleteUser(string userName)
        {
            throw new NotImplementedException();
        }

        public void DeleteForum(string forumName)
        {
            throw new NotImplementedException();
        }

        public bool DeletePost(string forumName, string subForumName, int threadID, int postID)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region Boolean Queries: IsExist, IsRegistered...

        public bool IsExistForum(string forumName)
        {
            throw new NotImplementedException();
        }

        public bool IsRegisteredToForum(string username, string forumName)
        {
            throw new NotImplementedException();
        }

        public bool IsAdmin(string username, string forumName)
        {
            throw new NotImplementedException();
        }

        public bool IsModerator(string forumName, string subForumName, string username)
        {
            throw new NotImplementedException();
        }

        public bool IsExistThread(string forumName, string subForumName, int threadID)
        {
            throw new NotImplementedException();
        }

        public bool IsMsgReceived(string username, string msgTitle, string msgContent)
        {
            throw new NotImplementedException();
        }

        public bool IsMsgSent(string username, string msgTitle, string msgContent)
        {
            throw new NotImplementedException();
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
            sl.RegisterToForum(forum, username, password, email, dateOfBirth);
        }

        public int CountNestedReplies(string forumName, string subForumName, int threadID, int postID)
        {
            throw new NotImplementedException();
        }

        public bool SendPrivateMsg(string forumName, string senderUsername, string receiverUsername, string msgTitle, string msgContent)
        {
            IForum forum = sl.GetForum(forumName);
            IUser sender = forum.getUser(senderUsername);
            IUser receiver = forum.getUser(receiverUsername);
            return sl.SendPrivateMessage(sender, receiver, msgTitle, msgContent);
        }

        public bool EditModeratorExpDate(string forumName, string subForumName, string username, DateTime newDate)
        {
            //TODO: get the moderator
            sl.ChangeExpirationDate(newDate,);
            return true;
        }

        public DateTime GetModeratorExpDate(string forumName, string subForumName, string username)
        {
            throw new NotImplementedException();
        }

        public bool LoginUser(string forumName, string username, string pass)
        {
            IForum forum = sl.GetForum(forumName);
            IUser user = sl.MemberLogin(username, pass, forum);
            return user != null;
        }

        public bool LoginSuperAdmin(string username, string pass)
        {
            throw new NotImplementedException();
        }

        public bool InitializeSystem(string username, string pass)
        {
            return sl.InitializeSystem(username, pass);
        }

        public bool ConfirmRegistration(string forumName, string username)
        {
            throw new NotImplementedException();
        }


    }
}
