using ForumsSystem.Server.ForumManagement.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ForumsSystem.Server.UserManagement.DomainLayer
{
    public interface Type
    {
        

        
        //Admin only ---------------------------------------------------------

        void createSubForum(IUser callingUser, string subForumName, IForum forum, Dictionary<string,DateTime> moderators); 

        void appointModerator(IUser callingUser, string userName, DateTime expirationTime, ISubForum subForum);

        void removeModerator(IUser callingUser, string userName, ISubForum subForum);

        void suspendModerator(IUser callingUser, string userName, ISubForum subForum);

        void appointAdmin(IUser callingUser, IUser user);

        void getComplaint(IUser callingUser);

        //in addition the admin should be able to get a report on the forum status





        //---------------------------------------------------------------------


        //Member---------------------------------------------------------------

        // login?

        bool postReply(IUser callingUser, Post parent, Thread thread, string title, string content); 

        bool createThread(IUser callingUser, ISubForum subForum, string title, string content); 

        bool editPost(IUser callingUser,string title, string content,Post post); 

        bool deletePost(IUser callingUser, Post post);

        void addFriend(IUser callingUser,IUser friend);

        bool removeFriend(IUser callingUser,IUser friendToRemove);

        bool acceptFriend(IUser callingUser, IUser userToAccept);

        void fileComplaint(IUser callingUser, IUser user);  //make sure that user is admin 

        void fileComplaint(IUser callingUser, Moderator moderator);  

        void deactivateUser(IUser callingUser);

        bool SendPrivateMessage(IUser callingUser, IUser reciever, string title, string content);

        void AddSentMessage(IUser callingUser, PrivateMessage privateMessage);

        void AddReceivedMessage(IUser callingUser, PrivateMessage privateMessage);
       
        //---------------------------------------------------------------------

        //Guest----------------------------------------------------------------

        //TODO: Search a forum for posts by content or by user public abstract List<>



        //---------------------------------------------------------------------
    }
}
