using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources.ForumManagement.DomainLayer
{
    public class Thread
    {
        private Post openingPost;
        private SubForum subForum;

        public int id { get; set; }

        public Post OpeningPost
        {
            get
            {
                return openingPost;
            }

            set
            {
                openingPost = value;
            }
        }

        public SubForum SubForum
        {
            get
            {
                return subForum;
            }

            set
            {
                subForum = value;
            }
        }

        private static int nextId = 1;//TODO: Change the way we get nextID

        public Post GetPostById(int id)
        {
            if (openingPost != null)
                return openingPost.GetPostById(id);
            return null;
        }
    }
}
