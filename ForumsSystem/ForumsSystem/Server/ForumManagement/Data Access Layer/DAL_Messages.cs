using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.Data_Access_Layer
{
    public class DAL_Messages : DAL_Connection
    {
        /// <summary>
        /// Gets the current maximum id of messages
        /// </summary>
        /// <returns></returns>
        public int getMaxId()
        {
            Connect_to_DB();
            string sql = "Select MAX(ID) AS MaxId From Messages";

            DataTable tb = connect_me.DownloadData(sql, "Messages");
            if (tb.Rows.Count == 0)
                return 0;
            if (tb.Rows[0][0] == null || tb.Rows[0][0].ToString() == "")
                return 0;
            return (int)tb.Rows[0][0];
        }
        /// <summary>
        /// Adds a message to the database
        /// </summary>
        /// <param name="forumName"></param>
        /// <param name="senderUserName"></param>
        /// <param name="recieverUserName"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <returns>The new message Id</returns>
        public int CreateMessage(string forumName, string senderUserName, string recieverUserName, string title, string content)
        {
            int newId = getMaxId() + 1;

            Connect_to_DB();
            string sql = "Insert into [Messages] values(@p1,@p2,@p3,@p4,@p5,@p6)";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", newId);
            cmd.Parameters.AddWithValue("@p2", title);
            cmd.Parameters.AddWithValue("@p3", content);
            cmd.Parameters.AddWithValue("@p4", forumName);
            cmd.Parameters.AddWithValue("@p5", senderUserName);
            cmd.Parameters.AddWithValue("@p6", recieverUserName);

            connect_me.TakeAction(cmd);

            return newId;
        }

        public DataTable GetUsersMessages(string forumName, string userName)
        {
            Connect_to_DB();
            string sql = "Select * From Messages WHERE [Forum]=@p1 AND ([SenderUserName]=@p2 OR [RecieverUserName]=@p2)";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", forumName);
            cmd.Parameters.AddWithValue("@p2", userName);



            return connect_me.DownloadData2(cmd, "Messages");
        }


        public DataTable GetUsersSentMessages(string forumName, string userName)
        {
            Connect_to_DB();
            string sql = "Select * From Messages WHERE Forum=@p1 AND SenderUserName=@p2";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", forumName);
            cmd.Parameters.AddWithValue("@p2", userName);


            return connect_me.DownloadData2(cmd, "Messages");
        }

        public DataTable GetUsersRecievedMessages(string forumName, string userName)
        {
            Connect_to_DB();
            string sql = "Select * From Messages WHERE Forum=@p1 AND RecieverUserName=@p2";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", forumName);
            cmd.Parameters.AddWithValue("@p2", userName);


            return connect_me.DownloadData2(cmd, "Messages");
        }

        public void DeleteMessage(int messageId)
        {
            Connect_to_DB();
            string sql = "DELETE FROM [Messages] WHERE ID=@p1";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", messageId);
         
            connect_me.TakeAction(cmd);

        }

        public void DeleteAllMessagesFromForum(string forumName)
        {
            Connect_to_DB();
            string sql = "DELETE FROM [Messages] WHERE Forum=@p1";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", forumName);

            connect_me.TakeAction(cmd);

        }

        //TODO: Can delete message? - if so what happens
        /* public void RemoveFriend(string forumName, string userName, string friendUserName)
         {
             Connect_to_DB();
             OleDbCommand cmd = new OleDbCommand();
             cmd.CommandText = "Delete From [Friends] Where [ForumName]=@p1 AND [UserName]=@p2 AND [FriendUserName]=@p3";


             cmd.Parameters.AddWithValue("@p1", forumName);
             cmd.Parameters.AddWithValue("@p2", userName);
             cmd.Parameters.AddWithValue("@p3", friendUserName);


             connect_me.TakeAction(cmd);
             cmd = null;
         }
         */




    }
}
