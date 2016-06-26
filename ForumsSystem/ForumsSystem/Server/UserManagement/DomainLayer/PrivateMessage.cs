using ForumsSystem.Server.ForumManagement.Data_Access_Layer;
using ForumsSystem.Server.ForumManagement.DomainLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.UserManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(User))]
    public class PrivateMessage
    {
        [DataMember]
        public string title { get;private set; }
        [DataMember]
        public string content { get; private set; }
        [IgnoreDataMember]
        public IUser sender { get; private set; }
        [IgnoreDataMember]
        public IUser receiver { get; private set; }

        public int id { get; private set; }
        [DataMember]
        public string senderUsername { get; private set; }

        public PrivateMessage(string title,string content, IUser sender, IUser receiver)
        {
            this.title = title;
            this.content = content;
            this.sender = sender;
            this.receiver = receiver;
            this.senderUsername = sender.getUsername();
            DAL_Messages dm = new DAL_Messages();
            this.id = dm.CreateMessage(sender.getForum().getName(), sender.getUsername(), receiver.getUsername(), title, content);
        }

        public PrivateMessage(string title, string content, IUser sender, IUser receiver,int i)
        {
            this.title = title;
            this.content = content;
            this.sender = sender;
            this.receiver = receiver;
            this.senderUsername = sender.getUsername();
          
        }

        private PrivateMessage()
        {

        }

        public static void populateMessages(Dictionary<string, IUser> users, Dictionary<string, IUser> waiting_users)
        {
            DAL_Messages dm = new DAL_Messages();
            Dictionary<string, IUser> allUsers = users.Union(waiting_users).ToDictionary(k => k.Key, v => v.Value);
            foreach (KeyValuePair<string, IUser> entry in allUsers)
            {
                User user = (User)entry.Value;
                DataTable messageTbl = dm.GetUsersSentMessages(user.getForum().getName(), user.getUsername());
                foreach (DataRow messageRow in messageTbl.Rows)
                {
                    PrivateMessage message = new PrivateMessage();
                    message.title = messageRow["Title"].ToString();
                    message.content = messageRow["Content"].ToString();
                    message.sender = user;
                    message.senderUsername = user.getUsername();
                    User reciever =(User)allUsers[messageRow["RecieverUserName"].ToString()];
                    message.receiver = reciever;
                    message.id = (int)messageRow["ID"];

                    user.AddTosentMessages(message);
                    reciever.AddToreceivedMessages(message);
                }
            }
        }

       

    }
}
