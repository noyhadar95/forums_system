using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcceptanceTestsBridge
{
    public class UserStub
    {
        private string username;
        private string password;
        private string email;
        private string forumName; // a user belongs to a forum

        public string Username { get { return username; } set { username = value; } }

        public UserStub(string username, string password, string email, string forumName)
        {
            this.username = username;
            this.password = password;
            this.email = email;
            this.forumName = forumName;
        }

    }
}
