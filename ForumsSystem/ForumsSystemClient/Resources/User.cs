using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources
{
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



        public string Username { get { return userName; } set { userName = value; } }

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
            dateOfBirth= dateTime;
        }


    }
}
