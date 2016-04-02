using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcceptanceTestsBridge
{
    public interface Bridge
    {
        // return a positive number upon success
        int CreateForum(string adminUserName, string forumProperties);
    }
}
