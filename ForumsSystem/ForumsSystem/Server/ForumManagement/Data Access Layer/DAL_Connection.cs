using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.Data_Access_Layer
{
    public class DAL_Connection
    {
        protected static Connect connect_me = null;
        protected void Connect_to_DB()
        {

            string directory = System.Reflection.Assembly.GetExecutingAssembly().Location;
            int from = directory.IndexOf("\\ForumsSystem");
            directory = directory.Substring(0, from);
            connect_me = new Connect(Path.GetDirectoryName(directory + "/ForumsSystem/ForumsSystem/Server/ForumManagement/Data Access Layer/Database/") + "\\ForumSystemDB.mdb");
        }


    }
}
