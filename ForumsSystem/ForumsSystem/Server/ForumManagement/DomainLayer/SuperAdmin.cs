using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.ForumManagement.DomainLayer;
using ForumsSystem.Server.UserManagement.DomainLayer;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    public class SuperAdmin
    {

        private static SuperAdmin instance = null;
        public string userName { get; set; }
        public string password { get; set; }
        public bool isLoggedIn { get; set; }
        public System forumSystem { get; private set; }


        private SuperAdmin(string userName, string password, System forumSystem)
        {
            this.userName = userName;
            this.password = password;
            this.forumSystem = forumSystem;
            this.isLoggedIn = false;
        }

        public static SuperAdmin CreateSuperAdmin(string userName, string password, System forumSystem)
        {
            if (instance == null)
            {
                instance= new SuperAdmin(userName, password, forumSystem);
            }
            
                return instance;
        }

        public static SuperAdmin GetInstance()
        {
            return instance;
        }

        public Forum createForum(string forumName, Policy properties, List<User> adminUsername)
        {
            if (adminUsername.Count == 0)// there must be at least 1 admin
                return null;

            PolicyParametersObject param = new PolicyParametersObject(Policies.AdminAppointment);
            foreach (IUser user in adminUsername.ToList<IUser>())
            {
                param.User = user;
                if (!properties.CheckPolicy(param))//check if user can be an admin (Policies) 
                    return null;
            }

            IForum forum = this.forumSystem.createForum(forumName);
            if (forum == null)
                return null;

            forum.AddPolicy(properties);
            foreach (IUser user in adminUsername.ToList<IUser>())
            {
                user.SetForum(forum);
                user.ChangeType(new Admin());
            }

            return (Forum)forum;

        }
        public void removeForum(string forumName)
        {

            this.forumSystem.removeForum(forumName);
        }

        public bool Login(string username, string password)
        {
            if (this.userName.Equals(username) && this.password.Equals(password))
            {
                this.isLoggedIn = true;
                return true;
            }
            return false;
        }
        public void Logout()
        {
            this.isLoggedIn = false;
        }
        public static bool IsInitialized()
        {
            return instance != null;
        }
    }
}
