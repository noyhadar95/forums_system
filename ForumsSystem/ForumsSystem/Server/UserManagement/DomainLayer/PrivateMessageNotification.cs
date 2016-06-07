
﻿using ForumsSystem.Server.ForumManagement.Data_Access_Layer;
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
    public class PrivateMessageNotification
    {
        [DataMember]
        public string sender { get; private set; }
        [DataMember]
        public string title { get; private set; }
        [DataMember]
        public string content { get; private set; }
        [DataMember]
        public int id { get; private set; }
        public PrivateMessageNotification(string sender,string title,string content,int id)
        {
            this.sender = sender;
            this.title = title;
            this.content = content;
            this.id = id;
        }

        public static void populateMessageNotification(Dictionary<string, IUser> users, Dictionary<string, IUser> waiting_users)
        {
            DAL_MessagesNotification dm = new DAL_MessagesNotification();
            Dictionary<string, IUser> allUsers = users.Union(waiting_users).ToDictionary(k => k.Key, v => v.Value);

            foreach (KeyValuePair<string, IUser> entry in allUsers)
            {
                DataTable messageNotificationTbl = dm.GetUsersNotifications(entry.Value.getForum().getName(), entry.Key);
                foreach (DataRow messageRow in messageNotificationTbl.Rows)
                {
                    string sender = messageRow["SenderUserName"].ToString();
                    string title = messageRow["Title"].ToString();
                    string content = messageRow["Content"].ToString();
                    int id = (int)messageRow["Messages.Id"];

                    entry.Value.AddToMessageNotification(new PrivateMessageNotification(sender, title, content, id));
                }
            }
        }
    }
}
