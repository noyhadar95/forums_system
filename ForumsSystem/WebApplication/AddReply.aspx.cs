using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication.Communication;

namespace WebApplication
{
    public partial class AddReply : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string forumName = Request.QueryString["forumName"];
                string subforumName = Request.QueryString["subforumName"];
                Label myLabel = this.FindControl("Label1") as Label;
                myLabel.Text = forumName + "/" + subforumName;
            }
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            string forumName = Request.QueryString["forumName"];
            string subforumName = Request.QueryString["subforumName"];
            string thread = Request.QueryString["thread"];
            string post = Request.QueryString["post"];
            string title = TextBox1.Text;
            string content = TextBox2.Text;
            ICL cl = new CL();
            if ((title == "" || title == null) && (content == "" || content == null))
            {
                Page.ClientScript.RegisterStartupScript(
             this.GetType(),
             "Scripts",
             "<script language='javascript'>alert('" + "pleae enter title or content" + "');</script>");
                return;
            }
            cl.AddReply(forumName, subforumName, int.Parse(thread), (string)Session["Data"],
                int.Parse(post), title, content);
            Response.Redirect("ThreadPage.aspx?forumName=" + forumName +
                "&subforumName=" + subforumName + "&thread=" + thread);
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            string forumName = Request.QueryString["forumName"];
            string subforumName = Request.QueryString["subforumName"];
            string thread = Request.QueryString["thread"];
            Response.Redirect("ThreadPage.aspx?forumName=" + forumName +
                "&subforumName=" + subforumName+"&thread="+thread);
        }
    }
}