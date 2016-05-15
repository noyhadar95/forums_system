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
        private string sender;
        [DataMember]
        private string title;
        [DataMember]
        private string content;
        [DataMember]
        private int id;

        public PrivateMessageNotification(string sender,string title, string content,int id)
        {
            this.sender = sender;
            this.title = title;
            this.content = content;
            this.id = id;
        }
    }
}
