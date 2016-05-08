using ForumsSystemClient.Resources.ForumManagement.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources.UserManagement.DomainLayer
{
    [Serializable]
    public class User 
    {
        private string userName;
        private string password;
        private string email;
        private int age;
        private DateTime dateJoined;
        private DateTime dateOfBirth;
        private Forum forum;
        private int numOfMessages;
        private int numOfComplaints;
        private Type type;
        private List<PrivateMessage> sentMessages;
        private List<PrivateMessage> receivedMessages;
        private List<PrivateMessage> notifications;
        private List<User> friends;
        private List<User> waitingFriendsList;
        private List<Post> postsNotifications;
        private bool isLoggedIn;
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
