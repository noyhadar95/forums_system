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
    public class ModeratorSuspensionPolicy : Policy
    {
        [DataMember]
        private int numOfComplaints;
        //TODO: maybe add more things

        public ModeratorSuspensionPolicy(Policies type, int numOfComplaints) : base(type)
        {
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
                int t =
                 user.NumOfComplaints;
                return t <= numOfComplaints;
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
            dal_policyParameter.CreatePolicyParameter(ID, -1, -1, numOfComplaints, false, -1, -1, -1, -1, -1, -1, false,-1);

        }
    }
}
