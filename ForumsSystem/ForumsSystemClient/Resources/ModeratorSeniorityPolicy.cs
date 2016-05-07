using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources
{
    public class ModeratorSeniorityPolicy :Policy
    {
        private int minSeniority;

        public ModeratorSeniorityPolicy(Policies type, int minSeniority) : base(type)
        {
            this.minSeniority = minSeniority;
        }
    }
}
