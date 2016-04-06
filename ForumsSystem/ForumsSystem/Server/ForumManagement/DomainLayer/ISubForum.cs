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

        Thread createThread();
        bool removeThread(int threadNumber);
        string getName();
        void addModerator(IUser admin, IUser user, DateTime expirationDate);//admin is the appointer
        bool changeModeratorExpirationDate(IUser user, DateTime newExpirationDate);
        Moderator getModeratorByUserName(string userName);
        IUser getCreator();
        Thread getThread(int num);
    }
}
