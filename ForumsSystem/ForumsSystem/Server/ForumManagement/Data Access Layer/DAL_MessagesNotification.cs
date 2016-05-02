using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.Data_Access_Layer
{
    public class DAL_MessagesNotification : DAL_Connection
    {
        public void AddNotification(string forumName, string userName, int messageId)
        {

            Connect_to_DB();
            string sql = "Insert into [MessagesNotification] values(@p1,@p2,@p3)";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", forumName);
            cmd.Parameters.AddWithValue("@p2", userName);
            cmd.Parameters.AddWithValue("@p3", messageId);
           

            connect_me.TakeAction(cmd);

        }

        public DataTable GetUsersNotifications(string forumName, string userName)
        {
            Connect_to_DB();
            string sql = "Select * From MessagesNotification WHERE ForumName=@p1 AND UserName=@p2";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", forumName);
            cmd.Parameters.AddWithValue("@p2", userName);


            return connect_me.DownloadData2(cmd, "Messages");
        }

        /// <summary>
        /// Removes a notification from the user
        /// </summary>
        /// <param name="forumName"></param>
        /// <param name="userName"></param>
        /// <param name="messageId"></param>
         public void RemoveNotification(string forumName, string userName, int messageId)
         {
             Connect_to_DB();
             OleDbCommand cmd = new OleDbCommand();
             cmd.CommandText = "Delete From [MessagesNotification] Where [ForumName]=@p1 AND [UserName]=@p2 AND [Message]=@p3";


             cmd.Parameters.AddWithValue("@p1", forumName);
             cmd.Parameters.AddWithValue("@p2", userName);
             cmd.Parameters.AddWithValue("@p3", messageId);


             connect_me.TakeAction(cmd);
             cmd = null;
         }

        /// <summary>
        /// Removes all of the users message notifications 
        /// </summary>
        /// <param name="forumName"></param>
        /// <param name="userName"></param>
        public void RemoveAllNotifications(string forumName, string userName)
        {
            Connect_to_DB();
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = "Delete From [MessagesNotification] Where [ForumName]=@p1 AND [UserName]=@p2";


            cmd.Parameters.AddWithValue("@p1", forumName);
            cmd.Parameters.AddWithValue("@p2", userName);

            connect_me.TakeAction(cmd);
            cmd = null;
        }






    }
}
