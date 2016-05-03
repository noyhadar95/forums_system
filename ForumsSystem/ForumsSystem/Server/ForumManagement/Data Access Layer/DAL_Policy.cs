using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.Data_Access_Layer
{
    public class DAL_Policy: DAL_Connection //TODO: THIS NEEDS MAJOR CHANGES
    {
        
        public void createPolicy(int policyID, int type, int nextPolicyId)
        {

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


        }


       /* public bool EditPolicy(string forumName)
        {
            Connect_to_DB();
            OleDbCommand sql = new OleDbCommand();
            sql.CommandText = "Update Visit Set [DateOfVisit]='" + visit.DateofVisit.ToShortDateString() + "', [AssignedDoctor]=" + visit.AssignedDoctor + ", [PatientID]=" + visit.PatientID + ", [DoctorNotes]'=" + visit.DoctorNotes + "', [TreatmentsMade]='" + visit.TreatmentsMade + "' Where [ID]=" + visit.VisitID;

            connect_me.TakeAction(sql);
            sql = null;

            return true;
        }

    */

    }
}
