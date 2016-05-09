using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    /// <summary>
    /// check if users can register to the forum
    /// </summary>
    [DataContract(IsReference = true)]
    public class UsersLoadPolicy :Policy
    {
        [DataMember]
        private int maxNumOfUsers;

        public UsersLoadPolicy(Policies type,int maxNumOfUsers): base(type)
        {
            dal_policyParameter.CreatePolicyParameter(ID, -1, -1, -1, false, -1, -1, -1, -1, maxNumOfUsers);
            this.maxNumOfUsers = maxNumOfUsers;
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
