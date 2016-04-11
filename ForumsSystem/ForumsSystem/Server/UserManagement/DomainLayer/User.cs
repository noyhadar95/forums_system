﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.ForumManagement.DomainLayer;

namespace ForumsSystem.Server.UserManagement.DomainLayer
{

    public class User : IUser
    {
        private string userName;
        private string password;
        private string email;
        private int age;
        private DateTime dateJoined;
        private IForum forum;
        private int numOfMessages;
        private int numOfComplaints;
        private Type type;
        private List<PrivateMessage> sentMessages;
        private List<PrivateMessage> receivedMessages;
        private List<PrivateMessage> notifications;
        private List<IUser> friends;
        private List<IUser> waitingFriendsList;
        private List<Post> postsNotifications;
        private bool isLoggedIn;
        private bool emailAccepted;

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

            this.notifications = new List<PrivateMessage>();

            this.emailAccepted = false;

        }

        public User(string userName,string password,string email,IForum forum)
        {
            this.userName = userName;
            this.password = password;
            this.forum = forum;
            this.email = email;
            this.dateJoined = DateTime.Today;
            this.numOfMessages = 0;
            this.numOfComplaints = 0;
            this.sentMessages = new List<PrivateMessage>();
            this.receivedMessages = new List<PrivateMessage>();
            this.type = new Member();                            
            this.friends = new List<IUser>();
            this.waitingFriendsList = new List<IUser>();
            this.forum.RegisterToForum(this);
            this.isLoggedIn = false;

            this.notifications = new List<PrivateMessage>();

            this.emailAccepted = false;

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
        }

        
        public bool RegisterToForum(string userName,string password,IForum forum,string email)
        {
            if (this.forum == null)
            {
                this.userName = userName;
                this.password = password;
                this.forum = forum;
                this.email = email;
                this.dateJoined = DateTime.Today;
                type = new Member();
                return forum.RegisterToForum(this);
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
            return type.postReply(this,parent,thread,title,content);
        }

        public Thread createThread(ISubForum subForum, string title, string content)
        {
            return type.createThread(this,subForum,title,content);
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

        public ISubForum createSubForum(string subForumName,Dictionary<string, DateTime> users)
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
                this.isLoggedIn = true;
            }

        }

        public void LogOff()
        {
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

        public void AddNotification(PrivateMessage newMessage)
        {
            notifications.Add(newMessage);
        }

        public List<PrivateMessage> GetNotifications()
        {
            List<PrivateMessage> notifications = this.notifications;
            this.notifications = new List<PrivateMessage>();
            return notifications;
        }

        public void AddPostNotification(Post post)
        {
            postsNotifications.Add(post);
        }

        public List<Post> GetPostNotifications()
        {
            List<Post> notifications = this.postsNotifications;
            this.postsNotifications = new List<Post>();
            return notifications;
        }

        public List<IUser> GetFriendsList()
        {
            return friends;
        }

        public void AcceptEmail()
        {
            this.emailAccepted = true;

        }
    }
}
