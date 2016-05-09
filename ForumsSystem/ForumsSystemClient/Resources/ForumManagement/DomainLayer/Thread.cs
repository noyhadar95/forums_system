using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources.ForumManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(Post))]
    [KnownType(typeof(SubForum))]
    public class Thread
    {
        [DataMember]
        private Post openingPost;
        [IgnoreDataMember]
        private SubForum subForum;
        [DataMember]
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
