using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.ServiceLayer;
using ForumsSystem.Server.ForumManagement.DomainLayer;
using ForumsSystem.Server.UserManagement.DomainLayer;
using System.Xml.Linq;

namespace AcceptanceTestsBridge
{
    class RealBridge : IBridge
    {

        private IServiceLayer sl;
        // default values for policies params
        private int numOfComplaints = 100;
        private bool blockPassword = false;
        private int numOfMessages = 0;
        private int seniorityInDays = 0;
        private int maxNumOfUsers = 200;
        private int minAge = 1;
        private int maxModerators = 20;

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
            Policy policy;
            switch (forumPol)
            {
                case Policies.Password:
                    policy = new PasswordPolicy(forumPol, 2,100);
                    break;
                case Policies.Authentication:
                    policy = new AuthenticationPolicy(forumPol);
                    break;
                case Policies.ModeratorSuspension:
                    policy = new ModeratorSuspensionPolicy(forumPol, numOfComplaints);
                    break;
                case Policies.Confidentiality:
                    policy = new ConfidentialityPolicy(forumPol, blockPassword);
                    break;
                case Policies.ModeratorAppointment:
                    policy = new ModeratorAppointmentPolicy(forumPol, seniorityInDays, numOfMessages, numOfComplaints);
                    break;
                case Policies.AdminAppointment:
                    policy = new AdminAppointmentPolicy(forumPol, seniorityInDays, numOfMessages, numOfComplaints);
                    break;
                case Policies.MemberSuspension:
                    policy = new MemberSuspensionPolicy(forumPol, numOfComplaints);
                    break;
                case Policies.UsersLoad:
                    policy = new UsersLoadPolicy(forumPol, maxNumOfUsers);
                    break;
                case Policies.MinimumAge:
                    policy = new MinimumAgePolicy(forumPol, minAge);
                    break;
                case Policies.MaxModerators:
                    policy = new MaxModeratorsPolicy(forumPol, maxModerators);
                    break;
                default:
                    policy = new PasswordPolicy(forumPol, 2,100);
                    break;
            }
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
            return sl.IsAdmin(username, forumName);
        }

        public bool IsModerator(string forumName, string subForumName, string username)
        {
            return sl.IsModerator(forumName, subForumName, username);
        }

