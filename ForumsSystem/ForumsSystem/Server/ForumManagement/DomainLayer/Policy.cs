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
            this.nextPolicy = null;
        }

        public Policies Type { get { return type; } set { this.type = value; } }
        public Policy NextPolicy { get { return nextPolicy; } set { this.nextPolicy = value; } }
        /// <summary>
        /// Check the params given according to the forum policies
        /// </summary>
        public virtual bool CheckPolicy(PolicyParametersObject param)
        {
            if(nextPolicy!=null)
               return nextPolicy.CheckPolicy(param);
            return true;//TODO: check this
        }
        

        /// <summary>
        /// Add new policy to the end of the chain
        /// </summary>
        /// <param name="newPolicy"></param>
        public bool AddPolicy(Policy newPolicy)
        {
            if (CheckIfPolicyExists(newPolicy.type))
                return false;
            AddPolicyHelper(newPolicy);
            return true;
        }

        private void AddPolicyHelper(Policy newPolicy)
        {
            if (nextPolicy != null)
                nextPolicy.AddPolicyHelper(newPolicy);
            else
                this.nextPolicy = newPolicy;
        }

        private bool CheckIfPolicyExists(Policies type)
        {
            if (this.type == type)
                return true;
            if (this.nextPolicy == null)
                return false;
            return this.nextPolicy.CheckIfPolicyExists(type);
        }
        /// <summary>
        /// Removes the policy of the given type from the chain
        /// </summary>
        /// <param name="type"></param>
        /// <returns>the new head of the chain. returns NULL if the new chain is empty</returns>
        public Policy RemovePolicy(Policies type)
        {
            if (this.type == type)//if the first node is the one to be removed
                return this.nextPolicy;
            RemovePolicyHelper(type);
            return this;
        }

        private void RemovePolicyHelper(Policies type)
        {
            if (this.NextPolicy.Type == type)
            {
                Policy temp = this.NextPolicy.NextPolicy;
                this.NextPolicy = temp;
                //return true;
            }
            if (nextPolicy == null)
                //return false;

                //return 
                this.nextPolicy.RemovePolicyHelper(type);
        }
    }
}
