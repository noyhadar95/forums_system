using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication.Communication;
using WebApplication.Resources.ForumManagement.DomainLayer;
using WebApplication.Resources.UserManagement.DomainLayer;

namespace WebApplication
{
    public partial class ThreadPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string forumName = Request.QueryString["forumName"];
                string subforumName = Request.QueryString["subforumName"];
                string threadID = Request.QueryString["thread"];
                ICL cl = new CL();
                Label myLabel = this.FindControl("Label1") as Label;
                myLabel.Text = forumName + "/" + subforumName;

                List<Post> posts = cl.GetPosts(forumName, subforumName, int.Parse(threadID));

                
                var tree = sender as TreeView;
                foreach (Post post in posts)
                {
                    MyTreeNode node = new MyTreeNode("<b>"+post.Title + "</b><br/>" + post.Content+"<br/>");
                    TreeView1.Nodes.Add(node);

                    CreateNested(post, node);
                }
                TreeView1.NodeStyle.BorderColor = System.Drawing.Color.Black;
                TreeView1.NodeStyle.BorderWidth = 1;
                TreeView1.NodeStyle.HorizontalPadding = 3;
                TreeView1.NodeStyle.VerticalPadding = 2;
                TreeView1.NodeStyle.Width = 600;
                
               
                
            }
            if (Session["Data"].Equals(""))
            {
                LabelLogin.Visible = false;
                BtnLogout.Visible = false;
                BtnLogout.Enabled = false;
            }
            else
            {
                LabelUserName.Visible = false;
                LabelPassword.Visible = false;
                TextBox1.Visible = false;
                TextBox2.Visible = false;
                Button1.Visible = false;
                Button1.Enabled = false;
                LabelLogin.Text = "logged in as " + Session["Data"];
                LabelLogin.Visible = true;
                BtnLogout.Visible = true;
                BtnLogout.Enabled = true;
            }
        }

        private void CreateNested(Post post, TreeNode node)
        {
            List<Post> replies = post.Replies;
            //node.Expanded = true;
            foreach (Post reply in replies)
            {
                MyTreeNode child = new MyTreeNode("<b>"+reply.Title + "</b><br/>" + reply.Content+"<br/>");
                node.ChildNodes.Add(child);

                CreateNested(reply, child);
            }
           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string forumName = Request.QueryString["forumName"];
            string userName = TextBox1.Text;
            string password = TextBox2.Text;
            if (userName == "" || password == "")
            {
                userName = "";
                displayMessage("user name or password inncorrect");
                return;
            }
            ICL cl = new CL();
            User user = cl.MemberLogin(forumName, userName, password);
            if (user == null)
            {
                userName = "";
                displayMessage("user name or password inncorrect");
                return;
            }
            LabelUserName.Visible = false;
            LabelPassword.Visible = false;
            TextBox1.Visible = false;
            TextBox2.Visible = false;
            Button1.Visible = false;
            Button1.Enabled = false;
            LabelLogin.Text = "logged in as " + userName;
            LabelLogin.Visible = true;
            BtnLogout.Visible = true;
            BtnLogout.Enabled = true;

            Session["Data"] = userName;
        }

        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            ICL cl = new CL();
            cl.MemberLogout(Request.QueryString["forumName"], (string)Session["Data"]);
            LabelUserName.Visible = true;
            LabelPassword.Visible = true;
            TextBox1.Visible = true;
            TextBox2.Visible = true;
            Button1.Visible = true;
            Button1.Enabled = true;
            LabelLogin.Visible = false;
            BtnLogout.Visible = false;
            BtnLogout.Enabled = false;

            Session["Data"] = "";
        }

        private void displayMessage(string Message)
        {
            Page.ClientScript.RegisterStartupScript(
             this.GetType(),
             "Scripts",
             "<script language='javascript'>alert('" + Message + "');</script>");
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            string forumName = Request.QueryString["forumName"];
            string subforumName = Request.QueryString["subforumName"];
            Response.Redirect("SubForumPage.aspx?forumName=" + forumName+"&subforumName="+subforumName);
        }
    }
}