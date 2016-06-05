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
        public void CreateUser(string ForumName, string userName, string password, string email, DateTime dateJoined, DateTime DateOfBirth, int numOfComplaints, UserType.UserTypes type, DateTime dateLastPasswordChanged, string passwordSalt)
        {

            Connect_to_DB();
         
            OleDbCommand sql = new OleDbCommand();

            sql.CommandText = "Insert into [Users] values ('"+ForumName+
                "','"+userName+"', '" + password + "', '" + email +
                "',#" + dateJoined.ToShortDateString() +
                "#, #" + DateOfBirth.ToShortDateString() + "#, " + numOfComplaints + 
                ", " + (int)type + ","+false+" ,#" + dateLastPasswordChanged + "#, " + passwordSalt+")";

            connect_me.TakeAction(sql);
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

        public void editUser(string ForumName, string userName, string password, string email, DateTime dateJoined, DateTime DateOfBirth, int numOfComplaints, UserType.UserTypes type, DateTime dateLastPasswordChanged)
        {
            Connect_to_DB();
            OleDbCommand sql = new OleDbCommand();

            sql.CommandText = "Update Users Set [password]='" + password + "', [email]='" + email + "', [DateJoined]=#"+ dateJoined.ToShortDateString() + "#, [DateOfBirth]=#" + DateOfBirth.ToShortDateString() + "#, [Complaints]=" + numOfComplaints + ", [Type]="+ (int)type + ", [DateLastPasswordChanged]=#"+ dateLastPasswordChanged+"# Where [ForumName]='" + ForumName +"' AND [UserName]='" + userName +"'";



            connect_me.TakeAction(sql);
            sql = null;

        }

        private void DeleteUserFromMessages(string forumName, string userName)
        {
            Connect_to_DB();
            OleDbCommand cmd = new OleDbCommand();

            cmd.CommandText = "Update Messages Set [SenderUserName]=@p1 Where [Forum]=@p2 AND [SenderUserName]=@p3";

            cmd.Parameters.AddWithValue("@p1", "Deleted");
            cmd.Parameters.AddWithValue("@p2", forumName);
            cmd.Parameters.AddWithValue("@p3", userName);

            connect_me.TakeAction(cmd);
            cmd = null;

            Connect_to_DB();
            cmd = new OleDbCommand();

            cmd.CommandText = "Update Messages Set [RecieverUserName]=@p1 Where [Forum]=@p2 AND [RecieverUserName]=@p3";

            cmd.Parameters.AddWithValue("@p1", "Deleted");
            cmd.Parameters.AddWithValue("@p2", forumName);
            cmd.Parameters.AddWithValue("@p3", userName);

            connect_me.TakeAction(cmd);
            cmd = null;
        }

        private void DeleteUserFromSubForum(string forumName, string userName)
        {
            Connect_to_DB();
            OleDbCommand cmd = new OleDbCommand();

            cmd.CommandText = "Update SubForums Set [CreatorUserName]=@p1 Where [ForumName]=@p2 AND [CreatorUserName]=@p3";

            cmd.Parameters.AddWithValue("@p1", "Deleted");
            cmd.Parameters.AddWithValue("@p2", forumName);
            cmd.Parameters.AddWithValue("@p3", userName);

            connect_me.TakeAction(cmd);
            cmd = null;
        }

        public void deleteUser(string forumName, string userName)
        {
            DeleteUserFromMessages(forumName, userName);
            DeleteUserFromSubForum(forumName, userName);
            Connect_to_DB();
            string sql = "DELETE FROM [Users] WHERE ForumName=@p1 AND UserName=@p2";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", forumName);
            cmd.Parameters.AddWithValue("@p2", userName);

            connect_me.TakeAction(cmd);

        }


    }
}
