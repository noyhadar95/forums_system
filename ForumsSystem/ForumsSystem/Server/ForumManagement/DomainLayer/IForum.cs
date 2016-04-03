using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    interface IForum
    {
         bool InitForum(); //Needs to get Admins

         bool AddPolicy();

         bool RemovePolicy();

      //  public void EditForumProperties();

         bool RegisterToForum(string userName, string password, String Email);

         bool CreateSubForum(string subForumName);

         bool Login(string userName, string password);
    }
}
