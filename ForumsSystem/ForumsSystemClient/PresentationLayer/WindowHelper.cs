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
    }
}
