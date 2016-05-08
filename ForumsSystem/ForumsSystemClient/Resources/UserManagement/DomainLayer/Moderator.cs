using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources.UserManagement.DomainLayer
{
    public class Moderator
    {
        private User user;
        private DateTime expirationDate;
        private User appointer;

        public User User
        {
            get
            {
                return user;
            }

            set
            {
                user = value;
            }
        }

        public DateTime ExpirationDate
        {
            get
            {
                return expirationDate;
            }

            set
            {
                expirationDate = value;
            }
        }

        public User Appointer
        {
            get
            {
                return appointer;
            }

            set
            {
                appointer = value;
            }
        }

        public Moderator()
        {

        }
    }
}
