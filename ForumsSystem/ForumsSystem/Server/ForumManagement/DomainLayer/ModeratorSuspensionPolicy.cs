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
    public class ModeratorSuspensionPolicy: Policy
    {
        [DataMember]
        private int numOfComplaints;
        //TODO: maybe add more things

        public ModeratorSuspensionPolicy(Policies type, int numOfComplaints):base(type)
        {
            dal_policyParameter.CreatePolicyParameter(ID, -1, -1,numOfComplaints, false, -1, -1, -1, -1, -1);
            this.numOfComplaints = numOfComplaints;
        }
        private ModeratorSuspensionPolicy() : base()
        {

        }

        public static ModeratorSuspensionPolicy createModeratorSuspensionPolicyForInit(int numOfComplaints)
        {
            ModeratorSuspensionPolicy policy = new ModeratorSuspensionPolicy();
            policy.numOfComplaints = numOfComplaints;

            return policy;
        }
        public override bool CheckPolicy(PolicyParametersObject param)
        {
            if (param.GetPolicy() == type)
            {
                

                User user = (User)param.User;
                return user.NumOfComplaints > numOfComplaints;
            }
            else
                return base.CheckPolicy(param);

        }
    }
}
