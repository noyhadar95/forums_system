using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.Data_Access_Layer
{
    public class DAL_PolicyParameter : DAL_Connection
    {
        /// <summary>
        /// Creates a policy parameter according to some of the parameters
        /// It is important to note that not all are used
        /// if an int is not used, then enter a number that is less than 0.
        /// </summary>
        /// <param name="policyId"></param>
        /// <param name="seniorityInDays"></param>
        /// <param name="numOfMessages"></param>
        /// <param name="numOfComplaints"></param>
        /// <param name="blockPassword"></param>
        /// <param name="maxModerators"></param>
        /// <param name="minAge"></param>
        /// <param name="requiredLength"></param>
        /// <param name="passwordValidity"></param>
        /// <param name="maxNumOfUsers"></param>
        public void CreatePolicyParameter(int policyId, int seniorityInDays, int numOfMessages,
            int numOfComplaints, bool blockPassword, int maxModerators, int minAge,int minSeniority, int requiredLength,int passwordValidity, int maxNumOfUsers, bool moderatorDeletePermission)
        {

            Connect_to_DB();
            string sql = "Insert into [PolicyParameter] values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12)";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", policyId);

            if (seniorityInDays < 0)
                cmd.Parameters.AddWithValue("@p2", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@p2", seniorityInDays);

            if (numOfMessages < 0)
                cmd.Parameters.AddWithValue("@p3", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@p3", numOfMessages);

            if (numOfComplaints < 0)
                cmd.Parameters.AddWithValue("@p4", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@p4", numOfComplaints);


            cmd.Parameters.AddWithValue("@p5", blockPassword);

            if (maxModerators < 0)
                cmd.Parameters.AddWithValue("@p6", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@p6", maxModerators);

            if (minAge < 0)
                cmd.Parameters.AddWithValue("@p7", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@p7", minAge);

            if (minSeniority < 0)
                cmd.Parameters.AddWithValue("@p8", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@p8", minSeniority);

            if (requiredLength < 0)
                cmd.Parameters.AddWithValue("@p9", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@p9", requiredLength);

            if (passwordValidity < 0)
                cmd.Parameters.AddWithValue("@p10", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@p10", passwordValidity);

            if (maxNumOfUsers < 0)
                cmd.Parameters.AddWithValue("@p11", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@p11", maxNumOfUsers);

            cmd.Parameters.AddWithValue("@p12", moderatorDeletePermission);
            

            connect_me.TakeAction(cmd);

        }


        
        public DataTable GetPolicyParameter(int policyId)
        {
            Connect_to_DB();
            string sql = "Select * From PolicyParameter WHERE PolicyId=@p1";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", policyId);

            return connect_me.DownloadData2(cmd, "PolicyParameter");
        }

        /// <summary>
        /// Updates a policy parameter according to some of the parameters
        /// It is important to note that not all are used
        /// if an int is not used, then enter a number that is less than 0.
        /// </summary>
        /// <param name="policyId"></param>
        /// <param name="seniorityInDays"></param>
        /// <param name="numOfMessages"></param>
        /// <param name="numOfComplaints"></param>
        /// <param name="blockPassword"></param>
        /// <param name="maxModerators"></param>
        /// <param name="minAge"></param>
        /// <param name="requiredLength"></param>
        /// <param name="passwordValidity"></param>
        /// <param name="maxNumOfUsers"></param>
        public void updatePolicyParameter(int policyId, int seniorityInDays, int numOfMessages,
            int numOfComplaints, bool blockPassword, int maxModerators, int minAge, int minSeniority, int requiredLength, int passwordValidity, int maxNumOfUsers , bool moderatorDeletePermission)
        {
            Connect_to_DB();
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = "UPDATE [PolicyParameter] SET seniorityInDays=@p2, numOfMessages=@p3, numOfComplaints=@p4, " +
                "blockPassword=@p5, maxModerators=@p6, minAge=@p7, minSeniority=@p8, requiredLength=@p9, passwordValidity=@p10, maxNumOfUsers=@p11, moderatorDeletePermission=@p12 Where PolicyId=@p1";

            
           

            if (seniorityInDays < 0)
                cmd.Parameters.AddWithValue("@p2", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@p2", seniorityInDays);

            if (numOfMessages < 0)
                cmd.Parameters.AddWithValue("@p3", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@p3", numOfMessages);

            if (numOfComplaints < 0)
                cmd.Parameters.AddWithValue("@p4", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@p4", numOfComplaints);


            cmd.Parameters.AddWithValue("@p5", blockPassword);

            if (maxModerators < 0)
                cmd.Parameters.AddWithValue("@p6", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@p6", maxModerators);

            if (minAge < 0)
                cmd.Parameters.AddWithValue("@p7", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@p7", minAge);

            if (minSeniority < 0)
                cmd.Parameters.AddWithValue("@p8", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@p8", minSeniority);

            if (requiredLength < 0)
                cmd.Parameters.AddWithValue("@p9", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@p9", requiredLength);

            if (passwordValidity < 0)
                cmd.Parameters.AddWithValue("@p10", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@p10", passwordValidity);

            if (maxNumOfUsers < 0)
                cmd.Parameters.AddWithValue("@p11", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@p11", maxNumOfUsers);

            cmd.Parameters.AddWithValue("@p12", moderatorDeletePermission);
            cmd.Parameters.AddWithValue("@p1", policyId);

            connect_me.TakeAction(cmd);
            cmd = null;
        }


        public void DeletePolicyParameter(int policyId)
        {
            Connect_to_DB();
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = "Delete From [PolicyParameter] Where [PolicyId]=@p1";

            cmd.Parameters.AddWithValue("@p1", policyId);

            connect_me.TakeAction(cmd);
            cmd = null;
        }


    }
}
