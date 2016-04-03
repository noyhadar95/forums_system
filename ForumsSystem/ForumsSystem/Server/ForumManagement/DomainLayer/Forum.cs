using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    class Forum : IForum
    {

        public string name { get; private set; }
        private List<ISubForum> sub_forums;
        private Policy policies;
 //      private Dictionary<string, Iuser> //username, user


        public Forum(string forumName)
        {

        }

        public bool RegisterToForum(string userName, string password, string Email)
        {
            throw new NotImplementedException();
        }

        public bool CreateSubForum(string subForumName)
        {
            throw new NotImplementedException();
        }

        public bool Login(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public bool InitForum()
        {
            sub_forums = new List<ISubForum>();

            return true;
        }

        public bool AddPolicy(Policy policy)
        {
            return policies.AddPolicy(policy);
        }

        public void RemovePolicy(Policies policyType)
        {
            policies = policies.RemovePolicy(policyType);
            
        }

        public ISubForum getSubForum(string subForumName)
        {
            foreach (ISubForum subForum in sub_forums)
            {
                if (subForum.getName().Equals(subForumName))
                    return subForum;
            }
            return null;
        }
    }
}
