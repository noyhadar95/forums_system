using ForumsSystem.Server.ForumManagement.Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.UserManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(User))]
    public class PrivateMessage
    {
        [DataMember]
        public string title { get;private set; }
        [DataMember]
        public string content { get; private set; }
        [DataMember]
        public IUser sender { get; private set; }
        [DataMember]
        public IUser receiver { get; private set; }

        public int id { get; private set; }

        public PrivateMessage(string title,string content, IUser sender, IUser receiver)
        {
            this.title = title;
            this.content = content;
            this.sender = sender;
            this.receiver = receiver;
            DAL_Messages dm = new DAL_Messages();
            this.id = dm.CreateMessage(sender.getForum().getName(), sender.getUsername(), receiver.getUsername(), title, content);
        }

    }
}
