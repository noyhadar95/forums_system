using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    public enum Policies
    {
        Password=0,
        Authentication = 1,
        ModeratorSuspension=2,
            Confidentiality=3,
            ModeratorAppointment=4,
            AdminAppointment=5,
            MemberSuspension=6,
            UsersLoad=7,
            MinimumAge=8,
        MaxModerators=9,
        ModeratorSeniority=10,
        PasswordValidity=11,
        ModeratorPermissionToDelete=12
    }
}
