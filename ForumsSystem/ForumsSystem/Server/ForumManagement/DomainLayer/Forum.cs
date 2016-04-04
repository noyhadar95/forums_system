using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    public class Forum : IForum
    {

        public List<ISubForum> sub_forums; 


        public bool RegisterToForum(string userName, string password, string Email)
        {
            throw new NotImplementedException();
        }

        public bool CreateSubForum(string subForumName)
        {
            throw new NotImplementedException();
        }

        public bool Login(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public bool InitForum()
        {
            throw new NotImplementedException();
        }

        public bool AddPolicy()
        {
            throw new NotImplementedException();
        }

        public bool RemovePolicy()
        {
            throw new NotImplementedException();
        }
    }
}
