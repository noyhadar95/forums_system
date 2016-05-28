using ForumsSystemClient.CommunicationLayer;
using ForumsSystemClient.Resources.ForumManagement.DomainLayer;
using ForumsSystemClient.Resources.UserManagement.DomainLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ForumsSystemClient.PresentationLayer
{
    /// <summary>
    /// Interaction logic for ForumWindow.xaml
    /// </summary>
    public partial class ForumWindow : Window, INotifiableWindow
    {

        private CL cl;
        private string forumName;
        private string loggedUsername;
        private string badLoginMsg = "your username/password are incorrect";
        private ObservableCollection<MenuItem> friendRequestsOC;
        private MenuItem mi_type;

        public ForumWindow(string forumName)
        {
            InitializeComponent();

            WindowHelper.SetWindowBGImg(this);

            // initialize fields
            this.forumName = forumName;
            Title = forumName;
            loggedUsername = "";
            cl = new CL();
            friendRequestsOC = new ObservableCollection<MenuItem>();
            mi_type = new MenuItem();

            // initialize sub-forums list
            List<string> items = cl.GetSubForumsList(forumName);
            subForumsListView.ItemsSource = items;

            // initialize different types grids (login, user, admin, super admin)
            userGrid.Visibility = Visibility.Hidden;
            userGrid.Margin = loginGrid.Margin;
            adminGrid.Visibility = Visibility.Hidden;
            adminGrid.Margin = new Thickness(userGrid.Margin.Left, userGrid.Margin.Top + userGrid.Height,
                userGrid.Margin.Right, userGrid.Margin.Bottom);
            superAdminGrid.Visibility = Visibility.Hidden;
            superAdminGrid.Margin = new Thickness(adminGrid.Margin.Left, adminGrid.Margin.Top + adminGrid.Height,
                adminGrid.Margin.Right, adminGrid.Margin.Bottom);

            // hide user menu bar (notifications bar included)
            userMenuBar.Visibility = Visibility.Hidden;

            if (WindowHelper.IsLoggedSuperAdmin())
            {
                // super admin is already logged in
                SuperAdmin superAdmin = WindowHelper.GetLoggedSuperAdmin();
                SwitchLoginToSuperAdminViewMode(superAdmin.userName);
            }
            else if (WindowHelper.IsLoggedUser(forumName))
            {
                // user is already logged in
                User user = WindowHelper.GetLoggedUser(forumName);
                string type = cl.GetUserType(forumName, user.Username);
                if (type == UserTypes.Member)
                    ShowMemberViewMode(user.Username);
                else if (type == UserTypes.Admin)
                    ShowAdminViewMode(user.Username);

                IniNotificationsBar(user.Username);
            }

        }

        private void IniNotificationsBar(string username)
        {
            string type = cl.GetUserType(forumName, username);
            mi_type.Header = "logged in as " + type;
            if (userMenuBar.Items.Count < 2)
                userMenuBar.Items.Add(mi_type);

            // initialize friend requests menu bar
            IniFriendReqsMenu(username);
        }

        private void IniFriendReqsMenu(string username)
        {
            friendRequestsMenu.Header = WindowHelper.GetFriendReqMenuHeader();
            List<string> cl_friendReqs = cl.GetFriendRequests(forumName, username);
            List<MenuItem> menuItems = new List<MenuItem>();
            foreach (string str in cl_friendReqs)
            {
                MenuItem mi = new MenuItem();
                mi.Header = str;
                mi.Click += new RoutedEventHandler(friendReqsMenu_Click);
                menuItems.Add(mi);
                //friendRequestsOC.Add(mi);
                friendRequestsMenu.Items.Add(mi);
            }
            //friendRequestsMenu.ItemsSource = friendRequestsOC;
        }

        public void Notify()
        {
            MessageBox.Show("Got here!! ");
            //friendRequestsOC.Clear();
            friendRequestsMenu.Items.Clear();
            IniFriendReqsMenu(loggedUsername);
            userMenuBar.Items.Refresh();
        }

        private void subForumsListView_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                Window newWin = new SubForumWindow(forumName, (string)item);
                WindowHelper.SwitchWindow(this, newWin);
            }
        }

        private void ShowMemberViewMode(string username)
        {
            loginGrid.Visibility = Visibility.Hidden;
            usernameTB.Text = "";
            passwordBox.Password = "";
            this.loggedUsername = username;
            welcomeTextBlock.Text = "welcome " + username;
            userGrid.Visibility = Visibility.Visible;

            IniNotificationsBar(username);
            userMenuBar.Visibility = Visibility.Visible;
        }

        private void ShowAdminViewMode(string username)
        {
            ShowMemberViewMode(username);
            adminGrid.Visibility = Visibility.Visible;
        }

        private void SwitchLoginToSuperAdminViewMode(string superAdmin)
        {
            ShowMemberViewMode(superAdmin);
            ShowAdminViewMode(superAdmin);
            logoutBtn.Visibility = Visibility.Hidden;
            superAdminGrid.Visibility = Visibility.Visible;
        }

        private void ShowBadLoginMsg()
        {
            badLoginLbl.Content = badLoginMsg;
            // Make the bad login message fadeaway animation.
            DoubleAnimation animate = new DoubleAnimation();
            animate.From = 1.0;
            animate.To = 0;
            animate.Duration = TimeSpan.FromSeconds(3);
            badLoginLbl.BeginAnimation(OpacityProperty, animate);
        }

        private void ShowGuestViewMode()
        {
            // handle grids
            loginGrid.Visibility = Visibility.Visible;
            userGrid.Visibility = Visibility.Hidden;
            adminGrid.Visibility = Visibility.Hidden;

            // hide and clear user menu bar
            userMenuBar.Visibility = Visibility.Hidden;
            //friendRequestsOC.Clear();
            friendRequestsMenu.Items.Clear();
            userMenuBar.Items.Refresh();
        }



        #region Click Handlers

        private void sendMsgBtn_Click(object sender, RoutedEventArgs e)
        {
            Window sendPrMsgWin = new SendPrMsgWindow(forumName, loggedUsername);
            WindowHelper.ShowWindow(this, sendPrMsgWin);
        }

        private void addSubForumBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new AddSubForumWindow(forumName));
        }

        private void addFriendBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new SendFriendReqWindow(forumName));
        }

        private void logoutBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowGuestViewMode();

            WindowHelper.LogoutUser(forumName);
            cl.MemberLogout(forumName, loggedUsername);
        }

        private void registerBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new RegisterWindow(forumName));
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTB.Text;
            string password = passwordBox.Password;
            if (username == "" || password == "")
            {
                ShowBadLoginMsg();
                return;
            }
            WindowHelper.SetCurrentWindow(this);
            // fields are not empty try to login
            User user = cl.MemberLogin(forumName, username, password);
            if (user == null)
            {
                WindowHelper.SetCurrentWindow(null);
                ShowBadLoginMsg();
                return;
            }
            else
            {
                // save the user in WindowHelper so all windows will know 
                // that the user is logged in.
                WindowHelper.SetLoggedUser(forumName, user);

                // user is logged in, get type of user and change window accordingly.
                string type = cl.GetUserType(forumName, username);
                if (type == UserTypes.Member)
                    ShowMemberViewMode(user.Username);
                else if (type == UserTypes.Admin)
                    ShowAdminViewMode(user.Username);

                IniNotificationsBar(user.Username);

            }
        }

        private void friendReqsMenu_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you accept the friend request?",
                                                    "Confirmation", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
            {
                // accept friend request

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

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new MainWindow());
        }

        #endregion

    }
}
