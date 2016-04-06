using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.ForumManagement.DomainLayer;
using ForumsSystem.Server.UserManagement.DomainLayer;

namespace ForumsSystem.Server.ServiceLayer
{
    public class SendPrivateMessage
    {
        public static bool Send(IUser from,IUser to,string title,string content)
        {
            if (!from.isInFriendsList(to))
                return false;//private messages only between friends
            PrivateMessage message = new PrivateMessage(title, content, from, to);
            from.AddSentMessage(message);
            to.AddReceivedMessage(message);
            return true;
        }
    }
}
