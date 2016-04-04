using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.UserManagement.DomainLayer;

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

        public IUser User { get { return this.user; } set { this.user = value; } }

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
        public void increaseNumOfUsers()
        {
            this.numOfUsers++;
        }

        public int getAgeOfUser()
        {
            return ageOfUser;
        }
        public void iSetAgeOfUser(int ageOfUser)
        {
            this.ageOfUser = ageOfUser;
        }

    }
}
