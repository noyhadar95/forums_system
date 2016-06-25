using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    public class MaxModeratorsPolicy:Policy
    {
        [DataMember]
        public int maxModerators;

        public MaxModeratorsPolicy(Policies type, int maxModerators):base(type)
        {
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

                //Console.WriteLine(maxModerators+" "+param.NumOfModerators);
                return this.maxModerators >= param.CurrNumOfModerators+ param.NumOfModeratorsToAdd;
            }
            else
                return base.CheckPolicy(param);

        }
        public override bool AddPolicy(Policy newPolicy)
        {
            bool flag = base.AddPolicy(newPolicy);
            if (flag)
                newPolicy.AddParamObject();
            return flag;
        }
        public override void AddParamObject()
        {
            dal_policyParameter = new Data_Access_Layer.DAL_PolicyParameter();
            dal_policyParameter.CreatePolicyParameter(ID, -1, -1, -1, false, maxModerators, -1, -1, -1, -1, -1, false, -1);

        }
    }
}
