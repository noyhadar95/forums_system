using ForumsSystem.Server.UserManagement.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    public class Moderator
    {
        public IUser user { get; private set; }
        public DateTime expirationDate { get; private set; }

        public Moderator(IUser user, DateTime expirationDate)
        {
            this.user = user;
            this.expirationDate = expirationDate;
        }

        public void changeExpirationDate(DateTime expirationDate)
        {
            this.expirationDate = expirationDate;
        }
    }
}
