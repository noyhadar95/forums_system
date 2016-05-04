using ForumsSystem.Server.ForumManagement.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Server.CommunicationLayer.Server.StartServer();

            Forum f = new Forum("test");
            string s =Server.CommunicationLayer.Server.ObjectToString(f);
            Forum f2 = (Forum)Server.CommunicationLayer.Server.StringToObject(s, "Forum");
        }
    }
}
