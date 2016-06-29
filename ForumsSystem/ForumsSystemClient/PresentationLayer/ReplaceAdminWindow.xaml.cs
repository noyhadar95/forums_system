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
    /// Interaction logic for ReplaceAdminWindow.xaml
    /// </summary>
    public partial class ReplaceAdminWindow : NotifBarWindow
    {
        private ObservableCollection<string> nonAdmins;

        public ReplaceAdminWindow(string forumName) : base(forumName)
        {
            InitializeComponent();

            WindowHelper.SetWindowBGImg(this);
            cl = new CL();

            base.Initialize(dockPanel);

            List<string> nonAdminsList = cl.getNonAdmins(forumName);
            nonAdmins = new ObservableCollection<string>(nonAdminsList);
            lv_users.ItemsSource = nonAdmins;

            RefreshNotificationsBar(loggedUsername);
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new ForumWindow(forumName));
        }

        private void btn_switch_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = lv_users.SelectedItems;
            List<string> selectedItemsCopy = new List<string>();
            foreach (string item in selectedItems)
            {
                selectedItemsCopy.Add(item);
            }
            if (selectedItemsCopy.Count != 1)
            {
                MessageBox.Show("Please Select exactly one user");
                return;
            }
            foreach (string selectedItem in selectedItemsCopy)
            {
                bool f = cl.AddAdmin(forumName, selectedItem);
                if (f)
                {
                    cl.RemoveAdmin(forumName, loggedUsername);
                    MessageBox.Show("you have successfuly passed your administrative permissions");
                    WindowHelper.SwitchWindow(this, new ForumWindow(forumName));
                }
                else
                {
                    MessageBox.Show("new user cannot be an admin");
                    return;
                }
            }
        }
    }
}
