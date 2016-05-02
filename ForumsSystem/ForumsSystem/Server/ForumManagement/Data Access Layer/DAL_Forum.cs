using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.Data_Access_Layer
{
    public class DAL_Forum
    {

        private static Connect connect_me = null;
        private void Connect_to_DB()
        {

            string directory = System.Reflection.Assembly.GetExecutingAssembly().Location;
            int from = directory.IndexOf("\\ForumsSystem");
            directory = directory.Substring(0, from);
              connect_me = new Connect(Path.GetDirectoryName( directory + "/ForumsSystem/ForumsSystem/Server/ForumManagement/Data Access Layer/Database/") +"\\ForumSystemDB.mdb");
           // connect_me = new Connect("C:\\Users\\shai\\Desktop\\hope\\db.mdb");
        }
        public void CreateForum(string name, int policyID)
        {

            Connect_to_DB();
            string sql = "Insert into [Forums] values(@p1,@p2)";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", name);
            cmd.Parameters.AddWithValue("@p2", policyID);
            
            
            connect_me.TakeAction(cmd);


        }


    }
}
