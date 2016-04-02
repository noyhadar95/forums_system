using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    interface IForum
    {
        public void EditForumProperties();

        public bool RegisterToForum(string userName, string password, String Email);

        public bool CreateSubForum(string subForumName);

        public bool Login(string userName, string password);
    }
}
