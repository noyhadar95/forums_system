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
            SuperAdmin sa = SuperAdmin.populateSuperAdmin();
            Server.CommunicationLayer.Server.StartServer();
        }
    }
}
