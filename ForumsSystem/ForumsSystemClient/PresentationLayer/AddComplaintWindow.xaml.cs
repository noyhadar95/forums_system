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
    /// Interaction logic for AddComplaintWindow.xaml
    /// </summary>
    public partial class AddComplaintWindow : Window
    {
        CL cl;
        private string forumName;

        public AddComplaintWindow(string forumName)
        {
            InitializeComponent();

            WindowHelper.SetWindowBGImg(this);

            cl = new CL();
            this.forumName = forumName;

        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void sendBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
