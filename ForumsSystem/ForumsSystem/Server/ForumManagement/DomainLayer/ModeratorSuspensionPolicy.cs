using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.UserManagement.DomainLayer;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    public class ModeratorSuspensionPolicy: Policy
    {
        private int numOfComplaints;
        //TODO: maybe add more things

        public ModeratorSuspensionPolicy(Policies type, int numOfComplaints):base(type)
        {
            this.numOfComplaints = numOfComplaints;
        }
        public override bool CheckPolicy(PolicyParametersObject param)
        {
            if (param.GetPolicy() == type)
            {
                

                User user = (User)param.User;
                return user.NumOfComplaints > numOfComplaints;
            }
            else
                return base.CheckPolicy(param);

        }
    }
}
