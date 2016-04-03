using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    public class PasswordPolicy : Policy
    {
        private int requiredLength;

        public PasswordPolicy(Policies type, int requiredLength):base(type)
        {
            this.requiredLength = requiredLength;
        }
        public override bool CheckPolicy(PolicyParametersObject param)
        {
            if (param.getPolicy() == type)
            {
                return checkLength(param.getPassword());
            }
            else
                return base.CheckPolicy(param);

        }

        private bool checkLength(string pass)
        {
            return pass.Length >= requiredLength;
        }
    }
}
