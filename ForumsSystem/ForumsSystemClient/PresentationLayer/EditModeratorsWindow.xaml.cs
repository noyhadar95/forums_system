using ForumsSystemClient.CommunicationLayer;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for EditModeratorsWindow.xaml
    /// </summary>
    public partial class EditModeratorsWindow : Window
    {
        private CL cl;
        private string forumName;
        private string subForumName;
        private List<string> moderators;
        private string adminUsername;  // the current logged-in admin username

        public EditModeratorsWindow(string forumName, string subForumName)
        {
            InitializeComponent();

            WindowHelper.SetWindowBGImg(this);

            cl = new CL();
            this.forumName = forumName;
            this.subForumName = subForumName;

            adminUsername = WindowHelper.GetLoggedUsername(forumName);
            moderators = cl.GetModeratorsList(forumName, subForumName, adminUsername);
        }

        private void moderatorsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string moderatorUsername = moderatorsComboBox.SelectedValue.ToString();
            DateTime modExpDate = cl.GetModeratorExpDate(forumName, subForumName, moderatorUsername);
            expDatePicker.SelectedDate = modExpDate;
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new SubForumWindow(forumName, subForumName));
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            string moderatorUsername = moderatorsComboBox.SelectedValue.ToString();
            if (moderatorUsername == "" || moderatorUsername == null)
            {
                MessageBox.Show("please choose a moderator to edit");
                return;
            }
            DateTime? nullable_expDate = expDatePicker.SelectedDate;
            if (nullable_expDate == null)
            {
                MessageBox.Show("please choose an expiration date");
                return;
            }
            DateTime expDate = nullable_expDate.Value;

            try
            {
                cl.ChangeExpirationDate(forumName, subForumName, adminUsername, moderatorUsername, expDate);
            }
            catch (Exception)
            {
                MessageBox.Show("an error occured while sending your request");
                return;
            }
            WindowHelper.SwitchWindow(this, new SubForumWindow(forumName, subForumName));

        }

        private void removeModBtn_Click(object sender, RoutedEventArgs e)
        {
            string moderatorUsername = moderatorsComboBox.SelectedValue.ToString();
            if (moderatorUsername == "" || moderatorUsername == null)
            {
                MessageBox.Show("please choose a moderator to remove");
                return;
            }

            try
            {
                cl.RemoveModerator(forumName, subForumName, adminUsername, moderatorUsername);
            }
            catch (Exception)
            {
                MessageBox.Show("an error occured while sending your request");
                return;
            }
        }
    }
}
