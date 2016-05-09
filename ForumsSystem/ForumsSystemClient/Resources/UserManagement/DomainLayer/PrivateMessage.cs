using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources.UserManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    public class PrivateMessage
    {
        [DataMember]
        public string title { get; private set; }
        [DataMember]
        public string content { get; private set; }
        [DataMember]
        public User sender { get; private set; }
        [DataMember]
        public User receiver { get; private set; }
    }
}
