﻿using ForumsSystemClient.Resources.ForumManagement.DomainLayer;
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
    /// Interaction logic for AddAdminWindow.xaml
    /// </summary>
    public partial class AddAdminWindow : Window
    {
        AddForumWindow addForumWin;

        public AddAdminWindow(AddForumWindow addForumWin)
        {
            InitializeComponent();

            WindowHelper.SetWindowBGImg(this);

            this.addForumWin = addForumWin;
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void addAdminBtn_Click(object sender, RoutedEventArgs e)
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






            if (nullable_dob == null)
            {
                MessageBox.Show("please choose date of birth");
                return;
            }

            DateTime dob = nullable_dob.Value;
            User admin = new User(username, password, email, dob);
            addForumWin.AddAdmin(admin);

            Close();

        }
    }
}
