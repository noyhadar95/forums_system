using ForumsSystemClient.CommunicationLayer;
using ForumsSystemClient.Resources.ForumManagement.DomainLayer;
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
    /// Interaction logic for SuperAdminLoginWindow.xaml
    /// </summary>
    public partial class SuperAdminLoginWindow : Window
    {
        private CL cl;

        public SuperAdminLoginWindow()
        {
            InitializeComponent();

            WindowHelper.SetWindowBGImg(this);

            cl = new CL();
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new MainWindow());
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTB.Text;
            string password = passwordBox.Password;

            if (username == "" || password == "")
            {
                MessageBox.Show("Missing login information, please try again");
                return;
            }

            bool success = cl.LoginSuperAdmin(username, password);
            if (success)
            {
                SuperAdmin sa = new SuperAdmin();
                sa.userName = username;
                sa.password = password;
                WindowHelper.SetLoggedSuperAdmin(sa);
                WindowHelper.SwitchWindow(this, new MainWindow());
            }
            else
            {
                MessageBox.Show("Incorrect username/password, please try again");
            }

        }
    }
}
