using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.UserManagment;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    public class Post
    {
        private IUser publisher;
        private List<Post> replies;
        private Post parentPost;

        public Post(IUser publisher , Post parentPost)
        {
            this.publisher = publisher;
            this.parentPost = parentPost;

        }

        public bool DeletePost()
        {
            foreach (Post p in replies)
            {
                p.DeletePost();
            }
            return this.parentPost.DeleteReply(this);
        }

        private bool DeleteReply(Post post)
        {
            return replies.Remove(post);
        }

        public bool AddReply(Post reply)
        {
            this.replies.Add(reply);
            return true;
        }

        public void Notify()
        {
            throw new NotImplementedException();
            //TODO
        }
    }
}
