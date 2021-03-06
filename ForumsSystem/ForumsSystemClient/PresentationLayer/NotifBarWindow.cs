﻿using ForumsSystemClient.CommunicationLayer;
using ForumsSystemClient.Resources.ForumManagement.DomainLayer;
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
        private const string POSTS_MENU_HEADER = "_New Posts";
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
        // Posts
        protected MenuItem postsMenu;
        private string _postsMenuHeader;
        private string PostsMenuHeader
        {
            get { return _postsMenuHeader; }
            set { _postsMenuHeader = value; isPostsMenuHeaderChanged = true; }
        }
        protected bool isPostsMenuHeaderChanged = false;

        protected string session_token;



        // constructor
        public NotifBarWindow(string forumName)
        {
            this.forumName = forumName;
            mi_type = new MenuItem();
            mi_sessionToken = new MenuItem();
            session_token = "";
            FriendReqMenuHeader = FRIEND_REQUEST_MENU_HEADER;
            PrivateMsgsMenuHeader = PRIVATE_MSG_MENU_HEADER;
            PostsMenuHeader = POSTS_MENU_HEADER;

            if (WindowHelper.IsLoggedUser(forumName))
                loggedUsername = WindowHelper.GetLoggedUsername(forumName);


            // on mouse move the notif bar checks if a new nitification has been recieved
            this.MouseMove += NotifBarWindow_MouseMove;
        }

        public string GetForumName()
        {
            return forumName;
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
            if (isPostsMenuHeaderChanged)
            {
                lock (this)
                {
                    RefreshPostsMenu();
                    isPostsMenuHeaderChanged = false;
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

            // create New Posts menu
            postsMenu = new MenuItem();
            postsMenu.Header = GetPostsMenuHeader();
            postsMenu.Click += new RoutedEventHandler(postsMenu_Click);
            postsMenu.ToolTip = "See new posts in the forum.";

            userMenuBar = new Menu();
            userMenuBar.Visibility = Visibility.Visible;
            userMenuBar.Items.Add(friendRequestsMenu);
            userMenuBar.Items.Add(privateMsgsMenu);
            userMenuBar.Items.Add(postsMenu);
            userMenuBar.Items.Add(mi_type);
            userMenuBar.Items.Add(mi_sessionToken);


            DockPanel.SetDock(userMenuBar, Dock.Top);
            dock.Children.Insert(0, userMenuBar);
        }

        protected void ResetHeaders()
        {
            FriendReqMenuHeader = FRIEND_REQUEST_MENU_HEADER;
            PrivateMsgsMenuHeader = PRIVATE_MSG_MENU_HEADER;
            PostsMenuHeader = POSTS_MENU_HEADER;
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
            // refresh posts menu bar
            RefreshPostsMenu();
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
            int notifNum = GetFRNotifNum();
            if (notifNum <= 0)
                return;

            if (notifNum == 1)
            {
                FriendReqMenuHeader = FRIEND_REQUEST_MENU_HEADER;
                return;
            }

            // in case notifNum > 1
            FriendReqMenuHeader = FRIEND_REQUEST_MENU_HEADER + "(" + (notifNum - 1) + ")";

        }

        public void NotifyFriendRequests(int friendReqsNum)
        {
            if (friendReqsNum > 0)
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
            if (cl_friendReqs != null)
            {
                foreach (string str in cl_friendReqs)
                {
                    MenuItem mi = new MenuItem();
                    mi.Header = str;
                    mi.Click += new RoutedEventHandler(friendReq_Click);
                    friendRequestsMenu.Items.Add(mi);
                }
                friendRequestsMenu.IsSubmenuOpen = true;
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

        public int GetFRNotifNum()
        {
            if (FriendReqMenuHeader != FRIEND_REQUEST_MENU_HEADER)
            {
                // FriendReqMenuHeader is of the form: FRIEND_REQUEST_MENU_HEADER(notifNum)
                int numStartIndex = FRIEND_REQUEST_MENU_HEADER.Length + 1;
                int digitsCount = 0;
                while (FriendReqMenuHeader[numStartIndex + digitsCount] != ')')
                    digitsCount++;
                string notifNumSrt = FriendReqMenuHeader.Substring(numStartIndex, digitsCount);
                try
                {
                    int num = int.Parse(notifNumSrt);
                    return num;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            else
                return 0;
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
            int notifNum = GetPMNotifNum();
            if (notifNum <= 0)
                return;

            if (notifNum == 1)
            {
                PrivateMsgsMenuHeader = PRIVATE_MSG_MENU_HEADER;
                return;
            }

            // in case notifNum > 1
            PrivateMsgsMenuHeader = PRIVATE_MSG_MENU_HEADER + "(" + (notifNum - 1) + ")";

        }

        public void NotifyPrivateMessages(int privateMsgsNum)
        {
            if (privateMsgsNum > 0)
            {
                System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke((Action)(() =>
                {
                    SetPrivateMsgMenuHeaderOn(privateMsgsNum);
                    //isPrivateMsgsMenuHeaderChanged = true;
                    userMenuBar.Items.Refresh();

                }));
            }
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
            if (cl_privateMsgs != null)
            {
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

        public int GetPMNotifNum()
        {
            if (PrivateMsgsMenuHeader != PRIVATE_MSG_MENU_HEADER)
            {
                // PrivateMsgsMenuHeader is of the form: PRIVATE_MSG_MENU_HEADER(notifNum)
                int numStartIndex = PRIVATE_MSG_MENU_HEADER.Length + 1;
                int digitsCount = 0;
                while (PrivateMsgsMenuHeader[numStartIndex + digitsCount] != ')')
                    digitsCount++;
                string notifNumSrt = PrivateMsgsMenuHeader.Substring(numStartIndex, digitsCount);
                try
                {
                    int num = int.Parse(notifNumSrt);
                    return num;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            else
                return 0;
        }

        #endregion


        #region posts

        private string GetPostsMenuHeader()
        {
            return PostsMenuHeader;
        }

        private void SetPostsMenuHeaderOn(int notifNum)
        {
            PostsMenuHeader = POSTS_MENU_HEADER + "(" + notifNum + ")";
        }

        private void SetPostsMenuHeaderMinus1()
        {
            int notifNum = GetPostsNotifNum();
            if (notifNum <= 0)
                return;

            if (notifNum == 1)
            {
                PostsMenuHeader = POSTS_MENU_HEADER;
                return;
            }

            // in case notifNum > 1
            PostsMenuHeader = POSTS_MENU_HEADER + "(" + (notifNum - 1) + ")";

        }

        public void NotifyPosts(int postsNum)
        {
            if (postsNum > 0)
            {
                System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke((Action)(() =>
                {
                    SetPostsMenuHeaderOn(postsNum);
                    userMenuBar.Items.Refresh();
                }));
            }
        }

        protected void RefreshPostsMenu()
        {
            postsMenu.Items.Clear();
            postsMenu.Header = GetPostsMenuHeader();
        }

        private void postsMenu_Click(object sender, RoutedEventArgs e)
        {
            postsMenu.Items.Clear();
            List<PostNotification> cl_posts = cl.GetPostNotifications(forumName, loggedUsername);
            if (cl_posts != null)
            {
                foreach (PostNotification post in cl_posts)
                {
                    MenuItem mi = new MenuItem();
                    mi.Header = post.GetPublisher() + " : " + post.GetTitle();
                    mi.Tag = post.id;
                    mi.Click += new RoutedEventHandler(post_Click);
                    postsMenu.Items.Add(mi);
                }
                postsMenu.IsSubmenuOpen = true;
            }
        }

        private void post_Click(object sender, RoutedEventArgs e)
        {
            // TODO:
        }

        public int GetPostsNotifNum()
        {
            if (PostsMenuHeader != POSTS_MENU_HEADER)
            {
                // PostsMenuHeader is of the form: POSTS_MENU_HEADER(notifNum)
                int numStartIndex = POSTS_MENU_HEADER.Length + 1;
                int digitsCount = 0;
                while (PostsMenuHeader[numStartIndex + digitsCount] != ')')
                    digitsCount++;
                string notifNumSrt = PostsMenuHeader.Substring(numStartIndex, digitsCount);
                try
                {
                    int num = int.Parse(notifNumSrt);
                    return num;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            else
                return 0;
        }

        #endregion



    }
}
