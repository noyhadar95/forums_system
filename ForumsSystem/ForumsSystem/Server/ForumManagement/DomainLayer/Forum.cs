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
            Loggers.Logger.GetInstance().AddActivityEntry(forumName + "created");
        }
        
        public bool RegisterToForum(string userName, string password, string Email) //TODO: Need to add age
        {
            if (users.ContainsKey(userName))
                return false;
            if (!CheckRegistrationPolicies(password))
                return false;

            IUser newUser = new User(userName, password, Email, this);
            Loggers.Logger.GetInstance().AddActivityEntry("User: " + userName + " Created");
            return true;

        }

        public bool RegisterToForum(IUser user)
        {
            if (isUserMember(user.getUsername()))
                return false;
            if (!CheckRegistrationPolicies(user.getPassword()))
                return false;
            users.Add(user.getUsername(), user);
            Loggers.Logger.GetInstance().AddActivityEntry("User: " + user.getUsername() + " Registered");
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

        public void addSubForum(ISubForum subForum)
        {
            sub_forums.Add(subForum);
            Loggers.Logger.GetInstance().AddActivityEntry("SubForum: " + subForum.getName() + " created by: " + subForum.getCreator().getUsername());
        }

        public void CreateSubForum(IUser creator, string subForumName)
        {
            sub_forums.Add(new SubForum(this,creator,subForumName));
            Loggers.Logger.GetInstance().AddActivityEntry("SubForum: " + subForumName + " created by: " + creator.getUsername());
        }

        public IUser Login(string userName, string password)
        {
            if (users.ContainsKey(userName))
            {
                Loggers.Logger.GetInstance().AddActivityEntry("User: " + userName + " logged in");
                if (users[userName].getPassword().Equals(password))
                {
                    users[userName].Login();
                    return users[userName];

                }
                else
                    return null;
            }
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
            Loggers.Logger.GetInstance().AddActivityEntry("Policy: " + policy.Type + "added to subforum: " + this.name);
            if (policies == null)
            {
                policies = policy;
                return true;
            }
            return policies.AddPolicy(policy);
        }

        public void RemovePolicy(Policies policyType)
        {
            if (policies != null)
            {
                policies = policies.RemovePolicy(policyType);
                Loggers.Logger.GetInstance().AddActivityEntry("Policy of type: " + policyType + "removed from subforum " + this.name);

            }

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

        public IUser getUser(string username)
        {
            if(this.users.ContainsKey(username))
                return this.users[username];
            return null;
        }

        public bool isUserMember(string username)
        {
            return this.users.ContainsKey(username);
        }

        public void SetPolicy(Policy policy)
        {
            this.policies = policy;
        }

        public Policy GetPolicy()
        {
            return this.policies;
        }

        public int GetNumOfUsers()
        {
            return this.users.Count;
        }
    }
}
