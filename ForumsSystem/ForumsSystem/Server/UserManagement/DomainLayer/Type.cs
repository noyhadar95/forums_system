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

        public virtual ISubForum createSubForum(IUser callingUser, string subForumName, IForum forum, Dictionary<string,DateTime> users)
        {
            if (!callingUser.isLogin())
                return null;
            Dictionary<IUser, DateTime> moderators = new Dictionary<IUser, DateTime>();
            if (subForumName == "" || forum == null)
                return null;
            foreach (KeyValuePair<string, DateTime> moderator in users)
            {
                if (!forum.isUserMember(moderator.Key))
                    return null; // moderator should be member in the forum
                if (moderator.Value != null && moderator.Value.CompareTo(DateTime.Now) < 0)
                    return null; // expiration date should be after now
                moderators.Add(forum.getUser(moderator.Key), moderator.Value);
            }
            if (forum.getSubForum(subForumName) != null)
                return null;
            ISubForum subforum = new SubForum(forum, callingUser, subForumName);
            forum.addSubForum(subforum);
            foreach (KeyValuePair<IUser, DateTime> moderator in moderators)
            {
                subforum.addModerator(callingUser, moderator.Key, moderator.Value);
            }
            return subforum;
        }

        public virtual bool appointModerator(IUser callingUser, string userName, DateTime expirationTime, ISubForum subForum)
        {
            if (!callingUser.isLogin())
                return false;
            IForum forum = subForum.getForum();
            if (forum.isUserMember(userName))
                return false; // moderator should be member in the forum
            if (expirationTime != null && expirationTime.CompareTo(DateTime.Now) < 0)
                return false; // expiration date should be after now
            IUser moderator = forum.getUser(userName);
            subForum.addModerator(callingUser, moderator, expirationTime);
            return true;
        }

        public virtual bool editExpirationTimeOfModerator(IUser callingUser,string userName,DateTime expirationTime, ISubForum subForum)
        {
            if (!callingUser.isLogin())
                return false;
            if (userName == "")
                return false;
            if (subForum == null)
                return false;
            if (!subForum.isModerator(userName))
                return false;
            Moderator moderator = subForum.getModeratorByUserName(userName);
            if (moderator.appointer != callingUser)
                return false;
            moderator.changeExpirationDate(expirationTime);
            return true;
        }

        public virtual bool removeModerator(IUser callingUser, string userName, ISubForum subForum)
        {
            if (!callingUser.isLogin())
                return false;
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

        public virtual bool Login(IUser callingUser)
        {
            IForum forum = callingUser.getForum();
            if (forum.Login(callingUser.getUsername(), callingUser.getPassword()) == null)
                return false;
            return true;
        }

        public virtual Post postReply(IUser callingUser, Post parent, Thread thread, string title, string content)
        {
            if (!callingUser.isLogin())
                return null;
            if ((title == null | title == "") & (content == null || content ==""))
                return null;
            if (thread == null)
                return null;
            if (parent == null)
                return null;
            Post reply = new Post(callingUser, thread, title, content);
            if (parent.AddReply(reply))
                return reply;
            return null;
        }

        public virtual Thread createThread(IUser callingUser, ISubForum subForum, string title, string content)
        {
            if (!callingUser.isLogin())
                return null;
            if (subForum == null)
                return null;
            if ((title == null || title =="") & (content == null || content == ""))
                return null;
            Thread thread = new Thread(subForum);
            Post openingPost = new Post(callingUser, thread, title, content);
            if (thread.AddOpeningPost(openingPost))
                return thread;
            return null;
        }

        public virtual bool editPost(IUser callingUser,string title, string content,Post post)
        {
            if (!callingUser.isLogin())
                return false;
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
            if (!callingUser.isLogin())
                return false;
            if (post == null)
                return false;
            if (post.getPublisher() == callingUser)
                return post.DeletePost();
            return false; // user can't delete other user's posts
        }

        public virtual void addFriend(IUser callingUser,IUser friend)
        {
            if (!callingUser.isLogin())
                throw new Exception("user should login first");
            friend.addToWaitingFriendsList(callingUser);
        }

        public virtual bool removeFriend(IUser callingUser,IUser friendToRemove)
        {
            if (!callingUser.isLogin())
                return false;
            if (!callingUser.isInFriendsList(friendToRemove))
                return false;
            callingUser.removeFromFriendList(friendToRemove);
            friendToRemove.removeFromFriendList(callingUser);
            return true;
        }

        public virtual bool acceptFriend(IUser callingUser, IUser userToAccept)
        {
            if (!callingUser.isLogin())
                return false;
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

        public virtual PrivateMessage SendPrivateMessage(IUser callingUser, string recieverUserName, string title, string content)
        {
            if (!callingUser.isLogin())
                return null;
            if ((title == null || title=="") & (content == null || content ==""))
                return null;
            IForum forum = callingUser.getForum();
            if (!forum.isUserMember(recieverUserName))
                return null; // not a member in this forum
            IUser reciever = forum.getUser(recieverUserName);
            PrivateMessage privateMessage = new PrivateMessage(title, content, callingUser, reciever);
            reciever.AddReceivedMessage(privateMessage);
            callingUser.AddSentMessage(privateMessage);
            return privateMessage;
        }

        public virtual void AddSentMessage(IUser callingUser, PrivateMessage privateMessage)
        {
            if (!callingUser.isLogin())
                throw new Exception("user should login first");
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
