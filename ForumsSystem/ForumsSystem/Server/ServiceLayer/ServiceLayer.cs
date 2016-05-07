using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.ForumManagement.DomainLayer;
using ForumsSystem.Server.UserManagement.DomainLayer;

namespace ForumsSystem.Server.ServiceLayer
{
    public class ServiceLayer : IServiceLayer
    {
        private ForumsSystem.Server.ForumManagement.DomainLayer.System sys;


        public ServiceLayer()
        {
            sys = new ForumsSystem.Server.ForumManagement.DomainLayer.System();
        }

        // initialize the system with the username and pass as the super admin login info
        public bool InitializeSystem(string username, string pass)
        {
            if (username != null && username != "" && pass != null && pass != "")
            {
                SuperAdmin.CreateSuperAdmin(username, pass, sys);
                return true;
            }
            else
                return false;
        }

        public IForum CreateForum(SuperAdmin creator, string name, Policy properties, List<IUser> adminUsername)
        {
            return creator.createForum(name, properties, adminUsername);
        }

        public bool SetForumProperties(IUser user, IForum forum, Policy properties)
        {
            return user.SetForumProperties(forum, properties);
        }

        public bool ChangeForumProperties(IUser user, IForum forum, Policy properties)
        {
            return user.ChangeForumProperties(forum, properties);
        }

        public bool RegisterToForum(IUser guest, IForum forum, string userName, string password, string email, DateTime dateOfBirth)
        {
            return guest.RegisterToForum(userName, password, forum, email, dateOfBirth);

        }

        public ISubForum CreateSubForum(IUser creator, string name, Dictionary<string, DateTime> moderators)
        {

            return creator.createSubForum(name, moderators);

        }

        public Thread AddThread(ISubForum subForum, IUser publisher, string title, string content)
        {
            return publisher.createThread(subForum, title, content);

        }

        public Post AddReply(Post post, IUser publisher, string title, string content)
        {
            return publisher.postReply(post, post.Thread, title, content);

        }

        public IUser MemberLogin(string username, string password, IForum forum)
        {
            return forum.Login(username, password);
        }

        public PrivateMessage SendPrivateMessage(IUser from, string to, string title, string content)
        {
            return from.SendPrivateMessage(to, title, content);


        }

        public bool ChangeExpirationDate(IUser admin, DateTime newDate, string moderator, ISubForum subforum)
        {
            return admin.editExpirationTimeOfModerator(moderator, newDate, subforum);
        }

        public bool DeletePost(IUser deleter, Post post)
        {
            return deleter.deletePost(post);
        }

        public bool DeleteForumProperties(IUser user, IForum forum, List<Policies> properties)
        {
            return user.DeleteForumProperties(forum, properties);

        }

        public IForum GetForum(string forumName)
        {

            return SuperAdmin.GetInstance().forumSystem.getForum(forumName);
        }

        public bool AddModerator(IUser admin, ISubForum subForum, string username, DateTime expiratoinDate)
        {
            return admin.appointModerator(username, expiratoinDate, subForum);
        }

        public void removeForum(string forumName)
        {
            SuperAdmin.GetInstance().removeForum(forumName);
        }

        public bool ConfirmRegistration(string forumName, string username)
        {
            IForum forum = this.GetForum(forumName);
            if (forum == null)
                return false;
            IUser user = forum.GetWaitingUser(username);
            if (user == null)
                return false;
            user.AcceptEmail();
            return true;
        }

        public bool LoginSuperAdmin(string username, string pass)
        {
            return SuperAdmin.GetInstance().Login(username, pass);
        }

        public DateTime GetModeratorExpDate(ISubForum subForum, string username)
        {
            return subForum.getModeratorByUserName(username).expirationDate;
        }

        public int CountNestedReplies(ISubForum subforum, int threadID, int postID)
        {
            return ((SubForum)subforum).GetThreadById(threadID).GetPostById(postID).GetNumOfNestedReplies();
        }

        public bool IsMsgSent(IUser user, string msgTitle, string msgContent)
        {
            return user.IsMessageSent(msgTitle, msgContent);
        }

        public bool IsMsgReceived(IUser user, string msgTitle, string msgContent)
        {
            return user.IsMessageReceived(msgTitle, msgContent);
        }

