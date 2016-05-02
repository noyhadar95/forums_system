using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.Data_Access_Layer
{
    class DAL_Users : DAL_Connection
    {
        //TODO: REMEMBER TO CHANGE STUFF ABOUT THE WAITING
        public void CreateUser(string ForumName, string userName, string password, string email, DateTime dateJoined, DateTime DateOfBirth, int numOfComplaints, UserType type)
        {

            Connect_to_DB();
            string sql = "Insert into [Users] values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", ForumName);
            cmd.Parameters.AddWithValue("@p2", userName);
            cmd.Parameters.AddWithValue("@p3", password);
            cmd.Parameters.AddWithValue("@p4", email);
            cmd.Parameters.AddWithValue("@p5", dateJoined);
            cmd.Parameters.AddWithValue("@p6", DateOfBirth);
            cmd.Parameters.AddWithValue("@p7", numOfComplaints);
            cmd.Parameters.AddWithValue("@p8", type);

            connect_me.TakeAction(cmd);

        }





    }
}
