using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources.ForumManagement.DomainLayer
{
    public class UsersLoadPolicy:Policy
    {
        private int maxNumOfUsers;

        public UsersLoadPolicy() : base()
        {

        }

        public UsersLoadPolicy(Policies type, int maxNumOfUsers) : base(type)
        {
            this.maxNumOfUsers = maxNumOfUsers;
        }

        public int MaxNumOfUsers
        {
            get
            {
                return maxNumOfUsers;
            }

            set
            {
                maxNumOfUsers = value;
            }
        }
    }
}
