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
    /// Interaction logic for EditModeratorsWindow.xaml
    /// </summary>
    public partial class EditModeratorsWindow : Window
    {
        private CL cl;
        private string forumName;
        private string subForumName;
        private List<string> moderators;

        public EditModeratorsWindow(string forumName, string subForumName)
        {
            InitializeComponent();

            WindowHelper.SetWindowBGImg(this);

            cl = new CL();
            this.forumName = forumName;
            this.subForumName = subForumName;

            string adminUsername = WindowHelper.GetLoggedUsername(forumName);
            moderators = cl.GetModeratorsList(forumName, subForumName, adminUsername);
        }

        private void moderatorsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new SubForumWindow(forumName, subForumName));
        }
    }
}
