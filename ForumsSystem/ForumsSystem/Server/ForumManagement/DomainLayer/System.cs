using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    public class System
    {
        private SuperAdmin superAdmin;
        private Dictionary<string, IForum> forums; //name, forum

        public System()
        {
            this.superAdmin = new SuperAdmin("root", "toor", this);
            forums = new Dictionary<string, IForum>();
        }

        public void changeAdminUserName(string userName)
        {
            this.superAdmin.userName = userName;
        }
        public void changeAdminPassword(string password)
        {
            this.superAdmin.password = password;
        }
        public Forum createForum(string forumName)
        {
            if (forums.ContainsKey(forumName))
                return null;
            Forum forum = new Forum(forumName);
            forums.Add(forumName, forum);
            return forum;
        }

        public IForum getForum(string forumName)
        {
            if (!forums.ContainsKey(forumName))
                return null;
            return forums[forumName];
        }

        public void removeForum(string forumName)
        {
            if (forums.ContainsKey(forumName))
                forums.Remove(forumName);
        }

    }
}
