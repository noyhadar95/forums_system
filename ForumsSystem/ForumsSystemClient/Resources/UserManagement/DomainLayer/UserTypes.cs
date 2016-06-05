using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources.UserManagement.DomainLayer
{
    class UserTypes
    {
        private const string guest = "guest";
        private const string member = "member";
        private const string admin = "admin";

        public static string Guest
        {
            get
            {
                return guest;
            }
        }

        public static string Member
        {
            get
            {
                return member;
            }
        }

        public static string Admin
        {
            get
            {
                return admin;
            }
        }
    }
}
