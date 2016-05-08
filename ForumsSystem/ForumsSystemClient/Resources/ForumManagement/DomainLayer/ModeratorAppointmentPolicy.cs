using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources.ForumManagement.DomainLayer
{
    public class ModeratorAppointmentPolicy : Policy
    {
        private int seniorityInDays;
        private int numOfMessages;
        private int numOfComplaints;

        public int SeniorityInDays
        {
            get
            {
                return seniorityInDays;
            }

            set
            {
                seniorityInDays = value;
            }
        }

        public int NumOfMessages
        {
            get
            {
                return numOfMessages;
            }

            set
            {
                numOfMessages = value;
            }
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

        public ModeratorAppointmentPolicy() : base()
        {

        }

        public ModeratorAppointmentPolicy(Policies type, int seniority, int numOfMessages, int numOfComplaints) : base(type)
        {
            this.seniorityInDays = seniority;
            this.numOfMessages = numOfMessages;
            this.numOfComplaints = numOfComplaints;

        }
    }
}
