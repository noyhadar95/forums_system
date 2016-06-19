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
                // int totalPosts = cl
                int totalPost = -1;
                subForumsListView.Items.Add(new SubForumListItem { SubForum = subForumName, TotalPosts = totalPost });
            }




        }


        //private void postsTreeView_Loaded(object sender, RoutedEventArgs e)
        //{
        //    string username;
        //    cl.ReportPostsByMember(forumName, WindowHelper.GetLoggedUsername(forumName), username);
        //    posts = cl.GetPosts(forumName, subForumName, threadID);

        //    // Get TreeView reference and add the items for the posts.
        //    var tree = sender as TreeView;
        //    foreach (Post post in posts)
        //    {
        //        TreeViewItem item = new TreeViewItem();
        //        tree.Items.Add(item);
        //        // handles nested posts too
        //        CreatePostTVItem(item, post, firstLevelItemOffset);
        //    }

        //}


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
