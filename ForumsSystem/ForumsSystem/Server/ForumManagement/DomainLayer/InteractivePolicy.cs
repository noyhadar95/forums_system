using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    public class InteractivePolicy : Policy
    {
        
        [DataMember]
        public int notifyMode;
        /// <summary>
        /// 0 - online only
        /// 1 - offline and online
        /// 2 - selective
        /// </summary>
        /// <param name="type"></param>
        /// <param name="notifyOffline"></param>

        public InteractivePolicy(Policies type, int notifyMode) : base(type)
        {
            this.notifyMode = notifyMode;
        }
        private InteractivePolicy() : base()
        {

        }

        public static InteractivePolicy createInteractivePolicyForInit(int notifyMode)
        {
            InteractivePolicy policy = new InteractivePolicy();
            policy.notifyMode = notifyMode;

            return policy;
        }

        /// <summary>
        /// returns true if policy is selective
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public override bool CheckPolicy(PolicyParametersObject param)
        {
            if (param.GetPolicy() == type)
            {
                return notifyMode==2;
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
            dal_policyParameter.CreatePolicyParameter(ID, -1, -1, -1, false, -1, -1, -1, -1, -1, -1, false,notifyMode);

        }
    }
}
