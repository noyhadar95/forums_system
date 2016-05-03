using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.Data_Access_Layer
{
    public class DAL_ActivityLog : DAL_Log
    {
        public DAL_ActivityLog()
        {
            this.logName = "ActivityLogs";
            this.dateName = "ActivityDate";
        }
    }
}
