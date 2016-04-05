using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.Loggers
{
    class Logger
    {
        private static Logger instance = new Logger();
        private List<Tuple<DateTime, string>> activityLogs;
        private List<Tuple<DateTime, string>> errorLogs;
        private Object activityLock = new Object();
        private Object errorLock = new Object();

        private Logger()
        {
            activityLogs = new List<Tuple<DateTime, string>>();
            errorLogs = new List<Tuple<DateTime, string>>();
        }

        public static Logger GetInstance()
        {
            return instance;
        }

        public void AddActivityEntry(string entry)
        {
            lock (activityLock)
            {
                DateTime date = DateTime.Today;
                activityLogs.Add(Tuple.Create(date, entry));
            }
        }

        public void AddErrorEntry(string entry)
        {
            lock (errorLock)
            {
                DateTime date = DateTime.Today;
                errorLogs.Add(Tuple.Create(date, entry));
            }
        }
        public string GetActivities()
        {
            string res = "";
            foreach (Tuple<DateTime, string> tup in activityLogs.ToList())
            {
                res += tup.Item1.ToShortTimeString() + ": " + tup.Item2 + "\n";
            }
            return res;
        }
        public string GetErrors()
        {
            string res = "";
            foreach (Tuple<DateTime, string> tup in errorLogs.ToList())
            {
                res += tup.Item1.ToShortTimeString() + ": " + tup.Item2 + "\n";
            }
            return res;
        }
    }
}
