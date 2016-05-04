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
    /// Interaction logic for AddReplyWindow.xaml
    /// </summary>
    public partial class AddReplyWindow : Window
    {
        private string forumName;
        private string subForumName;
        private StackPanel parentSP;

        public AddReplyWindow(string forumName, string subForumName, StackPanel sp)
        {
            InitializeComponent();

            this.forumName = forumName;
            this.subForumName = subForumName;
            parentSP = sp;
        }

        private Border CreateReplyBorder()
        {
            Border border = new Border();
            StackPanel sp = new StackPanel();
            TextBlock subjectTextBlock = new TextBlock();
            TextBox titleTextBox = new TextBox();
            TextBox contentTextBox = new TextBox();
            Button addReplyBtn = new Button();
            Button cancelBtn = new Button();
            AlignStackPanel horizontalSubjectSP = new AlignStackPanel();
            AlignStackPanel horizontalBtnsSP = new AlignStackPanel();

            subjectTextBlock.Text = "Title:";
            titleTextBox.Text = "Re:";
            addReplyBtn.Content = "add reply";
            cancelBtn.Content = "cancel";

            addReplyBtn.Click += new RoutedEventHandler(addReplyBtn_Click);

            horizontalSubjectSP.Children.Add(subjectTextBlock);
            horizontalSubjectSP.Children.Add(titleTextBox);

            horizontalBtnsSP.Children.Add(addReplyBtn);
            horizontalBtnsSP.Children.Add(cancelBtn);

            sp.Children.Add(horizontalSubjectSP);
            sp.Children.Add(contentTextBox);
            sp.Children.Add(horizontalBtnsSP);

            return border;
        }

        private void addReplyBtn_Click(object sender, RoutedEventArgs e)
        {
            parentSP.Children.Add(CreateReplyBorder());

            WindowHelper.SwitchWindow(this, new ThreadWindow(forumName, subForumName));
        }


    }
}
