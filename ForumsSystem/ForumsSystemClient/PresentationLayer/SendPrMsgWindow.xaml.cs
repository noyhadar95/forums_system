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
    /// Interaction logic for SendPrMsgWindow.xaml
    /// </summary>
    public partial class SendPrMsgWindow : Window
    {
        private CL cl;
        private string forumName;
        private string sender;

        public SendPrMsgWindow(string forumName, string sender)
        {
            InitializeComponent();

            WindowHelper.SetWindowBGImg(this);

            cl = new CL();
            this.forumName = forumName;
            this.sender = sender;
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void sendBtn_Click(object sender, RoutedEventArgs e)
        {
            string receiver = sendToTB.Text;
            string title = titleTB.Text;
            string content = contentTB.Text;

            if (receiver == "" || !cl.IsExistUser(receiver, forumName))
            {
                MessageBox.Show("please enter a valid username to send the message to");
                return;
            }
            if (title == "" && content == "")
            {
                MessageBox.Show("please enter either a title or a content for the message");
                return;
            }

            bool isSuccess = cl.SendPrivateMessage(forumName,this.sender, receiver, title, content);
            if (isSuccess)
            {
                MessageBox.Show("message sent");
                this.Close();
            }
            else
            {
                MessageBox.Show("there was an error, please try again");
            }
        }
    }
}
