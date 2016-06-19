using ForumsSystem.Server.ForumManagement.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem
{
    public class Program
    {
        static void Main(string[] args)
        {
          

            SuperAdmin.populateSuperAdmin();
            Server.CommunicationLayer.Server.StartServer();

            /*
            string s = "erge3434tg3";
            string hash = Server.PRG.Hash.GetHash(s);
            string hash2 = Server.PRG.Hash.GetHash(s);
            string hash3 = Server.PRG.Hash.GetHash(s);
            bool flag = hash == hash2;
            bool flag2 = hash.Equals(hash2);
            
            */
            Byte[] key = ForumsSystem.Server.Encryption.AESThenHMAC.NewKey();
            Byte[] key2 = ForumsSystem.Server.Encryption.AESThenHMAC.NewKey();
            string c=ForumsSystem.Server.Encryption.AESThenHMAC.SimpleEncrypt("message", key, key2);
            string m = ForumsSystem.Server.Encryption.AESThenHMAC.SimpleDecrypt(c, key, key2);
int i = 1;


        }
        
    }
}
