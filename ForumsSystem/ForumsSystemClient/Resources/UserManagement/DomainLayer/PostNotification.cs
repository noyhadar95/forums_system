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

            public PostNotification(NotificationType type, string forumName, string publisher,
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
        public string GetTitle()
        {
            return this.title;
        }
        public string GetPublisher()
        {
            return this.publisher;
        }

    }
    }

