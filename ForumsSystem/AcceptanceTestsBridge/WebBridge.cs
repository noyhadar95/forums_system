using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Communication;
using WebApplication.Resources.ForumManagement.DomainLayer;
using System.Xml.Linq;
using WebApplication.Resources.UserManagement.DomainLayer;
using ForumsSystemClient.CommunicationLayer;

namespace AcceptanceTestsBridge
{
    public class WebBridge : IBridge
    {
        private WebApplication.Communication.CL cl;
        // default values for policies params
        private int numOfComplaints = 100;
        private bool blockPassword = false;
        private int numOfMessages = 0;
        private int seniorityInDays = 0;
        private int maxNumOfUsers = 200;
        private int minAge = 1;
        private int maxModerators = 20;
        private Dictionary<Tuple<string, string>, string> sessionKeys = new Dictionary<Tuple<string, string>, string>();

        public WebBridge()
        {
            cl = new WebApplication.Communication.CL();
        }
        #region Add/Create Methods




        public int AddThread(string forumName, string subForumName, string publisher, string title, string content)
        {
            return cl.AddThread(forumName, subForumName, publisher, title, content);
        }

        /*   public int AddReplyPost(string forumName, string subForumName, int threadID, string publisher, int postID, string title, string content)
           {
               Post reply = cl.AddReply(forumName, subForumName, threadID, publisher, postID, title, content);
               if (reply == null)
                   return -1;
               int replyID = reply.GetId();
               return replyID;
           }*/

        public bool AddModerator(string forumName, string subForumName, string adminUsername, KeyValuePair<string, DateTime> newMod)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Delete Methods

        public void DeleteUser(string forumName, string userName)
        {
            throw new NotImplementedException();
        }

        public void DeleteForum(string forumName)
        {
            throw new NotImplementedException();
        }

        public bool DeletePost(string forumName, string subForumName, int threadID, string deleter, int postID)
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

        public bool IsMsgReceived(string forumName, string username, string msgTitle, string msgContent)
        {
            throw new NotImplementedException();
        }

        public bool IsMsgSent(string forumName, string username, string msgTitle, string msgContent)
        {
            throw new NotImplementedException();
        }



        #endregion



        // ---------------------------------- Other Methods


        public bool RegisterToForum(string forumName, string username, string password, string email, DateTime dateOfBirth)
        {
            // Forum forum = cl.GetForum(forumName);
            throw new NotImplementedException();
        }

        public int CountNestedReplies(string forumName, string subForumName, int threadID, int postID)
        {
            throw new NotImplementedException();
        }

        public bool SendPrivateMsg(string forumName, string senderUsername, string receiverUsername, string msgTitle, string msgContent)
        {

            throw new NotImplementedException();
        }

        public bool EditModeratorExpDate(string forumName, string subForumName, string admin, string moderator, DateTime newDate)
        {
            throw new NotImplementedException();
        }

        public DateTime GetModeratorExpDate(string forumName, string subForumName, string username)
        {
            //Forum forum = cl.GetForum(forumName);
            //SubForum subForum = forum.getSubForum(subForumName);
            throw new NotImplementedException();
        }

        public bool LoginUser(string forumName, string username, string pass)
        {
            //   Tuple<User, string> t = cl.MemberLogin(forumName, username, pass);
            User t = cl.MemberLogin(forumName, username, pass);
            //    sessionKeys.Add(new Tuple<string, string>(forumName, username), t.Item2);
            return t != null;
        }

        public bool LoginSuperAdmin(string username, string pass)
        {
            throw new NotImplementedException();
        }

        public bool InitializeSystem(string username, string pass)
        {
            throw new NotImplementedException();
        }

        public bool ConfirmRegistration(string forumName, string username)
        {
            throw new NotImplementedException();
        }

        public int GetOpenningPostID(string forumName, string subForumName, int threadID)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException(); ;
        }


        public void EditPost(string forumName, string subForumName, int threadId, string editor, int postId, string newTitle, string newContent)
        {
            if (newTitle == "" && newContent == "")
                return;//illegal post

            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public int GetNumOfPostsInForumByMember(string forumName, string adminUserName, string username)
        {
            throw new NotImplementedException();
        }

        public List<string> GetListOfModerators(string forumName, string subForumName, string adminUserName)
        {
            throw new NotImplementedException();
        }

        //TUPLE: postId,title,content

        public int GetNumOfForums(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, List<Tuple<string, string>>> GetMultipleUsersInfo(string userName, string password)
        {
            throw new NotImplementedException();
        }



        public Tuple<string, string, DateTime, string> GetModeratorAppointmentsDetails(string forumName, string subForumName, string adminUserName1, string username1)
        {
            throw new NotImplementedException();
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
            string key = "";
            if (!sessionKeys.TryGetValue(new Tuple<string, string>(forumName, userName), out key))
                key = "";
            return key;
        }

        public bool LoginUserWithClientSession(string forumName, string username, string pass, string clientServer)
        {
            //return cl.MemberLogin(forumName, username, pass, clientServer) != null;
            return true;
        }

        public bool CreateForum(string creator, string creatorPass, string forumName, List<UserStub> admins, PoliciesStub forumPolicies)
        {
            throw new NotImplementedException();
        }

        public bool CreateForum(string creator, string creatorPass, string forumName, List<UserStub> admins, PoliciesStub forumPolicies, params object[] policyParams)
        {
            throw new NotImplementedException();
        }

        public bool CreateSubForum(string creator, string forumName, string subForumName, Dictionary<string, DateTime> moderators)
        {
            throw new NotImplementedException();
        }

        public int AddReplyPost(string forumName, string subForumName, int threadID, string publisher, int postID, string title, string content)
        {
            throw new NotImplementedException();
        }

        public bool IsForumHasPolicy(string forumName, PoliciesStub forumPolicy)
        {
            throw new NotImplementedException();
        }

        public bool SetForumProperties(string forumName, string username, PoliciesStub forumPolicies)
        {
            throw new NotImplementedException();
        }

        public bool IsExistNotificationOfPost(string forumName, string username, int postId)
        {
            throw new NotImplementedException();
        }

        public List<Tuple<int, string, string>> GetPostsInForumByUser(string forumName, string adminUserName, string userEmail)
        {
            throw new NotImplementedException();
        }

        public List<string> GetNotifications(string forumName, string username)
        {
            throw new NotImplementedException();
        }

        public bool recievedNotification(string forumName, string userName)
        {
            return true;
        }
    }
}
