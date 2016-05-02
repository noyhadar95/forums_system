using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.Data_Access_Layer
{
    public class DAL_ActivityLog : DAL_Connection
    {
        public void AddActivity(DateTime activityDate, string content)
        {
            Connect_to_DB();
            string sql = "Insert into [ActivityLogs] values(@p1,@p2)";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.Add("@p1", OleDbType.Date).Value = activityDate;
            cmd.Parameters.AddWithValue("@p2", content);
          
            connect_me.TakeAction(cmd);
        }

        public DataTable GetActivityByDate(DateTime activityDate)
        {
            Connect_to_DB();
            string sql = "Select * From ActivityLogs WHERE ActivityDate=@p1";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.Add("@p1", OleDbType.Date).Value = activityDate;

            return connect_me.DownloadData2(cmd, "ActivityLogs");
            
        }

        public DataTable GetAllActivities()
        {
            Connect_to_DB();
            string sql = "Select * From ActivityLogs";
            return connect_me.DownloadData(sql, "ActivityLogs");
        }

        public void DeleteActivity(DateTime activityDate)
        {
            Connect_to_DB();
            string sql = "DELETE FROM [ActivityLogs] WHERE ActivityDate=@p1";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.Add("@p1", OleDbType.Date).Value = activityDate;

            connect_me.TakeAction(cmd);
        }
    }
}
