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
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            // TODO: consider forum policies, maybe email confirmation is needed
            string username = usernameTB.Text;
            string password = passwordBox.Password;
            string confPassword = confPasswordBox.Password;
            string email = emailTB.Text;
            DateTime? nullable_dob = dateOfBirthDP.SelectedDate;

            // check that all fields are not empty
            if (username == "")
            {
                MessageBox.Show("please enter a username");
                //Label markFieldLbl = new Label();
                //markFieldLbl.Content = "*";
                //markFieldLbl.Foreground = Brushes.Red;
                //markFieldLbl.Margin = new Thickness(usernameTB.Margin.Left - 20, usernameTB.Margin.Top,
                //    usernameTB.Margin.Right, usernameTB.Margin.Bottom);
                //grid.Children.Add(markFieldLbl);

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

            DateTime dob = nullable_dob.Value;
            bool isRegistered = cl.RegisterToForum(forumName, username, password, email, dob);
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
