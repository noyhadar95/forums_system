using ForumsSystem.Server.UserManagement.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    public class Moderator
    {
        [DataMember]
        public IUser user { get; private set; }
        [DataMember]
        public DateTime expirationDate { get; private set; }
        [DataMember]
        public IUser appointer { get; private set; }

        public DateTime appointmentDate { get; private set; }

        public Moderator(IUser appointer, IUser user, DateTime expirationDate)
        {
            this.appointer = appointer;
            this.user = user;
            this.expirationDate = expirationDate;
            this.appointmentDate = DateTime.Today;
        }

        public void changeExpirationDate(DateTime expirationDate)
        {
            this.expirationDate = expirationDate;
        }
        public bool CanBeDeletedBy(string user)
        {
            return appointer.getUsername().Equals(user);
        }
    }
}
