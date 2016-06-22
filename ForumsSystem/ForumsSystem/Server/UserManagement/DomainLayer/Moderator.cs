using ForumsSystem.Server.ForumManagement.Data_Access_Layer;
using ForumsSystem.Server.UserManagement.DomainLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(User))]
    public class Moderator
    {
        [IgnoreDataMember]
        public IUser user { get; private set; }
        [DataMember]
        public DateTime expirationDate { get; private set; }
        [IgnoreDataMember]
        public IUser appointer { get; private set; }

        public DateTime appointmentDate { get; private set; }

        public Moderator(IUser appointer, IUser user, DateTime expirationDate)
        {
            this.appointer = appointer;
            this.user = user;
            this.expirationDate = expirationDate;
            this.appointmentDate = DateTime.Today;
        }
        private Moderator()
        { 

        }

        public static Dictionary<string, Moderator> populateModerators(Forum forum, string subForumName)
        {
            Dictionary<string, Moderator> moderators = new Dictionary<string, Moderator>();
            DAL_Moderators dm = new DAL_Moderators();
            DataTable moderatorsTbl = dm.GetAllModerators(forum.getName(), subForumName);
            foreach (DataRow moderatorRow in moderatorsTbl.Rows)
            {
                Moderator mod = new Moderator();
               
                string userName = moderatorRow["UserName"].ToString();
                mod.user = forum.getDictionaryOfUsers()[userName];

                DateTime expirationDate = (DateTime)moderatorRow["ExpirationDate"];
                mod.expirationDate = expirationDate;

                string appointedUserName = moderatorRow["AppointerUserName"].ToString();
                mod.appointer = forum.getDictionaryOfUsers()[appointedUserName];

                //TODO: What about the appointmentDate

                moderators[userName] = mod;
            }

            return moderators;

        }
        public void changeExpirationDate(DateTime expirationDate)
        {
            this.expirationDate = expirationDate;
        }
        public bool CanBeDeletedBy(string user)
        {
            //only the appointer is able to delete the modeartor
            return appointer.getUsername().Equals(user);
        }

        public bool hasSeniority()
        {
            return (DateTime.Now - appointmentDate).TotalDays > 10;
        }
    }
}
