using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    class AuthenticationPolicy: Policy
    {
        public AuthenticationPolicy(Policies type):base(type)
        {

        }
        public override bool CheckPolicy(PolicyParametersObject param)
        {
            if (param.getPolicy() == type)
            {
                return true;//TODO: check this
            }
            else
                return base.CheckPolicy(param);

        }

    }
}
