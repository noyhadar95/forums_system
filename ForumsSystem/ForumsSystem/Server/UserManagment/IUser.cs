using ForumsSystem.Server.ForumManagement.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.UserManagment
{
    interface IUser
    {
        bool ChangeType(); // Type object?
        bool SendPrivateMessage(IUser reciever /* ,PrivateMessage message*/);
        bool AddSentMessage( /* ,PrivateMessage message*/);
        bool AddReceivedMessage( /* ,PrivateMessage message*/);
        bool RegisterToForum();

    }
}
