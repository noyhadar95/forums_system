using ForumsSystem.Server.UserManagement.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    public class Forum : IForum
    {

        public string name { get; private set; }
        private List<ISubForum> sub_forums;
        private Policy policies;
        private Dictionary<string, IUser> users;//username, user


        public Forum(string forumName)
        {
            this.name = forumName;
            InitForum();
        }

        public bool RegisterToForum(string userName, string password, string Email) //TODO: Need to add age
        {
            if (users.ContainsKey(userName))
                return false;
            if (!CheckRegistrationPolicies(password))
                return false;

            IUser newUser = new User(userName, password, Email, this);
            users.Add(userName, newUser);
            return true;

        }

        private bool CheckRegistrationPolicies(string password)
        {
            if (policies == null)
                return true;
            PolicyParametersObject param = new PolicyParametersObject(Policies.Password);
            param.SetPassword(password);
            if (!policies.CheckPolicy(param))
                return false;
            param.SetPolicy(Policies.UsersLoad);
            param.SetNumOfUsers(users.Count);
            if (!policies.CheckPolicy(param))
                return false;
            return true;
        }

        public void CreateSubForum(string subForumName)
        {
            sub_forums.Add(new SubForum(this,subForumName));
        }

        public IUser Login(string userName, string password)
        {
            if (users.ContainsKey(userName))
                return users[userName];
            else
                return null;
        }

        public bool InitForum()
        {
            sub_forums = new List<ISubForum>();
            //policies?
            users = new Dictionary<string, IUser>();

            return true;
        }

        public bool AddPolicy(Policy policy)
        {
            if (policies == null)
            {
                policies = policy;
                return true;
            }
            return policies.AddPolicy(policy);
        }

        public void RemovePolicy(Policies policyType)
        {
            if(policies != null)
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
