using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    public class AuthenticationPolicy: Policy
    {

        public AuthenticationPolicy(Policies type):base(type)
        {

        }
        private AuthenticationPolicy() : base()
        {

        }

        public static AuthenticationPolicy createAuthenticationPolicyForInit()
        {
            AuthenticationPolicy policy = new AuthenticationPolicy();
           

            return policy;
        }
        public override bool CheckPolicy(PolicyParametersObject param)
        {
            if (param.GetPolicy() == type)
            {
                return true;//TODO: check this
            }
            else
                return base.CheckPolicy(param);

        }

    }
}
