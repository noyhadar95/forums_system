using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystemClient.CommunicationLayer;
using ForumsSystemClient.Resources.ForumManagement.DomainLayer;
using System.Xml.Linq;
using ForumsSystemClient.Resources.UserManagement.DomainLayer;
//using ForumsSystem.Server.UserManagement.DomainLayer;

namespace AcceptanceTestsBridge
{
   public class ClientBridge : IBridge
    {
        private CL cl;
        // default values for policies params
        private int numOfComplaints = 100;
        private bool blockPassword = false;
        private int numOfMessages = 0;
        private int seniorityInDays = 0;
        private int maxNumOfUsers = 200;
        private int minAge = 1;
        private int maxModerators = 20;
        private Dictionary<Tuple<string,string>, string> sessionKeys =  new Dictionary<Tuple<string, string>, string>();
        private int notifyMode = 1;

        public ClientBridge()
        {
            cl = new CL();
            cl.startTesting();
        }
        #region Add/Create Methods

        public bool CreateForum(string creator,string creatorPass, string forumName, List<UserStub> admins, PoliciesStub forumPolicies)
        {
            List<User> newAdmins = new List<User>();

            foreach (UserStub user in admins)
            {
                User u = new User(user.Username, user.Password, user.Email, DateTime.Today.AddDays(100));
                newAdmins.Add(u);
            }
            SuperAdmin superAdmin = cl.GetSuperAdmin();
            Policies forumPol = ConvertPolicyStubToReal(forumPolicies);
            Policy policy;
            switch (forumPol)
            {
                case Policies.Password:
                    policy = new PasswordPolicy(forumPol, 2, 100);
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
                case Policies.InteractivePolicy:
                    policy = new InteractivePolicy(forumPol, notifyMode);
                    break;
                default:
                    policy = new PasswordPolicy(forumPol, 2, 100);
                    break;
            }
            Forum newForum = cl.CreateForum(superAdmin.userName, superAdmin.password, forumName, policy, newAdmins);
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
            SubForum subForum = cl.CreateSubForum(creator, forumName, subForumName, moderators);
            return subForum != null;
        }

        public int AddThread(string forumName, string subForumName, string publisher, string title, string content)
        {
            return cl.AddThread(forumName, subForumName, publisher, title, content);
        }

        public int AddReplyPost(string forumName, string subForumName, int threadID, string publisher, int postID, string title, string content)
        {
            Post reply = cl.AddReply(forumName, subForumName, threadID, publisher, postID, title, content);
            if (reply == null)
                return -1;
            int replyID = reply.GetId();
            return replyID;
        }

        public bool AddModerator(string forumName, string subForumName, string adminUsername, KeyValuePair<string, DateTime> newMod)
        {
            return cl.AddModerator(forumName, subForumName, adminUsername, newMod);
        }

        #endregion

        #region Delete Methods

        public void DeleteUser(string forumName, string userName)
        {
            cl.DeleteUser(userName, forumName);
        }

        public void DeleteForum(string forumName)
        {
            cl.DeleteForum(forumName);
        }

        public bool DeletePost(string forumName, string subForumName, int threadID, string deleter, int postID)
        {
            return cl.DeletePost(forumName,subForumName,threadID,deleter,postID);
        }

        #endregion


        #region Boolean Queries: IsExist, IsRegistered...

        public bool IsExistForum(string forumName)
        {
            return cl.IsExistForum(forumName);
        }

        public bool IsRegisteredToForum(string username, string forumName)
        {
            return cl.IsRegisteredToForum(username, forumName);
        }

        public bool IsAdmin(string username, string forumName)
        {
            return cl.IsAdmin(username, forumName);
        }

        public bool IsModerator(string forumName, string subForumName, string username)
        {
            return cl.IsModerator(forumName, subForumName, username);
        }

        public bool IsExistThread(string forumName, string subForumName, int threadID)
        {
            return cl.IsExistThread(forumName, subForumName, threadID);
        }

        public bool IsMsgReceived(string forumName, string username, string msgTitle, string msgContent)
        {
            return cl.IsMsgReceived(forumName, username, msgTitle, msgContent);
        }

        public bool IsMsgSent(string forumName, string username, string msgTitle, string msgContent)
        {
            return cl.IsMsgSent(forumName, username, msgTitle, msgContent);
        }

        public bool IsForumHasPolicy(string forumName, PoliciesStub forumPolicy)
        {
            Policies expectedPolicy = ConvertPolicyStubToReal(forumPolicy);
            bool res = cl.CheckIfPolicyExists(forumName, expectedPolicy);
            return res;
        }

        #endregion



        // ---------------------------------- Other Methods


