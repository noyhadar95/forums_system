using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.Data_Access_Layer
{
    class DAL_PostsNotification : DAL_Notification
    {
        public DAL_PostsNotification()
        {
            this.notificationDB = "PostsNotification";
            this.notificationItem = "Post";
        }
    }
}
