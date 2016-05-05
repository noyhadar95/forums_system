using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.CommunicationLayer
{
    class Post
    {

        private string title;
        private string content;
        private List<Post> nestedPosts;

        public string Title { get { return title; } set { title = value; } }
        public string Content { get { return content; } set { content = value; } }

        public Post(string title, string content)
        {
            this.title = title;
            this.content = content;
            nestedPosts = new List<Post>();
        }

        public List<Post> GetNestedPosts()
        {
            return nestedPosts;
        }

        public void AddNestedPost(Post post)
        {
            nestedPosts.Add(post);
        }

    }
}
