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
    /// Interaction logic for UserPostsWindow.xaml
    /// </summary>
    public partial class UserPostsWindow : Window
    {
        private CL cl;
        private string forumName;
        private string username; // show the posts of this user

        private double firstLevelItemOffset = 70; // offset of the items in the first level of the treeview
        private List<Post> posts;

        public UserPostsWindow(string forumName, string username)
        {
            InitializeComponent();

            WindowHelper.SetWindowBGImg(this);

            cl = new CL();
            this.forumName = forumName;
            this.username = username;
            usernameTB.Text = username;

            noPostsLbl.Visibility = Visibility.Hidden;
        }

        private void postsTreeView_Loaded(object sender, RoutedEventArgs e)
        {
            posts = cl.ReportPostsByMember(forumName, WindowHelper.GetLoggedUsername(forumName), username);
            // update postsCount text block
            postsCountTB.Text = "" + posts.Count;

            if (posts.Count == 0)
            {
                noPostsLbl.Visibility = Visibility.Visible;
            }

            // Get TreeView reference and add the items for the posts.
            var tree = sender as TreeView;
            foreach (Post post in posts)
            {
                TreeViewItem item = new TreeViewItem();
                tree.Items.Add(item);
                // handles nested posts too
                CreatePostTVItem(item, post, firstLevelItemOffset);
            }

        }

        // CreatePostTreeViewItem
        // Recursive method that sets item.Header and item.ItemsSource according to
        // post, calling recursively on post.children
        private void CreatePostTVItem(TreeViewItem item, Post post, double nestedItemOffset)
        {
            Border border = CreatePostBorder(post);
            border.Width = postsTreeView.Width - nestedItemOffset;
            item.Header = border;

        }

        // return border with stack pnael that contains the controls for a post
        private Border CreatePostBorder(Post post)
        {
            // post title
            TextBlock titleTB = new TextBlock();
            titleTB.Text = post.Title;
            titleTB.Background = Brushes.AliceBlue;

            // post content
            TextBlock contentTB = new TextBlock();
            contentTB.MaxWidth = postsTreeView.Width;
            contentTB.TextWrapping = TextWrapping.WrapWithOverflow;
            contentTB.Text = post.Content;

            // stack panel for the whole post
            StackPanel sp = new StackPanel();
            sp.Children.Add(WrapElementWithBorder(titleTB));
            sp.Children.Add(WrapElementWithBorder(contentTB));

            // post border
            Border border = new Border();
            border.Margin = new Thickness(0, 10, 15, 0);
            border.BorderThickness = new Thickness(0.3);
            border.BorderBrush = Brushes.Black;
            border.Child = sp;

            return border;
        }

        private Border WrapElementWithBorder(UIElement c)
        {
            Border border = new Border();
            border.BorderThickness = new Thickness(0.3);
            border.BorderBrush = Brushes.Black;
            border.Child = c;
            return border;
        }



    }
}
