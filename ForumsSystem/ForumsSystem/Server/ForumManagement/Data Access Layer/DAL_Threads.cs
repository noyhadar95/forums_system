using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.Data_Access_Layer
{
    class DAL_Threads : DAL_Connection
    {

        /// <summary>
        /// Gets the current maximum id of threads
        /// </summary>
        /// <returns></returns>
        public int getMaxId()
        {
            Connect_to_DB();
            string sql = "Select MAX(ThreadId) AS MaxId From Threads";

            DataTable tb = connect_me.DownloadData(sql, "Threads");
            if (tb.Rows.Count == 0)
                return 0;
            if (tb.Rows[0][0] == null || tb.Rows[0][0].ToString() == "")
                return 0;
            return (int)tb.Rows[0][0];
        }

        /// <summary>
        /// Creates a thread in subforum
        /// </summary>
        /// <param name="threadId"></param>
        /// <param name="postId"></param>
        /// <param name="forumName"></param>
        /// <param name="subForumName"></param>
        public void CreateThread(int threadId, int postId, string forumName, string subForumName)
        {

            Connect_to_DB();
            string sql = "Insert into [Threads] values(@p1,@p2,@p3,@p4)";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", threadId);
            if(postId<0)
                cmd.Parameters.AddWithValue("@p2", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@p2", postId);
            cmd.Parameters.AddWithValue("@p3", forumName);
            cmd.Parameters.AddWithValue("@p4", subForumName);


            connect_me.TakeAction(cmd);

        }


        public DataTable GetThread(int threadId)
        {
            Connect_to_DB();
            string sql = "Select * From Threads WHERE ThreadId=@p1";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", threadId);

            return connect_me.DownloadData2(cmd, "Threads");
        }

        /// <summary>
        /// Gets all of the threads of a subforum
        /// </summary>
        /// <param name="forumName"></param>
        /// <param name="subForumName"></param>
        /// <returns></returns>
        public DataTable GetAllThreads(string forumName, string subForumName)
        {
            Connect_to_DB();
            string sql = "Select * From Threads WHERE ForumName=@p1 AND SubForumName=@p2";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", forumName);
            cmd.Parameters.AddWithValue("@p2", subForumName);

            return connect_me.DownloadData2(cmd, "Threads");
        }

        public void DeleteThread(int threadId)
        {
            Connect_to_DB();
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = "Delete From [Threads] Where [ThreadId]=@p1";

            cmd.Parameters.AddWithValue("@p1", threadId);

            connect_me.TakeAction(cmd);
            cmd = null;
        }

        public void AddOpenningPost(int threadId, int postId)
        {
            Connect_to_DB();
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = "Update [Threads] Set [PostId] = @p1 Where [ThreadId]=@p2";

            cmd.Parameters.AddWithValue("@p1", postId);
            cmd.Parameters.AddWithValue("@p2", threadId);

            connect_me.TakeAction(cmd);
            cmd = null;
        }
    }
}
