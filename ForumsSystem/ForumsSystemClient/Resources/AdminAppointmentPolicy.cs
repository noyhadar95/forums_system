using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources
{
    public class AdminAppointmentPolicy :Policy
    {
        private int seniorityInDays;
        private int numOfMessages;
        private int numOfComplaints;

        public AdminAppointmentPolicy(Policies type, int seniorityInDays, int numOfMessages, int numOfComplaints) : base(type)
        {
            this.seniorityInDays = seniorityInDays;
            this.numOfMessages = numOfMessages;
            this.numOfComplaints = numOfComplaints;
            
        }
    }
}
