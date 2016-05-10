using ForumsSystemClient.Resources.ForumManagement.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources.UserManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(Post))]
    [KnownType(typeof(User))]
    [Serializable]

    public class Moderator
    {
        [DataMember]
        private User user;
        [DataMember]
        private DateTime expirationDate;
        [DataMember]
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
