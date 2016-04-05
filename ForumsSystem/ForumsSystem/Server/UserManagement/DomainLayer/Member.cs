using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.ForumManagement.DomainLayer;

namespace ForumsSystem.Server.UserManagement.DomainLayer
{
    class Member : Type
    {
        public bool acceptFriend(IUser callingUser, IUser userToAccept)
        {
            if (!callingUser.isInWaitingList(userToAccept))
                return false;
            callingUser.removeFromWaitingFriendsList(userToAccept);
            callingUser.addToFriendsList(userToAccept);
            userToAccept.addToFriendsList(callingUser);
            return true;
        }

        public void addFriend(IUser callingUser, IUser friend)
        {
            friend.addToWaitingFriendsList(callingUser);
        }

        public void appointAdmin(IUser callingUser, IUser user)
        {
            throw new NotImplementedException();
        }

        public void appointModerator(IUser callingUser, IUser user, ISubForum subForum)
        {
            throw new NotImplementedException();
        }

        public void createSubForum(IUser callingUser, string subForumName)
        {
            throw new NotImplementedException();
        }

        public bool createThread(IUser callingUser,ISubForum subForum, string title, string content)
        {
            if (subForum == null)
                return false;
            if (title == null & content == null)
                return false;
            Thread thread = new Thread(subForum);
            Post openingPost = new Post(callingUser, thread, title, content);
            return thread.AddOpeningPost(openingPost);
        }

        public void deactivateUser(IUser callingUser)
        {
            throw new NotImplementedException();
        }

        public bool deletePost(IUser callingUser, Post post)
        {
            // get post publisher and check if he is equal to callingUser
            return post.DeletePost();
        }

        public bool editPost(IUser callingUser, string title, string content, Post post)
        {
            // get post publisher and check if he is equal to callingUser
            if (title == null & content == null)
                return false;
            post.Content = content;
            post.Title = title;
            return true;
        }

        public void fileComplaint(IUser callingUser, IUser user)
        {
            throw new NotImplementedException();
        }

        public bool postReply(IUser callingUser,Post parent, Thread thread, string title, string content)
        {
            if (title == null & content == null)
                return false;
            if (thread == null)
                return false;
            Post reply = new Post(callingUser, thread, title, content);
            return parent.AddReply(reply);
        }

        public bool removeFriend(IUser callingUser, IUser friendToRemove)
        {
            if (!callingUser.isInFriendsList(friendToRemove))
                return false;
            callingUser.removeFromFriendList(friendToRemove);
            friendToRemove.removeFromFriendList(callingUser);
            return true;
        }

        public void removeModerator(IUser callingUser, string userName, ISubForum subForum)
        {
            throw new NotImplementedException();
        }

        public void suspendModerator(IUser callingUser, string userName, ISubForum subForum)
        {
            throw new NotImplementedException();
        }
    }
}
