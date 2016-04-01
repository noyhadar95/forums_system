using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    class ConfidentialityPolicy: Policy
    {
        private bool blockPassword;
        
        public ConfidentialityPolicy(bool blockPassword)
        {
            this.blockPassword = blockPassword;
        } 
        public override bool checkPolicy(PolicyParametersObject param)
        {
            if (param.getPolicy() == type)
            {
                return this.blockPassword;
            }
            else
                return base.checkPolicy(param);

        }
    }
}
