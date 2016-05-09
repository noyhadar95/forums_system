using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources.ForumManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(Forum))]
    public class System
    {
        // private SuperAdmin superAdmin;
        [DataMember]
        private Dictionary<string, Forum> forums; //name, forum

        public Dictionary<string, Forum> Forums
        {
            get
            {
                return forums;
            }

            set
            {
                forums = value;
            }
        }
    }
}
