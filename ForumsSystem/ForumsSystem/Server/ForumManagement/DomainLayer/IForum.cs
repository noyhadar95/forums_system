using ForumsSystem.Server.UserManagement.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    public interface IForum
    {
         bool InitForum(); //Needs to get Admins
        string getName();
        bool addSubForum(ISubForum subForum);

        List<ISubForum> GetSubForums();
            bool AddPolicy(Policy policy);
        void SetPolicy(Policy policy);

        void RemovePolicy(Policies policyType);

      //  public void EditForumProperties();
      //quick edit
         bool RegisterToForum(string userName, string password, string Email, DateTime dateOfBirth);
        bool RegisterToForum(IUser user);

         bool CreateSubForum(IUser creator, string subForumName);

         IUser Login(string userName, string password);
        void sendMail(string email, string userName, string subject, string body);
         ISubForum getSubForum(string subForumName);
        IUser getUser(string username);
        bool isUserMember(string username);
         Policy GetPolicy();
        int GetNumOfUsers();
        void DeleteUser(string userName);

        IUser GetWaitingUser(string username);
        bool AddWaitingUser(IUser user);
        Dictionary<string,string> GetAllUsers();
        List<Post> GetPostsByMember(string moderatorName);
        int GetNumOfPostsByUser(string username);

        IUser GetGuest(string guestName);


        List<IUser> getUsersInForum();

        bool CheckRegistrationPolicies(string password, DateTime dateOfBirth);



        bool ShouldNotify(string notifier, string username);

        void AddComplaint(string subforum, string username);
        void DeactivateUser (string username);
        bool isBanned(string userName);


    }
}
