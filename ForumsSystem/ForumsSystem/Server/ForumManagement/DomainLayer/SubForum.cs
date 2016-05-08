using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.UserManagement.DomainLayer;
using ForumsSystem.Server.ForumManagement.Data_Access_Layer;
using System.Data;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{

   public class SubForum : ISubForum
    {
        private string name;
        private IForum forum;
        private Dictionary<string, Moderator> moderators;//Username, Moderator
        private List<Thread> threads;
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

        public static List<SubForum> populateSubForums(Forum forum)
        {
            List<SubForum> sForums = new List<SubForum>();
            DAL_SubForums dsf = new DAL_SubForums();
            DataTable sForumtbl = dsf.GetAllSubForums(forum.getName());
            foreach (DataRow sForumRow in sForumtbl.Rows)
            {
                SubForum sf = new SubForum();
                string sForumName = sForumRow["SubForumName"].ToString();
                string createdUserName = sForumRow["CreatorUserName"].ToString();

                sf.name = sForumName;
                sf.forum = forum;


            }
        }
        public int numOfModerators()
        {
            return moderators.Count;
        }

        public void addModerator(IUser admin, IUser user, DateTime expirationDate)
        {
            
            Moderator mod = new Moderator(admin,user, expirationDate);
            moderators[user.getUsername()]= mod;
            Loggers.Logger.GetInstance().AddActivityEntry("Moderator: " + user.getUsername() + "added to subforum: " + this.name + " by: " + admin.getUsername());
        }

        public bool isModerator(string userName)
        {
            Moderator moderator = getModeratorByUserName(userName);
            if (moderator == null)
                return false;
            return moderator.expirationDate>DateTime.Today;
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

        public bool removeThread(int threadNumber) //TODO need an identifier for threads in the future
        {
            int newThreadNumber = threadNumber - 1;
            if (newThreadNumber <= threads.Count)
                return false;
            threads.RemoveAt(newThreadNumber);
            Loggers.Logger.GetInstance().AddActivityEntry("Thread removed from subforum: " + name);
            return true;
        }

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
            int newIndex = index - 1;
            if (newIndex >= threads.Count)
                return null;
            else return threads.ElementAt(newIndex);
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
