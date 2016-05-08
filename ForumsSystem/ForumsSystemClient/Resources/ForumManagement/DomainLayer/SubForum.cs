using ForumsSystemClient.Resources.UserManagement.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources.ForumManagement.DomainLayer
{
    public class SubForum
    {
        private string name;
        private Forum forum;
        private Dictionary<string, Moderator> moderators;//Username, Moderator
        private List<Thread> threads;
        private User creator; //admin who created the subforum

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public Forum Forum
        {
            get
            {
                return forum;
            }

            set
            {
                forum = value;
            }
        }

        public Dictionary<string, Moderator> Moderators
        {
            get
            {
                return moderators;
            }

            set
            {
                moderators = value;
            }
        }

        public List<Thread> Threads
        {
            get
            {
                return threads;
            }

            set
            {
                threads = value;
            }
        }

        public User Creator
        {
            get
            {
                return creator;
            }

            set
            {
                creator = value;
            }
        }

        public string getName()
        {
            return name;
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
    }
}
