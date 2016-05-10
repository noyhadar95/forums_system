using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources.ForumManagement.DomainLayer
{ 
    [DataContract(IsReference = true)]
    [Serializable]

    public class MaxModeratorsPolicy :Policy
    {
    [DataMember]
    private int maxModerators;

        public MaxModeratorsPolicy():base()
        {

        }

        public MaxModeratorsPolicy(Policies type, int maxModerators) : base(type)
        {
            this.maxModerators = maxModerators;
        }

        public int MaxModerators
        {
            get
            {
                return maxModerators;
            }

            set
            {
                maxModerators = value;
            }
        }
    }
}
