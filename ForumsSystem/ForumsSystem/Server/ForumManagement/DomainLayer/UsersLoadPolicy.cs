using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    /// <summary>
    /// check if users can register to the forum
    /// </summary>
    class UsersLoadPolicy:Policy
    {
        private int maxNumOfUsers;

        public UsersLoadPolicy(Policies type,int maxNumOfUsers): base(type)
        {
            this.maxNumOfUsers = maxNumOfUsers;
        }

        public override bool CheckPolicy(PolicyParametersObject param)
        {
            if (param.getPolicy() == type)
            {
                return param.getNumOfUsers()<this.maxNumOfUsers; 
            }
            else
                return base.CheckPolicy(param);

        }
    }
}
