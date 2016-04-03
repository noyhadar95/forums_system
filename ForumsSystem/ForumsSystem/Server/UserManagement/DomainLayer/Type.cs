using ForumsSystem.Server.ForumManagement.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ForumsSystem.Server.UserManagement.DomainLayer
{
    abstract class Type
    {
        

        
        //Admin only ---------------------------------------------------------

        public abstract void createSubForum(IUser callingUser, string subForumName); 

        public abstract void appointModerator(IUser callingUser, IUser user, ISubForum subForum);

        public abstract void removeModerator(IUser callingUser, string userName, ISubForum subForum);

        public abstract void suspendModerator(IUser callingUser, string userName, ISubForum subForum);

        public abstract void appointAdmin(IUser callingUser, IUser user);

        //in addition the admin should be able to get a report on the forum status





        //---------------------------------------------------------------------


        //Member---------------------------------------------------------------

        public abstract void postReply(IUser callingUser ); //TODO: Change once we get a post Interface

        public abstract void createThread(IUser callingUser); //TODO: Change once we get create

        public abstract void editPost(IUser callingUser);  //TODO

        public abstract void deletePost(IUser callingUser); //TODO

        public abstract void addFriend(IUser callingUser);

        public abstract void removeFriend(IUser callingUser);

        public abstract void acceptFriend(IUser callingUser);

        public abstract void fileComplaint(IUser callingUser, IUser user);  //make sure admin or moderator

        public abstract void deactivateUser(IUser callingUser);

        
        

        //---------------------------------------------------------------------

        //Guest----------------------------------------------------------------

       //TODO: Search a forum for posts by content or by user public abstract List<>



        //---------------------------------------------------------------------
    }
}
