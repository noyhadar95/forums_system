using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources.UserManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(User))]
    public class PrivateMessage
    {
        [DataMember]
        public string title { get; private set; }
        [DataMember]
        public string content { get; private set; }
        [IgnoreDataMember]
        public User sender { get; private set; }
        [IgnoreDataMember]
        public User receiver { get; private set; }
        [DataMember]
        public string senderUsername { get; private set; }
    }
}
