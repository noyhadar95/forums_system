﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using WebApplication.Resources.UserManagement.DomainLayer;

namespace WebApplication.Resources.ForumManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(User))]
    //[KnownType(typeof(SubForum))]
    public class Forum
    {
        [DataMember]
        public string name { get; set; }
        // [DataMember]
        // private List<SubForum> sub_forums;
        //  [DataMember]
        //  private Policy policies;
        [IgnoreDataMember]
        private Dictionary<string, User> users;//username, user
        [IgnoreDataMember]
        private Dictionary<string, User> waiting_users;//username, user - waiting for confirmation

        /* public List<SubForum> Sub_forums
         {
             get
             {
                 return sub_forums;
             }

             set
             {
                 sub_forums = value;
             }
         }*/

        /* public Policy Policies
         {
             get
             {
                 return policies;
             }

             set
             {
                 policies = value;
             }
         }*/
        public Forum()
        {

        }

        public string getName()
        {
            return name;
        }
        /* public SubForum getSubForum(string subForumName)
         {
             foreach (SubForum subForum in sub_forums)
             {
                 if (subForum.getName().Equals(subForumName))
                     return subForum;
             }
             return null;
         }*/

        public User getUser(string username)
        {
            if (this.users.ContainsKey(username))
                return this.users[username];
            return null;
        }
        /*   public Policy GetPolicy()
           {
               return this.policies;
           }*/
    }
}