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
        void ChangeType(Type type); 
        void SendPrivateMessage(string reciever, string title, string content);
        void AddSentMessage(PrivateMessage privateMessage);
        void AddReceivedMessage(PrivateMessage privateMessage);
        bool RegisterToForum(string userName, string password, IForum forum, string email);

        bool postReply(Post parent, Thread thread, string title, string content);
        bool createThread(ISubForum subForum, string title, string content);
        bool editPost(string title, string content, Post post);
        bool deletePost(Post post);

        bool isInWaitingList(IUser user);
        bool isInFriendsList(IUser user);
        void addToWaitingFriendsList(IUser user);
        void addToFriendsList(IUser user);
        void removeFromFriendList(IUser user);
        void addFriend(IUser friend);
        bool removeFriend(IUser friendToRemove);
        bool acceptFriend(IUser userToAccept);
        void removeFromWaitingFriendsList(IUser user);

        string getUsername();
        string getPassword();
        string getEmail();
        IForum getForum();
        void AddToreceivedMessages(PrivateMessage privateMessage);
        void AddTosentMessages(PrivateMessage privateMessage);
    }
}
