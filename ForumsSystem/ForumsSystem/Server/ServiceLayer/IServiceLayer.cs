using System;
using ForumsSystem.Server.UserManagement.DomainLayer;
using ForumsSystem.Server.ForumManagement.DomainLayer;
using System.Collections.Generic;

namespace ForumsSystem.Server.ServiceLayer
{
    public interface IServiceLayer
    {

        #region Use Cases Methods

        bool AddReply(Post post, IUser publisher, string title, string content);

        bool AddThread(ISubForum subForum, IUser publisher, string title, string content);

        bool ChangeExpirationDate(DateTime newDate, Moderator moderator);

        bool ChangeForumProperties(IForum forum, Policy properties);

        IForum CreateForum(IUser creator, string name, Policy properties, List<IUser> adminUsername);

        ISubForum CreateSubForum(IForum forum, IUser creator, string name, List<IUser> moderators);

        bool DeletePost(IUser deleter, Post post);

        bool InitializeSystem(string username, string pass);

        IUser MemberLogin(string username, string password, IForum forum);

        bool RegisterToForum(IForum forum, string userName, string password, string email, int age);

        bool SendPrivateMessage(IUser from, IUser to, string title, string content);

        bool SetForumProperties(IForum forum, Policy properties);

        #endregion


        // other methods
        bool DeleteForumProperties(IForum forum, List<Policies> properties);

        IForum GetForum(string forumName);

       
    }
}
