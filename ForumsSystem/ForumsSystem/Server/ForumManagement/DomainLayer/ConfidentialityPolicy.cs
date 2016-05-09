using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    public class ConfidentialityPolicy : Policy
    {
        [DataMember]
        private bool blockPassword;
        
        public ConfidentialityPolicy(Policies type, bool blockPassword):base(type)
        {
            dal_policyParameter.CreatePolicyParameter(ID, -1, -1, -1, blockPassword, -1, -1, -1, -1, -1);
            this.blockPassword = blockPassword;
        } 
        public override bool CheckPolicy(PolicyParametersObject param)
        {
            if (param.GetPolicy() == type)
            {
                return this.blockPassword;
            }
            else
                return base.CheckPolicy(param);

        }
    }
}
