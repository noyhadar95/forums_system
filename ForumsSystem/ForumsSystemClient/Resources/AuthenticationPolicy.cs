using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources
{
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
