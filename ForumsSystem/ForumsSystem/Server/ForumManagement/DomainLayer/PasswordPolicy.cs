using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.UserManagement.DomainLayer;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    public class PasswordPolicy : Policy
    {
        private int requiredLength;
        private int passwordValidity;

        public PasswordPolicy(Policies type, int requiredLength, int passwordValidity) :base(type)
        {
            this.requiredLength = requiredLength;
            this.passwordValidity = passwordValidity;
        }
        public override bool CheckPolicy(PolicyParametersObject param)
        {
            if (param.GetPolicy() == type)
            {
                User user = (User)param.User;
               // int passwordDuration = (int)((DateTime.Today - user.GetDateOfPassLastChange()).TotalDays);

                return (checkLength(param.GetPassword())); //&& (passwordDuration <= passwordValidity));
            }
            else
                return base.CheckPolicy(param);

        }

        private bool checkLength(string pass)
        {
            return pass.Length >= requiredLength;
        }
    }
}
