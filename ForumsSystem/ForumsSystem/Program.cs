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
            ForumsSystem.Server.ForumManagement.Data_Access_Layer.DAL_Forum d = new Server.ForumManagement.Data_Access_Layer.DAL_Forum();
            d.DeleteAll();
            Server.CommunicationLayer.Server.StartServer();
            
        }
    }
}
