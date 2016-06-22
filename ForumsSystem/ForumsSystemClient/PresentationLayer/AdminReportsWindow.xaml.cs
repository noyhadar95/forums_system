using ForumsSystemClient.CommunicationLayer;
using ForumsSystemClient.Resources.ForumManagement.DomainLayer;
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
    /// Interaction logic for AdminReportsWindow.xaml
    /// </summary>
    public partial class AdminReportsWindow : Window
    {
        private CL cl;
        private string forumName;

        public AdminReportsWindow(string forumName)
        {
            InitializeComponent();

            WindowHelper.SetWindowBGImg(this);

            cl = new CL();
            this.forumName = forumName;

            List<string> subForums = cl.GetSubForumsList(forumName);
            foreach (string subForumName in subForums)
            {
                // TODO::
                // int totalPosts = cl.GetTotalPosts(forumName, );
                int totalPost = -1;
                subForumsListView.Items.Add(new SubForumListItem { SubForum = subForumName, TotalPosts = totalPost });
            }

            List<string> members = cl.GetForumMembers(forumName);
            foreach (string mem in members)
            {
                membersListView.Items.Add(new MembersListItem { Member = mem });
            }

            List<Tuple<string, string, DateTime, string>> modsDetailsList = cl.ReportModeratorsDetails(forumName, WindowHelper.GetLoggedUsername(forumName));
            foreach (Tuple<string, string, DateTime, string> modsDetails in modsDetailsList)
            {
                moderatorsListView.Items.Add(new ModeratorsListItem
                {
                    Username = modsDetails.Item1,
                    Appointer = modsDetails.Item2,
                    AppointmentDate = modsDetails.Item3,
                    SubForum = modsDetails.Item4
                });
            }

        }

        private void membersListView_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                Window newWin = new UserPostsWindow(forumName, ((MembersListItem)item).Member);
                WindowHelper.ShowWindow(this, newWin);
            }
        }

        private void moderatorsListView_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                Window newWin = new UserPostsWindow(forumName, ((ModeratorsListItem)item).Username);
                WindowHelper.ShowWindow(this, newWin);
            }
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new ForumWindow(forumName));
        }

    }

    public class SubForumListItem
    {
        public string SubForum { get; set; }

        public int TotalPosts { get; set; }
    }

    public class MembersListItem
    {
        public string Member { get; set; }
    }

    public class ModeratorsListItem
    {
        public string Username { get; set; }
        public string Appointer { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string SubForum { get; set; }
    }

}
