using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    abstract class Policy
    {
        protected Policies type;
        private Policy nextPolicy;

        public Policy(Policies type)
        {
            this.type = type;
        }
        /// <summary>
        /// Check the params given according to the forum policies
        /// </summary>
        public virtual bool checkPolicy(PolicyParametersObject param)
        {
            if(nextPolicy!=null)
               return nextPolicy.checkPolicy(param);
            return true;//TODO: check this
        }
        

        /// <summary>
        /// Add new policy to the end of the chain
        /// </summary>
        /// <param name="newPolicy"></param>
        public void addPolicy(Policy newPolicy)
        {
            if (nextPolicy != null)
                nextPolicy.addPolicy(newPolicy);
            else
                newPolicy = newPolicy;
        }
    }
}
