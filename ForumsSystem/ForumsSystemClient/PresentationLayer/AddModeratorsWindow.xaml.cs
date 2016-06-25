using ForumsSystemClient.CommunicationLayer;
using ForumsSystemClient.Resources.ForumManagement.DomainLayer;
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
    /// Interaction logic for AddModeratorsWindow.xaml
    /// </summary>
    public partial class AddModeratorsWindow : NotifBarWindow
    {
        private string subForumName;
        private int moderatorExpDateMonths = 12;
        private ObservableCollection<string> notModeratorsLVItems;
        private ObservableCollection<KeyValuePair<string, DateTime>> moderatorsLVItems;

        public AddModeratorsWindow(string forumName, string subForumName) : base(forumName)
        {
            InitializeComponent();

            WindowHelper.SetWindowBGImg(this);

            cl = new CL();
            this.subForumName = subForumName;

            base.Initialize(dockPanel);

            List<string> usersList = cl.GetUsersInForum(forumName);
            notModeratorsLVItems = new ObservableCollection<string>(usersList);
            moderatorsLVItems = new ObservableCollection<KeyValuePair<string, DateTime>>();

            notModeratorsListView.ItemsSource = notModeratorsLVItems;
            moderatorsListView.ItemsSource = moderatorsLVItems;

            // set the width of two columns in moderators list view to be 50% each
            GridView gv = (GridView)moderatorsListView.View;
            gv.Columns[0].Width = moderatorsListView.Width / 2;
            gv.Columns[1].Width = moderatorsListView.Width / 2;

            RefreshNotificationsBar(loggedUsername);
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
            List<string> couldntAddList = new List<string>(); // a list of the moderators that could not be added
            Dictionary<string, DateTime> moderators = new Dictionary<string, DateTime>();
            foreach (KeyValuePair<string, DateTime> pair in moderatorsLVItems)
            {
                moderators.Add(pair.Key, pair.Value);
                bool isAdded = cl.AddModerator(forumName, subForumName, loggedUsername, pair);
                if (!isAdded)
                    couldntAddList.Add(pair.Key);
            }

            if (couldntAddList.Count == moderators.Count)
                MessageBox.Show("moderators couldn't be added");
            else if (couldntAddList.Count > 0)
            {
                string couldntAddStr = "";
                for (int i = 0; i < couldntAddList.Count; i++)
                {
                    couldntAddStr += couldntAddList[i];
                    if (i < couldntAddList.Count - 1)
                        couldntAddStr += ",";
                }
                MessageBox.Show("the following moderators couldn't be added:\n" + couldntAddStr);
            }
            else
            {
                MessageBox.Show("the new moderators has been added successfully");
                WindowHelper.SwitchWindow(this, new SubForumWindow(forumName, subForumName));
            }

        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new SubForumWindow(forumName, subForumName));
        }

    }
}
