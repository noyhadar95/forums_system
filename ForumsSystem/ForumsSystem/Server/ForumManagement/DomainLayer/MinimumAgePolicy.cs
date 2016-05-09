using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    /// <summary>
    /// check if a user is old enough to register to the forum
    /// </summary>
    
    [DataContract(IsReference = true)]
    [Serializable]
    [KnownType(typeof(MinimumAgePolicy))]
    public class MinimumAgePolicy :Policy
    {
        [DataMember]
        public int minAge { get; set; }

        public MinimumAgePolicy(Policies type, int minAge) : base(type)
        {
            dal_policyParameter.CreatePolicyParameter(ID, -1, -1, -1, false, -1, minAge, -1, -1, -1, -1, false);
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
