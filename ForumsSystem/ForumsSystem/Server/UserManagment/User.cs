using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.ForumManagement.DomainLayer;

namespace ForumsSystem.Server.UserManagment
{
    class User : IUser
    {
        private string userName;
        private string password;
        private string email;
        private IForum forum;
        // Type
        // list of private message sent
        // list of private message recieved 
        public User(string userName,string password,string email,IForum forum)
        {
            this.userName = userName;
            this.password = password;
            this.forum = forum;
            this.email = email;
            //list = null
            //list = null
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

        public bool SendPrivateMessage(IUser reciever, string message)
        {
            throw new NotImplementedException();
        }
    }
}
