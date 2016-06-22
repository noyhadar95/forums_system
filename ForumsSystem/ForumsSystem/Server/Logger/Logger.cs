using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.Loggers
{
   public class Logger
    {
        private static Logger instance = new Logger();
        private List<Tuple<DateTime, string>> activityLogs;
        private List<Tuple<DateTime, string>> errorLogs;
        private Object activityLock = new Object();
        private Object errorLock = new Object();
        private string activityLogsPath = "ActivityLogs.txt";
        private string errorLogsPath = "ErrorLogs.txt";
        StreamWriter activityLogsFile;
        StreamWriter errorLogsFile;


        private Logger()
        {
            activityLogs = new List<Tuple<DateTime, string>>();
            errorLogs = new List<Tuple<DateTime, string>>();
            try
            {

                // Delete the file if it exists.
                if (File.Exists(activityLogsPath))
                {
                    File.Delete(activityLogsPath);
                }
                if (File.Exists(errorLogsPath))
                {
                    File.Delete(errorLogsPath);
                }

                
                
            }
            catch(Exception e)
            {

            }

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
                StreamWriter activityLogsFile = new System.IO.StreamWriter(activityLogsPath, true);
                activityLogsFile.WriteLine(entry+"\n");
                activityLogsFile.Close();
            }
        }

        public void AddErrorEntry(string entry)
        {
            lock (errorLock)
            {
                DateTime date = DateTime.Today;
                errorLogs.Add(Tuple.Create(date, entry));
                StreamWriter errorLogsFile = new System.IO.StreamWriter(errorLogsPath, true);
                errorLogsFile.WriteLine(entry + "\n");
                errorLogsFile.Close();
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
