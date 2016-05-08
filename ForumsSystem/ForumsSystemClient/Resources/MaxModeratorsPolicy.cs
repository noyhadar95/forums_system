using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources
{
    public class MaxModeratorsPolicy :Policy
    {
        private int maxModerators;

        public MaxModeratorsPolicy():base()
        {

        }

        public MaxModeratorsPolicy(Policies type, int maxModerators) : base(type)
        {
            this.maxModerators = maxModerators;
        }
    }
}
