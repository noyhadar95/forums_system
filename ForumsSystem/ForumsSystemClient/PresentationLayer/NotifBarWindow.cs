using ForumsSystemClient.CommunicationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ForumsSystemClient.PresentationLayer
{
    /// <summary>
    /// Child class must call Initialize(dock) in constructor with it's DockPanel
    /// </summary>
    public abstract class NotifBarWindow : Window, INotifiableWindow
    {
        private const string FRIEND_REQUEST_MENU_HEADER = "_Friend Requests";
        private const string PRIVATE_MSG_MENU_HEADER = "_Private Messages";

        protected CL cl;
        protected string forumName;
        protected string loggedUsername;

        protected Menu userMenuBar;
        protected MenuItem mi_type; // the menu item for displaying the user type on the user notifications bar

        // Friend Requests
        protected MenuItem friendRequestsMenu;
        private string friendReqMenuHeader = FRIEND_REQUEST_MENU_HEADER;
        private bool notifiedFriendReq; // used in order to save in calls to CL
        // Private Msgs
        protected MenuItem privateMsgsMenu;
        private string privateMsgsMenuHeader = PRIVATE_MSG_MENU_HEADER;


        public NotifBarWindow(string forumName)
        {
            this.forumName = forumName;
            mi_type = new MenuItem();
            notifiedFriendReq = true;

            if (WindowHelper.IsLoggedUser(forumName))
                loggedUsername = WindowHelper.GetLoggedUsername(forumName);
        }

        protected virtual void Initialize(DockPanel dock)
        {
            // create Friend Requests menu
            friendRequestsMenu = new MenuItem();
            friendRequestsMenu.Header = GetFriendReqMenuHeader();
            friendRequestsMenu.Click += new RoutedEventHandler(friendReqsMenu_Click);

            // create Private Msgs menu
            privateMsgsMenu = new MenuItem();
            privateMsgsMenu.Header = GetPrivateMsgMenuHeader();
            privateMsgsMenu.Click += new RoutedEventHandler(friendReqsMenu_Click);

            userMenuBar = new Menu();
            userMenuBar.Visibility = Visibility.Visible;
            userMenuBar.Items.Add(friendRequestsMenu);

            DockPanel.SetDock(userMenuBar, Dock.Top);
            dock.Children.Insert(0, userMenuBar);
        }

        protected void RefreshNotificationsBar(string username)
        {
            string type = cl.GetUserType(forumName, username);
            mi_type.Header = "logged in as " + type;
            if (userMenuBar.Items.Count < 2)
                userMenuBar.Items.Add(mi_type);

            // refresh friend requests menu bar
            RefreshFriendReqsMenu();
        }


        #region friend requests

        private string GetFriendReqMenuHeader()
        {
            return friendReqMenuHeader;
        }

        private void SetFriendReqMenuHeaderOn(int notifNum)
        {
            friendReqMenuHeader = FRIEND_REQUEST_MENU_HEADER + "(" + notifNum + ")";
        }

        public void NotifyFriendRequests(int friendReqsNum)
        {
            notifiedFriendReq = true;
            SetFriendReqMenuHeaderOn(friendReqsNum);
            MessageBox.Show("notify friend request");
            System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke((Action)(() =>
            {
                friendRequestsMenu.Items.Clear();
            }));

            RefreshFriendReqsMenu();
            userMenuBar.Items.Refresh();
        }

        protected void RefreshFriendReqsMenu()
        {
            friendRequestsMenu.Items.Clear();
            friendRequestsMenu.Header = GetFriendReqMenuHeader();
        }

        private void friendReqsMenu_Click(object sender, RoutedEventArgs e)
        {
            if (notifiedFriendReq)
            {
                MessageBox.Show("bring reqs from db");
                List<string> cl_friendReqs = cl.GetFriendRequests(forumName, loggedUsername);
                foreach (string str in cl_friendReqs)
                {
                    MenuItem mi = new MenuItem();
                    mi.Header = str;
                    mi.Click += new RoutedEventHandler(friendReq_Click);
                    friendRequestsMenu.Items.Add(mi);
                }
                notifiedFriendReq = false;
            }

        }

        private void friendReq_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you accept the friend request?",
                                                    "Confirmation", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
            {
                MenuItem mi = sender as MenuItem;
                string requestSender = (string)mi.Header;
                MessageBox.Show("loggedUsername = " + loggedUsername + "\nrequestSender = " + requestSender);
                // accept friend request
                cl.AcceptFriendRequest(forumName, loggedUsername, requestSender);

            }
            else if (result == MessageBoxResult.No)
            {
                // ignore friend request


            }
            else
            {
                // canel, do nothing
            }
        }

        #endregion


        #region private messages

        private string GetPrivateMsgMenuHeader()
        {
            return privateMsgsMenuHeader;
        }

        private void SetPrivateMsgMenuHeaderOn(int notifNum)
        {
            privateMsgsMenuHeader = PRIVATE_MSG_MENU_HEADER + "(" + notifNum + ")";
        }

        public void NotifyPrivateMessages(int privateMsgsNum)
        {
            SetPrivateMsgMenuHeaderOn(privateMsgsNum);
            MessageBox.Show("notify PM");
            System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke((Action)(() =>
            {
                privateMsgsMenu.Items.Clear();
            }));

            RefreshPrivateMsgMenu();
            userMenuBar.Items.Refresh();
        }

        protected void RefreshPrivateMsgMenu()
        {
            privateMsgsMenu.Items.Clear();
            privateMsgsMenu.Header = GetPrivateMsgMenuHeader();
        }

        private void privateMsgsMenu_Click(object sender, RoutedEventArgs e)
        {
            //if (notifiedPrivateMsg)
            //{
            //    MessageBox.Show("bring PM's from db");
            //    List<string> cl_privateMsgs = cl.(forumName, loggedUsername);
            //    foreach (string str in cl_privateMsgs)
            //    {
            //        MenuItem mi = new MenuItem();
            //        mi.Header = str;
            //        mi.Click += new RoutedEventHandler(friendReq_Click);
            //        privateMsgsMenu.Items.Add(mi);
            //    }
            //    notifiedPrivateMsg = false;
            //}

        }

        #endregion





    }
}
