using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcceptanceTestsBridge
{
    public class ProxyBridge : Bridge
    {
        private Bridge realBridge;

        public ProxyBridge()
        {
            realBridge = null;
        }

        public int CreateForum(string adminUserName, string forumProperties)
        {
            return 1;
        }

        public int SetForumProperties(string forumProperties)
        {
            return 1;
        }


    }
}
