using ForumsSystemClient.CommunicationLayer;
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

            this.forumName = forumName;
            Title = forumName;
            loggedUsername = "";

            // initialize different types grids (user,admin,login)
            userGrid.Visibility = Visibility.Hidden;
            userGrid.Margin = loginGrid.Margin;
            adminGrid.Visibility = Visibility.Hidden;
            adminGrid.Margin = new Thickness(userGrid.Margin.Left, userGrid.Margin.Top + userGrid.Height,
                userGrid.Margin.Right, userGrid.Margin.Bottom);

            cl = new CL();
            List<string> items = cl.GetSubForumsList(forumName);
            subForumsListView.ItemsSource = items;


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
            bool isSuccess = cl.MemberLogin(forumName, username, password);
            if (!isSuccess)
            {
                ShowBadLoginMsg();
                return;
            }
            else
            {
                // user is logged in, get type of user and change window accordingly.
                string type = "user";
                loginGrid.Visibility = Visibility.Hidden;
                usernameTB.Text = "";
                passwordBox.Password = "";

                this.loggedUsername = username;
                welcomeTextBlock.Text = "welcome " + username;
                //TODO: handle type
                userGrid.Visibility = Visibility.Visible;
                if (type == "admin")
                {
                    adminGrid.Visibility = Visibility.Visible;

                }
                adminGrid.Visibility = Visibility.Visible;


            }
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
            // TODO: handle other types
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
