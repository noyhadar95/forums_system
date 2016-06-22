using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.CommunicationLayer
{
    public class NotificationHelper
    {
        static bool gotFriendRequest = false;
        static bool gotPrivateMessage = false;

        public static bool recievedNotification()
        {
            return gotFriendRequest || gotPrivateMessage;
        }

        public static void recieveFriendRequest()
        {
            gotFriendRequest = true;
        }

        public static void recievePrivateMessage()
        {
            gotPrivateMessage = true;
        }

        public static void CleanUp()
        {
            gotFriendRequest = false;
            gotPrivateMessage = false;
        }
    }
}
