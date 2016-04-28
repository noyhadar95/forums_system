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

        public ForumWindow(string forumName)
        {
            InitializeComponent();

            this.forumName = forumName;
            Title = forumName;

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
            if (username == "")
            {
                MessageBox.Show("please enter a username");
                return;
            }
            if (password == "")
            {
                MessageBox.Show("please enter a password");
                return;
            }
            // fields are not empty try to login
            bool isSuccess = cl.MemberLogin(forumName, username, password);
            if (!isSuccess)
            {
                MessageBox.Show("invalid login information, please try again");
                return;
            }
            else
            {
                // user is logged in, get tyoe of user and change window accordingly.
                string type = "user";
                loginGrid.Visibility = Visibility.Hidden;
                usernameTB.Text = "";
                passwordBox.Password = "";

                welcomeLbl.Content = "welcome " + username;
                //TODO: handle type
                userGrid.Visibility = Visibility.Visible;
                if (type == "admin")
                    adminGrid.Visibility = Visibility.Visible;



            }
        }

        private void logoutBtn_Click(object sender, RoutedEventArgs e)
        {
            loginGrid.Visibility = Visibility.Visible;
            userGrid.Visibility = Visibility.Hidden;
            // TODO: handle other types
        }
    }
}
