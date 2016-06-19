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
    /// Interaction logic for SuperAdminReportsWindow.xaml
    /// </summary>
    public partial class SuperAdminReportsWindow : Window
    {
        private CL cl;

        public SuperAdminReportsWindow()
        {
            InitializeComponent();

            WindowHelper.SetWindowBGImg(this);

            cl = new CL();

            SuperAdmin sa = WindowHelper.GetLoggedSuperAdmin();
            if (sa != null)
            {
                int numOfForums = cl.GetNumOfForums(sa.userName, sa.password);
                numOfForumsTB.Text = "" + numOfForums;

                Dictionary<string, List<Tuple<string, string>>> dict = cl.GetMultipleUsersInfoBySuperAdmin(sa.userName, sa.password);

                foreach (KeyValuePair<string, List<Tuple<string, string>>> pair in dict)
                {
                    string email = pair.Key;
                    List<Tuple<string, string>> forumUsernamelist = pair.Value;
                    foreach (Tuple<string, string> tuple in forumUsernamelist)
                    {
                        string forumName = tuple.Item1;
                        string Username = tuple.Item2;
                        emailsListView.Items.Add(new EmailListItem { Email = email, ForumName = forumName, Username = Username });

                    }

                }
            }

        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new MainWindow());
        }


    }

    public class EmailListItem
    {
        public string Email { get; set; }

        public string ForumName { get; set; }

        public string Username { get; set; }
    }
}
