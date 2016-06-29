using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.Data_Access_Layer
{
    public class DAL_Friends : DAL_Connection
    {
        public void addFriend(string forumName, string userName, string friendUserName)
        {

            Connect_to_DB();
            string sql = "Insert into [Friends] values(@p1,@p2,@p3,@p4)";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", forumName);
            cmd.Parameters.AddWithValue("@p2", userName);
            cmd.Parameters.AddWithValue("@p3", friendUserName);
            cmd.Parameters.AddWithValue("@p4", false); //accepted

            connect_me.TakeAction(cmd);

        }
        public DataTable GetAllFriendsInForum(string forumName)
        {
            Connect_to_DB();
            string sql = "Select * From Friends WHERE ForumName=@p1";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", forumName);

            return connect_me.DownloadData2(cmd, "Friends");
        }
        public DataTable GetUsersFriends(string forumName, string userName)
        {
            Connect_to_DB();
            string sql = "Select * From Friends WHERE ForumName=@p1 AND (UserName=@p2 OR FriendUserName=@p2) AND Accepted=true";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", forumName);
            cmd.Parameters.AddWithValue("@p2", userName);


            return connect_me.DownloadData2(cmd, "Friends");
        }
        public DataTable GetUsersWaitingFriends(string forumName, string userName)
        {
            Connect_to_DB();
            string sql = "Select * From Friends WHERE ForumName=@p1 AND FriendUserName=@p2 AND Accepted=false";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", forumName);
            cmd.Parameters.AddWithValue("@p2", userName);


            return connect_me.DownloadData2(cmd, "Friends");
        }
        public void RemoveFriend(string forumName, string userName, string friendUserName)
        {
            Connect_to_DB();
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = "Delete From [Friends] Where [ForumName]=@p1 AND"+
                "(([UserName]=@p2 AND [FriendUserName]=@p3) OR ([UserName]=@p3 AND [FriendUserName]=@p2))";



            cmd.Parameters.AddWithValue("@p1", forumName);
            cmd.Parameters.AddWithValue("@p2", userName);
            cmd.Parameters.AddWithValue("@p3", friendUserName);


            connect_me.TakeAction(cmd);
            cmd = null;
        }

        public void AcceptFriend(string forumName, string userName, string friendUserName)
        {
            Connect_to_DB();
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = "UPDATE [Friends] SET Accepted=true Where [ForumName]=@p1 AND [UserName]=@p2 AND [FriendUserName]=@p3";

            cmd.Parameters.AddWithValue("@p1", forumName);
            cmd.Parameters.AddWithValue("@p2", userName);
            cmd.Parameters.AddWithValue("@p3", friendUserName);


            connect_me.TakeAction(cmd);
            cmd = null;
        }


    }
}
