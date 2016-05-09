using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    /// <summary>
    /// check if a user is old enough to register to the forum
    /// </summary>
    public class MinimumAgePolicy :Policy
    {
        private int minAge;

        public MinimumAgePolicy(Policies type, int minAge) : base(type)
        {
            this.minAge = minAge;
        }
        private MinimumAgePolicy() : base()
        {

        }

        public static MinimumAgePolicy createMinimumAgePolicyForInit(int minAge)
        {
            MinimumAgePolicy policy = new MinimumAgePolicy();
            policy.minAge = minAge;
          

            return policy;
        }
        public override bool CheckPolicy(PolicyParametersObject param)
        {


            if (param.GetPolicy() == type)
            {
                return param.GetAgeOfUser() >= this.minAge;
            }
            else
                return base.CheckPolicy(param);

        }
    }
}
