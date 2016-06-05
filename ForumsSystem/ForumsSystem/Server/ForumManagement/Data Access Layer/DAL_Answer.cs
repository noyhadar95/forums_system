using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.Data_Access_Layer
{
    class DAL_Answer : DAL_Connection
    {


        public void AddAnswer(string forumName, string userName, int questionNum, string answer)
        {

            Connect_to_DB();
            string sql = "Insert into [Answers] values(@p1,@p2,@p3,@p4)";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", forumName);
            cmd.Parameters.AddWithValue("@p2", userName);
            cmd.Parameters.AddWithValue("@p3", questionNum);
            cmd.Parameters.AddWithValue("@p4", answer);


            connect_me.TakeAction(cmd);

        }

        public void DeleteAll()
        {
            Connect_to_DB();
            string sql = "Delete * From Answers";
            OleDbCommand cmd = new OleDbCommand(sql);
            connect_me.TakeAction(cmd);
        }

        public DataTable GetAnswers(string forumName, string userName)
        {
            Connect_to_DB();
            string sql = "Select * From Answers WHERE ForumName='" + forumName+"' AND UserName='" + userName+ "'";
            return connect_me.DownloadData(sql, "Answers");
        }

        public DataTable GetAllAnswers()
        {
            Connect_to_DB();
            string sql = "Select * From Answers";
            return connect_me.DownloadData(sql, "Answers");
        }

        public void DeleteAnswer(string forumName, string userName, int questionNum)
        {
            Connect_to_DB();
            OleDbCommand sql = new OleDbCommand();
            sql.CommandText = "Delete From [Answers] Where [ForumName]='" + forumName + "' AND [UserName]='" + userName + "' AND [QuestionNum]="+questionNum;

            connect_me.TakeAction(sql);
            sql = null;
        }

        public void UpdateAnswer(string forumName, string userName, int questionNum, string answer)
        {
            Connect_to_DB();
            string sql = "Update [Answers] Set [Answer]=@p1  Where [ForumName]=@p2 AND [UserName]=@p3 AND [QuestionNum]=@p4";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", answer);
            cmd.Parameters.AddWithValue("@p2", forumName);
            cmd.Parameters.AddWithValue("@p3", userName);
            cmd.Parameters.AddWithValue("@p4", questionNum);


            connect_me.TakeAction(cmd);
        }


    }


}
}
