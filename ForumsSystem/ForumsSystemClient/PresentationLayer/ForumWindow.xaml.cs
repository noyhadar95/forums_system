using ForumsSystemClient.CommunicationLayer;
using ForumsSystemClient.Resources;
using System;
using System.Collections.Generic;
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
    public partial class ForumWindow : Window
    {
        private CL cl;
        private string forumName;
        private string loggedUsername;
        private string badLoginMsg = "your username/password are incorrect";

        public ForumWindow(string forumName)
        {
            InitializeComponent();

            WindowHelper.SetWindowBGImg(this);

            // initialize fields
            this.forumName = forumName;
            Title = forumName;
            loggedUsername = "";
            cl = new CL();

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

            if (WindowHelper.IsLoggedSuperAdmin())
            {
                // super admin is already logged in
                string superAdmin = WindowHelper.GetLoggedSuperAdmin();
                SwitchLoginToSuperAdminViewMode(superAdmin);
            }
            else if (WindowHelper.IsLoggedUser(forumName))
            {
                // user is already logged in
                User user = WindowHelper.GetLoggedUser(forumName);
                SwitchLoginToUserViewMode(user.Username);
                string type = cl.GetUserType(forumName, user.Username);
                if (type == "admin")
                    SwitchUserToAdminViewMode();
            }

        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new MainWindow());
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
            // fields are not empty try to login
            User user = cl.MemberLogin(forumName, username, password);
            if (user == null)
            {
                ShowBadLoginMsg();
                return;
            }
            else
            {
                // user is logged in, get type of user and change window accordingly.
                string type = cl.GetUserType(forumName, username);

                // handle user view
                SwitchLoginToUserViewMode(username);

                // check if admin
                if (type == "admin")
                {
                    SwitchUserToAdminViewMode();
                }

                // save the user in WindowHelper so all windows will know 
                // that the user is logged in.
                WindowHelper.SetLoggedUser(forumName, user);

            }
        }

        private void SwitchLoginToUserViewMode(string username)
        {
            loginGrid.Visibility = Visibility.Hidden;
            usernameTB.Text = "";
            passwordBox.Password = "";
            this.loggedUsername = username;
            welcomeTextBlock.Text = "welcome " + username;
            userGrid.Visibility = Visibility.Visible;
        }

        private void SwitchUserToAdminViewMode()
        {
            adminGrid.Visibility = Visibility.Visible;
        }

        private void SwitchLoginToSuperAdminViewMode(string superAdmin)
        {
            SwitchLoginToUserViewMode(superAdmin);
            SwitchUserToAdminViewMode();
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

        private void logoutBtn_Click(object sender, RoutedEventArgs e)
        {
            loginGrid.Visibility = Visibility.Visible;
            userGrid.Visibility = Visibility.Hidden;
            adminGrid.Visibility = Visibility.Hidden;

            WindowHelper.LogoutUser(forumName);

            // TODO:
            // cl.Logout();
        }

        private void sendMsgBtn_Click(object sender, RoutedEventArgs e)
        {
            Window sendPrMsgWin = new SendPrMsgWindow(forumName, loggedUsername);
            WindowHelper.ShowWindow(this, sendPrMsgWin);
        }

        private void addSubForumBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new AddSubForumWindow(forumName));
        }
    }
}
