using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    /// <summary>
    /// check if a user's has enough seniority to be the moderator of the forum
    /// </summary>
    [DataContract(IsReference = true)]
    [Serializable]
    public class ModeratorSeniorityPolicy : Policy
    {
        [DataMember]
        private int minSeniority;

        public ModeratorSeniorityPolicy(Policies type, int minSeniority) : base(type)
        {
            this.minSeniority = minSeniority;
        }
        private ModeratorSeniorityPolicy() : base()
        {

        }

        public static ModeratorSeniorityPolicy createModeratorSeniorityPolicyForInit(int minSeniority)
        {
            ModeratorSeniorityPolicy policy = new ModeratorSeniorityPolicy();
            policy.minSeniority = minSeniority;

            return policy;
        }
        public override bool CheckPolicy(PolicyParametersObject param)
        {


            if (param.GetPolicy() == type)
            {
                return param.GetModeratorSeniority() >= this.minSeniority;
            }
            else
                return base.CheckPolicy(param);

        }
    }
}
