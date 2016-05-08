using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources.ForumManagement.DomainLayer
{
    public class System
    {
        // private SuperAdmin superAdmin;
        private Dictionary<string, Forum> forums; //name, forum

        public Dictionary<string, Forum> Forums
        {
            get
            {
                return forums;
            }

            set
            {
                forums = value;
            }
        }
    }
}
