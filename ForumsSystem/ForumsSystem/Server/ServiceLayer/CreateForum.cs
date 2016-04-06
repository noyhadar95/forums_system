using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.ForumManagement.DomainLayer;
using ForumsSystem.Server.UserManagement.DomainLayer;

namespace ForumsSystem.Server.ServiceLayer
{
    public class CreateForum
    {
        public static IForum Create(IUser creator,string name, Policy properties, List<IUser> adminUsername)
        {
            if (adminUsername.Count == 0)//must be an admin
                return null;

            PolicyParametersObject param = new PolicyParametersObject(Policies.AdminAppointment);
            foreach (IUser user in adminUsername.ToList<IUser>())
            {
                param.User = user;
                if (!properties.CheckPolicy(param))//check if user can be an admin (Policies) 
                    return null;
            }

            IForum forum = new Forum(name);
            forum.AddPolicy(properties);
            foreach (IUser user in adminUsername.ToList<IUser>())
            {
                user.ChangeType(new Admin());
                forum.RegisterToForum(user);
            }

            return forum;
        }
    }
}
