using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.ForumManagement.DomainLayer;

namespace ForumsSystem.Server.UserManagement.DomainLayer
{
    [Serializable]
    public class Admin : Type
    {
        public override string ToString()
        {
            return "admin";
        }
        public override void appointAdmin(IUser callingUser, IUser user)
        {
            throw new NotImplementedException();
        }

        public override void deactivateUser(IUser callingUser)
        {
            throw new NotImplementedException();
        }


        public override void getComplaint(IUser callingUser)
        {
            throw new NotImplementedException();
        }

        public override void suspendModerator(IUser callingUser, string userName, ISubForum subForum)
        {
            throw new NotImplementedException();
        }

    }
}
