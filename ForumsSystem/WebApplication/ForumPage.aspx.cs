using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication.Communication;
using WebApplication.Resources.UserManagement.DomainLayer;

namespace WebApplication
{
    public partial class ForumPage : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string forumName = Request.QueryString["forumName"];
                Label myLabel = this.FindControl("Label1") as Label;
                myLabel.Text = forumName;
                ListBox1.Items.Clear();
                ICL cl = new CL();
                List<string> items = cl.GetSubForumsList(forumName);
                for (int i = 0; i < items.Count; i++)
                {
                    ListBox1.Items.Add(new ListItem(items.ElementAt(i)));
                }
                LabelLogin.Visible = false;
                LabelSession.Visible = false;
                BtnLogout.Visible = false;
                BtnLogout.Enabled = false;
                if(Session["Data"]== null)
                    Session["Data"] = "";
            }
            if (Session["Data"].Equals(""))
            {
                LabelLogin.Visible = false;
                LabelSession.Visible = false;
                BtnLogout.Visible = false;
                BtnLogout.Enabled = false;
            }
            else
            {
                LabelUserName.Visible = false;
                LabelPassword.Visible = false;
                LabelSessionKey.Visible = false;
                TextBox1.Visible = false;
                TextBox2.Visible = false;
                TextBox3.Visible = false;
                Button1.Visible = false;
                Button1.Enabled = false;
                LabelLogin.Text = "logged in as " + Session["Data"];
                LabelSession.Text = "session " + Session["Session"];
                LabelSession.Visible = true;
                LabelLogin.Visible = true;
                BtnLogout.Visible = true;
                BtnLogout.Enabled = true;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Tuple<User, string> user=null;
            string forumName = Request.QueryString["forumName"];
            string userName = TextBox1.Text;
            string password = TextBox2.Text;
            string session = TextBox3.Text;
            if (userName == "" || password == "")
            {
                userName = "";
                displayMessage("user name or password inncorrect");
                return;
            }
            ICL cl = new CL();
            if(session=="")
                user = cl.MemberLogin(forumName, userName, password);
            else
            {
                user = cl.MemberLogin(forumName, userName, password,session);
            }
            if(user.Item1 == null)
            {
                userName = "";
                if(user.Item2 == "-1")
                    displayMessage("the password has expired");
                if (user.Item2 == "-2")
                    displayMessage("your user has been deactivated");
                else
                    displayMessage("user name or password inncorrect");
                return;
            }
            LabelUserName.Visible = false;
            LabelPassword.Visible = false;
            LabelSessionKey.Visible = false;
            TextBox1.Visible = false;
            TextBox2.Visible = false;
            TextBox3.Visible = false;
            Button1.Visible = false;
            Button1.Enabled = false;
            LabelLogin.Text = "logged in as " + userName;
            LabelLogin.Visible = true;
            LabelSession.Text = "session " + Session["Session"];
            LabelSession.Visible = true;
            BtnLogout.Visible = true;
            BtnLogout.Enabled = true;

            Session["Data"] = userName;
            Session["Session"] = user.Item2;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string forumName = Request.QueryString["forumName"];
            if (ListBox1.SelectedItem != null)
                Response.Redirect("SubForumPage.aspx?forumName="+forumName+"&subforumName=" + ListBox1.SelectedItem.Text+"&session="+Session["Session"]);
        }

        private void displayMessage(string Message)
        {
            Page.ClientScript.RegisterStartupScript(
             this.GetType(),
             "Scripts",
             "<script language='javascript'>alert('" + Message + "');</script>");
        }

        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            ICL cl = new CL();
            cl.MemberLogout(Request.QueryString["forumName"], (string)Session["Data"]);
            LabelUserName.Visible = true;
            LabelPassword.Visible = true;
            LabelSessionKey.Visible = true;
            TextBox1.Visible = true;
            TextBox2.Visible = true;
            TextBox3.Visible = true;
            Button1.Visible = true;
            Button1.Enabled = true;
            LabelLogin.Visible = false;
            LabelSession.Visible = false;
            BtnLogout.Visible = false;
            BtnLogout.Enabled = false;

            Session["Data"] = "";
            Session["Session"] = "";
        }
    }
}