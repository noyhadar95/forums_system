﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    /// <summary>
    /// check if a user is old enough to register to the forum
    /// </summary>
    class MinimumAgePolicy :Policy
    {
        private int minAge;

        public MinimumAgePolicy(Policies type, int minAge) : base(type)
        {
            this.minAge = minAge;
        }

        public override bool checkPolicy(PolicyParametersObject param)
        {
            if (param.getPolicy() == type)
            {
                return param.getAgeOfUser() >= this.minAge;
            }
            else
                return base.checkPolicy(param);

        }
    }
}
