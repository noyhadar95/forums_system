using ForumsSystem.Server.ForumManagement.Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    public class System
    {
       // private SuperAdmin superAdmin;
        private Dictionary<string, IForum> forums; //name, forum

        public System()
        {
         //   this.superAdmin = SuperAdmin.CreateSuperAdmin("root", "toor", this);
            forums = new Dictionary<string, IForum>();
        }

      //  public void changeAdminUserName(string userName)
      //  {
      //      this.superAdmin.userName = userName;
    //        Loggers.Logger.GetInstance().AddActivityEntry("The Super Admin UserName has been changed to: " + userName);
  //      }
        //public void changeAdminPassword(string password)
        //{
        //    this.superAdmin.password = password;
        //    Loggers.Logger.GetInstance().AddActivityEntry("The Super Admin password has been changed");
        //}
        public Forum createForum(string forumName)
        {
            try
            {
                Forum forum = new Forum(forumName);
                forums.Add(forumName, forum);
                Loggers.Logger.GetInstance().AddActivityEntry("A new forum: " + forumName + " has been created");
                return forum;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IForum getForum(string forumName)
        {
            if (!forums.ContainsKey(forumName))
                return null;
            return forums[forumName];
        }

        public void removeForum(string forumName)
        {
            if (forums.ContainsKey(forumName))
            {
                DAL_Forum dal_forum = new DAL_Forum();
                dal_forum.DeleteForum(forumName);
                forums.Remove(forumName);
                Loggers.Logger.GetInstance().AddActivityEntry("The forum: " + forumName + " has been removed");
            } 
        }
        public  Dictionary<string, List<Tuple<string, string>>> GetMultipleUsersInfo()
        {
            Dictionary<string, List<Tuple<string, string>>> info =
                new Dictionary<string, List<Tuple<string, string>>>(); //<email,List<forum,username>>
            Dictionary<string, IForum> currForums = new Dictionary<string, IForum>();
            foreach (KeyValuePair<string, IForum> item in forums)
            {
                currForums.Add(item.Key, item.Value);
            }
            foreach (KeyValuePair<string, IForum> item in currForums)
            {
                IForum currForum = item.Value;
                foreach (KeyValuePair<string, string> user in currForum.GetAllUsers() ?? new Dictionary<string, string>())//<username,email>
                {
                    List<Tuple<string, string>> userInfo = null;
                    if (!info.TryGetValue(user.Value, out userInfo))//if mail does not exist
                        info.Add(user.Value, new List<Tuple<string, string>>());//add it
                    //add the new user
                    info.TryGetValue(user.Value, out userInfo);
                    userInfo.Add(new Tuple<string, string>(item.Key, user.Key));

                }
            }
            return info;
        }
        public int GetNumOfForums()
        {
            return forums.Count;
        }


        public static System populateSystem()
        {
            System sys = new System();
            DAL_Forum dforum = new DAL_Forum();
            var tbl = dforum.GetAllForums();
            foreach (var forumRow in tbl.Rows)
            {
                Forum f = 
                sys.forums.Add()
            }
        }
    }
}
