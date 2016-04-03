using ForumsSystem.Server.ForumManagement.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.UserManagement.DomainLayer
{
    public interface IUser
    {
        bool ChangeType(); // Type object?
        void SendPrivateMessage(IUser reciever, string title, string content);
        void AddSentMessage(PrivateMessage privateMessage);
        void AddReceivedMessage(PrivateMessage privateMessage);
        bool RegisterToForum();

    }
}
