using ForumsSystem.Server.ForumManagement.Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.UserManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    [Serializable]
    public class PostNotification
    {
        private NotificationType type;
        private string forumName;
        private string publisher;
        private string subForumName;
        private string title;
        private string content;
        public int id { get; private set; }

        public PostNotification(NotificationType type,string forumName,string publisher,
            string subForumName, string title, string content, int id)
        {
            this.forumName = forumName;
            this.type = type;
            this.publisher = publisher;
            this.subForumName = subForumName;
            this.title = title;
            this.content = content;
            this.id = id;
        }

        private PostNotification()
        {

        }

        public static void populatePostNotification(Dictionary<string, IUser> users, Dictionary<string, IUser> waiting_users, string forumName)
        {
            
            DAL_PostsNotification dpn = new DAL_PostsNotification();
            DataTable forumNotificationTBL = dpn.GetForumNotification(forumName);
            Dictionary<string, IUser> allUsers = users.Union(waiting_users).ToDictionary(k => k.Key, v => v.Value);

            foreach (DataRow notificationRow in forumNotificationTBL.Rows)
            {
                PostNotification notification = new PostNotification();

                int notificationId = (int)notificationRow["PostID"];
                int type = (int)notificationRow["Type"];
                string userName = notificationRow["UserName"].ToString();
                string publisherUserName = notificationRow["PublisherUserName"].ToString();
                string subForumName = notificationRow["SubForumName"].ToString();
                string title = notificationRow["Title"].ToString();
                string content = notificationRow["Content"].ToString();

                notification.type = (NotificationType)type;
                notification.forumName = forumName;
                notification.publisher = publisherUserName;
                notification.subForumName = subForumName;
                notification.title = title;
                notification.content = content;

                allUsers[userName].AddToPostNotification(notification);
                


            }

        }

    }
}
