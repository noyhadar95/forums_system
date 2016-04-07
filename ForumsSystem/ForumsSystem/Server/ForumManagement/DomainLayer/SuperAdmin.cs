using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    class SuperAdmin
    {
       

        public string userName { get;  set; }
        public string password { get; set; }
        public System forumSystem { get; private set; }

        public SuperAdmin(string userName, string password, System forumSystem)
        {
            this.userName = userName;
            this.password = password;
            this.forumSystem = forumSystem;
        }

        public Forum createForum(string forumName)
        {
            return this.forumSystem.createForum(forumName);
        }
        public void removeForum(string forumName)
        {
            this.forumSystem.removeForum(forumName);
        }
    }
}
