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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ForumsSystemClient.PresentationLayer
{
    /// <summary>
    /// Interaction logic for AddSubForumWindow.xaml
    /// </summary>
    public partial class AddSubForumWindow : Window
    {
        private int moderatorExpDateMonths = 12;
        private CL cl;
        private string forumName;
        private ObservableCollection<string> notModeratorsLVItems;
        private ObservableCollection<KeyValuePair<string, DateTime>> moderatorsLVItems;

        public AddSubForumWindow(string forumName)
        {
            InitializeComponent();

            WindowHelper.SetWindowBGImg(this);

            this.forumName = forumName;
            cl = new CL();
            List<string> usersList = cl.GetUsersInForum(forumName);
            notModeratorsLVItems = new ObservableCollection<string>(usersList);
            moderatorsLVItems = new ObservableCollection<KeyValuePair<string, DateTime>>();

            notModeratorsListView.ItemsSource = notModeratorsLVItems;
            moderatorsListView.ItemsSource = moderatorsLVItems;

            // set the width of two columns in moderators list view to be 50% each
            GridView gv= (GridView) moderatorsListView.View;
            gv.Columns[0].Width = moderatorsListView.Width / 2;
            gv.Columns[1].Width = moderatorsListView.Width / 2;
        }

        private void moveRightBtn_Click(object sender, RoutedEventArgs e)
        {
            // move username from left to right
            var selectedItems = notModeratorsListView.SelectedItems;
            List<string> selectedItemsCopy = new List<string>();
            foreach (string item in selectedItems)
            {
                selectedItemsCopy.Add(item);
            }
            foreach (string selectedItem in selectedItemsCopy)
            {
                DateTime expDate = DateTime.Now.AddMonths(moderatorExpDateMonths);
                moderatorsLVItems.Add(new KeyValuePair<string, DateTime>(selectedItem, expDate));
                notModeratorsLVItems.Remove(selectedItem);
            }

        }

        private void moveLeftBtn_Click(object sender, RoutedEventArgs e)
        {
            // move username from right to left
            var selectedItems = moderatorsListView.SelectedItems;
            List<KeyValuePair<string, DateTime>> selectedItemsCopy = new List<KeyValuePair<string, DateTime>>();
            foreach (KeyValuePair<string, DateTime> item in selectedItems)
            {
                selectedItemsCopy.Add(item);
            }
            foreach (KeyValuePair<string, DateTime> selectedItem in selectedItemsCopy)
            {
                notModeratorsLVItems.Add(selectedItem.Key);
                moderatorsLVItems.Remove(selectedItem);
               
            }
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            string subForumName = nameTB.Text;

            if (subForumName == "")
            {
                MessageBox.Show("please enter the name of the sub forum");
                return;
            }

            Regex rgx = new Regex(@"^[a-z0-9_-]{1,30}$");
            if (!rgx.IsMatch(subForumName))
            {
                MessageBox.Show("Enter valid Sub Forum Name");
                return;
            }

            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            foreach (KeyValuePair<string, DateTime> pair in moderatorsLVItems)
            {
                moderators.Add(pair.Key, pair.Value);
            }
            string creator = WindowHelper.GetLoggedUsername(forumName);

            SubForum sb= cl.CreateSubForum(creator, forumName, subForumName, moderators);
            if (sb != null)
                WindowHelper.SwitchWindow(this, new ForumWindow(forumName));
            else
                MessageBox.Show("sub-forum creation was unsuccessfull, please try again");
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new ForumWindow(forumName));
        }
    }
}
