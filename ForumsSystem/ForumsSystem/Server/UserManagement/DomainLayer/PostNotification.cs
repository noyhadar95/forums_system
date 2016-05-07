﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.UserManagement.DomainLayer
{
    public class PostNotification
    {
        private NotificationType type; 
        private string publisher;
        private string subForumName;
        private string title;
        private string content;
        public int id { get; private set; }

        public PostNotification(NotificationType type,string publisher,
            string subForumName, string title, string content, int id)
        {
            this.type = type;
            this.publisher = publisher;
            this.subForumName = subForumName;
            this.title = title;
            this.content = content;
            this.id = id;
        }
    }
}