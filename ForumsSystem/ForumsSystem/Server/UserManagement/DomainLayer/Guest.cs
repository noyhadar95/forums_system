using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.ForumManagement.DomainLayer;

namespace ForumsSystem.Server.UserManagement.DomainLayer
{
    class Guest : Type
    {
        public bool acceptFriend(IUser callingUser, IUser userToAccept)
        {
            throw new NotImplementedException();
        }

        public void addFriend(IUser callingUser, IUser friend)
        {
            throw new NotImplementedException();
        }

        public void AddReceivedMessage(IUser callingUser, PrivateMessage privateMessage)
        {
            throw new NotImplementedException();
        }

        public void AddSentMessage(IUser callingUser, PrivateMessage privateMessage)
        {
            throw new NotImplementedException();
        }

        public void appointAdmin(IUser callingUser, IUser user)
        {
            throw new NotImplementedException();
        }

        public void appointModerator(IUser callingUser, string userName, DateTime expirationTime, ISubForum subForum)
        {
            throw new NotImplementedException();
        }

        public void createSubForum(IUser callingUser, string subForumName)
        {
            throw new NotImplementedException();
        }

        public void createSubForum(IUser callingUser, string subForumName, IForum forum, Dictionary<string, DateTime> moderators)
        {
            throw new NotImplementedException();
        }

        public bool createThread(IUser callingUser, ISubForum subForum, string title, string content)
        {
            throw new NotImplementedException();
        }

        public void deactivateUser(IUser callingUser)
        {
            throw new NotImplementedException();
        }

        public bool deletePost(IUser callingUser, Post post)
        {
            throw new NotImplementedException();
        }

        public bool editPost(IUser callingUser, string title, string content, Post post)
        {
            throw new NotImplementedException();
        }

        public void fileComplaint(IUser callingUser, Moderator moderator)
        {
            throw new NotImplementedException();
        }

        public void fileComplaint(IUser callingUser, IUser user)
        {
            throw new NotImplementedException();
        }

        public void getComplaint(IUser callingUser)
        {
            throw new NotImplementedException();
        }

        public bool postReply(IUser callingUser, Post parent, Thread thread, string title, string content)
        {
            throw new NotImplementedException();
        }

        public bool removeFriend(IUser callingUser, IUser friendToRemove)
        {
            throw new NotImplementedException();
        }

        public void removeModerator(IUser callingUser, string userName, ISubForum subForum)
        {
            throw new NotImplementedException();
        }

        public bool SendPrivateMessage(IUser callingUser, IUser reciever, string title, string content)
        {
            throw new NotImplementedException();
        }

        public void suspendModerator(IUser callingUser, string userName, ISubForum subForum)
        {
            throw new NotImplementedException();
        }
    }
}
