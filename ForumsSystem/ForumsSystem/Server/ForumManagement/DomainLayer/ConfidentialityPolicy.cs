using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    public class ConfidentialityPolicy : Policy
    {
        private bool blockPassword;
        
        public ConfidentialityPolicy(Policies type, bool blockPassword):base(type)
        {
            this.blockPassword = blockPassword;
        }
        private ConfidentialityPolicy() : base()
        {

        }

        public static ConfidentialityPolicy createConfidentialityPolicyForInit(bool blockPassword)
        {
            ConfidentialityPolicy policy = new ConfidentialityPolicy();
            policy.blockPassword = blockPassword;

            return policy;
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
