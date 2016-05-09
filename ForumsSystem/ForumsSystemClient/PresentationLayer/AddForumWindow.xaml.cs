using ForumsSystemClient.CommunicationLayer;
using ForumsSystemClient.Resources.ForumManagement.DomainLayer;
using ForumsSystemClient.Resources.UserManagement.DomainLayer;
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
        private ObservableCollection<string> adminsLVItems;
        private List<User> admins;

        public AddForumWindow()
        {
            InitializeComponent();

            WindowHelper.SetWindowBGImg(this);

            cl = new CL();
            adminsLVItems = new ObservableCollection<string>();
            admins = new List<User>();

            adminsListView.ItemsSource = adminsLVItems;
        }

        public void AddAdmin(User admin)
        {
            admins.Add(admin);
            adminsLVItems.Add(((User)admin).Username);
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            string forumName = nameTB.Text;

            if (forumName == "")
            {
                MessageBox.Show("please enter the name of the forum");
                return;
            }
            if (!WindowHelper.IsLoggedSuperAdmin())
            {
                MessageBox.Show("error: super admin is not logged in");
                return;
            }
            SuperAdmin creator = WindowHelper.GetLoggedSuperAdmin();
            
            // TODO: handle policy
            cl.CreateForum(creator.userName,creator.password, forumName,new MinimumAgePolicy(Policies.MinimumAge,1) ,admins);

            WindowHelper.SwitchWindow(this, new MainWindow());
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new MainWindow());
        }

        private void addAdminBtn_Click(object sender, RoutedEventArgs e)
        {
            // open a new window without closing this one
            WindowHelper.ShowWindow(this, new AddAdminWindow(this));
        }

        private void removeAdminBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = adminsListView.SelectedItems;
            List<string> selectedItemsCopy = new List<string>();
            foreach (string item in selectedItems)
            {
                selectedItemsCopy.Add(item);
            }
            foreach (string selectedItem in selectedItemsCopy)
            {
                adminsLVItems.Remove(selectedItem);
                // remove admin from admins list
                foreach (User a in admins)
                {
                    if (a.Username == selectedItem)
                    {
                        admins.Remove(a);
                        break;
                    }
                }
            }
        }

    }
}
