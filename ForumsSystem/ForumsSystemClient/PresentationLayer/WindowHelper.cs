using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ForumsSystemClient.PresentationLayer
{
    class WindowHelper
    {
        public static void SwitchWindow(Window oldWin, Window newWin)
        {
            newWin.Left = oldWin.Left;
            newWin.Top = oldWin.Top;
            newWin.Show();
            oldWin.Close();
        }

        // show newWin without closing oldWin.
        public static void ShowWindow(Window oldWin, Window newWin)
        {
            newWin.Left = oldWin.Left;
            newWin.Top = oldWin.Top;
            newWin.Show();
        }

        public static void SetWindowBGImg(Window win)
        {
            Style style = Application.Current.FindResource("WindowStyle") as Style;
            win.Style = style;
        }

    }
}
