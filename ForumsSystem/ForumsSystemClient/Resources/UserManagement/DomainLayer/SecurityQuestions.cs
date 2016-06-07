using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.Resources.UserManagement.DomainLayer
{
    [DataContract]
    public enum SecurityQuestionsEnum
    {
        [EnumMember]
        firstSchool = 0,
        [EnumMember]
        firstPet =1
    }

    [DataContract]
    public class SecurityQuestions
    {
        [IgnoreDataMember]
        public static readonly string[] questions = { "What is the name of your first school",
        "What is the name of your first pet"
        };
    }
}
