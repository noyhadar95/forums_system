using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ForumsSystem.Server.CommunicationLayer
{
    public class Server
    {
        const int CLIENT_PORT_NO = 4000;
        const int SERVER_PORT_NO = 5000;
        const string delimeter = "$|deli|$";
        const string SERVER_IP = "132.72.225.97";

        private static Dictionary<Tuple<string, string>, string> halfClients; //not yet subscribed

        private static Dictionary<Tuple<string, string>, string> clients; //<Forum,Username> Ip address
        public static void StartServer()
        {
            clients = new Dictionary<Tuple<string, string>, string>();
                //---listen at the specified IP and port no.---
                IPAddress localAdd = IPAddress.Parse(SERVER_IP);
                TcpListener listener = new TcpListener(localAdd, SERVER_PORT_NO);
           
                listener.Start();
            ThreadPool.SetMaxThreads(10, 10);

            while (true)
            {
                Console.WriteLine("Listening...");
                //---incoming client connected---
                TcpClient client = listener.AcceptTcpClient();

             
                //---get the incoming data through a network stream---
                NetworkStream nwStream = client.GetStream();
                byte[] buffer = new byte[client.ReceiveBufferSize];

                //---read incoming stream---
                int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

                //---convert the data received into a string---
                string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Received : " + dataReceived);

                Console.WriteLine("Adding to threadpool");
                ThreadParameter tp = new ThreadParameter(dataReceived,client);
                ThreadPool.QueueUserWorkItem(new WaitCallback(task), (Object)tp);

               
            }
          //  listener.Stop   

        }

        //should be done only on login
        public static void HalfSubscribeClient(TcpClient client ,string forumName, string userName)
        {
            string ip = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
            // int port = ((IPEndPoint)client.Client.RemoteEndPoint).Port;

            halfClients[new Tuple<string, string>(forumName, userName)] = ip;
            
        }

        public static void SubscribeClient(string forumName, string userName)
        {
            clients[new Tuple<string, string>(forumName, userName)] = halfClients[new Tuple<string, string>(forumName, userName)];
            halfClients.Remove(new Tuple<string, string>(forumName, userName));
        }
        public static void UnSubscribeClient(string forumName, string userName)
        {
            clients.Remove(new Tuple<string, string>(forumName, userName)); 
        }


        public static void notifyClient(string forumName, string userName)
        {
            Tuple<string, string> clientTuple = new Tuple<string, string>(forumName, userName);
            if (!clients.ContainsKey(clientTuple))
                return;
            string ip = clients[clientTuple];
            
                //---data to send to the server---
                string textToSend = "Sent from server";



                //---create a TCPClient object at the IP and port no.---
                TcpClient client = new TcpClient(ip, CLIENT_PORT_NO);

                int port = ((IPEndPoint)client.Client.RemoteEndPoint).Port;


                NetworkStream nwStream = client.GetStream();
                byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(textToSend);

                //---send the text---
                Console.WriteLine("Sending : " + textToSend);
                nwStream.Write(bytesToSend, 0, bytesToSend.Length);

                client.Close();
            
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }



        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
        public static void task(Object tp)
        {
            string textFromClient = ((ThreadParameter)tp).param;
            string[] seperators = new string[] { delimeter };
            string[] items = textFromClient.Split(seperators, StringSplitOptions.None);


            string method = items[0];
            List<Object> parameters = new List<object>();

            for (int i = 1; i < items.Length; i+=2)
            {

            }

            TcpClient client = ((ThreadParameter)tp).client;
            if (method.Equals("MemberLogin"){//check if login then Halfsubscribe
                HalfSubscribeClient(client, ((ThreadParameter)tp).param, ((ThreadParameter)tp).param);
            }
            if (method.Equals("MemberLogout"))//TODO:check if logout then unsubscribe
                UnSubscribeClient();


                string returnValue = ((ThreadParameter)tp).param + "HAHA";
           

            //---write back the text to the client---
            Console.WriteLine("Sending back : " + returnValue);
            NetworkStream nwStream = client.GetStream();
            byte[] buf = GetBytes(returnValue);
            nwStream.Write(buf, 0,buf.Length);
            client.Close();
        }


        public static string ObjectToString(Object obj)
        { 
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            StringWriter writer = new StringWriter();
            serializer.Serialize(writer,obj);
            return writer.ToString();

        }

        public static Object StringToObject(string str, string classType)
        {
            Type type = Type.GetType("ForumsSystem.Server.ForumManagement.DomainLayer." + classType);
            XmlSerializer serializer = new XmlSerializer(type);
            StringReader reader = new StringReader(str);
            return serializer.Deserialize(reader);
        }
        
    }
}
