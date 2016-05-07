using ForumsSystemClient.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ForumsSystemClient.PresentationLayer
{
    class WindowHelper
    {
        // save the user in WindowHelper so all windows will know 
        // that the user is logged in.
        private static Dictionary<string, User> loggedUsers = new Dictionary<string, User>();
        private static SuperAdmin loggedSuperAdmin = null;


        public static string GetLoggedUsername(string forumName)
        {
            if (WindowHelper.IsLoggedSuperAdmin())
                return  WindowHelper.GetLoggedSuperAdmin().userName;
            else
                return WindowHelper.GetLoggedUser(forumName).Username;
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

        public static void LogoutAllUsers()
        {
            // forget all logged users by reseting the dict
            loggedUsers = new Dictionary<string, User>();
        }

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

        public static void SwitchWindow(Window oldWin, Window newWin)
        {
            newWin.Left = oldWin.Left;
            newWin.Top = oldWin.Top;
            newWin.Show();
            oldWin.Close();
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

    }
}
