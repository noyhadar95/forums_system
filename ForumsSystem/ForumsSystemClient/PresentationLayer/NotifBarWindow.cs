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
    public abstract class NotifBarWindow : Window
    {
        protected CL cl;
        protected string forumName;
        protected string loggedUsername;

        protected Menu userMenuBar;
        protected MenuItem friendRequestsMenu;
        protected MenuItem mi_type; // the menu item for displaying the user type on the user notifications bar


        public NotifBarWindow()
        {
            mi_type = new MenuItem();

        }

        protected virtual void Initialize(DockPanel dock)
        {
            friendRequestsMenu = new MenuItem();
            friendRequestsMenu.Header = WindowHelper.GetFriendReqMenuHeader();

            userMenuBar = new Menu();
            userMenuBar.Visibility = Visibility.Visible;
            userMenuBar.Items.Add(friendRequestsMenu);

            DockPanel.SetDock(userMenuBar, Dock.Top);
            dock.Children.Insert(0, userMenuBar);
        }

        protected void IniNotificationsBar(string username)
        {
            string type = cl.GetUserType(forumName, username);
            mi_type.Header = "logged in as " + type;
            if (userMenuBar.Items.Count < 2)
                userMenuBar.Items.Add(mi_type);

            // initialize friend requests menu bar
            IniFriendReqsMenu(username);
        }

        protected void IniFriendReqsMenu(string username)
        {
            friendRequestsMenu.Items.Clear();
            friendRequestsMenu.Header = WindowHelper.GetFriendReqMenuHeader();
            List<string> cl_friendReqs = cl.GetFriendRequests(forumName, username);
            foreach (string str in cl_friendReqs)
            {
                MenuItem mi = new MenuItem();
                mi.Header = str;
                mi.Click += new RoutedEventHandler(friendReqsMenu_Click);
                friendRequestsMenu.Items.Add(mi);
            }
        }

        private void friendReqsMenu_Click(object sender, RoutedEventArgs e)
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


    }
}
