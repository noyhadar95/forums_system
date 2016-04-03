using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    /// <summary>
    /// check if a member should be suspended
    /// </summary>
    class MemberSuspensionPolicy:Policy
    {
        private int numOfComplaints;

        public MemberSuspensionPolicy(Policies type, int numOfComplaints):base(type)
        {
            this.numOfComplaints = numOfComplaints;
        }

        public override bool CheckPolicy(PolicyParametersObject param)
        {

            if (param.getPolicy() == type)
            {
                return true; //TODO: check if the member given in the param should be suspended
            }
            else
                return base.CheckPolicy(param);

        }
    }
}
