using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources.ForumManagement.DomainLayer
{
    class ModeratorDeletePermissionPolicy : Policy
    {
        [DataMember]
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

        public bool ModeratorDeletePermission { get { return moderatorDeletePermission; } set { moderatorDeletePermission = value; } }

    }
}
