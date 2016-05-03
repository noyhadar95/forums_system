using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.Data_Access_Layer
{
    class DAL_Moderators : DAL_Connection
    {
        /// <summary>
        /// Creates a moderator with the required fields
        /// </summary>
        /// <param name="forumName"></param>
        /// <param name="subForumName"></param>
        /// <param name="userName">The username of the user that will become a moderator</param>
        /// <param name="expirationDate"></param>
        /// <param name="appointerUserName">The username of the user that appointed the moderator</param>
        public void CreateModerator(string forumName, string subForumName, string userName, DateTime expirationDate, string appointerUserName)
        {

            Connect_to_DB();
            string sql = "Insert into [Moderators] values(@p1,@p2,@p3,@p4,@p5)";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", forumName);
            cmd.Parameters.AddWithValue("@p2", subForumName);
            cmd.Parameters.AddWithValue("@p3", userName);
            cmd.Parameters.AddWithValue("@p4", expirationDate);
            cmd.Parameters.AddWithValue("@p5", appointerUserName);



            connect_me.TakeAction(cmd);

        }

        public DataTable GetModerator(string forumName, string subForumName, string userName)
        {
            Connect_to_DB();
            string sql = "Select * From Moderators WHERE ForumName=@p1 AND SubForumName=@p2 AND UserName=@p3";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", forumName);
            cmd.Parameters.AddWithValue("@p2", subForumName);
            cmd.Parameters.AddWithValue("@p3", userName);

            return connect_me.DownloadData2(cmd, "Moderators");
        }
        /// <summary>
        /// Gets all of the moderators from a subforum
        /// </summary>
        /// <param name="forumName"></param>
        /// <param name="subForumName"></param>
        /// <returns></returns>
        public DataTable GetAllModerators(string forumName, string subForumName)
        {
            Connect_to_DB();
            string sql = "Select * From Moderators WHERE ForumName=@p1 AND SubForumName=@p2";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", forumName);
            cmd.Parameters.AddWithValue("@p2", subForumName);

            return connect_me.DownloadData2(cmd, "Moderators");
        }
        /// <summary>
        /// Removes the user as a moderator from subforum
        /// </summary>
        /// <param name="forumName"></param>
        /// <param name="subForumName"></param>
        /// <param name="userName"></param>
        public void DeleteModerator(string forumName, string subForumName, string userName)
        {
            Connect_to_DB();
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = "Delete From [Moderators] Where [ForumName]=@p1 AND [SubForumName]=@p2 AND UserName=@p3";

            cmd.Parameters.AddWithValue("@p1", forumName);
            cmd.Parameters.AddWithValue("@p2", subForumName);
            cmd.Parameters.AddWithValue("@p3", userName);

            connect_me.TakeAction(cmd);
            cmd = null;
        }
    }
}

