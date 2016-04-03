using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    public class ModeratorSuspensionPolicy: Policy
    {
        private int numOfComplaints;

        public ModeratorSuspensionPolicy(Policies type, int numOfComplaints):base(type)
        {
            this.numOfComplaints = numOfComplaints;
        }
        public override bool CheckPolicy(PolicyParametersObject param)
        {
            if (param.getPolicy() == type)
            {
                //TODO: check if the moderator has too many complaints and suspend him if needed

                return true; 
            }
            else
                return base.CheckPolicy(param);

        }
    }
}
