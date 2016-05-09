using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources.ForumManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    public class MemberSuspensionPolicy :Policy
    {
        [DataMember]
        private int numOfComplaints;
        //TODO: maybe add more things

        public MemberSuspensionPolicy():base()
        {

        }

        public MemberSuspensionPolicy(Policies type, int numOfComplaints) : base(type)
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
