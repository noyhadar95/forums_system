using ForumsSystemClient.CommunicationLayer;
using ForumsSystemClient.PresentationLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ForumsSystemClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnExit(ExitEventArgs e)
        {
            // try logout if possible
            //ICL cl = new CL();
            //cl.LogoutAll();

            base.OnExit(e);
        }
    }
}
