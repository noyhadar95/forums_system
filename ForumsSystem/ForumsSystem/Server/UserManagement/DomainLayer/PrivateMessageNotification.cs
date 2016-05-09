using ForumsSystem.Server.ForumManagement.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.UserManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    public class PrivateMessageNotification
    {
        public string sender { get; private set; } 
        public string title { get; private set; }
        public string content { get; private set; }

        public int id { get; private set; }
        public PrivateMessageNotification(string sender,string title,string content,int id)
        {
            this.sender = sender;
            this.title = title;
            this.content = content;
            this.id = id;
        }
    }
}
