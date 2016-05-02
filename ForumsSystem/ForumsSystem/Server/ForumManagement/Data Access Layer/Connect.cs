﻿using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;


namespace ForumsSystem.Server.ForumManagement.Data_Access_Layer
{
    public class Connect
    {
        private string my_path;

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
            DataSet ds = new DataSet();

            OleDbCommand cmmd = new OleDbCommand(my_sql, this.my_con);

            OleDbDataAdapter da = new OleDbDataAdapter(cmmd);

            da.Fill(ds, tableName);

            DataTable dt = ds.Tables[0];

            return (dt);
        }
        public DataTable DownloadData2(OleDbCommand cmd, string tableName)
        {
            DataSet ds = new DataSet();
            cmd.Connection = this.my_con;

            OleDbDataAdapter da = new OleDbDataAdapter(cmd);

            da.Fill(ds, tableName);

            DataTable dt = ds.Tables[0];

            return (dt);
        }
        public DataSet SearchByName(string sql1, string sql2)
        {
            DataSet ds = new DataSet();

            OleDbCommand cmd = new OleDbCommand(sql1, this.my_con);

            OleDbDataAdapter d = new OleDbDataAdapter(cmd);

            d.Fill(ds, "users");

            OleDbCommand cmd2 = new OleDbCommand(sql2, this.my_con);

            d.DeleteCommand = cmd2;

            d.Fill(ds, "users");

            return ds;
        }
        //------------------------------------------------------------------
        public void TakeAction(OleDbCommand cmmd)
        {
            cmmd.Connection = this.my_con;

            this.my_con.Open();

            cmmd.ExecuteNonQuery();

            this.my_con.Close();
        }
        //------------------------------------------------------------------
        public int ReturnValue(string sql)
        {
            OleDbCommand cmmd = new OleDbCommand(sql, my_con);

            this.my_con.Open();

            int result = (int)cmmd.ExecuteScalar();

            this.my_con.Close();

            return (result);
        }
        //----------------------------------------------------------------------
        public void TakeAction2(OleDbCommand cmmd)
        {
            cmmd.Connection = this.my_con;

            this.my_con.Open();

            cmmd.ExecuteReader();

            this.my_con.Close();
        }
    }

}