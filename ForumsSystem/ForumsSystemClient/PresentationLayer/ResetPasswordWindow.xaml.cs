using ForumsSystemClient.CommunicationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for ResetPasswordWindow.xaml
    /// </summary>
    public partial class ResetPasswordWindow : Window
    {
        private CL cl;
        private string forumName;

        public ResetPasswordWindow(string forumName)
        {
            InitializeComponent();

            WindowHelper.SetWindowBGImg(this);

            cl = new CL();
            this.forumName = forumName;

        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new ForumWindow(forumName));
        }

        private void btn_reset_Click(object sender, RoutedEventArgs e)
        {

            Regex rgx = new Regex(@"^[a-z0-9_-]{1,16}$");
            if (!rgx.IsMatch(txt_username.Text))
            {
                MessageBox.Show("Username is incorrect");
                return;
            }
            if (!rgx.IsMatch(txt_oldPass.Text))
            {
                MessageBox.Show("Old password is incorrect");
                return;
            }

            if (!rgx.IsMatch(txt_newPass.Text))
            {
                MessageBox.Show("Enter valid Password");
                return;
            }
            if (!txt_newPass.Text.Equals(txt_newPassCon.Text))
            {
                MessageBox.Show("Passwords do not match");
                return;
            }

            bool res = cl.SetUserPassword(forumName, txt_username.Text, txt_oldPass.Text, txt_newPass.Text);
            if (res)
            {
                MessageBox.Show("password has been successfully reset");
                WindowHelper.SwitchWindow(this, new ForumWindow(forumName));
            }
            else
            {
                MessageBox.Show("Incorrect information, please try again");
            }
        }

    }
}
