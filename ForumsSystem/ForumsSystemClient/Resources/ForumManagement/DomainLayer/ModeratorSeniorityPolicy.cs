using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources.ForumManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    [Serializable]

    public class ModeratorSeniorityPolicy :Policy
    {
        [DataMember]
        private int minSeniority;

        public ModeratorSeniorityPolicy() : base()
        {

        }

        public ModeratorSeniorityPolicy(Policies type, int minSeniority) : base(type)
        {
            this.minSeniority = minSeniority;
        }

        public int MinSeniority
        {
            get
            {
                return minSeniority;
            }

            set
            {
                minSeniority = value;
            }
        }
    }
}
