using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources
{
    class Forum
    {
        public string name { get; set; }
        private List<SubForum> sub_forums { get; set; }
        private Policy policies { get; set; }
        private Dictionary<string, User> users { get; set; }//username, user
        private Dictionary<string, User> waiting_users { get; set; }//username, user - waiting for confirmation

    }
}
