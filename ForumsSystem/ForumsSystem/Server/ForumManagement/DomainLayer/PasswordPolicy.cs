using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    class PasswordPolicy : Policy
    {
        private int requiredLength;
        public override bool checkPolicy(PolicyParametersObject param)
        {
            if (param.getPolicy() == type)
            {
                return checkLength(param.getPassword());
            }
            else
                return base.checkPolicy(param);

        }

        private bool checkLength(string pass)
        {
            return pass.Length >= requiredLength;
        }
    }
}
