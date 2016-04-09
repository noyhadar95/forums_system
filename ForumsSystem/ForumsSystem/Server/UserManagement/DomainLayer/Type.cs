using ForumsSystem.Server.ForumManagement.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ForumsSystem.Server.UserManagement.DomainLayer
{
    public class Type
    {
        

        
        //Admin only ---------------------------------------------------------

        public virtual bool createSubForum(IUser callingUser, string subForumName, IForum forum, Dictionary<string,DateTime> users)
        {
            Dictionary<IUser, DateTime> moderators = new Dictionary<IUser, DateTime>();
            if (subForumName == "" || forum == null)
                return false;
            foreach (KeyValuePair<string, DateTime> moderator in users)
            {
                if (forum.isUserMember(moderator.Key))
                    return false; // moderator should be member in the forum
                if (moderator.Value != null && moderator.Value.CompareTo(DateTime.Now) < 0)
                    return false; // expiration date should be after now
                moderators.Add(forum.getUser(moderator.Key), moderator.Value);
            }
            ISubForum subforum = new SubForum(forum, callingUser, subForumName);
            foreach (KeyValuePair<IUser, DateTime> moderator in moderators)
            {
                subforum.addModerator(callingUser, moderator.Key, moderator.Value);
            }
            return true;
        }

        public virtual bool appointModerator(IUser callingUser, string userName, DateTime expirationTime, ISubForum subForum)
        {
            IForum forum = subForum.getForum();
            if (forum.isUserMember(userName))
                return false; // moderator should be member in the forum
            if (expirationTime != null && expirationTime.CompareTo(DateTime.Now) < 0)
                return false; // expiration date should be after now
            IUser moderator = forum.getUser(userName);
            subForum.addModerator(callingUser, moderator, expirationTime);
            return true;
        }

        public virtual bool removeModerator(IUser callingUser, string userName, ISubForum subForum)
        {
            return subForum.removeModerator(userName);
        }

        public virtual void suspendModerator(IUser callingUser, string userName, ISubForum subForum)
        {
            throw new NotImplementedException();
        }

        public virtual void appointAdmin(IUser callingUser, IUser user)
        {
            throw new NotImplementedException();
        }

        public virtual void getComplaint(IUser callingUser)
        {
            throw new NotImplementedException();
        }

        //in addition the admin should be able to get a report on the forum status





        //---------------------------------------------------------------------


        //Member---------------------------------------------------------------

        // login?

        public virtual bool postReply(IUser callingUser, Post parent, Thread thread, string title, string content)
        {
            if (title == null & content == null)
                return false;
            if (thread == null)
                return false;
            Post reply = new Post(callingUser, thread, title, content);
            return parent.AddReply(reply);
        }

        public virtual bool createThread(IUser callingUser, ISubForum subForum, string title, string content)
        {
            if (subForum == null)
                return false;
            if (title == null & content == null)
                return false;
            Thread thread = new Thread(subForum);
            Post openingPost = new Post(callingUser, thread, title, content);
            return thread.AddOpeningPost(openingPost);
        }

        public virtual bool editPost(IUser callingUser,string title, string content,Post post)
        {
            if (post.getPublisher() != callingUser)
                return false; // user can't edit other user's posts
            if (title == null & content == null)
                return false;
            post.Content = content;
            post.Title = title;
            return true;
        }

        public virtual bool deletePost(IUser callingUser, Post post)
        {
            if (post.getPublisher() == callingUser)
                return post.DeletePost();
            return false; // user can't delete other user's posts
        }

        public virtual void addFriend(IUser callingUser,IUser friend)
        {
            friend.addToWaitingFriendsList(callingUser);
        }

        public virtual bool removeFriend(IUser callingUser,IUser friendToRemove)
        {
            if (!callingUser.isInFriendsList(friendToRemove))
                return false;
            callingUser.removeFromFriendList(friendToRemove);
            friendToRemove.removeFromFriendList(callingUser);
            return true;
        }

        public virtual bool acceptFriend(IUser callingUser, IUser userToAccept)
        {
            if (!callingUser.isInWaitingList(userToAccept))
                return false;
            callingUser.removeFromWaitingFriendsList(userToAccept);
            callingUser.addToFriendsList(userToAccept);
            userToAccept.addToFriendsList(callingUser);
            return true;
        }

        public virtual void fileComplaint(IUser callingUser, IUser user)
        {
            throw new NotImplementedException();
        }

        public virtual void fileComplaint(IUser callingUser, Moderator moderator)
        {
            throw new NotImplementedException();
        }

        public virtual void deactivateUser(IUser callingUser)
        {
            throw new NotImplementedException();
        }

        public virtual bool SendPrivateMessage(IUser callingUser, string recieverUserName, string title, string content)
        {
            if (title == null & content == null)
                return false;
            IForum forum = callingUser.getForum();
            if (!forum.isUserMember(recieverUserName))
                return false; // not a member in this forum
            IUser reciever = forum.getUser(recieverUserName);
            PrivateMessage privateMessage = new PrivateMessage(title, content, callingUser, reciever);
            reciever.AddReceivedMessage(privateMessage);
            callingUser.AddSentMessage(privateMessage);
            return true;
        }

        public virtual void AddSentMessage(IUser callingUser, PrivateMessage privateMessage)
        {
            callingUser.AddTosentMessages(privateMessage);
        }

        public virtual void AddReceivedMessage(IUser callingUser, PrivateMessage privateMessage)
        {
            callingUser.AddToreceivedMessages(privateMessage);
        }

        //---------------------------------------------------------------------

        //Guest----------------------------------------------------------------

        //TODO: Search a forum for posts by content or by user public abstract List<>



        //---------------------------------------------------------------------
    }
}
