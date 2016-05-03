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
    /// Interaction logic for AddForumWindow.xaml
    /// </summary>
    public partial class AddForumWindow : Window
    {
        private CL cl;
        private string forumName;
        private ObservableCollection<string> notAdminsLVItems;
        private ObservableCollection<string> adminsLVItems;

        public AddForumWindow()
        {
            InitializeComponent();

            WindowHelper.SetWindowBGImg(this);

            cl = new CL();
            List<string> usersList = cl.GetUsersInForum(forumName);
            notAdminsLVItems = new ObservableCollection<string>(usersList);
            adminsLVItems = new ObservableCollection<string>();

            notAdminsListView.ItemsSource = notAdminsLVItems;
            adminsListView.ItemsSource = adminsLVItems;
        }

        private void moveRightBtn_Click(object sender, RoutedEventArgs e)
        {
            // move username from left to right
            var selectedItems = notAdminsListView.SelectedItems;
            List<string> selectedItemsCopy = new List<string>();
            foreach (string item in selectedItems)
            {
                selectedItemsCopy.Add(item);
            }
            foreach (string selectedItem in selectedItemsCopy)
            {
                adminsLVItems.Add(selectedItem);
                notAdminsLVItems.Remove(selectedItem);
            }
        }

        private void moveLeftBtn_Click(object sender, RoutedEventArgs e)
        {
            // move username from right to left
            var selectedItems = adminsListView.SelectedItems;
            List<string> selectedItemsCopy = new List<string>();
            foreach (string item in selectedItems)
            {
                selectedItemsCopy.Add(item);
            }
            foreach (string selectedItem in selectedItemsCopy)
            {
                notAdminsLVItems.Add(selectedItem);
                adminsLVItems.Remove(selectedItem);
            }
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            string forumName = nameTB.Text;

            if (forumName == "")
            {
                MessageBox.Show("please enter the name of the forum");
                return;
            }

            //List<string> admins = new List<string>(adminsLVItems)
            //cl.CreateForum(creator, forumName, admins);

            WindowHelper.SwitchWindow(this, new MainWindow());
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new MainWindow());
        }
    }
}
