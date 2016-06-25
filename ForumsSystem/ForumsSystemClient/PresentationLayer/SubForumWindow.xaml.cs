using ForumsSystemClient.CommunicationLayer;
using ForumsSystemClient.Resources.ForumManagement.DomainLayer;
using ForumsSystemClient.Resources.UserManagement.DomainLayer;
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
    /// Interaction logic for SubForumWindow.xaml
    /// </summary>
    public partial class SubForumWindow : NotifBarWindow
    {
        private string subForumName;
        private Dictionary<int, int> itemIndexThreadIDDict;

        public SubForumWindow(string forumName, string subForumName) : base(forumName)
        {
            InitializeComponent();

            WindowHelper.SetWindowBGImg(this);

            cl = new CL();
            this.subForumName = subForumName;
            Title = subForumName;

            base.Initialize(dockPanel);

            // initialize threads list view
            Dictionary<int, string> threadsDict = cl.GetThreads(forumName, subForumName);
            List<string> items = new List<string>();
            itemIndexThreadIDDict = new Dictionary<int, int>();
            int index = 0;
            foreach (KeyValuePair<int, string> pair in threadsDict)
            {
                // add title of thread to the list view
                items.Add(pair.Value);
                // map index of title in the LV items to it's matching thread id (pair.Key)
                itemIndexThreadIDDict.Add(index, pair.Key);
                index++;
            }
            threadsListView.ItemsSource = items;

            // initialize different types grids (login, user, admin)
            userGrid.Visibility = Visibility.Hidden;
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

                RefreshNotificationsBar(user.Username);
            }
        }

        private void ShowMemberViewMode(string username)
        {
            this.loggedUsername = username;
            userGrid.Visibility = Visibility.Visible;

            RefreshNotificationsBar(username);
            userMenuBar.Visibility = Visibility.Visible;
        }

        private void ShowAdminViewMode(string username)
        {
            ShowMemberViewMode(username);
            adminGrid.Visibility = Visibility.Visible;
        }


        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new ForumWindow(forumName));
        }

        private void threadsListView_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                int indexOfItem = threadsListView.Items.IndexOf(item);
                // retrieve threadID using the indexOfItem key for the itemIndexThreadIDDict
                int threadID = itemIndexThreadIDDict[indexOfItem];
                Window newWin = new ThreadWindow(forumName, subForumName, threadID);
                WindowHelper.SwitchWindow(this, newWin);
            }
        }

        private void addThreadBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new AddThreadWindow(forumName, subForumName));
        }

        private void editModsBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new EditModeratorsWindow(forumName, subForumName));
        }

        private void addModsBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new AddModeratorsWindow(forumName, subForumName));
        }

    }
}
