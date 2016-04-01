﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    class ModeratorSuspensionPolicy: Policy
    {
        private int numOfComplaints;

        public ModeratorSuspensionPolicy(int numOfComplaints)
        {
            this.numOfComplaints = numOfComplaints;
        }
        public override bool checkPolicy(PolicyParametersObject param)
        {
            if (param.getPolicy() == type)
            {
                //TODO: check if the moderator has too many complaints and suspend him if needed

                return true; 
            }
            else
                return base.checkPolicy(param);

        }
    }
}
