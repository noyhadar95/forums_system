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

            void RemovePolicy(Policies policyType);

      //  public void EditForumProperties();

         bool RegisterToForum(string userName, string password, string Email);


         void CreateSubForum(string subForumName);

         IUser Login(string userName, string password);

         ISubForum getSubForum(string subForumName);

    }
}
