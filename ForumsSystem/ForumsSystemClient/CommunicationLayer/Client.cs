using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace ForumsSystemClient.CommunicationLayer
{
    class Client
    {
        const int CLIENT_PORT_NO = 4000;
        const int SERVER_PORT_NO = 5000;
        const string delimeter = "$|deli|$";
        static string SERVER_IP = "79.179.27.79";

       private static string connect(string textToSend)
        {
            SERVER_IP = GetLocalIPAddress();
            //---create a TCPClient object at the IP and port no.---
            TcpClient client = new TcpClient(SERVER_IP, SERVER_PORT_NO);

                int port = ((IPEndPoint)client.Client.RemoteEndPoint).Port;


                NetworkStream nwStream = client.GetStream();
                byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(textToSend);

                //---send the text---
                Console.WriteLine("Sending : " + textToSend);
                nwStream.Write(bytesToSend, 0, bytesToSend.Length);

                //---read back the text---
                byte[] bytesToRead = new byte[client.ReceiveBufferSize];
                int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
                // Console.WriteLine("Received : " + Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));
                string textFromServer = GetString(bytesToRead, bytesRead);
                client.Close();

            return textFromServer;

            
        }
        public static void WaitForNotification()
        {
            string myIp = GetLocalIPAddress();
            //---listen at the specified IP and port no.---
            IPAddress localAdd = IPAddress.Parse(myIp);
            TcpListener listener = new TcpListener(localAdd, CLIENT_PORT_NO);

            listener.Start();

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
                string[] seperators = new string[] { delimeter };
                string[] items = dataReceived.Split(seperators, StringSplitOptions.None);

                //TODO: MAKE THIS WORK ---------------------
                List<Object> parameters = new List<object>();

                for (int i = 1; i < items.Length; i += 2)
                {
                    parameters.Add(StringToObject(items[i], items[i + 1]));
                }

                Console.WriteLine("Received : " + dataReceived);

                //TODO: Handle notification------------------

            }
            //  listener.Stop   
        }
        static string GetString(byte[] bytes, int bytesRead)
        {
            char[] chars = new char[bytesRead / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytesRead);
            return new string(chars);
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

        public static Object SendRequest(string methodName, params Object[] methodParameter)
        {
            string textToSend = methodName;
            foreach (Object param in methodParameter)
            {
                string pType = param.GetType().ToString();
              //  if(!pType.StartsWith("System."))
               //     pType = pType.Substring(pType.LastIndexOf('.') + 1);

                textToSend += delimeter + pType;
                textToSend += delimeter + ObjectToString(param);
            }
            if(methodName == "MemberLogin")
            {
                //should be only on login
                ThreadStart startNotification = new ThreadStart(WaitForNotification);
                Thread notificationThread = new Thread(startNotification);
                notificationThread.Start();
            }
            string textFromServer = connect(textToSend);
            if (textFromServer.Equals("null"))
                return null;

            string[] seperators =new string[] { delimeter };
            string[] items = textFromServer.Split(seperators, StringSplitOptions.None);

            return StringToObject(items[0], items[1]);

        }

        /*   public static string ObjectToString(Object obj)
           {

               if (IsList(obj))
               {
                   List<object> myAnythingList = (obj as IEnumerable<object>).Cast<object>().ToList();
                   var send = myAnythingList.ToArray();
                   XmlSerializer serializer = new XmlSerializer(send.GetType());
                   StringWriter writer = new StringWriter();
                   serializer.Serialize(writer, send);
                   return writer.ToString();
               }
               else {
                   XmlSerializer serializer = new XmlSerializer(obj.GetType());

                   StringWriter writer = new StringWriter();
                   serializer.Serialize(writer, obj);
                   return writer.ToString();
               }

           }




           public static bool IsList(object o)
           {
               if (o == null) return false;
               return o is IList &&
                      o.GetType().IsGenericType &&
                      o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
           }

           public static Object StringToObject(string classType, string str)
           {
               string addition = "ForumsSystemClient.Resources.";
               if (classType == "String" || classType == "Integer" || classType == "Boolean" || classType == "string" || classType == "int" || classType == "bool")
                   addition = "System.";
               Type type = Type.GetType(addition + classType);
               if (type == null)
                   return null;

               XmlSerializer serializer = new XmlSerializer(type);
               StringReader reader = new StringReader(str);
               return serializer.Deserialize(reader);
           }
           */

        public static string ObjectToString(Object obj)
        {
            /* XmlSerializer serializer = new XmlSerializer(obj.GetType());
             StringWriter writer = new StringWriter();
             serializer.Serialize(writer, obj);
             return writer.ToString();
             */

            return Serialize(obj);

        }

        public static Object StringToObject(string classType, string str)
        {

         //   string addition = "ForumsSystemClient.Resources.";
            // if (classType == "String" || classType == "Integer" || classType == "Boolean" || classType == "string" || classType == "int" || classType == "bool")
            //     addition = "System
           // if (classType.StartsWith("System."))
           //     addition = "";

            int index = classType.IndexOf("ForumsSystem.Server");
            if (index > -1)
            {
                string[] seperators = new string[] { "ForumsSystem.Server" };
                string[] items = classType.Split(seperators, StringSplitOptions.None);
                classType = items[0] + "ForumsSystemClient.Resources" + items[1];
            }
            Type type = Type.GetType(classType);

            /*
            XmlSerializer serializer = new XmlSerializer(type);
            StringReader reader = new StringReader(str);
            return serializer.Deserialize(reader);
            */

            return Deserialize(str, type);

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
            int index = xml.IndexOf("ForumsSystem.Server");
            if (index > -1)
            {
                string[] seperators = new string[] { "ForumsSystem.Server" };
                string[] items = xml.Split(seperators, StringSplitOptions.None);
                xml = "";
                for (int i = 0; i < items.Length; i += 2)
                {
                    xml += items[i] + "ForumsSystemClient.Resources" + items[i + 1];
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

    }
}
