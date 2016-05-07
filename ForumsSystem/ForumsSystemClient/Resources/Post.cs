using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources
{
    public class Post
    {
        private User publisher;
        private List<Post> replies;
        private Post parentPost;
        private Thread thread;
        private string title;
        private string content;
        private int id;
        private static int nextId = 1;//TODO: Change the way to initialize this


        public User Publisher { get { return publisher; } set { publisher = value; } }


        public Post(string title, string content)
        {
            this.title = title;
            this.content = content;
            replies = new List<Post>();
        }

        public List<Post> GetNestedPosts()
        {
            return replies;
        }

        public void AddNestedPost(Post post)
        {
            replies.Add(post);
        }


        public string Title { get { return title; } set { this.title = value; } }
        public string Content { get { return content; } set { this.content = value; } }
        public Thread Thread { get { return thread; } set { this.thread = value; } }

        public int GetId()
        {
            return this.id;
        }

        public Post GetPostById(int id)
        {
            if (this.id == id)
                return this;
            if (replies.Count == 0)
                return null;
            Post res;
            foreach (Post p in replies.ToList<Post>())
            {
                res = p.GetPostById(id);
                if (res != null)
                    return res;
            }
            return null;
        }

    }
}
