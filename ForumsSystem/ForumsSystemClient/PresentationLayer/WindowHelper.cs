using ForumsSystemClient.Resources.ForumManagement.DomainLayer;
using ForumsSystemClient.Resources.UserManagement.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ForumsSystemClient.PresentationLayer
{
    class WindowHelper
    {
        // save the user in WindowHelper so all windows will know 
        // that the user is logged in.
        private static Dictionary<string, User> loggedUsers = new Dictionary<string, User>();
        private static SuperAdmin loggedSuperAdmin = null;
        private static INotifiableWindow currentNotifyWin;


        // not including super admin
        public static string GetLoggedUsername(string forumName)
        {
            if (IsLoggedUser(forumName))
                return GetLoggedUser(forumName).Username;
            else
                return null;
        }

        public static bool IsLoggedUser(string forumName)
        {
            return (loggedUsers.ContainsKey(forumName) && loggedUsers[forumName] != null);
        }

        public static User GetLoggedUser(string forumName)
        {
            if (loggedUsers.ContainsKey(forumName))
                return loggedUsers[forumName];
            else
                return null;
        }

        public static void SetLoggedUser(string forumName, User user)
        {
            if (loggedUsers.ContainsKey(forumName))
                loggedUsers[forumName] = user;
            else
                loggedUsers.Add(forumName, user);
        }

        public static void LogoutUser(string forumName)
        {
            SetLoggedUser(forumName, null);
        }

        //public static void LogoutAllUsers()
        //{
        //    // forget all logged users by reseting the dict
        //    loggedUsers = new Dictionary<string, User>();
        //}

        public static bool IsLoggedSuperAdmin()
        {
            return loggedSuperAdmin != null;
        }

        public static SuperAdmin GetLoggedSuperAdmin()
        {
            return loggedSuperAdmin;
        }

        public static void SetLoggedSuperAdmin(SuperAdmin sa)
        {
            loggedSuperAdmin = sa;
        }

        public static void LogoutSuperAdmin()
        {
            loggedSuperAdmin = null;
        }

        public static void SetCurrentWindow(INotifiableWindow newWin)
        {
            currentNotifyWin = newWin;
        }

        public static void SwitchWindow(Window oldWin, Window newWin)
        {
            newWin.Left = oldWin.Left;
            newWin.Top = oldWin.Top;
            newWin.Show();
            oldWin.Close();

            // handle INotifiableWindow
            if (newWin is INotifiableWindow)
                currentNotifyWin = (INotifiableWindow)newWin;
        }

        // show newWin without closing oldWin.
        public static void ShowWindow(Window oldWin, Window newWin)
        {
            newWin.Left = oldWin.Left;
            newWin.Top = oldWin.Top;
            newWin.Show();
        }

        public static void SetWindowBGImg(Window win)
        {
            Style style = Application.Current.FindResource("WindowStyle") as Style;
            win.Style = style;
        }

        public static void NotifyFriendRequests(int friendReqsNum)
        {
            if (currentNotifyWin != null)
            {
                currentNotifyWin.NotifyFriendRequests(friendReqsNum);
            }
        }


    }
}
