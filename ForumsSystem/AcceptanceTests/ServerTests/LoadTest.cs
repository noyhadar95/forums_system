using AcceptanceTestsBridge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            bridge = ProxyBridge.GetInstance();
            Console.WriteLine("Starting Load Test...");
            Console.WriteLine("Initializing System...");
            if (!bridge.InitializeSystem("superadmin", "superadminpass"))
            {
                Console.WriteLine("Error While Initializing System - Stopping");
                return;
            }
            Console.WriteLine("System Initialized");
            string forumName = "LoadTestForum";
            Console.WriteLine("Creating Forum...");
            if (!CreateForum(forumName))
            {
                Console.WriteLine("Error While Creating Forum - Stopping");
                return;
            }
            try {
                numOfusers = int.Parse(args[0]);
                Console.WriteLine(numOfusers+" Users In The Forum");
            }
            catch(Exception e)
            {
                numOfusers = 100;
                Console.WriteLine(numOfusers + " Users In The Forum - Default Value");
            }
            Console.WriteLine("Creating Users...");
            int userIndex = 1;
            for (; userIndex<= numOfusers; userIndex++)
            {
                //create user
                bridge.RegisterToForum(forumName, "user" + userIndex, "pass" + userIndex, "user" + userIndex + "@gmail.com", DateTime.Today.AddYears(-30));
                bridge.ConfirmRegistration(forumName, "user" + userIndex);
                bridge.LoginUser(forumName, "user" + userIndex, "pass" + userIndex);
            }
            
            Console.WriteLine("Users Created");
            //create message
            //read message
            //wait 10 seconds


            //start task for each user:

            Thread[] threads = new Thread[numOfusers];
            userIndex = 1;
            for (; userIndex<=numOfusers; userIndex++)
            {
                int temp = userIndex;
                Console.WriteLine("!"+userIndex);
                threads[userIndex-1] = new Thread(() => UserTask(forumName, temp));
                threads[userIndex - 1].Start();
            }
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }
            Console.ReadLine();

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
            for (int i = 0; i < 20; i++)
            {


                int receiver = GetUserIndex(sender);
                Console.WriteLine(sender + " to " + receiver);
                bridge.SendPrivateMsg(forumName, "user" + sender, "user" + receiver, sender + " to " + receiver, sender + " to " + receiver);
                //TODO:read message if exists
                //Task.Delay(10000);
                Thread.Sleep(10 * 1000);
            }
        }

        private static int GetUserIndex(int sender)
        {
            int temp=rand.Next(1,numOfusers+1);
            while (temp == sender)
                temp = rand.Next(1,numOfusers+1);
            return temp;
        }
    }
}
