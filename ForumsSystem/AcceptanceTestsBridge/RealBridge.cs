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

        public bool CreateForum(string creator, string forumName, List<UserStub> admins, PoliciesStub forumPolicies)
        {
            List<IUser> newAdmins = new List<IUser>();

            foreach (UserStub user in admins)
            {
                IUser u = new User(user.Username, user.Password, user.Email, DateTime.Today.AddDays(100));
                newAdmins.Add(u);
            }
            SuperAdmin superAdmin = SuperAdmin.GetInstance();
            Policies forumPol = ConvertPolicyStubToReal(forumPolicies);
            Policy policy = new PasswordPolicy(forumPol, 6);
            IForum newForum = sl.CreateForum(superAdmin, forumName, policy, newAdmins);
            return newForum != null;
        }

        private Policies ConvertPolicyStubToReal(PoliciesStub forumPolicies)
        {
            Policies res;
            switch (forumPolicies)
            {
                case PoliciesStub.Password:
                    res = Policies.Password;
                    break;
                case PoliciesStub.Authentication:
                    res = Policies.Authentication;
                    break;
                case PoliciesStub.ModeratorSuspension:
                    res = Policies.ModeratorSuspension;
                    break;
                case PoliciesStub.Confidentiality:
                    res = Policies.Confidentiality;
                    break;
                case PoliciesStub.ModeratorAppointment:
                    res = Policies.ModeratorAppointment;
                    break;
                case PoliciesStub.AdminAppointment:
                    res = Policies.AdminAppointment;
                    break;
                case PoliciesStub.MemberSuspension:
                    res = Policies.MemberSuspension;
                    break;
                case PoliciesStub.UsersLoad:
                    res = Policies.UsersLoad;
                    break;
                case PoliciesStub.MinimumAge:
                    res = Policies.MinimumAge;
                    break;
                case PoliciesStub.MaxModerators:
                    res = Policies.MaxModerators;
                    break;
                default:
                    res = Policies.Password;
                    break;
            }
            return res;
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
            if (thread == null)
                return -1;
            return thread.id;
        }

        public int AddReplyPost(string forumName, string subForumName, int threadID, string publisher, int postID, string title, string content)
        {
            IForum forum = sl.GetForum(forumName);
            IUser user = forum.getUser(publisher);
            ISubForum subForum = forum.getSubForum(subForumName);
            Thread thread = subForum.GetThreadById(threadID);
            Post post = thread.GetPostById(postID);
            Post reply = sl.AddReply(post, user, title, content);
            if (reply == null)
                return -1;
            int replyID = reply.GetId();
            return replyID;
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
            sl.DeleteUser(userName, forumName);
        }

        public void DeleteForum(string forumName)
        {
            sl.DeleteForum(forumName);
        }

        public bool DeletePost(string forumName, string subForumName, int threadID, string deleter, int postID)
        {
            IForum forum = sl.GetForum(forumName);
            IUser user = forum.getUser(deleter);
            ISubForum subForum = forum.getSubForum(subForumName);
            Thread thread = subForum.GetThreadById(threadID);
            Post post = thread.GetPostById(postID);
            return sl.DeletePost(user, post);
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
            return true;
        }

        public bool IsModerator(string forumName, string subForumName, string username)
        {
            return sl.IsModerator(forumName, subForumName, username);
        }

        public bool IsExistThread(string forumName, string subForumName, int threadID)
        {
            //TODO: implement
            return true;
        }

        public bool IsMsgReceived(string forumName, string username, string msgTitle, string msgContent)
        {
            IForum forum = sl.GetForum(forumName);
            IUser user = forum.getUser(username);
            return sl.IsMsgReceived(user, msgTitle, msgContent);
        }

        public bool IsMsgSent(string forumName, string username, string msgTitle, string msgContent)
        {
            IForum forum = sl.GetForum(forumName);
            IUser user = forum.getUser(username);
            return sl.IsMsgSent(user, msgTitle, msgContent);
        }

        #endregion



        // ---------------------------------- Other Methods


        public bool SetForumProperties(string forumName, string username, PoliciesStub forumPolicies)
        {
            IForum forum = sl.GetForum(forumName);
            IUser user = forum.getUser(username);
            Policies forumPol = ConvertPolicyStubToReal(forumPolicies);
            Policy policy = new PasswordPolicy(forumPol, 6);
            return sl.SetForumProperties(user, forum, policy);
        }

        public bool RegisterToForum(string forumName, string username, string password, string email, DateTime dateOfBirth)
        {
            IForum forum = sl.GetForum(forumName);
            return sl.RegisterToForum(new User(), forum, username, password, email, dateOfBirth);
        }

        public int CountNestedReplies(string forumName, string subForumName, int threadID, int postID)
        {
            IForum forum = sl.GetForum(forumName);
            ISubForum subForum = forum.getSubForum(subForumName);
            return sl.CountNestedReplies(subForum, threadID, postID);
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
            IForum forum = sl.GetForum(forumName);
            ISubForum subForum = forum.getSubForum(subForumName);
            return sl.GetModeratorExpDate(subForum, username);
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

        public int GetOpenningPostID(string forumName, string subForumName, int threadID)
        {
            return sl.GetOpenningPostID(forumName, subForumName, threadID);
        }



    }
}
