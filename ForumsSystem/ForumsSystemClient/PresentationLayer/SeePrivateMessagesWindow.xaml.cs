using ForumsSystemClient.CommunicationLayer;
using ForumsSystemClient.Resources.ForumManagement.DomainLayer;
using ForumsSystemClient.Resources.UserManagement.DomainLayer;
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
    /// Interaction logic for SeePrivateMessagesWindow.xaml
    /// </summary>
    public partial class SeePrivateMessagesWindow : NotifBarWindow
    {
        private int CONTENT_LENGTH_TO_SHOW = 10;

        public SeePrivateMessagesWindow(string forumName) : base(forumName)
        {
            InitializeComponent();
            WindowHelper.SetWindowBGImg(this);

            cl = new CL();
            base.Initialize(dockPanel);

            List<PrivateMessage> pmList = cl.getReceivedMessages(forumName, loggedUsername);
            foreach (PrivateMessage pm in pmList)
            {
                string content;
                if (pm.content.Length < CONTENT_LENGTH_TO_SHOW)
                    content = pm.content;
                else
                    content = pm.content.Substring(0, 10) + "...";
                pmListView.Items.Add(new PMListItem { Sender = pm.senderUsername, Title = pm.title, Content = content });
            }

            RefreshNotificationsBar(loggedUsername);
        }


        private void pmListView_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                PMListItem pmItem = ((PMListItem)item);
                Window newWin = new PrivateMsgWindow(pmItem.Sender, pmItem.Title, pmItem.Content);
                WindowHelper.ShowWindow(this, newWin);
            }
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new ForumWindow(forumName));
        }

    }


    public class PMListItem
    {
        public string Sender { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }

}
