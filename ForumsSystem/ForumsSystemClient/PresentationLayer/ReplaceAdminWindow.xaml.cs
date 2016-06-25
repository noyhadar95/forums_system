using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ReplaceAdminWindow.xaml
    /// </summary>
    public partial class ReplaceAdminWindow : Window
    {
        private string forumName;
        private string loggedUsername;
        private CommunicationLayer.CL cl;

        private ObservableCollection<string> nonAdmins;
        public ReplaceAdminWindow(string forumName)
        {
            InitializeComponent();

            this.forumName = forumName;

            cl = new CommunicationLayer.CL();
            List<string> nonAdminsList = cl.getNonAdmins(forumName);

            nonAdmins = new ObservableCollection<string>(nonAdminsList);
            lv_users.ItemsSource = nonAdmins;
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new ForumWindow(forumName));
        }

        private void btn_switch_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
