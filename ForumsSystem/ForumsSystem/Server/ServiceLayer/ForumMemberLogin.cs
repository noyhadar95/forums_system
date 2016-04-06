using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.ForumManagement.DomainLayer;
using ForumsSystem.Server.UserManagement.DomainLayer;

namespace ForumsSystem.Server.ServiceLayer
{
    class ForumMemberLogin
    {
        public static IUser MemberLogin(string username,string password,IForum forum)
        {
            return forum.Login(username, password);
        }
    }
}
