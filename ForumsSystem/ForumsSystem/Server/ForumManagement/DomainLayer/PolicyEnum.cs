﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    enum Policies
    {
        password=0,
        Authentication = 1,
        ModeratorSuspension=2,
            Confidentiality=3,
            ModeratorAppointment=4,
            AdminAppointment=5,
            MemberSuspension=6,
            UsersLoad=7,
            MinimumAge=8
    }
}
