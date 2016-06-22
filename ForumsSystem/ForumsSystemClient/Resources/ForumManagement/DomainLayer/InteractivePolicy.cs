using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources.ForumManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    public class InteractivePolicy : Policy
    {
        [DataMember]
        private int notifyMode;

        public int NotifyMode
        {
            get
            {
                return notifyMode;
            }

            set
            {
                notifyMode = value;
            }
        }
        public InteractivePolicy() : base()
        {

        }

        public InteractivePolicy(Policies type, int notifyMode) : base(type)
        {
            this.notifyMode = notifyMode;
        }
    }
}
