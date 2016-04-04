using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.UserManagement.DomainLayer;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    class SubForum : ISubForum
    {
        public string name { get ; private set; }
        public SubForum(string name)
        {
            this.name = name;
        }
        public bool addModerator(IUser user, DateTime expirationDate)
        {
            throw new NotImplementedException();
        }

        public bool changeModeratorExpirationDate(IUser user, DateTime newExpirationDate)
        {
            throw new NotImplementedException();
        }

        public void createThread()
        {
            throw new NotImplementedException();
        }

        public string getName()
        {
            throw new NotImplementedException();
        }

        public void removeThread()
        {
            throw new NotImplementedException();
        }
    }
}
