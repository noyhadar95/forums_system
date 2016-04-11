using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.ForumManagement.DomainLayer;
using ForumsSystem.Server.UserManagement.DomainLayer;

namespace ForumsSystem.Server.ServiceLayer
{
    public class RegisterToForum
    {
        public static bool Register(IForum forum,string userName, string password, string email, int age)
        {
            // ----CHECK POLICIES----
            PolicyParametersObject param = new PolicyParametersObject(Policies.MinimumAge);
            param.SetAgeOfUser(age);
            if (!forum.GetPolicy().CheckPolicy(param))
                return false;
            param.SetPolicy(Policies.Password);
            param.SetPassword(password);
            if (!forum.GetPolicy().CheckPolicy(param))
                return false;
            param.SetPolicy(Policies.UsersLoad);
            param.SetNumOfUsers(forum.GetNumOfUsers());
            if (!forum.GetPolicy().CheckPolicy(param))
                return false;

            DateTime today = DateTime.Today;
            forum.RegisterToForum(userName, password, email, today.AddYears(age));
            return true;
        }
    }
}
