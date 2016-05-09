using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.UserManagement.DomainLayer;
using ForumsSystem.Server.ForumManagement.Data_Access_Layer;
using System.Data;

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
        private int id;
        private static int nextId = 1;//TODO: Change the way to initialize this

        public Post(IUser publisher, Thread thread, string title, string content)
        {
            this.publisher = publisher;
            this.parentPost = null;
            this.replies = new List<Post>();
            this.title = title;
            this.content = content;
            this.thread = thread;
            this.id = nextId++;

        }
        private Post()
        {

        }

        public static void PopulatePosts(Dictionary<int, Thread> threads)
        {
            DAL_Posts dp = new DAL_Posts();

            foreach (KeyValuePair<int, Thread> entry in threads)
            {
                DataTable openingPostsTbl = dp.GetPost(entry.Key);
                foreach (DataRow threadRow in openingPostsTbl.Rows) //should be only one
                {
                    Post post = new Post();

                    post.id = entry.Key;

                    string publisherUserName = threadRow["PublisherUserName"].ToString();
                    Forum forum = (Forum)entry.Value.GetSubforum().getForum();

                    post.publisher = forum.getDictionaryOfUsers()[publisherUserName];

                    post.parentPost = null;
                    post.thread = entry.Value;
                    
                    post.title = threadRow["Title"].ToString();
                    post.content = threadRow["Content"].ToString();


                    List<Post> replies = getRepliesOfPost(post);
                    post.replies = replies;


                    //Update the thread
                    entry.Value.setOpeningPost(post);

                }
            }
        }

        private static List<Post> getRepliesOfPost(Post post)
        {
            List<Post> posts = new List<Post>();
            DAL_Posts dp = new DAL_Posts();
            DataTable repliesTbl = dp.GetPostReplies(post.id);
            foreach (DataRow replyRow in repliesTbl.Rows)
            {
                Post reply = new Post();

                reply.id = (int)replyRow["PostID"];

                string publisherUserName = replyRow["PublisherUserName"].ToString();
                Forum forum = (Forum)post.thread.GetSubforum().getForum();

                reply.publisher = forum.getDictionaryOfUsers()[publisherUserName];

                reply.parentPost = post;
                reply.thread = post.thread;

                reply.title = replyRow["Title"].ToString();
                reply.content = replyRow["Content"].ToString();

                List<Post> recursiveReplies = getRepliesOfPost(reply);
                reply.replies = recursiveReplies;

                posts.Add(reply);
            }

            return posts;
        }

        public int GetId()
        {
            return this.id;
        }

        public Post GetPostById(int id)
        {
            if(this.id==id)
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
        public bool DeletePost()
        {
            Loggers.Logger.GetInstance().AddActivityEntry("The post was deleted");
            foreach (Post p in replies.ToList<Post>())
            {
                p.DeletePost();
            }
            if(this.parentPost!=null)
                return this.parentPost.DeleteReply(this);
            else//opening post
            {
                this.Thread.DeleteOpeningPost();
                this.Thread.GetSubforum().removeThread(this.Thread);
                return true;
            }
        }

        private bool DeleteReply(Post post)
        {
            return replies.Remove(post);
        }

        public bool AddReply(Post reply)
        {
            if (this == reply)
                return false;
            if (reply.replies.Contains(this))
                return false;
            reply.SetParent(this);
            this.replies.Add(reply);
            Loggers.Logger.GetInstance().AddActivityEntry("A reply to the post was added");
            return true;
        }

        public void Notify()
        {
            throw new NotImplementedException();
            //TODO
        }

        public void SetParent(Post parent)
        {
            this.parentPost = parent;
        }

        public int NumOfReplies()
        {
            return replies.Count;
        }

        public Post GetReply(int id)
        {
            return replies[id];
        }
        public List<Post> GetReplies()
        {
            return replies;
        }


        public string Title { get { return title; } set { this.title = value; } }
        public string Content { get { return content; } set { this.content = value; } }
        public Thread Thread { get { return thread; } set { this.thread = value; } }
        public IUser getPublisher()
        {
            return publisher;
        }
        public int GetNumOfNestedReplies()
        {
            int res = replies.Count;
            if (replies.Count == 0)
                return res;
            foreach (Post p in replies.ToList<Post>())
            {
                res += p.GetNumOfNestedReplies();
            }
            return res;
        }

        public List<Post> GetNestedPostsByMember(string memberUserName)
        {
            List<Post> posts = new List<Post>();
            if(publisher.getUsername() == memberUserName)
                posts.Add(this);
            if (replies.Count == 0)
                return posts;
            foreach (Post p in replies.ToList<Post>())
            {
                posts.AddRange(p.GetNestedPostsByMember(memberUserName));
            }
            return posts;
        }

    }
}
