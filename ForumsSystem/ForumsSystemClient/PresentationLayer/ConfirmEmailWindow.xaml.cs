﻿using ForumsSystemClient.CommunicationLayer;
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
    /// Interaction logic for ConfirmEmailWindow.xaml
    /// </summary>
    public partial class ConfirmEmailWindow : Window
    {
        private CL cl;
        private string forumName;

        public ConfirmEmailWindow(string forumName)
        {
            InitializeComponent();

            WindowHelper.SetWindowBGImg(this);

            cl = new CL();
            this.forumName = forumName;

        }

        private void confirmBtn_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTB.Text;
            string token = tokenTB.Text;
            if (token == "" || username == "")
            {
                MessageBox.Show("incorrect information, please try again");
                return;
            }

            // TODO: bool res = cl.ConfirmRegistration(forumName, username, token);

            MessageBox.Show("registration has been completed\ntry to login");
            WindowHelper.SwitchWindow(this, new ForumWindow(forumName));
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new ForumWindow(forumName));
        }


    }
}
