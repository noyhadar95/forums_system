using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.Loggers
{
    class ActivityLogger
    {
        private static ActivityLogger instance = new ActivityLogger();
        private List<Tuple<DateTime, string>> logs;
        private Object thisLock = new Object();

        private ActivityLogger()
        {
            logs = new List<Tuple<DateTime, string>>();
        }

        public static ActivityLogger GetInstance()
        {
            return instance;
        }

        public void AddEntry(string entry)
        {
            lock (thisLock)
            {
                DateTime date = DateTime.Today;
                logs.Add(Tuple.Create(date, entry));
            }
        }
        public override string ToString()
        {
            string res = "";
            foreach (Tuple<DateTime, string> tup in logs.ToList())
            {
                res += tup.Item1.ToShortTimeString() + ": " + tup.Item2 + "\n";
            }
            return res;
        }
    }
}
