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


            bool AddPolicy(Policy policy);
        void SetPolicy(Policy policy);

        void RemovePolicy(Policies policyType);

      //  public void EditForumProperties();
      //quick edit
         bool RegisterToForum(string userName, string password, string Email);
        bool RegisterToForum(IUser user);

         void CreateSubForum(IUser creator, string subForumName);

         IUser Login(string userName, string password);

         ISubForum getSubForum(string subForumName);
        IUser getUser(string username);
        bool isUserMember(string username);
         Policy GetPolicy();
        int GetNumOfUsers();

    }
}
