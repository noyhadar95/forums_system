using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    class System
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
            Loggers.Logger.GetInstance().AddActivityEntry("The Super Admin UserName has been changed to: " + userName);
        }
        public void changeAdminPassword(string password)
        {
            this.superAdmin.password = password;
            Loggers.Logger.GetInstance().AddActivityEntry("The Super Admin password has been changed");
        }
        public Forum createForum(string forumName)
        {
            if (forums.ContainsKey(forumName))
                return null;
            Forum forum = new Forum(forumName);
            forums.Add(forumName, forum);
            Loggers.Logger.GetInstance().AddActivityEntry("A new forum: " + forumName + " has been created");
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
            {
                forums.Remove(forumName);
                Loggers.Logger.GetInstance().AddActivityEntry("The forum: " + forumName + " has been removed");
            } 
        }

    }
}