        public bool SetForumProperties(string forumName, string username, PoliciesStub forumPolicies)
        {
            Forum forum = cl.GetForum(forumName);
            User user = forum.getUser(username);
            Policies forumPol = ConvertPolicyStubToReal(forumPolicies);
            Policy policy;
            switch (forumPol)
            {
                case Policies.Password:
                    policy = new PasswordPolicy(forumPol, 2, 100);
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
                    policy = new PasswordPolicy(forumPol, 2, 100);
                    break;
            }
            return cl.SetForumProperties(username, forumName, policy);
        }

        public bool RegisterToForum(string forumName, string username, string password, string email, DateTime dateOfBirth)
        {
           // Forum forum = cl.GetForum(forumName);
            return cl.RegisterToForum( forumName, username, password, email, dateOfBirth, 1, "Bonzo");
        }

        public int CountNestedReplies(string forumName, string subForumName, int threadID, int postID)
        {
            return cl.CountNestedReplies(forumName, subForumName, threadID, postID);
        }

        public bool SendPrivateMsg(string forumName, string senderUsername, string receiverUsername, string msgTitle, string msgContent)
        {
            
            return cl.SendPrivateMessage(forumName, senderUsername, receiverUsername, msgTitle, msgContent);
        }

        public bool EditModeratorExpDate(string forumName, string subForumName, string admin, string moderator, DateTime newDate)
        {
            return cl.ChangeExpirationDate(forumName,subForumName, admin, moderator, newDate);
        }

        public DateTime GetModeratorExpDate(string forumName, string subForumName, string username)
        {
            //Forum forum = cl.GetForum(forumName);
            //SubForum subForum = forum.getSubForum(subForumName);
            return cl.GetModeratorExpDate(forumName,subForumName, username);
        }

        public bool LoginUser(string forumName, string username, string pass)
        {
            Tuple<User,string> t = cl.MemberLogin(forumName, username, pass);
            sessionKeys.Add(new Tuple<string, string>(forumName,username), t.Item2);
            return t != null;
        }

        public bool LoginSuperAdmin(string username, string pass)
        {
            return cl.LoginSuperAdmin(username, pass);
        }

        public bool InitializeSystem(string username, string pass)
        {
            return cl.InitializeSystem(username, pass);
        }

        public bool ConfirmRegistration(string forumName, string username)
        {
            return cl.ConfirmRegistration(forumName, username,"");
        }

        public int GetOpenningPostID(string forumName, string subForumName, int threadID)
        {
            return cl.GetOpenningPostID(forumName, subForumName, threadID);
        }

        //===============================================================
        //===============================================================
        //===============================================================
        //===============================================================


        public bool ShouldCleanup(string className, string methodName)
        {
            try
            {
                XDocument doc = XDocument.Load
                    ("C:\\Users\\omerh\\Documents\\GitHub\\forums_system\\ForumsSystem\\AcceptanceTests\\ServerTests\\AddModeratorTestsData.xml");
                //TODO: add new xml file for client
                var classVals = doc.Descendants(className);
                var methodVals = classVals.ToArray()[0].Element(methodName);
                if (methodVals == null)
                    return true;
                string val = methodVals.Value;
                if (string.Equals(val, "true", StringComparison.CurrentCultureIgnoreCase))
                    return true;
                return false;
            }
            catch (Exception e)
            {
                return true;
            }
        }

        public void AddFriend(string forumName, string username1, string username2)
        {
            cl.AddFriend(forumName, username1, username2);
        }
        
        public bool IsExistNotificationOfPost(string forumName, string username, int postId)
        {
            PostNotification[] notifications = cl.GetPostNotifications(forumName,username).ToArray();
            PostNotification temp;
            for (int i = 0; i < notifications.Length; i++)
            {
                temp = notifications[i];
                if (temp.id == postId)
                    return true;
            }
            return false;
        }

        public void EditPost(string forumName, string subForumName, int threadId, string editor, int postId, string newTitle, string newContent)
        {
            if (newTitle == "" && newContent == "")
                return;//illegal post

            cl.EditPost(forumName, subForumName, threadId, editor, postId, newTitle, newContent);
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
            return cl.RemoveModerator(forumName,subForumName, remover, moderatorName);
        }

