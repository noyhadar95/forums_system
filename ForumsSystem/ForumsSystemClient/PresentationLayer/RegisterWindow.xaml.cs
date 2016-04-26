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

            cl = new CL();
            this.forumName = forumName;
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            // TODO: register the user
            string username = usernameTB.Text;
            string password = passwordTB.Text;
            string confPassword = confPassTB.Text;
            string email = emailTB.Text;
            DateTime? nullable_dob = dateOfBirthDP.SelectedDate;

            // check all fields are not empty
            if (nullable_dob == null)
            {
                //TOD: handle no date
            }
            else {
                DateTime dob = nullable_dob.Value;
            }

            // check password confirmation

            WindowHelper.SwitchWindow(this, new ForumWindow(forumName));
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new ForumWindow(forumName));
        }
    }
}
