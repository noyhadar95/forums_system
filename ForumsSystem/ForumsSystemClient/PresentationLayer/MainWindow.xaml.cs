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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CL cl;

        public MainWindow()
        {
            InitializeComponent();

            WindowHelper.SetWindowBGImg(this);

            cl = new CL();

            // check if the system is initialized
            if (!cl.IsInitialized())
            {
                WindowHelper.SwitchWindow(this, new InitializationWindow());
            }


            List<string> items = cl.GetForumsList();
            forumsListView.ItemsSource = items;


            bool ans = (bool)CommunicationLayer.Client.SendRequest("InitializeSystem", "superadmin", "pass");


        }

        private void forumsListView_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                Window newWin = new ForumWindow((string)item);
                WindowHelper.SwitchWindow(this, newWin);
            }
        }

        private void superAdminLoginBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new SuperAdminLoginWindow());
        }

        private void createForumBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new AddForumWindow());
        }
    }
}
