using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.UserManagement.DomainLayer;
using System.Runtime.Serialization;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    public class AdminAppointmentPolicy:Policy
    {
        [DataMember]
        private int seniorityInDays;
        [DataMember]
        private int numOfMessages;
        [DataMember]
        private int numOfComplaints;

        public AdminAppointmentPolicy(Policies type,int seniorityInDays, int numOfMessages, int numOfComplaints): base(type)
        {
            this.seniorityInDays = seniorityInDays;
            this.numOfMessages = numOfMessages;
            this.numOfComplaints = numOfComplaints;
            //TODO: maybe add more things
        }

        public override bool CheckPolicy(PolicyParametersObject param)
        {
            if (param.GetPolicy() == type)
            {
                User user = (User)param.User;
                int seniorityOfUser = (int) ((DateTime.Today - user.DateJoined).TotalDays);
                return (seniorityOfUser>=seniorityInDays)
                    &&(user.NumOfMessages>=numOfMessages)
                    &&(user.NumOfComplaints<=numOfComplaints); 
            }
            else
                return base.CheckPolicy(param);

        }
    }
}
