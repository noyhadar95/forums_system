using ForumsSystemClient.CommunicationLayer;
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
    /// Interaction logic for SendFriendReqWindow.xaml
    /// </summary>
    public partial class SendFriendReqWindow : NotifBarWindow
    {
        private ObservableCollection<string> usersNotFriends;

        public SendFriendReqWindow(string forumName) : base(forumName)
        {
            InitializeComponent();

            WindowHelper.SetWindowBGImg(this);

            cl = new CL();

            base.Initialize(dockPanel);

            loggedUsername = WindowHelper.GetLoggedUsername(forumName);
            List<string> users = cl.GetUsersNotFriends(forumName, loggedUsername);
            usersNotFriends = new ObservableCollection<string>(users);
            usersLV.ItemsSource = usersNotFriends;

            RefreshNotificationsBar(loggedUsername);
        }



        private void sendBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = usersLV.SelectedItems;
            List<string> selectedItemsCopy = new List<string>();
            foreach (string item in selectedItems)
            {
                selectedItemsCopy.Add(item);
            }
            foreach (string selectedItem in selectedItemsCopy)
            {
                cl.SendFriendRequest(forumName, loggedUsername, selectedItem);
            }
            if (selectedItemsCopy.Count == 0)
            {
                MessageBox.Show("no user to send request to, please try again");
                return;
            }
            MessageBox.Show("your friend request/s have been successfully sent");
            WindowHelper.SwitchWindow(this, new ForumWindow(forumName));
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new ForumWindow(forumName));
        }
    }
}
