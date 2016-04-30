using AcceptanceTestsBridge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcceptanceTests.ServerTests
{
    class LoadTest
    {
        private static IBridge bridge;
        private static int numOfusers;
        private static Random rand = new Random();
        static void Main(string[] args)
        {
            string forumName = "LoadTestForum";
            bridge = ProxyBridge.GetInstance();
            if (!CreateForum(forumName))
            {
                //error creating the forum
                return;
            }
            numOfusers = int.Parse(args[0]);
            int userIndex = 1;
            for (; userIndex<= numOfusers; userIndex++)
            {
                //create user
                bridge.RegisterToForum(forumName, "user" + userIndex, "pass" + userIndex, "user" + userIndex + "@gmail.com", DateTime.Today.AddYears(-30));
                bridge.ConfirmRegistration(forumName, "user" + userIndex);
                bridge.LoginUser(forumName, "user" + userIndex, "pass" + userIndex);
            }
            //create message
            //read message
            //wait 10 seconds


            //start task for each user:
            Task task;
            userIndex = 1;
            while (userIndex<=numOfusers)
            {
                Task.Run(() => UserTask(forumName, userIndex));
                userIndex++;
            }


        }

        private static bool CreateForum(string forumName)
        {
            
            string adminUserName1 = "adm1", adminUserName2 = "adm2";
            string adminPass1 = "root1", adminPass2 = "root2";
            string adminEmail1 = "adm1@gmail.com", adminEmail2 = "adm2@gmail.com";
            string superAdminUsername = "superadmin";
            string superAdminPass = "superadminpass";
        PoliciesStub forumPolicy = PoliciesStub.Password;
            List<UserStub> admins = new List<UserStub>();
            UserStub user1 = new UserStub(adminUserName1, adminPass1, adminEmail1, forumName);
            UserStub user2 = new UserStub(adminUserName2, adminPass2, adminEmail2, forumName);
            admins.Add(user1);
            admins.Add(user2);

            // create the forum
            bool res = bridge.CreateForum(superAdminUsername, forumName, admins, forumPolicy);
            return res;
        }

        private static void UserTask(string forumName, int sender)
        {
            int receiver = GetUserIndex(sender);
            bridge.SendPrivateMsg(forumName, "user" + sender, "user" + receiver, sender + " to " + receiver, sender + " to " + receiver);
            //TODO:read message if exists
            Task.Delay(10 * 1000);
        }

        private static int GetUserIndex(int sender)
        {
            int temp=rand.Next(numOfusers+1);
            while (temp == sender)
                temp = rand.Next(numOfusers + 1);
            return temp;
        }
    }
}
