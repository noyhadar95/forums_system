using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.PresentationLayer
{
    interface INotifiableWindow
    {

        void NotifyFriendRequests(int friendReqsNum);

        void NotifyPrivateMessages(int privateMsgsNum);


    }
}
