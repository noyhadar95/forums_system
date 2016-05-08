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
    /// Interaction logic for SubForumWindow.xaml
    /// </summary>
    public partial class SubForumWindow : Window
    {
        private CL cl;
        private string forumName;
        private string subForumName;

        public SubForumWindow(string forumName, string subForumName)
        {
            InitializeComponent();

            WindowHelper.SetWindowBGImg(this);

            cl = new CL();
            List<string> items = cl.GetThreadsList(forumName, subForumName);
            threadsListView.ItemsSource = items;

            Dictionary<int, string> dict = new Dictionary<int, string>();
            threadsListView.ItemsSource = dict;


            this.forumName = forumName;
            this.subForumName = subForumName;
            Title = subForumName;
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new ForumWindow(forumName));
        }

        private void threadsListView_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //KeyValuePair<int, string> keyValuePair = (KeyValuePair<int, string>)threadsListView.SelectedItem;

            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
<<<<<<< HEAD
                Window newWin = new ThreadWindow(forumName, subForumName);
=======
                Window newWin = new ThreadWindow(forumName, subForumName, 1);
>>>>>>> refs/remotes/origin/GUI-with-server
                WindowHelper.SwitchWindow(this, newWin);
            }
        }
    }
}
