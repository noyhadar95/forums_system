using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources
{
    public class ModeratorSuspensionPolicy :Policy
    {
        private int numOfComplaints;

        public ModeratorSuspensionPolicy() : base()
        {

        }

        public ModeratorSuspensionPolicy(Policies type, int numOfComplaints) : base(type)
        {
            this.numOfComplaints = numOfComplaints;
        }
    }
}
