using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources.ForumManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    public class UsersLoadPolicy:Policy
    {
        [DataMember]
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
