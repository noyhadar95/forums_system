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
    /// Interaction logic for ThreadWindow.xaml
    /// </summary>
    public partial class ThreadWindow : Window
    {
        private CL cl;
        private string forumName;
        private string subForumName;
        private double firstLevelItemOffset = 70; // offset of the items in the first level of the treeview


        public ThreadWindow(string forumName, string subForumName)
        {
            InitializeComponent();

            WindowHelper.SetWindowBGImg(this);

            cl = new CL();
            this.forumName = forumName;
            this.subForumName = subForumName;
        }

        private void postsTreeView_Loaded(object sender, RoutedEventArgs e)
        {
            string threadID = "";
            List<Post> posts = cl.GetPosts(threadID);

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

            foreach (Post p in post.GetNestedPosts())
            {
                TreeViewItem newItem = new TreeViewItem();
                item.Items.Add(newItem);
                CreatePostTVItem(newItem, p, nestedItemOffset + 30);
            }
        }

        // return border with stack pnael that contains the controls for a post
        private Border CreatePostBorder(Post post)
        {
            TextBlock titleTB = new TextBlock();
            titleTB.Text = post.Title;

            TextBlock contentTB = new TextBlock();
            contentTB.MaxWidth = postsTreeView.Width;
            contentTB.TextWrapping = TextWrapping.WrapWithOverflow;
            contentTB.Text = post.Content;

            Hyperlink replyLink = new Hyperlink();
            TextBlock replyTB = new TextBlock();
            replyLink.Inlines.Add("reply");
            replyTB.Inlines.Add(replyLink);

            StackPanel sp = new StackPanel();
            sp.Children.Add(titleTB);
            sp.Children.Add(contentTB);
            sp.Children.Add(replyTB);

            Border border = new Border();
            border.Margin = new Thickness(0, 0, 15, 0);
            border.BorderThickness = new Thickness(0.3);
            border.BorderBrush = Brushes.Black;
            border.Child = sp;

            return border;
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new SubForumWindow(forumName, subForumName));
        }
    }
}
