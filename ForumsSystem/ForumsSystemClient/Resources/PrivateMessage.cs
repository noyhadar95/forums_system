using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources
{
    public class PrivateMessage
    {
        public string title { get; private set; }
        public string content { get; private set; }
        public User sender { get; private set; }
        public User receiver { get; private set; }
    }
}
