using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.Data_Access_Layer
{
    class DAL_PostsNotification : DAL_Connection
    {
        public void AddNotification(int postId, int type, string forumName, string recieverUserName)
        {

            Connect_to_DB();
            string sql = "Insert into [PostNotification] values(@p1,@p2,@p3,@p4)";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", postId);
            cmd.Parameters.AddWithValue("@p2", type);
            cmd.Parameters.AddWithValue("@p3", forumName);
            cmd.Parameters.AddWithValue("@p4", recieverUserName);


            connect_me.TakeAction(cmd);
        }

        public DataTable GetUsersNotifications(string forumName, string userName)
        {
            Connect_to_DB();
            // string sql = "SELECT PostNotification.*, Posts.*, Threads.* "+
            //      "FROM(Posts INNER JOIN PostNotification ON Posts.PostID = PostNotification.NotificationId) "+
            //      "INNER JOIN Threads ON (Threads.ThreadId = Posts.ThreadID) "+
            //      "AND(Posts.PostID = Threads.OpeningPostId) WHERE Forum=@p1 AND UserName=@p2";


            string sql = "SELECT PostNotification.Type, Posts.PublisherUserName, Threads.SubForumName, Posts.Title, Posts.Content, Posts.PostID " +
            "FROM(Posts INNER JOIN Threads ON(Threads.ThreadId = Posts.ThreadID) AND(Posts.PostID = Threads.OpeningPostId)) INNER JOIN PostNotification ON Posts.PostID = PostNotification.NotificationId WHERE Forum=@p1 AND UserName=@p2";


            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", forumName);
            cmd.Parameters.AddWithValue("@p2", userName);


            return connect_me.DownloadData2(cmd, "UserNotification");
        }

        /// <summary>
        /// Removes a notification from the user
        /// </summary>
        /// <param name="forumName"></param>
        /// <param name="userName"></param>
        /// <param name="Id"></param>
        public void RemoveNotification(int notificationId, int type, string forum, string userName)
        {
            Connect_to_DB();
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = "Delete From [PostNotifivation] Where NotificationId=@p1 AND Type=@p2 AND Forum=@p3 AND UserName=@p4";

            cmd.Parameters.AddWithValue("@p1", notificationId);
            cmd.Parameters.AddWithValue("@p2", type);
            cmd.Parameters.AddWithValue("@p3", forum);
            cmd.Parameters.AddWithValue("@p4", userName);



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
            cmd.CommandText = "DELETE PostNotification.* " +
                "FROM(Posts INNER JOIN PostNotification ON Posts.PostID = PostNotification.NotificationId) " +
                "INNER JOIN Threads ON (Threads.ThreadId = Posts.ThreadID) " +
                "AND(Posts.PostID = Threads.OpeningPostId) WHERE Forum=@p1 AND UserName=@p2";


            cmd.Parameters.AddWithValue("@p1", forumName);
            cmd.Parameters.AddWithValue("@p2", userName);

            connect_me.TakeAction(cmd);
            cmd = null;
        }



    }
}
