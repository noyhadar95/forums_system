using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.ForumManagement.DomainLayer;

namespace ForumsSystem.Server.UserManagement.DomainLayer
{
    public class Admin : Type
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

        void appointModerator(IUser callingUser, string userName, DateTime expirationTime, ISubForum subForum)
        {
            // check if userName is member in the forum
            // if not - return false or throw exception
            // also check the expiration date
            // else - get user

            //subForum.addModerator(user, expirationTime);
            // save the admin who appointed this modeator?
        }

        public bool createSubForum(IUser callingUser, string subForumName, IForum forum, Dictionary<string, DateTime> moderators)
        {
            if (subForumName == "" || forum == null)
                return false;
            foreach (KeyValuePair<string, DateTime> moderator in moderators)
            {
                // check if moderator.Key is member in the forum
                // if not - return false or throw exception
                // also check the expiration date
                // else - get user and add to dictionary of <IUser,DateTime>
            }
            ISubForum subforum = new SubForum(forum,callingUser, subForumName);
            //subforum.addModerator(user, expirationDate);
            // save the admin who created the sub forum?
            return true;
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

        void Type.createSubForum(IUser callingUser, string subForumName, IForum forum, Dictionary<string, DateTime> moderators)
        {
            throw new NotImplementedException();
        }

        void Type.appointModerator(IUser callingUser, string userName, DateTime expirationTime, ISubForum subForum)
        {
            throw new NotImplementedException();
        }
    }
}
