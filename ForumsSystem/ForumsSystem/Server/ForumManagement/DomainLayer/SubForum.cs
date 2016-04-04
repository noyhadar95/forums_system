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
        private Dictionary<string, Moderator> moderators;//Username, Moderator
        private List<Thread> threads;

        public SubForum(string name)
        {
            this.name = name;
            moderators = new Dictionary<string, Moderator>();
            threads = new List<Thread>();
        }
        public void addModerator(IUser user, DateTime expirationDate)
        {
            Moderator mod = new Moderator(user, expirationDate);
            moderators.Add(user.getUsername(), mod);

        }

        public bool changeModeratorExpirationDate(IUser user, DateTime newExpirationDate)
        {
            Moderator mod = moderators[user.getUsername()];
            if (mod == null)
                return false;
            mod.changeExpirationDate(newExpirationDate);
            return true;
        }

        public void createThread()
        {
            threads.Add(new Thread());
        }

        public string getName()
        {
            return name;
        }

        public void removeThread() //TODO need an identifier for threads
        {
            throw new NotImplementedException();
        }
    }
}
