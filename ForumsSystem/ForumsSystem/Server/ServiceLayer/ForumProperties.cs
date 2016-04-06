using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.ForumManagement.DomainLayer;
using ForumsSystem.Server.UserManagement.DomainLayer;

namespace ForumsSystem.Server.ServiceLayer
{
    public class ForumProperties
    {
        public static bool SetForumProperties(IForum forum, Policy properties)
        {
            forum.SetPolicy(properties);
            return true;
        }

        public static bool ChangeForumProperties(IForum forum, Policy properties)
        {
            Policy temp = properties;
            while (temp != null)
            {
                forum.RemovePolicy(temp.Type); temp = temp.NextPolicy;//delete old ones
            }
            forum.AddPolicy(properties);//add new ones
            return true;
        }
        public static bool DeleteForumProperties(IForum forum, List<Policies> properties)
        {
            foreach (Policies pol in properties.ToList<Policies>())
            {
                forum.RemovePolicy(pol);
            }
            return true;
        }
    }
}
