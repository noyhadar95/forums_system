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
    /// Interaction logic for AddAdminToForumWindow.xaml
    /// </summary>
    public partial class AddAdminToForumWindow : NotifBarWindow
    {
        
        
        private ObservableCollection<string> notModeratorsLVItems;
        private ObservableCollection<string> moderatorsLVItems;
        CL cl;
        public AddAdminToForumWindow(string forumName) : base(forumName)
        {
            InitializeComponent();
            WindowHelper.SetWindowBGImg(this);

            cl = new CL();
           

            base.Initialize(dockPanel);

            List<string> usersList = cl.GetUsersInForum(forumName);
            notModeratorsLVItems = new ObservableCollection<string>(usersList);
            moderatorsLVItems = new ObservableCollection<string>();

            notModeratorsListView.ItemsSource = notModeratorsLVItems;
            moderatorsListView.ItemsSource = moderatorsLVItems;

            // set the width of two columns in moderators list view to be 50% each
            GridView gv = (GridView)moderatorsListView.View;
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
            List<string> couldntAddList = new List<string>(); // a list of the moderators that could not be added
            List<string> moderators = new List<string>();
            foreach (string pair in moderatorsLVItems)
            {
                moderators.Add(pair);
                bool isAdded = cl.AddAdmin(forumName, pair);
                if (!isAdded)
                    couldntAddList.Add(pair);
            }

            if (couldntAddList.Count == moderators.Count)
                MessageBox.Show("admins couldn't be added");
            else if (couldntAddList.Count > 0)
            {
                string couldntAddStr = "";
                for (int i = 0; i < couldntAddList.Count; i++)
                {
                    couldntAddStr += couldntAddList[i];
                    if (i < couldntAddList.Count - 1)
                        couldntAddStr += ",";
                }
                MessageBox.Show("the following admins couldn't be added:\n" + couldntAddStr);
            }
            else
            {
                MessageBox.Show("the new admins have been added successfully");
        //        WindowHelper.SwitchWindow(this,);//TODO
            }

        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            //WindowHelper.SwitchWindow(this, );//TODO
        }

    }
}
