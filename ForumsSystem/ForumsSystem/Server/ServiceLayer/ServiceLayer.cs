﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.ForumManagement.DomainLayer;
using ForumsSystem.Server.UserManagement.DomailLayer;
using ForumsSystem.Server.UserManagement.DomainLayer;
using ForumsSystem.Server.ForumManagement.Data_Access_Layer;

namespace ForumsSystem.Server.ServiceLayer
{
    public class ServiceLayer : IServiceLayer
    {
        private ForumsSystem.Server.ForumManagement.DomainLayer.System sys;

        public ServiceLayer()
        {
            SuperAdmin sa = SuperAdmin.GetInstance();
            if (sa == null)
                sys = new ForumsSystem.Server.ForumManagement.DomainLayer.System();
            else
                sys = sa.forumSystem;
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

        public Forum CreateForum(string creatorName,string password, string name, Policy properties, List<User> adminUsername)
        {
            List<User> serverAdmins = new List<User>();
            foreach (User admin in adminUsername)
            {
                serverAdmins.Add(new User(admin));
            }
            SuperAdmin creator;
            if (!SuperAdmin.GetInstance().userName.Equals(creatorName))
                return null;
            if (!SuperAdmin.GetInstance().password.Equals(password))
                return null;
            creator = SuperAdmin.GetInstance();
           // creator.Login(creatorName, password);//TODO ?????
            return creator.createForum(name, properties, serverAdmins);
        }

        public bool SetForumProperties(string username, string forumName, Policy properties)
        {
            IForum forum = GetForum(forumName);
            IUser user = forum.getUser(username);
            return user.SetForumProperties(forum, properties);
        }

        public bool ChangeForumProperties(string username, string forumName, Policy properties)
        {
            IForum forum = GetForum(forumName);
            IUser user = forum.getUser(username);
            return user.ChangeForumProperties(forum, properties);
        }

        public bool RegisterToForum(string forumName, string guestName, string password, string email, DateTime dob)
        {
            
            IForum forum = GetForum(forumName);

            //IUser guest = forum.GetGuest(guestName);
            IUser guest = new User();
            return guest.RegisterToForum(guestName, password, forum, email, dob);

        }

        public ISubForum CreateSubForum(string creatorName, string forumName, string subforumName, Dictionary<string, DateTime> moderators)
        {
            IUser creator = GetForum(forumName).getUser(creatorName);
            if (creator == null)
                return null;
            return creator.createSubForum(subforumName, moderators);

        }

        public int AddThread(string forumName, string subForumName, string publisherName, string title, string content)
        {
            IForum forum = GetForum(forumName);
            ISubForum subForum = forum.getSubForum(subForumName);
            IUser publisher = forum.getUser(publisherName);
            if (publisher == null)
                return -1;
            Thread t = publisher.createThread(subForum, title, content);
            if(t!=null)
                return t.id;
            return -1;

        }

        public Post AddReply(string forumName, string subForumName, int threadID, string publisherName, int postID, string title, string content)
        {
            Post post = GetForum(forumName).getSubForum(subForumName).getThread(threadID).GetPostById(postID);
            IUser publisher = GetForum(forumName).getUser(publisherName);
            return publisher.postReply(post, post.Thread, title, content);

        }

        public IUser MemberLogin(string username, string password, string forumName)
        {
            IForum forum = GetForum(forumName);
            return forum.Login(username, password);
        }

        public bool SendPrivateMessage(string forumName, string senderUsername, string to, string title, string content)
        {
            IUser from = GetForum(forumName).getUser(senderUsername);
            return from.SendPrivateMessage(to, title, content)!=null;


        }

        public bool ChangeExpirationDate(string forumName, string subForumName, string adminName, string moderator, DateTime newDate)
        {
            IForum forum=GetForum(forumName);
            IUser admin = forum.getUser(adminName);
            ISubForum subforum = forum.getSubForum(subForumName);
            return admin.editExpirationTimeOfModerator(moderator, newDate, subforum);
        }

        private bool DeletePostHelper(IUser deleter, Post post)
        {
            return deleter.deletePost(post);
        }

        public bool DeleteForumProperties(string deleter, string forumName , List<Policies> properties)
        {
            IForum forum = GetForum(forumName);
            IUser user = forum.getUser(deleter);
            if (user == null)
                return false;
            return user.DeleteForumProperties(forum, properties);

        }

        public IForum GetForum(string forumName)
        {

            return SuperAdmin.GetInstance().forumSystem.getForum(forumName);
        }

        public bool AddModerator(string forumName, string subForumName, string adminUsername, string username, DateTime expiratoinDate)
        {
            IForum forum = GetForum(forumName);
            IUser admin = forum.getUser(adminUsername);
            ISubForum subForum = forum.getSubForum(subForumName);
            return admin.appointModerator(username, expiratoinDate, subForum);
        }

      /*  public void removeForum(string forumName)
        {
            SuperAdmin.GetInstance().removeForum(forumName);
        }
        */

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

        public DateTime GetModeratorExpDate(string forumName, string subForumName, string username)
        {
            ISubForum subForum = GetForum(forumName).getSubForum(subForumName);
            return subForum.getModeratorByUserName(username).expirationDate;
        }

        public int CountNestedReplies(string forumName, string subForumName, int threadID, int postID)
        {
            ISubForum subforum = GetForum(forumName).getSubForum(subForumName);
            return ((SubForum)subforum).GetThreadById(threadID).GetPostById(postID).GetNumOfNestedReplies();
        }

        public bool IsMsgSent(string forumName, string username, string msgTitle, string msgContent)
        {
            return GetForum(forumName).getUser(username).IsMessageSent(msgTitle, msgContent);
        }

        public bool IsMsgReceived(string forumName, string username, string msgTitle, string msgContent)
        {
            return GetForum(forumName).getUser(username).IsMessageReceived(msgTitle, msgContent);
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

        public bool DeletePost(string forumName, string subForumName,string deleter, int threadID, int postID)
        {
            IForum forum = GetForum(forumName);
            Thread thread = forum.getSubForum(subForumName).GetThreadById(threadID);
            IUser user = forum.getUser(deleter);
            if (thread != null && user != null)
            {
                Post post = thread.GetPostById(postID);
                if (post == null)
                    return false;

                return DeletePostHelper(user, post);
            }
            else
                return false;
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
        public bool IsExistThread(string forumName, string subForumName, int threadID)
        {
            ISubForum subForum = GetForum(forumName).getSubForum(subForumName);
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

        public List<PrivateMessageNotification> GetPrivateMessageNotifications(string forumName, string username)
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
            user2.acceptFriend(user1);//TODO: probably need to remove this!
        }
        
        // <moderatorUserName,appointerUserName,appointmentDate,subForumName,moderatorPosts>
        public List<Tuple<string, string, DateTime, string, List<Post>>> ReportModeratorsDetails(string forumName, string adminUserName1)
        {
            IForum forum = GetForum(forumName);
            IUser admin = forum.getUser(adminUserName1);
            return admin.ReportModerators();
        }

        public void MemberLogout(string forumName, string username)
        {
            IForum forum = GetForum(forumName);
            IUser user = forum.getUser(username);
            user.Logout();
        }

        public List<Post> GetPosts(string forumName,string subforumName,int threadId)
        {
            Thread thread = sys.getForum(forumName).getSubForum(subforumName).GetThreadById(threadId);
            List<Post> res = new List<Post>();
            res.Insert(0, thread.GetOpeningPost());
            return res;
        }

        public bool CheckIfPolicyExists(string forumName, Policies expectedPolicy)
        {
            IForum forum = GetForum(forumName);
            Policy policy = forum.GetPolicy();
            bool res = policy.CheckIfPolicyExists(expectedPolicy);
            return res;
        }

        public List<PostNotification> GetPostNotifications(string forumName, string username)
        {
            IForum forum = GetForum(forumName);
            IUser user = forum.getUser(username);
            return user.GetPostNotifications();
        }

        public List<string> GetWaitingFriendsList(string forumName, string username)
        {
            IForum forum = GetForum(forumName);
            IUser user = forum.getUser(username);
            List<IUser> waitingFriends =  user.GetWaitingFriendsList();
            List<string> res = new List<string>();
            foreach(IUser u in waitingFriends)
            {
                res.Add(u.getUsername());
            }
            return res;
        }

        public void EditPost(string forumName, string subForumName, int threadId, string editor, int postId, string newTitle, string newContent)
        {
            if (newTitle == "" && newContent == "")
                return;//illegal post

            IForum forum = GetForum(forumName);
            ISubForum subforum = forum.getSubForum(subForumName);
            Thread thread = subforum.GetThreadById(threadId);
            thread.EditPost(postId, newTitle, newContent);
            DAL_Posts dp = new DAL_Posts();
            dp.EditPost(postId, newTitle, newContent);
        }
        public bool RemoveModerator(string forumName, string subForumName, string remover, string moderatorName)
        {
            IForum forum = GetForum(forumName);
            if (forum == null) return false;
            ISubForum subforum = forum.getSubForum(subForumName);
            if (subforum == null) return false;
            Moderator moderator = subforum.getModeratorByUserName(moderatorName);
            //IUser user = forum.getUser(remover);
            //IUser moderator = forum.getUser(moderatorName);
            if (moderator == null)
                return false;
            //  if (!moderator.CanBeDeletedBy(remover))
            //      return false;
            return subforum.removeModerator(remover, moderatorName);
        }

        public int ReportNumOfPostsByMember(string adminUsername, string forumName, string username)
        {
            IForum forum = GetForum(forumName);
            IUser admin = forum.getUser(adminUsername);
            try
            {
                return admin.ReportNumOfPostsByMember(username);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public List<string> GetModeratorsList(string forumName, string subForumName, string adminUserName)
        {
            IForum forum = GetForum(forumName);
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

        public List<Post> ReportPostsByMember(string forumName, string adminUserName, string username)
        {
            try
            {
                IForum forum = GetForum(forumName);
                IUser admin = forum.getUser(adminUserName);

                List<Post> posts = admin.ReportPostsByMember(username);
                return posts;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool IsExistUser(string forumName, string username)
        {
            return GetForum(forumName).getUser(username) != null;
        }

        public bool IsInitialized()
        {
            return SuperAdmin.IsInitialized();
        }
        public SuperAdmin GetSuperAdmin()
        {
            return SuperAdmin.GetInstance();
        }
        public List<string> GetForumMembers(string forumName)
        {
            IForum forum = GetForum(forumName);
            Dictionary<string,string>details= forum.GetAllUsers();
            return details.Keys.ToList<string>();
        }
        public List<string> GetThreadsList(string forumName, string subForumName)
        {
            IForum forum = GetForum(forumName);
            ISubForum subforum = forum.getSubForum(subForumName);
            List<Thread> threads = subforum.GetThreads();
            List<string> res = new List<string>();
            foreach (Thread thrd in threads)
            {
                res.Add(thrd.GetTiltle());
            }
            return res;
        }
        public Dictionary<int, string> GetThreads(string forumName, string subForumName)
        {
            IForum forum = GetForum(forumName);
            ISubForum subforum = forum.getSubForum(subForumName);
            List<Thread> threads = subforum.GetThreads();
            Dictionary<int, string> res = new Dictionary<int, string>();
            try
            {
                foreach (Thread thrd in threads)
                {
                    res.Add(thrd.id, thrd.GetTiltle());
                }
                return res;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<string> GetSubForumsList(string forumName)
        {
            IForum forum = GetForum(forumName);
            List<ISubForum> subforums = forum.GetSubForums();
            List<string> res = new List<string>();
            foreach (ISubForum subf in subforums)
            {
                res.Add(subf.getName());
            }
            return res;
        }
        public List<string> GetForumsList()
        {
            List<string> forums = sys.GetForumsNamesList();
            return forums;
        }

        public string GetUserType(string forumName, string username)
        {
            IForum forum = GetForum(forumName);
            IUser user = forum.getUser(username);
            return user.GetTypeString();
            
        }

        public bool IgnoreFriend(string forumName, string userName, string userToIgnore)
        {
            IForum forum = GetForum(forumName); 
            IUser user = forum.getUser(userName);
            IUser user2 = forum.getUser(userToIgnore);
            return user.IgnoreFriend(user2);
        }

        public void SendFriendRequest(string forumName, string sender, string reciever)
        {
            IForum forum = GetForum(forumName);
            IUser user = forum.getUser(sender);
            IUser user2 = forum.getUser(reciever);
            user.addFriend(user2);
        }

        public void AcceptFriendRequest(string forumName, string accepter, string toAccept)
        {
            IForum forum = GetForum(forumName);
            IUser user = forum.getUser(accepter);
            IUser user2 = forum.getUser(toAccept);
            user.addFriend(user2);
        }

        public List<string> GetUsersNotFriends(string forumName, string username)
        {
            IForum forum = GetForum(forumName);
            IUser user = forum.getUser(username);
            List<IUser> tempUsers = forum.getUsersInForum();
            List<string> users = new List<string>();
            foreach (IUser usr in tempUsers)
            {
                users.Add(usr.getUsername());
            }
            List<IUser> tempFriends = user.GetFriendsList();
            List<string> friends = new List<string>();
            foreach (IUser usr in tempFriends)
            {
                friends.Add(usr.getUsername());
            }
            foreach (string usr in friends)
            {
                if (users.Contains(usr))
                    users.Remove(usr);
            }
            users.Remove(username);
            return users;
        }

        public bool AddSecurityQuestion(string forumName, string username, SecurityQuestionsEnum question, string answer)
        {
            IForum forum = GetForum(forumName);
            IUser user = forum.getUser(username);
            return user.AddSecurityQuestion(question, answer);
        }

        public bool RemoveSecurityQuestion(string forumName, string username, SecurityQuestionsEnum question)
        {
            IForum forum = GetForum(forumName);
            IUser user = forum.getUser(username);
            return user.RemoveSecurityQuestion(question);
        }

        public bool CheckSecurityQuestion(string forumName, string username, SecurityQuestionsEnum question, string answer)
        {
            IForum forum = GetForum(forumName);
            IUser user = forum.getUser(username);
            return user.CheckSecurityQuestion(question,answer);
        }

        public bool SetUserPassword(string forumName, string username, string newPassword)
        {
            IForum forum = GetForum(forumName);
            IUser user = forum.getUser(username);
            return user.SetPassword(newPassword);
        }

        public int getNumOfPostsInSubForum(string forumName, string subForumName)
        {
            int total = 0;
            IForum forum = GetForum(forumName);
            foreach (SubForum sub in forum.GetSubForums())
            {
                foreach (Thread thread in sub.GetThreads())
                {
                    total += thread.GetNumOfNestedReplies();
                }
            }

            return total;
        }
    }
}
