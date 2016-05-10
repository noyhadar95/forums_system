using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    [DataContract]
    [Serializable]
    public enum Policies
    {
        [EnumMember]
        Password =0,
        [EnumMember]
        Authentication = 1,
        [EnumMember]
        ModeratorSuspension =2,
        [EnumMember]
        Confidentiality =3,
        [EnumMember]
        ModeratorAppointment =4,
        [EnumMember]
        AdminAppointment =5,
        [EnumMember]
        MemberSuspension =6,
        [EnumMember]
        UsersLoad =7,
        [EnumMember]
        MinimumAge =8,
        [EnumMember]
        MaxModerators =9,
        [EnumMember]
        ModeratorSeniority =10,
        [EnumMember]
        PasswordValidity =11,
        [EnumMember]
        ModeratorPermissionToDelete =12

    }
}
