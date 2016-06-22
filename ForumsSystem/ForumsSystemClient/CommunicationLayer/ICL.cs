using ForumsSystemClient.Resources.ForumManagement.DomainLayer;
using ForumsSystemClient.Resources.UserManagement.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.CommunicationLayer
{
    public interface ICL
    {
        #region Use Cases Methods

        Post AddReply(string forumName, string subForumName, int threadID, string publisherName, int postID, string title, string content);

        int AddThread(string forumName, string subForumName, string publisher, string title, string content);
        bool ChangeExpirationDate(string forumName, string subForumName, string admin, string moderator, DateTime newDate);

        bool ChangeForumProperties(string username, string forumName, Policy properties);

        Forum CreateForum(string creatorName, string password, string name, Policy properties, List<User> adminUsername);

        SubForum CreateSubForum(string creator, string forumName, string subforumName, Dictionary<string, DateTime> moderators);

       // bool DeletePost(User deleter, Post post);

        bool InitializeSystem(string username, string pass);
        bool IsInitialized();

        Tuple<User, string> MemberLogin(string forumName, string username, string password);
        Tuple<User, string> MemberLogin(string forumName, string username, string password, string sessionToken);
        bool RegisterToForum(string forumName, string guestName, string password, string email, DateTime dob);


        bool SendPrivateMessage(string forumName, string from, string to, string title, string content);

        bool SetForumProperties(string username, string forumName, Policy properties);

        #endregion


        // other methods
        
        bool DeleteForumProperties(string deleter, string forumName, List<Policies> properties);

        Forum GetForum(string forumName);

        bool AddModerator(string forumName, string subForumName, string adminUsername, string username, DateTime expiratoinDate);
       // void removeForum(string forumName);
        bool ConfirmRegistration(string forumName, string username);
        bool LoginSuperAdmin(string username, string pass);
        DateTime GetModeratorExpDate(string forumName, string subForumName, string username);
        int CountNestedReplies(string forumName, string subForumName, int threadID, int postID);
        bool IsMsgSent(string forumName, string username, string msgTitle, string msgContent);
        bool IsMsgReceived(string forumName, string username, string msgTitle, string msgContent);
        bool IsModerator(string forumName, string subForumName, string username);
        bool IsRegisteredToForum(string username, string forumName);
        bool IsExistForum(string forumName);
        bool IsExistThread(string forumName, string subForumName, int threadID);
        bool IsExistUser(string forumName, string username);
        bool DeletePost(string forumName, string subForumName, int threadID, string deleter, int postID);
        void DeleteForum(string forumName);
        void DeleteUser(string userName, string forumName);
        int GetOpenningPostID(string forumName, string subForumName, int threadID);

        bool IsAdmin(string username, string forumName);
        //Dictionary<string, List<Tuple<string, string>>> GetMultipleUsersInfo();
        // int GetNumOfForums();
      //  List<PrivateMessageNotification> GetNotifications(string forumName, string username);
        int GetNumOfForums(string userName, string password);

        Dictionary<string, List<Tuple<string, string>>> GetMultipleUsersInfoBySuperAdmin(string userName, string password);
        void AddFriend(string forumName, string username1, string username2);

        List<Tuple<string, string, DateTime, string, List<Post>>> ReportModeratorsDetails(string forumName, string adminUserName1);
        void MemberLogout(string forumName, string username);
        //  Tuple<string, string, DateTime, string> GetModeratorAppointmentsDetails(string forumName, string subForumName, string adminUserName1, string username1);
        List<Post> GetPosts(string forumName, string subforumName, int threadId);
        bool CheckIfPolicyExists(string forumName, Policies policy);
       // List<PostNotification> GetPostNotifications(string forumName, string username);
        void EditPost(string forumName, string subForumName, int threadId, string editor, int postId, string newTitle, string newContent);
        bool RemoveModerator(string forumName, string subForumName, string remover, string moderatorName);
        int ReportNumOfPostsByMember(string adminUsername, string forumName, string username);
        List<string> GetModeratorsList(string forumName, string subForumName, string adminUserName);
        List<Post> ReportPostsByMember(string forumName, string adminUserName, string username);
        SuperAdmin GetSuperAdmin();
        List<string> GetForumMembers(string forumName);
        List<string> GetThreadsList(string forumName, string subForumName);
        Dictionary<int, string> GetThreads(string forumName, string subForumName);
        List<string> GetSubForumsList(string forumName);
        List<string> GetForumsList();

        bool IgnoreFriend(string forumName, string userName, string userToIgnore);

        void AcceptFriendRequest(string forumName, string accepter, string toAccept);
        void SendFriendRequest(string forumName, string sender, string reciever);
        List<string> GetUsersNotFriends(string forumName,string username);
        
        List<string> GetFriendRequests(string forumName, string username);

        bool SetUserPassword(string forumName, string username, string newPassword);
        bool AddSecurityQuestion(string forumName, string username, SecurityQuestionsEnum question, string answer);
        bool RemoveSecurityQuestion(string forumName, string username, SecurityQuestionsEnum question);
        bool CheckSecurityQuestion(string forumName, string username, SecurityQuestionsEnum question, string answer);
        List<PrivateMessageNotification> GetPrivateMessageNotifications(string forumName, string username);
    }
}