        public bool IsModerator(string forumName, string subForumName, string username)
        {
            SuperAdmin superAdmin = SuperAdmin.GetInstance();
            ForumsSystem.Server.ForumManagement.DomainLayer.System sys = superAdmin.forumSystem;
            ISubForum subforum = sys.getForum(forumName).getSubForum(subForumName);
            return subforum.isModerator(username);
        }

        public bool IsRegisteredToForum(string username, string forumName)
        {
            SuperAdmin superAdmin = SuperAdmin.GetInstance();
            ForumsSystem.Server.ForumManagement.DomainLayer.System sys = superAdmin.forumSystem;
            IForum forum = sys.getForum(forumName);
            return forum.isUserMember(username);
        }

        public bool IsExistForum(string forumName)
        {
            SuperAdmin superAdmin = SuperAdmin.GetInstance();
            ForumsSystem.Server.ForumManagement.DomainLayer.System sys = superAdmin.forumSystem;
            IForum forum = sys.getForum(forumName);
            if (forum == null)
                return false;
            return true;
        }

        public bool DeletePost(string forumName, string subForumName, int threadID, int postID)
        {
            SuperAdmin superAdmin = SuperAdmin.GetInstance();
            ForumsSystem.Server.ForumManagement.DomainLayer.System sys = superAdmin.forumSystem;
            Thread thread = sys.getForum(forumName).getSubForum(subForumName).GetThreadById(threadID);
            IUser user = thread.GetOpeningPost().getPublisher();
            return user.deletePost(thread.GetPostById(postID));
        }

        public void DeleteForum(string forumName)
        {
            SuperAdmin superAdmin = SuperAdmin.GetInstance();
            ForumsSystem.Server.ForumManagement.DomainLayer.System sys = superAdmin.forumSystem;
            sys.removeForum(forumName);
        }

        public void DeleteUser(string userName, string forumName)
        {
            SuperAdmin superAdmin = SuperAdmin.GetInstance();
            ForumsSystem.Server.ForumManagement.DomainLayer.System sys = superAdmin.forumSystem;
            IForum forum = sys.getForum(forumName);
            forum.DeleteUser(userName);
        }

        public int GetOpenningPostID(string forumName, string subForumName, int threadID)
        {
            SuperAdmin superAdmin = SuperAdmin.GetInstance();
            ForumsSystem.Server.ForumManagement.DomainLayer.System sys = superAdmin.forumSystem;
            Thread thread = sys.getForum(forumName).getSubForum(subForumName).GetThreadById(threadID);
            return thread.GetOpeningPost().GetId();
        }

        public bool IsAdmin(string username, string forumName)
        {
            SuperAdmin superAdmin = SuperAdmin.GetInstance();
            ForumsSystem.Server.ForumManagement.DomainLayer.System sys = superAdmin.forumSystem;
            IForum forum = sys.getForum(forumName);
            IUser user = sys.getForum(forumName).getUser(username);
            ForumsSystem.Server.UserManagement.DomainLayer.Type type = user.getType();
            return (type is Admin);

        }
        public bool IsExistThread(ISubForum subForum, int threadID)
        {
            return subForum.GetThreadById(threadID) != null;
        }

        public Dictionary<string, List<Tuple<string, string>>> GetMultipleUsersInfoBySuperAdmin(string userName, string password)
        {
            if (SuperAdmin.GetInstance().userName == userName && SuperAdmin.GetInstance().password == password)
                return sys.GetMultipleUsersInfo();
            else
                return null;
        }

        public int GetNumOfForums(string userName, string password)
        {
            if (SuperAdmin.GetInstance().userName == userName && SuperAdmin.GetInstance().password == password)
                return sys.GetNumOfForums();
            else
                return -1;
        }

        public List<PrivateMessageNotification> GetNotifications(string forumName, string username)
        {
            IForum forum = sys.getForum(forumName);
            IUser user = sys.getForum(forumName).getUser(username);
            return user.GetPrivateMessageNotifications();
        }

        public void AddFriend(string forumName, string username1, string username2)
        {
            IForum forum = GetForum(forumName);
            IUser user1 = forum.getUser(username1);
            IUser user2 = forum.getUser(username2);
            user1.addFriend(user2);
            user2.acceptFriend(user1);
        }

        public Tuple<string, string, DateTime, string> GetModeratorAppointmentsDetails(string forumName, string subForumName, string adminUserName1, string username1)
        {
            throw new NotImplementedException();
        }
    }
}
