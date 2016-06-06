using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.UserManagement.DomainLayer;
using ForumsSystem.Server.ForumManagement.Data_Access_Layer;
using System.Data;
using System.Runtime.Serialization;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(Moderator))]
    [KnownType(typeof(Thread))]
    [KnownType(typeof(User))]
    public class SubForum : ISubForum
    {
        [DataMember]
        private string name;
        [DataMember]
        private IForum forum;
        [DataMember]
        private Dictionary<string, Moderator> moderators;//Username, Moderator
        [DataMember]
        private List<Thread> threads;
        [IgnoreDataMember]
        private IUser creator; //admin who created the subforum
        public SubForum(IForum forum, IUser creator, string name)
        {
            this.forum = forum;
            this.creator = creator;
            this.name = name;
            moderators = new Dictionary<string, Moderator>();
            threads = new List<Thread>();
            Loggers.Logger.GetInstance().AddActivityEntry("SubForum: " + name + "created for Forum: " + ((Forum)forum).name + "by: " + creator.getUsername());

        }

        private SubForum()
        {
        }

        public static List<ISubForum> populateSubForums(Forum forum)
        {
            List<ISubForum> sForums = new List<ISubForum>();
            DAL_SubForums dsf = new DAL_SubForums();
            DataTable sForumtbl = dsf.GetAllSubForums(forum.getName());
            foreach (DataRow sForumRow in sForumtbl.Rows)
            {
                SubForum sf = new SubForum();
                string sForumName = sForumRow["SubForumName"].ToString();
                string createdUserName = sForumRow["CreatorUserName"].ToString();

                sf.name = sForumName;
                sf.forum = forum;

                //populate moderators
               sf.moderators = Moderator.populateModerators(forum, sForumName);

                //populate threads (includes posts)
                sf.threads = Thread.populateThreads(sf);

                sf.creator = forum.getDictionaryOfUsers()[createdUserName];

                sForums.Add(sf);
            }

            return sForums;
        }
        public int numOfModerators()
        {
            int val = 0;
            foreach (string mod in moderators.Keys)
            {
                if (isModerator(mod))
                    val++;
            }
            return val;
        }

        public bool addModerator(IUser admin, IUser user, DateTime expirationDate)
        {
            if (!checkedModertorAdditionPolicies(user))
                return false;
            Moderator mod = new Moderator(admin,user, expirationDate);
            moderators[user.getUsername()]= mod;
            Loggers.Logger.GetInstance().AddActivityEntry("Moderator: " + user.getUsername() + "added to subforum: " + this.name + " by: " + admin.getUsername());
            return true;
        }

        private bool checkedModertorAdditionPolicies(IUser modToBe)
        {
            PolicyParametersObject modAddition = new PolicyParametersObject(Policies.MaxModerators);
            modAddition.NumOfModerators = numOfModerators();
            if(!this.forum.GetPolicy().CheckPolicy(modAddition))
                return false;
            modAddition.SetPolicy(Policies.ModeratorAppointment);
            modAddition.User = modToBe;
            if (!this.forum.GetPolicy().CheckPolicy(modAddition))
                return false;
            return true;

        }

        public bool isModerator(string userName)
        {//TODO suspension policy?
            Moderator moderator = getModeratorByUserName(userName);
            if (moderator == null)
                return false;
            if (moderator.expirationDate <= DateTime.Today)
                return false;
            PolicyParametersObject modSuspension = new PolicyParametersObject(Policies.ModeratorSuspension);
            modSuspension.User = getModeratorByUserName(userName).user;
            if (!this.forum.GetPolicy().CheckPolicy(modSuspension))
                return false;
            return true;
        }

        public bool changeModeratorExpirationDate(IUser user, DateTime newExpirationDate)
        {
            Moderator mod = moderators[user.getUsername()];
            if (mod == null)
                return false;
            mod.changeExpirationDate(newExpirationDate);
            Loggers.Logger.GetInstance().AddActivityEntry("The expiration date of the moderator: " + user.getUsername() + " has been changed to " + newExpirationDate + " in subforum: " + this.name);
            return true;
        }

        public Thread createThread()
        {
            Thread thread = new Thread(this);
            threads.Add(thread);
            Loggers.Logger.GetInstance().AddActivityEntry("Thread created in subforum: " + name);
            return thread;
        }

        public string getName()
        {
            return name;
        }

      /*  public bool removeThread(int threadNumber) //TODO need an identifier for threads in the future
        {
            
            int newThreadNumber = threadNumber - 1;
            if (newThreadNumber <= threads.Count)
                return false;
            threads.RemoveAt(newThreadNumber);
            Loggers.Logger.GetInstance().AddActivityEntry("Thread removed from subforum: " + name);
            return true;
        }
        */
        public bool removeThread(Thread thread) //TODO need an identifier for threads in the future
        {
            return threads.Remove(thread);
        }

        public Moderator getModeratorByUserName(string userName)
        {
            if (!moderators.ContainsKey(userName))
                return null;
            return moderators[userName];
        }

        public IUser getCreator()
        {
            return this.creator;
        }

        public Thread getThread(int index)
        {
            foreach(Thread t in threads)
            {
                if (t.id == index)
                    return t;
            }
            return null;
        }

        public IForum getForum()
        {
            return this.forum;
        }

        public bool removeModerator(string remover, string moderator)
        {
            if (!moderators.ContainsKey(moderator))
                return false; // username is not moderator of this sub forum
            if (moderators.Count == 1)
                return false;
            Moderator mod = getModeratorByUserName(moderator);
            if (!mod.CanBeDeletedBy(remover))
                return false;
            moderators.Remove(moderator);
            Loggers.Logger.GetInstance().AddActivityEntry("Moderator: " + moderator + "removes from subforum: " + this.name );
            return true;
        }
        public Thread GetThreadById(int id)
        {
            foreach (Thread t in threads.ToList<Thread>())
            {
                if (t.id == id)
                    return t;
            }
            return null;
        }

        public List<Post> GetPostsByUser(string moderatorName)
        {
            List<Post> posts = new List<Post>();
            List<Post> threadPosts;
            foreach (Thread thread in threads.ToList<Thread>())
            {
                threadPosts = thread.GetPostsByUser(moderatorName);
                foreach (Post post in threadPosts)
                {
                    posts.Add(post);
                }
            }
            return posts;
        }

        public List<string> GetModeratorsList()
        {
            List<string> res = new List<string>();
            foreach (KeyValuePair<string, Moderator> mod in moderators?? new Dictionary<string, Moderator>())
            {
                if(isModerator(mod.Key))
                    res.Add(mod.Key);
            }
            return res;
        }

        public int GetNumOfPostsByUser(string username)
        {
            int posts = 0;
            int threadPosts;
            foreach (Thread thread in threads.ToList<Thread>())
            {
                threadPosts = thread.GetNumOfPostsByUser(username);
                posts += threadPosts;
            }
            return posts;
        }
        public List<Thread> GetThreads()
        {
            return threads;
        }
    }
}
