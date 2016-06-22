using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    /// <summary>
    /// check if users can register to the forum
    /// </summary>
    [DataContract(IsReference = true)]
    public class UsersLoadPolicy :Policy
    {
        [DataMember]
        private int maxNumOfUsers;

        public UsersLoadPolicy(Policies type,int maxNumOfUsers): base(type)
        {
            this.maxNumOfUsers = maxNumOfUsers;
        }
        private UsersLoadPolicy() : base()
        {

        }

        public static UsersLoadPolicy createUsersLoadPolicyForInit(int maxNumOfUsers)
        {
            UsersLoadPolicy policy = new UsersLoadPolicy();
            policy.maxNumOfUsers = maxNumOfUsers;

            return policy;
        }

        public override bool CheckPolicy(PolicyParametersObject param)
        {
            if (param.GetPolicy() == type)
            {
                return param.GetNumOfUsers()<this.maxNumOfUsers; 
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
            dal_policyParameter.CreatePolicyParameter(ID, -1, -1, -1, false, -1, -1, -1, -1,-1, maxNumOfUsers,  false, -1);

        }
    }
}
