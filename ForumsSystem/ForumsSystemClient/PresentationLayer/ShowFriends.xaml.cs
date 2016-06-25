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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ForumsSystemClient.PresentationLayer
{
    /// <summary>
    /// Interaction logic for ShowFriends.xaml
    /// </summary>
    public partial class ShowFriends : Window
    {
        private string forumName;
        private string loggedUsername;
        private CommunicationLayer.CL cl;


        private ObservableCollection<string> friends;
        public ShowFriends(string forumName )
        {
            InitializeComponent();
            this.forumName = forumName;

            loggedUsername = WindowHelper.GetLoggedUsername(forumName);

            cl = new CommunicationLayer.CL();
            List<string> friendsList = cl.getUsersFriends(forumName, loggedUsername);

            friends = new ObservableCollection<string>(friendsList);
            lv_friends.ItemsSource = friends;


        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new ForumWindow(forumName));
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = lv_friends.SelectedItems;
            List<string> selectedItemsCopy = new List<string>();
            foreach (string item in selectedItems)
            {
                selectedItemsCopy.Add(item);
            }
            foreach (string selectedItem in selectedItemsCopy)
            {
                cl.removeFriend(forumName, loggedUsername, selectedItem);
            }
            if (selectedItemsCopy.Count == 0)
            {
                MessageBox.Show("Please Select a user to remove");
                return;
            }
            MessageBox.Show("Your friends have successfully been removed");
            WindowHelper.SwitchWindow(this, new ForumWindow(forumName));

        }
    }
}
