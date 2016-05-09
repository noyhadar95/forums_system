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
    /// Interaction logic for EditPostWindow.xaml
    /// </summary>
    public partial class EditPostWindow : Window
    {
        private string forumName;
        private string subForumName;
        private int threadID;
        private Post post;
        private CL cl;

        public EditPostWindow(string forumName, string subForumName, int threadID, Post post)
        {
            InitializeComponent();

            cl = new CL();
            this.forumName = forumName;
            this.subForumName = subForumName;
            this.post = post;

            contentTB.Text = post.Content;
            titleTB.Text = post.Title;
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new ThreadWindow(forumName, subForumName, threadID));
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            string editor = WindowHelper.GetLoggedUsername(forumName);
            cl.EditPost(forumName, subForumName, threadID, editor, post.GetId(), titleTB.Text, contentTB.Text);

            WindowHelper.SwitchWindow(this, new ThreadWindow(forumName, subForumName, threadID));
        }
    }
}
