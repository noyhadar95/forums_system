using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources.ForumManagement.DomainLayer
{
    public class SuperAdmin
    {
        private static SuperAdmin instance = null;
        public string userName { get; set; }
        public string password { get; set; }
        public bool isLoggedIn { get; set; }
        public System forumSystem { get; private set; }
    }
}
