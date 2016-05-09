using ForumsSystemClient.Resources.ForumManagement.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources.UserManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    [Serializable]
    public class User 
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
        private Forum forum;
        [DataMember]
        private int numOfMessages;
        [DataMember]
        private int numOfComplaints;
        [DataMember]
        private Type type;
        [DataMember]
        private List<PrivateMessage> sentMessages;
        [DataMember]
        private List<PrivateMessage> receivedMessages;
        [DataMember]
        private List<PrivateMessage> notifications;
        [DataMember]
        private List<User> friends;
        [DataMember]
        private List<User> waitingFriendsList;
        [DataMember]
        private List<Post> postsNotifications;
        [DataMember]
        private bool isLoggedIn;
        [DataMember]
        private bool emailAccepted;


        #region Gettes/Setters

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

        public int Age
        {
            get
            {
                return age;
            }

            set
            {
                age = value;
            }
        }

        public DateTime DateJoined
        {
            get
            {
                return dateJoined;
            }

            set
            {
                dateJoined = value;
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

        public Forum Forum
        {
            get
            {
                return forum;
            }

            set
            {
                forum = value;
            }
        }

        public int NumOfMessages
        {
            get
            {
                return numOfMessages;
            }

            set
            {
                numOfMessages = value;
            }
        }

        public int NumOfComplaints
        {
            get
            {
                return numOfComplaints;
            }

            set
            {
                numOfComplaints = value;
            }
        }

        public Type Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public List<PrivateMessage> SentMessages
        {
            get
            {
                return sentMessages;
            }

            set
            {
                sentMessages = value;
            }
        }

        public List<PrivateMessage> ReceivedMessages
        {
            get
            {
                return receivedMessages;
            }

            set
            {
                receivedMessages = value;
            }
        }

        public List<PrivateMessage> Notifications
        {
            get
            {
                return notifications;
            }

            set
            {
                notifications = value;
            }
        }

        public List<User> Friends
        {
            get
            {
                return friends;
            }

            set
            {
                friends = value;
            }
        }

        public List<User> WaitingFriendsList
        {
            get
            {
                return waitingFriendsList;
            }

            set
            {
                waitingFriendsList = value;
            }
        }

        public List<Post> PostsNotifications
        {
            get
            {
                return postsNotifications;
            }

            set
            {
                postsNotifications = value;
            }
        }

        public bool IsLoggedIn
        {
            get
            {
                return isLoggedIn;
            }

            set
            {
                isLoggedIn = value;
            }
        }

        public bool EmailAccepted
        {
            get
            {
                return emailAccepted;
            }

            set
            {
                emailAccepted = value;
            }
        }

        #endregion


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
