using ForumsSystem.Server.ForumManagement.Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(Post))]
    [KnownType(typeof(SubForum))]
    public class Thread
    {
        [DataMember]
        private Post openingPost;
        [IgnoreDataMember]
        private ISubForum subForum;

        [DataMember]
        public int id { get; set; }
        private static int nextId = 1;//TODO: Change the way we get nextID

        public Thread(ISubForum subForum)
        {
            this.openingPost = null;
            this.subForum = subForum;

            this.id = nextId++;
        }

        private Thread()
        {

        }
        public static void setNextId()
        {
            DAL_Threads dt = new DAL_Threads();
            Thread.nextId = dt.getMaxId() + 1;
        }
        public static List<Thread> populateThreads(SubForum subForum)
        {
            //List<Thread> threads = new List<Thread>();
            Dictionary<int, Thread> threads = new Dictionary<int, Thread>(); // opening postId, thread
            DAL_Threads dt = new DAL_Threads();
            DataTable threadTbl = dt.GetAllThreads(subForum.getForum().getName(), subForum.getName());
            foreach (DataRow threadRow in threadTbl.Rows)
            {
                Thread thr = new Thread();
                
                int threadId = (int)threadRow["ThreadId"];
                int openingPostID = (int)threadRow["OpeningPostId"];

                thr.subForum = subForum;
                thr.id = threadId;

                threads[openingPostID] = thr;
            }

            Post.PopulatePosts(threads);

            return threads.Values.ToList();


        }
        /// <summary>
        /// Sets the opening post - USED FOR INITIALIZATION
        /// </summary>
        /// <param name="post"></param>
        public void setOpeningPost(Post post)
        {
            this.openingPost = post;
        }

        public string GetTiltle()
        {
            if (openingPost == null)
                return "";
            return openingPost.Title;
        }

        public Post GetOpeningPost()
        {
            return this.openingPost;
        }

        public bool AddOpeningPost(Post openingPost)
        {
            if (this.openingPost != null)
                return false;

            this.openingPost = openingPost;
            Loggers.Logger.GetInstance().AddActivityEntry("A new opening post: " + openingPost.Title + " was added to the thread");
            return true;
        }

        public void DeleteOpeningPost()
        {
            Loggers.Logger.GetInstance().AddActivityEntry("The opening post: " +this.openingPost.Title +" has been deleted");
       this.openingPost = null;
       
        }

        public ISubForum GetSubforum()
        {
            return this.subForum;
        }
        public Post GetPostById(int id)
        {
            if(openingPost!=null)
            return openingPost.GetPostById(id);
            return null;
         /*   if (openingPost == null)
                return null;

            char[] delimiter = { '.' };
            string[] idArray = id.Split(delimiter);
            try {
                int[] idIntArray = Array.ConvertAll(idArray, s => int.Parse(s));
                if (idIntArray[0] != 1)
                    return null;//first number is 1 by the definition of the ID
                Post currPost=openingPost;
                for (int i = 1; i < idIntArray.Length; i++)
                {
                    currPost = currPost.GetReply(idIntArray[i] - 1);
                }
                return currPost;
            } catch(Exception e)
            {
                return null;//error in given id
            }
            
            */
        }
        public int GetNumOfNestedReplies()
        {
            return 1 + openingPost.GetNumOfNestedReplies();
        }

       /* public List<Tuple<int, string, string>> GetPostsByUser(string moderatorName)
        {
            List<Tuple<int, string, string>> posts = new List<Tuple<int, string, string>>();
            Queue<Post> queue = new Queue<Post>();
            queue.Enqueue(this.openingPost);
            Post currPost;
            while (queue.Count>0)
            {
                currPost = queue.Dequeue();
                if (currPost.getPublisher().Equals(moderatorName))
                    posts.Add(new Tuple<int, string, string>(currPost.GetId(), currPost.Title, currPost.Content));
                foreach (Post item in currPost.GetReplies() ?? new List<Post>())
                {
                    queue.Enqueue(item);
                }
            }
            return posts;
        }*/

        public List<Post> GetPostsByUser(string moderatorName)
        {
            List<Post> posts = new List<Post>();
            Queue<Post> queue = new Queue<Post>();
            queue.Enqueue(this.openingPost);
            Post currPost;
            while (queue.Count > 0)
            {
                currPost = queue.Dequeue();
                if (currPost.getPublisher().getUsername().Equals(moderatorName))
                    posts.Add(currPost);
                foreach (Post item in currPost.GetReplies() ?? new List<Post>())
                {
                    queue.Enqueue(item);
                }
            }
            return posts;
        }

        public int GetNumOfPostsByUser(string username)
        {
            int posts = 0;
            Queue<Post> queue = new Queue<Post>();
            queue.Enqueue(this.openingPost);
            Post currPost;
            while (queue.Count > 0)
            {
                currPost = queue.Dequeue();
                if (currPost.getPublisher().getUsername().Equals(username))
                    posts++;
                foreach (Post item in currPost.GetReplies() ?? new List<Post>())
                {
                    queue.Enqueue(item);
                }
            }
            return posts;
        }
        
    }
}
