﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.UserManagement.DomainLayer;
using ForumsSystem.Server.ForumManagement.Data_Access_Layer;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    /// <summary>
    /// this class is the parameter for policy checks.
    /// You should initialize the parameters needed for the policy check.
    /// </summary>
    public class PolicyParametersObject
    {
        public PolicyParametersObject(Policies policy)
        {
            this.policy = policy;
        }

        DAL_PolicyParameter dal_policyParamete = new DAL_PolicyParameter();
        private Policies policy;
        public Policies GetPolicy()
        {
            return policy;
        }

        public void SetPolicy(Policies policy)
        {
            this.policy = policy;
        }
      /*
      public void setPolicy(Policies newPolicy)
        {
            policy = newPolicy;
        }
        */


        private string password;

        private int numOfUsers;

        private int ageOfUser;

        private IUser user;

        private int currNumOfModerators;

        private int numOfModeratorsToAdd;

        private int moderatorSeniority;

        private int passwordValidity;

        private Boolean moderatorDeletePermission; 

        public IUser User { get { return this.user; } set { this.user = value; } }
        public int CurrNumOfModerators { get { return this.currNumOfModerators; } set { this.currNumOfModerators = value; } }
        public int NumOfModeratorsToAdd { get { return this.numOfModeratorsToAdd; } set { this.numOfModeratorsToAdd = value; } }

        public string GetPassword()
        {
            return password;
        }
        public void SetPassword(string newPassword)
        {
            password = newPassword;

        }

        public int GetNumOfUsers()
        {
            return numOfUsers;
        }
        public void SetNumOfUsers(int numOfUsers)
        {
            this.numOfUsers = numOfUsers;
        }

        public int GetAgeOfUser()
        {
            return ageOfUser;
        }
        public void SetAgeOfUser(int ageOfUser)
        {
            this.ageOfUser = ageOfUser;
        }

        public int GetModeratorSeniority()
        {

            return moderatorSeniority;

        }

        public void SetModeratorSeniority(int moderatorSeniority)
        {

            this.moderatorSeniority = moderatorSeniority;

        }

        

             public int GetPasswordValidity()
        {

            return passwordValidity;

        }

        public void SetPasswordValidity(int passwordValidity)
        {

            this.passwordValidity = passwordValidity;

        }

        public Boolean GetModeratorDeletePermission()
        {
            return this.moderatorDeletePermission;
        }


        public void SetModeratorDeletePermission(Boolean moderatorDeletePermission)
        {

            this.moderatorDeletePermission = moderatorDeletePermission;

        }
    }
}
