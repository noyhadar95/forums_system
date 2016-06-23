using ForumsSystemClient.CommunicationLayer;
using ForumsSystemClient.Resources.ForumManagement.DomainLayer;
using ForumsSystemClient.Resources.UserManagement.DomainLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class ForumWindow : NotifBarWindow
    {

        private string badLoginMsg = "your username/password are incorrect";

        public ForumWindow(string forumName) : base(forumName)
        {
            InitializeComponent();

            WindowHelper.SetWindowBGImg(this);

            // initialize fields
            Title = forumName;
            loggedUsername = "";
            cl = new CL();

            base.Initialize(dockPanel);


            // initialize sub-forums list
            List<string> items = cl.GetSubForumsList(forumName);
            subForumsListView.ItemsSource = items;

            // initialize different types grids (login, user, admin)
            userGrid.Visibility = Visibility.Hidden;
            userGrid.Margin = loginGrid.Margin;
            adminGrid.Visibility = Visibility.Hidden;
            adminGrid.Margin = new Thickness(userGrid.Margin.Left, userGrid.Margin.Top + userGrid.Height,
                userGrid.Margin.Right, userGrid.Margin.Bottom);


            // hide user menu bar (notifications bar included)
            userMenuBar.Visibility = Visibility.Hidden;

            if (WindowHelper.IsLoggedUser(forumName))
            {
                // user is already logged in
                User user = WindowHelper.GetLoggedUser(forumName);
                string type = cl.GetUserType(forumName, user.Username);
                if (type == UserTypes.Member)
                    ShowMemberViewMode(user.Username);
                else if (type == UserTypes.Admin)
                    ShowAdminViewMode(user.Username);

            }

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

            RefreshNotificationsBar(username);
            userMenuBar.Visibility = Visibility.Visible;
        }

        private void ShowAdminViewMode(string username)
        {
            ShowMemberViewMode(username);
            adminGrid.Visibility = Visibility.Visible;
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
            //friendRequestsMenu.Items.Clear();
            ResetHeaders();
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
            string sessionToken = sessionTokenPB.Password;
            if (username == "" || password == "")
            {
                ShowBadLoginMsg();
                return;
            }

            Regex rgx = new Regex(@"([0-9]8)$");
            if (sessionToken!="" && !rgx.IsMatch(sessionToken))
            {
                MessageBox.Show("Session Token must be 8 numbers");
            }

            WindowHelper.SetCurrentWindow(this);

            //// fields are not empty try to login
            Tuple<User, string> userTokenTuple = null;
            if (sessionToken == "")
            {
                userTokenTuple = cl.MemberLogin(forumName, username, password);
            }
            else
            {
                userTokenTuple = cl.MemberLogin(forumName, username, password, sessionToken);
            }
            User user = null;
            if (userTokenTuple != null)
                user = userTokenTuple.Item1;

            if (user == null)
            {
                // failed login
                WindowHelper.SetCurrentWindow(null);
                ShowBadLoginMsg();
                return;
            }
            else
            {
                base.session_token = userTokenTuple.Item2;
                // save the user in WindowHelper so all windows will know 
                // that the user is logged in.
                WindowHelper.SetLoggedUser(forumName, user);
                

                // user is logged in, get type of user and change window accordingly.
                string type = cl.GetUserType(forumName, username);
                MessageBox.Show(type);

                if (type == UserTypes.Member)
                    ShowMemberViewMode(user.Username);
                else if (type == UserTypes.Admin)
                    ShowAdminViewMode(user.Username);

                
            }
        }

        private void reportsBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new AdminReportsWindow(forumName));
        }

        private void confirmEmailBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new ConfirmEmailWindow(forumName));
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new MainWindow());
        }

        #endregion

        
    }
}
