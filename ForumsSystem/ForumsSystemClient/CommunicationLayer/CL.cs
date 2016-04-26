﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.CommunicationLayer
{
    class CL
    {

        public List<string> getForumsList()
        {
            List<string> res = new List<string>();
            res.Add("forum1");
            res.Add("forum2");
            res.Add("forum3");
            return res;
        }

        public List<string> getSubForumsList(string forumName)
        {
            List<string> res = new List<string>();
            res.Add("subforum1");
            res.Add("subforum2");
            res.Add("subforum3");
            return res;
        }

        // return a list of titles of all threads in the subforum.
        public List<string> getThreadsList(string forumName, string subForumName)
        {
            List<string> res = new List<string>();
            res.Add("thread1");
            res.Add("thread2");
            res.Add("thread3");
            return res;
        }

        public bool registerToForum(string forumName, string username, string password, string email, DateTime dob)
        {
            // TODO: implement

            return true;
        }

    }
}
