using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.CommunicationLayer
{
    public class Client
    {
        private static int nextId = 0;
        public int id;
        public Byte[] encKey;
        public Byte[] authKey;

        public Client()
        {
            this.id = nextId++;
            this.encKey = Encryption.AESThenHMAC.NewKey();
            this.authKey = Encryption.AESThenHMAC.NewKey();
        }
    }
}
