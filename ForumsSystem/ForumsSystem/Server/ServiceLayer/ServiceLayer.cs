using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.ForumManagement.DomainLayer;
using ForumsSystem.Server.UserManagement.DomainLayer;

namespace ForumsSystem.Server.ServiceLayer
{
    public class ServiceLayer : IServiceLayer
    {
        private ForumsSystem.Server.ForumManagement.DomainLayer.System sys;


        public ServiceLayer()
        {
            sys = new ForumsSystem.Server.ForumManagement.DomainLayer.System();
        }

        // initialize the system with the username and pass as the super admin login info
        public bool InitializeSystem(string username, string pass)
        {
            sys.changeAdminUserName(username);
            sys.changeAdminPassword(pass);
            return true;
        }

        public IForum CreateForum(IUser creator, string name, Policy properties, List<IUser> adminUsername)
        {
            if (adminUsername.Count == 0)// there must be at least 1 admin
                return null;

            PolicyParametersObject param = new PolicyParametersObject(Policies.AdminAppointment);
            foreach (IUser user in adminUsername.ToList<IUser>())
            {
                param.User = user;
                if (!properties.CheckPolicy(param))//check if user can be an admin (Policies) 
                    return null;
            }

            IForum forum = sys.createForum(name);
            forum.AddPolicy(properties);
            foreach (IUser user in adminUsername.ToList<IUser>())
            {
                user.ChangeType(new Admin());
                forum.RegisterToForum(user);
            }

            return forum;
        }

        public bool SetForumProperties(IForum forum, Policy properties)
        {
            forum.SetPolicy(properties);
            return true;
        }

        public bool ChangeForumProperties(IForum forum, Policy properties)
        {
            Policy temp = properties;
            while (temp != null)
            {
                forum.RemovePolicy(temp.Type);
                temp = temp.NextPolicy; // delete old ones
            }
            return forum.AddPolicy(properties); // add new ones
        }

        public bool RegisterToForum(IForum forum, string userName, string password, string email, int age)
        {
            // ----CHECK POLICIES----
            PolicyParametersObject param = new PolicyParametersObject(Policies.MinimumAge);
            param.SetAgeOfUser(age);
            if (!forum.GetPolicy().CheckPolicy(param))
                return false;
            param.SetPolicy(Policies.Password);
            param.SetPassword(password);
            if (!forum.GetPolicy().CheckPolicy(param))
                return false;
            param.SetPolicy(Policies.UsersLoad);
            param.SetNumOfUsers(forum.GetNumOfUsers());
            if (!forum.GetPolicy().CheckPolicy(param))
                return false;

            return forum.RegisterToForum(userName, password, email);
        }

        public ISubForum CreateSubForum(IForum forum, IUser creator, string name, List<IUser> moderators)
        {
            //----CHECK POLICIES----
            //  if(creator.GetType()!=) TODO: check if admin
            //return false;

            if (moderators.Count == 0)
                return null;
            PolicyParametersObject param = new PolicyParametersObject(Policies.MaxModerators);
            param.NumOfModerators = moderators.Count;
            if (!forum.GetPolicy().CheckPolicy(param))
                return null;
            param.SetPolicy(Policies.ModeratorAppointment);
            foreach (IUser user in moderators.ToList<IUser>())
            {
                param.User = user;
                if (!forum.GetPolicy().CheckPolicy(param))
                    return null;
            }
            ISubForum newSub = new SubForum(forum, creator, name);

            foreach (IUser user in moderators.ToList<IUser>())
            {
                newSub.addModerator(creator, user, DateTime.Today);//TODO: get dates as args
            }
            return newSub;

        }

        public bool AddThread(ISubForum subForum, IUser publisher, string title, string content)
        {
            //TODO: check that user is a member in the forum
            Thread thread = subForum.createThread();
            thread.AddOpeningPost(new Post(publisher, thread, title, content));
            //TODO: notify;
            // TODO: maybe return the result of post.AddReply(..);
            return true;
        }

        public bool AddReply(Post post, IUser publisher, string title, string content)
        {
            //TODO: check that user is a member in the forum
            post.AddReply(new Post(publisher, post.Thread, title, content));
            //TODO: notify
            // TODO: maybe return the result of post.AddReply(..);
            return true;
        }

        public IUser MemberLogin(string username, string password, IForum forum)
        {
            return forum.Login(username, password);
        }

        public bool SendPrivateMessage(IUser from, IUser to, string title, string content)
        {
            if (!from.isInFriendsList(to))
                return false; // private messages only between friends
            PrivateMessage message = new PrivateMessage(title, content, from, to);
            from.AddSentMessage(message);
            to.AddReceivedMessage(message);
            return true;
        }

        public bool ChangeExpirationDate(DateTime newDate, Moderator moderator)
        {
            if ((DateTime.Today - newDate).TotalMilliseconds > 0)
                return false; // cant change to a date that already passed
            moderator.changeExpirationDate(newDate);
            //TODO: notify moderator
            return true;
        }

        public bool DeletePost(IUser deleter, Post post)
        {
            //TODO: check that user can delete the post

            bool res = post.DeletePost();
            if (!res)
                return false;
            if (post.Thread.GetOpeningPost() == null)
            {
                return post.Thread.GetSubforum().removeThread(post.Thread);
            }
            return res;
        }

        public bool DeleteForumProperties(IForum forum, List<Policies> properties)
        {
            foreach (Policies pol in properties.ToList<Policies>())
            {
                forum.RemovePolicy(pol);
            }
            return true;
        }

        public IForum GetForum(string forumName)
        {
            return sys.getForum(forumName);
        }



    }
}
