using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources.ForumManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(System))]
    [Serializable]

    public class SuperAdmin
    {

        //private static SuperAdmin instance = null;
        [DataMember]
        public string userName { get; set; }
        [DataMember]
        public string password { get; set; }
        [DataMember]
        public bool isLoggedIn { get; set; }
        [DataMember]
        public System forumSystem { get; private set; }
    }
}
