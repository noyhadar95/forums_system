using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebApplication.Resources.UserManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    //  [KnownType(typeof(Forum))]
    //  [KnownType(typeof(PrivateMessage))]
    [Serializable]
    public class User
    {
        [DataMember]
        private string userName;
        [IgnoreDataMember]
        private string password;
        [IgnoreDataMember]
        private string email;
        [IgnoreDataMember]
        private int age;
        [IgnoreDataMember]
        private DateTime dateJoined;
        [IgnoreDataMember]
        private DateTime dateOfBirth;
        //  [IgnoreDataMember]
        //  private Forum forum;
        [IgnoreDataMember]
        private int numOfMessages;
        [IgnoreDataMember]
        private int numOfComplaints;
        [IgnoreDataMember]
        private Type type;
        //  [IgnoreDataMember]
        //  private List<PrivateMessage> sentMessages;
        //  [IgnoreDataMember]
        //  private List<PrivateMessage> receivedMessages;
        //  [IgnoreDataMember]
        //  private List<PrivateMessage> notifications;
        [IgnoreDataMember]
        private List<User> friends;
        [IgnoreDataMember]
        private List<User> waitingFriendsList;
        //  [IgnoreDataMember]
        //  private List<Post> postsNotifications;
        [IgnoreDataMember]
        private bool isLoggedIn;
        [IgnoreDataMember]
        private bool emailAccepted;

        public string Username { get { return userName; } set { userName = value; } }

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

        public User()
        {

        }

        public User(string username, string password)
        {
            this.userName = username;
            this.password = password;
        }
        private DateTime dateTime;

        public User(string username, string password, string email, DateTime dateTime)
        {
            userName = username;
            this.password = password;
            this.email = email;
            dateOfBirth = dateTime;
        }
    }
}