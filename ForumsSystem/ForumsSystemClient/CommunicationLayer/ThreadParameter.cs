using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.CommunicationLayer
{
   public class ThreadParameter
    {
        public string param;
        public TcpClient client;


        public ThreadParameter(string param, TcpClient client)
        {
            this.param = param;
            this.client = client;
        }
    }
}
