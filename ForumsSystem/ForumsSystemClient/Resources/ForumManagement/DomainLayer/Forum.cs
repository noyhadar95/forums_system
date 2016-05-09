using ForumsSystemClient.Resources.UserManagement.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources.ForumManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    public class Forum
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        private List<SubForum> sub_forums;
        [DataMember]
        private Policy policies;
        [DataMember]
        private Dictionary<string, User> users;//username, user
        [DataMember]
        private Dictionary<string, User> waiting_users;//username, user - waiting for confirmation

        public List<SubForum> Sub_forums
        {
            get
            {
                return sub_forums;
            }

            set
            {
                sub_forums = value;
            }
        }

        public Policy Policies
        {
            get
            {
                return policies;
            }

            set
            {
                policies = value;
            }
        }

        public Dictionary<string, User> Users
        {
            get
            {
                return users;
            }

            set
            {
                users = value;
            }
        }

        public Dictionary<string, User> Waiting_users
        {
            get
            {
                return waiting_users;
            }

            set
            {
                waiting_users = value;
            }
        }

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
