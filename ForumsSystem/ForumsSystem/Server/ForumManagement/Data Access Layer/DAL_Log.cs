using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.Data_Access_Layer
{
   public abstract class DAL_Log : DAL_Connection
    {
        protected string logName;
        protected string dateName;
        public void AddLog(DateTime date, string content)
        {
            Connect_to_DB();
            string sql = "Insert into ["+logName+"] values(@p1,@p2)";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.Add("@p1", OleDbType.Date).Value = date;
            cmd.Parameters.AddWithValue("@p2", content);

            connect_me.TakeAction(cmd);
        }

        public DataTable GetLogByDate(DateTime date)
        {
            Connect_to_DB();
            string sql = "Select * From "+logName+" WHERE "+dateName+"=@p1";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.Add("@p1", OleDbType.Date).Value = date;

            return connect_me.DownloadData2(cmd, logName);

        }

        public DataTable GetAllLogs()
        {
            Connect_to_DB();
            string sql = "Select * From "+logName;
            return connect_me.DownloadData(sql, logName);
        }

        public void DeleteLog(DateTime date)
        {
            Connect_to_DB();
            string sql = "DELETE FROM ["+logName+"] WHERE "+dateName+"=@p1";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.Add("@p1", OleDbType.Date).Value = date;

            connect_me.TakeAction(cmd);
        }
    }
}

