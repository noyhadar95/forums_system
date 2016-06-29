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
    /// Interaction logic for AddComplaintWindow.xaml
    /// </summary>
    public partial class AddComplaintWindow : NotifBarWindow
    {
        private ObservableCollection<string> users;

        public AddComplaintWindow(string forumName) : base(forumName)
        {
            InitializeComponent();

            WindowHelper.SetWindowBGImg(this);
            cl = new CL();
            base.Initialize(dockPanel);

            List<string> usersList = cl.GetUsersInForum(forumName);
            usersList.Remove(loggedUsername);
            users = new ObservableCollection<string>(usersList);
            usersLV.ItemsSource = users;

            RefreshNotificationsBar(loggedUsername);
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new ForumWindow(forumName));
        }

        private void sendBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = usersLV.SelectedItems;
            List<string> selectedItemsCopy = new List<string>();
            if (selectedItems.Count != 1)
            {
                MessageBox.Show("Can only complain on one user at a time");
                return;
            }
            foreach (string item in selectedItems)
            {
                selectedItemsCopy.Add(item);
            }
            foreach (string selectedItem in selectedItemsCopy)
            {
                cl.AddComplaint(forumName, "", selectedItem);
            }

            MessageBox.Show("your complaint has been successfully sent");
            WindowHelper.SwitchWindow(this, new ForumWindow(forumName));
        }
    }
}
