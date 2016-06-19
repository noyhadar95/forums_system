
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication.Communication;
using WebApplication.Resources.ForumManagement.DomainLayer;

namespace WebApplication
{
    public partial class FirstPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ListBox1.Items.Clear();
                ICL cl = new CL();
                List<string> items = cl.GetForumsList();
                for (int i = 0; i < items.Count; i++)
                {
                    ListBox1.Items.Add(new ListItem(items.ElementAt(i)));
                }
            }

        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            if(ListBox1.SelectedItem !=null)         
               Response.Redirect("ForumPage.aspx?forumName=" + ListBox1.SelectedItem.Text);
        }
    }
}