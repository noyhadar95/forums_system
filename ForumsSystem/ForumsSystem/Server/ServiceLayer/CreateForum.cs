using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.ForumManagement.DomainLayer;
using ForumsSystem.Server.UserManagement.DomainLayer;

namespace ForumsSystem.Server.ServiceLayer
{
    class CreateForum
    {
        public static IForum Create(IUser creator, Policy properties, List<IUser> adminUsername)
        {

            PolicyParametersObject param = new PolicyParametersObject(Policies.AdminAppointment);
            foreach (IUser user in adminUsername.ToList<IUser>())
            {
                param.User = user;
                if (!properties.CheckPolicy(param))//check if user can be an admin (Policies) 
                    return null;
            }


            return null;
        }
    }
}
