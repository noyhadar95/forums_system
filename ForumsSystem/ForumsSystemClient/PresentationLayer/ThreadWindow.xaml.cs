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

        public ThreadWindow()
        {
            InitializeComponent();

            cl = new CL();
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
            item.Header = CreatePostGrid(post);
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
        private Grid CreatePostGrid(Post post)
        {
            TextBlock titlTB = new TextBlock();
            titlTB.Text = post.Title;
            TextBlock contentTB = new TextBlock();
            contentTB.MaxWidth = postsTreeView.Width;
            contentTB.TextWrapping = TextWrapping.WrapWithOverflow;
            contentTB.Text = post.Content;

            ListView listView = new ListView();
            List<object> list = new List<object>();
            list.Add(titlTB);
            list.Add(contentTB);

            listView.ItemsSource = list;
            Grid grid = new Grid();
            grid.Children.Add(listView);
            return grid;
        }
    }
}
