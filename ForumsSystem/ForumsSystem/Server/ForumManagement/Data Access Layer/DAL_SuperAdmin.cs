using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.Data_Access_Layer
{
    class DAL_SuperAdmin : DAL_Connection
    {
       

        public void createSuperAdmin(string username, string password)
        {

            Connect_to_DB();
            string sql = "Insert into [SuperAdmin] values(@p1,@p2)";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", username);
            cmd.Parameters.AddWithValue("@p2", password);


            connect_me.TakeAction(cmd);

        }

        public DataTable GetSuperAdmin(string username)
        {
            Connect_to_DB();
            string sql = "Select * From [SuperAdmin] WHERE UserName=" + username;
            return connect_me.DownloadData(sql, "Super Admin");
        }

        public DataTable GetAllSuperAdmins()
        {
            Connect_to_DB();
            string sql = "Select * From [SuperAdmin]";
            return connect_me.DownloadData(sql, "Super Admin");
        }

        public void DeleteSuperAdmin(string username)
        {
            Connect_to_DB();
            OleDbCommand sql = new OleDbCommand();
            sql.CommandText = "Delete From [SuperAdmin] Where [UserName]=" + username;

            connect_me.TakeAction(sql);
            sql = null;
        }


    }
}
