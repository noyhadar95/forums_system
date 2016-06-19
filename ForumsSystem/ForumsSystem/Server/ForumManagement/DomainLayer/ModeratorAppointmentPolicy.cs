using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using ForumsSystem.Server.UserManagement.DomainLayer;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{

    [DataContract(IsReference = true)]
    public class ModeratorAppointmentPolicy: Policy
    {
        [DataMember]
        private int seniorityInDays;
        [DataMember]
        private int numOfMessages;
        [DataMember]
        private int numOfComplaints;

        public ModeratorAppointmentPolicy(Policies type, int seniority, int numOfMessages, int numOfComplaints):base(type)
        {
            this.seniorityInDays = seniority;
            this.numOfMessages = numOfMessages;
            this.numOfComplaints = numOfComplaints;
            //TODO: maybe add more things
        }
        private ModeratorAppointmentPolicy() : base()
        {

        }

        public static ModeratorAppointmentPolicy createModeratorAppointmentPolicyForInit(int seniorityInDays, int numOfMessages, int numOfComplaints)
        {
            ModeratorAppointmentPolicy policy = new ModeratorAppointmentPolicy();
            policy.seniorityInDays = seniorityInDays;
            policy.numOfMessages = numOfMessages;
            policy.numOfComplaints = numOfComplaints;

            return policy;
        }

        public override bool CheckPolicy(PolicyParametersObject param)
        {
            if (param.GetPolicy() == type)
            {
                User user = (User)param.User;
                int seniorityOfUser = (int)((DateTime.Today - user.DateJoined).TotalDays);
                return (seniorityOfUser >= seniorityInDays)
                    && (user.NumOfMessages >= numOfMessages)
                    && (user.NumOfComplaints <= numOfComplaints);
            }
            else
                return base.CheckPolicy(param);

        }
        public override bool AddPolicy(Policy newPolicy)
        {
            bool flag = base.AddPolicy(newPolicy);
            if (flag)
                newPolicy.AddParamObject();
            return flag;
        }
        public override void AddParamObject()
        {
            dal_policyParameter = new Data_Access_Layer.DAL_PolicyParameter();
            dal_policyParameter.CreatePolicyParameter(ID, seniorityInDays, numOfMessages, numOfComplaints, false, -1, -1, -1, -1, -1, -1, false);

        }
    }
}
