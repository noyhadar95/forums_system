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
    /// Interaction logic for AddSubForumWindow.xaml
    /// </summary>
    public partial class AddSubForumWindow : Window
    {
        private CL cl;
        private string forumName;
        private ObservableCollection<string> notModeratorsLVItems;
        private ObservableCollection<string> moderatorsLVItems;

        public AddSubForumWindow(string forumName)
        {
            InitializeComponent();

            WindowHelper.SetWindowBGImg(this);

            this.forumName = forumName;
            cl = new CL();
            List<string> usersList = cl.GetUsersInForum(forumName);
            notModeratorsLVItems = new ObservableCollection<string>(usersList);
            moderatorsLVItems = new ObservableCollection<string>();

            notModeratorsListView.ItemsSource = notModeratorsLVItems;
            moderatorsListView.ItemsSource = moderatorsLVItems;

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
                moderatorsLVItems.Add(selectedItem);
                notModeratorsLVItems.Remove(selectedItem);
            }

        }

        private void moveLeftBtn_Click(object sender, RoutedEventArgs e)
        {
            // move username from right to left
            var selectedItems = moderatorsListView.SelectedItems;
            List<string> selectedItemsCopy = new List<string>();
            foreach (string item in selectedItems)
            {
                selectedItemsCopy.Add(item);
            }
            foreach (string selectedItem in selectedItemsCopy)
            {
                notModeratorsLVItems.Add(selectedItem);
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

            //List<string> moderators = new List<string>(moderatorsLVItems)
            //cl.CreateSubForum(creator, subForumName, moderators);

            WindowHelper.SwitchWindow(this, new ForumWindow(forumName));
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new ForumWindow(forumName));
        }
    }
}
