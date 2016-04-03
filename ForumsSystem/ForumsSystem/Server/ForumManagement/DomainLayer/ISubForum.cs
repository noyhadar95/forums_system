using ForumsSystem.Server.UserManagement.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    public interface ISubForum
    {

        void createThread();
        string getName();
        bool addModerator(IUser user, DateTime expirationDate);
        bool changeModeratorExpirationDate(IUser user, DateTime newExpirationDate);
        
    }
}
