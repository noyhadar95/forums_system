using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.ForumManagement.DomainLayer;
using ForumsSystem.Server.UserManagement.DomainLayer;

namespace ForumsSystem.Server.ServiceLayer
{
    class ServiceLayer
    {
        public IForum CreateForum(IUser creator, string name, Policy properties, List<IUser> adminUsername)
        {
            return ForumsSystem.Server.ServiceLayer.CreateForum.Create(creator, name, properties, adminUsername);
        }

        public ISubForum CreateSubForum(IForum forum, IUser creator, string name, List<IUser> moderators)
        {
            return ForumsSystem.Server.ServiceLayer.CreateSubForum.Create(forum, creator, name, moderators);
        }

        public bool DeletePost(IUser deleter, Post post)
        {
            return ForumsSystem.Server.ServiceLayer.DeletePost.Delete(deleter, post);
        }

        public IUser MemberLogin(string username, string password, IForum forum)
        {
            return ForumsSystem.Server.ServiceLayer.ForumMemberLogin.MemberLogin(username, password, forum);
        }

        public bool SetForumProperties(IForum forum, Policy properties)
        {
            return ForumsSystem.Server.ServiceLayer.ForumProperties.SetForumProperties(forum, properties);
        }

        public bool ChangeForumProperties(IForum forum, Policy properties)
        {
            return ForumsSystem.Server.ServiceLayer.ForumProperties.ChangeForumProperties(forum, properties);
        }
        public bool DeleteForumProperties(IForum forum, List<Policies> properties)
        {
            return ForumsSystem.Server.ServiceLayer.ForumProperties.DeleteForumProperties(forum, properties);
        }
        public bool AddThread(ISubForum subForum, IUser publisher, string title, string content)
        {
            return ForumsSystem.Server.ServiceLayer.PostInSubForum.AddThread(subForum, publisher, title, content);
        }

        public bool AddReply(Post post, IUser publisher, string title, string content)
        {
            return ForumsSystem.Server.ServiceLayer.PostInSubForum.AddReply(post, publisher, title, content);
        }
        public static bool Register(IForum forum, string userName, string password, string email, int age)
        {
            return ForumsSystem.Server.ServiceLayer.RegisterToForum.Register(forum, userName, password, email, age);
        }
        public bool Send(IUser from, IUser to, string title, string content)
        {
            return ForumsSystem.Server.ServiceLayer.SendPrivateMessage.Send(from, to, title, content);
        }
        public bool ChangeExpirationDate(DateTime newDate, Moderator moderator)
        {
            return ForumsSystem.Server.ServiceLayer.TrackModerators.ChangeExpirationDate(newDate, moderator);
        }
    }
}
