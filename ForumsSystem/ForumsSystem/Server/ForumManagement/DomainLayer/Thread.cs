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

        public Thread(ISubForum subForum)
        {
            this.openingPost = null;
            this.subForum = subForum;
        }

        public string GetTiltle()
        {
            return openingPost.Title;
        }

        public bool AddOpeningPost(Post openingPost)
        {
            if (openingPost != null)
                return false;

            this.openingPost = openingPost;
            return true;
        }
        public Post GetPostById(string id)
        {
            if (openingPost == null)
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
            
            
        }

    }
}
