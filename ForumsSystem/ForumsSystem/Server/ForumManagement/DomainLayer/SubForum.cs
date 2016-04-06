using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.UserManagement.DomainLayer;

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
        public void addModerator(IUser admin, IUser user, DateTime expirationDate)
        {
            
            Moderator mod = new Moderator(admin,user, expirationDate);
            moderators.Add(user.getUsername(), mod);
            Loggers.Logger.GetInstance().AddActivityEntry("Moderator: " + user.getUsername() + "added to subforum: " + this.name + " by: " + admin.getUsername());
        }

        public bool changeModeratorExpirationDate(IUser user, DateTime newExpirationDate)
        {
            Moderator mod = moderators[user.getUsername()];
            if (mod == null)
                return false;
            mod.changeExpirationDate(newExpirationDate);
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
            if (newThreadNumber < threads.Count)
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
            if (newIndex > threads.Count)
                return null;
            else return threads.ElementAt(newIndex);
        }
    }
}
