using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources.UserManagement.DomainLayer
{
    [DataContract]

    public enum NotificationType
    {
        [EnumMember]
        Posted = 0,
        [EnumMember]
        Changed = 1,
        [EnumMember]
        Deleted = 2
    }
}
