using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.ForumManagement.DomainLayer;
using System.Data;

namespace ForumsSystem.Server.ForumManagement.Data_Access_Layer
{
    public class DAL_Forum  : DAL_Connection
    {

       
        /// <summary>
        /// Creates a forum
        /// </summary>
        /// <param name="name">Forum Name</param>
        /// <param name="policyID">The policy id, negative if null</param>
        public void CreateForum(string name, int policyID)
        {

            Connect_to_DB();
            string sql = "Insert into [Forums] values(@p1,@p2)";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", name);
            if(policyID < 0)
            {
                cmd.Parameters.AddWithValue("@p2", DBNull.Value);
            }
            else
                cmd.Parameters.AddWithValue("@p2", policyID);
            
            
            connect_me.TakeAction(cmd);

            DAL_Users du = new DAL_Users();
            du.CreateUser(name, "Deleted", "Deleted4Ever", "Deleted", DateTime.Now, DateTime.Now, 0, UserType.UserTypes.Member);

        }

        public DataTable GetForum(string name)
        {
            Connect_to_DB();
            string sql = "Select * From Forums WHERE ForumName=" + name;
            return connect_me.DownloadData(sql, "Forums");
        }

        public DataTable GetAllForums()
        {
            Connect_to_DB();
            string sql = "Select * From Forums";
            return connect_me.DownloadData(sql, "Forums");
        }

        public void DeleteForum(string name)
        { 
            Connect_to_DB();
            OleDbCommand sql = new OleDbCommand();
            sql.CommandText = "Delete From [Forums] Where [ForumName]='" + name+"'";

            connect_me.TakeAction(sql);
            sql = null;
        }

       


    }
}
