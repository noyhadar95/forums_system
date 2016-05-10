using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.ForumManagement.DomainLayer;
using ForumsSystem.Server.ForumManagement.Data_Access_Layer;
using System.Data;
using System.Runtime.Serialization;


namespace ForumsSystem.Server.UserManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(Forum))]
    [KnownType(typeof(PrivateMessage))]
    [KnownType(typeof(PrivateMessageNotification))]
    [KnownType(typeof(PostNotification))]
    [Serializable]
    public class User : IUser
    {
        [DataMember]
        private string userName;
        [DataMember]
        private string password;
        [DataMember]
        private string email;
        [DataMember]
        private int age;
        [DataMember]
        private DateTime dateJoined;
        [DataMember]
        private DateTime dateOfBirth;
        [DataMember]
        private IForum forum;
        [DataMember]
        private int numOfMessages;
        [DataMember]
        private int numOfComplaints;
        private Type type;
        [DataMember]
        private List<PrivateMessage> sentMessages;
        [DataMember]
        private List<PrivateMessage> receivedMessages;
        [DataMember]
        private List<PrivateMessageNotification> privateMessageNotifications;
        [DataMember]
        private List<IUser> friends;
        [DataMember]
        private List<IUser> waitingFriendsList;
        [DataMember]
        private List<PostNotification> postNotifications;
        [DataMember]
        private bool isLoggedIn;
        [DataMember]
        private bool emailAccepted;
        private DAL_Users dal_users = new DAL_Users();
        private DateTime dateOfPassLastchange;

        public User()
        {
            this.userName = "";
            this.password = "";
            this.forum = null;
            this.email = "";
            this.numOfMessages = 0;
            this.numOfComplaints = 0;
            this.sentMessages = new List<PrivateMessage>();
            this.receivedMessages = new List<PrivateMessage>();
            this.type = new Guest();
            this.friends = new List<IUser>();
            this.waitingFriendsList = new List<IUser>();
            this.isLoggedIn = false;
            this.emailAccepted = false;
            this.dateOfBirth = new DateTime();
            this.privateMessageNotifications = new List<PrivateMessageNotification>();
            this.postNotifications = new List<PostNotification>();
            this.dateOfPassLastchange = new DateTime(); 

        }

        public User(string userName, string password, string email, IForum forum, DateTime dateOfBirth)
        {
            this.userName = userName;
            this.password = password;
            this.dateOfPassLastchange = DateTime.Today;
            this.forum = forum;
            this.email = email;
            this.dateJoined = DateTime.Today;
            this.dateOfBirth = dateOfBirth;
            this.numOfMessages = 0;
            this.numOfComplaints = 0;
            this.sentMessages = new List<PrivateMessage>();
            this.receivedMessages = new List<PrivateMessage>();
            this.type = new Member();
            this.friends = new List<IUser>();
            this.waitingFriendsList = new List<IUser>();
            Policy policy = forum.GetPolicy();
            dal_users.CreateUser(this.forum.getName(), this.userName, this.password, this.email,
                this.dateJoined, this.dateOfBirth, this.numOfComplaints, UserType.UserTypes.Member,this.dateOfPassLastchange);
            if ((policy == null) || (!policy.CheckIfPolicyExists(Policies.Authentication)))
                this.forum.RegisterToForum(this);
            else
            {
                this.forum.AddWaitingUser(this);
                dal_users.changeUserWaitingStatus(this.forum.getName(), this.userName, true);
            }
            this.isLoggedIn = false;

            this.privateMessageNotifications = new List<PrivateMessageNotification>();
            this.emailAccepted = false;
            this.postNotifications = new List<PostNotification>();
            

        }

        public User(string userName, string password, string email, DateTime dateOfBirth)
        {
            this.userName = userName;
            this.password = password;
            this.dateOfPassLastchange = DateTime.Today;
            this.forum = null;
            this.email = email;
            this.dateJoined = DateTime.Today;
            this.dateOfBirth = dateOfBirth;
            this.numOfMessages = 0;
            this.numOfComplaints = 0;
            this.sentMessages = new List<PrivateMessage>();
            this.receivedMessages = new List<PrivateMessage>();
            this.type = new Member();
            this.friends = new List<IUser>();
            this.waitingFriendsList = new List<IUser>();
            this.isLoggedIn = false;

            this.privateMessageNotifications = new List<PrivateMessageNotification>();
            this.emailAccepted = false;
            this.postNotifications = new List<PostNotification>();
        }

        public static Dictionary<string, IUser> populateUsers(Forum forum)//Not waiting
        {
            Dictionary<string, IUser> users = new Dictionary<string, IUser>();
            DAL_Users dsu = new DAL_Users();
            DataTable userTbl = dsu.GetAllUsersFromForum(forum.getName());
            foreach (DataRow userRow in userTbl.Rows)
            {
                if ((bool)userRow["Waiting"] == false)
                {
                    User user = new User();
                    user.userName = userRow["UserName"].ToString();
                    user.password = userRow["Password"].ToString();
                    user.email = userRow["Email"].ToString();
                    user.dateJoined = (DateTime)userRow["DateJoined"];
                    user.dateOfBirth = (DateTime)userRow["DateOfBirth"];
                    user.age = (int)(DateTime.Now.Year - user.dateOfBirth.Year);
                    user.forum = forum;
                    user.numOfComplaints = (int)userRow["Complaints"];


                    switch ((int)userRow["Type"]) 
                    {
                        case (int)UserType.UserTypes.Guest:
                            user.type = new Guest();
                            break;
                        case (int)UserType.UserTypes.Member:
                            user.type = new Member();
                            break;
                        case (int)UserType.UserTypes.Admin:
                            user.type = new Admin();
                            break;
                        default:
                            user.type = new Type();
                            break;
                    }

                    user.dateOfPassLastchange = (DateTime)userRow["DateLastPasswordChanged"];
                    user.emailAccepted = true;
                    users[user.userName] = user;
                }
            }
            return users;
        }


        

        public static Dictionary<string, IUser> populateWaitingUsers(Forum forum)//Waiting
        {
            Dictionary<string, IUser> users = new Dictionary<string, IUser>();
            DAL_Users dsu = new DAL_Users();
            DataTable userTbl = dsu.GetAllUsersFromForum(forum.getName());
            foreach (DataRow userRow in userTbl.Rows)
            {
                if ((bool)userRow["Waiting"] == true)
                {
                    User user = new User();
                    user.userName = userRow["UserName"].ToString();
                    user.password = userRow["Password"].ToString();
                    user.email = userRow["Email"].ToString();
                    user.dateJoined = (DateTime)userRow["DateJoined"];
                    user.dateOfBirth = (DateTime)userRow["DateOfBirth"];
                    user.age = (int)(DateTime.Now.Year - user.dateOfBirth.Year);
                    user.forum = forum;
                    user.numOfComplaints = (int)userRow["Complaints"];


                    switch ((int)userRow["Type"])
                    {
                        case (int)UserType.UserTypes.Guest:
                            user.type = new Guest();
                            break;
                        case (int)UserType.UserTypes.Member:
                            user.type = new Member();
                            break;
                        case (int)UserType.UserTypes.Admin:
                            user.type = new Admin();
                            break;
                        default:
                            user.type = new Type();
                            break;
                    }

                    user.dateOfPassLastchange = (DateTime)userRow["DateLastPasswordChanged"];
                    user.emailAccepted = false;
                    users[user.userName] = user;
                }
            }
            return users;
        }

        public static void populateFriends(Dictionary<string, IUser> users, Dictionary<string, IUser> waiting_users, string forumName)
        {
            DAL_Friends df = new DAL_Friends();
            Dictionary<string, IUser> allUsers = users.Union(waiting_users).ToDictionary(k => k.Key, v => v.Value);
            DataTable friendsTbl = df.GetAllFriendsInForum(forumName);

            foreach (DataRow friendRow in friendsTbl.Rows)
            {
                User user = (User)allUsers[friendRow["UserName"].ToString()];
                User friendUser = (User)allUsers[friendRow["FriendUserName"].ToString()];
                bool accepted= (bool)friendRow["Accepted"];

                if (accepted)
                {
                    user.addToFriendsList(friendUser);
                    friendUser.addToFriendsList(user);
                }
                else
                {
                    friendUser.addToWaitingFriendsList(user);
                }

            }


        }

        


        public bool SetForum(IForum forum)
        {
            if (this.forum == null)
            {
                this.forum = forum;
                dal_users.CreateUser(forum.getName(), this.userName, this.password, this.email,
               this.dateJoined, this.dateOfBirth, this.numOfComplaints, UserType.UserTypes.Member,this.dateOfPassLastchange);
                Policy policy = forum.GetPolicy();
                if ((policy == null) || (!policy.CheckIfPolicyExists(Policies.Authentication)))
                    this.forum.RegisterToForum(this);
                else
                {
                    this.forum.AddWaitingUser(this);
                    dal_users.changeUserWaitingStatus(this.forum.getName(), this.userName, true);
                }
                return true;
            }
            return false;
        }

        public List<PrivateMessage> getSentMessages()
        {
            return sentMessages;
        }
        public List<PrivateMessage> getReceivedMessages()
        {
            return receivedMessages;
        }

        public bool isInWaitingList(IUser user)
        {
            return waitingFriendsList.Contains(user);
        }
        public bool isInFriendsList(IUser user)
        {
            return friends.Contains(user);
        }
        public void addToWaitingFriendsList(IUser user)
        {
            if (isLoggedIn) {
   //             Server.CommunicationLayer.Server.notifyClient(forum.getName(), userName, user);
            }
            waitingFriendsList.Add(user);
        }
        public void removeFromWaitingFriendsList(IUser user)
        {
            waitingFriendsList.Remove(user);
        }
        public void addToFriendsList(IUser user)
        {
            friends.Add(user);
        }
        public void removeFromFriendList(IUser user)
        {
            friends.Remove(user);
        }
        public void addFriend(IUser friend)
        {
            type.addFriend(this, friend);
        }

        public bool removeFriend(IUser friendToRemove)
        {
            return type.removeFriend(this, friendToRemove);
        }

        public bool acceptFriend(IUser userToAccept)
        {
            return type.acceptFriend(this, userToAccept);
        }

        public void ChangeType(Type type)
        {
            this.type = type;
            if (type is Guest)
                dal_users.editUser(this.forum.getName(), this.userName, this.password, this.email, this.dateJoined,
                this.dateOfBirth, this.numOfComplaints, UserType.UserTypes.Guest,this.dateOfPassLastchange);
            else if (type is Member)
                dal_users.editUser(this.forum.getName(), this.userName, this.password, this.email, this.dateJoined,
                this.dateOfBirth, this.numOfComplaints, UserType.UserTypes.Member,this.dateOfPassLastchange);
            else if (type is Admin)
                dal_users.editUser(this.forum.getName(), this.userName, this.password, this.email, this.dateJoined,
                this.dateOfBirth, this.numOfComplaints, UserType.UserTypes.Admin,this.dateOfPassLastchange);

        }


        public bool RegisterToForum(string userName, string password, IForum forum, string email, DateTime dateOfBirth)
        {
            if (forum == null)
                return false;
            if (this.forum == null)
            {

                PolicyParametersObject param = new PolicyParametersObject(Policies.MinimumAge);
                param.SetAgeOfUser((int)((DateTime.Today- dateOfBirth).TotalDays) / 365);
                if (forum.GetPolicy() != null && !forum.GetPolicy().CheckPolicy(param))
                    return false;
                param.SetPolicy(Policies.Password);
                param.SetPassword(password);
                if (forum.GetPolicy() != null && !forum.GetPolicy().CheckPolicy(param))
                    return false;
                param.SetPolicy(Policies.UsersLoad);
                param.SetNumOfUsers(forum.GetNumOfUsers());
                if (forum.GetPolicy() != null && !forum.GetPolicy().CheckPolicy(param))
                    return false;


                this.userName = userName;
                this.password = password;
                this.dateOfPassLastchange = DateTime.Today;
                this.forum = forum;
                this.email = email;
                this.dateJoined = DateTime.Today;
                this.dateOfBirth = dateOfBirth;
                this.dateOfPassLastchange = DateTime.Today;
                type = new Member();
                dal_users.CreateUser(this.forum.getName(), this.userName, this.password, this.email,
                this.dateJoined, this.dateOfBirth, this.numOfComplaints, UserType.UserTypes.Member,this.dateOfPassLastchange);
                Policy policy = forum.GetPolicy();
                     if ((policy == null) || (!policy.CheckIfPolicyExists(Policies.Authentication)))
                         return forum.RegisterToForum(this);
                     else
                     {
                         forum.AddWaitingUser(this);
                         dal_users.changeUserWaitingStatus(this.forum.getName(), this.userName, true);
                         return true;
                     }
            }
            else
                return false;
        }

    
        public PrivateMessage SendPrivateMessage(string reciever, string title, string content)
        {
            return type.SendPrivateMessage(this, reciever, title, content);
        }

        public void AddSentMessage(PrivateMessage privateMessage)
        {
            type.AddSentMessage(this, privateMessage);
        }

        public void AddReceivedMessage(PrivateMessage privateMessage)
        {
            type.AddReceivedMessage(this, privateMessage);
        }


        public Post postReply(Post parent, Thread thread, string title, string content)
        {
            return type.postReply(this, parent, thread, title, content);
        }

        public Thread createThread(ISubForum subForum, string title, string content)
        {
            return type.createThread(this, subForum, title, content);
        }

        public bool editPost(string title, string content, Post post)
        {
            return type.editPost(this, title, content, post);
        }

        public bool deletePost(Post post)
        {
            return type.deletePost(this, post);
        }


        public DateTime DateJoined { get { return dateJoined; } set { this.dateJoined = value; } }
        public int NumOfMessages { get { return numOfMessages; } set { this.numOfMessages = value; } }
        public int NumOfComplaints { get { return numOfComplaints; } set { this.numOfComplaints = value; } }
        public int Age { get { return age; } set { this.age = value; } }

        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
            }
        }
        

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }

        public DateTime DateOfBirth
        {
            get
            {
                return dateOfBirth;
            }

            set
            {
                dateOfBirth = value;
            }
        }

        public string getUsername()
        {
            return userName;
        }

        public string getPassword()
        {
            return password;
        }

        public string getEmail()
        {
            return email;
        }

        public void AddToreceivedMessages(PrivateMessage privateMessage)
        {
            receivedMessages.Add(privateMessage);
        }

        public void AddTosentMessages(PrivateMessage privateMessage)
        {
            sentMessages.Add(privateMessage);
        }

        public IForum getForum()
        {
            return forum;
        }

        public Type getType()
        {
            return type;
        }

        public string GetTypeString()
        {
            return type.ToString();
        }

        public ISubForum createSubForum(string subForumName, Dictionary<string, DateTime> users)
        {
            IForum forum = this.forum;
            return type.createSubForum(this, subForumName, forum, users);
        }

        public bool appointModerator(string userName, DateTime expirationTime, ISubForum subForum)
        {
            return type.appointModerator(this, userName, expirationTime, subForum);
        }

        public bool removeModerator(string userName, ISubForum subForum)
        {
            return type.removeModerator(this, userName, subForum);
        }

        public bool editExpirationTimeOfModerator(string userName, DateTime expirationTime, ISubForum subForum)
        {
            return type.editExpirationTimeOfModerator(this, userName, expirationTime, subForum);
        }

        public void Login()
        {

            Policy policy = forum.GetPolicy();
            if ((policy == null) || (!policy.CheckIfPolicyExists(Policies.Authentication)) || (policy.CheckIfPolicyExists(Policies.Authentication) && emailAccepted))
            { 
                //                Server.CommunicationLayer.Server.SubscribeClient(this.forum.getName(), this.userName);
                this.isLoggedIn = true;
                foreach(PostNotification p in postNotifications)
                {
              //      Server.CommunicationLayer.Server.notifyClient(forum.getName(), userName, p);
                }
                DAL_PostsNotification dal_postNotification = new DAL_PostsNotification();
                dal_postNotification.RemoveAllNotifications(forum.getName(), userName);
                postNotifications = new List<PostNotification>();
                foreach (PrivateMessageNotification m in privateMessageNotifications)
                {
              //      Server.CommunicationLayer.Server.notifyClient(forum.getName(), userName, m);
                }
                privateMessageNotifications = new List<PrivateMessageNotification>();
                DAL_MessagesNotification dal_messagesNotification = new DAL_MessagesNotification();
                dal_messagesNotification.RemoveAllNotifications(forum.getName(), userName);

                foreach(IUser u in waitingFriendsList)
                {
                 Server.CommunicationLayer.Server.notifyClient(forum.getName(), userName, u.getUsername());
                }
            }

        }

        public void Logout()
        {
//            Server.CommunicationLayer.Server.UnSubscribeClient(this.userName, this.forum.getName());
            this.isLoggedIn = false;

        }

        public bool isLogin()
        {
            return this.isLoggedIn;
        }


        public bool SetForumProperties(IForum forum, Policy properties)
        {
            return type.SetForumProperties(forum, properties);
        }

        public bool ChangeForumProperties(IForum forum, Policy properties)
        {
            return type.ChangeForumProperties(forum, properties);
        }
        public bool DeleteForumProperties(IForum forum, List<Policies> properties)
        {
            return type.DeleteForumProperties(forum, properties);
        }

        public void AddPrivateMessageNotification(PrivateMessage newMessage)
        {
            PrivateMessageNotification notification = new PrivateMessageNotification(
                newMessage.sender.getUsername(), newMessage.title, newMessage.content,newMessage.id);
            if (isLoggedIn)
            {
            //    Server.CommunicationLayer.Server.notifyClient(forum.getName(), userName, notification);
            }
            else
            {
                DAL_MessagesNotification dal_messagesNotification = new DAL_MessagesNotification();
                dal_messagesNotification.AddNotification(newMessage.id);
                privateMessageNotifications.Add(notification);
            }
        }

        public List<PrivateMessageNotification> GetPrivateMessageNotifications()
        {
            List<PrivateMessageNotification> notifications = this.privateMessageNotifications;
            //this.privateMessageNotifications = new List<PrivateMessageNotification>();
            return notifications;
        }

        public void AddPostNotification(Post post,NotificationType type)
        {
            PostNotification notification = new PostNotification(type, post.getPublisher().getForum().getName(),
                post.getPublisher().getUsername(), post.Thread.GetSubforum().getName(),
                post.Title, post.Content, post.GetId());
            if (isLoggedIn)
            {
             //   Server.CommunicationLayer.Server.notifyClient(this.forum.getName(), this.userName, notification);
            }
            else
            {
                DAL_PostsNotification dal_postNotification = new DAL_PostsNotification();
                dal_postNotification.AddNotification(notification.id, (int)type, post.getPublisher().getForum().getName(), userName);
                postNotifications.Add(notification);
            }
        }

        public List<PostNotification> GetPostNotifications()
        {
            List<PostNotification> notifications = this.postNotifications;
            //this.postNotifications = new List<PostNotification>();
            return notifications;
        }

        public List<IUser> GetFriendsList()
        {
            return friends;
        }

        public void AcceptEmail()
        {
            this.emailAccepted = true;
            this.forum.RegisterToForum(this);

        }

        public bool IsMessageSent(string msgTitle, string msgContent)
        {
            foreach (PrivateMessage msg in sentMessages.ToList<PrivateMessage>())
            {
                if (msg.title.Equals(msgTitle) && msg.content.Equals(msgContent))
                    return true;
            }
            return false;
        }

        public bool IsMessageReceived(string msgTitle, string msgContent)
        {
            foreach (PrivateMessage msg in receivedMessages.ToList<PrivateMessage>())
            {
                if (msg.title.Equals(msgTitle) && msg.content.Equals(msgContent))
                    return true;
            }
            return false;
        }

        public List<Post> ReportPostsByMember(string memberUserName)
        {
            return type.ReportPostsByMember(this,memberUserName);
        }

        public int ReportNumOfPostsByMember(string memberUserName)
        {
            return type.ReportNumOfPostsByMember(this,memberUserName);
        }

        public List<string> GetModeratorsList(ISubForum subforum)
        {
            return type.GetModeratorsList(this, subforum);
        }

        public void SetPassword(string password)
        {

            this.password = password;
            this.dateOfPassLastchange = DateTime.Today;
            if (type is Guest)
            {
                dal_users.editUser(this.forum.getName(), this.userName, this.password, this.email, this.DateJoined,
                    this.dateOfBirth, this.numOfComplaints, UserType.UserTypes.Guest,DateTime.Today);
            } else if(type is Member)
            {
                dal_users.editUser(this.forum.getName(), this.userName, this.password, this.email, this.DateJoined,
                   this.dateOfBirth, this.numOfComplaints, UserType.UserTypes.Member,DateTime.Today);
            } else if(type is Admin)
            {
                dal_users.editUser(this.forum.getName(), this.userName, this.password, this.email, this.DateJoined,
                   this.dateOfBirth, this.numOfComplaints, UserType.UserTypes.Member,DateTime.Today);
            }

        }
        public DateTime GetDateOfPassLastChange()
        {

            return this.dateOfPassLastchange;
        }

        public int ReportNumOfPostsInSubForum(ISubForum subForum)
        {
            return type.ReportNumOfPostsInSubForum(this, subForum);
        }

        public List<Tuple<string, string, DateTime, string, List<Post>>> ReportModerators()
        {
            return type.ReportModerators(this);
        }


        public void AddToMessageNotification(PrivateMessageNotification messageNotification)
        {
            this.privateMessageNotifications.Add(messageNotification);
        }

        public void AddToPostNotification(PostNotification postNotification)
        {
            postNotifications.Add(postNotification);
        }

        public bool IgnoreFriend(IUser userToIgnore)
        {
            return type.IgnoreFriend(this,userToIgnore);
        }
    }




}


