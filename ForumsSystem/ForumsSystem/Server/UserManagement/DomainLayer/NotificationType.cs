using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.UserManagement.DomainLayer
{
    [DataContract]
    [Serializable]

    public enum  NotificationType
    {
        [EnumMember]
        Posted = 0,
        [EnumMember]
        Changed = 1,
        [EnumMember]
        Deleted = 2
    }
}
