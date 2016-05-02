using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.Data_Access_Layer
{
    class UserType
    {
        public enum UserTypes
        {
            Guest = 1,
            Member = 2,
            Admin = 3,
        }
    }
}
