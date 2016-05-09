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
    public class ModeratorSeniorityPolicy : Policy
    {
        [DataMember]
        private int minSeniority;

        public ModeratorSeniorityPolicy(Policies type, int minSeniority) : base(type)
        {
            this.minSeniority = minSeniority;
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
