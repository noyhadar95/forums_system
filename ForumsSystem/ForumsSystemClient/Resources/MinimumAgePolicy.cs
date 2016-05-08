using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources
{
    public class MinimumAgePolicy : Policy
    {
        private int minAge;

        public MinimumAgePolicy() : base()
        {

        }

        public MinimumAgePolicy(Policies type, int minAge) : base(type)
        {
            this.minAge = minAge;
        }
    }
}
