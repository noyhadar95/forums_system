using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Threading.Tasks;


namespace ForumsSystemClient.Resources.ForumManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    public class MinimumAgePolicy : Policy
    {
        [DataMember]
        private int minAge;

        public MinimumAgePolicy() : base()
        {

        }

        public MinimumAgePolicy(Policies type, int minAge) : base(type)
        {
            this.MinAge = minAge;
        }

        public int MinAge
        {
            get
            {
                return minAge;
            }

            set
            {
                minAge = value;
            }
        }
    }
}
