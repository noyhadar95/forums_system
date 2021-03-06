﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.UserManagement.DomainLayer;
using System.Runtime.Serialization;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    public class PasswordPolicy : Policy
    {
        [DataMember]
        private int requiredLength;
        [DataMember]
        public int passwordValidity;//in days

        public PasswordPolicy(Policies type, int requiredLength, int passwordValidity) : base(type)
        {
            this.requiredLength = requiredLength;
            this.passwordValidity = passwordValidity;
        }

        private PasswordPolicy() : base()
        {

        }

        public static PasswordPolicy createPasswordPolicyForInit(int requiredLength, int passwordValidity)
        {
            PasswordPolicy policy = new PasswordPolicy();
            policy.requiredLength = requiredLength;
            policy.passwordValidity = passwordValidity;

            return policy;
        }

        public override bool CheckPolicy(PolicyParametersObject param)
        {
            if (param.GetPolicy() == type)
            {
                User user = (User)param.User;
                // int passwordDuration = (int)((DateTime.Today - user.GetDateOfPassLastChange()).TotalDays);

                return (checkLength(param.GetPassword())); //&& (passwordDuration <= passwordValidity));
            }
            else
                return base.CheckPolicy(param);

        }


        private bool checkLength(string pass)
        {
            return pass.Length >= requiredLength;
        }
        public override bool AddPolicy(Policy newPolicy)
        {
            bool flag = base.AddPolicy(newPolicy);
            if (flag)
                newPolicy.AddParamObject();
            return flag;
        }
        public override void AddParamObject()
        {
            dal_policyParameter = new Data_Access_Layer.DAL_PolicyParameter();
            dal_policyParameter.CreatePolicyParameter(ID, -1, -1, -1, false, -1, -1, requiredLength, passwordValidity, -1, -1, false, -1);

        }
    }
    }
