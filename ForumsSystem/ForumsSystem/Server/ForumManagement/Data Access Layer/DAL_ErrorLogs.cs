using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.Data_Access_Layer
{
    class DAL_ErrorLogs : DAL_Log
    {
        public DAL_ErrorLogs()
        {
            this.logName = "ErrorLogs";
            this.dateName = "ErrorDate";
        }
    }
}