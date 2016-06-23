using ForumsSystemClient.CommunicationLayer;
using ForumsSystemClient.Resources.UserManagement.DomainLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
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
        private string _friendReqMenuHeader;
        private string FriendReqMenuHeader
        {
            get { return _friendReqMenuHeader; }
            set { _friendReqMenuHeader = value; isFriendReqMenuHeaderChanged = true; }
        }
        protected bool isFriendReqMenuHeaderChanged = false;
        // Private Msgs
        protected MenuItem privateMsgsMenu;
        private string _privateMsgsMenuHeader;
        private string PrivateMsgsMenuHeader
        {
            get { return _privateMsgsMenuHeader; }
            set { _privateMsgsMenuHeader = value; isPrivateMsgsMenuHeaderChanged = true; }
        }
        protected bool isPrivateMsgsMenuHeaderChanged = false;
        private bool notifiedPrivateMsg; // used in order to save in calls to CL

        protected string session_token;



        public NotifBarWindow(string forumName)
        {
            this.forumName = forumName;
            mi_type = new MenuItem();
            mi_sessionToken = new MenuItem();
            session_token = "";
            FriendReqMenuHeader = FRIEND_REQUEST_MENU_HEADER;
            PrivateMsgsMenuHeader = PRIVATE_MSG_MENU_HEADER;

            if (WindowHelper.IsLoggedUser(forumName))
                loggedUsername = WindowHelper.GetLoggedUsername(forumName);


            // on mouse move the notif bar checks if a new nitification has been recieved
            this.MouseMove += NotifBarWindow_MouseMove;
        }

        private void NotifBarWindow_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (isFriendReqMenuHeaderChanged)
            {
                lock (this)
                {
                    RefreshFriendReqsMenu();
                    isFriendReqMenuHeaderChanged = false;
                }
            }
            if (isPrivateMsgsMenuHeaderChanged)
            {
                lock (this)
                {
                    RefreshPrivateMsgMenu();
                    isPrivateMsgsMenuHeaderChanged = false;
                }
            }
        }


        protected virtual void Initialize(DockPanel dock)
        {
            // create Friend Requests menu
            friendRequestsMenu = new MenuItem();
            friendRequestsMenu.Header = GetFriendReqMenuHeader();
            //Binding b = new Binding();
            //b.Path = new PropertyPath("friendReqMenuHeader");
            //b.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            //BindingOperations.SetBinding(friendRequestsMenu, MenuItem.HeaderProperty, b);
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


            DockPanel.SetDock(userMenuBar, Dock.Top);
            dock.Children.Insert(0, userMenuBar);
        }

        protected void ResetHeaders()
        {
            FriendReqMenuHeader = FRIEND_REQUEST_MENU_HEADER;
            PrivateMsgsMenuHeader = PRIVATE_MSG_MENU_HEADER;
        }

        protected void RefreshNotificationsBar(string username)
        {
            // handle user type menu item
            string type = cl.GetUserType(forumName, username);
            mi_type.Header = TYPE_MENU_HEADER + type;

            // handle session token menu item
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
            return FriendReqMenuHeader;
        }

        private void SetFriendReqMenuHeaderOn(int notifNum)
        {
            FriendReqMenuHeader = FRIEND_REQUEST_MENU_HEADER + "(" + notifNum + ")";
        }

        private void SetFriendReqMenuHeaderMinus1()
        {
            if (FriendReqMenuHeader != FRIEND_REQUEST_MENU_HEADER)
            {
                // FriendReqMenuHeader is of the form: FRIEND_REQUEST_MENU_HEADER(notifNum)
                char notifNum = FriendReqMenuHeader[FRIEND_REQUEST_MENU_HEADER.Length + 1];
                try
                {
                    int num = int.Parse(notifNum.ToString());
                    FriendReqMenuHeader = FRIEND_REQUEST_MENU_HEADER + "(" + (num - 1) + ")";
                }
                catch (Exception)
                {
                    return;
                }
            }
        }

        public void NotifyFriendRequests(int friendReqsNum)
        {
            System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke((Action)(() =>
            {
                SetFriendReqMenuHeaderOn(friendReqsNum);

                //friendRequestsMenu.Header = GetFriendReqMenuHeader();
                //isFriendReqMenuHeaderChanged = true;

                //RefreshFriendReqsMenu();
                userMenuBar.Items.Refresh();
            }));

        }

        protected void RefreshFriendReqsMenu()
        {
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
                // accept friend request
                cl.AcceptFriendRequest(forumName, loggedUsername, requestSender);
                SetFriendReqMenuHeaderMinus1();
            }
            else if (result == MessageBoxResult.No)
            {
                MenuItem mi = sender as MenuItem;
                string requestSender = (string)mi.Header;
                // ignore friend request
                cl.IgnoreFriend(forumName, loggedUsername, requestSender);
                SetFriendReqMenuHeaderMinus1();
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
            return PrivateMsgsMenuHeader;
        }

        private void SetPrivateMsgMenuHeaderOn(int notifNum)
        {
            PrivateMsgsMenuHeader = PRIVATE_MSG_MENU_HEADER + "(" + notifNum + ")";
        }

        private void SetPrivateMsgMenuHeaderMinus1()
        {
            if (PrivateMsgsMenuHeader != FRIEND_REQUEST_MENU_HEADER)
            {
                // FriendReqMenuHeader is of the form: FRIEND_REQUEST_MENU_HEADER(notifNum)
                char notifNum = PrivateMsgsMenuHeader[PRIVATE_MSG_MENU_HEADER.Length + 1];
                try
                {
                    int num = int.Parse(notifNum.ToString());
                    PrivateMsgsMenuHeader = PRIVATE_MSG_MENU_HEADER + "(" + (num - 1) + ")";
                }
                catch (Exception)
                {
                    return;
                }
            }
        }

        public void NotifyPrivateMessages(int privateMsgsNum)
        {
            System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke((Action)(() =>
            {
                SetPrivateMsgMenuHeaderOn(privateMsgsNum);
                //isPrivateMsgsMenuHeaderChanged = true;
                userMenuBar.Items.Refresh();

            }));
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
                mi.Header = pm.sender + " : " + pm.title;
                mi.Tag = pm.id;
                mi.Click += new RoutedEventHandler(privateMsg_Click);
                privateMsgsMenu.Items.Add(mi);
            }
            privateMsgsMenu.IsSubmenuOpen = true;
        }

        private void privateMsg_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            string senderTitleStr = (string)mi.Header;
            string[] list = senderTitleStr.Split(':');
            string pmSender = list[0];
            PrivateMessage pm = cl.GetPrivateMsg(forumName, loggedUsername, pmSender, (int)mi.Tag);
            WindowHelper.ShowWindow(this, new PrivateMsgWindow(pmSender, pm.title, pm.content));
            SetPrivateMsgMenuHeaderMinus1();
        }

        #endregion





    }
}
