using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.UserManagement.DomainLayer;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    /// <summary>
    /// check if a member should be suspended
    /// </summary>
    public class MemberSuspensionPolicy :Policy
    {
        private int numOfComplaints;
        //TODO: maybe add more things
        public MemberSuspensionPolicy(Policies type, int numOfComplaints):base(type)
        {
            this.numOfComplaints = numOfComplaints;
        }

        /// <summary>
        /// check if a user should be suspended
        /// </summary>
        /// <param name="param"></param>
        /// <returns>true if the user <b>should</b> be suspended</returns>
        public override bool CheckPolicy(PolicyParametersObject param)
        {

            if (param.GetPolicy() == type)
            {
                User user = (User)param.User;
                return user.NumOfComplaints>numOfComplaints; 
            }
            else
                return base.CheckPolicy(param);

        }
    }
}