        public bool IsExistThread(string forumName, string subForumName, int threadID)
        {
            IForum forum = sl.GetForum(forumName);
            ISubForum subForum = forum.getSubForum(subForumName);
            return sl.IsExistThread(subForum, threadID);
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

        public bool IsForumHasPolicy(string forumName, PoliciesStub forumPolicy)
        {
            IForum forum = sl.GetForum(forumName);
            Policy policy = forum.GetPolicy();
            Policies expectedPolicy = ConvertPolicyStubToReal(forumPolicy);
            bool res = policy.CheckIfPolicyExists(expectedPolicy);
            return res;
        }

        #endregion



        // ---------------------------------- Other Methods


        public bool SetForumProperties(string forumName, string username, PoliciesStub forumPolicies)
        {
            IForum forum = sl.GetForum(forumName);
            IUser user = forum.getUser(username);
            Policies forumPol = ConvertPolicyStubToReal(forumPolicies);
            Policy policy;
            switch (forumPol)
            {
                case Policies.Password:
                    policy = new PasswordPolicy(forumPol, 2,100);
                    break;
                case Policies.Authentication:
                    policy = new AuthenticationPolicy(forumPol);
                    break;
                case Policies.ModeratorSuspension:
                    policy = new ModeratorSuspensionPolicy(forumPol, numOfComplaints);
                    break;
                case Policies.Confidentiality:
                    policy = new ConfidentialityPolicy(forumPol, blockPassword);
                    break;
                case Policies.ModeratorAppointment:
                    policy = new ModeratorAppointmentPolicy(forumPol, seniorityInDays, numOfMessages, numOfComplaints);
                    break;
                case Policies.AdminAppointment:
                    policy = new AdminAppointmentPolicy(forumPol, seniorityInDays, numOfMessages, numOfComplaints);
                    break;
                case Policies.MemberSuspension:
                    policy = new MemberSuspensionPolicy(forumPol, numOfComplaints);
                    break;
                case Policies.UsersLoad:
                    policy = new UsersLoadPolicy(forumPol, maxNumOfUsers);
                    break;
                case Policies.MinimumAge:
                    policy = new MinimumAgePolicy(forumPol, minAge);
                    break;
                case Policies.MaxModerators:
                    policy = new MaxModeratorsPolicy(forumPol, maxModerators);
                    break;
                default:
                    policy = new PasswordPolicy(forumPol, 2,100);
                    break;
            }
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

        //===============================================================
        //===============================================================
        //===============================================================
        //===============================================================


        public bool ShouldCleanup(string className,string methodName)
        {
            try {
                XDocument doc = XDocument.Load
                    ("C:\\Users\\omerh\\Documents\\GitHub\\forums_system\\ForumsSystem\\AcceptanceTests\\ServerTests\\AddModeratorTestsData.xml");

                var classVals = doc.Descendants(className);
                var methodVals = classVals.ToArray()[0].Element(methodName);
                if (methodVals == null)
                    return true;
                string val = methodVals.Value;
                if (string.Equals(val, "true", StringComparison.CurrentCultureIgnoreCase))
                    return true;
                return false;
            }
            catch(Exception e)
            {
                return true;
            }
        }

        public void AddFriend(string forumName, string username1, string username2)
        {
            //IForum forum = sl.GetForum(forumName);
            //IUser user1 = forum.getUser(username1);
            //IUser user2 = forum.getUser(username2);
            sl.AddFriend(forumName, username1, username2);
        }

        public bool IsExistNotificationOfPost(string forumName,string username, int postId)
        {
            IForum forum = sl.GetForum(forumName);
            IUser user = forum.getUser(username);
            PostNotification[] notifications = user.GetPostNotifications().ToArray();
            PostNotification temp;
            for (int i = 0; i < notifications.Length; i++)
            {
                temp = notifications[i];
                if (temp.id == postId)
                    return true;
            }
            return false;
        }

        public void EditPost(string forumName,string subForumName,int threadId,string editor, int postId, string newTitle, string newContent)
        {
            if (newTitle == "" && newContent == "")
                return;//illegal post

            IForum forum = sl.GetForum(forumName);
            ISubForum subforum= forum.getSubForum(subForumName);
            Thread thread= subforum.GetThreadById(threadId);
            Post post = thread.GetPostById(postId);
            post.Title = newTitle;
            post.Content = newContent;
        }

      /*  public void DeletePost(string forumName, string subForumName, int threadId, string deleter, int postId)
        {
            IForum forum = sl.GetForum(forumName);
            IUser user = forum.getUser(deleter);
            ISubForum subforum = forum.getSubForum(subForumName);
            Thread thread = subforum.GetThreadById(threadId);
            Post post = thread.GetPostById(postId);
        }
        */
        public bool RemoveModerator(string forumName, string subForumName, string remover, string moderatorName)
        {
            IForum forum = sl.GetForum(forumName);
            ISubForum subforum = forum.getSubForum(subForumName);
            Moderator moderator = subforum.getModeratorByUserName(moderatorName);
            //IUser user = forum.getUser(remover);
            //IUser moderator = forum.getUser(moderatorName);
            if (moderator == null)
                return false;
          //  if (!moderator.CanBeDeletedBy(remover))
          //      return false;
            return subforum.removeModerator(remover, moderatorName);
        }

        public int GetNumOfPostsInForumByMember(string forumName, string adminUserName, string username)
        {
            IForum forum = sl.GetForum(forumName);
            IUser admin = forum.getUser(adminUserName);
            try
            {
                return admin.ReportNumOfPostsByMember(username);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public List<string> GetListOfModerators(string forumName, string subForumName, string adminUserName)
        {
            IForum forum = sl.GetForum(forumName);
            IUser admin = forum.getUser(adminUserName);
            ISubForum subforum = forum.getSubForum(subForumName);
            try
            {
                return admin.GetModeratorsList(subforum);
            }
            catch (Exception)
            {
                return null;
            }
        }
    
        //TUPLE: postId,title,content
        public List<Tuple<int, string, string>> GetPostsInForumByUser(string forumName, string subForumName, string adminUserName, string moderatorName)
        {
            IForum forum = sl.GetForum(forumName);
            IUser admin = forum.getUser(adminUserName);
            try
            {
                List<Post> posts = admin.ReportPostsByMember(moderatorName);
                List<Tuple<int, string, string>> res = new List<Tuple<int, string, string>>();
                foreach (Post post in posts)
                {
                    res.Add(new Tuple<int, string, string>(post.GetId(), post.Title, post.Content));
                }
                return res;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public int GetNumOfForums(string username, string password)
        {
            return sl.GetNumOfForums(username, password);
        }

        public Dictionary<string, List<Tuple<string, string>>> GetMultipleUsersInfo(string userName,string password)
        {
            return sl.GetMultipleUsersInfoBySuperAdmin(userName,password);
        }

        public List<string> GetNotifications(string forumName, string username)
        {
            List<PrivateMessageNotification>notif= sl.GetNotifications(forumName, username);
            List<string> res = new List<string>();
            if (notif == null)
                return res;
            foreach (PrivateMessageNotification msg in notif)
            {
                res.Add(msg.title);
            }
            return res;
        }

        public Tuple<string, string, DateTime, string> GetModeratorAppointmentsDetails(string forumName, string subForumName, string adminUserName1, string username1)
        {
            return sl.GetModeratorAppointmentsDetails(forumName, subForumName, adminUserName1, username1);
        }
    }
}
