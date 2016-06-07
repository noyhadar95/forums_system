using ForumsSystem.Server.UserManagement.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using ForumsSystem.Server.ForumManagement.Data_Access_Layer;
using System.Runtime.Serialization;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(Forum))]
    [KnownType(typeof(User))]
    [KnownType(typeof(SubForum))]
    [Serializable]
    public class Forum : IForum
    {
        DAL_Forum dal_forum = new DAL_Forum();
        [DataMember]
        public string name { get;  set; }
        [DataMember]
        private List<ISubForum> sub_forums { get; set; }
        [DataMember]
        private Policy policies { get; set; }
        [IgnoreDataMember]
        private Dictionary<string, IUser> users { get; set; }//username, user
        [IgnoreDataMember]
        private Dictionary<string, IUser> waiting_users { get; set; }//username, user - waiting for confirmation

        private Forum()
        {

        }

        public static Forum populateForum(string forumName, int policyId)
        {
            Forum forum = new Forum();
            forum.name = forumName;
            //--users
            forum.users = User.populateUsers(forum);
            forum.waiting_users = User.populateWaitingUsers(forum);
            //----friends
            User.populateFriends(forum.users, forum.waiting_users, forumName);
            //----messages
            PrivateMessage.populateMessages(forum.users, forum.waiting_users);
            //----privateMessageNotifications
            PrivateMessageNotification.populateMessageNotification(forum.users, forum.waiting_users);

            //--subForums (includes threads, posts and moderators)
            forum.sub_forums = SubForum.populateSubForums(forum);

            //--post notification
            PostNotification.populatePostNotification(forum.users, forum.waiting_users, forum.name);

            //--policies
            forum.policies = Policy.populatePolicy(policyId);

            return forum;

        }
       
        public Forum(string forumName)
        {
            this.name = forumName;
            dal_forum.CreateForum(forumName, -1);
            InitForum();
            Loggers.Logger.GetInstance().AddActivityEntry(forumName + "created");
        }
        
        public string getName()
        {
            return name;
        }
        public Dictionary<string, IUser> getDictionaryOfUsers()
        {
            return users;
        }
        public List<ISubForum> GetSubForums()
        {
            return sub_forums;
        }
        private string createLinkForRegistration(IUser user)
        {
            return "someLinkForUser";
        }
        private void SendMailWhenRegistered(IUser user)
        {
            //PolicyParametersObject param = new PolicyParametersObject(Policies.Authentication);
            if (policies != null && policies.CheckIfPolicyExists(Policies.Authentication))
            {
                sendMail(user.getEmail(), user.getUsername(), "Successfully Registered To Forum: " + this.name,
                   "Hello" + user.getUsername() + ",\n You have registered to the Forum: " + this.name + ". Please click on this link to  complete your registration: " + createLinkForRegistration(user));
            }
            

            
        }

        public bool RegisterToForum(string userName, string password, string Email, DateTime dateOfBirth) 
        {
            if (users.ContainsKey(userName))
                return false;
            if (!CheckRegistrationPolicies(password,dateOfBirth))
                return false;

            IUser newUser = new User(userName, password, Email, this, dateOfBirth);

            return true;

        }
  
        public bool RegisterToForum(IUser user)
        {
            if (isUserMember(user.getUsername()))
                return false;
            if (!CheckRegistrationPolicies(user.getPassword(),user.GetDateOfBirth()))
                return false;
            users.Add(user.getUsername(), user);
            Loggers.Logger.GetInstance().AddActivityEntry("User: " + user.getUsername() + " Registered");
            waiting_users.Remove(user.getUsername());
             SendMailWhenRegistered(user);
            return true;
        }

        public bool CheckRegistrationPolicies(string password, DateTime dateOfBirth)
        {
            int age = (int)(DateTime.Today.Subtract(dateOfBirth).TotalDays / 365);
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
            param.SetPolicy(Policies.MinimumAge);
            param.SetAgeOfUser(age);
            if (!policies.CheckPolicy(param))
                return false;
            return true;
        }

        public bool addSubForum(ISubForum subForum)
        {
            if (!CheckSubForumAdditionPolicies())
                return false;
            sub_forums.Add(subForum);
            Loggers.Logger.GetInstance().AddActivityEntry("SubForum: " + subForum.getName() + " created by: " + subForum.getCreator().getUsername());
            return true;
        }

        public bool CreateSubForum(IUser creator, string subForumName)
        {
            if (!CheckSubForumAdditionPolicies())
                return false;
            sub_forums.Add(new SubForum(this,creator,subForumName));
            Loggers.Logger.GetInstance().AddActivityEntry("SubForum: " + subForumName + " created by: " + creator.getUsername());
            return true;
        }

        private bool CheckSubForumAdditionPolicies()
        {
            //can add here in the future policies for subforums addition
            return true;
        }

        public IUser Login(string userName, string password)
        {
            
            if (users.ContainsKey(userName))
            {
               
                password = users[userName].GetSalt()+password;
                password = PRG.Hash.GetHash(password);
                
                if (users[userName].getPassword().Equals(password))
                {
                    //check if the member is suspended
                    PolicyParametersObject susp = new PolicyParametersObject(Policies.MemberSuspension);
                    susp.User = users[userName];
                    if (!this.policies.CheckPolicy(susp))
                        return null;
                    users[userName].Login();
                    Loggers.Logger.GetInstance().AddActivityEntry("User: " + userName + " logged in");
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
            policies = null;
            users = new Dictionary<string, IUser>();
            this.waiting_users = new Dictionary<string, IUser>();
            return true;
        }

        public bool AddPolicy(Policy policy)
        {
            bool flag = false;
            Loggers.Logger.GetInstance().AddActivityEntry("Policy: " + policy.Type + "added to forum: " + this.name);
            if (policies == null)
            {
                if (policy is PasswordPolicy)
                {
                    policies = new AuthenticationPolicy(Policies.Authentication);//temp
                    flag = flag | policies.AddPolicy(policy);
                    policies = policies.NextPolicy;//delete the temp
                    policy = policies.NextPolicy;
                    if (policies != null)
                        policies.NextPolicy = null;
                }
                else
                {
                    policies = new PasswordPolicy(Policies.Password, 0, 1);//temp
                    flag = flag | policies.AddPolicy(policy);
                    policies = policies.NextPolicy;//delete the temp
                    policy = policies.NextPolicy;
                    if(policies!=null)
                       policies.NextPolicy = null;
                }
                //now we added the first policy. need to add the rest:
                // policy = policy.NextPolicy;
                Policy temp;
                while (policy != null)
                {
                    temp = policy;
                    flag = flag | policies.AddPolicy(policy);
                    policy = policy.NextPolicy;
                    temp.NextPolicy = null;
                }
                if (policies != null)
                {
                    dal_forum.SetForumPolicy(this.name,policies.ID);
                }

            }
            else
            {
                while (policy != null)
                {
                    flag = flag | policies.AddPolicy(policy);
                    policy = policy.NextPolicy;
                }
            }
            return flag;
        }

        public void RemovePolicy(Policies policyType)
        {
            if (policies != null)
            {
                policies = policies.RemovePolicy(policyType);
                if(policies == null)
                    dal_forum.SetForumPolicy(this.name, -1);
                else
                    dal_forum.SetForumPolicy(this.name,policies.ID);
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
        public IUser GetGuest(string guestName)
        {
            if (this.waiting_users.ContainsKey(guestName))
                return this.waiting_users[guestName];
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

        public void sendMail(string email, string userName, string subject, string body)
        {
            try
            {
                var fromAddress = new MailAddress("TimTimTeam1@gmail.com", "TimTimTeam");
                var toAddress = new MailAddress(email, userName);
                const string fromPassword = "TimTeamTim";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Timeout = 10;
                    //smtp.Send(message);
                }
                Loggers.Logger.GetInstance().AddActivityEntry("Email sent to: " + email);
            }
            catch (Exception e)
            {
                Loggers.Logger.GetInstance().AddErrorEntry("Failed sending email to " + userName + " at address " + email);
            }
            
            
        }    

        public void DeleteUser(string userName)
        {
            DAL_Users dal_users = new DAL_Users();
            dal_users.deleteUser(name, userName);
            this.users.Remove(userName);
        }

        public IUser GetWaitingUser(string username)
        {
            if (waiting_users.ContainsKey(username))
                return waiting_users[username];
            return null;
        }

        public bool AddWaitingUser(IUser user)
        {
            if (waiting_users.ContainsKey(user.getUsername()))
                return false;
            waiting_users.Add(user.getUsername(), user);
            return true;
        }

        public Dictionary<string, string> GetAllUsers()
        {
            if (this.users == null)
                return null;
            Dictionary<string, string> users = new Dictionary<string, string>();
            foreach (KeyValuePair<string,IUser> user in this.users)
            {
                users.Add(user.Key, user.Value.getEmail());
            }
            return users;
        }

        public List<Post> GetPostsByMember(string userName)
        {

            List<Post> posts = new List<Post>();
            List<Post> subforumPosts;
            if (!isUserMember(userName))
                return posts;
            foreach (ISubForum subforum in sub_forums)
            {
                subforumPosts = subforum.GetPostsByUser(userName);
                foreach (Post post in subforumPosts)
                {
                    posts.Add(post);
                }
            }
            return posts;
        }

        public int GetNumOfPostsByUser(string username)
        {
            int posts = 0;
            int subforumPosts;
            if (!isUserMember(username))
                return posts;
            foreach (ISubForum subforum in sub_forums)
            {
                subforumPosts = subforum.GetNumOfPostsByUser(username);
                posts += subforumPosts;
            }
            return posts;
        }

        public List<IUser> getUsersInForum()
        {
            List<IUser> users_res = new List<IUser>();
            foreach(KeyValuePair<string, IUser> u in users)
            {
                users_res.Add(u.Value);
            }
            return users_res;

        }
    }
}
