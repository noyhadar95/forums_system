using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    /// <summary>
    /// check if a moderator's has permission to delete message in sub forum
    /// </summary>
    /// 
    public class ModeratorDeletePermissionPolicy : Policy
    {

        [DataMember]
        public bool moderatorDeletePermission { get; set; }

        public ModeratorDeletePermissionPolicy(Policies type, bool moderatorDeletePermission) : base(type)
        {
            this.moderatorDeletePermission = moderatorDeletePermission;
        }

        private ModeratorDeletePermissionPolicy() : base()
        {

        }

        public static ModeratorDeletePermissionPolicy createmoderatorDeletePermissionForInit(bool moderatorDeletePermission)
        {
            ModeratorDeletePermissionPolicy policy = new ModeratorDeletePermissionPolicy();
            policy.moderatorDeletePermission = moderatorDeletePermission;

            return policy;
        }

        public override bool CheckPolicy(PolicyParametersObject param)
        {


            if (param.GetPolicy() == type)
            {
                return moderatorDeletePermission;
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
            dal_policyParameter.CreatePolicyParameter(ID, -1, -1, -1, false, -1, -1, -1, -1, -1, -1, moderatorDeletePermission, -1);
        }
    }
}
