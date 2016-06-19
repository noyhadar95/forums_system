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
    public partial class ThreadPage : System.Web.UI.Page, IPostBackEventHandler
    {


        protected void Page_Load(object sender, EventArgs e)
        {

            MyTreeNode node1 = new MyTreeNode();
            string forumName = Request.QueryString["forumName"];
            string subforumName = Request.QueryString["subforumName"];
            string threadID = Request.QueryString["thread"];
            ICL cl = new CL();
            Label myLabel = this.FindControl("Label1") as Label;
            myLabel.Text = forumName + "/" + subforumName;

            List<Post> posts = cl.GetPosts(forumName, subforumName, int.Parse(threadID));
            TreeView1.Nodes.Clear();

            var tree = sender as TreeView;

            foreach (Post post in posts)
            {
                MyTreeNode node;
                if (Session["Data"].Equals(""))
                    node = new MyTreeNode(post.Title, post.Content,post.Publisher.Username,post.GetId(),false);
                else
                    node = new MyTreeNode(post.Title, post.Content, post.Publisher.Username,post.GetId(), true);

                TreeView1.Nodes.Add(node);
                CreateNested(post, node);
            }
            TreeView1.NodeStyle.BorderColor = System.Drawing.Color.Black;
            TreeView1.NodeStyle.BorderWidth = 1;
            TreeView1.NodeStyle.HorizontalPadding = 3;
            TreeView1.NodeStyle.VerticalPadding = 2;
            TreeView1.NodeStyle.Width = 600;
            TreeView1.DataBind();
                

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
            node.Expanded = true;
            foreach (Post reply in replies)
            {
                MyTreeNode child;
                if (Session["Data"].Equals(""))
                    child = new MyTreeNode(reply.Title ,reply.Content, reply.Publisher.Username,reply.GetId(),false);
                else
                    child = new MyTreeNode(reply.Title, reply.Content, reply.Publisher.Username,reply.GetId(), true);
                node.ChildNodes.Add(child);
                CreateNested(reply, child);
            }
           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string forumName = Request.QueryString["forumName"];
            string subforumName = Request.QueryString["subforumName"];
            string thread = Request.QueryString["thread"];
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


            List<Post> posts = cl.GetPosts(forumName, subforumName, int.Parse(thread));
            TreeView1.Nodes.Clear();

            var tree = sender as TreeView;

            foreach (Post post in posts)
            {
                MyTreeNode node;
                node = new MyTreeNode(post.Title, post.Content, post.Publisher.Username, post.GetId(), true);

                TreeView1.Nodes.Add(node);
                CreateNested(post, node);
            }
        }

        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            string forumName = Request.QueryString["forumName"];
            string subforumName = Request.QueryString["subforumName"];
            string thread = Request.QueryString["thread"];
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

            List<Post> posts = cl.GetPosts(forumName, subforumName, int.Parse(thread));
            TreeView1.Nodes.Clear();

            var tree = sender as TreeView;

            foreach (Post post in posts)
            {
                MyTreeNode node;
                node = new MyTreeNode(post.Title, post.Content, post.Publisher.Username,post.GetId(), false);

                TreeView1.Nodes.Add(node);
                CreateNested(post, node);
            }
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

        public void RaisePostBackEvent(string eventArgument)
        {
            
            Page page = HttpContext.Current.CurrentHandler as Page;
            ClientScriptManager csm = page.ClientScript;
            string[] stringSeparators = new string[] { "@" };
            string[] result;
            result = eventArgument.Split(stringSeparators, StringSplitOptions.None);
            if (result[0] == "Reply")
            {
                int idarg = int.Parse(result[1]);
                string forumName = Request.QueryString["forumName"];
                string subforumName = Request.QueryString["subforumName"];
                string thread = Request.QueryString["thread"];
                Response.Redirect("AddReply.aspx?forumName=" + forumName + "&subforumName=" + subforumName +
                    "&thread=" + thread + "&post=" + idarg);
            }


        }

        private MyTreeNode findNodeById(TreeNodeCollection col, int id)
        {
            foreach (MyTreeNode node in col)
            {
                if (node.GetId() == id)
                    return node;
                else
                {
                    if (node.ChildNodes != null)
                    {
                        MyTreeNode res = findNodeById(node.ChildNodes, id);
                        if (res != null)
                            return res;
                    }
                }
            }
            return null;
        }

        private MyTreeNode findNodeByPostId(TreeNodeCollection collection, int id)
        {
            foreach(MyTreeNode node in collection)
            {
                if (node.GetPostId() == id)
                    return node;
                else
                {
                    if (node.ChildNodes != null)
                    {
                        MyTreeNode res = findNodeByPostId(node.ChildNodes, id);
                        if (res != null)
                            return res;
                    }
                }
            }
            return null;
        }


    }
}