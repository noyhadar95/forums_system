using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.ForumManagement.DomainLayer;
using ForumsSystem.Server.UserManagement.DomainLayer;
using ForumsSystem.Server.ForumManagement.Data_Access_Layer;
using System.Data;
using System.Runtime.Serialization;


namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(System))]
    public class SuperAdmin
    {

        private static SuperAdmin instance = null;
        [DataMember]
        public string userName { get; set; }
        [DataMember]
        public string password { get; set; }
        [DataMember]
        public bool isLoggedIn { get; set; }
        [DataMember]
        public System forumSystem { get; private set; }


        private SuperAdmin(string userName, string password, System forumSystem)
        {
            this.userName = userName;
            this.password = password;
            this.forumSystem = forumSystem;
            this.isLoggedIn = false;
        }
        private SuperAdmin()
        {

        }
        public static SuperAdmin CreateSuperAdmin(string userName, string password, System forumSystem)
        {
            if (instance == null)
            {
                instance= new SuperAdmin(userName, password, forumSystem);
                DAL_SuperAdmin ds = new DAL_SuperAdmin();
                ds.createSuperAdmin(userName, password);
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
                if (!forum.CheckRegistrationPolicies(user.getPassword(), user.GetDateOfBirth()))
                {//check if user can register 
                    this.forumSystem.removeForum(forumName);
                    return null;
                }
            }

            
            foreach (IUser user in adminUsername.ToList<IUser>())
            {
                user.RegisterToForum(user.getUsername(),user.getPassword(),forum,user.getEmail(),user.GetDateOfBirth());
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


        public static SuperAdmin populateSuperAdmin()
        {
            SuperAdmin admin = new SuperAdmin();
            DAL_SuperAdmin dsa = new DAL_SuperAdmin();
            DataTable sAdminTBL = dsa.GetAllSuperAdmins();
            foreach (DataRow sAdminRow in sAdminTBL.Rows) //should be one
            {
                admin.userName = sAdminRow["UserName"].ToString();
                admin.password = sAdminRow["Password"].ToString();
                admin.forumSystem = System.populateSystem();
                admin.isLoggedIn = false;

                instance = admin;
            }

            return instance;
        }

        public static bool IsInitialized()
        {
            return instance != null;

        }
    }
}
