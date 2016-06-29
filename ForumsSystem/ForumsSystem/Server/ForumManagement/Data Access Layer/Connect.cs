using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;


namespace ForumsSystem.Server.ForumManagement.Data_Access_Layer
{
    public class Connect
    {
        private string my_path;
        static object Lock = new object();

        private OleDbConnection my_con;
        //-------------------------------------------------------------------

        public Connect(string path)
        {
            this.my_path = @"Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + path;

            this.my_con = new OleDbConnection(this.my_path);
        }
        //------------------------------------------------------------------------
        public DataTable DownloadData(string my_sql, string tableName)
        {
            lock (Lock)
            {
                DataSet ds = new DataSet();

                OleDbCommand cmmd = new OleDbCommand(my_sql, this.my_con);

                OleDbDataAdapter da = new OleDbDataAdapter(cmmd);

                da.Fill(ds, tableName);

                DataTable dt = ds.Tables[0];

                return (dt);
            }
        }
        public DataTable DownloadData2(OleDbCommand cmd, string tableName)
        {
            lock (Lock)
            {
                DataSet ds = new DataSet();
                cmd.Connection = this.my_con;

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);

                da.Fill(ds, tableName);

                DataTable dt = ds.Tables[0];

                return (dt);
            }
        }
       
        //------------------------------------------------------------------
        public void TakeAction(OleDbCommand cmmd)
        {
            lock (Lock)
            {
                try
                {
                    cmmd.Connection = this.my_con;

                    this.my_con.Open();

                    cmmd.ExecuteNonQuery();

                    this.my_con.Close();
                }
                catch (Exception e)
                {

                }
            }
        }
        //------------------------------------------------------------------
        public int ReturnValue(string sql)
        {
            lock (Lock)
            {
                OleDbCommand cmmd = new OleDbCommand(sql, my_con);

                this.my_con.Open();

                int result = (int)cmmd.ExecuteScalar();

                this.my_con.Close();

                return (result);
            }
        }
        //----------------------------------------------------------------------
        public void TakeAction2(OleDbCommand cmmd)
        {
            lock (Lock)
            {
                cmmd.Connection = this.my_con;

                this.my_con.Open();

                cmmd.ExecuteReader();

                this.my_con.Close();
            }
        }
    }

}
