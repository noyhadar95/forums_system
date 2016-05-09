using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ForumsSystemClient.Resources.ForumManagement.DomainLayer
{

    [DataContract(IsReference = true)]
    public class PasswordPolicy :Policy
    {
        [DataMember]
        private int requiredLength;
        [DataMember]
        private int passwordValidity;

        public int RequiredLength
        {
            get
            {
                return requiredLength;
            }

            set
            {
                requiredLength = value;
            }
        }

        public int PasswordValidity
        {
            get
            {
                return passwordValidity;
            }

            set
            {
                passwordValidity = value;
            }
        }

        public PasswordPolicy() : base()
        {

        }

        public PasswordPolicy(Policies type, int requiredLength, int passwordValidity) : base(type)
        {
            this.requiredLength = requiredLength;
            this.passwordValidity = passwordValidity;
        }
    }
}
