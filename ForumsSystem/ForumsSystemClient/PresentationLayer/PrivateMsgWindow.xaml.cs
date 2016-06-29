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
    /// Interaction logic for PrivateMsgWindow.xaml
    /// </summary>
    public partial class PrivateMsgWindow : Window
    {
        public PrivateMsgWindow(string sender, string title, string content)
        {
            InitializeComponent();

            WindowHelper.SetWindowBGImg(this);

            senderTB.Text = sender;
            titleTB.Text = title;
            contentTB.Text = content;

        }
    }
}
