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
        private IForum forum;
        // Type
        private List<PrivateMessage> sentMessages;
        private List<PrivateMessage> receivedMessages;

        public User(string userName,string password,string email,IForum forum)
        {
            this.userName = userName;
            this.password = password;
            this.forum = forum;
            this.email = email;
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
            return password;
        }
    }
}
