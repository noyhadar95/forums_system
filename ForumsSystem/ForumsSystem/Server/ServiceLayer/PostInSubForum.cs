using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.ForumManagement.DomainLayer;
using ForumsSystem.Server.UserManagement.DomainLayer;

namespace ForumsSystem.Server.ServiceLayer
{
    class PostInSubForum
    {
        public static bool AddThread(ISubForum subForum,IUser publisher, string title, string content)
        {
            //TODO: check that user is a member in the forum
            Thread thread = subForum.createThread();
            thread.AddOpeningPost(new Post(publisher, thread, title, content));
            //TODO: notify;
            return true;
        }

        public static bool AddReply(Post post, IUser publisher, string title, string content)
        {
            //TODO: check that user is a member in the forum
            post.AddReply(new Post(publisher, post.Thread, title, content));
            //TODO: notify
            return true;
        }
    }
}
