using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.ForumManagement.DomainLayer;

namespace ForumsSystem.Server.UserManagement.DomainLayer
{
    public class Member : Type
    {

        public override void appointAdmin(IUser callingUser, IUser user)
        {
            throw new Exception("permission denied");
        }

        public override bool appointModerator(IUser callingUser, string userName, DateTime expirationTime, ISubForum subForum)
        {
            throw new Exception("permission denied");
        }

        public override ISubForum createSubForum(IUser callingUser, string subForumName, IForum forum, Dictionary<string, DateTime> moderators)
        {
            throw new Exception("permission denied");
        }

        public override void deactivateUser(IUser callingUser)
        {
            throw new NotImplementedException();
        }

        public override void fileComplaint(IUser callingUser, Moderator moderator)
        {
            throw new NotImplementedException();
        }

        public override void fileComplaint(IUser callingUser, IUser user)
        {
            throw new NotImplementedException();
        }

        public override void getComplaint(IUser callingUser)
        {
            throw new Exception("permission denied");
        }


        public override bool removeModerator(IUser callingUser, string userName, ISubForum subForum)
        {
            throw new Exception("permission denied");
        }

        public override void suspendModerator(IUser callingUser, string userName, ISubForum subForum)
        {
            throw new Exception("permission denied");
        }

        public override bool editExpirationTimeOfModerator(IUser callingUser, string userName, DateTime expirationTime, ISubForum subForum)
        {
            throw new Exception("permission denied");
        }

        public override bool SetForumProperties(IForum forum, Policy properties)
        {
            throw new Exception("permission denied");
        }

        public override bool ChangeForumProperties(IForum forum, Policy properties)
        {
            throw new Exception("permission denied");
        }
        public override bool DeleteForumProperties(IForum forum, List<Policies> properties)
        {
            throw new Exception("permission denied");
        }

        public override bool CancelModeratorAppointment(IUser callingUser, ISubForum subforum, string userName)
        {
            throw new Exception("permission denied");
        }

        public override int ReportNumOfPostsInSubForum(IUser callingUser, ISubForum subforum)
        {
            throw new Exception("permission denied");
        }

        public override List<Post> ReportPostsByMember(IUser callingUser, string memberUserName)
        {
            throw new Exception("permission denied");
        }
    }
}
