using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources
{
    public class ConfidentialityPolicy :Policy
    {
        private bool blockPassword;

        public ConfidentialityPolicy(Policies type, bool blockPassword) : base(type)
        {
            this.blockPassword = blockPassword;
        }
    }
}
