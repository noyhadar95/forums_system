using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.ForumManagement.DomainLayer;
using ForumsSystem.Server.UserManagement.DomainLayer;

namespace ForumsSystem.Server.ServiceLayer
{
    public class CreateSubForum
    {
        public static ISubForum Create(IForum forum, IUser creator, string name, List<IUser> moderators)
        {
            //----CHECK POLICIES----
            //  if(creator.GetType()!=) TODO: check if admin
            //return false;

            if (moderators.Count == 0)
                return null;
            PolicyParametersObject param = new PolicyParametersObject(Policies.MaxModerators);
            param.NumOfModerators = moderators.Count;
            if (!forum.GetPolicy().CheckPolicy(param))
                return null;
            param.SetPolicy(Policies.ModeratorAppointment);
            foreach (IUser user in moderators.ToList<IUser>())
            {
                param.User = user;
                if (!forum.GetPolicy().CheckPolicy(param))
                    return null;
            }
            ISubForum newSub = new SubForum(forum, creator, name);

            foreach (IUser user in moderators.ToList<IUser>())
            {
                newSub.addModerator(creator, user, DateTime.Today);//TODO: get dates as args
            }
            return newSub;

        }
    }
}
