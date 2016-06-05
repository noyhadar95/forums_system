using ForumsSystem.Server.ForumManagement.DomainLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Serialization;
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
        static string SERVER_IP = "79.179.27.79";
        private static ServiceLayer.IServiceLayer sl;
        private static Dictionary<Tuple<string, string>, string> halfClients; //not yet subscribed

        private static Dictionary<Tuple<string, string>, string> clients; //<Forum,Username> Ip address
        private static Dictionary<int,Client> clientsDetails = new Dictionary<int, Client>();
        private static Dictionary<Tuple<string, string>, Tuple<string, int>> clientSessions = new Dictionary<Tuple<string, string>, Tuple<string, int>>();//session token, list of logged in users

        public static void StartServer()
        {
            SERVER_IP = GetLocalIPAddress();
            clients = new Dictionary<Tuple<string, string>, string>();
            halfClients = new Dictionary<Tuple<string, string>, string>();
            sl = new ServiceLayer.ServiceLayer();
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
                ThreadParameter tp = new ThreadParameter(dataReceived, client);
                ThreadPool.QueueUserWorkItem(new WaitCallback(task), (Object)tp);


            }
            //  listener.Stop   

        }

        //should be done only on login
        public static void HalfSubscribeClient(TcpClient client, string forumName, string userName)
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


        public static void notifyClient(string forumName, string userName, Object notification)
        {
            string[] notifArr = ((string)notification).Split(',');
            try
            {
                if (int.Parse(notifArr[0]) == 0 && int.Parse(notifArr[1]) == 0 && int.Parse(notifArr[2]) == 0)
                    return;
            }
            catch (Exception)
            {
                return;
            }

            Tuple<string, string> clientTuple = new Tuple<string, string>(forumName, userName);
            if (!clients.ContainsKey(clientTuple))
                return;
            string ip = clients[clientTuple];

            //---data to send to the server---
            string pType = notification.GetType().ToString();
            // pType = pType.Substring(pType.LastIndexOf('.') + 1);

            string textToSend = pType + delimeter + ObjectToString(notification);

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
            int clientId=-1;
            TcpClient client = ((ThreadParameter)tp).client;
            string textFromClient = ((ThreadParameter)tp).param;
            if(textFromClient.StartsWith("StartSecuredConnection"))
            {
                StartSecuredConnection(client);
                return;

            }
            else
            {
                Object[] temp= Decrypt(textFromClient);
                clientId = (int)temp[0];
                textFromClient = (string)temp[1];
            }
            string[] seperators = new string[] { delimeter };
            string[] items = textFromClient.Split(seperators, StringSplitOptions.None);
           

            string method = items[0];

            List<Object> parameters = new List<object>();

            for (int i = 1; i < items.Length; i += 2)
            {
                parameters.Add(StringToObject(items[i], items[i + 1]));
            }



            if (method.Equals("GetSessionKey"))
            {
                SendSessionKey(client,clientId, (string)parameters.ElementAt(0),(string)parameters.ElementAt(1));
                return;
            }


           
            if (method.Equals("MemberLogin")){//check if login then Halfsubscribe

                string username = (string)parameters.ElementAt(0);
                //Forum f = (Forum)parameters.ElementAt(2);
                string forumName = (string)parameters.ElementAt(2);
                string clientSession=null;
                if ( parameters.ElementAt(3) != null && (string)parameters.ElementAt(3) != "")
                {
                    clientSession = (string)parameters.ElementAt(3);
                    string realSession;
                    if (clientSessions.ContainsKey(new Tuple<string, string>(forumName, username)))
                        realSession = clientSessions[new Tuple<string, string>(forumName, username)].Item1;
                    else
                        realSession = null;
                    if (realSession==null || !realSession.Equals(clientSession))
                    {
                        string returnValue = "null";
                        returnValue = Encrypt(clientId, returnValue);

                        //---write back the text to the client---
                        Console.WriteLine("Sending back : " + returnValue);
                        NetworkStream nwStream = client.GetStream();
                        byte[] buf = GetBytes(returnValue);
                        nwStream.Write(buf, 0, buf.Length);
                        client.Close();
                        return;
                    }
                    
                }
                else
                {
                    //check if user exists. if exists return null because he didnt provide session key
                    if(clientSessions.ContainsKey(new Tuple<string, string>(forumName, username)))
                    {
                        string returnValue = "null";
                        returnValue = Encrypt(clientId, returnValue);

                        //---write back the text to the client---
                        Console.WriteLine("Sending back : " + returnValue);
                        NetworkStream nwStream = client.GetStream();
                        byte[] buf = GetBytes(returnValue);
                        nwStream.Write(buf, 0, buf.Length);
                        client.Close();
                        return;
                    }

                    //new client - create session:
                    Tuple<string, int> newSession = new Tuple<string,int>(PRG.ClientSessionKeyGenerator.GetUniqueKey(), 0);
                    clientSessions[new Tuple<string, string>(forumName, username)] = newSession;
                }
                //client session is correct:
                //add to current session:
                clientSessions[new Tuple<string, string>(forumName, username)] =new Tuple<string, int>(
                    clientSessions[new Tuple<string, string>(forumName, username)].Item1,
                    clientSessions[new Tuple<string, string>(forumName, username)].Item2+1);

                parameters.RemoveAt(3);//no longer need the client session
                HalfSubscribeClient(client, forumName, username);
            }
            if (method.Equals("MemberLogout"))
            {
                string username = (string)parameters.ElementAt(1);
               // Forum f = (Forum)parameters.ElementAt(2);
                string forumName = (string) parameters.ElementAt(0);
                RemoveClientFromSession(client, username, forumName);     
                UnSubscribeClient(forumName, username);
            }

            //
            Type thisType = typeof(ServiceLayer.ServiceLayer);
            MethodInfo theMethod = thisType.GetMethod(method);
            Object returnObj = theMethod.Invoke(sl, parameters.ToArray());
            if (method.Equals("MemberLogin"))
            {
                string username = (string)parameters.ElementAt(0);
                string forumName = (string)parameters.ElementAt(2);

                if (returnObj == null)
                {
                    UnSubscribeClient(forumName, username);
                    RemoveClientFromSession(client, username, forumName);
                }
                else
                {
                    SubscribeClient(forumName, username);
                    //update session key in return object:
                 //   List<Object> tokenRetObj = new List<Object>();
                 //   tokenRetObj.Add(returnObj);
                 //   tokenRetObj.Add(clientSessions[new Tuple<string, string>(forumName, username)].Item1);
                 //  returnObj = tokenRetObj;
                }

                //HalfSubscribeClient(client, forumName, username);
            }
            if (returnObj == null)
            {

                string returnValue = "null";
                returnValue = Encrypt(clientId, returnValue);

                //---write back the text to the client---
                Console.WriteLine("Sending back : " + returnValue);
                NetworkStream nwStream = client.GetStream();
                byte[] buf = GetBytes(returnValue);
                nwStream.Write(buf, 0, buf.Length);
                client.Close();
            }
            else {
                string pType = returnObj.GetType().ToString();
                // if(!pType.StartsWith("System."))
                //   pType = pType.Substring(pType.LastIndexOf('.') + 1);

                string returnValue = pType + delimeter + ObjectToString(returnObj);

                returnValue = Encrypt(clientId, returnValue);
                //---write back the text to the client---
                Console.WriteLine("Sending back : " + returnValue);
                NetworkStream nwStream = client.GetStream();
                byte[] buf = GetBytes(returnValue);
                nwStream.Write(buf, 0, buf.Length);
                client.Close();
            }
        }

        private static void RemoveClientFromSession(TcpClient client, string username, string forumName)
        {
            clientSessions[new Tuple<string, string>(forumName, username)] = new Tuple<string, int>(
                    clientSessions[new Tuple<string, string>(forumName, username)].Item1,
                    clientSessions[new Tuple<string, string>(forumName, username)].Item2 - 1);
            int cliSession = clientSessions[new Tuple<string, string>(forumName, username)].Item2;

            //if clisession empty:
            if (cliSession == 0)
            {
                //remove session:
                clientSessions.Remove(new Tuple<string, string>(forumName, username));
               // Tuple<string,int> newSession = new Tuple<string,int>("", 0);
               // clientSessions[new Tuple<string, string>(forumName, username)] = newSession;
            }
        }

        private static Object[] Decrypt(string textFromClient)
        {
            Object[] ret = new Object[2];
            string[] seperators = new string[] { delimeter };
            string[] items = textFromClient.Split(seperators, StringSplitOptions.None);
            int cid= int.Parse(items[0]);
            string message = items[1];
            Client c = clientsDetails[cid];
            if (c == null)
            {
                ret[0] = -1;
                ret[1] = textFromClient;
                return ret;
            }
            message= Encryption.AESThenHMAC.SimpleDecrypt(message, c.encKey, c.authKey);
            ret[0] = cid;
            ret[1] = message;
            return ret;
        }
        private static string Encrypt(int cid, string textToClient)
        {
            if (cid == -1)
            {
                return "ERROR ENCRYPTING MESSAGE";
            }
            Client c = clientsDetails[cid];
           
            return Encryption.AESThenHMAC.SimpleEncrypt(textToClient, c.encKey, c.authKey);
        }

        private static void StartSecuredConnection(TcpClient client)
        {
            List<Object> ret = new List<Object>();
            Client newClient = new Client();
            clientsDetails.Add(newClient.id, newClient);
            ret.Add(newClient.id);
            ret.Add(newClient.encKey);
            ret.Add(newClient.authKey);
            Object securedDetails = ret;
            string pType = securedDetails.GetType().ToString();
            // if(!pType.StartsWith("System."))
            //   pType = pType.Substring(pType.LastIndexOf('.') + 1);

            string returnValue = pType + delimeter + ObjectToString(securedDetails);


            //---write back the text to the client---
            Console.WriteLine("Sending back : " + returnValue);
            NetworkStream nwStream = client.GetStream();
            byte[] buf = GetBytes(returnValue);
            nwStream.Write(buf, 0, buf.Length);
            client.Close();
        }


        public static Object StringToObject(string classType, string str)
        {
            string addition = "ForumsSystem.Server.";
            //if (classType == "String" || classType == "Integer" || classType == "Boolean" || classType == "string" || classType == "int" || classType == "bool")
            //    addition = "System.";
            if (classType.StartsWith("System."))
                addition = "";
            int index = classType.IndexOf("ForumsSystemClient.Resources");
            if (index > -1)
            {
                string[] seperators = new string[] { "ForumsSystemClient.Resources" };
                string[] items = classType.Split(seperators, StringSplitOptions.None);
                classType = "";
                for (int i = 0; i < items.Length; i += 2)
                {
                    classType += items[i] + "ForumsSystem.Server" + items[i + 1];
                }
            }


            Type type = Type.GetType(classType);
            /*
            XmlSerializer serializer = new XmlSerializer(type);
            StringReader reader = new StringReader(str);
            return serializer.Deserialize(reader);
            */
            return Deserialize(str, type);
        }

        public static string ObjectToString(Object obj)
        {
            /* XmlSerializer serializer = new XmlSerializer(obj.GetType());
             StringWriter writer = new StringWriter();
             serializer.Serialize(writer, obj);
             return writer.ToString();Domain
             */

            return Serialize(obj);

        }


        public static string Serialize(object obj)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            using (StreamReader reader = new StreamReader(memoryStream))
            {
                DataContractSerializer serializer = new DataContractSerializer(obj.GetType());
                serializer.WriteObject(memoryStream, obj);
                memoryStream.Position = 0;
                return reader.ReadToEnd();
            }
        }

        public static object Deserialize(string xml, Type toType)
        {
            int index = xml.IndexOf("ForumsSystemClient.Resources");
            if (index > -1)
            {
                string[] seperators = new string[] { "ForumsSystemClient.Resources" };
                string[] items = xml.Split(seperators, StringSplitOptions.None);
                xml = items[0];
                for (int i = 1; i < items.Length; i++)
                {
                    xml += "ForumsSystem.Server" + items[i];
                }
            }
            using (Stream stream = new MemoryStream())
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes(xml);
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                DataContractSerializer deserializer = new DataContractSerializer(toType);
                return deserializer.ReadObject(stream);
            }
        }

        private static void SendSessionKey(TcpClient client, int clientId, string username,string forumName)
        {
            Object returnObj= clientSessions[new Tuple<string, string>(forumName, username)].Item1;

            string pType = returnObj.GetType().ToString();
            // if(!pType.StartsWith("System."))
            //   pType = pType.Substring(pType.LastIndexOf('.') + 1);

            string returnValue = pType + delimeter + ObjectToString(returnObj);

            returnValue = Encrypt(clientId, returnValue);
            //---write back the text to the client---
            Console.WriteLine("Sending back : " + returnValue);
            NetworkStream nwStream = client.GetStream();
            byte[] buf = GetBytes(returnValue);
            nwStream.Write(buf, 0, buf.Length);
            client.Close();
        }

    }
}
