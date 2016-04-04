using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private Policies policy;
        public Policies getPolicy()
        {
            return policy;
        }

        public void setPolicy(Policies policy)
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


        public string getPassword()
        {
            return password;
        }
        public void setPassword(string newPassword)
        {
            password = newPassword;
        }

        public int getNumOfUsers()
        {
            return numOfUsers;
        }
        public void SetNumOfUsers(int numOfUsers)
        {
            this.numOfUsers = numOfUsers;
        }

        public int getAgeOfUser()
        {
            return ageOfUser;
        }
        public void SetAgeOfUser(int ageOfUser)
        {
            this.ageOfUser = ageOfUser;
        }

    }
}
