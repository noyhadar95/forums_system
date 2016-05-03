using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.Data_Access_Layer
{
    class DAL_SubForums : DAL_Connection
    {
        /// <summary>
        /// Creates a Sub Forum
        /// </summary>
        /// <param name="forumName"></param>
        /// <param name="subForumName"></param>
        /// <param name="creatorUserName"></param>
        public void CreateSubForum(string forumName,string subForumName, string creatorUserName)
        {

            Connect_to_DB();
            string sql = "Insert into [SubForums] values(@p1,@p2,@p3)";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", forumName);
            cmd.Parameters.AddWithValue("@p2", subForumName);
            cmd.Parameters.AddWithValue("@p3", creatorUserName);



            connect_me.TakeAction(cmd);

        }


        /// <summary>
        /// Gets the SubForum
        /// </summary>
        /// <param name="forumName"></param>
        /// <param name="subForumName"></param>
        /// <returns></returns>
        public DataTable GetSubForum(string forumName, string subForumName)
        {
            Connect_to_DB();
            string sql = "Select * From SubForums WHERE ForumName=@p1 AND SubForumName=@p2";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", forumName);
            cmd.Parameters.AddWithValue("@p2", subForumName);

            return connect_me.DownloadData2(cmd, "SubForums");
        }

        /// <summary>
        /// Gets All sub forums from a forum
        /// </summary>
        /// <param name="forumName">Name of the forum </param>
        /// <returns></returns>
        public DataTable GetAllSubForums(string forumName)
        {
            Connect_to_DB();
            string sql = "Select * From SubForums WHERE ForumName=@p1";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", forumName);

            return connect_me.DownloadData2(cmd, "SubForums");
        }
      
        public void DeleteSubForum(string forumName, string subForumName)
        {
            Connect_to_DB();
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = "Delete From [Forums] Where [ForumName]=@p1 AND [SubForumName]=@p2";

            cmd.Parameters.AddWithValue("@p1", forumName);
            cmd.Parameters.AddWithValue("@p2", subForumName);

            connect_me.TakeAction(cmd);
            cmd = null;
        }
    }
}
