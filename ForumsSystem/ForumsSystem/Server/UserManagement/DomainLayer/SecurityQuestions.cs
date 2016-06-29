using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.UserManagement.DomailLayer
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
        public static readonly string[] questions = { "Name of First School",
        "Name of First Pet"
        };
    }
}
