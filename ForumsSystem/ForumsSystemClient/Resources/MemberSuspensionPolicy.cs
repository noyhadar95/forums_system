using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources
{
    public class MemberSuspensionPolicy :Policy
    {
        private int numOfComplaints;
        //TODO: maybe add more things
        public MemberSuspensionPolicy(Policies type, int numOfComplaints) : base(type)
        {
            this.numOfComplaints = numOfComplaints;
        }
    }
}
