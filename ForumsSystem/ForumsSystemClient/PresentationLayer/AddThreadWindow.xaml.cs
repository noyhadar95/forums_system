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
    /// Interaction logic for AddThreadWindow.xaml
    /// </summary>
    public partial class AddThreadWindow : Window
    {
        private string forumName;
        private string subForumName;
        private CL cl;

        public AddThreadWindow(string  forumName, string  subForumName)
        {
            InitializeComponent();

            WindowHelper.SetWindowBGImg(this);

            cl = new CL();
            this.forumName = forumName;
            this.subForumName = subForumName;
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new SubForumWindow(forumName, subForumName));
        }

        private void addThreadBtn_Click(object sender, RoutedEventArgs e)
        {
            string title = titleTB.Text;
            string content = contentTB.Text;

            if (title == "" && content == "")
            {
                MessageBox.Show("please enter either a title or a content for the topic");
                return;
            }
            // TODO:
            //cl.AddThread();
            WindowHelper.SwitchWindow(this, new SubForumWindow(forumName, subForumName));
        }
    }
}
