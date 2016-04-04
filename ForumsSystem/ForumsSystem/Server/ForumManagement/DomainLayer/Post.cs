using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.UserManagement.DomainLayer;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    public class Post
    {
        private IUser publisher;
        private List<Post> replies;
        private Post parentPost;
        private Thread thread;
        private string title;
        private string content;

        public Post(IUser publisher, Thread thread, Post parentPost, string title, string content)
        {
            this.publisher = publisher;
            this.parentPost = parentPost;
            this.replies = new List<Post>();
            this.title = title;
            this.content = content;
            this.thread = thread;

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

        public int NumOfReplies()
        {
            return replies.Count;
        }

        public Post GetReply(int id)
        {
            return replies[id];
        }
        

        public string Title { get { return title; } set { this.title = value; } }
        public string Content { get { return content; } set { this.content = value; } }
        public Thread Thread { get { return thread; } set { this.thread = value; } }
    }
}
