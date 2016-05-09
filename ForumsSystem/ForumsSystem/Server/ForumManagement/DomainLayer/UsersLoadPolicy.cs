﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    /// <summary>
    /// check if users can register to the forum
    /// </summary>
    public class UsersLoadPolicy :Policy
    {
        private int maxNumOfUsers;

        public UsersLoadPolicy(Policies type,int maxNumOfUsers): base(type)
        {
            this.maxNumOfUsers = maxNumOfUsers;
        }
        private UsersLoadPolicy() : base()
        {

        }

        public static UsersLoadPolicy createUsersLoadPolicyForInit(int maxNumOfUsers)
        {
            UsersLoadPolicy policy = new UsersLoadPolicy();
            policy.maxNumOfUsers = maxNumOfUsers;

            return policy;
        }

        public override bool CheckPolicy(PolicyParametersObject param)
        {
            if (param.GetPolicy() == type)
            {
                return param.GetNumOfUsers()<this.maxNumOfUsers; 
            }
            else
                return base.CheckPolicy(param);

        }
    }
}
