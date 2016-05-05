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
    class ModeratorDeletePermissionPolicy : Policy
    {
   
        private Boolean moderatorDeletePermission;

        public ModeratorDeletePermissionPolicy(Policies type, Boolean moderatorDeletePermission) : base(type)
        {
            this.moderatorDeletePermission = moderatorDeletePermission;
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
