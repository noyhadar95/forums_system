using System;
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
        private DateTime dateJoined;
        private IForum forum;
        private int numOfMessages;
        private int numOfComplaints;
        // Type
        private List<PrivateMessage> sentMessages;
        private List<PrivateMessage> receivedMessages;

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
            // type = Guest
        }

        public bool ChangeType()
        {
            throw new NotImplementedException();
        }

        public bool RegisterToForum()
        {
            // type = member
            //return forum.RegisterToForum(this);
            throw new NotImplementedException();
        }

        public void SendPrivateMessage(IUser reciever, string title, string content)
        {
            PrivateMessage privateMessage = new PrivateMessage(title, content, this, reciever);
            reciever.AddReceivedMessage(privateMessage);
            this.AddSentMessage(privateMessage);
        }

        public void AddSentMessage(PrivateMessage privateMessage)
        {
            this.sentMessages.Add(privateMessage);
        }

        public void AddReceivedMessage(PrivateMessage privateMessage)
        {
            this.receivedMessages.Add(privateMessage);
        }

        public DateTime DateJoined { get { return dateJoined; } set { this.dateJoined = value; } }
        public int NumOfMessages { get { return numOfMessages; } set { this.numOfMessages = value; } }
        public int NumOfComplaints { get { return numOfComplaints; } set { this.numOfComplaints = value; } }
    }
}
