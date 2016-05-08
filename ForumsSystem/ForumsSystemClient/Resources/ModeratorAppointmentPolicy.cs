using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources
{
    public class ModeratorAppointmentPolicy :Policy
    {
        private int seniorityInDays;
        private int numOfMessages;
        private int numOfComplaints;

        public ModeratorAppointmentPolicy(Policies type, int seniority, int numOfMessages, int numOfComplaints) : base(type)
        {
            this.seniorityInDays = seniority;
            this.numOfMessages = numOfMessages;
            this.numOfComplaints = numOfComplaints;
            
        }
    }
}
