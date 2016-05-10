using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources.ForumManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    [Serializable]

    public class AuthenticationPolicy :Policy
    {
        public AuthenticationPolicy():base()
        {

        }

        public AuthenticationPolicy(Policies type) : base(type)
        {

        }
    }
}
