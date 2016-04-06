using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.ForumManagement.DomainLayer;
using ForumsSystem.Server.UserManagement.DomainLayer;

namespace ForumsSystem.Server.ServiceLayer
{
    public class TrackModerators
    {
        public static bool ChangeExpirationDate(DateTime newDate,Moderator moderator)
        {
            if ((DateTime.Today - newDate).TotalMilliseconds > 0)
                return false;//cant change to a date that already passed
            moderator.changeExpirationDate(newDate);
            //TODO: notify moderator
            return true;
        }
    }
}
