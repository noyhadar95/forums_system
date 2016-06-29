using ForumsSystemClient.CommunicationLayer;
using ForumsSystemClient.Resources.ForumManagement.DomainLayer;
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
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        CL cl;
        private string forumName;

        public RegisterWindow(string forumName)
        {
            InitializeComponent();

            WindowHelper.SetWindowBGImg(this);

            cl = new CL();
            this.forumName = forumName;

            this.SecurityQuestionCB.ItemsSource = SecurityQuestions.questions;//(typeof(SecurityQuestionsEnum)).Cast<SecurityQuestionsEnum>();
            
            this.SecurityQuestionCB.SelectedIndex = 0;
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTB.Text;
            string password = passwordBox.Password;
            string confPassword = confPasswordBox.Password;
            string email = emailTB.Text;
            DateTime? nullable_dob = dateOfBirthDP.SelectedDate;
            string answer = SecurityAnswerTB.Text;

            // check that all fields are not empty
            if (username == "")
            {
                MessageBox.Show("please enter a username");
                return;
            }
            if (password == "")
            {
                MessageBox.Show("please enter a password");
                return;
            }
            if (confPassword == "")
            {
                MessageBox.Show("please confirm your password");
                return;
            }
            // check password confirmation
            if (password != confPassword)
            {
                MessageBox.Show("passwords don't match, please try again");
                return;
            }
            if (email == "")
            {
                MessageBox.Show("please enter an email address");
                return;
            }
            if (!(email.Contains("@") && email.Contains(".") && email.IndexOf('@') < email.IndexOf('.')))
            {
                MessageBox.Show("please enter a valid email address");
                return;
            }
            if (nullable_dob == null)
            {
                MessageBox.Show("please choose date of birth");
                return;
            }
            if (answer == "")
            {
                MessageBox.Show("please enter an answer");
                return;
            }

            if (cl.CheckIfPolicyExists(forumName, Policies.Password))
            {
                Forum forum = cl.GetForum(forumName);
                Policy p = forum.GetPolicy();
                while (p != null && p.Type != Policies.Password)
                    p = p.NextPolicy;
                if (p != null && password.Length < ((PasswordPolicy)p).RequiredLength)
                {
                    MessageBox.Show("password length is required to be at least " + ((PasswordPolicy)p).RequiredLength);
                    return;
                }
            }


            Regex rgx = new Regex(@"^[a-z0-9_-]{1,16}$");
            if (!rgx.IsMatch(username))
            {
                MessageBox.Show("Enter valid UserName");
                return;
            }

            if (!rgx.IsMatch(password))
            {
                MessageBox.Show("Enter valid Password");
                return;
            }

            rgx = new Regex(@"^([a-z0-9_\.-]+)@([\da-z\.-]+)\.([a-z\.]{2,6})$");
            if (!rgx.IsMatch(email))
            {
                MessageBox.Show("Enter valid Email");
                return;
            }





            DateTime dob = nullable_dob.Value;
            int question = SecurityQuestionCB.SelectedIndex;
            bool isRegistered = cl.RegisterToForum(forumName, username, password, email, dob, question,answer);
            

            
           

            if (isRegistered)
            {
                MessageBox.Show("you have been successfully registered to the forum");
                WindowHelper.SwitchWindow(this, new ForumWindow(forumName));
            }
            else
            {
                MessageBox.Show("registration have failed, please try again");
            }

        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new ForumWindow(forumName));
        }
    }
}
