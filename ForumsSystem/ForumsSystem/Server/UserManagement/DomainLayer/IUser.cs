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
        PrivateMessage SendPrivateMessage(string reciever, string title, string content);
        void AddSentMessage(PrivateMessage privateMessage);
        void AddReceivedMessage(PrivateMessage privateMessage);
        bool RegisterToForum(string userName, string password, IForum forum, string email, DateTime dateOfBirth);

        Post postReply(Post parent, Thread thread, string title, string content);
        Thread createThread(ISubForum subForum, string title, string content);
        bool editPost(string title, string content, Post post);
        bool deletePost(Post post);
        void AddPostNotification(Post post);
        List<Post> GetPostNotifications();

        ISubForum createSubForum(string subForumName, Dictionary<string, DateTime> users);
        bool appointModerator(string userName, DateTime expirationTime, ISubForum subForum);
        bool removeModerator(string userName, ISubForum subForum);
        bool editExpirationTimeOfModerator(string userName, DateTime expirationTime, ISubForum subForum);

        bool isInWaitingList(IUser user);
        bool isInFriendsList(IUser user);
        void addToWaitingFriendsList(IUser user);
        void addToFriendsList(IUser user);
        void removeFromFriendList(IUser user);
        void addFriend(IUser friend);
        bool removeFriend(IUser friendToRemove);
        bool acceptFriend(IUser userToAccept);
        void removeFromWaitingFriendsList(IUser user);
        List<IUser> GetFriendsList();

        List<PrivateMessage> getReceivedMessages();
        List<PrivateMessage> getSentMessages();
        string getUsername();
        string getPassword();
        string getEmail();
        IForum getForum();
        Type getType();
        void AddToreceivedMessages(PrivateMessage privateMessage);
        void AddTosentMessages(PrivateMessage privateMessage);
        List<PrivateMessage> GetNotifications();
        void AddNotification(PrivateMessage newMessage);

        void Login();
        void LogOff();
        bool isLogin();

        bool SetForumProperties(IForum forum, Policy properties);
        bool ChangeForumProperties(IForum forum, Policy properties);
        bool DeleteForumProperties(IForum forum, List<Policies> properties);
    }
}
