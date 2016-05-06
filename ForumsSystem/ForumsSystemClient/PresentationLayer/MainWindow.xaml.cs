using ForumsSystemClient.CommunicationLayer;
using ForumsSystemClient.Resources;
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

            // initialize forums list
            List<string> items = cl.GetForumsList();
            forumsListView.ItemsSource = items;

            // initialize different types grids (login, super admin)
            superAdminGrid.Visibility = Visibility.Hidden;


            if (WindowHelper.IsLoggedSuperAdmin())
            {
                // super admin is already logged in
                string superAdmin = WindowHelper.GetLoggedSuperAdmin();
                SwitchLoginToSAViewMode(superAdmin);
            }


            // bool ans = (bool)CommunicationLayer.Client.SendRequest("InitializeSystem", "superadmin", "pass");


        }

        // hide login grid, show super admin grid
        private void SwitchLoginToSAViewMode(string superAdmin)
        {
            superAdminLoginGrid.Visibility = Visibility.Hidden;
            superAdminGrid.Visibility = Visibility.Visible;
            welcomeTB.Text = "welcome " + superAdmin;
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

        private void logoutBtn_Click(object sender, RoutedEventArgs e)
        {
            superAdminLoginGrid.Visibility = Visibility.Visible;
            superAdminGrid.Visibility = Visibility.Hidden;
            WindowHelper.LogoutSuperAdmin();
        }
    }
}
