﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.ForumManagement.DomainLayer;

namespace ForumsSystem.Server.UserManagement.DomainLayer
{
    public class Guest : Type
    {
        public override bool acceptFriend(IUser callingUser, IUser userToAccept)
        {
            throw new Exception("permission denied");
        }

        public override void addFriend(IUser callingUser, IUser friend)
        {
            throw new Exception("permission denied");
        }

        public override void AddReceivedMessage(IUser callingUser, PrivateMessage privateMessage)
        {
            throw new Exception("permission denied");
        }

        public override void AddSentMessage(IUser callingUser, PrivateMessage privateMessage)
        {
            throw new Exception("permission denied");
        }

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

        public override bool createThread(IUser callingUser, ISubForum subForum, string title, string content)
        {
            throw new Exception("permission denied");
        }

        public override void deactivateUser(IUser callingUser)
        {
            throw new Exception("permission denied");
        }

        public override bool deletePost(IUser callingUser, Post post)
        {
            throw new Exception("permission denied");
        }

        public override bool editPost(IUser callingUser, string title, string content, Post post)
        {
            throw new Exception("permission denied");
        }

        public override void fileComplaint(IUser callingUser, Moderator moderator)
        {
            throw new Exception("permission denied");
        }

        public override void fileComplaint(IUser callingUser, IUser user)
        {
            throw new Exception("permission denied");
        }

        public override void getComplaint(IUser callingUser)
        {
            throw new Exception("permission denied");
        }

        public override bool postReply(IUser callingUser, Post parent, Thread thread, string title, string content)
        {
            throw new Exception("permission denied");
        }

        public override bool removeFriend(IUser callingUser, IUser friendToRemove)
        {
            throw new Exception("permission denied");
        }

        public override bool removeModerator(IUser callingUser, string userName, ISubForum subForum)
        {
            throw new Exception("permission denied");
        }

        public override PrivateMessage SendPrivateMessage(IUser callingUser, string recieverUserName, string title, string content)
        {
            throw new Exception("permission denied");
        }

        public override void suspendModerator(IUser callingUser, string userName, ISubForum subForum)
        {
            throw new Exception("permission denied");
        }
    }
}
