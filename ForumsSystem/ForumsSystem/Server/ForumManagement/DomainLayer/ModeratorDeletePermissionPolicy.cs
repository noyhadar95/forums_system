using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{

    /// <summary>
    /// check if a moderator's has permission to delete message in sub forum
    /// </summary>
    [Serializable]
    class ModeratorDeletePermissionPolicy : Policy
    {
   
        private bool moderatorDeletePermission;

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

    }
}
