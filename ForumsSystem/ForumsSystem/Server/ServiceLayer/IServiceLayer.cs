using System;
using ForumsSystem.Server.UserManagement.DomainLayer;
using ForumsSystem.Server.ForumManagement.DomainLayer;
using System.Collections.Generic;

namespace ForumsSystem.Server.ServiceLayer
{
    public interface IServiceLayer
    {

        #region Use Cases Methods

         Post AddReply(Post post, IUser publisher, string title, string content);

        Thread AddThread(ISubForum subForum, IUser publisher, string title, string content);
        bool ChangeExpirationDate(IUser admin, DateTime newDate, string moderator, ISubForum subforum);

        bool ChangeForumPropertiesbool ChangeForumProperties(IUser user, IForum forum, Policy properties);

        IForum CreateForum(SuperAdmin creator, string name, Policy properties, List<IUser> adminUsername);

        ISubForum CreateSubForum(IUser creator, string name, Dictionary<string, DateTime> moderators);

        bool DeletePost(IUser deleter, Post post);

        bool InitializeSystem(string username, string pass);

        IUser MemberLogin(string username, string password, IForum forum);

        bool RegisterToForum(IUser guest, IForum forum, string userName, string password, string email, DateTime dateOfBirth);


        PrivateMessage SendPrivateMessage(IUser from, string to, string title, string content);

        bool SetForumProperties(IUser user, IForum forum, Policy properties);

        #endregion


        // other methods
        bool DeleteForumProperties(IUser user,IForum forum, List<Policies> properties);

        IForum GetForum(string forumName);

       
    }
}
