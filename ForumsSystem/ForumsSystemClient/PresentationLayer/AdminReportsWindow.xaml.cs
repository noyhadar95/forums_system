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

}
