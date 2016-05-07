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
    public class DAL_Policy: DAL_Connection //TODO: THIS NEEDS MAJOR CHANGES
    {

        /// <summary>
        /// Gets the current maximum id of Policies
        /// </summary>
        /// <returns></returns>
        public int getMaxId()
        {
            Connect_to_DB();
            string sql = "Select MAX(PolicyId) AS MaxId From Policies";

            DataTable tb = connect_me.DownloadData(sql, "Policies");
            if (tb.Rows.Count == 0)
                return 0;
            if (tb.Rows[0][0] == null || tb.Rows[0][0].ToString() == "")
                return 0;
            return (int)tb.Rows[0][0];
        }

        public int createPolicy(int type, int nextPolicyId)
        {
            int policyID = getMaxId() + 1;

            Connect_to_DB();
            string sql = "Insert into [Policies] values(@p1,@p2,@p3)";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", policyID);
            cmd.Parameters.AddWithValue("@p2", type);
            if (nextPolicyId < 0)
            {
                cmd.Parameters.AddWithValue("@p3", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@p3", nextPolicyId);
            }

            connect_me.TakeAction(cmd);

            return policyID;


        }

        public void DeletePolicy(int policyId)
        {
            Connect_to_DB();
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = "Delete From [Policies] Where [PolicyId]=@p1";

            cmd.Parameters.AddWithValue("@p1", policyId);

            connect_me.TakeAction(cmd);
            cmd = null;
        }

        public void SetNextPolicy(int policyID, int nextPolicyId)
        {
            Connect_to_DB();
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = "Update [Policies] Set [NextPolicyId]=@p1 Where [PolicyId]=@p2";

            cmd.Parameters.AddWithValue("@p2", policyID);
            if (nextPolicyId < 0)
            {
                cmd.Parameters.AddWithValue("@p1", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@p1", nextPolicyId);
            }
            connect_me.TakeAction(cmd);
            cmd = null;
        }



    }
}
