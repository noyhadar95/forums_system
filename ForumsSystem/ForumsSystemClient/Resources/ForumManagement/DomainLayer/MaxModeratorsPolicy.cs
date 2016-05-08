using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources.ForumManagement.DomainLayer
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

        public int MaxModerators
        {
            get
            {
                return maxModerators;
            }

            set
            {
                maxModerators = value;
            }
        }
    }
}
