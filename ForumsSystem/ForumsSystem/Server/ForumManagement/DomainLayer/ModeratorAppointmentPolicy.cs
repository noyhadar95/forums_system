using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    class ModeratorAppointmentPolicy: Policy
    {
        private int seniority;
        private int numOfMessages;
        private int numOfComplaints;

        public ModeratorAppointmentPolicy(Policies type, int seniority, int numOfMessages, int numOfComplaints):base(type)
        {
            this.seniority = seniority;
            this.numOfMessages = numOfMessages;
            this.numOfComplaints = numOfComplaints;
        }

        public override bool CheckPolicy(PolicyParametersObject param)
        {
            if (param.getPolicy() == type)
            {
                return true; //TODO: check if the member given in the param can be promoted to a moderator
            }
            else
                return base.CheckPolicy(param);

        }
    }
}
