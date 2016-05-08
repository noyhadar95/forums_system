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



        public Forum()
        {

        }

        public string getName()
        {
            return name;
        }
        public SubForum getSubForum(string subForumName)
        {
            foreach (SubForum subForum in sub_forums)
            {
                if (subForum.getName().Equals(subForumName))
                    return subForum;
            }
            return null;
        }

        public User getUser(string username)
        {
            if (this.users.ContainsKey(username))
                return this.users[username];
            return null;
        }
        public Policy GetPolicy()
        {
            return this.policies;
        }

    }
}
