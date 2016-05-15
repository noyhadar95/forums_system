using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources.UserManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    public class PostNotification
    {
        [DataMember]
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
        private int id;

        public PostNotification(string forumName, string publisher, string subForumName, string title, string content,
            int id, NotificationType type)
        {
            this.forumName = forumName;
            this.publisher = publisher;
            this.subForumName = subForumName;
            this.title = title;
            this.content = content;
            this.id = id;
            this.type = type;
        }

        public int GetId()
        {
            return id;
        }
    }
}
