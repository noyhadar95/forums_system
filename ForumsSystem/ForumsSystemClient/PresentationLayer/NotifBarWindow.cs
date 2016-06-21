using ForumsSystemClient.CommunicationLayer;
using ForumsSystemClient.Resources.UserManagement.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ForumsSystemClient.PresentationLayer
{
    /// <summary>
    /// Child class must call Initialize(dock) in constructor with it's DockPanel
    /// </summary>
    public abstract class NotifBarWindow : Window, INotifiableWindow
    {
        private const string FRIEND_REQUEST_MENU_HEADER = "_Friend Requests";
        private const string PRIVATE_MSG_MENU_HEADER = "_Private Messages";
        private const string TYPE_MENU_HEADER = "_logged in as ";
        private const string SESSION_TOKEN_MENU_HEADER = "_session token: ";

        protected CL cl;
        protected string forumName;
        protected string loggedUsername;

        protected Menu userMenuBar;
        protected MenuItem mi_type; // the menu item for displaying the user type on the user notifications bar
        protected MenuItem mi_sessionToken; // the menu item for displaying the user type on the user notifications bar

        // Friend Requests
        private MenuItem friendRequestsMenu;
        private string friendReqMenuHeader;
        // Private Msgs
        protected MenuItem privateMsgsMenu;
        private string privateMsgsMenuHeader;
        private bool notifiedPrivateMsg; // used in order to save in calls to CL

        protected string session_token;



        public NotifBarWindow(string forumName)
        {
            this.forumName = forumName;
            mi_type = new MenuItem();
            mi_sessionToken = new MenuItem();
            session_token = "";
            friendReqMenuHeader = FRIEND_REQUEST_MENU_HEADER;
            privateMsgsMenuHeader = PRIVATE_MSG_MENU_HEADER;

            if (WindowHelper.IsLoggedUser(forumName))
                loggedUsername = WindowHelper.GetLoggedUsername(forumName);
        }

        protected virtual void Initialize(DockPanel dock)
        {
            // create Friend Requests menu
            friendRequestsMenu = new MenuItem();
            friendRequestsMenu.Header = GetFriendReqMenuHeader();
            friendRequestsMenu.Click += new RoutedEventHandler(friendReqsMenu_Click);
            friendRequestsMenu.ToolTip = "See pending friend requests.";

            // create Private Msgs menu
            privateMsgsMenu = new MenuItem();
            privateMsgsMenu.Header = GetPrivateMsgMenuHeader();
            privateMsgsMenu.Click += new RoutedEventHandler(privateMsgsMenu_Click);
            privateMsgsMenu.ToolTip = "See pending messages.";

            userMenuBar = new Menu();
            userMenuBar.Visibility = Visibility.Visible;
            userMenuBar.Items.Add(friendRequestsMenu);
            userMenuBar.Items.Add(privateMsgsMenu);
            userMenuBar.Items.Add(mi_type);
            userMenuBar.Items.Add(mi_sessionToken);

            //// Create a linear gradient brush with five stops 
            //LinearGradientBrush fourColorLGB = new LinearGradientBrush();
            //fourColorLGB.StartPoint = new Point(0, 0);
            //fourColorLGB.EndPoint = new Point(0, 1);

            //// Create and add Gradient stops
            //GradientStop stop1 = new GradientStop();
            //stop1.Color = (Color)ColorConverter.ConvertFromString("#FF5978D6");
            //stop1.Offset = 0.01;
            //fourColorLGB.GradientStops.Add(stop1);

            //GradientStop stop2 = new GradientStop();
            //stop2.Color = (Color)ColorConverter.ConvertFromString("#FF9BACCF");
            //stop2.Offset = 1;
            //fourColorLGB.GradientStops.Add(stop2);

            //GradientStop stop3 = new GradientStop();
            //stop3.Color = (Color)ColorConverter.ConvertFromString("#FFB3D8EA");
            //stop3.Offset = 0.528;
            //fourColorLGB.GradientStops.Add(stop3);

            //GradientStop stop4 = new GradientStop();
            //stop4.Color = (Color)ColorConverter.ConvertFromString("#FF293991");
            //stop4.Offset = 1;
            //fourColorLGB.GradientStops.Add(stop4);


            //userMenuBar.Background = fourColorLGB;


            DockPanel.SetDock(userMenuBar, Dock.Top);
            dock.Children.Insert(0, userMenuBar);
        }

        protected void ResetHeaders()
        {
            friendReqMenuHeader = FRIEND_REQUEST_MENU_HEADER;
            privateMsgsMenuHeader = PRIVATE_MSG_MENU_HEADER;
        }

        protected void RefreshNotificationsBar(string username)
        {
            string type = cl.GetUserType(forumName, username);
            mi_type.Header = TYPE_MENU_HEADER + type;
            if (session_token != "")
                mi_sessionToken.Header = SESSION_TOKEN_MENU_HEADER + session_token;
            else
            {
                string session_token = cl.GetSessionKey(username, forumName);
                mi_sessionToken.Header = SESSION_TOKEN_MENU_HEADER + session_token;
            }

            // refresh friend requests menu bar
            RefreshFriendReqsMenu();
            // refresh private msgs menu bar
            RefreshPrivateMsgMenu();
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
            System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke((Action)(() =>
            {
                MessageBox.Show("notify 111111");
                SetFriendReqMenuHeaderOn(friendReqsNum);

                MessageBox.Show("notify friend request: count=" + friendReqsNum);

                friendRequestsMenu.Header = GetFriendReqMenuHeader();
                MessageBox.Show("notify 333333333333");
                friendRequestsMenu.Items.Clear();

                MessageBox.Show("NotifyFriendRequests: header=" + GetFriendReqMenuHeader());

                //RefreshFriendReqsMenu();
                userMenuBar.Items.Refresh();
            }));


            //this.Invoke((MethodInvoker)delegate {
            //    SetFriendReqMenuHeaderOn(friendReqsNum);

            //    MessageBox.Show("notify friend request: count=" + friendReqsNum);

            //    friendRequestsMenu.Header = GetFriendReqMenuHeader();
            //    friendRequestsMenu.Items.Clear();

            //    MessageBox.Show("NotifyFriendRequests: header=" + GetFriendReqMenuHeader());

            //    //RefreshFriendReqsMenu();
            //    userMenuBar.Items.Refresh();
            //});
        }

        protected void RefreshFriendReqsMenu()
        {
            MessageBox.Show("RefreshFriendReqsMenu: header=" + GetFriendReqMenuHeader());
            friendRequestsMenu.Items.Clear();
            friendRequestsMenu.Header = GetFriendReqMenuHeader();
        }

        private void friendReqsMenu_Click(object sender, RoutedEventArgs e)
        {
            friendRequestsMenu.Items.Clear();
            //MessageBox.Show("bring reqs from db");
            List<string> cl_friendReqs = cl.GetFriendRequests(forumName, loggedUsername);
            foreach (string str in cl_friendReqs)
            {
                MenuItem mi = new MenuItem();
                mi.Header = str;
                mi.Click += new RoutedEventHandler(friendReq_Click);
                friendRequestsMenu.Items.Add(mi);
            }
            friendRequestsMenu.IsSubmenuOpen = true;

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
            privateMsgsMenu.Items.Clear();
            //MessageBox.Show("bring PM's from db");
            List<PrivateMessageNotification> cl_privateMsgs = cl.GetPrivateMessageNotifications(forumName, loggedUsername);
            foreach (PrivateMessageNotification pm in cl_privateMsgs)
            {
                MenuItem mi = new MenuItem();
                mi.Header = pm.sender;
                mi.Click += new RoutedEventHandler(privateMsg_Click);
                privateMsgsMenu.Items.Add(mi);
            }
            privateMsgsMenu.IsSubmenuOpen = true;
        }

        private void privateMsg_Click(object sender, RoutedEventArgs e)
        {
            // TODO::
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





    }
}
