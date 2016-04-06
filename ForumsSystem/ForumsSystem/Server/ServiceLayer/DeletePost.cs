using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.ForumManagement.DomainLayer;
using ForumsSystem.Server.UserManagement.DomainLayer;

namespace ForumsSystem.Server.ServiceLayer
{
    public class DeletePost
    {
        public static bool Delete(IUser deleter,Post post)
        {
            //TODO: check that user can delete the post

            post.DeletePost();
            if (post.Thread.GetOpeningPost() == null)
            {
                post.Thread.GetSubforum().removeThread(post.Thread);
            }
            return true;
        }
    }
}
