using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ForumsSystemClient.Resources.ForumManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    [Serializable]

    public class ModeratorSuspensionPolicy :Policy
    {
        [DataMember]
        private int numOfComplaints;

        public ModeratorSuspensionPolicy() : base()
        {

        }

        public ModeratorSuspensionPolicy(Policies type, int numOfComplaints) : base(type)
        {
            this.numOfComplaints = numOfComplaints;
        }

        public int NumOfComplaints
        {
            get
            {
                return numOfComplaints;
            }

            set
            {
                numOfComplaints = value;
            }
        }
    }
}