        public int GetNumOfPostsInForumByMember(string forumName, string adminUserName, string username)
        {
            try
            {
                return cl.ReportNumOfPostsByMember(forumName,adminUserName, username);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public List<string> GetListOfModerators(string forumName, string subForumName, string adminUserName)
        {
            try
            {
                return cl.GetModeratorsList(forumName, subForumName,adminUserName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        //TUPLE: postId,title,content
        public List<Tuple<int, string, string>> GetPostsInForumByUser(string forumName, string adminUserName, string username)
        {
           // IForum forum = sl.GetForum(forumName);
           // IUser admin = forum.getUser(adminUserName);
            try
            {
                List<Post> posts = cl.ReportPostsByMember(forumName, adminUserName,username);
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
            return cl.GetNumOfForums(username, password);
        }

        public Dictionary<string, List<Tuple<string, string>>> GetMultipleUsersInfo(string userName, string password)
        {
            return cl.GetMultipleUsersInfoBySuperAdmin(userName, password);
        }

        public List<string> GetNotifications(string forumName, string username)
        {
            List<PrivateMessage> notifications= cl.GetNotifications(forumName, username);
            List<string> res = new List<string>();
            foreach (PrivateMessage msg in notifications)
            {
                res.Add(msg.title);
            }
            return res;
        }

        public Tuple<string, string, DateTime, string> GetModeratorAppointmentsDetails(string forumName, string subForumName, string adminUserName1, string username1)
        {
            return cl.GetModeratorAppointmentsDetails(forumName, subForumName, adminUserName1, username1);
        }

        public void LogoutUser(string forumName, string username)
        {
            if (sessionKeys.ContainsKey(new Tuple<string, string>(forumName, username)))
                sessionKeys.Remove(new Tuple<string, string>(forumName, username)); 
            cl.MemberLogout(forumName, username);
        }

        public List<Tuple<string, string, DateTime, string>> ReportModeratorsDetails(string forumName, string adminUserName1)
        {
            throw new NotImplementedException();
        }

        public string getUserClientSession(string forumName, string userName)
        {
            string key ="";
            if (!sessionKeys.TryGetValue(new Tuple<string, string>(forumName, userName), out key))
                key = "";
            return key;
        }

        public bool LoginUserWithClientSession(string forumName, string username, string pass, string clientServer)
        {
            return cl.MemberLogin(forumName, username, pass, clientServer) != null;
        }

        public bool CreateForum(string creator, string creatorPass, string forumName, List<UserStub> admins, PoliciesStub forumPolicies, params object[] policyParams)
        {
            List<User> newAdmins = new List<User>();

            foreach (UserStub user in admins)
            {
                User u = new User(user.Username, user.Password, user.Email, DateTime.Today.AddDays(100));
                newAdmins.Add(u);
            }
            SuperAdmin superAdmin = cl.GetSuperAdmin();
            Policies forumPol = ConvertPolicyStubToReal(forumPolicies);
            Policy policy;
            switch (forumPol)
            {
                case Policies.Password:
                    policy = new PasswordPolicy(forumPol, (int)policyParams.ElementAt(0), (int)policyParams.ElementAt(1));
                    break;
                case Policies.Authentication:
                    policy = new AuthenticationPolicy(forumPol);
                    break;
                case Policies.ModeratorSuspension:
                    policy = new ModeratorSuspensionPolicy(forumPol, (int)policyParams.ElementAt(0));
                    break;
                case Policies.Confidentiality:
                    policy = new ConfidentialityPolicy(forumPol, (bool)policyParams.ElementAt(0));
                    break;
                case Policies.ModeratorAppointment:
                    policy = new ModeratorAppointmentPolicy(forumPol, (int)policyParams.ElementAt(0), (int)policyParams.ElementAt(1), (int)policyParams.ElementAt(2));
                    break;
                case Policies.AdminAppointment:
                    policy = new AdminAppointmentPolicy(forumPol, (int)policyParams.ElementAt(0), (int)policyParams.ElementAt(1), (int)policyParams.ElementAt(2));
                    break;
                case Policies.MemberSuspension:
                    policy = new MemberSuspensionPolicy(forumPol, (int)policyParams.ElementAt(0));
                    break;
                case Policies.UsersLoad:
                    policy = new UsersLoadPolicy(forumPol, (int)policyParams.ElementAt(0));
                    break;
                case Policies.MinimumAge:
                    policy = new MinimumAgePolicy(forumPol, (int)policyParams.ElementAt(0));
                    break;
                case Policies.MaxModerators:
                    policy = new MaxModeratorsPolicy(forumPol, (int)policyParams.ElementAt(0));
                    break;
                default:
                    policy = new PasswordPolicy(forumPol, (int)policyParams.ElementAt(0), (int)policyParams.ElementAt(1));
                    break;
            }
            Forum newForum = cl.CreateForum(superAdmin.userName, superAdmin.password, forumName, policy, newAdmins);
            return newForum != null;
        }

     
        public bool recievedNotification(string forumName, string userName)
        {
            return NotificationHelper.recievedNotification();
        }
    }
}
