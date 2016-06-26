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
    /// Interaction logic for BanUserWindow.xaml
    /// </summary>
    public partial class BanUserWindow : Window
    {
        private string forumName;
        private CL cl;
        private ObservableCollection<string> users;
        private string loggedUsername;

        public BanUserWindow()
        {
            InitializeComponent();

            loggedUsername = WindowHelper.GetLoggedUsername(forumName);
            List<string> usersList = cl.GetUsersInForum(forumName);
            users = new ObservableCollection<string>(usersList);
            usersLV.ItemsSource = users;
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
                cl.DeactivateUser(forumName, selectedItem);
            }
            if (selectedItemsCopy.Count == 0)
            {
                MessageBox.Show("Must select at least one user, please try again");
                return;
            }
            MessageBox.Show("The user has been successfully banned");
            WindowHelper.SwitchWindow(this, new ForumWindow(forumName));
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new ForumWindow(forumName));
        }
    }
}
