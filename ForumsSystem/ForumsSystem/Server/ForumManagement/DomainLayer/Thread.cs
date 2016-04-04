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

        public Thread(ISubForum subForum, Post openingPost)
        {
            this.openingPost = openingPost;
            this.subForum = subForum;
        }

        public string GetTiltle()
        {
            return openingPost.Title;
        }

        public Post GetPostById(string id)
        {
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
