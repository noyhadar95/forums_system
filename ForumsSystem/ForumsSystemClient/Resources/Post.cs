using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources
{
    class Post
    {
        private User publisher;
        private List<Post> replies;
        private Post parentPost;
        private Thread thread;
        private string title;
        private string content;
        private int id;
        private static int nextId = 1;//TODO: Change the way to initialize this

        public string Title { get { return title; } set { title = value; } }
        public string Content { get { return content; } set { content = value; } }


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

    }
}
