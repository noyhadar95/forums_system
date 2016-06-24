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
    public partial class AddComplaintWindow : Window
    {
        CL cl;
        private string forumName;
        private string loggedUsername;
        private ObservableCollection<string> users;

        public AddComplaintWindow(string forumName)
        {
            InitializeComponent();

            WindowHelper.SetWindowBGImg(this);

            cl = new CL();
            this.forumName = forumName;

            this.loggedUsername = WindowHelper.GetLoggedUsername(forumName);
            List<string> usersList = cl.GetUsersInForum(forumName);
            users = new ObservableCollection<string>(usersList);
            usersLV.ItemsSource = users;
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new ForumWindow(forumName));
        }

        private void sendBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = usersLV.SelectedItems;
            List<string> selectedItemsCopy = new List<string>();
            if (selectedItemsCopy.Count != 1)
            {
                MessageBox.Show("Can only complaint one user at a time");
                return;
            }
            foreach (string item in selectedItems)
            {
                selectedItemsCopy.Add(item);
            }
            foreach (string selectedItem in selectedItemsCopy)
            {
                cl.AddComplaint(forumName,"", selectedItem);
            }
            
            MessageBox.Show("your complaint has been successfully sent");
            WindowHelper.SwitchWindow(this, new ForumWindow(forumName));
        }
    }
}
