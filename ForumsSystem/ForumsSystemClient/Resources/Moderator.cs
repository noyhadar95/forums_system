using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources
{
    class Moderator
    {
        public User user { get; private set; }
        public DateTime expirationDate { get; private set; }
        public User appointer { get; private set; }
    }
}
