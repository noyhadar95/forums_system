﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.UserManagement.DomainLayer;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    public class ModeratorAppointmentPolicy: Policy
    {
        private int seniorityInDays;
        private int numOfMessages;
        private int numOfComplaints;

        public ModeratorAppointmentPolicy(Policies type, int seniority, int numOfMessages, int numOfComplaints):base(type)
        {
            this.seniorityInDays = seniority;
            this.numOfMessages = numOfMessages;
            this.numOfComplaints = numOfComplaints;
            //TODO: maybe add more things
        }

        public override bool CheckPolicy(PolicyParametersObject param)
        {
            if (param.getPolicy() == type)
            {
                User user = (User)param.User;
                int seniorityOfUser = (int)((DateTime.Today - user.DateJoined).TotalDays);
                return (seniorityOfUser >= seniorityInDays)
                    && (user.NumOfMessages >= numOfMessages)
                    && (user.NumOfComplaints <= numOfComplaints);
            }
            else
                return base.CheckPolicy(param);

        }
    }
}
