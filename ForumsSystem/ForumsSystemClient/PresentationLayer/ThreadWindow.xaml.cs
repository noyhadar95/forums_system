using ForumsSystemClient.CommunicationLayer;
using ForumsSystemClient.Resources;
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
        private List<Post> posts;
        private double firstLevelItemOffset = 70; // offset of the items in the first level of the treeview
        private Dictionary<Button, StackPanel> btnSPParents;
        private Dictionary<Border, Post> borderPostDict;
        private bool isAddReplyMode = false;
        private Button addReplyModeCancelBtn = null; // save the cancel button, so we will be able to exit add-reply-mode when another reply-button is clicked

        public ThreadWindow(string forumName, string subForumName)
        {
            InitializeComponent();

            WindowHelper.SetWindowBGImg(this);

            cl = new CL();
            this.forumName = forumName;
            this.subForumName = subForumName;
            btnSPParents = new Dictionary<Button, StackPanel>();
            borderPostDict = new Dictionary<Border, Post>();
        }

        private void postsTreeView_Loaded(object sender, RoutedEventArgs e)
        {
            string threadID = "";
            posts = cl.GetPosts(threadID);

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
            titleTB.Background = Brushes.AliceBlue;

            TextBlock contentTB = new TextBlock();
            contentTB.MaxWidth = postsTreeView.Width;
            contentTB.TextWrapping = TextWrapping.WrapWithOverflow;
            contentTB.Text = post.Content;

            // create the reply button
            Button replyBtn = new Button();
            replyBtn.Content = "reply";
            replyBtn.Margin = new Thickness(5);
            replyBtn.Click += new RoutedEventHandler(replyBtn_Click);
            TextBlock replyTB = new TextBlock();
            replyTB.Inlines.Add(replyBtn);

            // create the delete button
            Button deleteBtn = new Button();
            deleteBtn.Content = "delete";
            deleteBtn.Margin = new Thickness(5);
            TextBlock deleteTB = new TextBlock();
            deleteTB.Inlines.Add(deleteBtn);

            AlignStackPanel horizontalSP = new AlignStackPanel();
            horizontalSP.Orientation = Orientation.Horizontal;
            horizontalSP.Children.Add(replyTB);
            horizontalSP.Children.Add(deleteTB);

            StackPanel sp = new StackPanel();
            sp.Children.Add(WrapElementWithBorder(titleTB));
            sp.Children.Add(WrapElementWithBorder(contentTB));
            sp.Children.Add(WrapElementWithBorder(horizontalSP));

            btnSPParents.Add(replyBtn, sp);

            Border border = new Border();
            border.Margin = new Thickness(0, 10, 15, 0);
            border.BorderThickness = new Thickness(0.3);
            border.BorderBrush = Brushes.Black;
            border.Child = sp;

            // save the created border with it's associated post
            borderPostDict.Add(border, post);

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

        private Border CreateAddReplyBorder(StackPanel parentSP)
        {
            Border border = new Border();
            StackPanel sp = new StackPanel();
            TextBlock subjectTextBlock = new TextBlock();
            TextBox titleTextBox = new TextBox();
            TextBox contentTextBox = new TextBox();
            Button addReplyBtn = new Button();
            Button cancelBtn = new Button();
            AlignStackPanel horizontalSubjectSP = new AlignStackPanel();
            AlignStackPanel horizontalBtnsSP = new AlignStackPanel();

            subjectTextBlock.Text = "Title:";
            titleTextBox.Text = "Re:";
            addReplyBtn.Content = "add reply";
            cancelBtn.Content = "cancel";

            // save the cancel button in field
            addReplyModeCancelBtn = cancelBtn;

            // set buttons click handlers
            addReplyBtn.Click += new RoutedEventHandler(addReplyBtn_Click);
            cancelBtn.Click += new RoutedEventHandler(cancelBtn_Click);
            btnSPParents.Add(addReplyBtn, parentSP);
            btnSPParents.Add(cancelBtn, parentSP);

            // set buttons margins
            addReplyBtn.Margin = new Thickness(5);
            cancelBtn.Margin = new Thickness(5);

            // set horizontal stack panel for subject label and title text box
            horizontalSubjectSP.Children.Add(subjectTextBlock);
            horizontalSubjectSP.Children.Add(titleTextBox);
            horizontalSubjectSP.Orientation = Orientation.Horizontal;

            // set horizontal stack panel for buttons
            horizontalBtnsSP.Children.Add(addReplyBtn);
            horizontalBtnsSP.Children.Add(cancelBtn);
            horizontalBtnsSP.Orientation = Orientation.Horizontal;

            // add elements to the root stack panel
            sp.Children.Add(horizontalSubjectSP);
            sp.Children.Add(contentTextBox);
            sp.Children.Add(horizontalBtnsSP);

            border.Child = sp;
            return border;
        }

        private void replyBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isAddReplyMode)
            {
                // cancel the current add reply mode in order to start a new one.
                StackPanel cancelBtnParentSP = btnSPParents[addReplyModeCancelBtn];
                cancelBtnParentSP.Children.RemoveAt(cancelBtnParentSP.Children.Count - 1);
                isAddReplyMode = false;
                addReplyModeCancelBtn = null;

                postsTreeView.Items.Refresh();
            }

            Button btn = (Button)e.OriginalSource;
            StackPanel parentSP = btnSPParents[btn];
            // send parentSP as parameter so it could be bounded to the buttons inside
            // the add reply border, this method also sets the field addReplyModeCancelBtn
            Border b = CreateAddReplyBorder(parentSP);
            parentSP.Children.Add(b);

            isAddReplyMode = true;

            postsTreeView.Items.Refresh();
        }

        private void addReplyBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)e.OriginalSource;
            //StackPanel addReplySP = (StackPanel)(btn.Parent as AlignStackPanel).Parent;
            StackPanel parentSP = btnSPParents[btn];
            Border addReplySPBorder= (Border)(parentSP.Children[parentSP.Children.Count -1]);
            StackPanel addReplySP =(StackPanel)addReplySPBorder.Child;

            // retrieve title of reply
            if (!(addReplySP.Children[0] is AlignStackPanel))
            {
                MessageBox.Show("there was an error while adding your reply, please try again");
                return;
            }
            AlignStackPanel horizontalTitleSP = ((AlignStackPanel)addReplySP.Children[0]);
            if (!(horizontalTitleSP.Children[1] is TextBox))
            {
                MessageBox.Show("there was an error while adding your reply, please try again");
                return;
            }
            string replyTitle = ((TextBox)horizontalTitleSP.Children[1]).Text;

            // retrieve content of reply
            if (!(addReplySP.Children[1] is TextBox))
            {
                MessageBox.Show("there was an error while adding your reply, please try again");
                return;
            }
            string replyContent = ((TextBox)addReplySP.Children[1]).Text;

            // retrieve the parent border
            if (!(parentSP.Parent is Border))
            {
                MessageBox.Show("there was an error while adding your reply, please try again");
                return;
            }
            Border parentBorder = (Border)parentSP.Parent;
            Post parentPost = borderPostDict[parentBorder];
            //cl.AddReply(parentPost, publisher, replyTitle, replyContent);

            // TODO: reload the tree veiw
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)e.OriginalSource;
            StackPanel parentSP = btnSPParents[btn];
            parentSP.Children.RemoveAt(parentSP.Children.Count - 1);
            isAddReplyMode = false;
            addReplyModeCancelBtn = null;

            postsTreeView.Items.Refresh();
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new SubForumWindow(forumName, subForumName));
        }
    }
}
