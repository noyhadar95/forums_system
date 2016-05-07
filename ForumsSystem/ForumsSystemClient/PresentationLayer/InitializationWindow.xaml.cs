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
    /// Interaction logic for InitializationWindow.xaml
    /// </summary>
    public partial class InitializationWindow : Window
    {
        private CL cl;

        public InitializationWindow()
        {
            InitializeComponent();

            WindowHelper.SetWindowBGImg(this);

            cl = new CL();
        }

        private void initializeBtn_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTB.Text;
            string password = passwordBox.Password;

            if (username == "" || password == "")
            {
                MessageBox.Show("please enter valid information");
                return;
            }
            cl.InitializeSystem(username, password);
            WindowHelper.SwitchWindow(this, new MainWindow());
        }
    }
}
