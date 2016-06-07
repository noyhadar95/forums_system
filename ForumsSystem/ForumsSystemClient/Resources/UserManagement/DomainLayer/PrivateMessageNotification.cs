using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources.UserManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    public class PrivateMessageNotification
    {
        [DataMember]
        public string sender { get; private set; }
        [DataMember]
        public string title { get; private set; }
        [DataMember]
        public string content { get; private set; }
        [DataMember]
        public int id { get; private set; }
        public PrivateMessageNotification(string sender, string title, string content, int id)
        {
            this.sender = sender;
            this.title = title;
            this.content = content;
            this.id = id;
        }

      
    }
}
