using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources
{
    public class SubForum
    {
        private string name;
        private Forum forum;
        private Dictionary<string, Moderator> moderators;//Username, Moderator
        private List<Thread> threads;
        private User creator; //admin who created the subforum
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
