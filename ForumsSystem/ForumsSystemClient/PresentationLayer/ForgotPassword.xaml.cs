using ForumsSystemClient.CommunicationLayer;
using ForumsSystemClient.Resources.UserManagement.DomainLayer;
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
    /// Interaction logic for ForgotPassword.xaml
    /// </summary>
    public partial class ForgotPassword : Window
    {
        CL cl;
        private string forumName;


        public ForgotPassword(string forumName)
        {
            InitializeComponent();

            WindowHelper.SetWindowBGImg(this);

            cl = new CL();
            this.forumName = forumName;

            this.cbbx_questions.ItemsSource = SecurityQuestions.questions;//(typeof(SecurityQuestionsEnum)).Cast<SecurityQuestionsEnum>();

            this.cbbx_questions.SelectedIndex = 0;
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
                MessageBox.Show("Enter valid UserName");
                return;
            }

            if (!rgx.IsMatch(txt_newPass.Password))
            {
                MessageBox.Show("Enter valid Password");
                return;
            }
            if (!txt_newPass.Password.Equals(txt_newPassCon.Password))
            {
                MessageBox.Show("Passwords do not match");
                return;
            }


            if (cl.CheckSecurityQuestion(forumName, txt_username.Text, cbbx_questions.SelectedIndex, txt_answer.Text,txt_newPass.Password))
            {
                MessageBox.Show("Successfully Changed Password");
                WindowHelper.SwitchWindow(this, new ForumWindow(forumName));

            }
            else
            {
                MessageBox.Show("Incorrect Answer or bad password");
            }
        }
    }
}
