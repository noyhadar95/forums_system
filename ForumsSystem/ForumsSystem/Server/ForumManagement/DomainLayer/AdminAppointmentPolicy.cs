using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    class AdminAppointmentPolicy:Policy
    {
        private int seniority;
        private int numOfMessages;
        private int numOfComplaints;

        public AdminAppointmentPolicy(int seniority, int numOfMessages, int numOfComplaints)
        {
            this.seniority = seniority;
            this.numOfMessages = numOfMessages;
            this.numOfComplaints = numOfComplaints;
        }

        public override bool checkPolicy(PolicyParametersObject param)
        {
            if (param.getPolicy() == type)
            {
                return true; //TODO: check if the member given in the param can be promoted to an admin
            }
            else
                return base.checkPolicy(param);

        }
    }
}
