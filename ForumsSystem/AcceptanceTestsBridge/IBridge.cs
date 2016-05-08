using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcceptanceTestsBridge
{
    public interface IBridge
    {

        #region Add/Create Methods

        bool CreateForum(string creator,string creatorPass, string forumName, List<UserStub> admins, PoliciesStub forumPolicies);

        bool CreateSubForum(string creator, string forumName, string subForumName, Dictionary<string, DateTime> moderators);

        // add thread also means adding the opening post of the thread
        int AddThread(string forumName, string subForumName, string publisher, string title, string content);

        int AddReplyPost(string forumName, string subForumName, int threadID, string publisher, int postID, string title, string content);

        bool AddModerator(string forumName, string subForumName, string adminUsername, KeyValuePair<string, DateTime> newMod);

        #endregion


        #region Delete Methods

        void DeleteUser(string forumName, string userName);

        void DeleteForum(string forumName);

        bool DeletePost(string forumName, string subForumName, int threadID, string deleter, int postID);

        #endregion


        #region Boolean Queries: IsExist, IsRegistered...

        bool IsExistForum(string forumName);

        bool IsRegisteredToForum(string username, string forumName);
        bool IsAdmin(string username, string forumName);

        bool IsModerator(string forumName, string subForumName, string username);
     
        bool IsExistThread(string forumName, string subForumName, int threadID);

        bool IsMsgReceived(string forumName, string username, string msgTitle, string msgContent);

        bool IsMsgSent(string forumName, string username, string msgTitle, string msgContent);

        bool IsForumHasPolicy(string forumName, PoliciesStub forumPolicy);

        #endregion


        // Other Methods

        bool SetForumProperties(string forumName, string username, PoliciesStub forumPolicies);

        bool RegisterToForum(string forumName, string username, string password, string email, DateTime dateOfBirth);

        // count the number of replies to a post recursively (i.e. including replies of replies adn so on)
        int CountNestedReplies(string forumName, string subForumName, int threadID, int postID);

        bool SendPrivateMsg(string forumName, string senderUsername, string receiverUsername, string msgTitle, string msgContent);

        bool EditModeratorExpDate(string forumName, string subForumName, string admin, string moderator, DateTime newDate);

        DateTime GetModeratorExpDate(string forumName, string subForumName, string username);

        bool LoginUser(string forumName, string username, string pass);

        bool LoginSuperAdmin(string username, string pass);

        // initializes the sytem with username, pass as the login info for the super admin
        bool InitializeSystem(string username, string pass);

        bool ConfirmRegistration(string forumName, string username);

        int GetOpenningPostID(string forumName, string subForumName, int threadID);

        bool ShouldCleanup(string className, string methodName);

        void AddFriend(string forumName, string username1, string username2);
        bool IsExistNotificationOfPost(string forumName, string username, int postId);
        void EditPost(string forumName, string subForumName, int threadId, string editor, int postId, string newTitle, string newContent);
       
        bool RemoveModerator(string forumName, string subForumName, string remover, string moderator);
        int GetNumOfPostsInForumByMember(string forumName, string adminUserName, string email);
        List<string> GetListOfModerators(string forumName, string subForumName, string adminUserName);
        void LogoutUser(string forumName, string username);
        List<Tuple<int, string, string>> GetPostsInForumByUser(string forumName, string adminUserName, string userEmail);
        int GetNumOfForums(string username,string password);//only superadmin can use this
        Dictionary<string, List<Tuple<string, string>>> GetMultipleUsersInfo(string userName, string password);//<email,List<forum,username>> - only superadmin can use this
        List<string> GetNotifications(string forumName, string username);
 
        List<Tuple<string, string, DateTime, string, List<int>>> ReportModeratorsDetails(string forumName, string adminUserName1);
    }
}
