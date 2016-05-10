using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    [Serializable]

    public class MaxModeratorsPolicy:Policy
    {
        [DataMember]
        private int maxModerators;

        public MaxModeratorsPolicy(Policies type, int maxModerators):base(type)
        {
            dal_policyParameter.CreatePolicyParameter(ID, -1, -1, -1, false, maxModerators, -1, -1, -1, -1, -1,false);
            this.maxModerators = maxModerators;
        }
        private MaxModeratorsPolicy() : base()
        {

        }

        public static MaxModeratorsPolicy createMaxModeratorsPolicyForInit(int maxModerators)
        {
            MaxModeratorsPolicy policy = new MaxModeratorsPolicy();
            policy.maxModerators = maxModerators;

            return policy;
        }
        /// <summary>
        /// check if a there is a free slot for a moderator 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public override bool CheckPolicy(PolicyParametersObject param)
        {
            if (param.GetPolicy() == type)
            {

                Console.WriteLine(maxModerators+" "+param.NumOfModerators);
                return this.maxModerators > param.NumOfModerators;
            }
            else
                return base.CheckPolicy(param);

        }
    }
}
