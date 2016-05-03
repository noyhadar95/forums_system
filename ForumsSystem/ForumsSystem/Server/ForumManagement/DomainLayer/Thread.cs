using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    public class Thread
    {
        private Post openingPost;
        private ISubForum subForum;

        public int id { get; set; }
        private static int nextId = 1;//TODO: Change the way we get nextID

        public Thread(ISubForum subForum)
        {
            this.openingPost = null;
            this.subForum = subForum;

            this.id = nextId++;
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

        public List<Tuple<int, string, string>> GetPostsByUser(string moderatorName)
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
                if (currPost.getPublisher().Equals(username))
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
