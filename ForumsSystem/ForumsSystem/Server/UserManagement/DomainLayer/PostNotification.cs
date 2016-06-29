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
    public class PostNotification
    {
        [IgnoreDataMember]
        private NotificationType type;
        [DataMember]
        private string forumName;
        [DataMember]
        private string publisher;
        [DataMember]
        private string subForumName;
        [DataMember]
        private string title;
        [DataMember]
        private string content;
        [DataMember]
        public int id { get; private set; }
        [DataMember]
        public int threadId;

        public PostNotification(NotificationType type,string forumName,string publisher,
            string subForumName, string title, string content, int id, int threadId)
        {
            this.forumName = forumName;
            this.type = type;
            this.publisher = publisher;
            this.subForumName = subForumName;
            this.title = title;
            this.content = content;
            this.id = id;
            this.threadId = threadId;
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
