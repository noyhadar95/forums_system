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
        private double postBorderOffset = 10;
        private double contentTitleOffset = 20; // the offset from the title of the post to it's content


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
            //// ... Create a TreeViewItem.
            //TreeViewItem item = new TreeViewItem();
            //item.Header = CreatePostGrid();
            //TreeViewItem itemNest2 = new TreeViewItem();
            //itemNest2.Header = CreatePostGrid();
            //itemNest2.ItemsSource = new Grid[] { CreatePostGrid(), CreatePostGrid(), CreatePostGrid() };
            //((List<object>)itemNest2.ItemsSource).Add(CreatePostGrid());
            //List<object> objList = new List<object>();
            //objList.Add(itemNest2); objList.Add(CreatePostGrid()); objList.Add(CreatePostGrid());
            //item.ItemsSource = objList;
            //objList.Add(CreatePostGrid());
            //// ... Create a second TreeViewItem.
            //TreeViewItem item2 = new TreeViewItem();
            //item2.Header = "Outfit";
            //item2.ItemsSource = new string[] { "Pants", "Shirt", "Hat", "Socks" };

            //// ... Get TreeView reference and add both items.
            //var tree = sender as TreeView;
            //tree.Items.Add(item);
            //tree.Items.Add(item2);


            //TreeViewItem item = new TreeViewItem();
            string threadID = "";
            List<Post> posts = cl.GetPosts(threadID);
            //BuildTreeView(posts);

            //item.Header = CreatePostTVItem();
            //List<object> childs = new List<object>();
            //item.ItemsSource = childs;

            // Get TreeView reference and add the items for the posts.
            var tree = sender as TreeView;
            foreach (Post post in posts)
            {
                TreeViewItem item = new TreeViewItem();
                // handles nested posts too
                CreatePostTVItem(item, post);
                tree.Items.Add(item);
            }

        }

        // TODO: remove method
        // Recursive method that builds the tree view from a posts list.
        private void BuildTreeView(TreeViewItem item, List<Post> posts)
        {
            //Post first = posts.First();
            //List<Post> rest = posts.GetRange(1, posts.Count - 1);
            //// handles the nested posts for first
            //TreeViewItem item = CreatePostTVItem(first);

            //// add the rest of the items to the tree
            //BuildTreeView(tree, rest);

        }

        // CreatePostTreeViewItem
        // Recursive method that sets item.Header and item.ItemsSource according to
        // post, calling recursively on post.children
        private void CreatePostTVItem(TreeViewItem item, Post post)
        {
            item.Header = CreatePostBorder(post);
            List<TreeViewItem> childs = new List<TreeViewItem>();
            item.ItemsSource = childs;
            foreach (Post p in post.GetNestedPosts())
            {
                TreeViewItem newItem = new TreeViewItem();
                CreatePostTVItem(newItem, p);
                childs.Add(newItem);
            }
        }

        // return a grid with ListView that contains 2 TextBlocks
        private Border CreatePostBorder(Post post)
        {
            TextBlock titlTB = new TextBlock();
            titlTB.Text = post.Title;
            titlTB.Margin = new Thickness(postBorderOffset, postBorderOffset / 2, postBorderOffset, postBorderOffset);
            Separator seperator = new Separator();
            seperator.Margin = new Thickness(0, titlTB.Margin.Top+5, 0, postBorderOffset);
            TextBlock contentTB = new TextBlock();
            contentTB.MaxWidth = postsTreeView.Width;
            contentTB.TextWrapping = TextWrapping.WrapWithOverflow;
            contentTB.Text = post.Content;
            contentTB.Margin = new Thickness(postBorderOffset, seperator.Margin.Top + contentTitleOffset, postBorderOffset, postBorderOffset);

            //ListView listView = new ListView();
            //List<object> list = new List<object>();
            //list.Add(titlTB);
            //list.Add(contentTB);

            //listView.ItemsSource = list;
            Grid grid = new Grid();
            grid.Children.Add(titlTB);
            grid.Children.Add(seperator);
            grid.Children.Add(contentTB);
            //grid.Children.Add(listView);

            Border border = new Border();
            border.BorderThickness = new Thickness(0.3);
            border.BorderBrush = Brushes.Black;
            border.Child = grid;

            return border;
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new SubForumWindow(forumName, subForumName));
        }
    }
}
