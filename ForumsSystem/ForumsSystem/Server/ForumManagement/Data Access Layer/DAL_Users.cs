using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.Data_Access_Layer
{
    public class DAL_Users : DAL_Connection
    {
        //TODO: REMEMBER TO CHANGE STUFF ABOUT THE WAITING
        public void CreateUser(string ForumName, string userName, string password, string email, DateTime dateJoined, DateTime DateOfBirth, int numOfComplaints, UserType.UserTypes type)
        {

            Connect_to_DB();
            string sql = "Insert into [Users] values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", ForumName);
            cmd.Parameters.AddWithValue("@p2", userName);
            cmd.Parameters.AddWithValue("@p3", password);
            cmd.Parameters.AddWithValue("@p4", email);
            cmd.Parameters.AddWithValue("@p5", dateJoined);
            cmd.Parameters.AddWithValue("@p6", DateOfBirth);
            cmd.Parameters.AddWithValue("@p7", numOfComplaints);
            cmd.Parameters.AddWithValue("@p8", type);
            cmd.Parameters.AddWithValue("@p8", false);

            connect_me.TakeAction(cmd);

        }

        public void changeUserWaitingStatus(string ForumName, string userName, bool waiting)
        {
            Connect_to_DB();
            OleDbCommand sql = new OleDbCommand();
            sql.CommandText = "Update Users Set [Waiting]="+ waiting + " Where [ForumName]='" + ForumName +"' AND [UserName]='" + userName + "'";

            connect_me.TakeAction(sql);
            sql = null;
        }

        public DataTable GetAllUsers()
        {
            Connect_to_DB();
            string sql = "Select * From Users";
            return connect_me.DownloadData(sql, "Users");
        }

        public DataTable GetAllUsersFromForum(string forumName)
        {       
            Connect_to_DB();
            string sql = "Select * From Users Where ForumName='"+forumName+"'";
            return connect_me.DownloadData(sql, "Users");
        }

        public DataTable GetUser(string forumName, string userName)
        {
            Connect_to_DB();
            string sql = "Select * From Users Where ForumName='" + forumName + "' AND UserName='" + userName +"'";
            return connect_me.DownloadData(sql, "Users");
        }

        public void editUser(string ForumName, string userName, string password, string email, DateTime dateJoined, DateTime DateOfBirth, int numOfComplaints, UserType.UserTypes type)
        {
            Connect_to_DB();
            OleDbCommand sql = new OleDbCommand();

            sql.CommandText = "Update Users Set [password]='" + password + "', [email]='" + email + "', [DateJoined]=#"+ dateJoined.ToShortDateString() + "#, [DateOfBirth]=#" + DateOfBirth.ToShortDateString() + "#, [Complaints]=" + numOfComplaints + ", [Type]="+ (int)type + " Where [ForumName]='" + ForumName +"' AND [UserName]='" + userName +"'";

            connect_me.TakeAction(sql);
            sql = null;

        }




    }
}
