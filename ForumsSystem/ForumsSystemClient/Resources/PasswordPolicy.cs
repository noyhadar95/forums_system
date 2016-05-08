using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources
{
    public class PasswordPolicy :Policy
    {
        private int requiredLength;
        private int passwordValidity;

        public PasswordPolicy() : base()
        {

        }

        public PasswordPolicy(Policies type, int requiredLength, int passwordValidity) : base(type)
        {
            this.requiredLength = requiredLength;
            this.passwordValidity = passwordValidity;
        }
    }
}
