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
         //   if (!Page.IsPostBack)
          //  {
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
                    MyTreeNode node = new MyTreeNode(post.Title, post.Content, post.GetId());
                    TreeView1.Nodes.Add(node);
                    node.enableButtonSave(false);
                    CreateNested(post, node);
                }
                TreeView1.NodeStyle.BorderColor = System.Drawing.Color.Black;
                TreeView1.NodeStyle.BorderWidth = 1;
                TreeView1.NodeStyle.HorizontalPadding = 3;
                TreeView1.NodeStyle.VerticalPadding = 2;
                TreeView1.NodeStyle.Width = 600;
                TreeView1.DataBind();
        //    }
                

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
                MyTreeNode child = new MyTreeNode(reply.Title ,reply.Content,reply.GetId());
                node.ChildNodes.Add(child);
                child.enableButtonSave(false);
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

        public void RaisePostBackEvent(string eventArgument)
        {
            Page page = HttpContext.Current.CurrentHandler as Page;
            ClientScriptManager csm = page.ClientScript;
            string[] stringSeparators = new string[] { "@" };
            string[] result;
            result = eventArgument.Split(stringSeparators, StringSplitOptions.None);
            int idarg = int.Parse(result[1]);
            TreeNodeCollection col = TreeView1.Nodes;
            
            if (result[0] == "Save")
            {
                MyTreeNode selected = findNodeByPostId(col, idarg);
                csm.RegisterStartupScript(
                 this.GetType(),
                 "Scripts",
                 "<script language='javascript'>alert('" + "title: " + "selected.GetTitle()" + " content: " + "selected.GetBody()" + "');</script>");
            }
            else
            {
                MyTreeNode selected = findNodeByPostId(col, idarg);
                if (selected == null)
                {
                    csm.RegisterStartupScript(
                     this.GetType(),
                     "Scripts",
                      "<script language='javascript'>alert('" + "an error occured " + "');</script>");
                    return;
                }
                                
                csm.RegisterStartupScript(
                 this.GetType(),
                 "Scripts",
                 "<script language='javascript'>alert('" + "click on reply " + idarg + "');</script>");
                
                MyTreeNode newNode = new MyTreeNode("", "");
                selected.ChildNodes.Add(newNode);
                newNode.enableTextBoxes();
                newNode.enableButtonSave(true);
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