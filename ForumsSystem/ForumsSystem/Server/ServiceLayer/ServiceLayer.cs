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

        public IForum CreateForum(SuperAdmin creator, string name, Policy properties, List<IUser> adminUsername)
        {
            return creator.createForum(name, properties, adminUsername);
        }

        public bool SetForumProperties(IUser user, IForum forum, Policy properties)
        {
            //TODO: fix this to use user
            forum.SetPolicy(properties);
            return true;
        }

        public bool ChangeForumProperties(IForum forum, Policy properties)
        {
            //TODO: fix this to use user
            Policy temp = properties;
            while (temp != null)
            {
                forum.RemovePolicy(temp.Type);
                temp = temp.NextPolicy; // delete old ones
            }
            return forum.AddPolicy(properties); // add new ones
        }

        public bool RegisterToForum(IUser guest, IForum forum, string userName, string password, string email, DateTime dateOfBirth)
        {
            return guest.RegisterToForum(userName, password, forum, email,dateOfBirth);
           
        }

        public ISubForum CreateSubForum( IUser creator, string name, Dictionary<string,DateTime> moderators)
        {

           return creator.createSubForum(name, moderators);

        }

        public Thread AddThread(ISubForum subForum, IUser publisher, string title, string content)
        {
            return publisher.createThread(subForum, title, content);
           
        }

        public Post AddReply(Post post, IUser publisher, string title, string content)
        {
            return publisher.postReply(post, post.Thread, title, content);
            
        }

        public IUser MemberLogin(string username, string password, IForum forum)
        {
            return forum.Login(username, password);
        }

        public PrivateMessage SendPrivateMessage(IUser from, string to, string title, string content)
        {
            return from.SendPrivateMessage(to, title, content);

           
        }

        public bool ChangeExpirationDate(IUser admin, DateTime newDate, string moderator,ISubForum subforum)
        {
           return admin.editExpirationTimeOfModerator(moderator, newDate, subforum);
        }

        public bool DeletePost(IUser deleter, Post post)
        {
            return deleter.deletePost(post);
        }

        public bool DeleteForumProperties(IForum forum, List<Policies> properties)
        {
            //TODO: pnina
            foreach (Policies pol in properties.ToList<Policies>())
            {
                forum.RemovePolicy(pol);
            }
            return true;
        }

        public IForum GetForum(string forumName)
        {
            return SuperAdmin.GetInstance().forumSystem.getForum(forumName);
        }



    }
}
