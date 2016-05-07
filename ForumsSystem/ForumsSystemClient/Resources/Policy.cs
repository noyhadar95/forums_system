﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources
{
    public class Policy
    {
       /* public enum Policies
        {
            Password = 0,
            Authentication = 1,
            ModeratorSuspension = 2,
            Confidentiality = 3,
            ModeratorAppointment = 4,
            AdminAppointment = 5,
            MemberSuspension = 6,
            UsersLoad = 7,
            MinimumAge = 8,
            MaxModerators = 9
        }
        */

        protected Policies type;
        private Policy nextPolicy;

        public Policies Type { get { return type; } set { this.type = value; } }
        public Policy NextPolicy { get { return nextPolicy; } set { this.nextPolicy = value; } }

        public Policy(Policies type)
        {
            this.type = type;
            this.nextPolicy = null;
        }
    }
}
