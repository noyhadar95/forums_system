using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources
{
    class Thread
    {
        private Post openingPost;
        private SubForum subForum;

        public int id { get; set; }
        private static int nextId = 1;//TODO: Change the way we get nextID
    }
}
